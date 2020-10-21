namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsBoxPage : ContentPage
    {
        public DetailsBoxPage()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home = new HomeViewModel();
            //await Navigation.PopToRootAsync();
            Application.Current.MainPage = new MasterPage();
        }

        private async void BoxDetails_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesBYPESM = new ProfilesBYPESMViewModel();
            await Navigation.PushAsync(new ProfilesBYPESMPage());
        }
    }
}