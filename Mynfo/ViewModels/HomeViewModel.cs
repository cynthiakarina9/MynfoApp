namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class HomeViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Box> box;
        private bool isRunning;
        private bool isNull;
        #endregion

        #region Properties
        public ObservableCollection<Box> Box
        {
            get { return this.box; }
            set { SetValue(ref this.box, value); }
        }
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }
        public bool IsNull
        {
            get
            {
                return this.isNull;
            }
            set
            {
                SetValue(ref this.isNull, value);
            }
        }
        #endregion

        #region Commands

        #endregion

        #region Contructor
        public HomeViewModel()
        {
            apiService = new ApiService();
            this.IsRunning = false;
            GetBoxDefault();
        }
        #endregion

        #region Methods
        public async Task<ObservableCollection<Box>> GetBoxDefault()
        {
            this.IsNull = false;
            Box = new ObservableCollection<Box>();
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            var BoxList = await this.apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                MainViewModel.GetInstance().User.UserId);

            if(BoxList != null)
            {
                Box.Add(BoxList); 
            }
            
            if(Box.Count != 0)
            {
                IsNull = true;
            }
            this.IsRunning = false;
            return Box;
        }

        #endregion

        #region Commands
        public ICommand GoToDetailsCommand
        {
            get
            {
                return new RelayCommand(GoToDetails);
            }
        }
        public async void GoToDetails ()
        {
            int BoxId = 0;
            foreach(Box boxCount in Box)
            {
                BoxId = boxCount.BoxId;
            }
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(BoxId);
            await App.Navigator.PushAsync(new DetailsBoxPage(BoxId));
        }
        #endregion
    }
}
