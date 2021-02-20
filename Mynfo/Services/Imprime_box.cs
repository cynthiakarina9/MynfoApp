using System;
using Mynfo.Domain;
using Mynfo.Models;
using Mynfo.ViewModels;
using Mynfo.Views;
using System.Data.SqlClient;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
using Rg.Plugins.Popup.Services;

namespace Mynfo.Services
{
    public class Imprime_box
    {
        public static void Consulta_user(string user_id, string tag_id)
        {
            try
            {
                //string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";

                string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";

                string queryLastBoxCreated =

                "select dbo.Users.FirstName, dbo.Users.LastName, dbo.Users.UserTypeId, dbo.Users.ImagePath, dbo.Users.Share, dbo.Boxes.BoxId from dbo.Users " +
                "join dbo.Boxes on(dbo.Boxes.UserId = dbo.Users.UserId) " +
                " where dbo.Users.UserId = " + user_id +
                " and dbo.Boxes.BoxDefault = 1";

                System.Text.StringBuilder sb;

                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryLastBoxCreated);

                    string sql = sb.ToString();

                    using (SqlCommand command3 = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command3.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string get_FirstName = (string)reader["FirstName"];
                                string get_LastName = (string)reader["LastName"];
                                string get_ImagePath = reader["ImagePath"].ToString();
                                int get_box_id = (int)reader["BoxId"];
                                int UserTypeId_get = (int)reader["UserTypeId"];
                                bool Share = (bool)reader["Share"];

                                //if (Share != true) { user_id = 0; get_box_id = 0; }

                                InsertForeignData(user_id, get_box_id);
                            }
                        }

                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static async void InsertForeignData(int user_id, int box_id)
        {
            ApiService apiService = new ApiService();

            int user_I = user_id;
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            User box_detail = new User();
            box_detail = await apiService.GetUserId(apiSecurity,
                                                "/api",
                                                "/Users",
                                                user_I);

            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            {
                connSQLite.CreateTable<ForeingProfile>();
            }

            ForeingBox foreingBox;
            ForeingProfile foreingProfile;
            ForeingBox A = new ForeingBox();
            //Validar que la box no exista
            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            {
                A = connSQLite.FindWithQuery<ForeingBox>("select * from ForeingBox where ForeingBox.BoxId = ?", box_id);
            }

            if (A == null)
            {
                //Inicializar la box foranea
                foreingBox = new ForeingBox
                {
                    BoxId = box_id,
                    UserId = user_I,
                    //Time = Convert.ToDateTime(nfcData[0].time).ToUniversalTime(),
                    Time = DateTime.Now,
                    ImagePath = box_detail.ImagePath,
                    UserTypeId = box_detail.UserTypeId,
                    FirstName = box_detail.FirstName,
                    LastName = box_detail.LastName

                };

                //Insertar la box foranea
                using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                {
                    connSQLite.Insert(foreingBox);
                }
            }
            else
            {
                foreingBox = A;
            }
            try
            {
                if (box_id != 0)
                {
                    //using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                    //{
                    //    connSQLite.CreateTable<Profile_get>();
                    //}
                    if (A != null)
                    {
                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                        {
                            connSQLite.Execute("Delete from ForeingProfile Where ForeingProfile.BoxId = ?", A.BoxId);
                        }
                    }
                    #region ForeignProfiles
                    System.Text.StringBuilder sb;
                    //string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";

                    string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";

                    //Creación de perfiles locales de box local
                    string queryGetBoxEmail = "select * from dbo.ProfileEmails " +
                                    "join dbo.Box_ProfileEmail on" +
                                    "(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                                    "where dbo.Box_ProfileEmail.BoxId = " + box_id;
                    string queryGetBoxPhone = "select * from dbo.ProfilePhones " +
                                                "join dbo.Box_ProfilePhone on" +
                                                "(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                                                "where dbo.Box_ProfilePhone.BoxId = " + box_id;
                    string queryGetBoxSMProfiles = "select * from dbo.ProfileSMs " +
                                                    "join dbo.Box_ProfileSM on" +
                                                    "(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                                    "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                                    "where dbo.Box_ProfileSM.BoxId = " + box_id;
                    string queryGetBoxWhatsappProfiles = "select * from dbo.ProfileWhatsapps " +
                                                    "join dbo.Box_ProfileWhatsapp on" +
                                                    "(dbo.ProfileWhatsapps.ProfileWhatsappId = dbo.Box_ProfileWhatsapp.ProfileWhatsappId) " +
                                                    "where dbo.Box_ProfileWhatsapp.BoxId = " + box_id;

                    //Consulta para obtener perfiles email
                    using (SqlConnection conn1 = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetBoxEmail);

                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, conn1))
                        {
                            conn1.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreingProfile = new ForeingProfile
                                    {
                                        BoxId = box_id,
                                        UserId = (int)reader["UserId"],
                                        ProfileName = (string)reader["Name"],
                                        value = (string)reader["Email"],
                                        ProfileType = "Email"
                                    };
                                    //Crear perfil de correo de box local predeterminada
                                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        connSQLite.Insert(foreingProfile);
                                    }
                                }
                            }

                            conn1.Close();
                        }
                    }

                    //Consulta para obtener perfiles teléfono
                    using (SqlConnection conn1 = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetBoxPhone);

                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, conn1))
                        {
                            conn1.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreingProfile = new ForeingProfile
                                    {
                                        BoxId = box_id,
                                        UserId = (int)reader["UserId"],
                                        ProfileName = (string)reader["Name"],
                                        value = (string)reader["Number"],
                                        ProfileType = "Phone"
                                    };
                                    //Crear perfil de teléfono de box local predeterminada
                                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        connSQLite.Insert(foreingProfile);
                                    }
                                }
                            }

                            conn1.Close();
                        }
                    }

                    //Consulta para obtener perfiles de redes sociales
                    using (SqlConnection conn1 = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetBoxSMProfiles);

                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, conn1))
                        {
                            conn1.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreingProfile = new ForeingProfile
                                    {
                                        BoxId = box_id,
                                        UserId = (int)reader["UserId"],
                                        ProfileName = (string)reader["ProfileName"],
                                        value = (string)reader["link"],
                                        ProfileType = (string)reader["Name"]
                                    };
                                    //Crear perfil de teléfono de box local predeterminada
                                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        connSQLite.Insert(foreingProfile);
                                    }
                                }
                            }
                            conn1.Close();
                        }
                    }

                    //Consulta para obtener perfiles Whatsapp
                    using (SqlConnection conn1 = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetBoxWhatsappProfiles);

                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, conn1))
                        {
                            conn1.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreingProfile = new ForeingProfile
                                    {
                                        BoxId = box_id,
                                        UserId = (int)reader["UserId"],
                                        ProfileName = (string)reader["Name"],
                                        value = (string)reader["Number"],
                                        ProfileType = "Whatsapp"
                                    };
                                    //Crear perfil de Whatsapp de box local predeterminada
                                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        connSQLite.Insert(foreingProfile);
                                    }
                                }
                            }

                            conn1.Close();
                        }
                    }
                    #endregion
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //App.Navigator.PushAsync(new ForeingBoxPage(foreingBox, true));
                        MainViewModel.GetInstance().ForeingBox = new ForeingBoxViewModel(foreingBox);
                        PopupNavigation.Instance.PushAsync(new ForeingBoxPage(foreingBox, true));
                        if (A == null)
                        {
                            MainViewModel.GetInstance().ListForeignBox.AddList(foreingBox);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}