using NAudio.Midi;

namespace Zameb.ChordFinder
{
    public class ChordFinder
    {
        public IEnumerable<Chord> GetChords(IList<MidiEvent> events)
        {
            foreach (var midiEvent in events)
            {
                if (midiEvent?.CommandCode == MidiCommandCode.NoteOn)
                {
                    var midiNote = midiEvent as NoteOnEvent;
                    if (midiNote != null)
                    {
                    }
                }
            }
            return new List<Chord>();
        }
    }
}
