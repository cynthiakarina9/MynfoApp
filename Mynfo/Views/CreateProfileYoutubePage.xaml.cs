namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfileYoutubePage : ContentPage
    {
        #region Constructor
        public CreateProfileYoutubePage()
        {
            InitializeComponent();

            #region Logo Superior
            OSAppTheme currentTheme = App.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Logosuperior.Source = "logo_superior2.png";
            }
            else
            {
                Logosuperior.Source = "logo_superior3.png";
            }
            #endregion

            //Save.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, "Youtube"));
        }
        #endregion

    }
}