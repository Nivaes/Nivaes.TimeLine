namespace Nivaes.TimeLine.WinUI
{
    using System;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Media;
    using Microsoft.UI.Xaml.Shapes;

    [TemplatePart(Name = "Circle", Type = typeof(Ellipse))]
    [TemplatePart(Name = "Text", Type = typeof(TextBlock))]
    [TemplatePart(Name = "StartLine", Type = typeof(UIElement))]
    [TemplatePart(Name = "EndLine", Type = typeof(UIElement))]
    public sealed class TimeLineMarkerView
        : Control
    {
        #region Properties
        private Ellipse mClircle;
        private TextBlock mText;
        private UIElement mStartLine;
        private UIElement mEndLine;
        private RowDefinition mStartRow;
        private RowDefinition mEndRow;

        #region MarketColor
        public Brush MarketColor
        {
            get => (Brush)GetValue(MarketColorProperty);
            set => SetValue(MarketColorProperty, value);
        }

        public static readonly DependencyProperty MarketColorProperty = DependencyProperty.Register(
            nameof(MarketColor),
            typeof(Brush),
            typeof(TimeLineMarkerView),
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
            typeof(TimeLineMarkerView),
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
            typeof(TimeLineMarkerView),
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
            typeof(TimeLineMarkerView),
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
            typeof(TimeLineMarkerView),
            new PropertyMetadata(null));
        #endregion

        #region TimeLineItemType
        public TimeLineItemType TimeLineItemType
        {
            get
            {
                var value = GetValue(TimeLineItemTypeProperty);
                if (value == null)
                    return TimeLineItemType.Normal;
                else
                    return (TimeLineItemType)value;
            }
            set => SetValue(TimeLineItemTypeProperty, value);
        }

        public static readonly DependencyProperty TimeLineItemTypeProperty = DependencyProperty.Register(
            nameof(TimeLineItemType),
            typeof(TimeLineItemType),
            typeof(TimeLineMarkerView),
            new PropertyMetadata(TimeLineItemType.Normal, OnTimeLineItemTypeChanged));

        private static void OnTimeLineItemTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeLineView = (TimeLineMarkerView)d;

            if (e.NewValue != null)
            {
                var itemType = (TimeLineItemType)e.NewValue;

                timeLineView.ProcessItemType(itemType);
            }
        }
        #endregion
        #endregion

        public TimeLineMarkerView()
        {
            base.DefaultStyleKey = typeof(TimeLineMarkerView);

            SizeChanged += OnSizeChanged;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mClircle = base.GetTemplateChild("Circle") as Ellipse;
            mText = base.GetTemplateChild("Text") as TextBlock;
            mStartLine = base.GetTemplateChild("StartLine") as UIElement;
            mEndLine = base.GetTemplateChild("EndLine") as UIElement;

            mStartRow = base.GetTemplateChild("StartRow") as RowDefinition;
            mEndRow = base.GetTemplateChild("EndRow") as RowDefinition;

            ProcessItemType(TimeLineItemType);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            mEndRow.Height = new GridLength(Math.Max(sizeChangedEventArgs.NewSize.Height - mStartRow.ActualHeight - 20, 0));
        }

        private void ProcessItemType(TimeLineItemType itemType)
        {
            if (mStartLine == null || mEndLine == null)
                return;

            switch (itemType)
            {
                case TimeLineItemType.Begin:
                    mClircle.Opacity = 1;
                    mText.Visibility = Visibility.Visible;
                    mStartLine.Visibility = Visibility.Collapsed;
                    mEndLine.Visibility = Visibility.Visible;
                    break;
                case TimeLineItemType.Normal:
                    mClircle.Opacity = 1;
                    mText.Visibility = Visibility.Visible;
                    mStartLine.Visibility = Visibility.Visible;
                    mEndLine.Visibility = Visibility.Visible;
                    break;
                case TimeLineItemType.End:
                    mClircle.Opacity = 1;
                    mText.Visibility = Visibility.Visible;
                    mStartLine.Visibility = Visibility.Visible;
                    mEndLine.Visibility = Visibility.Collapsed;
                    break;
                case TimeLineItemType.OnlyOne:
                    mClircle.Opacity = 1;
                    mText.Visibility = Visibility.Visible;
                    mStartLine.Visibility = Visibility.Collapsed;
                    mEndLine.Visibility = Visibility.Collapsed;
                    break;
                case TimeLineItemType.None:
                    mClircle.Opacity = 0;
                    mText.Visibility = Visibility.Collapsed;
                    mStartLine.Visibility = Visibility.Collapsed;
                    mEndLine.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}

