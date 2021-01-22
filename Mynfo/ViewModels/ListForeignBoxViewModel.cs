﻿namespace Mynfo.ViewModels
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
        private ObservableCollection<ForeingBox> foreingBox;
        #endregion

        #region Properties
        public ObservableCollection<ForeingBox> ForeingBox
        {
            get { return foreingBox; }
            private set
            {
                SetValue(ref foreingBox, value);
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
            ForeingBox = new ObservableCollection<ForeingBox>();

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                int a = conn.Table<ForeingProfile>().Count();

                foreignBoxList = conn.Table<ForeingBox>().ToList();
            }

            foreach (ForeingBox foreingBoxValue in foreignBoxList)
            {
                ForeingBox.Add(foreingBoxValue);
            }
        }
        public void AddList(ForeingBox _foreingBox)
        {
            ForeingBox.Add(_foreingBox);
        }
        #endregion
    }
}
