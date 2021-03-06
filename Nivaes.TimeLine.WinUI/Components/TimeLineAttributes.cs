﻿namespace Nivaes.TimeLine.WinUI
{
    using System.Drawing;

    internal class TimeLineAttributes
    {
        internal Color LineColor { get; set; }
        internal Color MarketColor { get; set; }
        internal float MarkerSize { get; set; }
        internal float IconSize { get; set; }
        internal float MarginSize { get; set; }
        internal TimeLineOrientation LineOrientation { get; set; }
        internal bool MarkerInCenter { get; set; }
        internal int TimeLinePositioin { get; set; }
        internal TimeLineMarkerType MarkerType { get; set; }
    }
}
