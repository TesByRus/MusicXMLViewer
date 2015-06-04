using System.Collections.Generic;
using SQLite;

namespace MusicXMLViewer.Android.RecentFileList
{
    class DatabaseWorker
    {
        private string sqlFolder;
        private SQLiteConnection sqlConn;
        private int _recentFilesCount;

        public DatabaseWorker(int recentFilesCount = 9)
        {
            _recentFilesCount = recentFilesCount;

            sqlFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            sqlConn = new SQLiteConnection(System.IO.Path.Combine(sqlFolder, "recentfiles.db"));
            sqlConn.CreateTable<RecentFile>();
            LoadRecentFilesPath();
        }

        /// <summary>
        /// Загружаем список путей к последним открытым файлам
        /// </summary>
        /// <returns></returns>
        public List<RecentFile> LoadRecentFilesPath()
        {
            var recentOpenedFileList = new List<RecentFile>();
            recentOpenedFileList.AddRange(sqlConn.Table<RecentFile>());
            return recentOpenedFileList;
        }

        /// <summary>
        /// Сохраняем в памяти устройства список последних открытых файлов
        /// </summary>
        private void SaveRecentFilesPath(List<RecentFile> resentFilesPathList)
        {
            //TODO сохранение списка в памяти устройства
            sqlConn.DeleteAll<RecentFile>();
            sqlConn.InsertAll(resentFilesPathList);
        }

        /// <summary>
        /// Добавляем новый путь в последние открытые файлы
        /// </summary>
        /// <param name="path"></param>
        public void AddRecentFilePath(string path)
        {
            var newList = new List<RecentFile>();
            var oldList = LoadRecentFilesPath();

            if (oldList.Find(p => p.Path == path) != null)
            {
                oldList.Remove(oldList.Find(p => p.Path == path));
            }
            var newRecFile = new RecentFile() { Path = path };
            newList.Add(newRecFile);
            if (newList.Count >= _recentFilesCount)
            {
                newList.AddRange(oldList.GetRange(0, _recentFilesCount - 1));
            }
            else
            {
                newList.AddRange(oldList); 
            }
            SaveRecentFilesPath(newList);
        }

    }
}
