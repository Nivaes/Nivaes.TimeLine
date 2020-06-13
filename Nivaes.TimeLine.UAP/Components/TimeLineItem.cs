namespace Nivaes.TimeLine.UAP
{
    using System.Windows.Input;
    using Windows.UI.Xaml;

    public class TimeLineItem
        : ITimeLineItem
    {
        public string MarkerText { get; set; }

        public bool ShowMarker { get; set; }

        public DependencyObject Icon { get; set; }

        public ICommand Click { get; set; }

        public ICommand LongClick { get; set; }
    }
}
