namespace Mynfo.ViewModels
{
    using Mynfo.Models;
    using Mynfo.Services;
    using Xamarin.Forms;

    public class MyQRViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private string user;
        private UserLocal userLocal;
        private ImageSource imageSource;
        #endregion

        #region Properties
        public string User
        {
            get
            {
                return this.user;
            }
            set
            {
                SetValue(ref this.user, value);
            }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public UserLocal UserLocal
        {
            get
            {
                return this.userLocal;
            }
            set
            {
                SetValue(ref this.userLocal, value);
            }
        }
        #endregion

        #region Contructor
        public MyQRViewModel()
        {
            apiService = new ApiService();
            UserLocal = MainViewModel.GetInstance().User;
            if (this.UserLocal.ImageFullPath == "noimage")
            {
                this.ImageSource = "no_image";
            }
            else
            {
                this.ImageSource = this.UserLocal.ImageFullPath;
            }
            GetDato(UserLocal);
        }
        #endregion

        #region Methods
        public string GetDato (UserLocal U)
        {
            User = "https://boxweb.azurewebsites.net/index.aspx?user_id=" + U.UserId + "&tag_id=";
            return User;
        }
        #endregion
    }
}
