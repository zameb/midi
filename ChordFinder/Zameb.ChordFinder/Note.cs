using System.Text.RegularExpressions;

namespace Zameb.ChordFinder
{
    public class Note
    {
        public string NoteName { get; set; } = default!;
        public string Root { get; set; } = default!;
        public string Accident { get; set; } = default!;
        public int Octave { get; set; }
        public int NoteValue { get; set; }
        public string Representation { get; set; } = default!;

        public Note(string note) 
        {
            var noteParts = Regex.Match(note, "([A-G])([#b]?)(\\d+)");
            if (noteParts.Groups.Count > 3)
            {
                var tone = noteParts.Groups[1].Value;
                Accident = noteParts.Groups[2].Value;
                NoteName = tone + Accident;
                NoteValue = "C D EF G A B".IndexOf(tone);
                NoteValue += Accident == "#" ? 1 : Accident == "b" ? -1 : 0;
                Octave = int.Parse(noteParts.Groups[3].Value);
                Representation = GetRepresentation();
            }
            else
            {
                Representation = "X";
            }
        }

        public override string ToString()
        {
            return NoteName;
        }

        private string GetRepresentation()
        {
            //return $"{Octave:00}{NoteValue:00}";
            return $"{NoteValue:00}";
        }
    }
}
