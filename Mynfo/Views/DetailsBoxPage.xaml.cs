namespace Mynfo.Views
{
    using ViewModels;
    using System;
    using System.Data.SqlClient;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsBoxPage : ContentPage
    {
        public DetailsBoxPage(int _boxId = 0)
        {
            InitializeComponent();

            int BoxId = _boxId;
            string consultaDefault = "select * from dbo.Boxes where dbo.Boxes.BoxId = " + BoxId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb;
            String BoxName = "";
            bool BoxDefault = false;
            var BxNameEntry = new Entry();
            var BxSaveName = new Button();
            var BxDefaultCheckBox = new CheckBox();

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
                }
            }

            //Creación de Entry para colocar nombre de la box
            BxNameEntry.Text = BoxName;
            BxNameEntry.FontSize = 25;

            BoxNameEntry.Children.Add(BxNameEntry);

            //Creación de botón para actualizar nombre de la Box
            //BxSaveName.Text = "Guardar";
            /*BxSaveName.Text = "S";
            BxSaveName.BackgroundColor = Color.FromHex("#FF5521");
            BxSaveName.CornerRadius = 20;
            BxSaveName.HeightRequest = 40;
            BxSaveName.WidthRequest = 40;

            BoxUpdateBtn.Children.Add(BxSaveName);*/

            //Creación del checkbox de box predeterminada
            BxDefaultCheckBox.IsChecked = BoxDefault;

            BoxDefaultCheckBox.Children.Add(BxDefaultCheckBox);

        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
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
    }
}