using System;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace com.xamarin.recipes.filepicker
{
    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;

    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class RecentFilesActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.recent_files);

            Button but = FindViewById<Button>(Resource.Id.button2);

            but.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof (FilePickerActivity));
                StartActivity(intent);
            };

        }
        
    }
}
