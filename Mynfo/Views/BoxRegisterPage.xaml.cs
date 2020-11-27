namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    public partial class BoxRegisterPage : ContentPage
    {
        public BoxRegisterPage()
        {
            InitializeComponent();
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
    }
}