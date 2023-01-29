using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Zameb.ChordFinder.App.Infra;

namespace Zameb.ChordFinder.App
{
    public class MainWindowVm : BaseViewModel
    {
        public ICommand OpenSongCommand { get; }
        public string? SongName { get; set; }
        public IEnumerable<string>? Tracks { get; set; }
        public IEnumerable<Chord>? Chords { get; set; }

        public MainWindowVm()
        {
            OpenSongCommand = new DelegateCommand(OpenSong);
        }

        private void OpenSong(object? obj)
        {
            //TODO: Inyectar config
            var path = Path.GetDirectoryName(Environment.ProcessPath);
            var builder = new ConfigurationBuilder().SetBasePath(path);
            builder.AddYamlFile("App.yml", optional: false);
            var config = builder.Build();

            var dialog = new OpenFileDialog
            {
                InitialDirectory = config["InitialDirectory"],
                DefaultExt = "Midi files (*.mid)|*.mid|Karaoke files (*.kar)|*.kar|All files (*.*)|*.*"
            };

            dialog.ShowDialog();
            SongName = dialog.FileName;
            OnPropertyChanged(nameof(SongName));
            ProcessSong();
        }

        private void ProcessSong()
        {
            if (!string.IsNullOrEmpty(SongName))
            {
                var manager = new MidiFileManager();
                manager.Open(SongName);
                Tracks = manager.ReadTrackInfo();
                Chords = manager.ReadChords();
                OnPropertyChanged(nameof(Chords));
                OnPropertyChanged(nameof(Tracks));
            }
        }
    }
}
