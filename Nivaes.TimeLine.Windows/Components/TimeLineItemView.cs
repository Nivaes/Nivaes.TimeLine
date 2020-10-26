namespace Nivaes.TimeLine.UAP
{
    using System.Windows.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;

    [TemplatePart(Name = "LeftTimeLineMarker", Type = typeof(TimeLineMarkerView))]
    [TemplatePart(Name = "RithTimeLineMarker", Type = typeof(TimeLineMarkerView))]
    [TemplatePart(Name = "LeftIcon", Type = typeof(UIElement))]
    [TemplatePart(Name = "RithIcon", Type = typeof(UIElement))]
    public class TimeLineItemView
        : ListViewItem
    {
        #region Properties
        private ITimeLineItem mTimeLineItem;
        private TimeLineMarkerView mLeftTimeLineMarker;
        private TimeLineMarkerView mRithTimeLineMarker;
        private UIElement mLeftIcon;
        private UIElement mRithIcon;

        #region MarkerType
        public TimeLineMarkerType MarkerType
        {
            get => (TimeLineMarkerType)GetValue(TimeLineMarkerTypeProperty);
            set => SetValue(TimeLineMarkerTypeProperty, value);
        }

        public static readonly DependencyProperty TimeLineMarkerTypeProperty = DependencyProperty.Register(
           nameof(MarkerType),
           typeof(TimeLineMarkerType),
           typeof(TimeLineItemView),
           new PropertyMetadata(TimeLineMarkerType.TextMarker, OnTimeLineMarkerTypeChanged));

        private static void OnTimeLineMarkerTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeLineView = (TimeLineItemView)d;

            timeLineView.ChangeVisibility();
        }
        #endregion       

        #region TimeLineOrientation
        public TimeLineOrientation TimeLineOrientation
        {
            get => (TimeLineOrientation)GetValue(TimeLineOrientationProperty);
            set => SetValue(TimeLineOrientationProperty, value);
        }

        public static readonly DependencyProperty TimeLineOrientationProperty = DependencyProperty.Register(
           nameof(TimeLineOrientation),
           typeof(TimeLineOrientation),
           typeof(TimeLineItemView),
           new PropertyMetadata(TimeLineOrientation.VerticalLeft, OnTimeLineOrientationChanged));

        private static void OnTimeLineOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeLineView = (TimeLineItemView)d;

            timeLineView.ChangeVisibility();
        }
        #endregion

        #region MarketColor
        public Brush MarketColor
        {
            get => (Brush)GetValue(MarketColorProperty);
            set => SetValue(MarketColorProperty, value);
        }

        public static readonly DependencyProperty MarketColorProperty = DependencyProperty.Register(
            nameof(MarketColor),
            typeof(Brush),
            typeof(TimeLineItemView),
            new PropertyMetadata(null));
        #endregion

        #region StartLineColor
        public Brush StartLineColor
        {
            get => (Brush)GetValue(StartLineColorProperty);
            set => SetValue(StartLineColorProperty, value);
        }

        public static readonly DependencyProperty StartLineColorProperty = DependencyProperty.Register(
            nameof(StartLineColor),
            typeof(Brush),
            typeof(TimeLineItemView),
            new PropertyMetadata(null));
        #endregion

        #region EndLineColor
        public Brush EndLineColor
        {
            get => (Brush)GetValue(EndLineColorProperty);
            set => SetValue(EndLineColorProperty, value);
        }

        public static readonly DependencyProperty EndLineColorProperty = DependencyProperty.Register(
            nameof(EndLineColor),
            typeof(Brush),
            typeof(TimeLineItemView),
            new PropertyMetadata(null));
        #endregion

        #region TextColor
        public Brush TextColor
        {
            get => (Brush)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(
            nameof(TextColor),
            typeof(Brush),
            typeof(TimeLineItemView),
            new PropertyMetadata(null));
        #endregion

        #region Text
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(TimeLineItemView),
            new PropertyMetadata(null));
        #endregion

        #region Icon
        public DependencyObject Icon
        {
            get => (DependencyObject)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
            nameof(Icon),
            typeof(DependencyObject),
            typeof(TimeLineItemView),
            new PropertyMetadata(null, OnIconChanged));

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeLineView = (TimeLineItemView)d;

            timeLineView.ChangeVisibility();
        }
        #endregion
        
        #region TimeLineItemType
        public TimeLineItemType TimeLineItemType
        {
            get => (TimeLineItemType)GetValue(TimeLineItemTypeProperty);
            set => SetValue(TimeLineItemTypeProperty, value);
        }

        public static readonly DependencyProperty TimeLineItemTypeProperty = DependencyProperty.Register(
            nameof(TimeLineItemType),
            typeof(TimeLineItemType),
            typeof(TimeLineItemView),
            new PropertyMetadata(null));
        #endregion
        #endregion

        #region Constructor
        public TimeLineItemView()
        {
            base.DefaultStyleKey = typeof(TimeLineItemView);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mLeftTimeLineMarker = base.GetTemplateChild("LeftTimeLineMarker") as TimeLineMarkerView;
            mRithTimeLineMarker = base.GetTemplateChild("RithTimeLineMarker") as TimeLineMarkerView;
            mLeftIcon = base.GetTemplateChild("LeftIcon") as UIElement;
            mRithIcon = base.GetTemplateChild("RithIcon") as UIElement;

            ChangeVisibility();
        }
        #endregion

        private void ChangeVisibility()
        {
            if (TimeLineOrientation == TimeLineOrientation.VerticalLeft)
            {
                if (mLeftTimeLineMarker != null)
                    mLeftTimeLineMarker.Visibility = Visibility.Visible;

                if (mRithTimeLineMarker != null)
                    mRithTimeLineMarker.Visibility = Visibility.Collapsed;

                if(mLeftIcon != null)
                    mLeftIcon.Visibility = Visibility.Visible;

                if (mRithIcon != null)
                    mRithIcon.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (mLeftTimeLineMarker != null)
                    mLeftTimeLineMarker.Visibility = Visibility.Collapsed;

                if (mRithTimeLineMarker != null)
                    mRithTimeLineMarker.Visibility = Visibility.Visible;

                if (mLeftIcon != null)
                    mLeftIcon.Visibility = Visibility.Collapsed;

                if (mRithIcon != null)
                    mRithIcon.Visibility = Visibility.Visible;
            }

            if (MarkerType == TimeLineMarkerType.Icon)
            {
                if (mLeftTimeLineMarker != null)
                    mLeftTimeLineMarker.Visibility = Visibility.Collapsed;

                if (mRithTimeLineMarker != null)
                    mRithTimeLineMarker.Visibility = Visibility.Collapsed;
            }
            else if(MarkerType == TimeLineMarkerType.TextMarker)
            {
                if (mLeftIcon != null)
                    mLeftIcon.Visibility = Visibility.Collapsed;

                if (mRithIcon != null)
                    mRithIcon.Visibility = Visibility.Collapsed;
            }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            if (newContent is ITimeLineItem timeLineItem)
            {
                mTimeLineItem = timeLineItem;
                Text = timeLineItem.MarkerText;
                Icon = timeLineItem.Icon;
            }
        }

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            base.OnTapped(e);

            ICommand command = mTimeLineItem?.Click;
            if(command != null)
            {
                if(command.CanExecute(mTimeLineItem))
                {
                    command.Execute(mTimeLineItem);
                }
            }
        }

        protected override void OnDoubleTapped(DoubleTappedRoutedEventArgs e)
        {
            base.OnDoubleTapped(e);

            ICommand command = mTimeLineItem?.LongClick;
            if (command != null)
            {
                if (command.CanExecute(mTimeLineItem))
                {
                    command.Execute(mTimeLineItem);
                }
            }
        }
    }
}
