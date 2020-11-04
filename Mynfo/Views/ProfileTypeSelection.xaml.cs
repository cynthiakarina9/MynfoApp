using Mynfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileTypeSelection : ContentPage
    {
        public ProfileTypeSelection(int _BoxId)
        {
            InitializeComponent();
            BackDetails.Clicked += new EventHandler((sender, e) => Back_Clicked(sender, e, _BoxId));

            //Botones de redes sociales
            ProfilesEmail.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Email"));
            ProfilesPhone.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Phone"));
            ProfilesFacebook.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Facebook"));
        }
        private void Back_Clicked(object sender, EventArgs e, int _BoxId)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.DetailsBox = new DetailsBoxViewModel();
            Application.Current.MainPage = new NavigationPage(new DetailsBoxPage(_BoxId));
        }

        private void ProfilesList_Clicked(object sender, EventArgs e, int _BoxId, string _profileType)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.DetailsBox = new DetailsBoxViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, _profileType));
        }
    }
}