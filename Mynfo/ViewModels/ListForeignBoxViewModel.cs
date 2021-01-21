namespace Mynfo.ViewModels
{
    using Models;
    using Mynfo.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class ListForeignBoxViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<ForeingBox> ForeingBox;
        #endregion

        #region Properties
        public ObservableCollection<ForeingBox> foreingBox
        {
            get { return ForeingBox; }
            private set
            {
                SetValue(ref ForeingBox, value);
            }
        }
        #endregion

        #region Contructor
        public ListForeignBoxViewModel(int _ForeignUserId = 0)
        {
            apiService = new ApiService();

            GetUSer(_ForeignUserId);
            GetList();
        }

        public async void GetUSer(int _ForeignUserId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            if (_ForeignUserId != 0)
            {
                var response = await this.apiService.GetUserId(
                apiSecurity,
                "/api",
                "/Users",
                _ForeignUserId);
            }
        }

        public void GetList()
        {            
            List<ForeingBox> foreignBoxList = new List<ForeingBox>();
            foreingBox = new ObservableCollection<ForeingBox>();

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                int a = conn.Table<ForeingProfile>().Count();

                foreignBoxList = conn.Table<ForeingBox>().ToList();
            }

            foreach (ForeingBox foreingBoxValue in foreignBoxList)
            {
                foreingBox.Add(foreingBoxValue);
            }
        }
        public void AddList(ForeingBox _foreingBox)
        {
            foreingBox.Add(_foreingBox);
        }
        #endregion
    }
}
