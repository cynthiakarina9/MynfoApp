namespace Mynfo.ViewModels
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.Views;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    public class ProfilesByEmailViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Name
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public IList<ProfileEmail> profileEmail { get; private set; }
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
        public ProfilesByEmailViewModel()
        {
            apiService = new ApiService();
            profileEmail = new List<ProfileEmail>();
            SetList();
        }
        #endregion

        #region Commands
        public async void SetList()
        {
            this.IsRunning = true;
            this.isEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();


            profileEmail = await this.apiService.GetListByUser<ProfileEmail>(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                MainViewModel.GetInstance().User.UserId);
            

        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ProfileEmail selectedItem = e.SelectedItem as ProfileEmail;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileEmail tappedItem = e.Item as ProfileEmail;
            App.Navigator.PushAsync(new EditProfileEmailPage(tappedItem.ProfileEmailId));
        }
        #endregion
    }
}
