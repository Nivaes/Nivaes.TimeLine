namespace Nivaes.TimeLine.Droid
{
    using Android.Content;

    internal sealed class TimeLinePositionMarkerView
        : TimeLineMarkerView
    {
        #region Constructors
        public TimeLinePositionMarkerView(Context context)
            : base(context)
        {
            Marker = new TimeLinePositionMarkerDrawable(base.Context.Resources);
        }
        #endregion
    }
}
