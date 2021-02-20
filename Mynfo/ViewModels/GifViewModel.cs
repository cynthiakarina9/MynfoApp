namespace Mynfo.ViewModels
{
    using Mynfo.Services;

    public class GifViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        private string gitImage;
        #endregion

        #region Properties

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public string GitImage
        {
            get { return this.gitImage; }
            set { SetValue(ref this.gitImage, value); }
        }
        #endregion

        #region Constructor
        public GifViewModel(string Name)
        {
            apiService = new ApiService();
            GitImage = "";
            IsEnabled = true;
            IsRunning = true;
            GetImage(Name);
        }
        #endregion

        #region Methods
        public string GetImage(string Name)
        {
            switch (Name)
            {
                case "Facebook":
                    GitImage = "GIF_Facebook4.gif";
                    break;
                case "LinkedIn":
                    GitImage = "GIF_LinkedIn.gif";
                    break;
                case "Spotify":
                    GitImage = "GIF_Spotify.gif";
                    break;
                case "YouTube":
                    GitImage = "GIF_Youtube.gif";
                    break;
                default:
                    break;
            }
            //IsRunning = false;
            return GitImage;
        }
        #endregion
    }
}
