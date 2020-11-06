namespace Mynfo.Views
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForeingBoxPage : ContentPage
    {
        #region Constructor
        public ForeingBoxPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var bxBtnBack = new ImageButton();
            //Creación del botón para volver a home
            bxBtnBack.BackgroundColor = Color.Transparent;
            bxBtnBack.Source = "back.png";
            bxBtnBack.WidthRequest = 50;
            bxBtnBack.HeightRequest = 50;
            bxBtnBack.Clicked += Back_Clicked;
            BackButton.Children.Add(bxBtnBack);
        }
        #endregion

        #region Command
        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        #endregion
    }
}