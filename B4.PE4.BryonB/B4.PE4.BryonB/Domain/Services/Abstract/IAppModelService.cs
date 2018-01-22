using B4.PE4.BryonB.Domain.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace B4.PE4.BryonB.Domain.Services.Abstract
{
    public interface IAppModelService
    {
        Task<ObservableCollection<ShoppingList>> GetAllShoppingLists();
        Task<ObservableCollection<Product>> GetAllProducts();
        Task<ShoppingList> GetShoppingListByGuid(Guid guid);
        Task<ObservableCollection<ShoppingDetail>> GetAllShoppingDetailsByShoppigListGuid(Guid guid);
        Task<Product> GetScannedProductFromShoppingList(ShoppingList shoppingList, String s);        
               
        Task SetScannedProductFromShoppingList(ShoppingList shoppingList, String s);

        Task SaveAppModel(AppModel appModel);
        Task SaveProduct(Product product);
        Task SaveShoppingDetail(ShoppingDetail shoppingDetail);
        Task SaveShoppingList(ShoppingList shoppingList);
        Task<ShoppingList> CreateNewShoppingList();

        Task DeleteProduct(Product product);
        Task DeleteShoppingList(ShoppingList shoppingList);
        Task DeleteShoppingDetail(ShoppingDetail shoppingDetail);
    }
}
