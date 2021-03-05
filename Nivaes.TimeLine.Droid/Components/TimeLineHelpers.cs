namespace Nivaes.TimeLine.Droid
{
    using Android.Content;
    using Android.Content.Res;
    using Android.Util;

    internal static class TimeLineHelpers
    {
        internal static float DpToPx(float dp, Context context)
        {
            return DpToPx(dp, context.Resources!);
        }

        internal static float DpToPx(float dp, Resources resources)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, resources.DisplayMetrics);
        }
    }
}
