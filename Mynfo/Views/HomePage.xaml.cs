namespace Mynfo.Views
{
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using SQLite;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            ButtonBox.Clicked += new EventHandler((sender, e) => ChangeBoxbool(sender, e, ButtonBox.IsPressed));
            //GoToTest.Clicked += new EventHandler((sender,e) => GoToTestPage());

            System.Text.StringBuilder sb;
            string      userId = MainViewModel.GetInstance().User.UserId.ToString();
            string      DefaultBoxName = "";
            string      consultaDefault = "select * from dbo.Boxes where dbo.Boxes.UserId = " + userId + " and dbo.Boxes.BoxDefault = 1";
            string      consultaBoxes = "select * from dbo.Boxes where dbo.Boxes.UserId = " + userId + " and dbo.Boxes.BoxDefault = 0";
            string      consultaGetBoxesNum = "select * from dbo.Boxes where Boxes.UserId = " + userId;
            string      cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            var         Default = new Button();
            var         LabelDefault = new Label();
            var         Box2 = new Button();
            var         Label2 = new Label();
            var         Box3 = new Button();
            var         Label3 = new Label();
            var         Box4 = new Button();
            var         Label4 = new Label();
            var         Box5 = new Button();
            var         Label5 = new Label();
            var         Box6 = new Button();
            var         Label6 = new Label();
            var         Box7 = new Button();
            var         Label7 = new Label();
            var         Box8 = new Button();
            var         Label8 = new Label();
            var         Box9 = new Button();
            var         Label9 = new Label();
            var         Box10 = new Button();
            var         Label10 = new Label();
            var         NoBoxes = new Label();
            string[]    boxes = new string[9];
            int[]       boxesIDs = new int[9];
            int         arrayPos = 0;
            int         DefaultBoxId = 0;
            int         BoxNum = 0;


            this.CheckLocalBox();

            //Primer consulta para saber cantidad de boxes creadas
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(consultaGetBoxesNum);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BoxNum++;
                        }
                    }
                    connection.Close();
                }
            }

            //Segunda consulta para obtener box default
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
                            DefaultBoxName  = (string)reader["Name"];
                            DefaultBoxId    = (int)reader["BoxId"];
                        }
                    }
                    connection.Close();
                }
            }

            //Si no hay box mandamos mensaje de que no hay boxes creadas
            if(DefaultBoxName == "")
            {
                NoBoxes.Text = "Aún no hay boxes creadas.";
                NoBoxes.FontSize = 22;

                DefaultButton.Children.Add(NoBoxes);
            }
            else
            {
                //Agregamos botón con el nombre de la box
                //Default.Text = DefaultBoxName;
                Default.BackgroundColor = Color.FromHex("#f9a589");
                Default.CornerRadius = 10;
                Default.FontAttributes = FontAttributes.Bold;
                Default.FontSize = 20;
                Default.HeightRequest = 140;
                Default.TextColor = Color.FromHex("#fff");
                Default.WidthRequest = 140;
                Default.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender,e,DefaultBoxId));

                LabelDefault.Text = DefaultBoxName;
                LabelDefault.FontAttributes = FontAttributes.Bold;
                LabelDefault.FontSize = 20;
                LabelDefault.HorizontalTextAlignment = TextAlignment.Center;

                DefaultButton.Children.Add(Default);
                DefaultButton.Children.Add(LabelDefault);
            }

            //Tercer consulta para obtener las demás boxes
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(consultaBoxes);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            boxes[arrayPos] = (string)reader["Name"];
                            boxesIDs[arrayPos] = (int)reader["BoxId"];

                            arrayPos++;
                        }
                    }
                    connection.Close();
                }
            }

            //Llenar variable global
            get_box();

            //Box 2
            if (boxes[0] != null)
            {
                //Agregamos botón con el nombre de la box
                Box2.BackgroundColor = Color.LightGray;
                Box2.CornerRadius = 5;
                Box2.FontAttributes = FontAttributes.Bold;
                Box2.FontSize = 12;
                Box2.HeightRequest = 80;
                Box2.TextColor = Color.Black;
                Box2.WidthRequest = 80;
                Box2.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[0]));

                Label2.Text = boxes[0].ToString();
                Label2.FontAttributes = FontAttributes.Bold;
                Label2.FontSize = 12;
                Label2.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox2.Children.Add(Box2);
                LayoutBox2.Children.Add(Label2);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box2.BackgroundColor = Color.Gray;
                Box2.CornerRadius = 5;
                Box2.HeightRequest = 80;
                Box2.WidthRequest = 80;
                Box2.IsEnabled = false;

                Label2.Text = "";

                LayoutBox2.Children.Add(Box2);
                LayoutBox2.Children.Add(Label2);
            }

            //Box 3
            if (boxes[1] != null)
            {
                //Agregamos botón con el nombre de la box
                Box3.BackgroundColor = Color.LightGray;
                Box3.CornerRadius = 5;
                Box3.FontAttributes = FontAttributes.Bold;
                Box3.FontSize = 12;
                Box3.HeightRequest = 80;
                Box3.TextColor = Color.Black;
                Box3.WidthRequest = 80;
                Box3.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[1]));

                Label3.Text = boxes[1].ToString();
                Label3.FontAttributes = FontAttributes.Bold;
                Label3.FontSize = 12;
                Label3.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox3.Children.Add(Box3);
                LayoutBox3.Children.Add(Label3);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box3.BackgroundColor = Color.Gray;
                Box3.CornerRadius = 5;
                Box3.HeightRequest = 80;
                Box3.WidthRequest = 80;
                Box3.IsEnabled = false;

                Label3.Text = "";

                LayoutBox3.Children.Add(Box3);
                LayoutBox3.Children.Add(Label3);
            }

            //Box 4
            if (boxes[2] != null)
            {
                //Agregamos botón con el nombre de la box
                Box4.BackgroundColor = Color.LightGray;
                Box4.CornerRadius = 5;
                Box4.FontAttributes = FontAttributes.Bold;
                Box4.FontSize = 12;
                Box4.HeightRequest = 80;
                Box4.TextColor = Color.Black;
                Box4.WidthRequest = 80;
                Box4.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[2]));

                Label4.Text = boxes[2].ToString();
                Label4.FontAttributes = FontAttributes.Bold;
                Label4.FontSize = 12;
                Label4.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox4.Children.Add(Box4);
                LayoutBox4.Children.Add(Label4);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box4.BackgroundColor = Color.Gray;
                Box4.CornerRadius = 5;
                Box4.HeightRequest = 80;
                Box4.WidthRequest = 80;
                Box4.IsEnabled = false;

                Label4.Text = "";

                LayoutBox4.Children.Add(Box4);
                LayoutBox4.Children.Add(Label4);
            }

            //Box 5
            if (boxes[3] != null)
            {
                //Agregamos botón con el nombre de la box
                Box5.BackgroundColor = Color.LightGray;
                Box5.CornerRadius = 5;
                Box5.FontAttributes = FontAttributes.Bold;
                Box5.FontSize = 12;
                Box5.HeightRequest = 80;
                Box5.TextColor = Color.Black;
                Box5.WidthRequest = 80;
                Box5.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[3]));

                Label5.Text = boxes[3].ToString();
                Label5.FontAttributes = FontAttributes.Bold;
                Label5.FontSize = 12;
                Label5.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox5.Children.Add(Box5);
                LayoutBox5.Children.Add(Label5);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box5.BackgroundColor = Color.Gray;
                Box5.CornerRadius = 5;
                Box5.HeightRequest = 80;
                Box5.WidthRequest = 80;
                Box5.IsEnabled = false;

                Label5.Text = "";

                LayoutBox5.Children.Add(Box5);
                LayoutBox5.Children.Add(Label5);
            }

            //Box 6
            if (boxes[4] != null)
            {
                //Agregamos botón con el nombre de la box
                Box6.BackgroundColor = Color.LightGray;
                Box6.CornerRadius = 5;
                Box6.FontAttributes = FontAttributes.Bold;
                Box6.FontSize = 12;
                Box6.HeightRequest = 80;
                Box6.TextColor = Color.Black;
                Box6.WidthRequest = 80;
                Box6.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[4]));

                Label6.Text = boxes[4].ToString();
                Label6.FontAttributes = FontAttributes.Bold;
                Label6.FontSize = 12;
                Label6.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox6.Children.Add(Box6);
                LayoutBox6.Children.Add(Label6);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box6.BackgroundColor = Color.Gray;
                Box6.CornerRadius = 5;
                Box6.HeightRequest = 80;
                Box6.WidthRequest = 80;
                Box6.IsEnabled = false;

                Label6.Text = "";

                LayoutBox6.Children.Add(Box6);
                LayoutBox6.Children.Add(Label6);
            }

            //Box 7
            if (boxes[5] != null)
            {
                //Agregamos botón con el nombre de la box
                Box7.BackgroundColor = Color.LightGray;
                Box7.CornerRadius = 5;
                Box7.FontAttributes = FontAttributes.Bold;
                Box7.FontSize = 12;
                Box7.HeightRequest = 80;
                Box7.TextColor = Color.Black;
                Box7.WidthRequest = 80;
                Box7.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[5]));

                Label7.Text = boxes[5].ToString();
                Label7.FontAttributes = FontAttributes.Bold;
                Label7.FontSize = 12;
                Label7.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox7.Children.Add(Box7);
                LayoutBox7.Children.Add(Label7);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box7.BackgroundColor = Color.Gray;
                Box7.CornerRadius = 5;
                Box7.HeightRequest = 80;
                Box7.WidthRequest = 80;
                Box7.IsEnabled = false;

                Label7.Text = "";

                LayoutBox7.Children.Add(Box7);
                LayoutBox7.Children.Add(Label7);
            }

            //Box 8
            if (boxes[6] != null)
            {
                //Agregamos botón con el nombre de la box
                Box8.BackgroundColor = Color.LightGray;
                Box8.CornerRadius = 5;
                Box8.FontAttributes = FontAttributes.Bold;
                Box8.FontSize = 12;
                Box8.HeightRequest = 80;
                Box8.TextColor = Color.Black;
                Box8.WidthRequest = 80;
                Box8.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[6]));

                Label8.Text = boxes[6].ToString();
                Label8.FontAttributes = FontAttributes.Bold;
                Label8.FontSize = 12;
                Label8.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox8.Children.Add(Box8);
                LayoutBox8.Children.Add(Label8);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box8.BackgroundColor = Color.Gray;
                Box8.CornerRadius = 5;
                Box8.HeightRequest = 80;
                Box8.WidthRequest = 80;
                Box8.IsEnabled = false;

                Label8.Text = "";

                LayoutBox8.Children.Add(Box8);
                LayoutBox8.Children.Add(Label8);
            }

            //Box 9
            if (boxes[7] != null)
            {
                //Agregamos botón con el nombre de la box
                Box9.BackgroundColor = Color.LightGray;
                Box9.CornerRadius = 5;
                Box9.FontAttributes = FontAttributes.Bold;
                Box9.FontSize = 12;
                Box9.HeightRequest = 80;
                Box9.TextColor = Color.Black;
                Box9.WidthRequest = 80;
                Box9.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[7]));

                Label9.Text = boxes[7].ToString();
                Label9.FontAttributes = FontAttributes.Bold;
                Label9.FontSize = 12;
                Label9.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox9.Children.Add(Box9);
                LayoutBox9.Children.Add(Label9);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box9.BackgroundColor = Color.Gray;
                Box9.CornerRadius = 5;
                Box9.HeightRequest = 80;
                Box9.WidthRequest = 80;
                Box9.IsEnabled = false;

                Label9.Text = "";

                LayoutBox9.Children.Add(Box9);
                LayoutBox9.Children.Add(Label9);
            }

            //Box 10
            if (boxes[8] != null)
            {
                //Agregamos botón con el nombre de la box
                Box10.BackgroundColor = Color.LightGray;
                Box10.CornerRadius = 5;
                Box10.FontAttributes = FontAttributes.Bold;
                Box10.FontSize = 12;
                Box10.HeightRequest = 80;
                Box10.TextColor = Color.Black;
                Box10.WidthRequest = 80;
                Box10.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[8]));

                Label10.Text = boxes[8].ToString();
                Label10.FontAttributes = FontAttributes.Bold;
                Label10.FontSize = 12;
                Label10.HorizontalTextAlignment = TextAlignment.Center;

                LayoutBox10.Children.Add(Box10);
                LayoutBox10.Children.Add(Label10);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box10.BackgroundColor = Color.Gray;
                Box10.CornerRadius = 5;
                Box10.HeightRequest = 80;
                Box10.WidthRequest = 80;
                Box10.IsEnabled = false;

                Label10.Text = "";

                LayoutBox10.Children.Add(Box10);
                LayoutBox10.Children.Add(Label10);
            }

            //Validamos que podamos crear boxes nuevas
            if(BoxNum == 10)
            {
                CreateBoxBtn.IsVisible = false;
                CreateBoxBtn.IsEnabled = false;
            }
            else if(BoxNum < 10)
            {
                CreateBoxBtn.IsVisible = true;
                CreateBoxBtn.IsEnabled = true;
            }            
        }

        public void get_box()
        {
            string json = "";
            try
            {
                var Profile = new ProfileLocal();
                var Profile_1 = new ProfileLocal();
                var Box_Local = new BoxLocal();
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    Profile_1 = conn.Table<ProfileLocal>().FirstOrDefault();
                    Box_Local = conn.Table<BoxLocal>().FirstOrDefault();
                    int coun = conn.Table<ProfileLocal>().Count();
                    string json_header = null;
                    string json_body = null;
                    string json_value = null;

                    json_header = "Box recibida correctamente!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +

                           "¡";                    
                    json_value = "{"
                              + @"""BoxId"":""" + Box_Local.BoxId + @""",
                                ""Name"":""" + Box_Local.Name + @""",
                                ""BoxDefault"":""" + Box_Local.BoxDefault + @""",
                                ""UserId"":""" + Box_Local.UserId + @""",
                                ""Time"":""" + Box_Local.Time + @""",
                                ""ImagePath"":""" + Box_Local.ImagePath + @""",
                                ""UserTypeId"":""" + Box_Local.UserTypeId + @""",
                                ""FirstName"":""" + Box_Local.FirstName + @""",
                                ""LastName"":""" + Box_Local.LastName + @""",
                                ""ImageFullPath"":""" + Box_Local.ImageFullPath + @""",
                                ""FullName"":""" + Box_Local.FullName + @""",
                                ""ProfileLocalId"":""" + Profile_1.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile_1.IdBox + @""",
                                ""UserId_p"":""" + Profile_1.UserId + @""",
                                ""ProfileName"":""" + Profile_1.ProfileName + @""",
                                ""value"":""" + Profile_1.value + @""",
                                ""ProfileType"":""" + Profile_1.ProfileType + @"""                                                              
                                }";

                    if (coun > 1)
                    {
                        for (int i = 1; i < coun; i++)
                        {
                            Profile = conn.Table<ProfileLocal>().ElementAt(i);

                            json_body = "{"
                              + @"""BoxId"":""" + Box_Local.BoxId + @""",
                                ""Name"":""" + Box_Local.Name + @""",
                                ""BoxDefault"":""" + Box_Local.BoxDefault + @""",
                                ""UserId"":""" + Box_Local.UserId + @""",
                                ""Time"":""" + Box_Local.Time + @""",
                                ""ImagePath"":""" + Box_Local.ImagePath + @""",
                                ""UserTypeId"":""" + Box_Local.UserTypeId + @""",
                                ""FirstName"":""" + Box_Local.FirstName + @""",
                                ""LastName"":""" + Box_Local.LastName + @""",
                                ""ImageFullPath"":""" + Box_Local.ImageFullPath + @""",
                                ""FullName"":""" + Box_Local.FullName + @""",
                                ""ProfileLocalId"":""" + Profile.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile.IdBox + @""",
                                ""UserId_p"":""" + Profile.UserId + @""",
                                ""ProfileName"":""" + Profile.ProfileName + @""",
                                ""value"":""" + Profile.value + @""",
                                ""ProfileType"":""" + Profile.ProfileType + @"""                                                              
                                }";

                            json_value = json_value + ",\n" + json_body;
                        }
                        json_value = "[" + json_value + "]";
                    }
                    json = json_value;
                }
            }
            catch (Exception exx)
            {
                Console.Write(exx);
                json = null;
            }
            Data_ntc.data_value = null;
            Data_ntc.data_value = json;
        }        

        //Ir hacia detalles de la box
        private void BoxDetailsView(object sender, EventArgs e, int _BoxId)
        {
            Application.Current.MainPage = new NavigationPage(new DetailsBoxPage(_BoxId));
        }

        private async void CreateBox_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.BoxRegister = new BoxRegisterViewModel();
            await Navigation.PushAsync(new BoxRegisterPage());

            //var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.BoxRegister = new BoxRegisterViewModel();
            //Application.Current.MainPage = new NavigationPage(new TabbedPage1());

            //await Launcher.OpenAsync(new Uri("fb://page/100000686899395"));
            //await Launcher.OpenAsync(new Uri("https://twitter.com/RToachee"));
            //await Launcher.OpenAsync(new Uri("instagram:page_id//user?username=rodritoachee"));
            //await Launcher.OpenAsync(new Uri("mailto:rrodriguez@atx.com"));
        }

        private async void ForeingBoxes_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ListForeignBox = new ListForeignBoxViewModel();
            await Navigation.PushAsync(new ListForeignBoxPage());

        }

        private void CheckLocalBox()
        {
            BoxLocal boxLocal = new BoxLocal();
            bool valBoxLocal = false;
            bool valProfileLocal = false;

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
                string queryToGetBoxDefault = "select * from dbo.Boxes where dbo.boxes.UserId = " 
                                                + MainViewModel.GetInstance().User.UserId
                                                + " and dbo.Boxes.BoxDefault = 1";
                StringBuilder sb;
                var resultBoxLocal = conn.GetTableInfo("BoxLocal");
                var resulForeingBox = conn.GetTableInfo("ForeingBox");
                var resultForeingProfiles = conn.GetTableInfo("ForeingProfile");

                if(resulForeingBox.Count == 0)
                {
                    conn.CreateTable<ForeingBox>();

                    if (resultForeingProfiles.Count == 0)
                    {
                        conn.CreateTable <ForeingProfile>();
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
                    if(resultProfileLocal.Count == 0)
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
                    if(boxLocal.BoxId != 0)
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
                        if(conn.Table<ProfileLocal>().Count() > 0)
                        {
                            valProfileLocal = true;
                        }
                    }

                    if(valBoxLocal == true && valProfileLocal == true)
                    {
                        this.get_box();
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
                            this.get_box();
                        }
                    }
                }

            }
        }
        private void ChangeBoxbool(object sender, EventArgs e, bool pressed)
        {
            if (pressed == true)
            {
                //ButtonBox.Source = "logo_superior.png";
            }
            else
            {
                //ButtonBox.Source = "logo_superior2.png";
            }
        }

        private void GoToTestPage()
        {
            Navigation.PushAsync(new Testing());
            //Application.Current.MainPage = new NavigationPage(new Testing());
        }

    }
}