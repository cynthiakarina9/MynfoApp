namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Resources;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using Rg.Plugins.Popup.Extensions;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsBoxEdithPage
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        public Entry BxNameEntry = new Entry();
        public String boxName;
        public Box Box;
        public Color NewColor;
        public string ColorH;
        #endregion

        #region Properties
        private ObservableCollection<ProfileLocal> ProfilesSelected { get; set; }
        public ProfileLocal selectedItem { get; set; }
        public String BoxName { get; set; }
        #endregion

        #region Constructor
        public DetailsBoxEdithPage(int _boxId = 0)
        {
            InitializeComponent();
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            changeColor();
            NavigationPage.SetHasNavigationBar(this, false);
            FullBackGround.BackgroundColor = NewColor;
            #region F
            apiService = new ApiService();
            
            FullBackGround.CloseWhenBackgroundIsClicked = true;
            ProfilesSelected = new ObservableCollection<ProfileLocal>();
            int BoxId = _boxId;
            int UserID = MainViewModel.GetInstance().User.UserId;
            bool BoxDefault = false;
            var BxSaveName = new Button();
            var BxBtnDelete = new ImageButton();
            var bxBtnHome = new ImageButton();
            var BxDefaultCheckBox = new CheckBox();

            //Definir color de fondo con respecto a si la box es predeterminada
            if (currentTheme == OSAppTheme.Dark)
            {
                BackG.BackgroundColor = Color.FromHex("#222b3a");
                bxBtnHome.BackgroundColor = Color.FromHex("#222b3a");
                BxSaveName.BackgroundColor = Color.FromHex("#222b3a");
                BxBtnDelete.BackgroundColor = Color.FromHex("#222b3a");
                DeleteButton.Source = "trash3";
            }
            else
            {
                BackG.BackgroundColor = Color.FromHex("#FFFFFF");
                bxBtnHome.BackgroundColor = Color.FromHex("#FFFFFF");
                BxSaveName.BackgroundColor = Color.FromHex("#FFFFFF");
                BxBtnDelete.BackgroundColor = Color.FromHex("#FFFFFF");
                DeleteButton.Source = "trash2";
            }
            
            //Acción de boton de borrado
            DeleteButton.Clicked += new EventHandler((sender, e) => deleteBox(sender, e, BoxId, UserID, BoxDefault));

            //Acción de botón actualización de Box
            BoxUpdateBtn.Clicked += new EventHandler((sender, e) => UpdateBoxName(sender, e, BoxId, BxNameEntry.Text, UserID, BxNameEntry.IsReadOnly));

            #endregion
        }
        #endregion

        #region Methods
        
        async void deleteBox(object sender, EventArgs e, int _BoxId, int _UserId, bool _BoxDefault)
        {
            string sqlDeleteEmails = "delete from dbo.Box_ProfileEmail where dbo.Box_ProfileEmail.BoxId = " + _BoxId,
                    sqlDeletePhones = "delete from dbo.Box_ProfilePhone where dbo.Box_ProfilePhone.BoxId = " + _BoxId,
                    sqlDeleteSMProfiles = "delete from dbo.Box_ProfileSM where dbo.Box_ProfileSM.BoxId = " + _BoxId,
                    sqlDeleteWhatsappProfiles = "delete from dbo.Box_ProfileWhatsapp where dbo.Box_ProfileWhatsapp.BoxId = " + _BoxId,
                    sqlDeleteBox = "delete from dbo.Boxes where dbo.boxes.BoxId = " + _BoxId;
            string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            //string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;
            string sql;

            bool answer = await DisplayAlert(Resource.Warning, Resource.DeleteBoxNotification, Resource.Yes, Resource.No);
            #region If
            if (answer == true)
            {
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    //Borrar emails de la box
                    sb = new System.Text.StringBuilder();
                    sb.Append(sqlDeleteEmails);
                    sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    //Borrar teléfonos de la box
                    sb = new System.Text.StringBuilder();
                    sb.Append(sqlDeletePhones);
                    sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    //Borrar perfiles de redes sociales de la box
                    sb = new System.Text.StringBuilder();
                    sb.Append(sqlDeleteSMProfiles);
                    sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    //Borrar perfiles de whatsapp de la box
                    sb = new System.Text.StringBuilder();
                    sb.Append(sqlDeleteWhatsappProfiles);
                    sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    //Borrar box
                    sb = new System.Text.StringBuilder();
                    sb.Append(sqlDeleteBox);
                    sql = sb.ToString();

                    var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

                    var Box = await this.apiService.GetBox(
                        apiSecurity,
                        "/api",
                        "/Boxes",
                        _BoxId);
                    MainViewModel.GetInstance().Home.RemoveList(Box);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    //Si la box era predeterminada
                    if (_BoxDefault == true)
                    {
                        //Borrar box predeterminada anterior
                        using (var conn = new SQLite.SQLiteConnection(App.root_db))
                        {
                            conn.DeleteAll<BoxLocal>();
                        }
                        //Borrar perfiles de box predeterminada anteriores
                        using (var conn = new SQLite.SQLiteConnection(App.root_db))
                        {
                            conn.DeleteAll<ProfileLocal>();
                        }

                        string sqlUpdateBoxDefault = "update top (1) dbo.Boxes set BoxDefault = 1 where dbo.Boxes.UserId = " + _UserId;
                        BoxLocal boxLocal;
                        bool boxLocalExists = false;
                        int boxIdLocal = 0;

                        //Definir nueva box default
                        sb = new System.Text.StringBuilder();
                        sb.Append(sqlUpdateBoxDefault);
                        sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }


                        string sqlGetNewDefault = "select * from dbo.Boxes " +
                                                    "where dbo.Boxes.UserId = " + _UserId +
                                                    "and dbo.Boxes.BoxDefault = 1";

                        //Definir nueva box default
                        sb = new System.Text.StringBuilder();
                        sb.Append(sqlGetNewDefault);
                        sql = sb.ToString();
                        //Creación de nueva box local
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
                                        Name = (String)reader["Name"],
                                        UserId = MainViewModel.GetInstance().User.UserId,
                                        Time = (DateTime)reader["Time"],
                                        FirstName = MainViewModel.GetInstance().User.FirstName,
                                        LastName = MainViewModel.GetInstance().User.LastName,
                                        ImagePath = MainViewModel.GetInstance().User.ImagePath,
                                        UserTypeId = MainViewModel.GetInstance().User.UserTypeId
                                    };
                                    //Crear box local predeterminada
                                    using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        conn.CreateTable<BoxLocal>();
                                        conn.Insert(boxLocal);
                                    }
                                    //Crear tabla de perfiles de box local predeterminada
                                    using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        conn.CreateTable<ProfileLocal>();
                                    }
                                    boxLocalExists = true;
                                    boxIdLocal = (int)reader["BoxId"];
                                }
                            }

                            connection.Close();
                        }

                        //Si se creo la box local, procedo a crear los perfiles locales
                        if (boxLocalExists == true)
                        {
                            //Creación de perfiles locales de box local
                            string queryGetBoxEmail = "select * from dbo.ProfileEmails " +
                                            "join dbo.Box_ProfileEmail on" +
                                            "(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                                            "where dbo.Box_ProfileEmail.BoxId = " + boxIdLocal;
                            string queryGetBoxPhone = "select * from dbo.ProfilePhones " +
                                                        "join dbo.Box_ProfilePhone on" +
                                                        "(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                                                        "where dbo.Box_ProfilePhone.BoxId = " + boxIdLocal;
                            string queryGetBoxSMProfiles = "select * from dbo.ProfileSMs " +
                                                            "join dbo.Box_ProfileSM on" +
                                                            "(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                                            "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                                            "where dbo.Box_ProfileSM.BoxId = " + boxIdLocal;
                            string queryGetBoxWhatsappProfiles = "select * from dbo.ProfileWhatsapps join dbo.Box_ProfileWhatsapp on " +
                                                "(dbo.ProfileWhatsapps.ProfileWhatsappId = dbo.Box_ProfileWhatsapp.ProfileWhatsappId) " +
                                                "where dbo.Box_ProfileWhatsapp.BoxId = " + boxIdLocal;

                            //Consulta para obtener perfiles email
                            using (SqlConnection conn = new SqlConnection(cadenaConexion))
                            {
                                sb = new System.Text.StringBuilder();
                                sb.Append(queryGetBoxEmail);

                                sql = sb.ToString();

                                using (SqlCommand command = new SqlCommand(sql, conn))
                                {
                                    conn.Open();
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            ProfileLocal emailProfile = new ProfileLocal
                                            {
                                                IdBox = boxIdLocal,
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

                                    conn.Close();
                                }
                            }

                            //Consulta para obtener perfiles teléfono
                            using (SqlConnection conn = new SqlConnection(cadenaConexion))
                            {
                                sb = new System.Text.StringBuilder();
                                sb.Append(queryGetBoxPhone);

                                sql = sb.ToString();

                                using (SqlCommand command = new SqlCommand(sql, conn))
                                {
                                    conn.Open();
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            ProfileLocal phoneProfile = new ProfileLocal
                                            {
                                                IdBox = boxIdLocal,
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

                                    conn.Close();
                                }
                            }

                            //Consulta para obtener perfiles de redes sociales
                            using (SqlConnection conn = new SqlConnection(cadenaConexion))
                            {
                                sb = new System.Text.StringBuilder();
                                sb.Append(queryGetBoxSMProfiles);

                                sql = sb.ToString();

                                using (SqlCommand command = new SqlCommand(sql, conn))
                                {
                                    conn.Open();
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            ProfileLocal smProfile = new ProfileLocal
                                            {
                                                IdBox = boxIdLocal,
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

                                    conn.Close();
                                }
                            }

                            //Consulta para obtener perfiles whatsapp
                            using (SqlConnection conn = new SqlConnection(cadenaConexion))
                            {
                                sb = new System.Text.StringBuilder();
                                sb.Append(queryGetBoxWhatsappProfiles);

                                sql = sb.ToString();

                                using (SqlCommand command = new SqlCommand(sql, conn))
                                {
                                    conn.Open();
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            ProfileLocal whatsappProfile = new ProfileLocal
                                            {
                                                IdBox = boxIdLocal,
                                                UserId = (int)reader["UserId"],
                                                ProfileName = (string)reader["Name"],
                                                value = (string)reader["Number"],
                                                ProfileType = "Whatsapp"
                                            };
                                            //Crear perfil de whatsapp de box local predeterminada
                                            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                            {
                                                connSQLite.Insert(whatsappProfile);
                                            }
                                        }
                                    }

                                    conn.Close();
                                }
                            }
                        }

                    }
                }

                //Regresar a home

            MainViewModel.GetInstance().Home = new HomeViewModel();
                Application.Current.MainPage = new MasterPage();
                await PopupNavigation.Instance.PopAllAsync();
            }
            #endregion
            //if(answer == true)
            //{
            //    MainViewModel.GetInstance().DetailsBoxEdith.DeleteBox(_BoxId);
            //}
            //else
            //{
            //    return;
            //}
        }

        private async void UpdateBoxName(object sender, EventArgs e, int _BoxId, string _name, int _UserId, bool disabled)
        {
            //Actualizar el nombre de la Box
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            Box = await this.apiService.GetBox(
                apiSecurity,
                "/api",
                "/Boxes",
                _BoxId);
            var box3 = new Box();
            if (ColorH == "")
            {
                box3 = new Box
                {
                    BoxId = Box.BoxId,
                    BoxDefault = Box.BoxDefault,
                    Name = NameEntry.Text,
                    UserId = Box.UserId,
                    Time = Box.Time,
                    ColorBox = Box.ColorBox
                };
            }
            else
            {
                box3 = new Box
                {
                    BoxId = Box.BoxId,
                    BoxDefault = Box.BoxDefault,
                    Name = NameEntry.Text,
                    UserId = Box.UserId,
                    Time = Box.Time,
                    ColorBox = ColorH
                };
            }
            
            await MainViewModel.GetInstance().DetailsBoxEdith.EdithBox(box3);

            //BoxName = _name;

            foreach (ProfileLocal Prof in ProfilesSelected)
            {
                if (Prof.Logo == "mail3")
                {
                    DeleteProfileEmail(_BoxId, Prof.ProfileId);
                    ProfileEmail E = Converter.ToProfileEmail(Prof);
                    MainViewModel.GetInstance().DetailsBox.removeProfileEmail(E);
                }
                else if (Prof.Logo == "tel3")
                {
                    DeleteProfilePhone(_BoxId, Prof.ProfileId);
                    ProfilePhone P = Converter.ToProfilePhone(Prof);
                    MainViewModel.GetInstance().DetailsBox.removeProfilePhone(P);
                }
                else if (Prof.Logo == "whatsapp3")
                {
                    DeleteProfileWhatsapp(_BoxId, Prof.ProfileId);
                    ProfileWhatsapp W = Converter.ToProfileWhatsapp(Prof);
                    MainViewModel.GetInstance().DetailsBox.removeProfileW(W);
                }
                else if (Prof.Logo != "mail3" && Prof.Logo != "tel3" && Prof.Logo != "whatsapp3")
                {
                    DeleteProfileSM(_BoxId, Prof.ProfileId);
                    ProfileSM SM = Converter.ToProfileSM(Prof);
                    MainViewModel.GetInstance().DetailsBox.removeProfileSM(SM);
                }
            }
            
            await Navigation.PopPopupAsync();
        }

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

        public async void DeleteProfileSM(int _box, int _profileSMId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await Navigation.PopPopupAsync();
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

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string previous = (e.PreviousSelection.FirstOrDefault() as ProfileLocal)?.Name;
            selectedItem = e.CurrentSelection.FirstOrDefault() as ProfileLocal;
            ProfilesSelected.Add(selectedItem);
            if(selectedItem.Logo== "mail3")
            {
                ProfileEmail E = Converter.ToProfileEmail(selectedItem);
                MainViewModel.GetInstance().DetailsBoxEdith.removeProfileEmail(E);
            }
            else if (selectedItem.Logo == "tel3")
            {
                ProfilePhone P = Converter.ToProfilePhone(selectedItem);
                MainViewModel.GetInstance().DetailsBoxEdith.removeProfilePhone(P);
            }
            else if (selectedItem.Logo == "whatsapp3")
            {
                ProfileWhatsapp W = Converter.ToProfileWhatsapp(selectedItem);
                MainViewModel.GetInstance().DetailsBoxEdith.removeProfileW(W);
            }
            else if (selectedItem.Logo != "mail3" && selectedItem.Logo != "tel3" && selectedItem.Logo != "whatsapp3")
            {
                ProfileSM SM = Converter.ToProfileSM(selectedItem);
                MainViewModel.GetInstance().DetailsBoxEdith.removeProfileSM(SM);
            }
        }

        private string changeColor()
        {
            ColorH = "";
            Dictionary<string, string> nameToColor = new Dictionary<string, string>
            {
                { "Verde", "#12947f" }, { "Verde Agua", "#2fc4b2" },
                { "Azul oscuro", "#404a7f" }, { "Anaranjado", "#FF5521" },
                { "Azul claro", "#508ed8" }, { "Amarillo", "#d89a00" },
                { "Fuchsia", "#ff0033" }, { "Verde Oscuro", "#008445" },
                { "Morado", "#7f416a" }, { "Lila ", "#6f50ff" },
                { "Rojo", "#c1271f" }, { "Rosa", "ce7d7d" },
                //{ "Silver", Color.Silver }, { "Teal", Color.Teal },
                //{ "White", Color.White }, { "Yellow", Color.Yellow }
            };

            foreach (string colorName in nameToColor.Keys)
            {
                paleta.Items.Add(colorName);
            }
            paleta.SelectedIndexChanged += (sender2, args) =>
                {
                    if (paleta.SelectedIndex == -1)
                    {
                        ColorH = "#c6c6c6";
                    }
                    else
                    {
                        string colorName = paleta.Items[paleta.SelectedIndex];
                        
                        switch(colorName)
                        {
                            case "Verde":
                                ColorH = "#12947f";
                                break;
                            case "Verde Agua":
                                ColorH = "#2fc4b2";
                                break;
                            case "Azul oscuro":
                                ColorH = "#404a7f";
                                break;
                            case "Anaranjado":
                                ColorH = "#FF5521";
                                break;
                            case "Azul claro":
                                ColorH = "#508ed8";
                                break;
                            case "Amarillo":
                                ColorH = "#d89a00";
                                break;
                            case "Fuchsia":
                                ColorH = "#ff0033";
                                break;
                            case "Verde Oscuro":
                                ColorH = "#008445";
                                break;
                            case "Morado":
                                ColorH = "#7f416a";
                                break;
                            case "Lila ":
                                ColorH = "#6f50ff";
                                break;
                            case "Rojo ":
                                ColorH = "#c1271f";
                                break;
                            case "Rosa ":
                                ColorH = "#ce7d7d";
                                break;
                            default:
                                break;
                        }
                    }
                };
            return ColorH;
        }
        
       
        #endregion

    }
}