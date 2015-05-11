using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace com.xamarin.recipes.filepicker
{
    [Activity(Label = "FilePickerActivity")]
    public class FilePickerActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.file_explorer);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetLogo(Resource.Drawable.ic_launcher);
            FileListFragment.OnOpenDirectory += UpdateLabel;
        }

        void UpdateLabel(string dir)
        {
            ActionBar.Title = "/" + dir.Split('/').Last();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }


        public delegate void OnBackPressedContainer();

        public static event OnBackPressedContainer OnBackPressedEvent;

        public override void OnBackPressed()
        {
            try
            {
                if (OnBackPressedEvent != null) OnBackPressedEvent();
            }
            catch (RootDirectoryException)
            {
                base.OnBackPressed();
            }
            
        }
    }
}