using System.Windows.Input;

namespace Nivaes.TimeLine.UIKitLib
{
    public class TimeLineItem
        : ITimeLineItem
    {
        public string MarkerText { get; set; } = string.Empty;

        public bool ShowMarker { get; set; }

        public UIImage? Icon { get; set; }

        public ICommand? Click { get; set; }

        public ICommand? LongClick { get; set; }
    }
}
