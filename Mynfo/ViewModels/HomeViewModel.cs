namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

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

        public ICommand PruebaPerfilCommand
        {
            get
            {
                return new RelayCommand(PruebaPerfil);
            }
        }
        private void PruebaPerfil()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
            App.Navigator.PushAsync(new CreateProfilePhonePage());
        }
        #endregion

        #region Contructor
        //public HomeViewModel()
        //{
        //    var Default = new Button
        //    {
        //        Text = "Pepillo"
        //    };
        //    DefaultButton

        //}
        #endregion
        #region Methods
        public void Botton()
        {
            //botonDefault.Name = Botton
        }

        #endregion
    }
}
