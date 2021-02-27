namespace Mynfo.Views
{
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using System.Data.SqlClient;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1
    {

        #region Properties
        public int NetworksQty{ get; set; }
        #endregion

        public TabbedPage1()
        {
            InitializeComponent();
            NetworksQty = 0;
            //ReloadConnections();
            On<Android>().SetToolbarPlacement(Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            OSAppTheme currentTheme = App.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Logosuperior.Source = "logo_superior2.png";
                BackG.BackgroundColor = Color.FromHex("#222b3a");
            }
            else
            {
                Logosuperior.Source = "logo_superior3.png";
                BackG.BackgroundColor = Color.FromHex("#FFFFFF");
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Profiles = new ProfilesViewModel();
            mainViewModel.ListForeignBox = new ListForeignBoxViewModel();

            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));
            BackgroundColor = Color.Transparent;
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                Children.Add(new ProfilesPage { IconImageSource = "Networks_icon" });
                Children.Add(new HomePage { IconImageSource = "Home_icon" });
                Children.Add(new ListForeignBoxPage { IconImageSource = "Connections_icon" });
            }
            else if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                Children.Add(new ProfilesPage {IconImageSource = "networks_icon.png"});
                Children.Add(new HomePage { IconImageSource = "home_icon.png" });
                Children.Add(new ListForeignBoxPage { IconImageSource = "connections_icon.png" });
            }

            NetworksQty += GetEmailQty();
            NetworksQty += GetPhoneQty();
            NetworksQty += GetWhatsAppQty();
            NetworksQty += GetSocialMediaQty();


            if (NetworksQty > 1)
            {
                CurrentPage = Children[1];
            }
            else
            {
                CurrentPage = Children[0];
            }
        }


        private int GetEmailQty()
        {
            System.Text.StringBuilder sb;
            int userId = MainViewModel.GetInstance().User.UserId;
            string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            string queryGetProfiles = "select * from dbo.ProfileEmails where dbo.ProfileEmails.UserId = " + userId;
            int netQty = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetProfiles);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            netQty++;
                        }
                    }
                    connection.Close();
                }
            }
            return netQty;
        }

        private int GetPhoneQty()
        {
            System.Text.StringBuilder sb;
            int userId = MainViewModel.GetInstance().User.UserId;
            string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            string queryGetProfiles = "select * from dbo.ProfilePhones where dbo.ProfilePhones.UserId = " + userId;
            int netQty = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetProfiles);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            netQty++;
                        }
                    }
                    connection.Close();
                }
            }
            return netQty;
        }

        private int GetWhatsAppQty()
        {
            System.Text.StringBuilder sb;
            int userId = MainViewModel.GetInstance().User.UserId;
            string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            string queryGetProfiles = "select * from dbo.ProfileWhatsapps where dbo.ProfileWhatsapps.UserId = " + userId;
            int netQty = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetProfiles);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            netQty++;
                        }
                    }
                    connection.Close();
                }
            }
            return netQty;
        }

        private int GetSocialMediaQty()
        {
            System.Text.StringBuilder sb;
            int userId = MainViewModel.GetInstance().User.UserId;
            string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            string queryGetProfiles = "select * from dbo.ProfileSMs where dbo.ProfileSMs.UserId = " + userId;
            int netQty = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetProfiles);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            netQty++;
                        }
                    }
                    connection.Close();
                }
            }
            return netQty;
        }

        /// <summary>
        /// Sirve para recargar las conexiones del usuario en home
        /// </summary>
        public static async void ReloadConnections()
        {
            var apiService = new ApiService();
            var apiSecurity = Xamarin.Forms.Application.Current.Resources["APISecurity"].ToString();
            var user = await apiService.GetUserId(apiSecurity,
                                                "/api",
                                                "/Users",
                                                MainViewModel.GetInstance().User.UserId);

            MainViewModel.GetInstance().User.Conexiones = user.Conexiones;
        }

    }
}