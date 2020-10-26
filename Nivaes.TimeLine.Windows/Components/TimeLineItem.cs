namespace Nivaes.TimeLine.Windows
{
    using System.Windows.Input;
    using Microsoft.UI.Xaml;

    public class TimeLineItem
        : ITimeLineItem
    {
        public string MarkerText { get; set; } = string.Empty;

        public bool ShowMarker { get; set; }

        public DependencyObject? Icon { get; set; }

        public ICommand? Click { get; set; }

        public ICommand? LongClick { get; set; }
    }
}
