namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Rg.Plugins.Popup.Animations;
    using Rg.Plugins.Popup.Enums;
    using Rg.Plugins.Popup.Services;
    using System;
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
        private ProfileLocal profile;
        private ObservableCollection<Box> box;
        private ObservableCollection<Box> boxNoDefault;
        private string viewsByUser;
        private bool isRunning;
        private bool isNull;
        private bool moreOne;
        private ImageSource imageSource;
        private bool edadLabel;
        private bool visibleButton;
        private bool _isRefreshing;
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
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(); }
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
        public ProfileLocal Profile
        {
            get
            {
                return this.profile;
            }
            set
            {
                SetValue(ref this.profile, value);
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
        public Box selectedItem { get; set; }
        public UserLocal User
        {
            get;
            set;
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public bool EdadLabel
        {
            get { return this.edadLabel; }
            set { SetValue(ref this.edadLabel, value); }
        }
        public bool VisibleButton
        {
            get { return this.visibleButton; }
            set { SetValue(ref this.visibleButton, value); }
        }
        public string ViewsByUser
        {
            get { return this.viewsByUser; }
            set { SetValue(ref this.viewsByUser, value);  }
        }

        public ICommand RefreshCommand { private set; get; }
        #endregion

        #region Contructor
        public HomeViewModel()
        {
            apiService = new ApiService();
            this.IsRunning = false;
            EdadLabel = true;
            this.User = MainViewModel.GetInstance().User;
            if (this.User.ImageFullPath == "noimage" 
                || this.User.ImageFullPath == string.Empty 
                || this.User.ImageFullPath == null)
            {
                this.ImageSource = "no_image";
            }
            else
            {
                this.ImageSource = this.User.ImageFullPath;
            }
            if (User.Edad == 0)
            {
                EdadLabel = false;
            }
            GetBoxCount();
            GetBoxDefault();
            GetBoxNoDefault();

            ViewsByUser = Convert.ToString(Imprime_box.GetViewsByUser(MainViewModel.GetInstance().User.UserId));

            RefreshCommand = new Command(async () => await RefreshViewsByUser());
        }
        #endregion

        #region Methods
        public async Task<ObservableCollection<Box>> GetBoxDefault()
        {
            this.IsRunning = true;
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
                IsNull = false;
            }
            
            if(Box.Count == 0)
            {
                IsNull = true;
            }
            this.IsRunning = false;
            return Box;
        }

        public async Task<ObservableCollection<Box>> GetBoxNoDefault()
        {
            this.IsRunning = true;
            selectedItem = null;
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

        #region Listas
        public void AddList(Box _Boxes)
        {
            if(_Boxes.BoxDefault == false)
            {
                BoxNoDefault.Add(_Boxes);
            }
            else
            {
                Box.Add(_Boxes);
            }
        }

        public void RemoveList(Box _Boxes)
        {
            var R = new Box();
            
            if(_Boxes.BoxDefault == false)
            {
                foreach (Box B in BoxNoDefault)
                {
                    if (B == _Boxes)
                    {
                        BoxNoDefault.Remove(B);
                        return;
                    }
                }
            }
            else
            {
                foreach (Box B in Box)
                {
                    if (B == _Boxes)
                    {
                        Box.Remove(B);
                        return;
                    }
                }
            }
        }

        public void UpdateList(Box _Boxes)
        {
            Box Aux = new Box();
            if(_Boxes.BoxDefault == false)
            {
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
            else
            {
                foreach (Box B in Box)
                {
                    if (_Boxes.BoxId == B.BoxId)
                    {
                        Aux = B;
                    }
                }
                int newIndex = Box.IndexOf(Aux);
                Box.Remove(Aux);

                Box.Insert(newIndex, _Boxes);
            }
            selectedItem = null;
        }
        #endregion

        public async void GoToDetailsNoDefault()
        {
            int BoxId = 0;
            Box _Box = new Box();
            foreach (Box boxCount in BoxNoDefault)
            {
                BoxId = boxCount.BoxId;
                _Box = boxCount;
            }
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(_Box);
            //await App.Navigator.PushAsync(new DetailsBoxPage(_Box));
            await PopupNavigation.Instance.PushAsync(new DetailBoxPopUpPage(_Box));
        }

        public async Task<bool> GetBoxCount()
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var BoxCount = await this.apiService.GetBoxCount(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxCount",
                MainViewModel.GetInstance().User.UserId);
            VisibleButton = false;
            if (BoxCount <4)
            {
                VisibleButton = true;
            }
            else
            {
                VisibleButton = false;
            }
            return VisibleButton;
        }

        async Task RefreshViewsByUser()
        {
            ViewsByUser = Convert.ToString(Imprime_box.GetViewsByUser(MainViewModel.GetInstance().User.UserId));
            IsRefreshing = false;
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
            Box _Box = new Box();
            foreach(Box boxCount in Box)
            {
                _Box = boxCount;
            }
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(_Box);
            await PopupNavigation.Instance.PushAsync(new DetailBoxPopUpPage(_Box));
            selectedItem = null;
        }

        public ICommand ImageSizeCommand
        {
            get
            {
                return new RelayCommand(ImageSize);
            }
        }
        public async void ImageSize()
        {
            MainViewModel.GetInstance().ImageSize = new ImageSizeViewModel();
            var Page = new ImageSizePopUpPage();
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Top,
                PositionOut = MoveAnimationOptions.Bottom,
                ScaleIn = 1.2,
                ScaleOut = 0.8,
                DurationIn = 400,
                DurationOut = 800,
                EasingIn = Easing.BounceIn,
                EasingOut = Easing.CubicOut,
                HasBackgroundAnimation = false
            };

            Page.Animation = scaleAnimation;
            await PopupNavigation.Instance.PushAsync(Page);
        }
        #endregion
    }
}
