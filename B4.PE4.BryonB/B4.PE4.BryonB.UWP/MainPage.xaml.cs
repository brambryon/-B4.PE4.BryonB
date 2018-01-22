using ZXing.Mobile;

namespace B4.PE4.BryonB.UWP
{
    public sealed partial class MainPage
    {
        

        public MainPage()
        {            
            this.InitializeComponent();

            ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingScannerViewRenderer.Init();

            
            //Xamarin.FormsMaps.Init("efN6kAd3GttFDN0J2OlN~YVatKCGZb6S2LPP863x8AQ~Av4v6TG5HBEsndLvzopl6CeMHc5FP2MQRfMWIJ-4Lk1IxHcLC7SmKTha7Q3d9CGX");
            LoadApplication(new B4.PE4.BryonB.App());
        }
    }
}
