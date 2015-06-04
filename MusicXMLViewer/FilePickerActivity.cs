using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using MusicXMLViewer.Android.FileList;

namespace MusicXMLViewer.Android
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
                case global::Android.Resource.Id.Home:
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