namespace Mynfo.Views
{
    using ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            OSAppTheme currentTheme = App.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Logosuperior.Source = "logo_superior2.png";
            }
            else
            {
                Logosuperior.Source = "logo_superior3.png";
            }
            var user = MainViewModel.GetInstance().User;

            if (user.UserTypeId == 1)
            {
                ExtProfile.IsVisible = false;
            }
            else
            {
                ButtonSave.IsVisible = false;
                ButtonChangePassw.IsVisible = false;
                Email.IsVisible = false;
                FirstNameEntry.IsReadOnly = true;
                LastNameEntry.IsReadOnly = true;
                ExtProfile.IsVisible = true;
                ChangeImage.IsVisible = false;
            }
        }
    }
}