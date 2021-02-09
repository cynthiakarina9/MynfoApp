namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Mynfo.Models;
    using Services;
    using System;
    using System.Data.SqlClient;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class BoxRegisterViewModel : BaseViewModel
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
        #endregion

        #region Constructors
        public BoxRegisterViewModel()
        {
            this.apiService = new ApiService();

            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveBoxCommand
        {
            get
            {
                return new RelayCommand(SaveBox);
            }
        }
        private async void SaveBox()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
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
            DateTime boxTime = DateTime.Now;
            System.Text.StringBuilder sb;
            bool defaultBoxExists = false;
            Box box;
            BoxLocal boxLocal;
            //string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            //string QueryToFindDefaultboxes = "select * from dbo.Boxes where dbo.Boxes.BoxDefault = 1 and dbo.Boxes.UserId = "
            //                                    + mainViewModel.User.UserId;

            //using (SqlConnection connection = new SqlConnection(cadenaConexion))
            //{
            //    sb = new System.Text.StringBuilder();
            //    sb.Append(QueryToFindDefaultboxes);
            //    string sql = sb.ToString();

            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        connection.Open();
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                defaultBoxExists = true;
            //            }
            //        }
            //        connection.Close();
            //    }
            //}
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var BoxList = await this.apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                MainViewModel.GetInstance().User.UserId);
            if(BoxList == default)
            {
                defaultBoxExists = false;
            }
            else
            {
                defaultBoxExists = true;
            }

            if (defaultBoxExists)
            {
                box = new Box
                {
                    Name = this.Name,
                    BoxDefault = false,
                    UserId = mainViewModel.User.UserId,
                    Time = boxTime,
                };
            }
            else
            {
                box = new Box
                {
                    Name = this.Name,
                    BoxDefault = true,
                    UserId = mainViewModel.User.UserId,
                    Time = boxTime,
                };
            }


            var response = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Boxes",
                box);

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

            //if (!defaultBoxExists)
            //{
            //    string QueryToFindLastBoxCreated = "SELECT MAX(dbo.Boxes.BoxId) as BoxId FROM dbo.Boxes WHERE dbo.Boxes.UserId = +"
            //                                    + mainViewModel.User.UserId;
            //    int lastBoxId = 0;

            //    using (SqlConnection connection = new SqlConnection(cadenaConexion))
            //    {
            //        sb = new System.Text.StringBuilder();
            //        sb.Append(QueryToFindLastBoxCreated);
            //        string sql = sb.ToString();

            //        using (SqlCommand command = new SqlCommand(sql, connection))
            //        {
            //            connection.Open();
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    lastBoxId = (int)reader["BoxId"];
            //                }
            //            }
            //            connection.Close();
            //        }
            //    }

            //    boxLocal = new BoxLocal
            //    {
            //        BoxId = lastBoxId,
            //        BoxDefault = true,
            //        Name = this.Name,
            //        UserId = MainViewModel.GetInstance().User.UserId,
            //        Time = boxTime,
            //        FirstName = MainViewModel.GetInstance().User.FirstName,
            //        LastName = MainViewModel.GetInstance().User.LastName,
            //        ImagePath = MainViewModel.GetInstance().User.ImagePath,
            //        UserTypeId = MainViewModel.GetInstance().User.UserTypeId
            //    };


            //    //Crear box local predeterminada
            //    using (var conn = new SQLite.SQLiteConnection(App.root_db))
            //    {
            //        conn.CreateTable<BoxLocal>();
            //        conn.Insert(boxLocal);
            //    }
            //    //Crear tabla de perfiles de box local predeterminada
            //    using (var conn = new SQLite.SQLiteConnection(App.root_db))
            //    {
            //        conn.CreateTable<ProfileLocal>();
            //    }
            //}

            this.Name = string.Empty;
            //mainViewModel.DetailsBox = new DetailsBoxViewModel();
            //await App.Navigator.PushAsync(new DetailsBoxPage());
            mainViewModel.Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }

        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }
        private  void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
