namespace Nivaes.TimeLine.Droid
{
    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Views;
    using System.Diagnostics.CodeAnalysis;

    internal abstract class TimeLineMarkerView
        : View
    {
        #region Properties
        [AllowNull]
        internal TimeLineMarkerDrawable Marker { get; set; } = null;

        [AllowNull]
        private Drawable mStartLine = null;

        [AllowNull]
        private Drawable mEndLine = null;

        internal Color StartColor
        {
            get => Marker!.Color;
            set => Marker!.Color = value;
        }

        internal Color EndColor { get; set; }

        [AllowNull]
        internal TimeLineAttributes TimeLineAttributes { get; set; } = null;

        internal TimeLinePositionType MarketPosition
        {
            get => Marker.MarketPosition;
            set => Marker.MarketPosition = value;
        }

        internal TimeLineItemType TimeLineType { get; set; }

        [AllowNull]
        private Rect mBounds = null;
        #endregion

        #region Constructors
        public TimeLineMarkerView(Context context)
            : base(context)
        {
        }
        #endregion

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            int w = TimeLineAttributes.MarkerSize + base.PaddingLeft + base.PaddingRight;
            int h = TimeLineAttributes.MarkerSize + base.PaddingTop + base.PaddingTop;

            int widthSize = ResolveSizeAndState(w, widthMeasureSpec, 0);
            int heightSize = ResolveSizeAndState(h, heightMeasureSpec, 0);

            base.SetMeasuredDimension(widthSize, heightSize);
            InitDrawable();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            InitDrawable();
        }

        internal virtual void InitDrawable()
        {
            int pLeft = base.PaddingLeft;
            int pRight = base.PaddingRight;
            int pTop = base.PaddingTop;
            int pBottom = base.PaddingBottom;

            int width = base.Width;
            int height = base.Height;

            int cWidth = width - pLeft - pRight;
            int cHeight = height - pTop - pBottom;

            int markSize = System.Math.Min(TimeLineAttributes.MarkerSize, System.Math.Min(cWidth, cHeight));          

            if (TimeLineAttributes.MarkerInCenter)
            {
                Marker.SetBounds((width / 2) - (markSize / 2), (height / 2) - (markSize / 2), (width / 2) + (markSize / 2), (height / 2) + (markSize / 2));
                mBounds = Marker.Bounds;
            }
            else
            {
                Marker.SetBounds(pLeft, pTop, pLeft + markSize, pTop + markSize);
                mBounds = Marker.Bounds;
            }

            int centerX = mBounds.CenterX();
            int lineLeft = centerX - (TimeLineAttributes.LineSize >> 1);

            switch (TimeLineType)
            {
                case TimeLineItemType.Normal:
                    mStartLine = new ColorDrawable(StartColor);
                    mEndLine = new ColorDrawable(EndColor);
                    break;
                case TimeLineItemType.OnlyOne:
                    mStartLine = null;
                    mEndLine = null;
                    break;
                case TimeLineItemType.Begin:
                    mStartLine = null;
                    mEndLine = new ColorDrawable(EndColor);
                    break;
                case TimeLineItemType.End:
                    mStartLine = new ColorDrawable(StartColor);
                    mEndLine = null;
                    break;
            }

            if (TimeLineAttributes.LineOrientation == TimeLineOrientation.HorizontalBottom || TimeLineAttributes.LineOrientation == TimeLineOrientation.HorizontalTop)
            {
                mStartLine?.SetBounds(0, pTop + (mBounds.Height() / 2), mBounds.Left - TimeLineAttributes.LinePadding, (mBounds.Height() / 2) + pTop + TimeLineAttributes.LineSize);
                mEndLine?.SetBounds(mBounds.Right + TimeLineAttributes.LinePadding, pTop + (mBounds.Height() / 2), width, (mBounds.Height() / 2) + pTop + TimeLineAttributes.LineSize);
            }
            else
            {
                mStartLine?.SetBounds(lineLeft, 0, TimeLineAttributes.LineSize + lineLeft, mBounds.Top - TimeLineAttributes.LinePadding);
                mEndLine?.SetBounds(lineLeft, mBounds.Bottom + TimeLineAttributes.LinePadding, TimeLineAttributes.LineSize + lineLeft, height);
            }
        }

        protected override void OnDraw(Canvas? canvas)
        {
            base.OnDraw(canvas);

            Marker?.Draw(canvas);

            if (canvas != null)
            {
                mStartLine?.Draw(canvas);

                mEndLine?.Draw(canvas);
            }
        }
    }
}
