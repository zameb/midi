using NAudio.Midi;
using System.Linq;

namespace Zameb.ChordFinder
{
    public class Chord
    {
        private readonly Dictionary<string, NoteEvent> events;

        public IEnumerable<Note> Notes { get; set; } = default!;
        public string ChordName { get; set; } = default!;
        public long AbsoluteTime { get; set; }

        public Chord(Dictionary<string, NoteEvent> currentEvents)
        {
            events = currentEvents;
            GetNotes();
            ChordName = GetChordName();
            AbsoluteTime = currentEvents.Max(n => n.Value.AbsoluteTime);
        }

        public override string ToString()
        {
            return $"{ChordName}: [{string.Join(",", Notes.Select(n => n.NoteName))}]";
        }

        private void GetNotes()
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
            Notes = notes;
        }

        private string GetChordName()
        {
            var root = new Note("C1");
            var chordFound = "NA";
            var totalNotes = Notes.Count();
            var fullMatch = false;
            var bestMatch = 0;

            foreach (var chord in ChordTable.Chords)
            {
                foreach (var currentRoot in Notes)
                {
                    var matches = 0;
                    root = currentRoot;
                    var noteValues = Notes.Select(n => GetTransposedNoteValue(currentRoot, n));

                    foreach (var noteValue in noteValues)
                    {
                        if (chord.Value.Contains((Intervals)noteValue))
                        {
                            matches++;
                            if (matches > bestMatch)
                            {
                                bestMatch = matches;
                                chordFound = chord.Key;
                                if (matches == totalNotes)
                                {
                                    fullMatch = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (fullMatch) break;
                }
                if (fullMatch) break;
            }

            return root.NoteName + chordFound;
        }

        private int GetTransposedNoteValue(Note root, Note note)
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
