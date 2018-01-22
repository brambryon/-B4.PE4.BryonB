using B4.PE4.BryonB.Domain.Models;
using B4.PE4.BryonB.Domain.Services.Abstract;
using FreshMvvm;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;

namespace B4.PE4.BryonB.ViewModels
{
    class ProductViewModel : FreshBasePageModel
    {
        public Product currentProduct;
        IAppModelService appModelService;
        MobileBarcodeScanner scanner;

        public ProductViewModel(IAppModelService appModelService)
        {
            this.appModelService = appModelService;
        }

        private string nieuweNaam;
        public string NieuweNaam
        {
            get { return nieuweNaam; }
            set
            {
                nieuweNaam = value;
            }
        }
        public override void Init(object initData)
        {
            if (initData == null)
            {
                base.Init(initData);
                currentProduct = new Product();
                currentProduct.Naam = "New product";
                currentProduct.ProductId = Guid.NewGuid();               
            }
            else
            {
                base.Init(initData);
                currentProduct = (Product)initData;                
            }
            NieuweNaam = currentProduct.Naam;
        }
        public void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                msg = "Found Barcode: " + result.Text;
                currentProduct.Result = result.ToString();
            }
            else
            {
                msg = "Scanning Canceled!";
            }
        }
        public ICommand NaamOKCommand => new Command(
            async () =>
            {
                currentProduct.Naam = NieuweNaam;
                await appModelService.SaveProduct(currentProduct);
                await CoreMethods.PopPageModel();
            }
        );
        public ICommand NaamCancelCommand => new Command(
            async () =>
            {
                await CoreMethods.PopPageModel();
            }
        );
        public ICommand ScanBarCodeCommand => new Command(
            () =>
           {
               //#if __ANDROID__
               //	// Initialize the scanner first so it can track the current context
               //MobileBarcodeScanner.Initialize(Application);            
               //#endif
               scanner = new MobileBarcodeScanner();

               MobileBarcodeScanningOptions ScanOpties = new MobileBarcodeScanningOptions
               {
                   AutoRotate = true,
                   TryHarder = true
               };
               //Tell our scanner to use the default overlay
               scanner.UseCustomOverlay = false;
               //We can customize the top and bottom text of our default overlay
               scanner.TopText = "Hold camera up to barcode";
               scanner.BottomText = "mainpage automatically scan barcode\r\n\r\nPress the 'Back' button to Cancel";
               //Start scanning
               //scanner.Scan().ContinueWith(t =>
               scanner.Scan(ScanOpties).ContinueWith(t =>
               {
                   if (t.Result != null)
                   {
                       HandleScanResult(t.Result);
                   }
               });
           }
       );
    }
}
