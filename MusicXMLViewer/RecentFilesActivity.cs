using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MusicXMLViewer.Android.FileList;
using MusicXMLViewer.Android.RecentFileList;

namespace MusicXMLViewer.Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class RecentFilesActivity : Activity
    {

        private List<RecentFile> _recentOpenedFileList;
        private const int RecentFilesCount = 9;

        private RecentFileAdapter recentFileAdapter;


        public delegate void OpenFileContainer(string str);

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.recent_files);
            UpdateRecentFileView();
            Button but = FindViewById<Button>(Resource.Id.button2);
            but.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(FilePickerActivity));
                StartActivity(intent);
            };
            
            FileListFragment.OnOpenFile += OpenFile;
            RecentFileItemHolder.OnRecentFileClick += OpenFile;

            ActionBar.Title = "Recently opened files";
            
        }



        void OpenFile(string path)
        {
            if (path != null)
            {
                var intent = new Intent(this, typeof (NotationActivity));
                intent.PutExtra("path", path);
                StartActivity(intent);
            }
            else
            {
                //TODO сделать удаление информации о файле 
            }
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
