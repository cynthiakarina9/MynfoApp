using Mynfo.Domain;
using Mynfo.Helpers;
using Mynfo.Services;
using Mynfo.ViewModels;
using System;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsBoxPage : ContentPage
    {
        private ApiService apiService;

        public DetailsBoxPage(int _boxId = 0)
        {
            InitializeComponent();

            this.apiService = new ApiService();

            int BoxId = _boxId;
            int UserID = MainViewModel.GetInstance().User.UserId;
            string consultaDefault;
            string queryLastBoxCreated = "SELECT TOP 1 * FROM dbo.Boxes where dbo.Boxes.UserId = " + UserID + " ORDER BY BoxId DESC";
            string queryUpdatesetDefault;
            string queryUpdateTakeOffDefault;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb;
            String BoxName = "";
            bool BoxDefault = false;
            var BxNameEntry = new Entry();
            var BxSaveName = new Button();
            var BxDefaultCheckBox = new CheckBox();

            //Llenar BoxId si es 0
            if(BoxId == 0)
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

            //Asignación de querys
            consultaDefault = "select * from dbo.Boxes where dbo.Boxes.BoxId = " + BoxId;
            queryUpdatesetDefault = "update dbo.Boxes set BoxDefault = 1 where dbo.Boxes.UserId =" + UserID + " and dbo.Boxes.BoxId =" + BoxId;
            queryUpdateTakeOffDefault = "update dbo.Boxes set BoxDefault = 0 where dbo.Boxes.UserId =" + UserID + " and dbo.Boxes.BoxDefault = 1 and dbo.Boxes.BoxId !=" + BoxId;

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

            //Creación de Entry para colocar nombre de la box
            BxNameEntry.FontSize = 25;
            BxNameEntry.Text = BoxName;
            BxNameEntry.HorizontalTextAlignment = TextAlignment.Center;
            BxNameEntry.WidthRequest = 200;

            BoxNameEntry.Children.Add(BxNameEntry);

            //Creación de botón para actualizar nombre de la Box
            //BxSaveName.Text = "Guardar";
            BxSaveName.Text = "S";
            BxSaveName.BackgroundColor = Color.FromHex("#FF5521");
            BxSaveName.CornerRadius = 20;
            BxSaveName.HeightRequest = 40;
            BxSaveName.WidthRequest = 40;
            BxSaveName.Clicked += new EventHandler((sender, e) => UpdateBoxName(sender, e, BoxId, BxNameEntry.Text, UserID));

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
            //await Navigation.PopToRootAsync();
            Application.Current.MainPage = new MasterPage();
        }

        private async void BoxDetails_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesBYPESM = new ProfilesBYPESMViewModel();
            await Navigation.PushAsync(new ProfilesBYPESMPage());
        }

        private async void UpdateBoxName(object sender, EventArgs e, int _BoxId, string _name, int _UserId)
        {
            //Actualizar el nombre de la Box
            string queryUpdateBoxName = "update dbo.Boxes set Name = '" + _name + "' where dbo.Boxes.UserId = " + _UserId + " and dbo.Boxes.BoxId = " + _BoxId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;


        }
    }
}