namespace Mynfo.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        //private const string token = "Token";
        //private const string tokenType = "TokenType";
        //private const string userASP = "UserASP";
        private const string isRemembered = "IsRemembered";
        private static readonly string stringDefault = string.Empty;
        //private static readonly bool booleanDefault = false;
        #endregion

        //public static string UserASP
        //{
        //    get
        //    {
        //        return AppSettings.GetValueOrDefault(userASP, stringDefault);
        //    }
        //    set
        //    {
        //        AppSettings.AddOrUpdateValue(userASP, value);
        //    }
        //}

        //public static string Token
        //{
        //    get
        //    {
        //        return AppSettings.GetValueOrDefault(token, stringDefault);
        //    }
        //    set
        //    {
        //        AppSettings.AddOrUpdateValue(token, value);
        //    }
        //}

        //public static string TokenType
        //{
        //    get
        //    {
        //        return AppSettings.GetValueOrDefault(tokenType, stringDefault);
        //    }
        //    set
        //    {
        //        AppSettings.AddOrUpdateValue(tokenType, value);
        //    }
        //}



        public static string IsRemembered
        {
            get
            {
                return AppSettings.GetValueOrDefault(isRemembered, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isRemembered, value);
            }
        }
    }
}
