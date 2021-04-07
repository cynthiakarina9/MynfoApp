namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class CreateProfileSMViewModel : BaseViewModel
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
        public string Link
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CreateProfileSMViewModel(string Type1)
        {
            this.apiService = new ApiService();
            Type = Type1;
        }
        #endregion

        #region Commands
        public ICommand SaveProfileCommand
        {
            get
            {
                return new RelayCommand(SaveProfile);
            }
        }        
        private async void SaveProfile()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Link))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LinkValidation,
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

            var mainViewModel = MainViewModel.GetInstance();
            var Profile = new ProfileSM();
            string ProfileType1 = string.Empty;
            string Logo1 = string.Empty;
            switch (Type)
            {
                case "Facebook":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 1
                    };
                    ProfileType1 = "Facebook";
                    Logo1 = "facebook2";
                    break;
                case "Instagram":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = "https://www.instagram.com/" + this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 2
                    };
                    ProfileType1 = "Instagram";
                    Logo1 = "instagramlogo2";
                    break;
                case "Twitter":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = "https://twitter.com/" + this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 3
                    };
                    ProfileType1 = "Twitter";
                    Logo1 = "twitterlogo2";
                    break;
                case "Snapchat":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = "https://www.snapchat.com/add/" + this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 4
                    };
                    ProfileType1 = "Snapchat";
                    Logo1 = "snapchat2";
                    break;
                case "LinkedIn":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 5
                    };
                    ProfileType1 = "LinkedIn";
                    Logo1 = "linkedin2";
                    break;
                case "TikTok":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = "https://tiktok.com/@" + this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 6
                    };
                    ProfileType1 = "TikTok";
                    Logo1 = "tiktok2";
                    break;
                case "Youtube":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 7
                    };
                    ProfileType1 = "Youtube";
                    Logo1 = "youtube2";
                    break;
                case "Spotify":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 8
                    };
                    ProfileType1 = "Spotify";
                    Logo1 = "spotify2";
                    break;
                case "Twitch":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = "https://www.twitch.tv/" + this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 9
                    };
                    ProfileType1 = "Twitch";
                    Logo1 = "twitch2";
                    break;
                case "WebPage":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 10
                    };
                    ProfileType1 = "WebPage";
                    Logo1 = "gmail2";
                    break;
                case "Telegram":
                    Profile = new ProfileSM
                    {
                        ProfileName = this.Name,
                        link = this.Link,
                        UserId = mainViewModel.User.UserId,
                        Exist = false,
                        RedSocialId = 11
                    };
                    ProfileType1 = "Telegram";
                    Logo1 = "telegram2";
                    break;
                default:
                    break;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileSM = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                Profile);

            if (profileSM == default)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorAddProfile,
                    Languages.Accept);
                return;
            }

            var ProfileLocal = new Profile
            {
                UserId = mainViewModel.User.UserId,
                ProfileName = profileSM.ProfileName,
                value = profileSM.link,
                ProfileType = ProfileType1,
                Logo = Logo1,
                ProfileId = profileSM.ProfileMSId,
            };

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                conn.CreateTable<Profile>();
                conn.Insert(ProfileLocal);
            }
            this.IsRunning = false;
            this.IsEnabled = true;

            //Agregar a la lista
            if (mainViewModel.ProfilesBYPESM != null)
            {
                mainViewModel.ProfilesBYPESM.addProfileSM(profileSM);
                mainViewModel.ListOfNetworks.addProfileSM(profileSM);
            }
            else
            {
                mainViewModel.Profiles.addProfileSM(profileSM);
                //switch (Type)
                //{
                //    case "Facebook":
                //        mainViewModel.Profiles.addProfileSM(profileSM);
                //        break;
                //    case "Instagram":
                //        mainViewModel.ProfilesByInstagram.addProfile(profileSM);
                //        break;
                //    case "Twitter":
                //        mainViewModel.ProfilesByTwitter.addProfile(profileSM);
                //        break;
                //    case "Snapchat":
                //        mainViewModel.ProfilesBySnapchat.addProfile(profileSM);
                //        break;
                //    case "LinkedIn":
                //        mainViewModel.ProfilesByLinkedin.addProfile(profileSM);
                //        break;
                //    case "TikTok":
                //        mainViewModel.ProfilesByTiktok.addProfile(profileSM);
                //        break;
                //    case "Youtube":
                //        mainViewModel.ProfilesByYoutube.addProfile(profileSM);
                //        break;
                //    case "Spotify":
                //        mainViewModel.ProfilesBySpotify.addProfile(profileSM);
                //        break;
                //    case "Twitch":
                //        mainViewModel.ProfilesByTwitch.addProfile(profileSM);
                //        break;
                //    case "WebPage":
                //        mainViewModel.ProfilesByWebPage.addProfile(profileSM);
                //        break;
                //    case "Telegram":
                //        mainViewModel.ProfilesByTelegram.addProfile(profileSM);
                //        break;
                //    default:
                //        break;
                //}                
            }


            this.Name = string.Empty;
            this.Link = string.Empty;

            if (mainViewModel.ProfilesBYPESM != null)
            {
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                await App.Navigator.PopAsync();
            }
        }

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
            MainViewModel.GetInstance().MenuItem = new MenuItemViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
