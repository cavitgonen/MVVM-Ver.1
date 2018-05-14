using MVVM.Droid.ConnectionHelper;
using MVVM.Helper;
using SQLite;

[assembly:Xamarin.Forms.Dependency(typeof(GetDroidConnection))]
namespace MVVM.Droid.ConnectionHelper
{
    public class GetDroidConnection : ISQLiteConnection
    {
        public SQLiteConnection GetConnection()
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentPath, App.DbName);
            //var _SQLiteFlags = new SQLiteOpenFlags();
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}