using B4.PE4.BryonB.Domain.Models;
using B4.PE4.BryonB.Domain.Services.Abstract;
using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE4.BryonB.ViewModels
{
    public class ProductListViewModel : FreshBasePageModel
    {
        IAppModelService appModelService;

        public ProductListViewModel(IAppModelService appModelService)
        {
            this.appModelService = appModelService;
        }
        private ObservableCollection<Product> producten;
        public ObservableCollection<Product> Producten
        {
            get { return producten; }
            set
            {
                producten = value;
                RaisePropertyChanged(nameof(Producten));
            }
        }
        public async override void Init(object initData)
        {
            base.Init(initData);
            //  currentLocatieLijst = initData as LocatieLijst;
            await RefreshProductLijst();
        }
        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshProductLijst();
        }
        public async override void ReverseInit(object returnedData)
        {
            await RefreshProductLijst();
        }
        private async Task RefreshProductLijst()
        {
            Producten = new ObservableCollection<Product>();
            Producten = await appModelService.GetAllProducts();
            //currentLocatieLijst = x;
            LoadLocatieLijstState();
        }
        private void LoadLocatieLijstState()
        {
            //Producten = new ObservableCollection<Product>();
            //producten = await appModelService.GetAllProducts();
        }
        public ICommand SelectProductCommand => new Command<Product>(
            async (Product product) =>
            {
                await CoreMethods.PopPageModel(product);                
            }
        );
        public ICommand DeleteProductCommand => new Command<Product>(
           async (Product product) =>
           {
               //await CoreMethods.PopPageModel(product);
               await appModelService.DeleteProduct(product);
               await RefreshProductLijst();
           }
       );
        public ICommand EditCommand => new Command<Product>(
        async (Product product) =>
        {
            await CoreMethods.PushPageModel<ProductViewModel>(product);           
        }
    );
        public ICommand AddNewProductCommand => new Command(
            async () =>
            {
                await CoreMethods.PushPageModel<ProductViewModel>();               
            }
        );
        public ICommand TerugCommand => new Command<Product>(
          async (Product product) =>
          {
              await CoreMethods.PopPageModel();
          }
      );

    }
}
