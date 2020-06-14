namespace Nivaes.TimeLine.UAP
{
    using System.Windows.Input;
    using Windows.UI.Xaml;

    public interface ITimeLineItem
    {
        string MarkerText { get; set; }

        bool ShowMarker { get; set; }

        DependencyObject? Icon { get; set; }

        ICommand? Click { get; set; }

        ICommand? LongClick { get; set; }
    }
}
