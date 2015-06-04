using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using MusicXMLViewer.Android.Domain;
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

            //notationLayout = FindViewById<LinearLayout>(Resource.Id.notationLayout);

            try
            {
                OpenFileAsync();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }


        }

        async void OpenFileAsync()
        {

            progressLayout.Visibility = ViewStates.Visible;
            score = await DeserializeObjectAsync<scorepartwise>(path);
            //score = await DeserializeObjectAsync(path);
            this.progressLayout.Visibility = ViewStates.Gone;
            DrawNotation();
        }

        //private static Task<Score> DeserializeObjectAsync(string xml)
        //{
        //    return Task.Run(() => MusicXmlParser.GetScore(xml));
        //}


        private static Task<T> DeserializeObjectAsync<T>(string xml)
        {
            return Task.Run(() =>
            {
                using (var fileStream = new FileStream(xml, FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        XmlSerializer serializer =
                            new XmlSerializer(typeof(T));
                        T theObject = (T)serializer.Deserialize(fileStream);
                        return theObject;
                    }
                }
            });
        }

        void DrawNotation()
        {
            //foreach (var part in score.Parts)
            //{
            //    var linLayout = new LinearLayout(this);
            //    linLayout.Orientation = Orientation.Horizontal;
            //    linLayout.SetMinimumHeight(50);

            //    var measures = new List<View>();
            //    foreach (var measure in part.Measures)
            //    {
            //        var measureLayout = new View(this);
            //        measureLayout.SetMinimumWidth(measure.Width);
            //        measureLayout.SetBackgroundColor(new Color(255, 0, 0));
            //        measures.Add(measureLayout);
            //    }

            //    notationLayout.AddChildrenForAccessibility(measures);
            //}

            var scoreDrawer = new ScoreDrawer(this, score);
            scoreDrawer.HorizontalScrollBarEnabled = true;
            SetContentView(scoreDrawer);
        }
    }
}