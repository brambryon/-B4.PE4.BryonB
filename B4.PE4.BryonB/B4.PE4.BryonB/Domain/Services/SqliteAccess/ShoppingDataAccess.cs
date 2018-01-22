//using System.Linq;
//using Xamarin.Forms;
//using System.Collections.ObjectModel;
////using B4.PE4.BryonB.Domain.Models.SQLite;
//using B4.PE4.BryonB.Domain.Models;
//using B4.PE4.BryonB.Domain.Services.Abstract;
//using System;
//using System.Threading.Tasks;
//using SQLite.Net;
//// NIET MEER GEBRUIKEN ////
//// NIET MEER GEBRUIKEN ////
//// NIET MEER GEBRUIKEN ////
//// NIET MEER GEBRUIKEN ////
//// NIET MEER GEBRUIKEN ////
//// NIET MEER GEBRUIKEN ////
//namespace B4.PE4.BryonB.Domain.Services.SqliteAccess
//{
//    public class ShoppingDataAccess : IAppModelService
//    {
//        private SQLiteConnection database;
//        private static object collisionLock = new object();
//        public ObservableCollection<Product> Products { get; set; }

//        public ShoppingDataAccess()
//        {
//            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
//            database.CreateTable<Product>();
//            database.CreateTable<ShoppingDetail>();
//            database.CreateTable<ShoppingList>();
//            database.CreateTable<AppModel>();

//            this.Products = new ObservableCollection<Product>(database.Table<Product>());
//            // If the table is empty, initialize the collection
//            if (!database.Table<Product>().Any())
//            {
//                // AddNewCustomer();
//            }
//        }        

//        public async Task<ObservableCollection<ShoppingList>> GetAllShoppingLists()
//        {
//            await Task.Delay(0);
//            return new ObservableCollection<ShoppingList>(database.Table<ShoppingList>());
//        }

//        public async Task<ObservableCollection<Product>> GetAllProducts()
//        {
//            await Task.Delay(0);
//            return new ObservableCollection<Product>(database.Table<Product>().AsEnumerable());
//        }

//        public async Task<ShoppingList> GetShoppingListByGuid(Guid guid)
//        {
//            await Task.Delay(0);
//            lock (collisionLock)
//            {
//                return database.Table<ShoppingList>().
//                FirstOrDefault(x => x.ShoppingListId == guid);
//            }
//        }

//        public Task AddProductToShoppingList(Guid guid, Product product)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<Product> GetScannedProductFromShoppingList(ShoppingList shoppingList, string s)
//        {
//            await Task.Delay(0);
//            try
//            {
//                Product p = database.Table<Product>().
//                    FirstOrDefault(b => b.Result == s);
//                return p;              
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        public async Task SetScannedProductFromShoppingList(ShoppingList shoppingList, string s)
//        {
//            await Task.Delay(0);
//            //Product p = database.Table<Product>().
//            //    FirstOrDefault(b => b.Result == s);
//            //database.Table<ShoppingDetail>()
//            //    .FirstOrDefault(b => b.Product == p.ProductId && b.ShoppingList == shoppingList.ShoppingListId)
//            //    .Scanned = true;
//        }

//        public async Task SaveProduct(Product product)
//        {
//            await Task.Delay(0);
//            database.Insert(product);
//        }

//        public async Task SaveShoppingList(ShoppingList shoppingList)
//        {
//            await Task.Delay(0);
//            database.Insert(shoppingList);
//        }

//        public async Task<ShoppingList> CreateNewShoppingList()
//        {
//            await Task.Delay(0);
//            ShoppingList x = new ShoppingList
//            {
//                ShoppingListId = Guid.NewGuid(),
//                ShoppingDetails = new ObservableCollection<ShoppingDetail>(),
//                Naam = "Your new shopping list"
//            };
//            database.Insert(x);
//            return x;
//        }       
        
//    }
//}
