namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Views;
    using System.Windows.Input;

    public class ProfilesBYPESMViewModel
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand GotoProfilesCommand
        {
            get
            {
                return new RelayCommand(GotoProfiles);
            }
        }
        private void GotoProfiles()
        {
            MainViewModel.GetInstance().Profiles = new ProfilesViewModel();
            App.Navigator.PushAsync(new ProfilesPage());
        }
        #endregion

        public ProfilesBYPESMViewModel()
        {
            var mainViewModel = MainViewModel.GetInstance();
        }

        #region Methods
        
        #endregion
    }
}