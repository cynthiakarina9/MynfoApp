using Mynfo.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Testing : ContentPage
    {
        public Testing()
        {
            InitializeComponent();

            BoxLocal boxLocal;
            List<ProfileLocal> profileLocalList = new List<ProfileLocal>();

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                int a = conn.Table<BoxLocal>().Count();
                if(a > 0)
                {
                    boxLocal = conn.Table<BoxLocal>().First();

                    boxLocalName.Text = boxLocal.Name + " - " + boxLocal.FullName;
                }
                
            }

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                int a = conn.Table<ProfileLocal>().Count();

                profileLocalList = conn.Table<ProfileLocal>().ToList();
            }

            foreach (ProfileLocal profileLocalValue in profileLocalList)
            {
                networksLocalNames.Text = networksLocalNames.Text + "- " + profileLocalValue.ProfileName;
            }

            //AbrirPopUp.Clicked += new EventHandler((sender, e) => OpenPopupTest(sender,e));

            //BackButton.Clicked += new EventHandler((sender, e) => GoToHome());
        }

        private void GoToHome()
        {
            Application.Current.MainPage = new MasterPage();
        }

        private async void OpenPopupTest(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupExample());
        }

    }
}