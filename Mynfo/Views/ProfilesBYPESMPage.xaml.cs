namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesBYPESMPage : ContentPage
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
        public ProfilePhone selectedProfilePhone { get; set; }
        public ProfileEmail selectedItemEmail { get; set; }
        public ProfileSM selectedItemSM { get; set; }
        public ProfileWhatsapp selectedItemWhatsapp { get; set; }
        #endregion

        #region Constructor
        public ProfilesBYPESMPage(int _BoxId, string _ProfileType, bool _BoxDefault, string _boxName = "")
        {
            apiService = new ApiService();
            Box = new Box();
            Box.BoxId = _BoxId;
            InitializeComponent();

            #region Variables
            int BoxId = _BoxId;
            int UserId = MainViewModel.GetInstance().User.UserId;
            string queryGetEmailProfiles;
            string queryGetPhoneProfiles;
            string queryGetFacebookProfiles;
            string queryGetWhatsappProfiles;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb, sb2;

            #endregion

            #region Queries

            queryGetEmailProfiles = "select dbo.ProfileEmails.ProfileEmailId from dbo.ProfileEmails " +
                                                "where dbo.ProfileEmails.UserId = " + UserId + " except select " +
                                                "dbo.Box_ProfileEmail.ProfileEmailId from dbo.Box_ProfileEmail " +
                                                "where dbo.Box_ProfileEmail.BoxId = " + BoxId;
            queryGetPhoneProfiles = "SELECT dbo.ProfilePhones.ProfilePhoneId FROM dbo.ProfilePhones " +
                                            "where dbo.ProfilePhones.UserId = " + UserId + " EXCEPT SELECT " +
                                            "dbo.Box_ProfilePhone.ProfilePhoneId FROM dbo.Box_ProfilePhone " +
                                            "where dbo.Box_ProfilePhone.BoxId = " + BoxId;
            queryGetFacebookProfiles = "SELECT dbo.ProfileSMs.ProfileMSId FROM dbo.ProfileSMs " +
                                            "where dbo.ProfileSMs.UserId = " + UserId +
                                            " and dbo.ProfileSMs.RedSocialId = 1 " +
                                            " EXCEPT SELECT " +
                                            "dbo.Box_ProfileSM.ProfileMSId FROM dbo.Box_ProfileSM " +
                                            "where dbo.Box_ProfileSM.BoxId = " + BoxId;
            queryGetWhatsappProfiles = "SELECT dbo.ProfileWhatsapps.ProfileWhatsappId FROM dbo.ProfileWhatsapps " +
                                            "where dbo.ProfileWhatsapps.UserId = " + UserId +
                                            " EXCEPT SELECT " +
                                            "dbo.Box_ProfileWhatsapp.ProfilePhoneId FROM dbo.Box_ProfileWhatsapp " +
                                            "where dbo.Box_ProfileWhatsapp.BoxId = " + BoxId;
            #endregion

            #region Commands

            //BackDetails.Clicked += new EventHandler((sender, e) => Back_Clicked(sender, e, _BoxId, _BoxDefault, _boxName));

            GoToProfiles.Clicked += new EventHandler((sender, e) => GoToProfiles_Clicked(sender, e, _BoxId, _ProfileType, _BoxDefault));



            #endregion

            switch (_ProfileType)
            {

                case "Email":
                    ProfileListEmail.IsVisible = true;
                    ProfileListPhone.IsVisible = false;
                    ProfileListSM.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    break;
                case "Phone":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = true;
                    ProfileListSM.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    break;
                case "Facebook":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListSM.IsVisible = true;
                    ProfileListWhatsapp.IsVisible = false;
                    break;
                case "Whatsapp":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListSM.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = true;
                    break;
                default:
                    break;


            }

            #region Consultas

            //switch (_ProfileType)
            //{
            //    case "Phone":
            //        Consulta para obtener teléfonos
            //        using (SqlConnection connection = new SqlConnection(cadenaConexion))
            //        {
            //            sb = new System.Text.StringBuilder();
            //            sb.Append(queryGetPhoneProfiles);
            //            string sql = sb.ToString();

            //            using (SqlCommand command = new SqlCommand(sql, connection))
            //            {
            //                connection.Open();
            //                using (SqlDataReader reader = command.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        int ProfilePhoneId = (int)reader["ProfilePhoneId"];
            //                        string queryGetProfile = "select * from dbo.ProfilePhones where dbo.ProfilePhones.ProfilePhoneId = " + ProfilePhoneId;

            //                        Validamos los valores que obtenemos
            //                        using (SqlConnection connection2 = new SqlConnection(cadenaConexion))
            //                        {
            //                            sb2 = new System.Text.StringBuilder();
            //                            sb2.Append(queryGetProfile);
            //                            string sql2 = sb2.ToString();

            //                            using (SqlCommand command2 = new SqlCommand(sql2, connection2))
            //                            {
            //                                connection2.Open();
            //                                using (SqlDataReader reader2 = command2.ExecuteReader())
            //                                {
            //                                    while (reader2.Read())
            //                                    {
            //                                        var phoneName = new Label();
            //                                        var phoneNumber = new Label();
            //                                        var CreateRelation = new ImageButton();
            //                                        var Line = new BoxView();

            //                                        phoneName.Text = (string)reader2["Name"];
            //                                        phoneName.FontSize = 15;
            //                                        phoneName.FontAttributes = FontAttributes.Bold;

            //                                        phoneNumber.Text = (string)reader2["Number"];
            //                                        phoneNumber.FontSize = 25;
            //                                        phoneNumber.HorizontalTextAlignment = TextAlignment.Center;

            //                                        CreateRelation.BackgroundColor = Color.Transparent;
            //                                        CreateRelation.CornerRadius = 15;
            //                                        CreateRelation.HeightRequest = 30;
            //                                        CreateRelation.WidthRequest = 30;
            //                                        CreateRelation.HorizontalOptions = LayoutOptions.End;
            //                                        CreateRelation.Source = "enter1";
            //                                        CreateRelation.Clicked += new EventHandler((sender, e) => CreateBoxPhoneRelation(sender, e, BoxId, ProfilePhoneId, _BoxDefault, _boxName));

            //                                        Line.HeightRequest = 1;
            //                                        Line.Color = Color.FromHex("#FF5521");

            //                                        ProfileList.Children.Add(phoneName);
            //                                        ProfileList.Children.Add(phoneNumber);
            //                                        ProfileList.Children.Add(CreateRelation);
            //                                        ProfileList.Children.Add(Line);
            //                                    }
            //                                }
            //                                connection2.Close();
            //                            }
            //                        }
            //                    }
            //                }
            //                connection.Close();
            //            }
            //        }
            //        break;
            //    case "Email":
            //        Consulta para obtener Emails
            //        using (SqlConnection connection = new SqlConnection(cadenaConexion))
            //        {
            //            sb = new System.Text.StringBuilder();
            //            sb.Append(queryGetEmailProfiles);
            //            string sql = sb.ToString();

            //            using (SqlCommand command = new SqlCommand(sql, connection))
            //            {
            //                connection.Open();
            //                using (SqlDataReader reader = command.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        int ProfileEmailId = (int)reader["ProfileEmailId"];
            //                        string queryGetProfile = "select * from dbo.ProfileEmails where dbo.ProfileEmails.ProfileEmailId = " + ProfileEmailId;

            //                        Validamos los valores que obtenemos
            //                        using (SqlConnection connection2 = new SqlConnection(cadenaConexion))
            //                        {
            //                            sb2 = new System.Text.StringBuilder();
            //                            sb2.Append(queryGetProfile);
            //                            string sql2 = sb2.ToString();

            //                            using (SqlCommand command2 = new SqlCommand(sql2, connection2))
            //                            {
            //                                connection2.Open();
            //                                using (SqlDataReader reader2 = command2.ExecuteReader())
            //                                {
            //                                    while (reader2.Read())
            //                                    {
            //                                        var emailProfile = new Label();
            //                                        var emailAddress = new Label();
            //                                        var CreateRelation = new ImageButton();
            //                                        var Line = new BoxView();

            //                                        emailProfile.Text = (string)reader2["Name"];
            //                                        emailProfile.FontSize = 15;
            //                                        emailProfile.FontAttributes = FontAttributes.Bold;

            //                                        emailAddress.Text = (string)reader2["Email"];
            //                                        emailAddress.FontSize = 25;
            //                                        emailAddress.HorizontalTextAlignment = TextAlignment.Center;

            //                                        CreateRelation.BackgroundColor = Color.Transparent;
            //                                        CreateRelation.CornerRadius = 15;
            //                                        CreateRelation.HeightRequest = 30;
            //                                        CreateRelation.WidthRequest = 30;
            //                                        CreateRelation.HorizontalOptions = LayoutOptions.End;
            //                                        CreateRelation.Source = "enter1";
            //                                        CreateRelation.Clicked += new EventHandler((sender, e) => CreateBoxEmailRelation(sender, e, BoxId, ProfileEmailId, _BoxDefault, _boxName));

            //                                        Line.HeightRequest = 1;
            //                                        Line.Color = Color.FromHex("#FF5521");

            //                                        ProfileList.Children.Add(emailProfile);
            //                                        ProfileList.Children.Add(emailAddress);
            //                                        ProfileList.Children.Add(CreateRelation);
            //                                        ProfileList.Children.Add(Line);
            //                                    }
            //                                }
            //                                connection2.Close();
            //                            }
            //                        }
            //                    }
            //                }
            //                connection.Close();
            //            }
            //        }
            //        break;
            //    case "Facebook":
            //        Consulta para obtener Facebook
            //        using (SqlConnection connection = new SqlConnection(cadenaConexion))
            //        {
            //            sb = new System.Text.StringBuilder();
            //            sb.Append(queryGetFacebookProfiles);
            //            string sql = sb.ToString();

            //            using (SqlCommand command = new SqlCommand(sql, connection))
            //            {
            //                connection.Open();
            //                using (SqlDataReader reader = command.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        int ProfileSMId = (int)reader["ProfileMSId"];
            //                        string queryGetProfile = "select * from dbo.ProfileSMs where dbo.ProfileSMs.ProfileMSId = " + ProfileSMId;

            //                        Validamos los valores que obtenemos
            //                        using (SqlConnection connection2 = new SqlConnection(cadenaConexion))
            //                        {
            //                            sb2 = new System.Text.StringBuilder();
            //                            sb2.Append(queryGetProfile);
            //                            string sql2 = sb2.ToString();

            //                            using (SqlCommand command2 = new SqlCommand(sql2, connection2))
            //                            {
            //                                connection2.Open();
            //                                using (SqlDataReader reader2 = command2.ExecuteReader())
            //                                {
            //                                    while (reader2.Read())
            //                                    {
            //                                        var SMProfile = new Label();
            //                                        var CreateRelation = new ImageButton();
            //                                        var Line = new BoxView();

            //                                        SMProfile.Text = (string)reader2["ProfileName"];
            //                                        SMProfile.FontSize = 25;
            //                                        SMProfile.FontAttributes = FontAttributes.Bold;
            //                                        SMProfile.HorizontalTextAlignment = TextAlignment.Center;

            //                                        CreateRelation.BackgroundColor = Color.Transparent;
            //                                        CreateRelation.CornerRadius = 15;
            //                                        CreateRelation.HeightRequest = 30;
            //                                        CreateRelation.WidthRequest = 30;
            //                                        CreateRelation.HorizontalOptions = LayoutOptions.End;
            //                                        CreateRelation.Source = "enter1";
            //                                        CreateRelation.Clicked += new EventHandler((sender, e) => CreateBoxSMRelation(sender, e, BoxId, ProfileSMId, _BoxDefault, _boxName));

            //                                        Line.HeightRequest = 1;
            //                                        Line.Color = Color.FromHex("#FF5521");

            //                                        ProfileList.Children.Add(SMProfile);
            //                                        ProfileList.Children.Add(CreateRelation);
            //                                        ProfileList.Children.Add(Line);
            //                                    }
            //                                }
            //                                connection2.Close();
            //                            }
            //                        }
            //                    }
            //                }
            //                connection.Close();
            //            }
            //        }
            //        break;

            //    case "Whatsapp":
            //        Consulta para obtener Whatsapp
            //        using (SqlConnection connection = new SqlConnection(cadenaConexion))
            //        {
            //            sb = new System.Text.StringBuilder();
            //            sb.Append(queryGetWhatsappProfiles);
            //            string sql = sb.ToString();

            //            using (SqlCommand command = new SqlCommand(sql, connection))
            //            {
            //                connection.Open();
            //                using (SqlDataReader reader = command.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        int ProfileWhatsappId = (int)reader["ProfileWhatsappId"];
            //                        string queryGetProfile = "select * from dbo.ProfileWhatsapps where dbo.ProfileWhatsapps.ProfileWhatsappId = " + ProfileWhatsappId;

            //                        Validamos los valores que obtenemos
            //                        using (SqlConnection connection2 = new SqlConnection(cadenaConexion))
            //                        {
            //                            sb2 = new System.Text.StringBuilder();
            //                            sb2.Append(queryGetProfile);
            //                            string sql2 = sb2.ToString();

            //                            using (SqlCommand command2 = new SqlCommand(sql2, connection2))
            //                            {
            //                                connection2.Open();
            //                                using (SqlDataReader reader2 = command2.ExecuteReader())
            //                                {
            //                                    while (reader2.Read())
            //                                    {
            //                                        var WAProfile = new Label();
            //                                        var CreateRelation = new ImageButton();
            //                                        var Line = new BoxView();

            //                                        WAProfile.Text = (string)reader2["Name"];
            //                                        WAProfile.FontSize = 25;
            //                                        WAProfile.FontAttributes = FontAttributes.Bold;
            //                                        WAProfile.HorizontalTextAlignment = TextAlignment.Center;

            //                                        CreateRelation.BackgroundColor = Color.Transparent;
            //                                        CreateRelation.CornerRadius = 15;
            //                                        CreateRelation.HeightRequest = 30;
            //                                        CreateRelation.WidthRequest = 30;
            //                                        CreateRelation.HorizontalOptions = LayoutOptions.End;
            //                                        CreateRelation.Source = "enter1";
            //                                        CreateRelation.Clicked += new EventHandler((sender, e) => CreateBoxWhatsappRelation(sender, e, BoxId, ProfileWhatsappId, _BoxDefault, _boxName));

            //                                        Line.HeightRequest = 1;
            //                                        Line.Color = Color.FromHex("#FF5521");

            //                                        ProfileList.Children.Add(WAProfile);
            //                                        ProfileList.Children.Add(CreateRelation);
            //                                        ProfileList.Children.Add(Line);
            //                                    }
            //                                }
            //                                connection2.Close();
            //                            }
            //                        }
            //                    }
            //                }
            //                connection.Close();
            //            }
            //        }
            //        break;
            //    default:
            //        break;
            //}

            #endregion

        }
        #endregion

        #region Methods
        private void GoToProfiles_Clicked(object sender, EventArgs e, int _boxId, string _profileType, bool _BoxDefault)
        {
            var mainViewModel = MainViewModel.GetInstance();
            switch (_profileType)
            {
                case "Phone":
                    mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
                    //Application.Current.MainPage = new NavigationPage(new CreateProfilePhonePage(_BoxDefault,_boxId));
                    App.Navigator.PushAsync(new CreateProfilePhonePage());
                    break;
                case "Email":
                    mainViewModel.CreateProfileEmail = new CreateProfileEmailViewModel();
                    //Application.Current.MainPage = new NavigationPage(new CreateProfileEmailPage(_BoxDefault,_boxId));
                    App.Navigator.PushAsync(new CreateProfileEmailPage());
                    break;
                case "Facebook":
                    mainViewModel.CreateProfileFacebook = new CreateProfileFacebookViewModel();
                    //Application.Current.MainPage= new NavigationPage(new CreateProfileFacebookPage(_BoxDefault, _boxId));
                    App.Navigator.PushAsync(new CreateProfileFacebookPage());
                    break;
                case "Whatsapp":
                    mainViewModel.CreateProfileWhatsApp = new CreateProfileWhatsAppViewModel();
                    //Application.Current.MainPage = new NavigationPage(new CreateProfileWhatsAppPage());
                    App.Navigator.PushAsync(new CreateProfileWhatsAppPage());
                    break;
                default:
                    break;
            }
        }

        private void CreateBoxEmailRelation(object sender, EventArgs e, int _BoxId, int _EmailId, bool _boxDefault, string _boxName)
        {
            //Crear la relación de la box con el correo
            string queryCreateEmailRelation = "INSERT INTO dbo.Box_ProfileEmail ( BoxId, ProfileEmailId) " +
                                         "VALUES(" + _BoxId + ","+ _EmailId + ")";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryCreateEmailRelation);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            //Agregar perfil local si la box es predeterminada
            if(_boxDefault == true)
            {
                string queryGetBoxEmail = "select * from dbo.ProfileEmails where dbo.ProfileEmails.ProfileEmailId = " + _EmailId;

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
                                    IdBox = _BoxId,
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
            }

            DisplayAlert(Languages.Success, Languages.NetworkAdded + "'" +  _boxName + "'", Languages.Close);

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Email", _boxDefault, _boxName));
        }

        private void CreateBoxPhoneRelation( int _BoxId, int _PhoneId, bool _boxDefault, string _boxName)
        {
            //Crear la relación de la box con el teléfono
            string queryCreatePhoneRelation = "INSERT INTO dbo.Box_ProfilePhone ( BoxId, ProfilePhoneId) " +
                                         "VALUES(" + _BoxId + "," + _PhoneId + ")";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryCreatePhoneRelation);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //Agregar perfil local si la box es predeterminada
            if (_boxDefault == true)
            {
                string queryGetBoxEmail = "select * from dbo.ProfilePhones where dbo.ProfilePhones.ProfilePhoneId = " + _PhoneId;

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
                                ProfileLocal phoneProfile = new ProfileLocal
                                {
                                    IdBox = _BoxId,
                                    UserId = (int)reader["UserId"],
                                    ProfileName = (string)reader["Name"],
                                    value = (string)reader["Number"],
                                    ProfileType = "Phone"
                                };
                                //Crear perfil de correo de box local predeterminada
                                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                {
                                    conn.Insert(phoneProfile);
                                }
                            }
                        }

                        connection.Close();
                    }
                }
            }

            DisplayAlert(Languages.Success, Languages.NetworkAdded + "'" + _boxName + "'", Languages.Close);

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Phone", _boxDefault, _boxName));
        }

        private void CreateBoxSMRelation(object sender, EventArgs e, int _BoxId, int _SMId, bool _boxDefault, string _boxName)
        {
            //Crear la relación de la box con Facebook
            string queryCreateSMRelation = "INSERT INTO dbo.Box_ProfileSM ( BoxId, ProfileMSId) " +
                                         "VALUES(" + _BoxId + "," + _SMId + ")";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryCreateSMRelation);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //Agregar perfil local si la box es predeterminada
            if (_boxDefault == true)
            {
                string queryGetBoxEmail = "select * from dbo.ProfileSMs where dbo.ProfileSMs.ProfileMSId = " + _SMId;

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
                                ProfileLocal facebookProfile = new ProfileLocal
                                {
                                    IdBox = _BoxId,
                                    UserId = (int)reader["UserId"],
                                    ProfileName = (string)reader["ProfileName"],
                                    value = (string)reader["link"],
                                    ProfileType = "Facebook"
                                };
                                //Crear perfil de correo de box local predeterminada
                                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                {
                                    conn.Insert(facebookProfile);
                                }
                            }
                        }

                        connection.Close();
                    }
                }
            }

            //DisplayAlert(Languages.Success, Languages.NetworkAdded + "'" + _boxName + "'", Languages.Close);

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Facebook", _boxDefault, _boxName));
        }

        private void CreateBoxWhatsappRelation(object sender, EventArgs e, int _BoxId, int _WhatsappId, bool _BoxDefault, string _boxName)
        {
            //Crear la relación de la box con Whatsapp
            string queryCreateSMRelation = "insert into dbo.Box_ProfileWhatsapp (BoxId, ProfilePhoneId) " +
                                         "VALUES(" + _BoxId + "," + _WhatsappId + ")";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryCreateSMRelation);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //Agregar perfil local si la box es predeterminada
            if (_BoxDefault == true)
            {
                string queryGetBoxEmail = "select * from dbo.ProfileWhatsapps where dbo.ProfileWhatsapps.ProfileWhatsappId = " + _WhatsappId;

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
                                ProfileLocal whatsAppProfile = new ProfileLocal
                                {
                                    IdBox = _BoxId,
                                    UserId = (int)reader["UserId"],
                                    ProfileName = (string)reader["Name"],
                                    value = (string)reader["Number"],
                                    ProfileType = "Whatsapp"
                                };
                                //Crear perfil de correo de box local predeterminada
                                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                                {
                                    conn.Insert(whatsAppProfile);
                                }
                            }
                        }

                        connection.Close();
                    }
                }
            }

            //DisplayAlert(Languages.Success, Languages.NetworkAdded + "'" + _boxName + "'", Languages.Close);

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Whatsapp", _BoxDefault, _boxName));
        }
        private void Back_Clicked(object sender, EventArgs e, int _BoxId,bool _boxDefault, string _boxName)
        {
            var mainViewModel = MainViewModel.GetInstance();
            Application.Current.MainPage = new NavigationPage(new ProfileTypeSelection(_BoxId, _boxDefault, _boxName));
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedItemEmail = e.SelectedItem as ProfileEmail;
            selectedProfilePhone = e.SelectedItem as ProfilePhone;
            selectedItemSM = e.SelectedItem as ProfileSM;
            selectedItemWhatsapp = e.SelectedItem as ProfileWhatsapp;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileEmail tappedItemEmail = e.Item as ProfileEmail;
            ProfilePhone tappedItemPhone = e.Item as ProfilePhone;
            ProfileSM tappedItemSM = e.Item as ProfileSM;
            ProfileWhatsapp tappedItemWhatsapp = e.Item as ProfileWhatsapp;

            if (tappedItemEmail != null)
            {
                if (tappedItemEmail.Exist == false)
                {
                    PostProfileEmail(Box.BoxId, tappedItemEmail.ProfileEmailId);
                    tappedItemEmail.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileEmail(tappedItemEmail);
                }
                else
                {
                    DeleteProfileEmail(Box.BoxId, tappedItemEmail.ProfileEmailId);
                    tappedItemEmail.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileEmail(tappedItemEmail);
                }
            }

            else if (tappedItemPhone != null)
            {
                if(tappedItemPhone.Exist == false)
                {
                    PostProfilePhone(Box.BoxId, tappedItemPhone.ProfilePhoneId);
                    tappedItemPhone.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfilePhone(tappedItemPhone);
                }
                else
                {
                    DeleteProfilePhone(Box.BoxId, tappedItemPhone.ProfilePhoneId);
                    tappedItemPhone.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfilePhone(tappedItemPhone);
                }
            }

            else if (tappedItemSM != null)
            {
                if (tappedItemSM.Exist == false)
                {
                    PostProfileSM(Box.BoxId, tappedItemSM.ProfileMSId);
                    tappedItemSM.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileSM(tappedItemSM);
                }
                else
                {
                    DeleteProfileSM(Box.BoxId, tappedItemSM.ProfileMSId);
                    tappedItemSM.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileSM(tappedItemSM);
                }
            }

            else if (tappedItemWhatsapp != null)
            {
                if (tappedItemWhatsapp.Exist == false)
                {
                    PostProfileWhatsapp(Box.BoxId, tappedItemWhatsapp.ProfileWhatsappId);
                    tappedItemWhatsapp.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileWhatsapp(tappedItemWhatsapp);
                }
                else
                {
                    DeleteProfileWhatsapp(Box.BoxId, tappedItemWhatsapp.ProfileWhatsappId);
                    tappedItemWhatsapp.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileWhatsapp(tappedItemWhatsapp);
                }
            }
        }

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