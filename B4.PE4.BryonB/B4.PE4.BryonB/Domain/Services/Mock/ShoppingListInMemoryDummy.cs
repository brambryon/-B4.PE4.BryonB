using B4.PE4.BryonB.Domain.Models;
using B4.PE4.BryonB.Domain.Services.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using System.Collections.Generic;

namespace B4.PE4.BryonB.Domain.Services.Mock
{
    public class ShoppingListInMemoryDummy : IAppModelService
    {
        public static AppModel appModel;
       

        public async Task<ObservableCollection<ShoppingList>> GetAllShoppingLists()
        {
            await Task.Delay(1);
            if (appModel == null)
            {
                appModel = InitializeAppModel();
            }
            return appModel.ShoppingLists;
        }

        public async Task<ObservableCollection<Product>> GetAllProducts()
        {
            await Task.Delay(1);
            if (appModel == null)
            {
                appModel = InitializeAppModel();
            }
            return appModel.BeschikbareProducten;
        }

        public async Task<ShoppingList> GetShoppingListByGuid(Guid guid)
        {
            await Task.Delay(0);
            return appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == guid);
        }

        public async Task AddProductToShoppingList(Guid guid, Product product)
        {
            await Task.Delay(0);
            //  appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == guid).Producten.Add(product);
            appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == guid).ShoppingDetails.Add(
                new ShoppingDetail
                {
                    ShoppingDetailId = Guid.NewGuid(),
                    Product = product
                }
                );
        }
        public async Task<Product> GetScannedProductFromShoppingList(ShoppingList shoppingList, String s)
        {
            await Task.Delay(0);
            try
            {
                //return appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == shoppingList.ShoppingListId).Producten.First(x => x.Result == s);
                return appModel.ShoppingLists.
                    FirstOrDefault(b => b.ShoppingListId == shoppingList.ShoppingListId).ShoppingDetails.First(x => x.Product.Result == s).Product;
            }
            catch
            {
                return null;
            }
        }

        public async Task SetScannedProductFromShoppingList(ShoppingList shoppingList, String s)
        {
            await Task.Delay(0);
           
                //return appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == shoppingList.ShoppingListId).Producten.First(x => x.Result == s);
                //return appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == shoppingList.ShoppingListId).ShoppingDetails.First(x => x.Product.Result == s).Product;
                //appModel.ShoppingLists.
                    //FirstOrDefault(b => b.ShoppingListId == shoppingList.ShoppingListId).ShoppingDetails.First(x => x.Product.Result == s).Scanned = true;           
        }

        public async Task SaveProduct(Product product)
        {
            //c = new ShoppingDataAccess();
            await Task.Delay(0);
            if (product.ProductId == Guid.Empty)
            {
                product.ProductId = Guid.NewGuid();
                appModel.BeschikbareProducten.Add(product);
                //c.AddProduct(product);
                //string x = c.Products.Count().ToString();
            }
        }

        public async Task SaveShoppingList(ShoppingList shoppingList)
        {
            await Task.Delay(0);
            appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == shoppingList.ShoppingListId).ShoppingDetails = shoppingList.ShoppingDetails;
            appModel.ShoppingLists.FirstOrDefault(b => b.ShoppingListId == shoppingList.ShoppingListId).Naam = shoppingList.Naam;
        }

        public async Task<ShoppingList> CreateNewShoppingList()
        {
            await Task.Delay(0);
            ShoppingList x = new ShoppingList
            {
                ShoppingListId = Guid.NewGuid(),
                ShoppingDetails = new ObservableCollection<ShoppingDetail>(),
                Naam = "Your new shopping list"
            };
            appModel.ShoppingLists.Add(x);
            return x;
        }

        public static AppModel InitializeAppModel()
        {
            var x1Guid = Guid.NewGuid();
            var x = new AppModel();
            var x1 = new ShoppingList
            {
                ShoppingListId = x1Guid,
                Naam = "Je eerste lijst.",
                ShoppingDetails = new ObservableCollection<ShoppingDetail>()
            };
            x.ShoppingLists = new ObservableCollection<ShoppingList>();
            x.ShoppingLists.Add(x1);

            x.BeschikbareProducten = new ObservableCollection<Product>();
            x.BeschikbareProducten.Add(new Product
            {
                Naam = "p1",
                ProductId = Guid.NewGuid()
            });
            x.BeschikbareProducten.Add(new Product
            {
                Naam = "p2",
                ProductId = Guid.NewGuid(),
                Result = "2299995005994"
            });
            x.BeschikbareProducten.Add(new Product
            {
                Naam = "p3",
                ProductId = Guid.NewGuid(),
                Result = "5414233169307"
            });
            return x;
        }

        public Task SaveShoppingDetail(ShoppingDetail shoppingDetail)
        {
            throw new NotImplementedException();
        }

        public Task SaveAppModel(AppModel appModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteShoppingList(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }

        public Task DeleteShoppingDetail(ShoppingDetail shoppingDetail)
        {
            throw new NotImplementedException();
        }
       
        public Task<ObservableCollection<ShoppingDetail>> GetAllShoppingDetailsByShoppigListGuid(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
