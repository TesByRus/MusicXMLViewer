using SQLite;

namespace com.xamarin.recipes.filepicker
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