namespace Nivaes.TimeLine.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Android.Content;
    using Android.Content.Res;
    using Android.Graphics;
    using Android.Runtime;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using AndroidX.RecyclerView.Widget;

    [Register("com.nivaes.TimeLineView")]
    public sealed class TimeLineView
        : RecyclerView
    {
        #region Properties
        private readonly TimeLineAttributes mTimeLineAttributes = new TimeLineAttributes();

        public int TimeLinePositioin
        {
            get => mTimeLineAttributes.TimeLinePositioin;
            set => mTimeLineAttributes.TimeLinePositioin = value;
        }

        public TimeLineMarkerType MarkerType
        {
            get => mTimeLineAttributes.MarkerType;
            set => mTimeLineAttributes.MarkerType = value;
        }

        public ObservableCollection<ITimeLineItem> Items { get; } = new ObservableCollection<ITimeLineItem>();
        #endregion

        #region Constructors
        public TimeLineView(Context context)
            : base(context)
        {
            InitView();
        }

        public TimeLineView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            InitView(attrs);
        }

        public TimeLineView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            InitView(attrs);
        }

        private TimeLineView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            InitView();
        }

        private void InitView()
        {
            base.OverScrollMode = OverScrollMode.Never;

            if (mTimeLineAttributes.LineOrientation == TimeLineOrientation.VerticalLeft || mTimeLineAttributes.LineOrientation == TimeLineOrientation.VerticalRight)
                base.SetLayoutManager(new LinearLayoutManager(base.Context, LinearLayoutManager.Vertical, false));
            else
                base.SetLayoutManager(new LinearLayoutManager(base.Context, LinearLayoutManager.Horizontal, false));

            base.SetItemAnimator(new DefaultItemAnimator());
        }

        private void InitView(IAttributeSet attrs)
        {
            //TypedArray typedArray = base.Context.ObtainStyledAttributes(attrs, Resource.Styleable.timeline_style);
            //mTimeLineAttributes.LineColor = typedArray.GetColor(Resource.Styleable.timeline_style_lineColor, Color.LightBlue);
            //mTimeLineAttributes.MarketColor = typedArray.GetColor(Resource.Styleable.timeline_style_marketColor, Color.Blue);
            //mTimeLineAttributes.MarkerSize = typedArray.GetDimensionPixelSize(Resource.Styleable.timeline_style_markerSize, TimeLineHelpers.DpToPx(20, base.Context));
            //mTimeLineAttributes.LineSize = typedArray.GetDimensionPixelSize(Resource.Styleable.timeline_style_lineSize, TimeLineHelpers.DpToPx(2, base.Context));
            //mTimeLineAttributes.LineOrientation = (TimeLineOrientation)typedArray.GetInt(Resource.Styleable.timeline_style_lineOrientation, (short)TimeLineOrientation.VerticalLeft);
            //mTimeLineAttributes.LinePadding = typedArray.GetDimensionPixelSize(Resource.Styleable.timeline_style_linePadding, 0);
            //mTimeLineAttributes.MarkerInCenter = typedArray.GetBoolean(Resource.Styleable.timeline_style_markerInCenter, true);
            //mTimeLineAttributes.MarkerType = (TimeLineMarkerType)typedArray.GetInt(Resource.Styleable.timeline_style_markerType, (short)TimeLineMarkerType.PositionMarker);
            //typedArray.Recycle();

            InitView();
        }
        #endregion

        #region Methods
        public override void SetAdapter(Adapter adapter)
        {
            if (adapter is TimeLineAdapter timeLineAdapter)
            {
                timeLineAdapter.TimeLineAttributes = mTimeLineAttributes;
            }
            else
            {
                throw new NotSupportedException("The adapter must inherit from TimeLineAdapter.");
            }

            base.SetAdapter(adapter);
        }

        public void Add(TimeLineItem item)
        {
            Items.Add(item);
        }
        #endregion

        #region Adapter and ViewHolder
        public abstract class TimeLineAdapter
            : RecyclerView.Adapter
        {
            public IEnumerable<ITimeLineItem> Items { get; private set; }
            internal TimeLineAttributes TimeLineAttributes { get; set; }

            protected TimeLineAdapter(IEnumerable<ITimeLineItem> items)
                : base()
            {
                Items = items;
            }

            public override int GetItemViewType(int position) => position;

            public override int ItemCount => Items.Count();

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                if (holder == null) throw new ArgumentNullException(nameof(holder));

                var timeLineViewHolder = (TimeLineMarketViewHolder)holder;

                var timeLineItem = Items.ElementAt(position);

                timeLineViewHolder.Click = timeLineItem.Click;
                timeLineViewHolder.LongClick = timeLineItem.LongClick;

                if (TimeLineAttributes.MarkerType == TimeLineMarkerType.Icon)
                {
                    timeLineViewHolder.Image.SetImageResource(timeLineItem.IconResource);
                    timeLineViewHolder.Image.Drawable?.SetColorFilter(TimeLineAttributes.LineColor, PorterDuff.Mode.SrcIn);
                }
                else if (!timeLineItem.ShowMarker)
                {
                    timeLineViewHolder.TimeLineMarker.Visibility = ViewStates.Invisible;
                }
                else
                {
                    BindingTypeLineMarker(timeLineViewHolder.TimeLineMarker, position);
                    switch (TimeLineAttributes.MarkerType)
                    {
                        case TimeLineMarkerType.TextMarker:
                            ((TimeLineTextMarkerView)timeLineViewHolder.TimeLineMarker).MarkerText = timeLineItem.MarkerText;
                            break;
                        case TimeLineMarkerType.IconMarker:
                            ((TimeLineIconMarkerView)timeLineViewHolder.TimeLineMarker).IconResource = timeLineItem.IconResource;
                            break;
                    }
                }

                OnBindViewHolder(timeLineViewHolder.TimeLineContentViewHolder, position);
            }

            public abstract void OnBindViewHolder(TimeLineContentViewHolder holder, int position);

            private void BindingTypeLineMarker(TimeLineMarkerView timeLineMarkerView, int position)
            {
                if (TimeLineAttributes.TimeLinePositioin < position)
                {
                    timeLineMarkerView.MarketPosition = TimeLinePositionType.NoMarket;
                    timeLineMarkerView.StartColor = TimeLineAttributes.LineColor;
                    timeLineMarkerView.EndColor = TimeLineAttributes.LineColor;
                }
                else if (TimeLineAttributes.TimeLinePositioin > position)
                {
                    timeLineMarkerView.MarketPosition = TimeLinePositionType.Market;
                    timeLineMarkerView.StartColor = TimeLineAttributes.MarketColor;
                    timeLineMarkerView.EndColor = TimeLineAttributes.MarketColor;
                }
                else
                {
                    timeLineMarkerView.MarketPosition = TimeLinePositionType.MarketPosition;
                    timeLineMarkerView.StartColor = TimeLineAttributes.MarketColor;
                    timeLineMarkerView.EndColor = TimeLineAttributes.LineColor;
                }

                if (Items.Count() == 1)
                {
                    timeLineMarkerView.TimeLineType = TimeLineItemType.OnlyOne;
                }
                else if (position == 0)
                {
                    if (!Items.ElementAt(1).ShowMarker)
                        timeLineMarkerView.TimeLineType = TimeLineItemType.OnlyOne;
                    else
                        timeLineMarkerView.TimeLineType = TimeLineItemType.Begin;
                }
                else if (position >= Items.Count() - 1)
                {
                    if (!Items.ElementAt(position - 1).ShowMarker)
                        timeLineMarkerView.TimeLineType = TimeLineItemType.OnlyOne;
                    else
                        timeLineMarkerView.TimeLineType = TimeLineItemType.End;
                }
                else
                {
                    bool showEnd = Items.ElementAt(position + 1).ShowMarker;
                    bool showBegin = Items.ElementAt(position - 1).ShowMarker;
                    if (showEnd && showBegin)
                        timeLineMarkerView.TimeLineType = TimeLineItemType.Normal;
                    else if (!showEnd)
                        timeLineMarkerView.TimeLineType = TimeLineItemType.End;
                    else if (!showBegin)
                        timeLineMarkerView.TimeLineType = TimeLineItemType.Begin;
                    else
                        timeLineMarkerView.TimeLineType = TimeLineItemType.Normal;
                }
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                if (parent == null) throw new ArgumentNullException(nameof(parent));

                var linearLayout = new LinearLayout(parent.Context);

                var vimeLineContentViewHolder = OnCreateContentViewHolder(parent, viewType);

                return new TimeLineMarketViewHolder(parent.Context, linearLayout,
                    vimeLineContentViewHolder, TimeLineAttributes);
            }

            public abstract TimeLineContentViewHolder OnCreateContentViewHolder(ViewGroup parent, int viewType);
        }

        internal class TimeLineMarketViewHolder
            : RecyclerView.ViewHolder
        {
            internal TimeLineContentViewHolder TimeLineContentViewHolder { get; set; }
            internal TimeLineMarkerView TimeLineMarker { get; set; }
            internal ImageView Image { get; set; }

            public TimeLineMarketViewHolder(Context context, LinearLayout linearLayout,
                TimeLineContentViewHolder timeLineContentViewHolder, TimeLineAttributes timeLineAttributes)
                : base(linearLayout)
            {
                FrameLayout.LayoutParams? markerLayoutParams;
                switch (timeLineAttributes.LineOrientation)
                {
                    case TimeLineOrientation.VerticalLeft:
                        linearLayout.LayoutParameters = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                        linearLayout.Orientation = Android.Widget.Orientation.Horizontal;
                        linearLayout.SetPadding(20, 0, 20, 0);
                        markerLayoutParams = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.MatchParent);
                        break;
                    case TimeLineOrientation.VerticalRight:
                        linearLayout.LayoutParameters = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                        linearLayout.Orientation = Android.Widget.Orientation.Horizontal;
                        linearLayout.SetPadding(20, 0, 20, 0);
                        markerLayoutParams = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.MatchParent);
                        break;
                    case TimeLineOrientation.HorizontalTop:
                        linearLayout.LayoutParameters = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.MatchParent);
                        linearLayout.Orientation = Android.Widget.Orientation.Vertical;
                        linearLayout.SetPadding(0, 20, 0, 20);
                        markerLayoutParams = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                        break;
                    case TimeLineOrientation.HorizontalBottom:
                        linearLayout.LayoutParameters = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.MatchParent);
                        linearLayout.Orientation = Android.Widget.Orientation.Vertical;
                        linearLayout.SetPadding(0, 20, 0, 20);
                        markerLayoutParams = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                        break;
                    default:
                        markerLayoutParams = null;
                        break;
                }
                linearLayout.Clickable = true;

                using TypedValue tv = new TypedValue();
                //context.Theme.ResolveAttribute(Resource.Attribute.selectableItemBackground, tv, true);
                linearLayout.SetBackgroundResource(tv.ResourceId);

                TimeLineContentViewHolder = timeLineContentViewHolder;

                if (timeLineAttributes.LineOrientation == TimeLineOrientation.VerticalRight ||
                        timeLineAttributes.LineOrientation == TimeLineOrientation.HorizontalBottom)
                {
                    linearLayout.AddView(timeLineContentViewHolder.ItemView);
                }

                switch (timeLineAttributes.MarkerType)
                {
                    case TimeLineMarkerType.TextMarker:
                        TimeLineMarker = new TimeLineTextMarkerView(context)
                        {
                            LayoutParameters = markerLayoutParams,
                            TimeLineAttributes = timeLineAttributes
                        };
                        linearLayout.AddView(TimeLineMarker);
                        break;
                    case TimeLineMarkerType.IconMarker:
                        TimeLineMarker = new TimeLineIconMarkerView(context)
                        {
                            LayoutParameters = markerLayoutParams,
                            TimeLineAttributes = timeLineAttributes
                        };
                        linearLayout.AddView(TimeLineMarker);
                        break;
                    case TimeLineMarkerType.PositionMarker:
                        TimeLineMarker = new TimeLinePositionMarkerView(context)
                        {
                            LayoutParameters = markerLayoutParams,
                            TimeLineAttributes = timeLineAttributes
                        };
                        linearLayout.AddView(TimeLineMarker);
                        break;
                    case TimeLineMarkerType.Icon:
                        Image = new ImageView(context)
                        {
                            LayoutParameters = markerLayoutParams,
                        };
                        linearLayout.AddView(Image);
                        break;
                }

                switch (timeLineAttributes.LineOrientation)
                {
                    case TimeLineOrientation.VerticalLeft:
                        TimeLineMarker?.SetPadding(20, 0, 20, 0);
                        Image?.SetPadding(20, 0, 20, 0);
                        break;
                    case TimeLineOrientation.VerticalRight:
                        TimeLineMarker?.SetPadding(20, 0, 20, 0);
                        Image?.SetPadding(20, 0, 20, 0);
                        break;
                    case TimeLineOrientation.HorizontalTop:
                        TimeLineMarker?.SetPadding(0, 20, 0, 20);
                        Image?.SetPadding(0, 20, 0, 20);
                        break;
                    case TimeLineOrientation.HorizontalBottom:
                        TimeLineMarker?.SetPadding(0, 20, 0, 20);
                        Image?.SetPadding(0, 20, 0, 20);
                        break;
                }

                if (timeLineAttributes.LineOrientation == TimeLineOrientation.VerticalLeft ||
                        timeLineAttributes.LineOrientation == TimeLineOrientation.HorizontalTop)
                {
                    linearLayout.AddView(timeLineContentViewHolder.ItemView);
                }

                linearLayout.Click += (o, e) =>
                {
                    ExecuteCommandOnItem(Click);
                };

                linearLayout.LongClick += (o, e) =>
                {
                    ExecuteCommandOnItem(LongClick);
                };
            }

            protected virtual void ExecuteCommandOnItem(ICommand command)
            {
                if (command != null && command.CanExecute(null))
                {
                    command.Execute(null);
                }
            }

            #region Command
            private ICommand mClick;

            internal ICommand Click
            {
                get => mClick;

                set
                {
                    if (!ReferenceEquals(mClick, value))
                    {
                        mClick = value;
                    }
                }
            }

            private ICommand mLongClick;

            internal ICommand LongClick
            {
                get => mLongClick;

                set
                {
                    if (!ReferenceEquals(mLongClick, value))
                    {
                        mLongClick = value;
                    }
                }
            }
            #endregion
        }

        public abstract class TimeLineContentViewHolder
            : RecyclerView.ViewHolder
        {
            public TimeLineContentViewHolder(View view)
                : base(view)
            { }
        }
        #endregion
    }
}
