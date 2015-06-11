using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace MusicXMLViewer.Android.Notation
{
    public class MyPagerAdapter : PagerAdapter
    {
        public override bool IsViewFromObject(View view, Object @object)
        {
            return view.Equals(@object);
        }

        public override int Count
        {
            get { return pages.Count; }
        }

        List<View> pages = null;

        public MyPagerAdapter(List<View> pages)
        {
            this.pages = pages;
        }

        public override Object InstantiateItem(View collection, int position)
        {
            View v = pages[position];
            ((ViewPager)collection).AddView(v, 0);
            return v;
        }

        public override void DestroyItem(View collection, int position, Object view)
        {
            ((ViewPager)collection).RemoveView((View)view);
        }

    }
}