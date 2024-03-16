namespace Nivaes.TimeLine.Droid
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Input;

    public abstract class TimeLineItem
        : ITimeLineItem
    {
        protected TimeLineItem()
        {
        }

        public string MarkerText { get; set; } = string.Empty;

        public bool ShowMarker { get; set; }

        public int IconResource { get; set; }

        [AllowNull]
        public ICommand? Click { get; set; } = null;

        [AllowNull]
        public ICommand? LongClick { get; set; } = null;
    }
}
