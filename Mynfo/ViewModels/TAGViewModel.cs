namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.Views;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class TAGViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Atributes
        public class CarouselModel
        {
            public string Title { get; set; }
            public string Detail { get; set; }
        }
        private ObservableCollection<CarouselModel> carouselInstructions;
        #endregion

        #region Properties
        public ObservableCollection<CarouselModel> CarouselInstructions
        {
            get { return carouselInstructions; }
            private set
            {
                SetValue(ref carouselInstructions, value);
            }
        }
        #endregion

        #region Constructor
        public TAGViewModel()
        {
            apiService = new ApiService();
            GetSteps();
        }
        #endregion

        #region Commands
        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }
        private void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }

        public ICommand GoToStoreCommand
        {
            get
            {
                return new RelayCommand(GoToStore);
            }
        }
        private async void GoToStore()
        {
            await Launcher.OpenAsync(new Uri("https://mynfo.mx/index.php/tienda/"));
        }
        #endregion

        #region Methods
        public void GetSteps()
        {
            CarouselInstructions = new ObservableCollection<CarouselModel>();

            CarouselInstructions.Add(new CarouselModel
            {
                Title = Languages.LabelStep1,
                Detail = Languages.Step1
            });
            CarouselInstructions.Add(new CarouselModel
            {
                Title = Languages.LabelStep2,
                Detail = Languages.Step2
            });
        }
        #endregion
    }
}
