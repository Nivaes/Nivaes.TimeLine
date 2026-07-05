namespace Nivaes.TimeLine.Droid
{
    using Android.Content.Res;
    using Android.Graphics;

    /// <summary>Position drawable.</summary>
    internal sealed class TimeLinePositionMarkerDrawable
        : TimeLineMarkerDrawable
    {
        #region Properties
        private Paint mPaintPosition;
        #endregion

        public TimeLinePositionMarkerDrawable(Resources res)
            : base(res)
        {
            mPaintPosition = new Paint(PaintFlags.AntiAlias);
        }

        public override void Draw(Canvas? canvas)
        {
            Rect bounds = base.Bounds;

            if (MarketPosition != TimeLinePositionType.MarketPosition)
            {
                base.Draw(canvas);
            }
            else
            {
                mPaintPosition.StrokeWidth = LineStroke;

                mPaintPosition.SetStyle(Paint.Style.Stroke);
                mPaintPosition.Color = Color;

                canvas.DrawOval(bounds.Left + LineStroke, bounds.Top + LineStroke, bounds.Right - LineStroke, bounds.Bottom - LineStroke, mPaintPosition);

                mPaintPosition.SetStyle(Paint.Style.Fill);

                var reduction = System.Math.Min(bounds.Right - bounds.Left, bounds.Bottom - bounds.Top) / 4;
                canvas.DrawOval(bounds.Left + reduction, bounds.Top + reduction, bounds.Right - reduction, bounds.Bottom - reduction, mPaintPosition);
            }
        }
    }
}
