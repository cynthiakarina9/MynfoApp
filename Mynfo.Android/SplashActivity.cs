using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Mynfo.Droid;

namespace SplashScreenDemo.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { NfcAdapter.ActionTechDiscovered }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "application/com.companyname.Mynfo", DataScheme = "vnd.android.nfc", DataPathPrefix = "/com.Mynfo:letypetype", DataHost = "ext")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}
