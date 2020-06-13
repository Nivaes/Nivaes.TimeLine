namespace Nivaes.TimeLine.iOS
{
    using System.Windows.Input;
    using UIKit;

    public interface ITimeLineItem
    {
        string MarkerText { get; set; }

        bool ShowMarker { get; set; }

        UIImage Icon { get; set; }

        ICommand Click { get; set; }

        ICommand LongClick { get; set; }
    }
}
