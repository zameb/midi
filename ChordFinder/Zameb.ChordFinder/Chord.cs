namespace Zameb.ChordFinder
{
    public class Chord
    {
        public string[]? NoteNames { get; set; }
        public string ChordName { get; set; } = default!;
        public long AbsoluteTime { get; set; }

        public Chord() { }

        public override string ToString()
        {
            return string.Join(",", NoteNames);
        }
    }
}
