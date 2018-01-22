using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B4.PE4.BryonB.Domain.Services;
using B4.PE4.BryonB.Domain.Services.Abstract;
using B4.PE4.BryonB.Domain.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE4.BryonB.ViewModels
{
    class MainViewModel : FreshBasePageModel
    {
        IAppModelService appModelService;
        public MainViewModel(IAppModelService appModelService)
        {
            this.appModelService = appModelService;
        }
        private ObservableCollection<ShoppingList> shoppingLists;
        public ObservableCollection<ShoppingList> ShoppingLists
        {
            get { return shoppingLists; }
            set
            {
                shoppingLists = value;
                RaisePropertyChanged(nameof(ShoppingLists));
            }
        }       
        public ICommand ManageYourProductsCommand => new Command(
            async () =>
            {
                await CoreMethods.PushPageModel<ProductListViewModel>();
                await RefreshShoppingLists();
            }
        );
        public ICommand AddNewShoppingListCommand => new Command(
           async () =>
           {
               ShoppingList NieuweShoppingList = await appModelService.CreateNewShoppingList();
               await CoreMethods.PushPageModel<ShoppingListViewModel>(NieuweShoppingList);
               await RefreshShoppingLists();
           }
       );
        public ICommand GoShoppingCommand => new Command(
           async () =>
           {               
               await CoreMethods.PushPageModel<ShoppingViewModel>();
               await RefreshShoppingLists();
           }
       );
        public ICommand EditShoppingListCommand => new Command<ShoppingList>(
           async (ShoppingList shoppingList) =>
           {
               await CoreMethods.PushPageModel<ShoppingListViewModel>(shoppingList);
               await RefreshShoppingLists();
           }
       );

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshShoppingLists();
        }

        public async override void ReverseInit(object returnedData)
        {
            await RefreshShoppingLists();
        }

        private async Task RefreshShoppingLists()
        {
            var x = await appModelService.GetAllShoppingLists();
            ShoppingLists = null;
            ShoppingLists = x;
            LoadMainState();
        }

        private void LoadMainState()
        {
            ShoppingLists = new ObservableCollection<ShoppingList>(shoppingLists);
        }

    }
}
