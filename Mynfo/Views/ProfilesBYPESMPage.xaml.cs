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
    public partial class ProfilesBYPESMPage : ContentPage
    {
        public ProfilesBYPESMPage()
        {
            InitializeComponent();
        }
        private async void GoToProfiles_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles = new ProfilesViewModel();
            await Navigation.PushAsync(new ProfilesPage());
        }
    }
}