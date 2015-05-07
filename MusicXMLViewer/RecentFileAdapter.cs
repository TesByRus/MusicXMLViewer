using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.xamarin.recipes.filepicker
{
    class RecentFileAdapter : ArrayAdapter<string>
    {
        Context context;

        public RecentFileAdapter(Context c)
            : base(c, Resource.Layout.RecentFileItem, Android.Resource.Id.Text1)
        {
            context = c;
        }


        public void AddRecentFilesInfo(IEnumerable<string> fileInfo)
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

            viewHolder.Update("someFile - Some file");

            return view;
        }

        // references to our images
        int[] thumbIds = { };
    }
}