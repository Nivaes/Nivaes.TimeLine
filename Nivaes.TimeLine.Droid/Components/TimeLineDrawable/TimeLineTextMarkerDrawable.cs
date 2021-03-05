namespace Nivaes.TimeLine.Droid
{
    using Android.Content.Res;
    using Android.Graphics;
    using Android.Util;

    /// <summary>Text drawable.</summary>
    internal sealed class TimeLineTextMarkerDrawable
        : TimeLineMarkerDrawable
    {
        #region Properties
        private static int sDefaultTextSize = 12;

        private Paint mPaintText;

        private string mText = string.Empty;

        public string Text
        {
            get => mText;
            set
            {
                if (mText != value)
                {
                    mText = value;

                    if (string.IsNullOrEmpty(value))
                    {
                        mText = string.Empty;
                        mIntrinsicWidth = 0;
                    }
                    else
                    {
                        mIntrinsicWidth = (int)(mPaintText.MeasureText(mText, 0, mText.Length) + .5);
                    }
                }
            }
        }

        private int mIntrinsicWidth;
        public override int IntrinsicWidth => mIntrinsicWidth;
        #endregion

        public TimeLineTextMarkerDrawable(Resources res)
            : base(res)
        {
            mPaintText = new Paint(PaintFlags.AntiAlias | PaintFlags.LinearText)
            {
                TextAlign = Paint.Align.Center,
                TextSize = TypedValue.ApplyDimension(ComplexUnitType.Sp, sDefaultTextSize, res.DisplayMetrics)
            };
        }

        public override void Draw(Canvas? canvas)
        {
            base.Draw(canvas);

            if (!string.IsNullOrEmpty(mText))
            {
                if (MarketPosition != TimeLinePositionType.NoMarket)
                {
                    mPaintText.SetStyle(Paint.Style.Fill);
                    mPaintText.Color = Color.White;
                }
                else
                {
                    mPaintText.SetStyle(Paint.Style.Fill);
                    mPaintText.Color = Color;
                }

                Rect bounds = base.Bounds;

                using var textBoundRect = new Rect();
                mPaintText.GetTextBounds(mText, 0, Text.Length, textBoundRect);
                var textHeight = textBoundRect.Height() - base.LineStroke / 2f;

                canvas?.DrawText(mText, bounds.ExactCenterX(), bounds.ExactCenterY() + (textHeight / 2f), mPaintText);
            }
        }
    }
}
