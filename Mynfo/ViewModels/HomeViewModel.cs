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
        private ObservableCollection<Box> boxNoDefault;
        private bool isRunning;
        private bool isNull;
        private bool moreOne;
        #endregion

        #region Properties
        public ObservableCollection<Box> Box
        {
            get { return this.box; }
            set { SetValue(ref this.box, value); }
        }
        public ObservableCollection<Box> BoxNoDefault
        {
            get { return this.boxNoDefault; }
            set { SetValue(ref this.boxNoDefault, value); }
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
        public bool MoreOne
        {
            get
            {
                return this.moreOne;
            }
            set
            {
                SetValue(ref this.moreOne, value);
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
            GetBoxNoDefault();
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

        public async Task<ObservableCollection<Box>> GetBoxNoDefault()
        {
            this.MoreOne = false;
            BoxNoDefault = new ObservableCollection<Box>();
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

            var BoxListNoDefault = await this.apiService.GetBoxNoDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxNoDefault",
                MainViewModel.GetInstance().User.UserId);
            foreach(Box boxes in BoxListNoDefault)
            {
                BoxNoDefault.Add(boxes);
            }

            if (BoxListNoDefault.Count != 0)
            {
                MoreOne = true;
            }
            this.IsRunning = false;
            return Box;
        } 
        //Actualizar listas
        public void AddList(Box _Boxes)
        {
            BoxNoDefault.Add(_Boxes);
        }

        public void RemoveList(Box _Boxes)
        {
            BoxNoDefault.Remove(_Boxes);
        }
        public void UpdateList(Box _Boxes)
        {
            Box Aux = new Box();
            foreach (Box B in BoxNoDefault)
            {
                if (_Boxes.BoxId == B.BoxId)
                {
                    Aux = B;
                }
            }
            int newIndex = BoxNoDefault.IndexOf(Aux);
            BoxNoDefault.Remove(Aux);

            BoxNoDefault.Insert(newIndex, _Boxes);
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
            int BoxId2 = 0;
            foreach(Box boxCount in Box)
            {
                BoxId = boxCount.BoxId;
            }
            foreach(Box BoxCountNoDefault in BoxNoDefault)
            {
                BoxId2 = BoxCountNoDefault.BoxId;
            }
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(BoxId);
            await App.Navigator.PushAsync(new DetailsBoxPage(BoxId));
        }
        public async void GoToDetailsNoDefault()
        {
            int BoxId = 0;
            foreach (Box boxCount in BoxNoDefault)
            {
                BoxId = boxCount.BoxId;
            }
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(BoxId);
            await App.Navigator.PushAsync(new DetailsBoxPage(BoxId));
        }
        #endregion
    }
}
