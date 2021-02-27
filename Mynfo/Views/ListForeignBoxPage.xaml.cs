namespace Mynfo.Views
{
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using Rg.Plugins.Popup.Services;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListForeignBoxPage : ContentPage
    {
        #region Contructor
        public ListForeignBoxPage()
        {           
            InitializeComponent();
        }
        #endregion

        #region Commands
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ForeingBox selectedItem = e.SelectedItem as ForeingBox;
            ForeignBoxList.SelectedItem = null;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ForeingBox tappedItem = e.Item as ForeingBox;
            MainViewModel.GetInstance().ForeingBox = new ForeingBoxViewModel(tappedItem);
            PopupNavigation.Instance.PushAsync(new ForeingBoxPage(tappedItem));

        }
        #endregion

        //#region Methods
        //public async void GetUSer(int _ForeignUserId)
        //{
        //    var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
        //    if (_ForeignUserId != 0)
        //    {
        //        var response = await this.apiService.GetUserId(
        //        apiSecurity,
        //        "/api",
        //        "/Users",
        //        _ForeignUserId);
        //    }
        //}

        //public void GetList()
        //{
        //    List<ForeingBox> foreignBoxList = new List<ForeingBox>();
        //    foreingBox = new List<ForeingBox>();

        //    using (var conn = new SQLite.SQLiteConnection(App.root_db))
        //    {
        //        int a = conn.Table<ForeingProfile>().Count();

        //        foreignBoxList = conn.Table<ForeingBox>().ToList();
        //    }

        //    foreach (ForeingBox foreingBoxValue in foreignBoxList)
        //    {
        //        foreingBox.Add(foreingBoxValue);
        //    }
        //}
        //#endregion
    }
}