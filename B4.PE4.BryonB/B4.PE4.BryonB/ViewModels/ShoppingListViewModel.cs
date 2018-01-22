using B4.PE4.BryonB.Domain.Models;
using B4.PE4.BryonB.Domain.Services.Abstract;
using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE4.BryonB.ViewModels
{
    public class ShoppingListViewModel : FreshBasePageModel
    {
        IAppModelService appModelService;
        ShoppingList currentShoppingList;

        public ShoppingListViewModel(IAppModelService appModelService)
        {
            this.appModelService = appModelService;
            ChangeNameIsEnabled = false;
            LabelNameIsEnabled = true;
        }
        private ObservableCollection<ShoppingDetail> shoppingDetails;
        public ObservableCollection<ShoppingDetail> ShoppingDetails
        {
            get { return shoppingDetails; }
            set
            {
                shoppingDetails = value;
                RaisePropertyChanged(nameof(ShoppingDetails));
            }
        }
        private string naam;
        public string Naam
        {
            get { return naam; }
            set
            {
                naam = value;
                RaisePropertyChanged(nameof(Naam));
            }
        }
        private Boolean changeNameIsEnabled;
        public Boolean ChangeNameIsEnabled
        {
            get { return changeNameIsEnabled; }
            set
            {
                changeNameIsEnabled = value;
                RaisePropertyChanged(nameof(ChangeNameIsEnabled));
            }
        }
        private Boolean labelNameIsEnabled;
        public Boolean LabelNameIsEnabled
        {
            get { return labelNameIsEnabled; }
            set
            {
                labelNameIsEnabled = value;
                RaisePropertyChanged(nameof(LabelNameIsEnabled));
            }
        }
        public async override void Init(object initData)
        {
            base.Init(initData);
            currentShoppingList = initData as ShoppingList;
            await RefreshShoppingList();
        }
        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshShoppingList();
        }
        public async override void ReverseInit(object returnedData)
        {
            var NieuwProductvoorShoppingList = returnedData as Product;
            if (!ShoppingDetails.Any(x => x.Product.ProductId == NieuwProductvoorShoppingList.ProductId))
            {
                ShoppingDetail nieuwSD = new ShoppingDetail
                {
                    ShoppingDetailId = Guid.NewGuid(),
                    Product = NieuwProductvoorShoppingList,
                  //  Scanned = false,
                    GescannedAantal = 0,
                    GevraagdAantal = 1
                };
                ShoppingDetails.Add(nieuwSD);
                await appModelService.SaveShoppingDetail(nieuwSD);
                currentShoppingList.ShoppingDetails = ShoppingDetails;
                await appModelService.SaveShoppingList(currentShoppingList);
            }
            await RefreshShoppingList();
        }
        private async Task RefreshShoppingList()
        {
            var x = await appModelService.GetShoppingListByGuid(currentShoppingList.ShoppingListId);
            currentShoppingList = x;
            LoadShoppingListState();
        }
        private void LoadShoppingListState()
        {
            Naam = currentShoppingList.Naam;
            ShoppingDetails = new ObservableCollection<ShoppingDetail>(currentShoppingList.ShoppingDetails);
        }
        public ICommand AddProductToShoppingListCommand => new Command<Product>(
            async (Product product) =>
            {
                await CoreMethods.PushPageModel<ProductListViewModel>(product);
            }
        );

        public ICommand DeleteShoppingDetailCommand => new Command<ShoppingDetail>(
                async (ShoppingDetail shoppingDetail) =>
            {
                await appModelService.DeleteShoppingDetail(shoppingDetail);
                await RefreshShoppingList();
            }
        );

        private bool Validate(ShoppingDetail shoppingDetail)
        {
            bool error = false;
            if (!(int.TryParse(shoppingDetail.GevraagdAantal.ToString(), out int result)))
            {
                error = true;
                //lblErrorTitle.Text = "Title cannot be empty";
                //lblErrorTitle.IsVisible = true;
            }
            if (!error)
            {
                //lblErrorTitle.Text = "";
                //lblErrorTitle.IsVisible = false;
                //lblErrorDescription.Text = "";
                //lblErrorDescription.IsVisible = false;
            }
            return !error;
        }

        public ICommand EditShoppingDetailCommand => new Command<ShoppingDetail>(
                async (ShoppingDetail shoppingDetail) =>
                {
                    if (Validate(shoppingDetail))
                    {
                        await appModelService.SaveShoppingDetail(shoppingDetail);
                        await RefreshShoppingList();
                    }
                }
        );
        public ICommand DeleteShoppingListCommand => new Command<Product>(
       async (Product product) =>
       {
           //await CoreMethods.PushPageModel<ProductListViewModel>(product);
           await appModelService.DeleteShoppingList(currentShoppingList);
           await CoreMethods.PopPageModel();
       }
        );
        public ICommand TerugCommand => new Command<Product>(
          async (Product product) =>
          {
              await CoreMethods.PopPageModel();
          }
      );
        public ICommand WijzigNaamShoppingListCommand => new Command(
                async () =>
                {
                    if (ChangeNameIsEnabled)
                    {
                        ChangeNameIsEnabled = false;
                        LabelNameIsEnabled = true;
                        await Task.Delay(0);
                        currentShoppingList.Naam = Naam;
                        await appModelService.SaveShoppingList(currentShoppingList);
                        await RefreshShoppingList();
                    }
                    else
                    {
                        ChangeNameIsEnabled = true;
                        LabelNameIsEnabled = false;
                    }
                }
            );
    }
}
