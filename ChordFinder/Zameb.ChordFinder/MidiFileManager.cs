using NAudio.Midi;
using System.Text;

namespace Zameb.ChordFinder
{
    public class MidiFileManager
    {
        private MidiFile? midiFile;

        public void Open(string fileName)
        {
            midiFile = new MidiFile(fileName, false);
        }

        public IEnumerable<Chord>? ReadChords()
        {
            if (midiFile != null)
            {
                var chordFinder = new ChordFinder();
                return chordFinder.GetChords(midiFile.Events[3].ToList());
            }
            return null;
        }

        public IEnumerable<string>? ReadTrackInfo()
        {
            var trackInfoList = new List<string>();
            if (midiFile != null)
            {
                foreach (var track in midiFile.Events)
                {
                    var trackIndex = 0;
                    var info = new StringBuilder();
                    do
                    {
                        try
                        {
                            var trackEvent = track[trackIndex];
                            if (trackEvent.AbsoluteTime > 0) break;
                            if (IsPrintableEvent(trackEvent))
                            {
                                info.AppendLine(trackEvent.ToString());
                            }
                            trackIndex++;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    while (true) ;
                    trackInfoList.Add(info.ToString());
                }
            }
            return trackInfoList;

            static bool IsPrintableEvent(MidiEvent trackEvent)
            {
                if (trackEvent.CommandCode == MidiCommandCode.MetaEvent)
                {
                    var metaEvent = trackEvent as MetaEvent;
                    return metaEvent?.MetaEventType == MetaEventType.SequenceTrackName;
                }
                return trackEvent.CommandCode == MidiCommandCode.PatchChange;
            }
        }
    }
}
