//using Android.Content;
//using Android.Content.PM;
//using Android.Support.V4.Content;
//using Android.Telephony;
//using Rg.Plugins.Popup.Services;
//using System;

//using Xamarin.Forms;
//using Xamarin.Forms.PlatformConfiguration;
//using Xamarin.Forms.Xaml;

//namespace Mynfo.Views
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class Testing : ContentPage
//    {
//        public Testing()
//        {
//            InitializeComponent();

//            AbrirPopUp.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender,e));

//            //BackButton.Clicked += new EventHandler((sender, e) => GoToHome());

//            //TelephonyManager mTelephonyMgr;

//            /*var permission = ContextCompat.CheckSelfPermission(Android.App.Application.Context, "READ_PHONE_STATE");

//            if(permission == Permission.Denied)
//            {

//            }*/

//            //mTelephonyMgr = Android.App.Application.Context.GetSystemService(Context.TelephonyService) as TelephonyManager;

//            //var Number = mTelephonyMgr.Line1Number;
//        }

//        private void GoToHome()
//        {
//            Application.Current.MainPage = new MasterPage();
//        }

//        private async void OpenPopupTest(object sender, EventArgs e)
//        {
//            await PopupNavigation.Instance.PushAsync(new PopupExample());
//        }

//    }
//}