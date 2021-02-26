namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using Rg.Plugins.Popup.Extensions;
    using Rg.Plugins.Popup.Services;
    using SkiaSharp.Views.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailBoxPopUpPage
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Properties
        public Entry BxNameEntry = new Entry();
        public String BoxName;
        public Box Box { get; set; }
        #endregion

        #region Constructor
        public DetailBoxPopUpPage(Box _Box)
        {
            InitializeComponent();
            apiService = new ApiService();
            NavigationPage.SetHasNavigationBar(this, false);
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            FrameB.CloseWhenBackgroundIsClicked = true;

            #region Lastcode
            int BoxId = _Box.BoxId;
            var boxLocal = new BoxLocal();
            int UserID = MainViewModel.GetInstance().User.UserId;
            string consultaDefault;
            string queryLastBoxCreated = "SELECT TOP 1 * FROM dbo.Boxes where dbo.Boxes.UserId = " + UserID + " ORDER BY BoxId DESC";
            string queryUpdatesetDefault;
            string queryUpdateTakeOffDefault;
            string queryGetPhones;
            string queryGetEmails;
            string queryGetSMProfiles;
            string queryGetWhatsapp;
            string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            //string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb;
            BoxName = "";
            bool BoxDefault = false;
            int UserId = 0;
            DateTime boxcreation = DateTime.Now;
            var BxSaveName = new ImageButton();
            var BxBtnDelete = new ImageButton();
            var bxBtnHome = new ImageButton();
            var BxDefaultCheckBox = new CheckBox();
            int listProfileNum = 0;

            //Asignación de querys
            consultaDefault = "select * from dbo.Boxes where dbo.Boxes.BoxId = " + BoxId;
            queryUpdatesetDefault = "update dbo.Boxes set BoxDefault = 1 where dbo.Boxes.UserId =" + UserID + " and dbo.Boxes.BoxId =" + BoxId;
            queryUpdateTakeOffDefault = "update dbo.Boxes set BoxDefault = 0 where dbo.Boxes.UserId =" + UserID + " and dbo.Boxes.BoxDefault = 1 and dbo.Boxes.BoxId !=" + BoxId;
            queryGetPhones = "select dbo.Boxes.BoxId, dbo.ProfilePhones.ProfilePhoneId, dbo.ProfilePhones.Name, " +
                             "dbo.ProfilePhones.Number from dbo.Box_ProfilePhone Join dbo.Boxes " +
                             "on(dbo.Boxes.BoxId = dbo.Box_ProfilePhone.BoxId) " +
                             "Join dbo.ProfilePhones on(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                             "where dbo.Boxes.BoxId = " + BoxId;
            queryGetEmails = "select dbo.Boxes.BoxId, dbo.ProfileEmails.ProfileEmailId, dbo.ProfileEmails.Name, " +
                              "dbo.ProfileEmails.Email from dbo.Box_ProfileEmail " +
                              "Join dbo.Boxes on(dbo.Boxes.BoxId = dbo.Box_ProfileEmail.BoxId) " +
                              "Join dbo.ProfileEmails on(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                              "where dbo.Boxes.BoxId = " + BoxId;
            queryGetSMProfiles = "select * from dbo.Box_ProfileSM " +
                                    "join dbo.ProfileSMs on(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                    "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                    "where dbo.Box_ProfileSM.BoxId = " + _Box.BoxId;
            queryGetWhatsapp = "select dbo.Boxes.BoxId, dbo.ProfileWhatsapps.ProfileWhatsappId, dbo.ProfileWhatsapps.Name, " +
                                        "dbo.ProfileWhatsapps.Number from dbo.Box_ProfileWhatsapp Join dbo.Boxes " +
                                        "on(dbo.Boxes.BoxId = dbo.Box_ProfileWhatsapp.BoxId) " +
                                        "Join dbo.ProfileWhatsapps on(dbo.ProfileWhatsapps.ProfileWhatsappId = dbo.Box_ProfileWhatsapp.ProfileWhatsappId) " +
                                        "where dbo.Boxes.BoxId =" + _Box.BoxId;

            //Consulta para obtener Box
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(consultaDefault);

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BoxName = (string)reader["Name"];
                            BoxDefault = (bool)reader["BoxDefault"];
                            UserId = (int)reader["UserId"];
                            boxcreation = (DateTime)reader["Time"];
                        }
                    }

                    connection.Close();
                }
            }

            boxLocal.BoxId = _Box.BoxId;
            boxLocal.Name = BoxName;
            boxLocal.BoxDefault = BoxDefault;
            boxLocal.UserId = UserId;
            boxLocal.Time = boxcreation;
            boxLocal.FirstName = MainViewModel.GetInstance().User.FirstName;
            boxLocal.LastName = MainViewModel.GetInstance().User.LastName;
            boxLocal.ImagePath = MainViewModel.GetInstance().User.ImagePath;
            boxLocal.UserTypeId = MainViewModel.GetInstance().User.UserTypeId;

            //Definir color de fondo con respecto a si la box es predeterminada
            if (_Box.BoxDefault == true)
            {
                if (currentTheme == OSAppTheme.Light)
                {
                    if (_Box.ColorBox != null)
                    {
                        FrameB.BackgroundColor = Color.FromHex(_Box.ColorBox);
                    }
                    else
                    {
                        FrameB.BackgroundColor = Color.FromHex("#c6c6c6");
                    }
                    BackG.BackgroundColor = Color.FromHex("#FFFFFF");
                    bxBtnHome.BackgroundColor = Color.FromHex("#FFFFFF");
                    BxSaveName.BackgroundColor = Color.FromHex("#FFFFFF");
                    BxBtnDelete.BackgroundColor = Color.FromHex("#FFFFFF");
                }
                else
                {
                    if (_Box.ColorBox != null)
                    {
                        FrameB.BackgroundColor = Color.FromHex(_Box.ColorBox);
                    }
                    else
                    {
                        FrameB.BackgroundColor = Color.FromHex("#a4a4a4");
                    }
                    BackG.BackgroundColor = Color.FromHex("#222b3a");
                    bxBtnHome.BackgroundColor = Color.FromHex("#222b3a");
                    BxSaveName.BackgroundColor = Color.FromHex("#222b3a");
                    BxBtnDelete.BackgroundColor = Color.FromHex("#222b3a");
                }

            }
            else
            {
                if (currentTheme == OSAppTheme.Light)
                {
                    if (_Box.ColorBox != null)
                    {
                        FrameB.BackgroundColor = Color.FromHex(_Box.ColorBox);
                    }
                    else
                    {
                        FrameB.BackgroundColor = Color.FromHex("#c6c6c6");
                    }
                    BackG.BackgroundColor = Color.FromHex("#FFFFFF");
                    bxBtnHome.BackgroundColor = Color.FromHex("#FFFFFF");
                    BxSaveName.BackgroundColor = Color.FromHex("#FFFFFF");
                    BxBtnDelete.BackgroundColor = Color.FromHex("#FFFFFF");
                }
                else
                {
                    if (_Box.ColorBox != null)
                    {
                        FrameB.BackgroundColor = Color.FromHex(_Box.ColorBox);
                    }
                    else
                    {
                        FrameB.BackgroundColor = Color.FromHex("#a4a4a4");
                    }
                    BackG.BackgroundColor = Color.FromHex("#222b3a");
                    bxBtnHome.BackgroundColor = Color.FromHex("#222b3a");
                    BxSaveName.BackgroundColor = Color.FromHex("#222b3a");
                    BxBtnDelete.BackgroundColor = Color.FromHex("#222b3a");
                }
            }

            //Tamaño de etiqueta de box predeterminada
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                defaultLabel.FontSize = 13;
            }


            //Navegación a ventana de perfiles
            BoxProfiles.Clicked += new EventHandler((sender, e) => BoxDetails_Clicked(sender, e, BoxId, BoxDefault, BoxName));

            //Botón de Editar
            EdithButton.Clicked += new EventHandler((sender, e) => edithBox(sender, e, BoxId, UserID, BoxDefault));

            //Creación del checkbox de box predeterminada
            BxDefaultCheckBox.IsChecked = BoxDefault;
            BxDefaultCheckBox.VerticalOptions = LayoutOptions.Start;
            if(Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                Thickness thick = new Thickness(20, -6, 0, 0);
                BoxDefaultCheckBox.Margin = thick;
            }
            else if(Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                Thickness thick = new Thickness(20, 0, 0, 0);
                BoxDefaultCheckBox.Margin = thick;
            }
            if (BoxDefault == true)
            {
                BxDefaultCheckBox.IsEnabled = false;
                if (currentTheme == OSAppTheme.Dark)
                {
                    BxDefaultCheckBox.Color = Color.White;
                    EdithButton.Source = "edit3";
                    BoxProfiles.Source = "plusb";
                }
                else
                {
                    BxDefaultCheckBox.Color = Color.FromHex("#222b3a");
                    EdithButton.Source = "edit1";
                    BoxProfiles.Source = "plusn";
                }
            }
            else
            {
                if (currentTheme == OSAppTheme.Dark)
                {
                    BxDefaultCheckBox.IsEnabled = true;
                    BxDefaultCheckBox.Color = Color.FromHex("FFFFFF");
                    EdithButton.Source = "edit3";
                    BoxProfiles.Source = "plusb";
                }
                else
                {
                    BxDefaultCheckBox.IsEnabled = true;
                    BxDefaultCheckBox.Color = Color.FromHex("FF5521");
                    EdithButton.Source = "edit2";
                    BoxProfiles.Source = "Plus";
                }
            }
            BxDefaultCheckBox.CheckedChanged += CheckDefaultBox;

            BoxDefaultCheckBox.Children.Add(BxDefaultCheckBox);

            //Marcar o desmarcar la box predeterminada
            void CheckDefaultBox(object sender, EventArgs e)
            {
                string queryGetBoxEmail = "select * from dbo.ProfileEmails " +
                                            "join dbo.Box_ProfileEmail on" +
                                            "(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                                            "where dbo.Box_ProfileEmail.BoxId = " + _Box.BoxId;
                string queryGetBoxPhone = "select * from dbo.ProfilePhones " +
                                            "join dbo.Box_ProfilePhone on" +
                                            "(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                                            "where dbo.Box_ProfilePhone.BoxId = " + _Box.BoxId;
                string queryGetBoxSMProfiles = "select * from dbo.ProfileSMs " +
                                                "join dbo.Box_ProfileSM on" +
                                                "(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                                "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                                "where dbo.Box_ProfileSM.BoxId = " + _Box.BoxId;
                string queryGetBoxWhatsapp = "select * from dbo.ProfileWhatsapps join dbo.Box_ProfileWhatsapp on " +
                                                "(dbo.ProfileWhatsapps.ProfileWhatsappId = dbo.Box_ProfileWhatsapp.ProfileWhatsappId) " +
                                                "where dbo.Box_ProfileWhatsapp.BoxId = " + _Box.BoxId;

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


                //Consulta para obtener perfiles email
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryGetBoxEmail);

                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProfileLocal emailProfile = new ProfileLocal
                                {
                                    IdBox = _Box.BoxId,
                                    UserId = (int)reader["UserId"],
                                    ProfileName = (string)reader["Name"],
                                    value = (string)reader["Email"],
                                    ProfileType = "Email"
                                };
                                //Crear perfil de correo de box local predeterminada
                                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                {
                                    conn.Insert(emailProfile);
                                }
                            }
                        }

                        connection.Close();
                    }
                }

                //Consulta para obtener perfiles teléfono
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryGetBoxPhone);

                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProfileLocal phoneProfile = new ProfileLocal
                                {
                                    IdBox = _Box.BoxId,
                                    UserId = (int)reader["UserId"],
                                    ProfileName = (string)reader["Name"],
                                    value = (string)reader["Number"],
                                    ProfileType = "Phone"
                                };
                                //Crear perfil de teléfono de box local predeterminada
                                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                {
                                    conn.Insert(phoneProfile);
                                }
                            }
                        }

                        connection.Close();
                    }
                }

                //Consulta para obtener perfiles de redes sociales
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryGetBoxSMProfiles);

                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProfileLocal smProfile = new ProfileLocal
                                {
                                    IdBox = _Box.BoxId,
                                    UserId = (int)reader["UserId"],
                                    ProfileName = (string)reader["ProfileName"],
                                    value = (string)reader["link"],
                                    ProfileType = (string)reader["Name"]
                                };
                                //Crear perfil de teléfono de box local predeterminada
                                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                {
                                    conn.Insert(smProfile);
                                }
                            }
                        }

                        connection.Close();
                    }
                }

                //Consulta para obtener perfiles de whatsapp
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryGetBoxWhatsapp);

                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProfileLocal whatsappProfile = new ProfileLocal
                                {
                                    IdBox = _Box.BoxId,
                                    UserId = (int)reader["UserId"],
                                    ProfileName = (string)reader["Name"],
                                    value = (string)reader["Number"],
                                    ProfileType = "Whatsapp"
                                };
                                //Crear perfil de teléfono de box local predeterminada
                                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                {
                                    conn.Insert(whatsappProfile);
                                }
                            }
                        }

                        connection.Close();
                    }
                }

                //Consulta para predeterminar la box actual
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryUpdatesetDefault);
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }

                //Consulta para quitar predeterminado de la box default anterior
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryUpdateTakeOffDefault);
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                MainViewModel.GetInstance().Home.GetBoxDefault();
                BxDefaultCheckBox.IsEnabled = false;
                MainViewModel.GetInstance().Home.GetBoxNoDefault();
            }
            #endregion

        }
        #endregion

        #region Methods
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        public async Task<Box> GetBoxe(int _BoxId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            Box = new Box();
            Box = await this.apiService.GetBox(
                apiSecurity,
                "/api",
                "/Boxes",
                _BoxId);

            return Box;
        }
        async void edithBox(object sender, EventArgs e, int _BoxId, int _UserId, bool _BoxDefault)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.DetailsBoxEdith = new DetailsBoxEdithViewModel(_BoxId);
            await PopupNavigation.Instance.PushAsync(new DetailsBoxEdithPage(_BoxId));
        }

        private void BoxDetails_Clicked(object sender, EventArgs e, int _BoxId, bool _boxDefault, string _boxName)
        {
            //App.Navigator.PushAsync(new TabbedListOfNetworksPage(_BoxId, _boxDefault, _boxName), false);
            //Navigation.PopPopupAsync();
            MainViewModel.GetInstance().ListOfNetworks = new ListOfNetworksViewModel(_BoxId);
            PopupNavigation.Instance.PushAsync(new ListOfNetworksPage(_BoxId));
        }

        private void BackHome(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
            Navigation.PopPopupAsync();
        }
        #endregion
    }
}