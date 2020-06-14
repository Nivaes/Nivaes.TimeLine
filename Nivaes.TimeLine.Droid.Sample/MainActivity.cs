using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Runtime;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace Nivaes.TimeLine.Droid.Sample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var timeLine = base.FindViewById<TimeLineView>(Resource.Id.time_line);

            var items = new[]{
                new TestTimeLineItem
                {
                    ShowMarker = true,
                    IconResource = Resource.Drawable.ic_budget,
                    MarkerText = "1",
                    Title = "1",
                    Message = "One",
                },
                new TestTimeLineItem
                {
                    IconResource = Resource.Drawable.ic_calendar,
                    Title = "2",
                    Message = "Two"
                },
                new TestTimeLineItem
                {
                    Title = "3",
                    Message = "Three"
                },
                new TestTimeLineItem
                {
                    Title = "4",
                    Message = "Four"
                }
            };

            var timeLineAdapter = new TestTimeLineAdapter(items, true);
            timeLine.SetAdapter(timeLineAdapter);
            timeLine.MarkerType = TimeLineMarkerType.Icon;
            timeLine.TimeLinePositioin = -1;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private class TestTimeLineAdapter
            : TimeLineView.TimeLineAdapter
        {
            private readonly bool mShowImageNext;

            public TestTimeLineAdapter(IEnumerable<TimeLineItem> items, bool showImageNext)
               : base(items)
            {
                mShowImageNext = showImageNext;
            }

            public override int GetItemViewType(int position)
            {
                return position;
            }

            public override void OnBindViewHolder(TimeLineView.TimeLineContentViewHolder holder, int position)
            {
                var timeLineViewHolder = (TestDetailViewHolder)holder;
                var timeLineItem = (TestTimeLineItem)base.Items.ElementAt(position);

                timeLineViewHolder.Title.Text = timeLineItem.Title;

                if (string.IsNullOrEmpty(timeLineItem.Message))
                {
                    timeLineViewHolder.Message.Visibility = ViewStates.Gone;
                }
                else
                {
                    timeLineViewHolder.Message.Visibility = ViewStates.Visible;
                    timeLineViewHolder.Message.Text = timeLineItem.Message;
                }

                timeLineViewHolder.ImageNext.Visibility = mShowImageNext ? ViewStates.Visible : ViewStates.Gone;
            }

            public override TimeLineView.TimeLineContentViewHolder OnCreateContentViewHolder(ViewGroup parent, int viewType)
            {
                var context = parent.Context;
                var layoutInflater = LayoutInflater.From(context);
                View view = layoutInflater.Inflate(Resource.Layout.time_line_button_title_message, parent, false);

                return new TestDetailViewHolder(view);
            }
        }

        private class TestDetailViewHolder
            : TimeLineView.TimeLineContentViewHolder
        {
            public TextView Title { get; private set; }
            public TextView Message { get; private set; }
            public View ImageNext { get; private set; }

            public TestDetailViewHolder(View view)
                : base(view)
            {
                Title = view.FindViewById<TextView>(Resource.Id.time_line_title);
                Message = view.FindViewById<TextView>(Resource.Id.time_line_message);
                ImageNext = view.FindViewById<View>(Resource.Id.image_next);
            }
        }

        private class TestTimeLineItem
            : TimeLineItem, ITimeLineItem
        {
            public string Title { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
        }
    }
}
