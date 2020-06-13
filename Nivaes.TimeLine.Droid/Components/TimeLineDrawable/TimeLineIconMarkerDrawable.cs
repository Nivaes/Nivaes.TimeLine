namespace Nivaes.TimeLine.Droid
{
    using Android.Content.Res;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Support.Graphics.Drawable;

    /// <summary>Text drawable.</summary>
    internal sealed class TimeLineIconMarkerDrawable
        : TimeLineMarkerDrawable
    {
        #region Properties
        private Paint mPaintIcon;

        #region BitmapDrawable
        private BitmapDrawable mBitmapDrawable;

        public BitmapDrawable BitmapDrawable
        {
            get => mBitmapDrawable;
            set
            {
                if (mBitmapDrawable != value)
                {
                    mBitmapDrawable = value;

                    if (mBitmapDrawable == null)
                    {
                        mIntrinsicWidth = 0;
                    }
                    else
                    {
                        mIntrinsicWidth = value.IntrinsicWidth;
                    }
                }
            }
        }
        #endregion

        #region VectorDrawable
        private VectorDrawableCompat mVectorDrawable;

        public VectorDrawableCompat VectorDrawable
        {
            get => mVectorDrawable;
            set
            {
                if (mVectorDrawable != value)
                {
                    mVectorDrawable = value;

                    if (mVectorDrawable == null)
                    {
                        mIntrinsicWidth = 0;
                    }
                    else
                    {
                        mIntrinsicWidth = value.IntrinsicWidth;
                    }
                }
            }
        }
        #endregion

        private int mIntrinsicWidth;
        public override int IntrinsicWidth => mIntrinsicWidth;
        #endregion

        public TimeLineIconMarkerDrawable(Resources resources)
            : base(resources)
        {
            mPaintIcon = new Paint(PaintFlags.AntiAlias | PaintFlags.LinearText);
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (mBitmapDrawable != null)
            {
                if (MarketPosition != TimeLinePositionType.NoMarket)
                {
                    mPaintIcon.SetStyle(Paint.Style.Fill);
                    mPaintIcon.Color = Color.White;
                }
                else
                {
                    mPaintIcon.SetStyle(Paint.Style.Fill);
                    mPaintIcon.Color = base.Color;
                }

                Rect bounds = base.Bounds;

                canvas.DrawBitmap(mBitmapDrawable.Bitmap, bounds.ExactCenterX(), bounds.ExactCenterY() + (mBitmapDrawable.IntrinsicHeight / 2f), mPaintIcon);
            }

            if (mVectorDrawable != null)
            {
                if (MarketPosition != TimeLinePositionType.NoMarket)
                {
                    mVectorDrawable.SetColorFilter(new PorterDuffColorFilter(Color.White, PorterDuff.Mode.SrcIn));
                }
                else
                {
                    mVectorDrawable.SetColorFilter(new PorterDuffColorFilter(base.Color, PorterDuff.Mode.SrcIn));
                }

                var bounds = base.Bounds;
                var reduction = System.Math.Min(bounds.Right - bounds.Left, bounds.Bottom - bounds.Top) / 5;

                mVectorDrawable.SetBounds(bounds.Left + reduction, bounds.Top + reduction, bounds.Right - reduction, bounds.Bottom - reduction);
                mVectorDrawable.Draw(canvas);
            }
        }
    }
}
