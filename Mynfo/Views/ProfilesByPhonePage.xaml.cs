namespace Mynfo.Views
{
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByPhonePage : ContentPage
    {
        public ProfilesByPhonePage()
        {
            InitializeComponent();
        }
        private async void NewProfilePhone_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
            await Navigation.PushAsync(new CreateProfilePhonePage());
        }
    }
}