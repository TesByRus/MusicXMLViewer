using System;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace com.xamarin.recipes.filepicker
{
    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;

    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class RecentFilesActivity : Activity
    {

        private List<string> _recentOpenedFilePathList;
        private const int RecentFilesCount = 9;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.recent_files);

            LoadRecentFilesPath();


            GridLayout grid = FindViewById<GridLayout>(Resource.Id.gridLayoutRecentFiles);
            foreach (var item in _recentOpenedFilePathList)
            {
                
                grid.
            }

            Button but = FindViewById<Button>(Resource.Id.button2);

            but.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(FilePickerActivity));
                StartActivity(intent);
            };

        }

        /// <summary>
        /// Загружаем список путей к последним открытым файлам
        /// </summary>
        /// <returns></returns>
        void LoadRecentFilesPath()
        {
            _recentOpenedFilePathList = new List<string>();
            //TODO подгрузка списка
        }

        /// <summary>
        /// Сохраняем в памяти устройства список последних открытых файлов
        /// </summary>
        void SaveRecentFilesPath()
        {
            //TODO сохранение списка в памяти устройства
        }

        /// <summary>
        /// Добавляем новый путь в последние открытые файлы
        /// </summary>
        /// <param name="path"></param>
        void AddPath(string path)
        {
            var tmp = new List<string>();
            tmp.Add(path);
            if (_recentOpenedFilePathList.Count >= RecentFilesCount)
            {
                tmp.AddRange(_recentOpenedFilePathList.GetRange(0, RecentFilesCount - 1));
            }
            _recentOpenedFilePathList = new List<string>(tmp);
            SaveRecentFilesPath();
        }

    }
}
