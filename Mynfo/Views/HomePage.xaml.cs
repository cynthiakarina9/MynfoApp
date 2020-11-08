namespace Mynfo.Views
{
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using SQLite;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();            

            System.Text.StringBuilder sb;
            string      userId = MainViewModel.GetInstance().User.UserId.ToString();
            string      DefaultBoxName = "";
            string      consultaDefault = "select * from dbo.Boxes where dbo.Boxes.UserId = " + userId + " and dbo.Boxes.BoxDefault = 1";
            string      consultaBoxes = "select * from dbo.Boxes where dbo.Boxes.UserId = " + userId + " and dbo.Boxes.BoxDefault = 0";
            string      consultaGetBoxesNum = "select * from dbo.Boxes where Boxes.UserId = " + userId;
            string      cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            var         Default = new Button();
            var         Box2 = new Button();
            var         Box3 = new Button();
            var         Box4 = new Button();
            var         Box5 = new Button();
            var         Box6 = new Button();
            var         Box7 = new Button();
            var         Box8 = new Button();
            var         Box9 = new Button();
            var         Box10 = new Button();
            var         NoBoxes = new Label();
            string[]    boxes = new string[9];
            int[]       boxesIDs = new int[9];
            int         arrayPos = 0;
            int         DefaultBoxId = 0;
            int         BoxNum = 0;



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
                Default.Text = DefaultBoxName;
                Default.BackgroundColor = Color.FromHex("#f9a589");
                Default.CornerRadius = 70;
                Default.FontAttributes = FontAttributes.Bold;
                Default.FontSize = 20;
                Default.HeightRequest = 140;
                Default.TextColor = Color.FromHex("#fff");
                Default.WidthRequest = 140;
                Default.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender,e,DefaultBoxId));

                DefaultButton.Children.Add(Default);
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
            get_box();
            //Box 2
            if (boxes[0] != null)
            {
                //Agregamos botón con el nombre de la box
                Box2.Text = boxes[0].ToString();
                Box2.BackgroundColor = Color.LightGray;
                Box2.CornerRadius = 40;
                Box2.FontAttributes = FontAttributes.Bold;
                Box2.FontSize = 10;
                Box2.HeightRequest = 80;
                Box2.TextColor = Color.Black;
                Box2.WidthRequest = 80;
                Box2.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[0]));

                LayoutBox2.Children.Add(Box2);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box2.BackgroundColor = Color.Gray;
                Box2.CornerRadius = 40;
                Box2.HeightRequest = 80;
                Box2.WidthRequest = 80;
                Box2.IsEnabled = false;

                LayoutBox2.Children.Add(Box2);
            }

            //Box 3
            if (boxes[1] != null)
            {
                //Agregamos botón con el nombre de la box
                Box3.Text = boxes[1].ToString();
                Box3.BackgroundColor = Color.LightGray;
                Box3.CornerRadius = 40;
                Box3.FontAttributes = FontAttributes.Bold;
                Box3.FontSize = 10;
                Box3.HeightRequest = 80;
                Box3.TextColor = Color.Black;
                Box3.WidthRequest = 80;
                Box3.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[1]));

                LayoutBox3.Children.Add(Box3);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box3.BackgroundColor = Color.Gray;
                Box3.CornerRadius = 40;
                Box3.HeightRequest = 80;
                Box3.WidthRequest = 80;
                Box3.IsEnabled = false;

                LayoutBox3.Children.Add(Box3);
            }

            //Box 4
            if (boxes[2] != null)
            {
                //Agregamos botón con el nombre de la box
                Box4.Text = boxes[2].ToString();
                Box4.BackgroundColor = Color.LightGray;
                Box4.CornerRadius = 40;
                Box4.FontAttributes = FontAttributes.Bold;
                Box4.FontSize = 10;
                Box4.HeightRequest = 80;
                Box4.TextColor = Color.Black;
                Box4.WidthRequest = 80;
                Box4.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[2]));

                LayoutBox4.Children.Add(Box4);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box4.BackgroundColor = Color.Gray;
                Box4.CornerRadius = 40;
                Box4.HeightRequest = 80;
                Box4.WidthRequest = 80;
                Box4.IsEnabled = false;

                LayoutBox4.Children.Add(Box4);
            }

            //Box 5
            if (boxes[3] != null)
            {
                //Agregamos botón con el nombre de la box
                Box5.Text = boxes[3].ToString();
                Box5.BackgroundColor = Color.LightGray;
                Box5.CornerRadius = 40;
                Box5.FontAttributes = FontAttributes.Bold;
                Box5.FontSize = 10;
                Box5.HeightRequest = 80;
                Box5.TextColor = Color.Black;
                Box5.WidthRequest = 80;
                Box5.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[3]));

                LayoutBox5.Children.Add(Box5);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box5.BackgroundColor = Color.Gray;
                Box5.CornerRadius = 40;
                Box5.HeightRequest = 80;
                Box5.WidthRequest = 80;
                Box5.IsEnabled = false;

                LayoutBox5.Children.Add(Box5);
            }

            //Box 6
            if (boxes[4] != null)
            {
                //Agregamos botón con el nombre de la box
                Box6.Text = boxes[4].ToString();
                Box6.BackgroundColor = Color.LightGray;
                Box6.CornerRadius = 40;
                Box6.FontAttributes = FontAttributes.Bold;
                Box6.FontSize = 10;
                Box6.HeightRequest = 80;
                Box6.TextColor = Color.Black;
                Box6.WidthRequest = 80;
                Box6.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[4]));

                LayoutBox6.Children.Add(Box6);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box6.BackgroundColor = Color.Gray;
                Box6.CornerRadius = 40;
                Box6.HeightRequest = 80;
                Box6.WidthRequest = 80;
                Box6.IsEnabled = false;

                LayoutBox6.Children.Add(Box6);
            }

            //Box 7
            if (boxes[5] != null)
            {
                //Agregamos botón con el nombre de la box
                Box7.Text = boxes[5].ToString();
                Box7.BackgroundColor = Color.LightGray;
                Box7.CornerRadius = 40;
                Box7.FontAttributes = FontAttributes.Bold;
                Box7.FontSize = 10;
                Box7.HeightRequest = 80;
                Box7.TextColor = Color.Black;
                Box7.WidthRequest = 80;
                Box7.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[5]));

                LayoutBox7.Children.Add(Box7);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box7.BackgroundColor = Color.Gray;
                Box7.CornerRadius = 40;
                Box7.HeightRequest = 80;
                Box7.WidthRequest = 80;
                Box7.IsEnabled = false;

                LayoutBox7.Children.Add(Box7);
            }

            //Box 8
            if (boxes[6] != null)
            {
                //Agregamos botón con el nombre de la box
                Box8.Text = boxes[6].ToString();
                Box8.BackgroundColor = Color.LightGray;
                Box8.CornerRadius = 40;
                Box8.FontAttributes = FontAttributes.Bold;
                Box8.FontSize = 10;
                Box8.HeightRequest = 80;
                Box8.TextColor = Color.Black;
                Box8.WidthRequest = 80;
                Box8.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[6]));

                LayoutBox8.Children.Add(Box8);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box8.BackgroundColor = Color.Gray;
                Box8.CornerRadius = 40;
                Box8.HeightRequest = 80;
                Box8.WidthRequest = 80;
                Box8.IsEnabled = false;

                LayoutBox8.Children.Add(Box8);
            }

            //Box 9
            if (boxes[7] != null)
            {
                //Agregamos botón con el nombre de la box
                Box9.Text = boxes[7].ToString();
                Box9.BackgroundColor = Color.LightGray;
                Box9.CornerRadius = 40;
                Box9.FontAttributes = FontAttributes.Bold;
                Box9.FontSize = 10;
                Box9.HeightRequest = 80;
                Box9.TextColor = Color.Black;
                Box9.WidthRequest = 80;
                Box9.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[7]));

                LayoutBox9.Children.Add(Box9);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box9.BackgroundColor = Color.Gray;
                Box9.CornerRadius = 40;
                Box9.HeightRequest = 80;
                Box9.WidthRequest = 80;
                Box9.IsEnabled = false;

                LayoutBox9.Children.Add(Box9);
            }

            //Box 10
            if (boxes[8] != null)
            {
                //Agregamos botón con el nombre de la box
                Box10.Text = boxes[8].ToString();
                Box10.BackgroundColor = Color.LightGray;
                Box10.CornerRadius = 40;
                Box10.FontAttributes = FontAttributes.Bold;
                Box10.FontSize = 10;
                Box10.HeightRequest = 80;
                Box10.TextColor = Color.Black;
                Box10.WidthRequest = 80;
                Box10.Clicked += new EventHandler((sender, e) => BoxDetailsView(sender, e, boxesIDs[8]));

                LayoutBox10.Children.Add(Box10);
            }
            else
            {
                //Agregamos botón sin nombre y desactivado
                Box10.BackgroundColor = Color.Gray;
                Box10.CornerRadius = 40;
                Box10.HeightRequest = 80;
                Box10.WidthRequest = 80;
                Box10.IsEnabled = false;

                LayoutBox10.Children.Add(Box10);
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
            string json;
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
                    string json_header = "Box recibida correctamente!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +

                           "¡";
                    string json_body;
                    string json_value = "{"
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

    }
}