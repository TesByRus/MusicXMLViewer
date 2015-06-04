using System;
using Android.Widget;
using Object = Java.Lang.Object;

namespace MusicXMLViewer.Android.RecentFileList
{
    class RecentFileItemHolder : Object
    {
        public RecentFileItemHolder(TextView textView)
        {
            TextView = textView;
            TextView.Click += OpenFile;
        }

        public delegate void RecentFileClick(string path);
        public static event RecentFileClick OnRecentFileClick;

        private void OpenFile(object o, EventArgs e)
        {
            if (OnRecentFileClick != null) OnRecentFileClick(FullPath);
        }

        public TextView TextView { get; private set; }
        public string FullPath { get; private set; }

        /// <summary>
        ///   This method will update the TextView and the ImageView that are
        ///   are
        /// </summary>
        /// <param name="fileName"> </param>
        public void Update(string fileName, string fullPath)
        {
            TextView.Text = fileName;
            this.FullPath = fullPath;
        }
    }
}