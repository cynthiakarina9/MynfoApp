using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Mynfo.Droid;
using System.Threading;
using System.Threading.Tasks;

namespace SplashScreenDemo.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState); 
        //    //SetContentView(Resource.Layout.splash);
        //    StartActivity(typeof(MainActivity));
        //}
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(800); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
