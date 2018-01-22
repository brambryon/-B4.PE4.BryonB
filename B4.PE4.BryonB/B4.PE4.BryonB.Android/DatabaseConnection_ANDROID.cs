using SQLite.Net;
using SQLite.Net.Interop;
using System.IO;
using Xamarin.Forms;
using B4.PE4.BryonB.Domain.Services.Abstract;
using System;
using SQLite.Net.Platform.XamarinAndroid;

[assembly: Dependency(typeof(B4.PE4.BryonB.Droid.DatabaseConnection_ANDROID))]
namespace B4.PE4.BryonB.Droid
{
    internal class DatabaseConnection_ANDROID : IDatabase­Connection
    {
        public SQLiteConnection CreateConnection(string databaseFileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, databaseFileName);
            return new SQLiteConnection(new SQLitePlatformAndroid(),
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
                );
        }
    }
}
