using UIKit;

namespace Nivaes.TimeLine.UIKitLib
{
    internal class TimeLineAttributes
    {
        internal UIColor? LineColor { get; set; }
        internal UIColor? MarketColor { get; set; }
        internal float MarkerSize { get; set; }
        internal float IconSize { get; set; }
        internal float MarginSize { get; set; }
        internal TimeLineOrientation LineOrientation { get; set; }
        internal bool MarkerInCenter { get; set; }
        internal int TimeLinePositioin { get; set; }
        internal TimeLineMarkerType? MarkerType { get; set; }
    }
}
