using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using B4.PE4.BryonB.Domain.Models;
using B4.PE4.BryonB.Domain.Services.Abstract;
using System.Collections.ObjectModel;

namespace B4.PE4.BryonB.Domain.Services.SqliteAccess
{
    public class AppModelSQLiteService : SQLiteServiceBase, IAppModelService
    {
        public async Task<ObservableCollection<ShoppingList>> GetAllShoppingLists()
        {
            return await Task.Run<ObservableCollection<ShoppingList>>(() =>
            {
                try
                {
                    var shoppinglists = new ObservableCollection<ShoppingList>(connection.GetAllWithChildren<ShoppingList>(recursive: false));
                    return shoppinglists;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<ObservableCollection<ShoppingDetail>> GetAllShoppingDetailsByShoppigListGuid(Guid guid)
        {
            return await Task.Run<ObservableCollection<ShoppingDetail>>(() =>
            {
                try
                {
                    //var shoppingdetails = connection.Table<ShoppingDetail>()
                    //.Where(e => e.ShoppingListId == guid)
                    //.ToList<ShoppingDetail>();
                    // var shoppingdetails = .ToList();
                    //var x = connection.GetAllWithChildren<ShoppingDetail>(e => e.ShoppingList.ShoppingListId.Equals(guid), true);
                    //var shoppingdetails = new ObservableCollection<ShoppingDetail>(x);
                    //var x = connection.Table<ShoppingDetail>().Where(e => e.ShoppingListId == guid).FirstOrDefault();

                    var shoppingdetails = new ObservableCollection<ShoppingDetail>();
                    var p = connection.Table<ShoppingDetail>();
                    foreach (ShoppingDetail shoppingdetail in p)
                    {
                        if (shoppingdetail.ShoppingListId == guid)
                        {
                            connection.GetChildren<ShoppingDetail>(shoppingdetail, true);
                            shoppingdetails.Add(shoppingdetail);
                        }
                    }
                    return shoppingdetails;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<ShoppingList> CreateNewShoppingList()
        {
            return await Task.Run(() =>
            {
                try
                {
                    ShoppingList x = new ShoppingList
                    {
                        ShoppingListId = Guid.NewGuid(),
                        ShoppingDetails = new ObservableCollection<ShoppingDetail>(),
                        Naam = "Your new shopping list"
                    };
                    connection.InsertOrReplaceWithChildren(x);
                    return x;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<ObservableCollection<Product>> GetAllProducts()
        {
            return await Task.Run<ObservableCollection<Product>>(() =>
            {
                try
                {
                    var p = new ObservableCollection<Product>(connection.GetAllWithChildren<Product>());
                    // var shoppinglists = new ObservableCollection<ShoppingList>(connection.Table<ShoppingList>());
                    return p;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public Task<Product> GetScannedProductFromShoppingList(ShoppingList shoppingList, string s)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingList> GetShoppingListByGuid(Guid guid)
        {
            return await Task.Run<ShoppingList>(() =>
            {
                try
                {
                    //ShoppingList shoppingList = connection.Table<ShoppingList>().Where(e => e.ShoppingListId == guid).FirstOrDefault();
                    ShoppingList shoppingList = connection.Find<ShoppingList>(guid);
                    if (shoppingList != null)
                        connection.GetChildren<ShoppingList>(shoppingList, true);
                    return shoppingList;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveShoppingDetail(ShoppingDetail shoppingDetail)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplaceWithChildren(shoppingDetail);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveProduct(Product product)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplaceWithChildren(product);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveShoppingList(ShoppingList shoppingList)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplaceWithChildren(shoppingList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SetScannedProductFromShoppingList(ShoppingList shoppingList, string s)
        {
            ShoppingDetail sd = new ShoppingDetail();
            ShoppingList sl = new ShoppingList();
            await Task.Run(() =>
            {
                try
                {
                    // doet het niet. Product = null..... tiens.
                    //sd = connection.Table<ShoppingList>().
                    //Where(e => e.ShoppingListId == shoppingList.ShoppingListId).FirstOrDefault().ShoppingDetails.
                    //Where(f => f.Product.Result == s).
                    //FirstOrDefault();
                    //sd.GescannedAantal = sd.GescannedAantal--;
                    //connection.InsertOrReplaceWithChildren(sd);
                    // daarom:
                    sl = connection.Table<ShoppingList>().
                    Where(e => e.ShoppingListId == shoppingList.ShoppingListId).First();
                    if (sl != null)
                    {
                        connection.GetChildren<ShoppingList>(sl, true);
                        sd = sl.ShoppingDetails.Where(e => e.Product.Result == s).FirstOrDefault();
                        if (!(sd == null))
                        {
                            sd.GescannedAantal = sd.GescannedAantal + 1;
                            connection.InsertOrReplaceWithChildren(sd);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });


        }

        public async Task SaveAppModel(AppModel appModel)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplaceWithChildren(appModel);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task DeleteProduct(Product product)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<Product>(product.ProductId);

                    var teVerwijderenDetails =
                    connection.Table<ShoppingDetail>().Where(e => e.ProductId == product.ProductId).ToList<ShoppingDetail>();
                    foreach (ShoppingDetail x in teVerwijderenDetails)
                    {
                        connection.Delete<ShoppingDetail>(x.ShoppingDetailId);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task DeleteShoppingList(ShoppingList shoppingList)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<ShoppingList>(shoppingList.ShoppingListId);

                    var teVerwijderenDetails =
                    connection.Table<ShoppingDetail>().Where(e => e.ShoppingListId == shoppingList.ShoppingListId).ToList<ShoppingDetail>();
                    foreach (ShoppingDetail x in teVerwijderenDetails)
                    {
                        connection.Delete<ShoppingDetail>(x.ShoppingDetailId);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task DeleteShoppingDetail(ShoppingDetail shoppingDetail)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<ShoppingDetail>(shoppingDetail.ShoppingDetailId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
