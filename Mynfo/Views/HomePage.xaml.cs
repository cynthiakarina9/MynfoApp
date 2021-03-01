namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using Rg.Plugins.Popup.Extensions;
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public partial class HomePage : ContentPage
    {
        #region Properties
        public Box selectedItem { get; set; }
        public Box selectedItem2 { get; set; }
        public UserLocal User
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public HomePage()
        {
            InitializeComponent();
            this.User = MainViewModel.GetInstance().User;
            this.CheckLocalBox();
            if(User.Ocupacion == null || User.Ocupacion == string.Empty)
            {
                OccupationLabel.Text = Languages.OccupationLabel;
                OccupationLabel.TextColor = Color.FromHex("#A1A1A1");
            }
            if(User.Ubicacion == null || User.Ubicacion == string.Empty)
            {
                UbicacionLabel.Text = Languages.LocationLabel;
                UbicacionLabel.TextColor = Color.FromHex("#A1A1A1");
            }
        }
        #endregion

        #region Methods
        private void CheckLocalBox()
        {
            BoxLocal boxLocal = new BoxLocal();
            bool valBoxLocal = false;
            bool valProfileLocal = false;

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
                //string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
                string queryToGetBoxDefault = "select * from dbo.Boxes where dbo.boxes.UserId = "
                                                + MainViewModel.GetInstance().User.UserId
                                                + " and dbo.Boxes.BoxDefault = 1";
                StringBuilder sb;
                var resultBoxLocal = conn.GetTableInfo("BoxLocal");
                var resulForeingBox = conn.GetTableInfo("ForeingBox");
                var resultForeingProfiles = conn.GetTableInfo("ForeingProfile");

                if (resulForeingBox.Count == 0)
                {
                    conn.CreateTable<ForeingBox>();

                    if (resultForeingProfiles.Count == 0)
                    {
                        conn.CreateTable<ForeingProfile>();
                    }
                }

                //Si no existe la tabla de las boxes locales...
                if (resultBoxLocal.Count == 0)
                {
                    //Validamos si existe la tabla de perfiles locales
                    var resultProfileLocal = conn.GetTableInfo("ProfileLocal");

                    //Crear tabla de box local
                    conn.CreateTable<BoxLocal>();

                    //Si no existe la tabla de perfiles...
                    if (resultProfileLocal.Count == 0)
                    {
                        //Creamos la tabla de perfiles local
                        conn.CreateTable<ProfileLocal>();
                    }
                    else
                    {
                        //Eliminamos los datos de la tabla de perfiles locales
                        conn.DeleteAll<ProfileLocal>();
                    }
                    //Buscar registros, y si existen, replicarlos a la box local
                    using (SqlConnection connection = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryToGetBoxDefault);
                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    boxLocal = new BoxLocal
                                    {
                                        BoxId = (int)reader["BoxId"],
                                        BoxDefault = true,
                                        Name = (string)reader["Name"],
                                        UserId = MainViewModel.GetInstance().User.UserId,
                                        Time = (DateTime)reader["Time"],
                                        FirstName = MainViewModel.GetInstance().User.FirstName,
                                        LastName = MainViewModel.GetInstance().User.LastName,
                                        ImagePath = MainViewModel.GetInstance().User.ImagePath,
                                        UserTypeId = MainViewModel.GetInstance().User.UserTypeId
                                    };

                                    conn.Insert(boxLocal);
                                    valBoxLocal = true;
                                }
                            }
                            connection.Close();
                        }

                    }
                    //Si existe la box en la nube
                    if (boxLocal.BoxId != 0)
                    {
                        //Creación de perfiles locales de box local
                        string queryGetBoxEmail = "select * from dbo.ProfileEmails " +
                                        "join dbo.Box_ProfileEmail on" +
                                        "(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                                        "where dbo.Box_ProfileEmail.BoxId = " + boxLocal.BoxId;
                        string queryGetBoxPhone = "select * from dbo.ProfilePhones " +
                                                    "join dbo.Box_ProfilePhone on" +
                                                    "(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                                                    "where dbo.Box_ProfilePhone.BoxId = " + boxLocal.BoxId;
                        string queryGetBoxSMProfiles = "select * from dbo.ProfileSMs " +
                                                        "join dbo.Box_ProfileSM on" +
                                                        "(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                                        "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                                        "where dbo.Box_ProfileSM.BoxId = " + boxLocal.BoxId;

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
                                        ProfileLocal emailProfile = new ProfileLocal
                                        {
                                            IdBox = boxLocal.BoxId,
                                            UserId = (int)reader["UserId"],
                                            ProfileName = (string)reader["Name"],
                                            value = (string)reader["Email"],
                                            ProfileType = "Email"
                                        };
                                        //Crear perfil de correo de box local predeterminada
                                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                        {
                                            connSQLite.Insert(emailProfile);
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
                                        ProfileLocal phoneProfile = new ProfileLocal
                                        {
                                            IdBox = boxLocal.BoxId,
                                            UserId = (int)reader["UserId"],
                                            ProfileName = (string)reader["Name"],
                                            value = (string)reader["Number"],
                                            ProfileType = "Phone"
                                        };
                                        //Crear perfil de teléfono de box local predeterminada
                                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                        {
                                            connSQLite.Insert(phoneProfile);
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
                                        ProfileLocal smProfile = new ProfileLocal
                                        {
                                            IdBox = boxLocal.BoxId,
                                            UserId = (int)reader["UserId"],
                                            ProfileName = (string)reader["ProfileName"],
                                            value = (string)reader["link"],
                                            ProfileType = (string)reader["Name"]
                                        };
                                        //Crear perfil de teléfono de box local predeterminada
                                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                        {
                                            connSQLite.Insert(smProfile);
                                        }
                                    }
                                }

                                conn1.Close();
                            }
                        }

                        //Validamos que se haya insertado al menos un perfil
                        if (conn.Table<ProfileLocal>().Count() > 0)
                        {
                            valProfileLocal = true;
                        }
                    }

                    if (valBoxLocal == true && valProfileLocal == true)
                    {
                        //this.get_box();
                    }
                }
                else
                {
                    //*********************************************
                    //Si la tabla de box local si existe
                    //La vacíamos para colocar los nuevos valores
                    conn.DeleteAll<BoxLocal>();

                    conn.DeleteAll<ProfileLocal>();

                    //Validamos que esté vacía
                    int a = conn.Table<BoxLocal>().Count();

                    //Buscar registros, y si existen, replicarlos a la box local
                    using (SqlConnection connection = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryToGetBoxDefault);
                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    boxLocal = new BoxLocal
                                    {
                                        BoxId = (int)reader["BoxId"],
                                        BoxDefault = true,
                                        Name = (string)reader["Name"],
                                        UserId = MainViewModel.GetInstance().User.UserId,
                                        Time = (DateTime)reader["Time"],
                                        FirstName = MainViewModel.GetInstance().User.FirstName,
                                        LastName = MainViewModel.GetInstance().User.LastName,
                                        ImagePath = MainViewModel.GetInstance().User.ImagePath,
                                        UserTypeId = MainViewModel.GetInstance().User.UserTypeId
                                    };

                                    conn.Insert(boxLocal);
                                    valBoxLocal = true;
                                }
                            }
                            connection.Close();
                        }

                    }

                    a = conn.Table<BoxLocal>().Count();

                    //Validamos que exista una box
                    if (boxLocal.BoxId != 0)
                    {
                        //Creación de perfiles locales de box local
                        string queryGetBoxEmail = "select * from dbo.ProfileEmails " +
                                        "join dbo.Box_ProfileEmail on" +
                                        "(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                                        "where dbo.Box_ProfileEmail.BoxId = " + boxLocal.BoxId;
                        string queryGetBoxPhone = "select * from dbo.ProfilePhones " +
                                                    "join dbo.Box_ProfilePhone on" +
                                                    "(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                                                    "where dbo.Box_ProfilePhone.BoxId = " + boxLocal.BoxId;
                        string queryGetBoxSMProfiles = "select * from dbo.ProfileSMs " +
                                                        "join dbo.Box_ProfileSM on" +
                                                        "(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                                        "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                                        "where dbo.Box_ProfileSM.BoxId = " + boxLocal.BoxId;

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
                                        ProfileLocal emailProfile = new ProfileLocal
                                        {
                                            IdBox = boxLocal.BoxId,
                                            UserId = (int)reader["UserId"],
                                            ProfileName = (string)reader["Name"],
                                            value = (string)reader["Email"],
                                            ProfileType = "Email"
                                        };
                                        //Crear perfil de correo de box local predeterminada
                                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                        {
                                            connSQLite.Insert(emailProfile);
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
                                        ProfileLocal phoneProfile = new ProfileLocal
                                        {
                                            IdBox = boxLocal.BoxId,
                                            UserId = (int)reader["UserId"],
                                            ProfileName = (string)reader["Name"],
                                            value = (string)reader["Number"],
                                            ProfileType = "Phone"
                                        };
                                        //Crear perfil de teléfono de box local predeterminada
                                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                        {
                                            connSQLite.Insert(phoneProfile);
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
                                        ProfileLocal smProfile = new ProfileLocal
                                        {
                                            IdBox = boxLocal.BoxId,
                                            UserId = (int)reader["UserId"],
                                            ProfileName = (string)reader["ProfileName"],
                                            value = (string)reader["link"],
                                            ProfileType = (string)reader["Name"]
                                        };
                                        //Crear perfil de teléfono de box local predeterminada
                                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                        {
                                            connSQLite.Insert(smProfile);
                                        }
                                    }
                                }

                                conn1.Close();
                            }
                        }

                        //Validamos que se haya insertado al menos un perfil
                        if (conn.Table<ProfileLocal>().Count() > 0)
                        {
                            valProfileLocal = true;
                        }


                        if (valBoxLocal == true && valProfileLocal == true)
                        {
                            //this.get_box();
                        }
                    }
                }

            }
        }
        private void GoToTestPage()
        {
            /*MainViewModel.GetInstance().Testing = new TestingViewModel();
            Navigation.PushAsync(new Testing());
            //await Launcher.OpenAsync(new Uri("https://www.facebook.com/roy.a.mustang"));*/

            //insertar box foranea
            System.Text.StringBuilder sb;
            ForeingBox foreingBox;
            ForeingProfile foreingProfile;
            int BoxId = 44;//44
            int UserId = 3;//3
            //Inicializar la box foranea
            foreingBox = new ForeingBox
            {
                BoxId = BoxId,
                UserId = UserId,
                Time = DateTime.Now,
                ImagePath = MainViewModel.GetInstance().User.ImageFullPath,
                UserTypeId = 1,
                FirstName = "Rodrigo",
                LastName = "Rodriguez"

            };

            //Insertar la box foranea
            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            {
                connSQLite.Insert(foreingBox);
            }

            string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            string queryGetPhones = "select dbo.Boxes.BoxId, dbo.ProfilePhones.ProfilePhoneId, dbo.ProfilePhones.Name, " +
                             "dbo.ProfilePhones.Number from dbo.Box_ProfilePhone Join dbo.Boxes " +
                             "on(dbo.Boxes.BoxId = dbo.Box_ProfilePhone.BoxId) " +
                             "Join dbo.ProfilePhones on(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                             "where dbo.Boxes.BoxId = " + BoxId;
            string queryGetEmails = "select dbo.Boxes.BoxId, dbo.ProfileEmails.ProfileEmailId, dbo.ProfileEmails.Name, " +
                              "dbo.ProfileEmails.Email from dbo.Box_ProfileEmail " +
                              "Join dbo.Boxes on(dbo.Boxes.BoxId = dbo.Box_ProfileEmail.BoxId) " +
                              "Join dbo.ProfileEmails on(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                              "where dbo.Boxes.BoxId = " + BoxId;
            string queryGetSMProfiles = "select * from dbo.Box_ProfileSM " +
                                    "join dbo.ProfileSMs on(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                    "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                    "where dbo.Box_ProfileSM.BoxId = " + BoxId;
            string queryGetWhatsapp = "select dbo.Boxes.BoxId, dbo.ProfileWhatsapps.ProfileWhatsappId, dbo.ProfileWhatsapps.Name, " +
                                        "dbo.ProfileWhatsapps.Number from dbo.Box_ProfileWhatsapp Join dbo.Boxes " +
                                        "on(dbo.Boxes.BoxId = dbo.Box_ProfileWhatsapp.BoxId) " +
                                        "Join dbo.ProfileWhatsapps on(dbo.ProfileWhatsapps.ProfileWhatsappId = dbo.Box_ProfileWhatsapp.ProfileWhatsappId) " +
                                        "where dbo.Boxes.BoxId =" + BoxId;

            //Recorrer la lista de perfiles para insertarlos
            //Emails
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetEmails);

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreingProfile = new ForeingProfile
                            {
                                BoxId = BoxId,
                                UserId = UserId,
                                ProfileName = (string)reader["Name"],
                                value = (string)reader["Email"],
                                ProfileType = "Email"
                            };

                            //Insertar la box foranea
                            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                            {
                                connSQLite.Insert(foreingProfile);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            //PHones
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetPhones);

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreingProfile = new ForeingProfile
                            {
                                BoxId = BoxId,
                                UserId = UserId,
                                ProfileName = (string)reader["Name"],
                                value = (string)reader["Number"],
                                ProfileType = "Phone"
                            };

                            //Insertar la box foranea
                            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                            {
                                connSQLite.Insert(foreingProfile);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            //Whatsapp
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetWhatsapp);

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreingProfile = new ForeingProfile
                            {
                                BoxId = BoxId,
                                UserId = UserId,
                                ProfileName = (string)reader["Name"],
                                value = (string)reader["Number"],
                                ProfileType = "Whatsapp"
                            };

                            //Insertar la box foranea
                            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                            {
                                connSQLite.Insert(foreingProfile);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            //Social media
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetSMProfiles);

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int IdRedSocial = (int)reader["RedSocialId"];
                            var Name = string.Empty;
                            switch (IdRedSocial)
                            {
                                case 1:
                                    Name = "Facebook";
                                    break;
                                case 2:
                                    Name = "Instagram";
                                    break;
                                case 3:
                                    Name = "Twitter";
                                    break;
                                case 4:
                                    Name = "Snapchat";
                                    break;
                                case 5:
                                    Name = "LinkedIn";
                                    break;
                                case 6:
                                    Name = "TikTok";
                                    break;
                                case 7:
                                    Name = "Youtube";
                                    break;
                                case 8:
                                    Name = "Spotify";
                                    break;
                                case 9:
                                    Name = "Twitch";
                                    break;
                                case 10:
                                    Name = "WebPage";
                                    break;
                                default:
                                    break;
                            };
                            foreingProfile = new ForeingProfile
                            {
                                BoxId = BoxId,
                                UserId = UserId,
                                ProfileName = (string)reader["ProfileName"],
                                value = (string)reader["link"],
                                ProfileType = Name
                            };

                            //Insertar la box foranea
                            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                            {
                                connSQLite.Insert(foreingProfile);
                            }
                        }
                    }

                    connection.Close();
                }
            }

            MainViewModel.GetInstance().ListForeignBox.AddList(foreingBox);
            MainViewModel.GetInstance().ListForeignBox.GetList();
            //Enviar a detalles de la box foranea cuando se inserta
            App.Navigator.PushAsync(new ForeingBoxPage(foreingBox, true));
            //App.Current.MainPage = new Xamarin.Forms.NavigationPage(new Mynfo.Views.ForeingBoxPage(foreingBox, true));
        }
        #endregion

        #region Commands
        private async void CreateBox_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.BoxRegister = new BoxRegisterViewModel();
            await Navigation.PushAsync(new BoxRegisterPage());
        }        
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection.FirstOrDefault() as Box;
            if (selectedItem == null)
                return;
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(selectedItem);
            await Navigation.PushPopupAsync(new DetailBoxPopUpPage(selectedItem));
            ((CollectionView)sender).SelectedItem = null;
        }
        #endregion
    }
}