using GalaSoft.MvvmLight.Command;
using Mynfo.Domain;
using Mynfo.Helpers;
using Mynfo.Services;
using Mynfo.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mynfo.ViewModels
{
    public class CreateProfileWhatsAppViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public string Name
        {
            get;
            set;
        }
        public string Number
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CreateProfileWhatsAppViewModel()
        {
            this.apiService = new ApiService();

            this.IsEnabled = true;
        }
        #endregion

        #region Commands

        public ICommand SaveProfileWhatsAppCommand
        {
            get
            {
                return new RelayCommand(SaveProfileWhatsApp);
            }
        }
        private async void SaveProfileWhatsApp()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Number))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NumberValidation,
                    Languages.Accept);
                return;
            }
            if (this.Number.Length != 10)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneValidation2,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var profileWhatsApp = new ProfileWhatsapp
            {
                Name = this.Name,
                Number = this.Number,
                UserId = mainViewModel.User.UserId,
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                profileWhatsApp);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Name = string.Empty;
            this.Number = string.Empty;

            string consultaDefault = "SELECT Top 1 * FROM dbo.ProfileWhatsapps where dbo.ProfileWhatsapps.UserId = "
                                        + MainViewModel.GetInstance().User.UserId +
                                        " ORDER BY dbo.ProfileWhatsapps.ProfileWhatsappId DESC";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            ProfileWhatsapp _profileWhatsapp = new ProfileWhatsapp();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(consultaDefault);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _profileWhatsapp.ProfileWhatsappId = (int)reader["ProfileWhatsappId"];
                            _profileWhatsapp.Name = (string)reader["Name"];
                            _profileWhatsapp.UserId = (int)reader["UserId"];
                            _profileWhatsapp.Number = (string)reader["Number"];
                        }
                    }
                    connection.Close();
                }
            }

            //Agregar a la lista
            MainViewModel.GetInstance().ProfilesByWhatsApp.addProfile(_profileWhatsapp);

            await App.Navigator.PopAsync();

            /*MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();*/
        }
        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }

        private async void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
