using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Mynfo.Droid;
using System.Threading;

namespace SplashScreenDemo.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);
        //    StartActivity(typeof(MainActivity));
        //}
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); 
            //SetContentView(Resource.Layout.splash);
            StartActivity(typeof(MainActivity));
        }
    }
    //[Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    //public class SplashActivity : Activity
    //{
    //    protected override void OnCreate(Bundle bundle)
    //    {
    //        base.OnCreate(bundle); SetContentView(Resource.Layout.splash);
    //        ThreadPool.QueueUserWorkItem(o => LoadActivity());
    //        // Create your application here 
    //    }
    //    private void LoadActivity()
    //    {
    //        Thread.Sleep(1000);
    //        // Simulate a long pause 
    //        RunOnUiThread(() => StartActivity(typeof(MainActivity)));
    //    }
    //}
}
