namespace Mynfo.Views
{
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroductionGifPage
    {
        public IntroductionGifPage()
        {
            InitializeComponent();
        }

        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            MainViewModel.GetInstance().User.MostrarTutorial = IntroCheckBox.IsChecked;
            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                conn.Query<UserLocal>("update UserLocal set MostrarTutorial = ? where UserId = ?",
                    IntroCheckBox.IsChecked,
                    MainViewModel.GetInstance().User.UserId);
            }
        }
    }
}