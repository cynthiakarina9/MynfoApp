namespace Mynfo.Views
{
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
        #endregion
    }
}