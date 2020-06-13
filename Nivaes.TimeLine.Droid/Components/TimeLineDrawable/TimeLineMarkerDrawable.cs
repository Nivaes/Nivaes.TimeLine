namespace Nivaes.TimeLine.Droid
{
    using Android.Content.Res;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Util;

    /// <summary>Text drawable.</summary>
    internal abstract class TimeLineMarkerDrawable
        : Drawable
    {
        #region Properties       
        private Paint mPaintMarket;

        internal Color Color { get; set; }

        internal TimeLinePositionType MarketPosition { get; set; }

        private int mIntrinsicHeight;
        internal float LineStroke { get; private set; }
        #endregion

        public TimeLineMarkerDrawable(Resources res)
        {
            mPaintMarket = new Paint(PaintFlags.AntiAlias | PaintFlags.LinearText);
           
            LineStroke = TypedValue.ApplyDimension(ComplexUnitType.Dip, 1, res.DisplayMetrics);          
            mIntrinsicHeight = mPaintMarket.GetFontMetricsInt(null);
        }

        public override void Draw(Canvas canvas)
        {
            Rect bounds = base.Bounds;

            if (MarketPosition != TimeLinePositionType.NoMarket)
            {
                mPaintMarket.SetStyle(Paint.Style.FillAndStroke);
                mPaintMarket.Color = Color;
            }
            else
            {
                mPaintMarket.SetStyle(Paint.Style.Stroke);
                mPaintMarket.Color = Color;
            }

            mPaintMarket.StrokeWidth = LineStroke;

            canvas.DrawOval(bounds.Left + LineStroke, bounds.Top + LineStroke, bounds.Right - LineStroke, bounds.Bottom - LineStroke, mPaintMarket);
        }

        public override int Opacity => mPaintMarket.Alpha;

        public override int IntrinsicHeight => mIntrinsicHeight;

        public override void SetAlpha(int alpha)
        {
            mPaintMarket.Alpha = alpha;
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
            mPaintMarket.SetColorFilter(colorFilter);
        }
    }
}
