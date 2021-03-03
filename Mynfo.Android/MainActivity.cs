namespace Mynfo.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Nfc;
    using Android.Nfc.Tech;
    using Android.OS;
    using Android.Runtime;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using Mynfo.Views;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    [Activity(Label = "Mynfo", Icon = "@mipmap/icon", /*Theme = "@style/MainTheme",*/ MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait), IntentFilter(new[] { "android.nfc.action.TECH_DISCOVERED" },    
    Categories = new[] { "android.intent.category.DEFAULT" }), 
    IntentFilter(new[] { "android.nfc.action.NDEF_DISCOVERED" },
    DataHost = "boxweb.azurewebsites.net", DataScheme = "http",
    Categories = new[] { "android.intent.category.DEFAULT" })]
    [MetaData("android.nfc.action.TECH_DISCOVERED", Resource = "@xml/techlist")]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static NfcAdapter NFCdevice;
        public string json;
        public Mynfo.Droid.Services.CardReader cardReader;
        public NfcReaderFlags READER_FLAGS = NfcReaderFlags.NfcA | NfcReaderFlags.SkipNdefCheck;
        #region Singleton
        private static MainActivity instance;

        public static MainActivity GetInstance()
        {
            if (instance == null)
            {
                instance = new MainActivity();
            }

            return instance;
        }
        #endregion       

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            //Set DB root
            string dbName = "Mynfo.db3";
            string dbBinder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbRoot = Path.Combine(dbBinder, dbName);
            instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            //popups
            Rg.Plugins.Popup.Popup.Init(this);

            //Color de tema
            Xamarin.Forms.Forms.SetFlags("AppTheme_Experimental");
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            base.OnCreate(savedInstanceState);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            //ShortcutBadger.ApplyCount();            
            
            //DetectShakeTest();
            //ToggleAccelerometer();
            get_status_nfc();
            var receiver = new Mynfo.Droid.Services.MessageReceiver();
            RegisterReceiver(receiver, new IntentFilter("MSG_NAME"));
            cardReader = new Mynfo.Droid.Services.CardReader();
            LoadApplication(new App(dbRoot));
            
            if (Intent?.Extras != null)
            {
                var message = Intent.Extras.GetString("MSG_DATA");
                //await App.DisplayAlertAsync(message);
            }


            if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                this.SetTheme(Resource.Style.NightTheme);
            }
            else
            {
                this.SetTheme(Resource.Style.MainTheme);
            }
        }

        public void get_status_nfc() 
        {
            NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
            NFCdevice = NfcManager.DefaultAdapter;
            if (NFCdevice != null)
            {
                MainViewModel.nfc_status = true;
            }
            else
            {
                MainViewModel.nfc_status = false;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
        private void EnableReaderMode()
        {
            try
            {
                var nfc = NfcAdapter.GetDefaultAdapter(this);
                if (nfc != null) nfc.EnableReaderMode(this, cardReader, READER_FLAGS, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }            
        }
        private void DisableReaderMode()
        {
            try 
            {
                var nfc = NfcAdapter.GetDefaultAdapter(this);
                if (nfc != null) 
                {
                    nfc.DisableReaderMode(this);
                    nfc.DisableForegroundDispatch(this);
                    nfc.DisableForegroundNdefPush(this);
                    nfc.Dispose();
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }            
        }
        protected string TagUid;
        protected override void OnResume()
        {
            base.OnResume();            
            try 
            {

                if (TAGPage.write_nfc == true)
                {
                    //App.Navigator.PushAsync(new ConfigStikerPage());                    
                    int user_id = 0;
                    if (NfcAdapter.ActionNdefDiscovered.Equals(Intent.Action))
                    {
                        //Get the NFC ID
                        var myTag = Intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);

                        var msg = (NdefMessage)myTag[0];
                        var record = msg.GetRecords()[0];
                        //If the NFC Card ID is not null
                        if (record != null)
                        {
                            if (record.Tnf == NdefRecord.TnfWellKnown) // The data is defined by the Record Type Definition (RTD) specification available from http://members.nfc-forum.org/specs/spec_list/
                            {
                                // Get the transfered data
                                var data = Encoding.ASCII.GetString(record.GetPayload());
                                string result = data.Substring(1);

                                string[] variables = result.Split('=');
                                string[] depura_userid = variables[1].Split('&');                                
                                user_id = Convert.ToInt32(depura_userid[0]);
                            }
                        }
                    }

                    string dominio = "boxweb.azurewebsites.net/";
                    string user = MainViewModel.GetInstance().User.UserId.ToString();
                    string tag_id = "";
                    if (user_id == Convert.ToInt32(user) || 0 == user_id)
                    {
                        string url = dominio + "index3.aspx?user_id=" + user + "&tag_id=" + tag_id;
                        //http://localhost:58951/index.aspx?user_id=7
                        var tag = Intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
                        if (tag != null)
                        {
                            Ndef ndef = Ndef.Get(tag);
                            if (ndef != null && ndef.IsWritable)
                            {
                                /*var payload = Encoding.ASCII.GetBytes(url);
                                var mimeBytes = Encoding.ASCII.GetBytes("text/html");
                                var record = new NdefRecord(NdefRecord.TnfWellKnown, mimeBytes, new byte[0], payload);
                                var ndefMessage = new NdefMessage(new[] { record });
                                ndef.Connect();
                                ndef.WriteNdefMessage(ndefMessage);
                                ndef.Close();*/

                                ndef.Connect();
                                NdefRecord mimeRecord = NdefRecord.CreateUri("http://" + url);
                                ndef.WriteNdefMessage(new NdefMessage(mimeRecord));
                                ndef.Close();
                            } 
                        }
                        TAGPage.write_nfc = false;
                        
                        var duration = TimeSpan.FromMilliseconds(2000);
                        Vibration.Vibrate(duration);
                    }
                    else 
                    {
                        System.Threading.Tasks.Task task = App.DisplayAlertAsync(Languages.MyTAGValidation);
                    }

                    App.Navigator.PushAsync(new Stickerconfig());
                    OnDestroy();
                }
                else 
                {
                    if (NfcAdapter.ActionNdefDiscovered.Equals(Intent.Action))
                    {
                        //Get the NFC ID
                        var myTag = Intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);

                        var msg = (NdefMessage)myTag[0];
                        var record = msg.GetRecords()[0];
                        //If the NFC Card ID is not null
                        if (record != null)
                        {
                            if (record.Tnf == NdefRecord.TnfWellKnown) // The data is defined by the Record Type Definition (RTD) specification available from http://members.nfc-forum.org/specs/spec_list/
                            {
                                // Get the transfered data
                                var data = Encoding.ASCII.GetString(record.GetPayload());
                                string result = data.Substring(1);                                
                                
                                string[] variables = result.Split('=');                                                                
                                string[] depura_userid = variables[1].Split('&');
                                string tag_id = variables[2]; 
                                string user_id = depura_userid[0];                                

                                Imprime_box.Consulta_user(user_id, tag_id);
                                OnDestroy();
                            }
                        }
                    }
                }                
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
            //DisableReaderMode();
            
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            NfcAdapter mNfcAdapter = NfcAdapter.GetDefaultAdapter(this);
            if (mNfcAdapter != null)
            {
                mNfcAdapter.Dispose();
                mNfcAdapter = null;
            }
        }
            

        //Convert the byte array of the NfcCard Uid to string
        private static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
       
        #region Trigger nfc
        // Set speed delay for monitoring changes.
        Xamarin.Essentials.SensorSpeed speed = Xamarin.Essentials.SensorSpeed.Game;

        public void DetectShakeTest()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Xamarin.Essentials.Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
        }
        void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            

            try
            {
                // Use default vibration length
                Vibration.Vibrate();
                // Or use specified time
                var duration = TimeSpan.FromMilliseconds(500);
                Vibration.Vibrate(duration);
                EnableReaderMode();

                Thread.Sleep(3000);

                DisableReaderMode();               

                Vibration.Vibrate();                             
                Vibration.Vibrate(duration);
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

//[assembly: Dependency(typeof(BackgroundDependency_Android))]
//namespace ProjectNamespace.Droid
//{
//    public class BackgroundDependency_Android : Java.Lang.Object, IBackgroundDependency
//    {
//        public void ExecuteCommand()
//        {
//            StartBeepWork();
//        }
//    }
//}