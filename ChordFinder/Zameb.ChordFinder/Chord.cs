using NAudio.Midi;
using System.Linq;

namespace Zameb.ChordFinder
{
    public class Chord
    {
        private readonly Dictionary<string, NoteEvent> events;

        public IEnumerable<Note> Notes { get; set; }
        public string ChordName { get; set; } = default!;
        public long AbsoluteTime { get; set; }

        public Chord(Dictionary<string, NoteEvent> currentEvents) 
        {
            events = currentEvents;
            Sort();
            ChordName = GetChordName();
            AbsoluteTime = currentEvents.Max(n => n.Value.AbsoluteTime);
        }

        public override string ToString()
        {
            return $"{ChordName}: [{string.Join(",", Notes.Select(n => n.NoteName))}]";
        }

        private void Sort()
        {
            var notes = new List<Note>();
            foreach (var noteEvent in events)
            {
                var note = new Note(noteEvent.Value.NoteName);
                var hash = note.Hash;
                if (!notes.Any(n => n.Hash == hash))
                {
                    notes.Add(note);
                }
            }
            Notes = notes.OrderBy(n => n.Hash);
        }

        private string GetChordName()
        {
            var root = Notes.First();

            var noteValues = Notes.Select(n => GetTransposedNoteValue(root, n));

            var total = noteValues.Count();
            var bestMatch = 0;
            var chordFound = "NA";
            foreach (var chord in ChordTable.Chords)
            {
                var matches = 0;
                foreach (var noteValue in noteValues)
                {
                    if (chord.Value.Contains((Intervals)noteValue))
                    {
                        matches++;
                        if (matches > bestMatch)
                        {
                            bestMatch = matches;
                            chordFound = chord.Key;
                        }
                    }
                }
            }
            return root.NoteName + chordFound;
        }

        private object GetTransposedNoteValue(Note root, Note note)
        {
            var transposedNoteValue = note.NoteValue - root.NoteValue;
            if (transposedNoteValue < 0)
            {
                transposedNoteValue += 12;
            }
            return transposedNoteValue;
        }
    }
}
