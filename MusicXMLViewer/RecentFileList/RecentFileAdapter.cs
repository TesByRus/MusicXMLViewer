using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using MusicXMLViewer.Android.FileList;

namespace MusicXMLViewer.Android.RecentFileList
{
    class RecentFileAdapter : ArrayAdapter<RecentFile>
    {
        Context context;

        public RecentFileAdapter(Context c)
            : base(c, (int)Resource.Layout.RecentFileItem, (int) global::Android.Resource.Id.Text1)
        {
            context = c;
        }


        public void AddRecentFilesInfo(IEnumerable<RecentFile> fileInfo)
        {
            Clear();
            // Notify the _adapter that things have changed or that there is nothing 
            // to display.
            if (fileInfo.Any())
            {
#if __ANDROID_11__
                // .AddAll was only introduced in API level 11 (Android 3.0). 
                // If the "Minimum Android to Target" is set to Android 3.0 or 
                // higher, then this code will be used.
                //AddAll(fileInfo.ToArray());
                foreach (var i in fileInfo)
                {
                    Add(i);
                }
#else
                // This is the code to use if the "Minimum Android to Target" is
                // set to a pre-Android 3.0 API (i.e. Android 2.3.3 or lower).
                lock (this)
                    foreach (var i in fileInfo)
                    {
                        Add(i);
                    }
#endif
                NotifyDataSetChanged();
            }
            else
            {
                NotifyDataSetInvalidated();
            }
        }

       

        // create a new ImageView for each item referenced by the Adapter
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textView;

            var item = GetItem(position);

            View view;
            RecentFileItemHolder viewHolder;

            if (convertView == null)
            {
                textView = new TextView(context);

                view = context.GetLayoutInflater().Inflate(Resource.Layout.RecentFileItem, parent, false);
                viewHolder = new RecentFileItemHolder(view.FindViewById<TextView>(Resource.Id.textViewRecentFile));
                view.Tag = viewHolder;
            }
            else
            {
                view = convertView;
                viewHolder = (RecentFileItemHolder)view.Tag;
            }

            string fileName;
            if (!string.IsNullOrEmpty(item.Title))
            {
                if (!string.IsNullOrEmpty(item.Author))
                {
                    viewHolder.Update(item.Title + " (" + item.Author + ")", item.Path);
                }
                else
                {
                    viewHolder.Update(item.Title, item.Path);
                }
            }
            else
            {
                var splited = item.Path.Split('/');
                var lastSplited = splited.Last().Split('.');
                var str = "";
                for (var i = 0; i < lastSplited.Count() - 1; i++)
                {
                    str += lastSplited[i];
                }
                viewHolder.Update(str, item.Path);
            }

            return view;
        }

    }
}