namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByInstagramPage : ContentPage
    {
        #region Constructor
        public ProfilesByInstagramPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Commands
        private void NewProfileInstagram_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileInstagram = new CreateProfileInstagramViewModel();
            App.Navigator.PushAsync(new CreateProfileInstagramPage());
        }

        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ProfileSM selectedItem = e.SelectedItem as ProfileSM;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            ProfileSM tappedItem = e.Item as ProfileSM;
            if (tappedItem == null)
                return;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditProfileInstagram = new EditProfileInstagramViewModel(tappedItem.ProfileMSId);
            ListaIG.SelectedItem = null;
            App.Navigator.PushAsync(new EditProfileInstagramPage());
        }
        #endregion
    }
}