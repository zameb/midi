using NAudio.Midi;

namespace Zameb.ChordFinder
{
    public class ChordFinder
    {
        public int Interval { get; set; } = 100;

        public IEnumerable<Chord> GetChords(IEnumerable<MidiEvent> events)
        {
            var chords = new List<Chord>();
            var currentEvents = new Dictionary<string, NoteEvent>();
            var previousChordName = "NA";
            foreach (var midiEvent in events)
            {
                if (midiEvent is NoteEvent midiNote)
                {
                    if (midiEvent?.CommandCode == MidiCommandCode.NoteOn)
                    {
                        if (midiNote.Velocity == 0)
                        {
                            currentEvents.Remove(midiNote.NoteName);
                        }
                        else
                        {
                            if (!currentEvents.ContainsKey(midiNote.NoteName))
                            {
                                currentEvents.Add(midiNote.NoteName, midiNote);
                            }
                        }
                    }
                    if (midiEvent?.CommandCode == MidiCommandCode.NoteOff)
                    {
                        currentEvents.Remove(midiNote.NoteName);
                    }
                }
                if (currentEvents.Count > 2 &&
                    midiEvent?.AbsoluteTime % Interval == 0)
                {
                    var chord = new Chord(currentEvents);
                    if (chord != null && previousChordName != chord.ChordName) 
                    { 
                        chords.Add(chord);
                        previousChordName = chord.ChordName;
                    }
                }
            }
            return chords;
        }
    }
}
