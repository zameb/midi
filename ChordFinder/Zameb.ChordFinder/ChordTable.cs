namespace Zameb.ChordFinder
{
    public class ChordTable
    {
        public static readonly Dictionary<string, int[]> ChordStructures = new()
        {
            { "M", new int[] { 1, 5, 8 } },
            { "m", new int[] { 1, 4, 8 } },
            { "7", new int[] { 1, 5, 8, 11 } },
            { "m7", new int[] { 1, 4, 8, 11} },
            { "M7", new int[] { 1, 5, 8, 12 } },
            { "sus4", new int[] { 1, 6, 8 } },
        };
    }
}

// C, Db, D, Eb, E, F, F#, G, Ab, A, Bb, B, C
// 1   2  3   4  5  6   7  8  9  10  11  12 1
