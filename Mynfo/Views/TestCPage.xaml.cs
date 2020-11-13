namespace Mynfo.Views
{

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using ViewModels;
    using System;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestCPage : ContentPage
    {
        public TestCPage()
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