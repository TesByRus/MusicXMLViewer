using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Java;
using Java.Util.Jar;
using MusicXMLViewer.Android.Notation;


namespace MusicXMLViewer.Android
{
    [Activity(Label = "NotationActivity")]
    public class NotationActivity : Activity
    {

        private string path;

        //private Score score;
        private scorepartwise score;

        private LinearLayout progressLayout;
        private LinearLayout notationLayout;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.Hide();
            SetContentView(Resource.Layout.notation);
            path = Intent.GetStringExtra("path");
            Toast.MakeText(this, "You opened file " + path, ToastLength.Short).Show();

            this.progressLayout = FindViewById<LinearLayout>(Resource.Id.progressLayout);
            this.progressLayout.Visibility = ViewStates.Gone;



            try
            {
                OpenFileAsync();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }


        }





        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            var menuItem = menu.FindItem(Resource.Id.menuItem);
            //Switch s = FindViewById<Switch>(Resource.Id.switchForActionBar);

            //s.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e)
            //{
            //    var toast = Toast.MakeText(this, "Your answer is " +
            //                                     (e.IsChecked ? "correct" : "incorrect"), ToastLength.Short);
            //    toast.Show();
            //};
            return true;
        }


        async void OpenFileAsync()
        {
            progressLayout.Visibility = ViewStates.Visible;
            var deserializer = new MusicXMLDeserializer();
            score = await deserializer.DeserializeObjectAsync<scorepartwise>(path);
            ActionBar.Show();
            ActionBar.Title = score.work.worktitle;
            this.progressLayout.Visibility = ViewStates.Gone;
            DrawNotation();
        }


        void DrawNotation()
        {
            LayoutInflater inflater = LayoutInflater.From(this);
            var modScore = new ScoreModified(score);


            List<View> pages = new List<View>();

            foreach (var p in modScore.Pages)
            {
                var page = new ScorePageView(this, p);
                pages.Add(page);
            }




            MyPagerAdapter pagerAdapter = new MyPagerAdapter(pages);
            var viewPager = new ViewPager(this);
            viewPager.Adapter = pagerAdapter;
            viewPager.CurrentItem = 0;     

            SetContentView(viewPager);
        }

        int GetScoreMeasureCount()
        {
            if (score.part.Length != 0)
            {
                return score.part[0].measure.Length;
            }
            return 0;
        }
    }
}