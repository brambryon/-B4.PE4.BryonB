using SQLite.Net;

namespace B4.PE4.BryonB.Domain.Services.Abstract
{
    public interface IDatabase­Connection
    {
        //SQLite.Net.SQLiteConnection DbConnection();
        
        SQLiteConnection CreateConnection(string databaseFileName);
    }
}
