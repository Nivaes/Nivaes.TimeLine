namespace Nivaes.TimeLine.Droid
{
    using System.Windows.Input;

    public interface ITimeLineItem
    {
        string MarkerText { get; set; }

        bool ShowMarker { get; set; }

        int IconResource { get; set; }

        ICommand Click { get; set; }

        ICommand LongClick { get; set; }
    }
}
