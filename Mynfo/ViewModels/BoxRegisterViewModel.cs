namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Domain;
    using Services;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class BoxRegisterViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        #endregion
        #region Properties
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
        public string Name
        {
            get;
            set;
        }
        #endregion
        #region Commands
        public ICommand SaveBoxCommand
        {
            get
            {
                return new RelayCommand(SaveBox);
            }
        }
        private async void SaveBox()
        {
             if (string.IsNullOrEmpty(this.Name))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.FirstNameValidation,
                        Languages.Accept);
                    return;
                }

                this.IsRunning = true;
                this.IsEnabled = false;

                var checkConnetion = await this.apiService.CheckConnection();
                if (!checkConnetion.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        checkConnetion.Message,
                        Languages.Accept);
                    return;
                }

                var box = new Box
                {
                    Name = this.Name,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    ImageArray = imageArray,
                    UserTypeId = 1,
                    UserId = this.Password,
                };

                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Post(
                    apiSecurity,
                    "/api",
                    "/Users",
                    user);

                if (!response.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        Languages.Accept);
                    return;
                }

                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ConfirmLabel,
                    Languages.UserRegisteredMessage,
                    Languages.Accept);
                var mainViewModel = new MainViewModel();
                //mainViewModel.SocialMedia = new SocialMediaViewModel();
                //Application.Current.MainPage = new SocialMediaPage();
                this.Email = string.Empty;
                this.FirstName = string.Empty;
                this.LastName = string.Empty;
                this.Password = string.Empty;
                this.Confirm = string.Empty;
                this.ImageSource = "no_image";
                mainViewModel.AddProfiles = new AddProfilesViewModel();
                App.Navigator.PushAsync(new AddProfilesPage());
            
            
        }
        #endregion

       
    }
}
