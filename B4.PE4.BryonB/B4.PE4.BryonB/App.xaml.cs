using FreshMvvm;
using Xamarin.Forms;
using B4.PE4.BryonB.Domain.Services.Abstract;
using B4.PE4.BryonB.ViewModels;
using B4.PE4.BryonB.Domain.Services.SqliteAccess;

namespace B4.PE4.BryonB
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //FreshIOC.Container.Register<IAppModelService>(new ShoppingListInMemoryDummy());
            FreshIOC.Container.Register<IAppModelService>(new AppModelSQLiteService());

            //MainPage = new B4.PE4.BryonB.MainPage();
            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
