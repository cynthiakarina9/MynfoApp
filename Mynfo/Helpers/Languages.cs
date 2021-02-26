namespace Mynfo.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;
    using System.Globalization;

    public static class Languages
    {
        static Languages()
        {

            if (Device.RuntimePlatform == Device.iOS)
            {
                Resource.Culture = CultureInfo.CurrentCulture;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Resource.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }

            DependencyService.Get<ILocalize>().SetLocale(Resource.Culture);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }

        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string EMail
        {
            get { return Resource.EMail; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }

        public static string Forgot
        {
            get { return Resource.Forgot; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }

        public static string Countries
        {
            get { return Resource.Countries; }
        }

        public static string Search
        {
            get { return Resource.Search; }
        }

        public static string Country
        {
            get { return Resource.Country; }
        }

        public static string Information
        {
            get { return Resource.Information; }
        }

        public static string Capital
        {
            get { return Resource.Capital; }
        }

        public static string Population
        {
            get { return Resource.Population; }
        }

        public static string Area
        {
            get { return Resource.Area; }
        }

        public static string AlphaCode2
        {
            get { return Resource.AlphaCode2; }
        }

        public static string AlphaCode3
        {
            get { return Resource.AlphaCode3; }
        }

        public static string Region
        {
            get { return Resource.Region; }
        }

        public static string Subregion
        {
            get { return Resource.Subregion; }
        }

        public static string Demonym
        {
            get { return Resource.Demonym; }
        }

        public static string GINI
        {
            get { return Resource.GINI; }
        }

        public static string NativeName
        {
            get { return Resource.NativeName; }
        }

        public static string NumericCode
        {
            get { return Resource.NumericCode; }
        }

        public static string CIOC
        {
            get { return Resource.CIOC; }
        }

        public static string Borders
        {
            get { return Resource.Borders; }
        }

        public static string Currencies
        {
            get { return Resource.Currencies; }
        }

        public static string MyLanguages
        {
            get { return Resource.MyLanguages; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }

        public static string MyAccount
        {
            get { return Resource.MyAccount; }
        }

        public static string Statics
        {
            get { return Resource.Statics; }
        }

        public static string LogOut
        {
            get { return Resource.LogOut; }
        }
        public static string RegisterTitle
        {
            get { return Resource.RegisterTitle; }
        }

        public static string ChangeImage
        {
            get { return Resource.ChangeImage; }
        }

        public static string FirstNameLabel
        {
            get { return Resource.FirstNameLabel; }
        }

        public static string FirstNamePlaceHolder
        {
            get { return Resource.FirstNamePlaceHolder; }
        }

        public static string FirstNameValidation
        {
            get { return Resource.FirstNameValidation; }
        }

        public static string LastNameLabel
        {
            get { return Resource.LastNameLabel; }
        }

        public static string LastNamePlaceHolder
        {
            get { return Resource.LastNamePlaceHolder; }
        }

        public static string LastNameValidation
        {
            get { return Resource.LastNameValidation; }
        }

        public static string PhoneLabel
        {
            get { return Resource.PhoneLabel; }
        }

        public static string PhonePlaceHolder
        {
            get { return Resource.PhonePlaceHolder; }
        }

        public static string PhoneValidation
        {
            get { return Resource.PhoneValidation; }
        }

        public static string ConfirmLabel
        {
            get { return Resource.ConfirmLabel; }
        }

        public static string ConfirmPlaceHolder
        {
            get { return Resource.ConfirmPlaceHolder; }
        }

        public static string ConfirmValidation
        {
            get { return Resource.ConfirmValidation; }
        }

        public static string EmailValidation2
        {
            get { return Resource.EmailValidation2; }
        }

        public static string PasswordValidation2
        {
            get { return Resource.PasswordValidation2; }
        }

        public static string ConfirmValidation2
        {
            get { return Resource.ConfirmValidation2; }
        }

        public static string UserRegisteredMessage
        {
            get { return Resource.UserRegisteredMessage; }
        }

        public static string SourceImageQuestion
        {
            get { return Resource.SourceImageQuestion; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }

        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }

        public static string FromCamera
        {
            get { return Resource.FromCamera; }
        }

        public static string Save
        {
            get { return Resource.Save; }
        }

        public static string ChangePassword
        {
            get { return Resource.ChangePassword; }
        }

        public static string CurrentPassword
        {
            get { return Resource.CurrentPassword; }
        }

        public static string CurrentPasswordPlaceHolder
        {
            get { return Resource.CurrentPasswordPlaceHolder; }
        }

        public static string NewPassword
        {
            get { return Resource.NewPassword; }
        }

        public static string NewPasswordPlaceHolder
        {
            get { return Resource.NewPasswordPlaceHolder; }
        }

        public static string ConnectionError1
        {
            get { return Resource.ConnectionError1; }
        }

        public static string ConnectionError2
        {
            get { return Resource.ConnectionError2; }
        }

        public static string LoginError
        {
            get { return Resource.LoginError; }
        }

        public static string ChagePasswordConfirm
        {
            get { return Resource.ChagePasswordConfirm; }
        }

        public static string PasswordError
        {
            get { return Resource.PasswordError; }
        }

        public static string ErrorChangingPassword
        {
            get { return Resource.ErrorChangingPassword; }
        }

        public static string MyProfiles
        {
            get { return Resource.MyProfiles; }
        }
        public static string Settings
        {
            get { return Resource.Settings; }
        }
        public static string Home
        {
            get { return Resource.Home; }
        }
        public static string Profiles
        {
            get { return Resource.Profiles; }
        }
        public static string AddPBox
        {
            get { return Resource.AddPBox; }
        }
        public static string NewBox
        {
            get { return Resource.NewBox; }
        }
        public static string BoxName
        {
            get { return Resource.BoxName; }
        }
        public static string Create
        {
            get { return Resource.Create; }
        }
        public static string AddPSocialMedia
        {
            get { return Resource.AddPSocialMedia; }
        }
        public static string User
        {
            get { return Resource.User; }
        }
        public static string LoginSession
        {
            get { return Resource.LoginSession; }
        }
        public static string NewProfile
        {
            get { return Resource.NewProfile; }
        }
        public static string DefaultBox
        {
            get { return Resource.DefaultBox; }
        }
        public static string ProfileDetails
        {
            get { return Resource.ProfileDetails; }
        }
        public static string EditProfileData
        {
            get { return Resource.EditProfileData; }
        }
        public static string ProfileName
        {
            get { return Resource.ProfileName; }
        }
        public static string Value
        {
            get { return Resource.Value; }
        }
        public static string Update
        {
            get { return Resource.Update; }
        }
        public static string BoxList
        {
            get { return Resource.BoxList; }
        }
        public static string MyAccounts
        {
            get { return Resource.MyAccounts; }
        }
        public static string AddAccount
        {
            get { return Resource.AddAccount; }
        }
        public static string ActiveAccounts
        {
            get { return Resource.ActiveAccounts; }
        }
        public static string AddProfiles
        {
            get { return Resource.AddProfiles; }
        }
        public static string CreateProfilePhone
        {
            get { return Resource.CreateProfilePhone; }
        }
        public static string NameProfile
        {
            get { return Resource.NameProfile; }
        }
        public static string NumberPhone
        {
            get { return Resource.NumberPhone; }
        }
        public static string ChooseTypeProfile
        {
            get { return Resource.ChooseTypeProfile; }
        }
        public static string ProfilePhone
        {
            get { return Resource.ProfilePhone; }
        }
        public static string NameValidation
        {
            get { return Resource.NameValidation; }
        }
        public static string PhoneValidation2
        {
            get { return Resource.PhoneValidation2; }
        }
        public static string NumberValidation
        {
            get { return Resource.NumberValidation; }
        }
        public static string CreateProfileEmail
        {
            get { return Resource.CreateProfileEmail; }
        }
        public static string DetailBox
        {
            get { return Resource.DetailBox; }
        }
        public static string Back
        {
            get { return Resource.Back; }
        }
        public static string Warning
        {
            get { return Resource.Warning; }
        }
        public static string DeleteBoxNotification
        {
            get { return Resource.DeleteBoxNotification; }
        }
        public static string Yes
        {
            get { return Resource.Yes; }
        }
        public static string No
        {
            get { return Resource.No; }
        }
        public static string Link
        {
            get { return Resource.Link; }
        }
        public static string NProfilePlaceH
        { 
            get { return Resource.NProfilePlaceH; }
        }
        public static string ExampleEmail
        {
            get { return Resource.ExampleEmail; }
        }
        public static string EditPhone
        {
            get { return Resource.EditPhone; }
        }
        public static string EditEmail
        {
            get { return Resource.EditEmail; }
        }
        public static string FacebookProfileList
        {
            get { return Resource.FacebookProfileList; }
        }
        public static string ReceivedBoxes
        {
            get { return Resource.ReceivedBoxes; }
        }
        public static string Delete
        {
            get { return Resource.Delete;}
        }
        public static string LinkValidation
        {
            get { return Resource.LinkValidation; }
        }
        
        public static string ProfileELabel
        {
            get { return Resource.ProfileELabel; }
        }

        public static string Success
        {
            get { return Resource.Success; }
        }
        public static string NetworkAdded
        {
            get { return Resource.NetworkAdded; }
        }
        public static string Close
        {
            get { return Resource.Close; }
        }

        public static string AskDeleteNetworkFromBox
        {
            get { return Resource.AskDeleteNetworkFromBox; }
        }
        public static string ErrorAddProfile
        {
            get { return Resource.ErrorAddProfile; }
        }

        public static string NetworksList
        {
            get { return Resource.NetworksList; }
        }

        public static string WrongEmail
        {
            get { return Resource.WrongEmail;  }
        }
        public static string ProfileNull
        {
            get { return Resource.ProfileNull; }
        }
        public static string NickName
        {
            get { return Resource.NickName; }
        }
        public static string NickNamePH
        {
            get { return Resource.NickNamePH; }
        }
        public static string EmptyBox
        {
            get { return Resource.EmptyBox; }
        }
        public static string Next
        {
            get { return Resource.Next; }
        }
        public static string HaveWhats
        {
            get { return Resource.HaveWhats; }
        }
        public static string Continue
        {
            get { return Resource.Continue; }
        }
        public static string SendEmail
        {
            get { return Resource.SendEmail; }
        }
        public static string PutEmail
        {
            get { return Resource.PutEmail; }
        }
        public static string Tag
        {
            get { return Resource.Tag; }
        }
        public static string QR
        {
            get { return Resource.QR; }
        }
        public static string MyQR
        {
            get { return Resource.MyQR; }
        }
        public static string EscanQR
        {
            get { return Resource.EscanQR; }
        }
        public static string SureTAG
        {
            get { return Resource.SureTAG; }
        }
        public static string TakeinStore
        {
            get { return Resource.TakeinStore; }
        }
        public static string Store
        {
            get { return Resource.Store; }
        }
        public static string Push
        {
            get { return Resource.Push; }
        }
        public static string ConfigureTAG
        {
            get { return Resource.ConfigureTAG; }
        }
        public static string AndStick
        {
            get { return Resource.AndStick; }
        }
        public static string ProfileMynfo
        {
            get { return Resource.ProfileMynfo; }
        }
        public static string QRForWhat
        {
            get { return Resource.QRForWhat; }
        }
        public static string QRMynfo
        {
            get { return Resource.QRMynfo; }
        }
        public static string ConfigSticker
        {
            get { return Resource.ConfigSticker; }
        }
        public static string ConfigStickerRi
        {
            get { return Resource.ConfigStickerRi; }
        }
        public static string AllConfigSticker
        {
            get { return Resource.AllConfigSticker; }
        }
        public static string HoldYourPhone
        {
            get { return Resource.HoldYourPhone; }
        }
        public static string ReadyToScan
        {
            get { return Resource.ReadyToScan; }
        }
        public static string AgeLabel
        {
            get { return Resource.AgeLabel; }
        }
        public static string AgePlaceHolder
        {
            get { return Resource.AgePlaceHolder; }
        }
        public static string LocationLabel
        {
            get { return Resource.LocationLabel; }
        }
        public static string LocationPlaceHolder
        {
            get { return Resource.LocationPlaceHolder; }
        }
        public static string OccupationLabel
        {
            get { return Resource.OccupationLabel; }
        }
        public static string OccupationPlaceHolder
        {
            get { return Resource.OccupationPlaceHolder; }
        }
    }
}