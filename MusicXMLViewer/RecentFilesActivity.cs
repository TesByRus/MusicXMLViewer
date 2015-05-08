using System;
using System.IO;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace com.xamarin.recipes.filepicker
{
    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;

    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class RecentFilesActivity : Activity
    {

        private List<RecentFile> _recentOpenedFileList;
        private const int RecentFilesCount = 9;

        private RecentFileAdapter recentFileAdapter;
        
        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.recent_files);
            UpdateRecentFileView();
            Button but = FindViewById<Button>(Resource.Id.button2);
            but.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(FilePickerActivity));
                StartActivity(intent);
            };
            
            base.OnCreate(bundle);
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            UpdateRecentFileView();
        }

        void UpdateRecentFileView()
        {
            var db = new DatabaseWorker();
            _recentOpenedFileList = new List<RecentFile>(db.LoadRecentFilesPath());

            recentFileAdapter = new RecentFileAdapter(this);
            recentFileAdapter.AddRecentFilesInfo(_recentOpenedFileList);

            GridView grid = FindViewById<GridView>(Resource.Id.gridViewRecentFiles);

            grid.Adapter = recentFileAdapter;
        }
    }
}
