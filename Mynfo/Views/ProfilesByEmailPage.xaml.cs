namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByEmailPage : ContentPage
    {
        public ProfilesByEmailPage()
        {
            InitializeComponent();
        }
        private async void NewProfileEmail_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileEmail = new CreateProfileEmailViewModel();
            await Navigation.PushAsync(new CreateProfileEmailPage());
        }
    }
}