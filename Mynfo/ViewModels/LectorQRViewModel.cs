namespace Mynfo.ViewModels
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using ZXing;

    public class LectorQRViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isScanning;
        private Result qrCode;
        private Box Box;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }
        public bool IsScanning
        {
            get
            {
                return this.isScanning;
            }
            set
            {
                SetValue(ref this.isScanning, value);
            }
        }
        public Result QrCode
        {
            get
            {
                return this.qrCode;
            }
            set
            {
                SetValue(ref this.qrCode, value);
            }
        }
        #endregion

        #region Constructor
        public LectorQRViewModel()
        {
            apiService = new ApiService();
            IsScanning = true;
        }
        #endregion

        #region Methods
        public void OnScanResult(Result result)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                IsScanning = false;
                string a = result.Text;
                string[] b = a.Split('=', '&');
                await MainViewModel.GetInstance().LectorQR.GetBoxDefault(Convert.ToInt32(b[1]));
                result = null;
            });
            IsScanning = true;
        }

        public async Task<Box> GetBoxDefault(int id)
        {
            if(id == null || id ==0)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   "El usuario no es valido",
                   Languages.Accept);
                return null;
            }
            Box = new Box();
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var UserFornaneo = await this.apiService.GetUserId(
                apiSecurity,
                "/api",
                "/Users/",
                id);
            if(UserFornaneo == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "El usuario no es valido",
                    Languages.Accept);
                return null;
            }
            var BoxL = await this.apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                id);
            if (BoxL == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "El usuario no tiene boxes",
                    Languages.Accept);
                return null;
            }

            int UserIdToSend = UserFornaneo.UserId;

            //Perfil predeterminado, que es el perfil de Mynfo y box predeterminada de Ese perfil
            if (UserFornaneo.Share != true) 
            { 
                UserIdToSend = 1;
                BoxL = await apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                UserIdToSend);

                if (BoxL == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        "El usuario no tiene boxes",
                        Languages.Accept);
                    return null;
                }
            }

            Imprime_box.InsertForeignData(UserIdToSend, BoxL.BoxId);
            return Box;
        }

        //private void GoToTestPage(int _id, int box, ForeingBox foreingBox2)
        //{
        //    //Insertar box foranea
        //    System.Text.StringBuilder sb;
        //    ForeingBox foreingBox;
        //    foreingBox = foreingBox2;
        //    ForeingProfile foreingProfile;
        //    int BoxId = box;//44
        //    int UserId = _id;//3

        //    //Insertar la box foranea
        //    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
        //    {
        //        connSQLite.Insert(foreingBox);
        //    }

        //    string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
        //    string queryGetPhones = "select dbo.Boxes.BoxId, dbo.ProfilePhones.ProfilePhoneId, dbo.ProfilePhones.Name, " +
        //                     "dbo.ProfilePhones.Number from dbo.Box_ProfilePhone Join dbo.Boxes " +
        //                     "on(dbo.Boxes.BoxId = dbo.Box_ProfilePhone.BoxId) " +
        //                     "Join dbo.ProfilePhones on(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
        //                     "where dbo.Boxes.BoxId = " + BoxId;
        //    string queryGetEmails = "select dbo.Boxes.BoxId, dbo.ProfileEmails.ProfileEmailId, dbo.ProfileEmails.Name, " +
        //                      "dbo.ProfileEmails.Email from dbo.Box_ProfileEmail " +
        //                      "Join dbo.Boxes on(dbo.Boxes.BoxId = dbo.Box_ProfileEmail.BoxId) " +
        //                      "Join dbo.ProfileEmails on(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
        //                      "where dbo.Boxes.BoxId = " + BoxId;
        //    string queryGetSMProfiles = "select * from dbo.Box_ProfileSM " +
        //                            "join dbo.ProfileSMs on(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
        //                            "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
        //                            "where dbo.Box_ProfileSM.BoxId = " + BoxId;
        //    string queryGetWhatsapp = "select dbo.Boxes.BoxId, dbo.ProfileWhatsapps.ProfileWhatsappId, dbo.ProfileWhatsapps.Name, " +
        //                                "dbo.ProfileWhatsapps.Number from dbo.Box_ProfileWhatsapp Join dbo.Boxes " +
        //                                "on(dbo.Boxes.BoxId = dbo.Box_ProfileWhatsapp.BoxId) " +
        //                                "Join dbo.ProfileWhatsapps on(dbo.ProfileWhatsapps.ProfileWhatsappId = dbo.Box_ProfileWhatsapp.ProfileWhatsappId) " +
        //                                "where dbo.Boxes.BoxId =" + BoxId;

        //    //Recorrer la lista de perfiles para insertarlos
        //    //Emails
        //    using (SqlConnection connection = new SqlConnection(cadenaConexion))
        //    {
        //        sb = new System.Text.StringBuilder();
        //        sb.Append(queryGetEmails);

        //        string sql = sb.ToString();

        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    foreingProfile = new ForeingProfile
        //                    {
        //                        BoxId = BoxId,
        //                        UserId = UserId,
        //                        ProfileName = (string)reader["Name"],
        //                        value = (string)reader["Email"],
        //                        ProfileType = "Email"
        //                    };

        //                    //Insertar la box foranea
        //                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
        //                    {
        //                        connSQLite.Insert(foreingProfile);
        //                    }
        //                }
        //            }

        //            connection.Close();
        //        }
        //    }
        //    //PHones
        //    using (SqlConnection connection = new SqlConnection(cadenaConexion))
        //    {
        //        sb = new System.Text.StringBuilder();
        //        sb.Append(queryGetPhones);

        //        string sql = sb.ToString();

        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    foreingProfile = new ForeingProfile
        //                    {
        //                        BoxId = BoxId,
        //                        UserId = UserId,
        //                        ProfileName = (string)reader["Name"],
        //                        value = (string)reader["Number"],
        //                        ProfileType = "Phone"
        //                    };

        //                    //Insertar la box foranea
        //                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
        //                    {
        //                        connSQLite.Insert(foreingProfile);
        //                    }
        //                }
        //            }

        //            connection.Close();
        //        }
        //    }
        //    //Whatsapp
        //    using (SqlConnection connection = new SqlConnection(cadenaConexion))
        //    {
        //        sb = new System.Text.StringBuilder();
        //        sb.Append(queryGetWhatsapp);

        //        string sql = sb.ToString();

        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    foreingProfile = new ForeingProfile
        //                    {
        //                        BoxId = BoxId,
        //                        UserId = UserId,
        //                        ProfileName = (string)reader["Name"],
        //                        value = (string)reader["Number"],
        //                        ProfileType = "Whatsapp"
        //                    };

        //                    //Insertar la box foranea
        //                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
        //                    {
        //                        connSQLite.Insert(foreingProfile);
        //                    }
        //                }
        //            }

        //            connection.Close();
        //        }
        //    }
        //    //Social media
        //    using (SqlConnection connection = new SqlConnection(cadenaConexion))
        //    {
        //        sb = new System.Text.StringBuilder();
        //        sb.Append(queryGetSMProfiles);

        //        string sql = sb.ToString();

        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int IdRedSocial = (int)reader["RedSocialId"];
        //                    var Name = string.Empty;
        //                    switch (IdRedSocial)
        //                    {
        //                        case 1:
        //                            Name = "Facebook";
        //                            break;
        //                        case 2:
        //                            Name = "Instagram";
        //                            break;
        //                        case 3:
        //                            Name = "Twitter";
        //                            break;
        //                        case 4:
        //                            Name = "Snapchat";
        //                            break;
        //                        case 5:
        //                            Name = "LinkedIn";
        //                            break;
        //                        case 6:
        //                            Name = "TikTok";
        //                            break;
        //                        case 7:
        //                            Name = "Youtube";
        //                            break;
        //                        case 8:
        //                            Name = "Spotify";
        //                            break;
        //                        case 9:
        //                            Name = "Twitch";
        //                            break;
        //                        case 10:
        //                            Name = "WebPage";
        //                            break;
        //                        default:
        //                            break;
        //                    };
        //                    foreingProfile = new ForeingProfile
        //                    {
        //                        BoxId = BoxId,
        //                        UserId = UserId,
        //                        ProfileName = (string)reader["ProfileName"],
        //                        value = (string)reader["link"],
        //                        ProfileType = Name
        //                    };

        //                    //Insertar la box foranea
        //                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
        //                    {
        //                        connSQLite.Insert(foreingProfile);
        //                    }
        //                }
        //            }

        //            connection.Close();
        //        }
        //    }

        //    MainViewModel.GetInstance().ListForeignBox.AddList(foreingBox);
        //    MainViewModel.GetInstance().ListForeignBox.GetList();

        //    //Enviar a detalles de la box foranea cuando se inserta
        //    MainViewModel.GetInstance().ForeingBox = new ForeingBoxViewModel(foreingBox, true);
        //    PopupNavigation.Instance.PushAsync(new ForeingBoxPage(foreingBox, true));
        //}
        #endregion
    }
}
