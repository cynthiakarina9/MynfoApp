namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using System;
    using System.Text;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfNetworksPage : ContentPage
    {
        #region Attributes
        public bool Actived;
        public bool isCheck;
        #endregion

        #region Services
        ApiService apiService;
        #endregion

        #region Properties
        public Box Box { get; set; }
        public ProfileLocal selectedItemProfile { get; set; }
        #endregion

        #region Constructor
        public ListOfNetworksPage(int _BoxId)
        {
            apiService = new ApiService();
            Box = new Box();
            Box.BoxId = _BoxId;
            InitializeComponent();
        }
        #endregion

        #region Methods

        #region Commands
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedItemProfile = e.SelectedItem as ProfileLocal;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileLocal tappedItemProfile = e.Item as ProfileLocal;

            switch(tappedItemProfile.Logo)
            {
                case "mail2":
                    ProfileEmail E = Converter.ToProfileEmail(tappedItemProfile);
                    if (E.Exist == false)
                    {
                        PostProfileEmail(Box.BoxId, E.ProfileEmailId);
                        E.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.addProfileEmail(E);
                        MainViewModel.GetInstance().DetailsBox.addProfileEmail(E);
                    }
                    else
                    {
                        DeleteProfileEmail(Box.BoxId, E.ProfileEmailId);
                        E.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileEmail(E);
                        MainViewModel.GetInstance().DetailsBox.removeProfileEmail(E);
                    }
                    break;
                case "phone2":
                    ProfilePhone P = Converter.ToProfilePhone(tappedItemProfile);
                    if (P.Exist == false)
                        {
                            PostProfilePhone(Box.BoxId, P.ProfilePhoneId);
                            P.Exist = true;
                            MainViewModel.GetInstance().ListOfNetworks.addProfilePhone(P);
                            MainViewModel.GetInstance().DetailsBox.addProfilePhone(P);
                        }
                        else
                        {
                            DeleteProfilePhone(Box.BoxId, P.ProfilePhoneId);
                            P.Exist = false;
                            MainViewModel.GetInstance().ListOfNetworks.removeProfilePhone(P);
                            MainViewModel.GetInstance().DetailsBox.removeProfilePhone(P);
                        }
                    break;
                case "facebook2":
                    ProfileSM SM = Converter.ToProfileSM(tappedItemProfile);
                    if (SM.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SM.ProfileMSId);
                        SM.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SM);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SM);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SM.ProfileMSId);
                        SM.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SM);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SM);
                    }
                break;
                case "whatsapp2":
                    ProfileWhatsapp W  = Converter.ToProfileWhatsapp(tappedItemProfile);
                    if (W.Exist == false)
                    {
                        PostProfileWhatsapp(Box.BoxId, W.ProfileWhatsappId);
                        W.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.addProfileW(W);
                        MainViewModel.GetInstance().DetailsBox.addProfileW(W);
                    }
                    else
                    {
                        DeleteProfileWhatsapp(Box.BoxId, W.ProfileWhatsappId);
                        W.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.removeProfileW(W);
                        MainViewModel.GetInstance().DetailsBox.removeProfileW(W);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Email
        public async void DeleteProfileEmail(int _box, int _profileEmailId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileEmail box_ProfileEmail = new Box_ProfileEmail
            {
                BoxId = _box,
                ProfileEmailId = _profileEmailId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_Email = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfileEmail/GetBox_ProfileEmail",
                box_ProfileEmail);

            var profileEmail = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfileEmail",
                idBox_Email.Box_ProfileEmailId);
        }

        public async void PostProfileEmail(int _box, int _profileEmailId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileEmail box_ProfileEmail = new Box_ProfileEmail
            {
                BoxId = _box,
                ProfileEmailId = _profileEmailId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileEmail = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfileEmail",
                box_ProfileEmail);
        }
        #endregion

        #region Phone
        public async void PostProfilePhone(int _box, int _profilePhoneId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfilePhone box_ProfilePhone = new Box_ProfilePhone
            {
                BoxId = _box,
                ProfilePhoneId = _profilePhoneId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profilePhone = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfilePhone",
                box_ProfilePhone);
        }
        public async void DeleteProfilePhone(int _box, int _profilePhoneId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfilePhone box_ProfilePhone = new Box_ProfilePhone
            {
                BoxId = _box,
                ProfilePhoneId = _profilePhoneId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_Phone = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfilePhone/GetBox_ProfilePhone",
                box_ProfilePhone);

            var profilePhone = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfilePhone",
                idBox_Phone.Box_ProfilePhoneId);
        }
        #endregion

        #region SM
        public async void PostProfileSM(int _box, int _profileSMId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileSM box_ProfileSM = new Box_ProfileSM
            {
                BoxId = _box,
                ProfileMSId = _profileSMId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileSM = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfileSM",
                box_ProfileSM);

        }
        public async void DeleteProfileSM(int _box, int _profileSMId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileSM box_ProfileSM = new Box_ProfileSM
            {
                BoxId = _box,
                ProfileMSId = _profileSMId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_SM = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfileSM/GetBox_ProfileSM",
                box_ProfileSM);

            var profileSM = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfileSM",
                idBox_SM.Box_ProfileSMId);
        }
        #endregion

        #region Whatsapp
        public async void PostProfileWhatsapp(int _box, int _profileWhatsappId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileWhatsapp box_ProfileWhatsapp = new Box_ProfileWhatsapp
            {
                BoxId = _box,
                ProfileWhatsappId = _profileWhatsappId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileSM = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp",
                box_ProfileWhatsapp);
        }
        public async void DeleteProfileWhatsapp(int _box, int _profileWhatsappId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileWhatsapp box_ProfileWhatsapp = new Box_ProfileWhatsapp
            {
                BoxId = _box,
                ProfileWhatsappId = _profileWhatsappId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_Whatsapp = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp/GetBox_ProfileWhatsapp",
                box_ProfileWhatsapp);

            var profileWhatsapp = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp",
                idBox_Whatsapp.Box_ProfileWhatsappId);
        }
        #endregion

        #endregion
    }
}