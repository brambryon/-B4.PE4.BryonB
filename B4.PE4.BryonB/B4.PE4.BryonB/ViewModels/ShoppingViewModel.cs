using B4.PE4.BryonB.Domain.Models;
using B4.PE4.BryonB.Domain.Services.Abstract;
using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;

namespace B4.PE4.BryonB.ViewModels
{
    class ShoppingViewModel : FreshBasePageModel
    {
        IAppModelService appModelService;
        MobileBarcodeScanner scanner;
        public ShoppingList currentShoppingList { get; set; }
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
        private ShoppingList selectedShoppingList;
        public ShoppingList SelectedShoppingList
        {
            get { return selectedShoppingList; }
            set
            {
                selectedShoppingList = value;
                RaisePropertyChanged(nameof(SelectedShoppingList));
            }
        }
        public ShoppingViewModel(IAppModelService appModelService)
        {
            this.appModelService = appModelService;
        }
        public async override void Init(object initData)
        {
            base.Init(initData);

            ShoppingLists = new ObservableCollection<ShoppingList>();
            ShoppingLists = await appModelService.GetAllShoppingLists();
            ShoppingDetails = new ObservableCollection<ShoppingDetail>();

            await RefreshList();
        }
        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshList();
        }
        public async override void ReverseInit(object returnedData)
        {
            await RefreshList();
        }
        private async Task RefreshList()
        {
            //////ShoppingLists = new ObservableCollection<ShoppingList>();
            //////ShoppingLists = await appModelService.GetAllShoppingLists();
            ////////ShoppingDetails = null;
            //////ShoppingDetails = new ObservableCollection<ShoppingDetail>();           

            if (currentShoppingList != null)
            {

                await Task.Delay(1000);
                //ShoppingDetails = new ObservableCollection<ShoppingDetail>(currentShoppingList.ShoppingDetails);
                var x = await appModelService.GetShoppingListByGuid(currentShoppingList.ShoppingListId);
                currentShoppingList = x;
                //shoppingDetails = currentShoppingList.ShoppingDetails;
                //ShoppingDetails = await appModelService.GetAllShoppingDetailsByShoppigListGuid(currentShoppingList.ShoppingListId);
                ////ShoppingDetails = currentShoppingList.ShoppingDetails;
                //////  SelectedShoppingList = currentShoppingList;
                LoadShoppingListState();
            }
        }

        private void LoadShoppingListState()
        {
            ShoppingDetails = new ObservableCollection<ShoppingDetail>(currentShoppingList.ShoppingDetails);
        }

        public async void HandleScanResult(ZXing.Result result)
        {
            string msg = "";
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                msg = "Found Barcode: " + result.Text;
                //Product test = await appModelService.GetScannedProductFromShoppingList(currentShoppingList, result.Text);
                await appModelService.SetScannedProductFromShoppingList(currentShoppingList, result.Text);
                // heb ik een product met de gescande barcode in mijn shoppinglist?
                await RefreshList();
            }
            else
            {
                msg = "Scanning Canceled!";
            }
        }
        public ICommand TerugCommand => new Command<Product>(
          async (Product product) =>
          {
              await CoreMethods.PopPageModel();
          }
      );

        //  public ICommand ScanCommand => new Command(
        //      async () =>
        //      {
        //          await appModelService.SetScannedProductFromShoppingList(currentShoppingList, "614141000036");
        //          await RefreshList();
        //      }
        //);

        public ICommand ScanCommand => new Command(
            () =>
          {
              scanner = new MobileBarcodeScanner();
              MobileBarcodeScanningOptions ScanOpties = new MobileBarcodeScanningOptions
              {
                  AutoRotate = true,
                  TryHarder = true
              };
              scanner.UseCustomOverlay = false;
              scanner.TopText = "Hold camera up to barcode";
              scanner.BottomText = "mainpage automatically scan barcode\r\n\r\nPress the 'Back' button to Cancel";
              scanner.Scan(ScanOpties).ContinueWith(t =>
             {
                 if (t.Result != null)
                 {
                     HandleScanResult(t.Result);
                 }
             });
          }
      );
        public ICommand SelectAPickerCommand => new Command(
                 async () =>
                {
                    if (selectedShoppingList != null)
                    {
                        currentShoppingList = selectedShoppingList;
                    }
                    await RefreshList();
                }
            );
    }
}
