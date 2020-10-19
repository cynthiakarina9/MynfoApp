namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Views;

    public class HomeViewModel
    {
        #region Properties
        public string Name { get; set; }
        
        #endregion
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

        #region Methods
        public void Botton()
        {
            //botonDefault.Name = Botton
        }

        #endregion
    }
}
