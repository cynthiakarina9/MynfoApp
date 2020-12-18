namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Models;
    using Mynfo.Views;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class DetailsBoxViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Prueba> testList;
        #endregion

        #region Properties
        private ObservableCollection<Prueba> TestList
        {
            get { return this.testList; }
            set { SetValue(ref this.testList, value); }
        }
        #endregion

        #region Constructor
        public DetailsBoxViewModel(int _idBox)
        {
            FillList();
        }
        #endregion

        #region Methods
        public void GetProfiles ()
        {

        }
        public void FillList ()
        {
            
        }
        #endregion
        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }

        private async void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
    }
}
