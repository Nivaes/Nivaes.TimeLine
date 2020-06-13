namespace Nivaes.TimeLine.Droid
{
    using Android.Graphics;

    internal class TimeLineAttributes
    {
        internal Color LineColor { get; set; }
        internal Color MarketColor { get; set; }
        internal int MarkerSize { get; set; }
        internal int LineSize { get; set; }
        internal TimeLineOrientation LineOrientation { get; set; }
        internal int LinePadding { get; set; }
        internal bool MarkerInCenter { get; set; }
        internal int TimeLinePositioin { get; set; }
        internal TimeLineMarkerType MarkerType { get; set; }
    }
}
