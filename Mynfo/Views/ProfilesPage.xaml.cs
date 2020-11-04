namespace Mynfo.Views
{
    using Badge.Plugin;
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Syncfusion.XForms.BadgeView;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesPage : ContentPage
    {
        public ProfilesPage()
        {
            InitializeComponent();
        }

        private async void PhoneProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByPhone = new ProfilesByPhoneViewModel();
            await Navigation.PushAsync(new ProfilesByPhonePage());
        }
        private async void EmailProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByEmail = new ProfilesByEmailViewModel();
            await Navigation.PushAsync(new ProfilesByEmailPage());
        }
        private async void FacebookProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileFacebook = new CreateProfileFacebookViewModel();
            await Navigation.PushAsync(new CreateProfileFacebookPage());
        }
    }
}