using SQLite;

namespace MusicXMLViewer.Android.RecentFileList
{
    public class RecentFile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Path { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }

    }
}