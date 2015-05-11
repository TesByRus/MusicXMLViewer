using System.Runtime.Serialization;

namespace com.xamarin.recipes.filepicker
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Android.OS;
    using Android.Support.V4.App;
    using Android.Util;
    using Android.Views;
    using Android.Widget;

    [Serializable]
    public class RootDirectoryException : ApplicationException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RootDirectoryException()
        {
        }

        public RootDirectoryException(string message)
            : base(message)
        {
        }

        public RootDirectoryException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RootDirectoryException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    ///   A ListFragment that will show the files and subdirectories of a given directory.
    /// </summary>
    /// <remarks>
    ///   <para> This was placed into a ListFragment to make this easier to share this functionality with with tablets. </para>
    ///   <para> Note that this is a incomplete example. It lacks things such as the ability to go back up the directory tree, or any special handling of a file when it is selected. </para>
    /// </remarks>
    public class FileListFragment : ListFragment
    {
        public static readonly string DefaultDirectory = "/";
        private FileListAdapter _adapter;
        private DirectoryInfo _directory;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _adapter = new FileListAdapter(Activity, new FileSystemInfo[0]);
            ListAdapter = _adapter;

            FilePickerActivity.OnBackPressedEvent += GoPrevDirectory;
        }

        public delegate void MethodContainer(string dir);
        public static event MethodContainer OnOpenDirectory;


        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var fileSystemInfo = _adapter.GetItem(position);

            if (fileSystemInfo.IsFile())
            {
                // Do something with the file.  In this case we just pop some toast.
                var db = new DatabaseWorker();
                db.AddRecentFilePath(fileSystemInfo.FullName);
                Log.Verbose("FileListFragment", "The file {0} was clicked.", fileSystemInfo.FullName);
                Toast.MakeText(Activity, "You selected file " + fileSystemInfo.FullName, ToastLength.Short).Show();
            }
            else
            {
                // Dig into this directory, and display it's contents
                RefreshFilesList(fileSystemInfo.FullName);

            }

            base.OnListItemClick(l, v, position, id);
        }

        public override void OnResume()
        {
            base.OnResume();
            RefreshFilesList(DefaultDirectory);
        }


        public override void OnDestroy()
        {
            FilePickerActivity.OnBackPressedEvent -= GoPrevDirectory;
            base.OnDestroy();
        }

        void GoPrevDirectory()
        {
            if (_directory.Parent != null)
            {
                RefreshFilesList(_directory.Parent.FullName);
            }
            else
            {
                FilePickerActivity.OnBackPressedEvent -= GoPrevDirectory;
                throw new RootDirectoryException();
            }
        }


        public void RefreshFilesList(string directory)
        {
            if (OnOpenDirectory != null) OnOpenDirectory(directory);
            IList<FileSystemInfo> visibleThings = new List<FileSystemInfo>();
            var dir = new DirectoryInfo(directory);

            try
            {
                foreach (var item in dir.GetFileSystemInfos().Where(item => item.IsVisible()))
                {
                    if (item.IsDirectory() || item.Extension == ".xml" || item.Extension == ".mxl")
                        visibleThings.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.Error("FileListFragment", "Couldn't access the directory " + _directory.FullName + "; " + ex);
                Toast.MakeText(Activity, "Problem retrieving contents of " + directory, ToastLength.Long).Show();
                return;
            }

            _directory = dir;

            _adapter.AddDirectoryContents(visibleThings);

            // If we don't do this, then the ListView will not update itself when then data set 
            // in the adapter changes. It will appear to the user that nothing has happened.
            ListView.RefreshDrawableState();

            Log.Verbose("FileListFragment", "Displaying the contents of directory {0}.", directory);
        }

    }
}
