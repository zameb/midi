using Zameb.ChordFinder.App.Infra;

namespace Zameb.ChordFinder.App.ViewModel
{
    public class TrackInformationVm : BaseViewModel
    {
        public int TrackNumber { get; set; }
        public string TrackInformationText { get; set; } = string.Empty;
    }
}
