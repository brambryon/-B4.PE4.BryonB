using SQLite.Net;
using Xamarin.Forms;
using B4.PE4.BryonB.Domain.Models;
using B4.PE4.BryonB.Domain.Services.Abstract;

namespace B4.PE4.BryonB.Domain.Services.SqliteAccess
{
    /// <summary>
    /// Base class for ALL "SQLite" implementations of a service
    /// </summary>
    public abstract class SQLiteServiceBase
    {
        protected readonly SQLiteConnection connection;

        public SQLiteServiceBase()
        {
            //get the platform-specific SQLiteConnection
            var connectionFactory = DependencyService.Get<IDatabaseConnection>();
            connection = connectionFactory.CreateConnection("Shoppingdata.db3");

            //connection.DropTable<AppModel>();
            //connection.DropTable<Product>();
            //connection.DropTable<ShoppingDetail>();
            //connection.DropTable<ShoppingList>();

            //create tables (if not existing)
            connection.CreateTable<AppModel>();
            connection.CreateTable<Product>();
            connection.CreateTable<ShoppingDetail>();
            connection.CreateTable<ShoppingList>();
        }
    }
}
