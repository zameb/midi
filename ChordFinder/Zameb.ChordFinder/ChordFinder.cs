using NAudio.Midi;
using System.Text.RegularExpressions;

namespace Zameb.ChordFinder
{
    public class ChordFinder
    {
        public int Interval { get; set; } = 100;

        public IEnumerable<Chord> GetChords(IEnumerable<MidiEvent> events)
        {
            var chords = new List<Chord>();
            var currentNotes = new Dictionary<string, NoteEvent>();
            foreach (var midiEvent in events)
            {
                if (midiEvent is NoteEvent midiNote)
                {
                    if (midiEvent?.CommandCode == MidiCommandCode.NoteOn)
                    {
                        if (midiNote.Velocity == 0)
                        {
                            currentNotes.Remove(midiNote.NoteName);
                        }
                        else
                        {
                            if (!currentNotes.ContainsKey(midiNote.NoteName))
                            {
                                currentNotes.Add(midiNote.NoteName, midiNote);
                            }
                        }
                    }
                    if (midiEvent?.CommandCode == MidiCommandCode.NoteOff)
                    {
                        currentNotes.Remove(midiNote.NoteName);
                    }
                }
                if (midiEvent?.AbsoluteTime % Interval == 0)
                {
                    var chord = GuessChord(currentNotes);
                    if (chord != null) 
                    { 
                        chords.Add(chord); 
                    }
                }
            }
            return chords;
        }

        private static Chord? GuessChord(Dictionary<string, NoteEvent> currentNotes)
        {
            if (currentNotes.Count > 2)
            {
                return new Chord()
                {

                    ChordName = GetChordName(currentNotes.Select(n => n.Value.NoteName)),
                    NoteNames = currentNotes.Select(n => n.Value.NoteName).ToArray(),
                    AbsoluteTime = currentNotes.Max(n => n.Value.AbsoluteTime)
                };
            }
            return null;
        }

        private static string GetChordName(IEnumerable<string> notes)
        {
            var sortedNotes = new Dictionary<string, string>();
            foreach (var note in notes) 
            {
                var hash = GetHash(note);
                if (!sortedNotes.ContainsKey(hash))
                {
                    sortedNotes.Add(hash, note);
                }
            }
            return "Paco";
        }

        private static string GetHash(string note)
        {
            var noteParts = Regex.Match(note, "([A-G])([#b]?)(\\d+)");
            var tone = noteParts.Groups[1].Value;
            var numericTone = "C D EF G A B".IndexOf(tone);
            var accident = noteParts.Groups[2].Value;
            var octave = int.Parse(noteParts.Groups[3].Value);
            numericTone += accident == "#" ? 1 : accident == "b" ? -1 : 0;
            return $"{octave:00}{numericTone:00}";
        }
    }
}
