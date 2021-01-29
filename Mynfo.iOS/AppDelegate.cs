namespace Mynfo.iOS
{
    using CoreFoundation;
    using CoreNFC;
    using Foundation;
    using Mynfo.Interfaces;
    using Mynfo.iOS.Services;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using Mynfo.Views;
    using NdefLibrary.Ndef;
    using Plugin.NFC;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using UIKit;
    using Xamarin.Essentials;    
    using Xamarin.Forms;

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]

    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, INFCNdefReaderSessionDelegate
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
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            DetectShakeTest();
            ToggleAccelerometer();
            LoadApplication(new App(dbRoot));
            DependencyService.Register<ILocalize>();
            return base.FinishedLaunching(app, options);
            //UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            //if (statusBar != null && statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            //{
            //    // change to your desired color 
            //    statusBar.BackgroundColor = Color.FromHex("#7f6550").ToUIColor();
            //}
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

                if (SettingsPage.write_nfc == true) 
                {
                    ScanWriteAsync();
                    SettingsPage.write_nfc = false;
                    var duration = TimeSpan.FromMilliseconds(1500);
                    Vibration.Vibrate(duration);
                }
                else 
                {
                    Vibration.Vibrate();

                    InvokeOnMainThread(() =>
                    {
                        Session = new NFCNdefReaderSession(this, null, true);
                        if (Session != null)
                        {
                            Session.BeginSession();
                        }
                    });
                }         
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

        public void DidDetect(NFCNdefReaderSession session, NFCNdefMessage[] messages)
        {         
            try
            {                                
                if (messages != null && messages.Length > 0)
                {
                    var first = messages[0];
                    string messa = GetRecords(first.Records);
                    string[] variables = messa.Split('=');
                    string[] depura_userid = variables[1].Split('&');
                    string tag_id = variables[2];
                    string user_id = depura_userid[0];

                    Imprime_box.Consulta_user(user_id, tag_id);
                }                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }            
        }        
             
        public void DidInvalidate(NFCNdefReaderSession session, NSError error)
        {

            var readerError = (NFCReaderError)(long)error.Code;

            if (readerError != NFCReaderError.ReaderSessionInvalidationErrorFirstNDEFTagRead &&
                readerError != NFCReaderError.ReaderSessionInvalidationErrorUserCanceled)
            {
                InvokeOnMainThread(() =>
                {
                    var alertController = UIAlertController.Create("Session Invalidated", error.LocalizedDescription, UIAlertControllerStyle.Alert);
                    alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    //DispatchQueue.MainQueue.DispatchAsync(() =>
                    //{
                    //    this.PresentViewController(alertController, true, null);
                    //});
                });
            }
        }

        string GetRecords(NFCNdefPayload[] records)
        {
            string record = null;
            var results = new NFCNdefRecord[records.Length];
            for (var i = 0; i < records.Length; i++)
            {
                record = records[i].Payload.ToString();                
            }
            return record;
        }

        public static String ProcessNFCRecord(NdefRecord record)
        {
            //Define the tag content we want to return.
            String tagContent = null;
            //Make sure we have a record.
            if (record != null)
            {
                //Check if the record is a URL.
                if (record.CheckSpecializedType(true) == typeof(NdefUriRecord))
                {
                    //The content is a URL.
                    tagContent = new NdefUriRecord(record).Uri;
                }
                else if (record.CheckSpecializedType(true) == typeof(NdefMailtoRecord))
                {
                    //The content is a mailto record.
                    tagContent = new NdefMailtoRecord(record).Uri;
                }
                else if (record.CheckSpecializedType(true) == typeof(NdefTelRecord))
                {
                    //The content is a tel record.
                    tagContent = new NdefTelRecord(record).Uri;
                }
                else if (record.CheckSpecializedType(true) == typeof(NdefSmsRecord))
                {
                    //The content is a sms record.
                    tagContent = new NdefSmsRecord(record).Uri;
                }
                else if (record.CheckSpecializedType(true) == typeof(NdefTextRecord))
                {
                    //The content is a text record.
                    tagContent = new NdefTextRecord(record).Text;
                }
                else
                {
                    //Try and force a pure text conversion.
                    tagContent = Encoding.UTF8.GetString(record.Payload);
                }
            }

            //Return the tag content.
            return tagContent;
        }        


        public NFCNdefReaderSession _tagSession;        
        public TaskCompletionSource<string> _tcs;
        public Task ScanWriteAsync()
        {

            if (!NFCNdefReaderSession.ReadingAvailable)
            {
                throw new InvalidOperationException("Reading NDEF is not available");
            }

            _tcs = new TaskCompletionSource<string>();
            var pollingOption = NFCPollingOption.Iso14443;
            _tagSession = new NFCNdefReaderSession(this, DispatchQueue.CurrentQueue, true)
            {
                AlertMessage = "Writing",
            };

            _tagSession.BeginSession();

            return _tcs.Task;
        }      

        [Foundation.Export("readerSession:didDetectTags:")]
        public void DidDetectTags(NFCNdefReaderSession session, INFCNdefTag[] tags)
        {
            var nFCNdefTag = tags[0];
            session.ConnectToTag(nFCNdefTag, CompletionHandler);

            string dominio = "http://boxweb1.azurewebsites.net/";
            string user = MainViewModel.GetInstance().User.UserId.ToString();
            string tag_id = "";
            string url = dominio + "index3.aspx?user_id=" + user + "&tag_id=" + tag_id;

            NFCNdefPayload payload = NFCNdefPayload.CreateWellKnownTypePayload(url);
            NFCNdefMessage nFCNdefMessage = new NFCNdefMessage(new NFCNdefPayload[] { payload });
            nFCNdefTag.WriteNdef(nFCNdefMessage, delegate
            {
                Console.WriteLine("escrito");
                SettingsPage.write_nfc = false;
            });
            Console.WriteLine("escrito");
            SettingsPage.write_nfc = false;
        }

        private void CompletionHandler(NSError obj)
        {
            //add code here
        }  
    }
}