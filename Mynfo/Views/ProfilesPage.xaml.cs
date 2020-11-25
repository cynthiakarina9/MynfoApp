namespace Mynfo.Views
{
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesPage : ContentPage
    {
        public ProfilesPage()
        {
            InitializeComponent();
        }

        private void PhoneProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByPhone = new ProfilesByPhoneViewModel();
            Navigation.PushAsync(new ProfilesByPhonePage());
        }
        private void EmailProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().ProfilesByEmail = new ProfilesByEmailViewModel();
            Navigation.PushAsync(new ProfilesByEmailPage());
        }
        private void FacebookProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByFacebook = new ProfilesByFacebookViewModel();
            Navigation.PushAsync(new ProfilesByFacebookPage());
        }
        private void WhatsAppProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByWhatsApp = new ProfilesByWhatsAppViewModel();
            Navigation.PushAsync(new ProfilesByWhatsAppPage());
        }
    }
}