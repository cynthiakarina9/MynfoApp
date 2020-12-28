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
    public partial class ProfilesByTwitterPage : ContentPage
    {
        #region Constructor
        public ProfilesByTwitterPage()
        {
            InitializeComponent();
        }
        #endregion
        #region Methods
        private void NewProfileTwitter_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileFacebook = new CreateProfileFacebookViewModel();
            App.Navigator.PushAsync(new CreateProfileFacebookPage());
        }
        #endregion
    }
}