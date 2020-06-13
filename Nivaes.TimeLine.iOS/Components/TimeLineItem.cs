namespace Nivaes.TimeLine.iOS
{
    using System.Windows.Input;
    using UIKit;

    public class TimeLineItem
        : ITimeLineItem
    {
        public string MarkerText { get; set; }

        public bool ShowMarker { get; set; }

        public UIImage Icon { get; set; }

        public ICommand Click { get; set; }

        public ICommand LongClick { get; set; }
    }
}
