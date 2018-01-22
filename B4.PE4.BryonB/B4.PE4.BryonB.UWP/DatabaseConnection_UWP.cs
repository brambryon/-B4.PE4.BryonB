using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using B4.PE4.BryonB.Domain.Services.Abstract;

[assembly: Dependency(typeof(B4.PE4.BryonB.UWP.DatabaseConnection_UWP))]
namespace B4.PE4.BryonB.UWP
{
    internal class DatabaseConnection_UWP : IDatabase­Connection
    {
        public SQLiteConnection CreateConnection(string databaseFileName)
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            path = Path.Combine(path, databaseFileName);

            return new SQLiteConnection(
                new SQLitePlatformWinRT(),
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
            );
        }
    }
}
