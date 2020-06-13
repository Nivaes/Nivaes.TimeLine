namespace Nivaes.TimeLine.Droid
{
    using Android.Content;
    using Android.Graphics.Drawables;
    using Android.Support.Graphics.Drawable;
    using Android.Support.V4.Content.Res;

    internal sealed class TimeLineIconMarkerView
        : TimeLineMarkerView
    {
        #region Properties
        private int mIconResource;

        internal int IconResource
        {
            get => mIconResource;
            set
            {
                if (mIconResource != value)
                {
                    mIconResource = value;

                    var vectorDrawable = VectorDrawableCompat.Create(base.Context.Resources, value, null);
                    ((TimeLineIconMarkerDrawable)base.Marker).VectorDrawable = vectorDrawable;

                    if(vectorDrawable == null)
                    {
                        var drawable = ResourcesCompat.GetDrawable(base.Context.Resources, value, null);
                        if (drawable is BitmapDrawable bitmapDrawable)
                            ((TimeLineIconMarkerDrawable)base.Marker).BitmapDrawable = bitmapDrawable;
                    }
                }
            }
        }
        #endregion

        #region Constructors
        public TimeLineIconMarkerView(Context context)
            : base(context)
        {
            Marker = new TimeLineIconMarkerDrawable(base.Context.Resources);
        }
        #endregion
    }
}
