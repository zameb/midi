using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Zameb.ChordFinder.App.Infra;
using Zameb.ChordFinder.App.ViewModel;

namespace Zameb.ChordFinder.App
{
    public class MainWindowVm : BaseViewModel
    {
        private string? initialDirectory = string.Empty;

        private readonly MidiFileManager manager;

        public string? SongName { get; set; }
        public IEnumerable<TrackInformationVm>? TrackInformationList { get; set; }
        public IEnumerable<Chord>? ChordList { get; set; }

        public ICommand OpenSongCommand { get; }
        public ICommand SelectTrackCommand { get; }

        public MainWindowVm()
        {
            manager = new MidiFileManager();
            //TODO: Inyectar config
            var path = Path.GetDirectoryName(Environment.ProcessPath);
            var builder = new ConfigurationBuilder().SetBasePath(path);
            builder.AddYamlFile("appconfig.yml", optional: false);
            var config = builder.Build();
            initialDirectory = config["InitialDirectory"];
            OpenSongCommand = new DelegateCommand(OpenSong);
            SelectTrackCommand = new DelegateCommand(SelectTrack);
        }

        private void SelectTrack(object? obj)
        {
            var trackInformation = obj as TrackInformationVm;
            if (trackInformation != null)
            {
                ChordList = manager.ReadChords(trackInformation.TrackNumber);
                OnPropertyChanged(nameof(ChordList));
            }
        }

        private void OpenSong(object? obj)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = initialDirectory,
                DefaultExt = "Midi files (*.mid)|*.mid|Karaoke files (*.kar)|*.kar|All files (*.*)|*.*"
            };

            dialog.ShowDialog();
            SongName = dialog.FileName;
            if (SongName == null) return;
            initialDirectory = Path.GetDirectoryName(SongName);
            OnPropertyChanged(nameof(SongName));
            ProcessSong();
        }

        private void ProcessSong()
        {
            if (!string.IsNullOrEmpty(SongName))
            {
                manager.Open(SongName);
                TrackInformationList = GetTrackInformationList(manager);
                OnPropertyChanged(nameof(TrackInformationList));
            }
        }

        private static IEnumerable<TrackInformationVm>? GetTrackInformationList(MidiFileManager manager)
        {
            var trackInformationList = manager.ReadTrackInfo();
            return trackInformationList?.Select((ti, i) =>
                new TrackInformationVm { TrackNumber = i, TrackInformationText = ti });
        }
    }
}
