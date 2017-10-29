using Windows.Storage;
using SQLite;
using Surveys.Core.ServiceInterfaces;
using Surveys.UWP.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(SqliteService))]

namespace Surveys.UWP.Services
{
    public class SqliteService : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, "surveys.db"));
            return conn;
        }
    }
}
