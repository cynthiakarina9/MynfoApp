﻿namespace Mynfo.iOS
{
    using Foundation;
    using Mynfo.Interfaces;
    using System;
    using System.IO;
    using UIKit;
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
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Set DB root
            string dbName = "Mynfo.db3";
            string dbBinder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));//, "..", "Library", "Databases");
            string dbRoot = Path.Combine(dbBinder, dbName);

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(dbRoot));
            DependencyService.Register<ILocalize>();
            return base.FinishedLaunching(app, options);
        }
    }
}
