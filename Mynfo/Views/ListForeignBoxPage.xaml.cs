namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListForeignBoxPage : ContentPage
    {
        #region Contructor
        public ListForeignBoxPage()
        {           
            InitializeComponent();          
        }
        #endregion

        #region Commands
        private async void ViewProfile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForeingBoxPage());
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}