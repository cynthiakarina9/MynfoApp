namespace Mynfo.Views
{
    using Mynfo.Models;
    using Newtonsoft.Json;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForeingBoxPage : ContentPage
    {
        #region Properties
        public string data = Data_ntc.data_value;
        #endregion
        #region Constructor
        public ForeingBoxPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            #region Button
            //Creación del botón para volver a home
            var bxBtnBack = new ImageButton();
            bxBtnBack.BackgroundColor = Color.Transparent;
            bxBtnBack.Source = "back.png";
            bxBtnBack.WidthRequest = 50;
            bxBtnBack.HeightRequest = 50;
            bxBtnBack.Clicked += Back_Clicked;
            BackButton.Children.Add(bxBtnBack);
            #endregion

            #region GetBox

            //data = {
            //        "BoxId"":""87"","
            //        "Name"":""home"","
            //        "BoxDefault"":""True"","
            //        "UserId"":""17"","
            //        "Time"":""11/6/2020 5:48:03 p. m."","
            //        "ImagePath"":""~/Content/Images/1318069d-5731-48ce-8c6f-13b802105352.jpg"","
            //        "UserTypeId"":""1"","
            //        "FirstName"":""Gerardo"","
            //        "LastName"":""Daza"","
            //        "ImageFullPath"":""https://mynfoapi.azurewebsites.net//Content/Images/1318069d-5731-48ce-8c6f-13b802105352.jpg"","
            //        "FullName"":""Gerardo Daza"","
            //        "ProfileLocalId"":""1"","
            //        "IdBox"":""87"","
            //        "UserId_p"":""17"","
            //        "ProfileName"":""ATX Email"","
            //        "value"":""glopez@atx.mx"","
            //        "ProfileType"":""Email"
            //        }
       //}

            #endregion
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