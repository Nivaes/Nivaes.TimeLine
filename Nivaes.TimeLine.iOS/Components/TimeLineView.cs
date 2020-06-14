namespace Nivaes.TimeLine.iOS
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using CoreGraphics;
    using Foundation;
    using ObjCRuntime;
    using UIKit;

    [Preserve(AllMembers = true), DesignTimeVisible(true)]
    public partial class TimeLineView
        : UITableView
    {
        #region Properties
        private TimeLineAttributes mTimeLineAttributes;

        public TimeLineMarkerType MarkerType
        {
            get => mTimeLineAttributes.MarkerType;
            set => mTimeLineAttributes.MarkerType = value;
        }

        public UIColor MarketColor
        {
            get => mTimeLineAttributes.MarketColor;
            set => mTimeLineAttributes.MarketColor = value;
        }

        public UIColor LineColor
        {
            get => mTimeLineAttributes.LineColor;
            set => mTimeLineAttributes.LineColor = value;
        }

        public int TimeLinePositioin
        {
            get => mTimeLineAttributes.TimeLinePositioin;
            set => mTimeLineAttributes.TimeLinePositioin = value;
        }
        #endregion

        #region Constructors
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Export("init")]
        public TimeLineView()
           : base()
        {
            Initialize();
        }

        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [DesignatedInitializer]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Export("initWithCoder:")]
        public TimeLineView(NSCoder coder)
           : base(coder)
        {
            Initialize();
        }

        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [Export("initWithFrame:")]
        public TimeLineView(CGRect frame)
            : base(frame)
        {
            Initialize();
        }

        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected TimeLineView(NSObjectFlag t)
            : base(t)
        {
            Initialize();
        }

        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal TimeLineView(IntPtr handle)
            : base(handle)
        {
            Initialize();
        }

        private void Initialize()
        {
            mTimeLineAttributes = new TimeLineAttributes
            {
                LineColor = UIColor.LightGray,
                MarketColor = UIColor.Blue,
                MarkerSize = 20.0f,
                IconSize = 32.0f,
                MarginSize = 7.0f,
                MarkerInCenter = true,
                LineOrientation = TimeLineOrientation.VerticalLeft
            };

            SetupContent();
        }
        #endregion

        private void SetupContent()
        {
            base.AutoresizingMask = UIViewAutoresizing.All;
            base.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            base.SeparatorColor = mTimeLineAttributes.LineColor;
            base.SeparatorInset = new UIEdgeInsets(0, mTimeLineAttributes.MarkerSize + 20, 0, 0);
            base.TableFooterView = new UIView();
        }

        public void SetSource(TimeLineTableViewSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            base.Source = source;

            source.TimeLineAttributes = mTimeLineAttributes;
        }

        private class TimeLineTableViewCell
            : UITableViewCell
        {
            #region Properties
            private readonly TimeLineAttributes mTimeLineAttributes;

            private readonly UIView mTimeLineView;

            private UIView mContectView;
            #endregion

            internal TimeLineTableViewCell(NSString cellId,
                                           TimeLineAttributes timeLineAttributes, TimeLineItemType timeLineType,
                                           bool showMarker, nfloat rowHeight)
                : base(UITableViewCellStyle.Default, cellId)
            {
                mTimeLineAttributes = timeLineAttributes;

                switch (timeLineAttributes.MarkerType)
                {
                    case TimeLineMarkerType.TextMarker:
                        var marketFrame = new CGRect(0, 0, mTimeLineAttributes.MarkerSize, rowHeight);
                        mTimeLineView = new TimeLineMarkerView(marketFrame, mTimeLineAttributes, timeLineType)
                        {
                            TranslatesAutoresizingMaskIntoConstraints = false,
                            Hidden = !showMarker
                        };
                        break;
                    case TimeLineMarkerType.Icon:
                        var imageFrame = new CGRect(0, 0, mTimeLineAttributes.IconSize, mTimeLineAttributes.IconSize);
                        mTimeLineView = new UIImageView(imageFrame)
                        {
                            TranslatesAutoresizingMaskIntoConstraints = false
                        };
                        break;
                }
                
                base.ContentView.Add(mTimeLineView);

                if (mTimeLineAttributes.MarkerInCenter || timeLineAttributes.MarkerType == TimeLineMarkerType.Icon)
                {
                    base.ContentView.AddConstraint(NSLayoutConstraint.Create(mTimeLineView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, base.ContentView, NSLayoutAttribute.CenterY, 1.0f, 0.0f));
                }
                else
                {
                    base.ContentView.AddConstraint(NSLayoutConstraint.Create(mTimeLineView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, base.ContentView, NSLayoutAttribute.Top, 1.0f, mTimeLineAttributes.MarginSize));
                }

                base.ContentView.AddConstraints(new NSLayoutConstraint[]
                {
                    NSLayoutConstraint.Create(mTimeLineView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, base.ContentView, NSLayoutAttribute.Left, 1.0f, mTimeLineAttributes.MarginSize),
                    NSLayoutConstraint.Create(mTimeLineView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 1.0f, mTimeLineAttributes.MarkerSize),
                    NSLayoutConstraint.Create(mTimeLineView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1.0f, mTimeLineAttributes.MarkerSize),
                });
            }

            internal void UpdateCell(TimeLineItem timeLineItem, UIView contentView)
            {
                if (mTimeLineView is TimeLineMarkerView timeLineMarkerView)
                {
                    timeLineMarkerView.MarketText = timeLineItem.MarkerText;
                }
                else if(mTimeLineView is UIImageView imageView)
                {
                    imageView.Image = timeLineItem.Icon;
                    imageView.TintColor = UIColor.DarkGray;
                }

                mContectView?.RemoveFromSuperview();

                mContectView = contentView;
                mContectView.ContentMode = UIViewContentMode.ScaleAspectFill;
                base.ContentView.Add(mContectView);

                base.ContentView.AddConstraint(NSLayoutConstraint.Create(mContectView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, mTimeLineView, NSLayoutAttribute.Trailing, 1.0f, mTimeLineAttributes.MarginSize));

                base.ContentView.AddConstraints(new NSLayoutConstraint[]
                {
                    NSLayoutConstraint.Create(mContectView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, base.ContentView, NSLayoutAttribute.Top, 1.0f, 0.0f),
                    NSLayoutConstraint.Create(mContectView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, mTimeLineView, NSLayoutAttribute.Trailing, 1.0f, 0.0f),
                    NSLayoutConstraint.Create(mContectView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, base.ContentView, NSLayoutAttribute.Bottom, 1.0f, 0.0f),
                });

                if (mTimeLineAttributes.LineOrientation == TimeLineOrientation.VerticalLeft)
                {
                    base.ContentView.AddConstraint(NSLayoutConstraint.Create(mContectView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, base.ContentView, NSLayoutAttribute.Right, 1.0f, 0.0f));
                }
                else
                {
                    base.ContentView.AddConstraint(NSLayoutConstraint.Create(mContectView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, base.ContentView, NSLayoutAttribute.Left, 1.0f, mTimeLineAttributes.MarginSize + 3));
                }
            }

            internal void UpdateMarket(TimeLinePositionType timeLinePositionType, UIColor startColor, UIColor endColor)
            {
                if (mTimeLineView is TimeLineMarkerView mTimeLineMarkerView)
                {
                    mTimeLineMarkerView.UpdateType(timeLinePositionType, startColor, endColor);
                }
            }

            public override void LayoutSubviews()
            {
                base.LayoutSubviews();

                base.ContentView.SetNeedsLayout();
                base.ContentView.LayoutIfNeeded();
            }
        }

        public abstract class TimeLineTableViewSource
            : UITableViewSource
        {
            #region TimeLines
            private readonly NSString cellIdentifier = new NSString("TimeLineTableCell");
            internal TimeLineAttributes TimeLineAttributes { get; set; }
            private nfloat mRowHeight;

            private TimeLineItem[] mTimeLines;
            #endregion

            public TimeLineTableViewSource(TimeLineItem[] timeLineItems, nfloat rowHeight)
            {
                mTimeLines = timeLineItems;
                mRowHeight = rowHeight;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return mTimeLines.Length;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                if (tableView == null) throw new ArgumentNullException(nameof(tableView));
                if (indexPath == null) throw new ArgumentNullException(nameof(indexPath));

                tableView.DeselectRow(indexPath, true);

                var item = mTimeLines[indexPath.Row];

                var command = item?.Click;
                if (command != null && command.CanExecute(null))
                {
                    command.Execute(null);
                }
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                if (tableView == null) throw new ArgumentNullException(nameof(tableView));
                if (indexPath == null) throw new ArgumentNullException(nameof(indexPath));


                if (!(tableView.DequeueReusableCell(cellIdentifier) is TimeLineTableViewCell cell))
                {
                    TimeLineItemType timeLineType = BindingTypeLineMarker(indexPath);

                    cell = new TimeLineTableViewCell(cellIdentifier, TimeLineAttributes, timeLineType,
                        mTimeLines[indexPath.Row].ShowMarker, mRowHeight);
                }

                var timeLineItem = mTimeLines[indexPath.Row];
                var contentView = GetContentView(timeLineItem);
                contentView.TranslatesAutoresizingMaskIntoConstraints = false;

                cell.UpdateCell(timeLineItem, contentView);

                TimeLinePositionType timeLinePositionType;
                UIColor startColor;
                UIColor endColor;
                if (TimeLineAttributes.TimeLinePositioin < indexPath.Row)
                {
                    timeLinePositionType = TimeLinePositionType.NoMarket;
                    startColor = TimeLineAttributes.LineColor;
                    endColor = TimeLineAttributes.LineColor;
                }
                else if (TimeLineAttributes.TimeLinePositioin > indexPath.Row)
                {
                    timeLinePositionType = TimeLinePositionType.Market;
                    startColor = TimeLineAttributes.MarketColor;
                    endColor = TimeLineAttributes.MarketColor;
                }
                else
                {
                    timeLinePositionType = TimeLinePositionType.MarketPosition;
                    startColor = TimeLineAttributes.MarketColor;
                    endColor = TimeLineAttributes.LineColor;
                }

                cell.UpdateMarket(timeLinePositionType, startColor, endColor);

                return cell;
            }

            private TimeLineItemType BindingTypeLineMarker(NSIndexPath indexPath)
            {
                if (mTimeLines.Length == 1)
                {
                    return TimeLineItemType.OnlyOne;
                }
                else if (indexPath.Row == 0)
                {
                    if (!mTimeLines[1].ShowMarker)
                        return TimeLineItemType.OnlyOne;
                    else
                        return TimeLineItemType.Begin;
                }
                else if (indexPath.Row >= mTimeLines.Length - 1)
                {
                    if (!mTimeLines[indexPath.Row - 1].ShowMarker)
                        return TimeLineItemType.OnlyOne;
                    else
                        return TimeLineItemType.End;
                }
                else
                {
                    bool showEnd = mTimeLines[indexPath.Row + 1].ShowMarker;
                    bool showBegin = mTimeLines[indexPath.Row - 1].ShowMarker;
                    if (showEnd && showBegin)
                        return TimeLineItemType.Normal;
                    else if (!showEnd)
                        return TimeLineItemType.End;
                    else if (!showBegin)
                        return TimeLineItemType.Begin;
                    else
                        return TimeLineItemType.Normal;
                }
            }

            protected abstract UIView GetContentView(TimeLineItem item);
        }
    }
}
