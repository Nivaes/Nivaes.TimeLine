using Windows.Foundation.Collections;

namespace Nivaes.TimeLine.WinUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.UI;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Media;

    public class TimeLineView
        : ItemsControl
    {
        #region Properties
        #region MarkerType
        public TimeLineMarkerType MarkerType
        {
            get => (TimeLineMarkerType)GetValue(MarkerTypeProperty);
            set => SetValue(MarkerTypeProperty, value);
        }

        public static readonly DependencyProperty MarkerTypeProperty = DependencyProperty.Register(
           nameof(MarkerType),
           typeof(TimeLineMarkerType),
           typeof(TimeLineView),
            new PropertyMetadata(TimeLineMarkerType.TextMarker, OnTimeLineMarkerTypeChanged));

        private static void OnTimeLineMarkerTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeLineView = (TimeLineView)d;

            for (int i = 1; i < timeLineView.Items.Count(); i++)
            {
                var timeLineItem = (TimeLineItemView)timeLineView.ContainerFromIndex(i);
                if (timeLineItem != null)
                    timeLineItem.MarkerType = (TimeLineMarkerType)e.NewValue;
            }
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
           typeof(TimeLineView),
           new PropertyMetadata(TimeLineOrientation.VerticalLeft, OnTimeLineOrientationChanged));

        private static void OnTimeLineOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeLineView = (TimeLineView)d;

            for (int i = 1; i < timeLineView.Items.Count(); i++)
            {
                var timeLineItem = (TimeLineItemView)timeLineView.ContainerFromIndex(i);
                timeLineItem.TimeLineOrientation = (TimeLineOrientation)e.NewValue;
            }
        }
        #endregion

        #region TimeLinePosition
        public int TimeLinePosition
        {
            get => (int)GetValue(TimeLinePositionProperty);
            set => SetValue(TimeLinePositionProperty, value);
        }

        public static readonly DependencyProperty TimeLinePositionProperty = DependencyProperty.Register(
           nameof(TimeLinePosition),
           typeof(int),
           typeof(TimeLineView),
            new PropertyMetadata(-1, OnTimeLinePositionChanged));

        private static void OnTimeLinePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeLineView = (TimeLineView)d;

            timeLineView.BindingTypeLineMarker();
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
            typeof(TimeLineView),
            new PropertyMetadata(new SolidColorBrush(Colors.SteelBlue)));
        #endregion

        #region NoMarketColor
        public Brush NoMarketColor
        {
            get => (Brush)GetValue(NoMarketColorProperty);
            set => SetValue(NoMarketColorProperty, value);
        }

        public static readonly DependencyProperty NoMarketColorProperty = DependencyProperty.Register(
            nameof(NoMarketColor),
            typeof(Brush),
            typeof(TimeLineView),
            new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));
        #endregion

        #region LineColor
        public Brush LineColor
        {
            get => (Brush)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        public static readonly DependencyProperty LineColorProperty = DependencyProperty.Register(
            nameof(LineColor),
            typeof(Brush),
            typeof(TimeLineView),
            new PropertyMetadata(new SolidColorBrush(Colors.Blue)));
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
            typeof(TimeLineView),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));
        #endregion
        #endregion

        #region Constructors
        public TimeLineView()
        {
            base.DefaultStyleKey = typeof(TimeLineView);

            base.Items.VectorChanged += Items_VectorChanged;
        }


        private void Items_VectorChanged(IObservableVector<object> sender, IVectorChangedEventArgs @event)
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                _ = ContainerFromIndex(i);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TimeLineItemView
            {
                MarkerType = MarkerType,
                TimeLineOrientation = TimeLineOrientation
            };
        }
        #endregion

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            BindingTypeLineMarker();
        }

        private void BindingTypeLineMarker()
        { 
            List<Tuple<ITimeLineItem, TimeLineItemView>> items = new List<Tuple<ITimeLineItem, TimeLineItemView>>();

            for (int i = 0; i < base.Items.Count; i++)
            {
                var timeLineItemView = (TimeLineItemView)ContainerFromIndex(i);

                if (timeLineItemView != null)
                {
                    var timeLineItem = ItemFromContainer(timeLineItemView) as ITimeLineItem;
                    items.Add(new Tuple<ITimeLineItem, TimeLineItemView>(timeLineItem, timeLineItemView));
                }
            }

            if (items.Count == base.Items.Count)
            {
                BindingTypeLineMarker(items);
            }
        }

        private void BindingTypeLineMarker(List<Tuple<ITimeLineItem, TimeLineItemView>> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                TimeLineItemView timeLineMarkerView = items[i].Item2;

                if (items[i].Item1.ShowMarker)
                {
                    timeLineMarkerView.TextColor = TextColor;

                    if (TimeLinePosition < i)
                    {
                        timeLineMarkerView.MarketColor = NoMarketColor;
                        timeLineMarkerView.StartLineColor = NoMarketColor;
                        timeLineMarkerView.EndLineColor = NoMarketColor;
                    }
                    else if (TimeLinePosition > i)
                    {
                        timeLineMarkerView.MarketColor = MarketColor;
                        timeLineMarkerView.StartLineColor = LineColor;
                        timeLineMarkerView.EndLineColor = LineColor;
                    }
                    else
                    {
                        timeLineMarkerView.MarketColor = MarketColor;
                        timeLineMarkerView.StartLineColor = LineColor;
                        timeLineMarkerView.EndLineColor = NoMarketColor;
                    }

                    if (items.Count == 1)
                    {
                        timeLineMarkerView.TimeLineItemType = TimeLineItemType.OnlyOne;
                    }
                    else if (i == 0)
                    {
                        if (!items[1].Item1.ShowMarker)
                            timeLineMarkerView.TimeLineItemType = TimeLineItemType.OnlyOne;
                        else
                            timeLineMarkerView.TimeLineItemType = TimeLineItemType.Begin;
                    }
                    else if (i >= items.Count - 1)
                    {
                        if (!items[i - 1].Item1.ShowMarker)
                            timeLineMarkerView.TimeLineItemType = TimeLineItemType.OnlyOne;
                        else
                            timeLineMarkerView.TimeLineItemType = TimeLineItemType.End;
                    }
                    else
                    {
                        bool? showEnd = items[i + 1].Item1?.ShowMarker;
                        bool? showBegin = items[i - 1].Item1?.ShowMarker;
                        if (showEnd.HasValue && showBegin.HasValue)
                        {
                            if (showEnd.Value && showBegin.Value)
                                timeLineMarkerView.TimeLineItemType = TimeLineItemType.Normal;
                            else if (!showEnd.Value)
                                timeLineMarkerView.TimeLineItemType = TimeLineItemType.End;
                            else if (!showBegin.Value)
                                timeLineMarkerView.TimeLineItemType = TimeLineItemType.Begin;
                            else
                                timeLineMarkerView.TimeLineItemType = TimeLineItemType.Normal;
                        }
                    }
                }
                else
                {
                    timeLineMarkerView.TimeLineItemType = TimeLineItemType.None;
                }
            }
        }
    }
}
