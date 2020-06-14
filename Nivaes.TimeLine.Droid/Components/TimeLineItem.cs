namespace Nivaes.TimeLine.Droid
{
    using System.Windows.Input;

    public class TimeLineItem
        : ITimeLineItem
    {
        public string MarkerText { get; set; } = string.Empty;

        public bool ShowMarker { get; set; }

        public int IconResource { get; set; }

        public ICommand? Click { get; set; }      

        public ICommand? LongClick { get; set; }
    }
}
