namespace Mynfo.iOS
{
    using CoreNFC;
    using Foundation;
    using Mynfo.Interfaces;
    using Mynfo.iOS.Services;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using UIKit;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]

    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate    
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public NFCNdefReaderSession Session { get; set; }
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Set DB root
            string dbName = "Mynfo.db3";
            string dbBinder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));//, "..", "Library", "Databases");
            string dbRoot = Path.Combine(dbBinder, dbName);
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();            
            DetectShakeTest();
            ToggleAccelerometer();
            LoadApplication(new App(dbRoot));
            DependencyService.Register<ILocalize>();
            return base.FinishedLaunching(app, options);
        }       



        #region Trigger nfc
        // Set speed delay for monitoring changes.
        Xamarin.Essentials.SensorSpeed speed = Xamarin.Essentials.SensorSpeed.Game;

        public void DetectShakeTest()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Xamarin.Essentials.Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
        }
        public void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            try
            {
                // Use default vibration length
                Vibration.Vibrate();
                // Or use specified time
                var duration = TimeSpan.FromMilliseconds(500);
                Vibration.Vibrate(duration);

                //string var = ReadTag.ScanAsync();

                /*Thread.Sleep(3000);

                Vibration.Vibrate();
                Vibration.Vibrate(duration);*/
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Xamarin.Essentials.Accelerometer.IsMonitoring)
                    Xamarin.Essentials.Accelerometer.Stop();
                else
                    Xamarin.Essentials.Accelerometer.Start(speed);
            }
            catch (Xamarin.Essentials.FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                Console.WriteLine("falla");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine("falla");
            }
        }
        #endregion Trigger nfc
       
    }
}
