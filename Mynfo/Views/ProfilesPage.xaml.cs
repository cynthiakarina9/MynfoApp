namespace Mynfo.Views
{
    using ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

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
            mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
            await Navigation.PushAsync(new CreateProfilePhonePage());
        }
        private async void EmailProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileEmail = new CreateProfileEmailViewModel();
            await Navigation.PushAsync(new CreateProfileEmailPage());
        }
    }
}