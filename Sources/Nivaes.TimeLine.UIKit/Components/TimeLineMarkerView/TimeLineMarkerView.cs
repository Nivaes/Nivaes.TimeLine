namespace Nivaes.TimeLine.iOS
{
    using System;
    using CoreAnimation;
    using CoreGraphics;
    using ObjCRuntime;
    using UIKit;

    internal partial class TimeLineMarkerView
        : UIView
    {
        #region Properties
        private CAShapeLayer? mShapeLayer;
        private readonly TimeLineAttributes mTimeLineAttributes;
        private CATextLayer? mMarketLayer = null;
        private CAShapeLayer? mStartLineLayer;
        private CAShapeLayer? mEndLineLayer;

        private readonly TimeLineItemType mTimeLineType;

        public string MarketText
        {
            get => mMarketLayer?.String ?? string.Empty;
            set
            {
                if(mMarketLayer != null)
                    mMarketLayer.String = value;
            }
        }
        #endregion

        public TimeLineMarkerView(CGRect frame, TimeLineAttributes timeLineAttributes, TimeLineItemType timeLineType)
            : base(frame)
        {
            mTimeLineAttributes = timeLineAttributes;
            mTimeLineType = timeLineType;
            base.BackgroundColor = UIColor.Clear;

            InitializeView();
            InitializeLinesView();
        }

        private void InitializeView()
        {
            var shapePath = UIBezierPath.FromOval(new CGRect(0, 0, mTimeLineAttributes.MarkerSize, mTimeLineAttributes.MarkerSize));
            mShapeLayer = new CAShapeLayer
            {
                FillColor = UIColor.Clear.CGColor,
                Path = shapePath.CGPath
            };
            base.Layer.AddSublayer(mShapeLayer);

            mMarketLayer = new CATextLayer
            {
                ContentsScale = UIScreen.MainScreen.Scale,
                ShouldRasterize = true,
                RasterizationScale = UIScreen.MainScreen.Scale,
                TextAlignmentMode = CATextLayerAlignmentMode.Center,
                FontSize = NMath.Min(base.Frame.Width * 0.8f, base.Frame.Height * 0.8f),
                Frame = base.Frame,
            };
            base.Layer.AddSublayer(mMarketLayer);
        }

        private void InitializeLinesView()
        {
            nfloat xLine = mTimeLineAttributes.MarkerSize / 2;
            nfloat lineSizeStart = - (base.Frame.Height - mTimeLineAttributes.MarkerSize) / 2;
            nfloat lineSizeEnd = base.Frame.Height;

            if (mTimeLineType == TimeLineItemType.Normal || mTimeLineType == TimeLineItemType.End)
            {
                var startLinePath = new UIBezierPath();
                
                startLinePath.MoveTo(new CGPoint(xLine, 0));
                startLinePath.AddLineTo(new CGPoint(xLine, lineSizeStart));

                mStartLineLayer = new CAShapeLayer()
                {
                    FillColor = UIColor.Clear.CGColor,
                    //LineDashPattern = new NSNumber[] { 2, 2 },
                    Path = startLinePath.CGPath
                };
                base.Layer.AddSublayer(mStartLineLayer);
            }

            if (mTimeLineType == TimeLineItemType.Normal || mTimeLineType == TimeLineItemType.Begin)
            {
                var endLinePath = new UIBezierPath();

                endLinePath.MoveTo(new CGPoint(xLine, mTimeLineAttributes.MarkerSize));
                endLinePath.AddLineTo(new CGPoint(xLine, lineSizeEnd));

                mEndLineLayer = new CAShapeLayer()
                {
                    FillColor = UIColor.Clear.CGColor,
                    //LineDashPattern = new NSNumber[] { 2, 2 },
                    Path = endLinePath.CGPath
                };
                base.Layer.AddSublayer(mEndLineLayer);
            }
        }

        internal void UpdateType(TimeLinePositionType timeLinePositionType, UIColor startColor, UIColor endColor)
        {
            if (timeLinePositionType != TimeLinePositionType.NoMarket)
            {
                if (mShapeLayer != null)
                {
                    mShapeLayer.FillColor = startColor.CGColor;
                    mShapeLayer.StrokeColor = startColor.CGColor;
                }

                if (mMarketLayer != null)
                    mMarketLayer.ForegroundColor = UIColor.White.CGColor;
            }
            else
            {
                if (mShapeLayer != null)
                {
                    mShapeLayer.FillColor = UIColor.Clear.CGColor;
                    mShapeLayer.StrokeColor = endColor.CGColor;
                }

                if (mMarketLayer != null)
                    mMarketLayer.ForegroundColor = endColor.CGColor;
            }

            if (mStartLineLayer != null)
                mStartLineLayer.StrokeColor = startColor.CGColor;

            if (mEndLineLayer != null)
                mEndLineLayer.StrokeColor = endColor.CGColor;
        }
    }
}
