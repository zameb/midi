namespace Zameb.ChordFinder
{
    public enum Intervals
    {
        // https://en.wikipedia.org/wiki/Interval_(music)
        // C, Db, D, Eb, E, F, F#, G, Ab, A, Bb, B, C
        // 0, 1   2  3   4  5  6   7  8   9  10  11 12
        Root, Base, Unison = 0,
        MinorSecond = 1,
        MajorSecond, Second = 2,
        MinorThird = 3,
        MajorThird = 4,
        PerfectFourth, Fourth = 5,
        DiminishedFifth = 6,
        PerfectFifth = 7,
        AugmentedFifth = 8,
        MajorSixth = 9,
        MinorSeventh = 10,
        MajorSeventh = 11,

        MinorNinth = 13, MajorNinth = 14,
        PerfectEleventh = 17,
        MinorThirteenth = 20, MajorThirteenth = 21,
    }

    public class ChordStructure
    {
        public static readonly Dictionary<string, Intervals[]> ChordComponents = new()
        {
            { "5", new[] { Intervals.Root, Intervals.PerfectFifth } },
            { "M", new[] { Intervals.Root, Intervals.MajorThird, Intervals.PerfectFifth } },
            { "m", new[] { Intervals.Root, Intervals.MinorThird, Intervals.PerfectFifth } },
            { "7", new[] { Intervals.Root, Intervals.MajorThird, Intervals.PerfectFifth, Intervals.MinorSeventh } },
            { "m7", new[] { Intervals.Root, Intervals.MinorThird, Intervals.PerfectFifth, Intervals.MinorSeventh } },
            { "M7", new[] { Intervals.Root, Intervals.MajorThird, Intervals.PerfectFifth, Intervals.MajorSeventh } },
            { "sus4", new[] { Intervals.Root, Intervals.Fourth, Intervals.PerfectFifth } },
        };
    }
}
