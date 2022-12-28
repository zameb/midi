using NAudio.Midi;

namespace Zameb.ChordFinder
{
    public class MidiFileManager
    {
        private MidiFile? midiFile;

        public void Open(string fileName)
        {
            midiFile = new MidiFile(fileName, false);
        }

        public void GetChords()
        {
            var chordFinder = new ChordFinder();
            chordFinder.GetChords(midiFile?.Events[3].ToList());
        }
    }
}
