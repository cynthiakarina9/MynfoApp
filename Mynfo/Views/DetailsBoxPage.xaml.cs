using Mynfo.Resources;
using Mynfo.ViewModels;
using System;
using System.Data.SqlClient;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsBoxPage : ContentPage
    {

        public Entry BxNameEntry = new Entry();
        public DetailsBoxPage(int _boxId = 0)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            int BoxId = _boxId;
            int UserID = MainViewModel.GetInstance().User.UserId;
            string consultaDefault;
            string queryLastBoxCreated = "SELECT TOP 1 * FROM dbo.Boxes where dbo.Boxes.UserId = " + UserID + " ORDER BY BoxId DESC";
            string queryUpdatesetDefault;
            string queryUpdateTakeOffDefault;
            string queryGetPhones;
            string queryGetEmails;
            string queryGetSMProfiles;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb;
            String BoxName = "";
            bool BoxDefault = false;
            var BxSaveName = new ImageButton();
            var BxBtnDelete = new ImageButton();
            var bxBtnHome = new ImageButton();
            var BxDefaultCheckBox = new CheckBox();
            int listProfileNum = 0;

            //Llenar BoxId si es 0
            if (BoxId == 0)
            {
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryLastBoxCreated);

                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BoxId = (int)reader["BoxId"];
                            }
                        }

                        connection.Close();
                    }
                }
            }

            //Navegación a ventana de perfiles
            BoxProfiles.Clicked += new EventHandler((sender, e) => BoxDetails_Clicked(sender, e, BoxId));

            //Asignación de querys
            consultaDefault = "select * from dbo.Boxes where dbo.Boxes.BoxId = " + BoxId;
            queryUpdatesetDefault = "update dbo.Boxes set BoxDefault = 1 where dbo.Boxes.UserId =" + UserID + " and dbo.Boxes.BoxId =" + BoxId;
            queryUpdateTakeOffDefault = "update dbo.Boxes set BoxDefault = 0 where dbo.Boxes.UserId =" + UserID + " and dbo.Boxes.BoxDefault = 1 and dbo.Boxes.BoxId !=" + BoxId;
            queryGetPhones = "select dbo.Boxes.BoxId, dbo.ProfilePhones.ProfilePhoneId, dbo.ProfilePhones.Name, " + 
                             "dbo.ProfilePhones.Number from dbo.Box_ProfilePhone Join dbo.Boxes " +
                             "on(dbo.Boxes.BoxId = dbo.Box_ProfilePhone.BoxId) "+
                             "Join dbo.ProfilePhones on(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) "+
                             "where dbo.Boxes.BoxId = " + BoxId;
            queryGetEmails = "select dbo.Boxes.BoxId, dbo.ProfileEmails.ProfileEmailId, dbo.ProfileEmails.Name, " +
                              "dbo.ProfileEmails.Email from dbo.Box_ProfileEmail " +
                              "Join dbo.Boxes on(dbo.Boxes.BoxId = dbo.Box_ProfileEmail.BoxId) " +
                              "Join dbo.ProfileEmails on(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                              "where dbo.Boxes.BoxId = " + BoxId;
            queryGetSMProfiles = "select * from dbo.ProfileSMs where dbo.ProfileSMs.UserId = " + UserID;

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
                        }
                    }

                    connection.Close();
                }
            }

            //Definir color de fondo con respecto a si la box es predeterminada
            if(BoxDefault == true)
            {
                FullBackGround.BackgroundColor = Color.FromHex("#FFAB8F");
                bxBtnHome.BackgroundColor = Color.FromHex("#FFAB8F");
                BxSaveName.BackgroundColor = Color.FromHex("#FFAB8F");
                BxBtnDelete.BackgroundColor = Color.FromHex("#FFAB8F");
            }
            else
            {
                FullBackGround.BackgroundColor = Color.FromHex("#AAAAAA");
                bxBtnHome.BackgroundColor = Color.FromHex("#AAAAAA");
                BxSaveName.BackgroundColor = Color.FromHex("#AAAAAA");
                BxBtnDelete.BackgroundColor = Color.FromHex("#AAAAAA");
            }

            //Creación del botón para volver a home
            bxBtnHome.Source = "back.png";
            bxBtnHome.WidthRequest = 50;
            bxBtnHome.HeightRequest = 50;
            bxBtnHome.Clicked += ToolbarItem_Clicked;

            HomeButton.Children.Add(bxBtnHome);

            //Creación de botón para borrar box
            BxBtnDelete.Source = "trash.png";
            BxBtnDelete.WidthRequest = 50;
            BxBtnDelete.HeightRequest = 50;
            BxBtnDelete.Clicked += new EventHandler((sender, e) => deleteBox(sender, e, BoxId, UserID, BoxDefault));

            DeleteButton.Children.Add(BxBtnDelete);

            //Creación de Entry para colocar nombre de la box
            BxNameEntry.FontSize = 25;
            BxNameEntry.Text = BoxName;
            BxNameEntry.HorizontalTextAlignment = TextAlignment.Center;
            BxNameEntry.WidthRequest = 200;
            BxNameEntry.TextColor = Color.FromHex("#FF5521");
            BxNameEntry.FontAttributes = FontAttributes.Bold;
            BxNameEntry.IsReadOnly = true;

            BoxNameEntry.Children.Add(BxNameEntry);

            //Creación de botón para actualizar nombre de la Box
            BxSaveName.Source = "edit2.png";
            BxSaveName.HeightRequest = 25;
            BxSaveName.WidthRequest = 25;
            BxSaveName.Clicked += new EventHandler((sender, e) => UpdateBoxName(sender, e, BoxId, BxNameEntry.Text, UserID, BxNameEntry.IsReadOnly));

            BoxUpdateBtn.Children.Add(BxSaveName);

            //Creación del checkbox de box predeterminada
            BxDefaultCheckBox.IsChecked = BoxDefault;
            if(BoxDefault == true)
            {
                BxDefaultCheckBox.IsEnabled = false;
            }
            else
            {
                BxDefaultCheckBox.IsEnabled = true;
            }
            BxDefaultCheckBox.CheckedChanged += CheckDefaultBox;

            BoxDefaultCheckBox.Children.Add(BxDefaultCheckBox);

            //**********************************************************
            //AGREGACIÓN DE PERFILES A BOX
            //**********************************************************

            //Consulta para obtener Teléfonos
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
                            var phoneIcon = new ImageButton();
                            var phoneName = new Label();
                            var deleteProfile = new ImageButton();
                            var Line = new BoxView();
                            int PhoneId = (int)reader["ProfilePhoneId"];

                            phoneIcon.Source = "tel3.png";
                            phoneIcon.WidthRequest = 50;
                            phoneIcon.HeightRequest = 50;
                            phoneIcon.HorizontalOptions = LayoutOptions.Center;

                            phoneName.Text = (string)reader["Name"];
                            phoneName.FontSize = 15;
                            phoneName.HorizontalTextAlignment = TextAlignment.Center;
                            phoneName.FontAttributes = FontAttributes.Bold;
                            phoneName.TextColor = Color.Black;

                            deleteProfile.Source = "trash2.png";
                            deleteProfile.BackgroundColor = Color.FromHex("#f9a589");
                            deleteProfile.CornerRadius = 15;
                            deleteProfile.HeightRequest = 30;
                            deleteProfile.WidthRequest = 30;
                            deleteProfile.HorizontalOptions = LayoutOptions.End;
                            deleteProfile.Clicked += new EventHandler((sender, e) => DeleteBoxPhone(sender, e, BoxId, PhoneId));

                            //Definir color de fondo de ícono de basura con respecto a si la box es predeterminada
                            if (BoxDefault == true)
                            {
                                deleteProfile.BackgroundColor = Color.FromHex("#FFAB8F");
                                phoneIcon.BackgroundColor = Color.FromHex("#FFAB8F");
                            }
                            else
                            {
                                deleteProfile.BackgroundColor = Color.FromHex("#AAAAAA");
                                phoneIcon.BackgroundColor = Color.FromHex("#AAAAAA");
                            }

                            //Asignación de caja en columnas
                            switch(listProfileNum)
                            {
                                case 0:
                                    listProfileNum = 2;

                                    ProfilesList1.Children.Add(phoneIcon);
                                    ProfilesList1.Children.Add(phoneName);
                                    ProfilesList1.Children.Add(deleteProfile);
                                    break;

                                case 1:
                                    listProfileNum = 2;

                                    ProfilesList1.Children.Add(phoneIcon);
                                    ProfilesList1.Children.Add(phoneName);
                                    ProfilesList1.Children.Add(deleteProfile);
                                    break;

                                case 2:
                                    listProfileNum = 3;

                                    ProfilesList2.Children.Add(phoneIcon);
                                    ProfilesList2.Children.Add(phoneName);
                                    ProfilesList2.Children.Add(deleteProfile);
                                    break;

                                case 3:
                                    listProfileNum = 1;

                                    ProfilesList3.Children.Add(phoneIcon);
                                    ProfilesList3.Children.Add(phoneName);
                                    ProfilesList3.Children.Add(deleteProfile);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    connection.Close();
                }
            }

            //Consulta para obtener Emails
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
                            var emailIcon = new ImageButton();
                            var emailProfile = new Label();
                            var deleteProfile = new ImageButton();
                            var Line = new BoxView();
                            int EmailId = (int)reader["ProfileEmailId"];

                            emailIcon.Source = "mail1.png";
                            emailIcon.WidthRequest = 50;
                            emailIcon.HeightRequest = 50;
                            emailIcon.HorizontalOptions = LayoutOptions.Center;

                            emailProfile.Text = (string)reader["Name"];
                            emailProfile.FontSize = 15;
                            emailProfile.HorizontalTextAlignment = TextAlignment.Center;
                            emailProfile.FontAttributes = FontAttributes.Bold;
                            emailProfile.TextColor = Color.Black;

                            deleteProfile.Source = "trash2.png";
                            deleteProfile.BackgroundColor = Color.FromHex("#f9a589");
                            deleteProfile.CornerRadius = 15;
                            deleteProfile.HeightRequest = 30;
                            deleteProfile.WidthRequest = 30;
                            deleteProfile.HorizontalOptions = LayoutOptions.End;
                            deleteProfile.Clicked += new EventHandler((sender, e) => DeleteBoxEmail(sender, e, BoxId, EmailId));

                            //Definir color de fondo de ícono de basura con respecto a si la box es predeterminada
                            if (BoxDefault == true)
                            {
                                deleteProfile.BackgroundColor = Color.FromHex("#FFAB8F");
                                emailIcon.BackgroundColor = Color.FromHex("#FFAB8F");
                            }
                            else
                            {
                                deleteProfile.BackgroundColor = Color.FromHex("#AAAAAA");
                                emailIcon.BackgroundColor = Color.FromHex("#AAAAAA");
                            }

                            //Asignación de caja en columnas
                            switch (listProfileNum)
                            {
                                case 0:
                                    listProfileNum = 2;

                                    ProfilesList1.Children.Add(emailIcon);
                                    ProfilesList1.Children.Add(emailProfile);
                                    ProfilesList1.Children.Add(deleteProfile);
                                    break;

                                case 1:
                                    listProfileNum = 2;

                                    ProfilesList1.Children.Add(emailIcon);
                                    ProfilesList1.Children.Add(emailProfile);
                                    ProfilesList1.Children.Add(deleteProfile);
                                    break;

                                case 2:
                                    listProfileNum = 3;

                                    ProfilesList2.Children.Add(emailIcon);
                                    ProfilesList2.Children.Add(emailProfile);
                                    ProfilesList2.Children.Add(deleteProfile);
                                    break;

                                case 3:
                                    listProfileNum = 1;

                                    ProfilesList3.Children.Add(emailIcon);
                                    ProfilesList3.Children.Add(emailProfile);
                                    ProfilesList3.Children.Add(deleteProfile);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    connection.Close();
                }
            }

            //Marcar o desmarcar la box predeterminada
            void CheckDefaultBox(object sender, EventArgs e)
            {
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

                BxDefaultCheckBox.IsEnabled = false;
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }

        async void deleteBox(object sender, EventArgs e, int _BoxId, int _UserId, bool _BoxDefault)
        {
            string  sqlDeleteEmails = "delete from dbo.Box_ProfileEmail where dbo.Box_ProfileEmail.BoxId = " + _BoxId, 
                    sqlDeletePhones = "delete from dbo.Box_ProfilePhone where dbo.Box_ProfilePhone.BoxId = " + _BoxId, 
                    sqlDeleteSMProfiles = "delete from dbo.Box_ProfileSM where dbo.Box_ProfileSM.BoxId = " + _BoxId, 
                    sqlDeleteBox = "delete from dbo.Boxes where dbo.boxes.BoxId = " + _BoxId;
            string  cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;
            string sql;

            bool answer = await DisplayAlert(Resource.Warning, Resource.DeleteBoxNotification, Resource.Yes, Resource.No);

            if(answer == true)
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

                    //Borrar box
                    sb = new System.Text.StringBuilder();
                    sb.Append(sqlDeleteBox);
                    sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    if(_BoxDefault == true)
                    {
                        string sqlUpdateBoxDefault = "update top (1) dbo.Boxes set BoxDefault = 1 where dbo.Boxes.UserId = " + _UserId;
                        
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
                    }
                }

                //Regresar a home
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Home = new HomeViewModel();
                Application.Current.MainPage = new MasterPage();
            }
        }

        private void BoxDetails_Clicked(object sender, EventArgs e, int _BoxId)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesBYPESM = new ProfilesBYPESMViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId));
        }

        private void UpdateBoxName(object sender, EventArgs e, int _BoxId, string _name, int _UserId, bool disabled)
        {
            if(disabled == true)
            {
                BxNameEntry.IsReadOnly = false;
                BxNameEntry.TextColor = Color.Black;
            }
            else
            {
                //Actualizar el nombre de la Box
                string queryUpdateBoxName = "update dbo.Boxes set Name = '" + _name + "' where dbo.Boxes.UserId = " + _UserId + " and dbo.Boxes.BoxId = " + _BoxId;
                string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
                StringBuilder sb;

                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    sb = new System.Text.StringBuilder();
                    sb.Append(queryUpdateBoxName);
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                BxNameEntry.IsReadOnly = true;
                BxNameEntry.TextColor = Color.FromHex("#FF5521");
            }
        }
        private void DeleteBoxPhone(object sender, EventArgs e, int _BoxId, int _PhoneId)
        {
            //Borrar la relación de la box con el teléfono
            string queryUpdateBoxName = "delete from dbo.Box_ProfilePhone where dbo.Box_ProfilePhone.BoxId = " + _BoxId + " and dbo.Box_ProfilePhone.ProfilePhoneId = " + _PhoneId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryUpdateBoxName);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new DetailsBoxPage(_BoxId));
        }

        private void DeleteBoxEmail(object sender, EventArgs e, int _BoxId, int _EmailId)
        {
            //Borrar la relación de la box con el correo
            string queryUpdateBoxName = "delete from dbo.Box_ProfileEmail where dbo.Box_ProfileEmail.BoxId = " + _BoxId + " and dbo.Box_ProfileEmail.ProfileEmailId = " + _EmailId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryUpdateBoxName);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new DetailsBoxPage(_BoxId));
        }


    }
}