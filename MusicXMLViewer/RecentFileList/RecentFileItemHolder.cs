using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Object = Java.Lang.Object;

namespace com.xamarin.recipes.filepicker
{
    class RecentFileItemHolder : Object
    {
        public RecentFileItemHolder(TextView textView)
        {
            TextView = textView;
        }

        public TextView TextView { get; private set; }

        /// <summary>
        ///   This method will update the TextView and the ImageView that are
        ///   are
        /// </summary>
        /// <param name="fileName"> </param>
        public void Update(string fileName)
        {
            TextView.Text = fileName;
        }
    }
}