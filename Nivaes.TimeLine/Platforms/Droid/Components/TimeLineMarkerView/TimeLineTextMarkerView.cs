namespace Nivaes.TimeLine.Droid
{
    using Android.Content;

    internal sealed class TimeLineTextMarkerView
        : TimeLineMarkerView
    {
        #region Properties
        internal string MarkerText
        {
            get => ((TimeLineTextMarkerDrawable)base.Marker).Text;
            set => ((TimeLineTextMarkerDrawable)base.Marker).Text = value;
        }
        #endregion

        #region Constructors
        public TimeLineTextMarkerView(Context context)
            : base(context)
        {
            Marker = new TimeLineTextMarkerDrawable(base.Context!.Resources!);
        }
        #endregion
    }
}
