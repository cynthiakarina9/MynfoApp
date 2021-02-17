namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByEmailPage : ContentPage
    {
        #region Constructors
        public ProfilesByEmailPage()
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
        }
        #endregion

        #region Commands
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileEmail tappedItem = e.Item as ProfileEmail;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditProfileEmail = new EditProfileEmailViewModel(tappedItem.ProfileEmailId);
            App.Navigator.PushAsync(new EditProfileEmailPage());
        }
        #endregion
    }
}