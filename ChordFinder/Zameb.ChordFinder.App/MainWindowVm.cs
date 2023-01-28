using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows.Input;
using Zameb.ChordFinder.App.Infra;

namespace Zameb.ChordFinder.App
{
    public class MainWindowVm : BaseViewModel
    {
        public ICommand OpenSongCommand { get; }
        public string? SongName { get; set; }
        public IEnumerable<Chord>? Chords { get; set; }

        public MainWindowVm()
        {
            OpenSongCommand = new DelegateCommand(OpenSong);
        }

        private void OpenSong(object? obj)
        {
            var dialog = new OpenFileDialog();

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
                Chords = manager.ReadChords();
                OnPropertyChanged(nameof(Chords));
            }
        }
    }
}
