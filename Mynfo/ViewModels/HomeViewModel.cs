namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Views;

    public class HomeViewModel
    {
        #region Commands
        public ICommand CreateBoxCommand
        {
            get
            {
                return new RelayCommand(CreateBox);
            }
        }
        private void CreateBox()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.BoxRegister = new BoxRegisterViewModel();
            App.Navigator.PushAsync(new BoxRegisterPage());
        }
        #endregion
    }
}
