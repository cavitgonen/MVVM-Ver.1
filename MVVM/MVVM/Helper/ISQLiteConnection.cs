using SQLite;

namespace MVVM.Helper
{
    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}
