namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Views;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class MyExternalProfileViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Properties
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

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructor
        public MyExternalProfileViewModel()
        {
            this.apiService = new ApiService();

            this.User = MainViewModel.GetInstance().User;
            if (this.User.ImageFullPath == "noimage")
            {
                this.ImageSource = "no_image";
            }
            else
            {
                this.ImageSource = this.User.ImageFullPath;
            }
        }
        #endregion
    }
}
