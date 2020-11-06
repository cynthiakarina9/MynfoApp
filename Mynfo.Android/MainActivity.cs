namespace Mynfo.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Nfc;
    using Android.Nfc.Tech;
    using Android.OS;
    using Android.Runtime;
    using Mynfo.Models;
    using Newtonsoft.Json;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;
    using Poz1.NFCForms.Abstract;
    using Poz1.NFCForms.Droid;
    using System;
    using System.IO;
    using System.Text;
    using Mynfo.Models;
    using Mynfo.ViewModels;

    [Activity(Label = "Mynfo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { NfcAdapter.ActionNdefDiscovered }, 
    Categories = new[] { Intent.CategoryDefault }, 
    DataMimeType = "application/com.mynfo",
    DataScheme = "vnd.android.nfc", 
    DataPathPrefix = "/com.mynfo:letypetype", 
    DataHost = "ext")]   

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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
                
        private NdefMessage ndefMessage;
        public NfcAdapter mNfcAdapter;
        public string json = Data_ntc.data_value;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Set DB root
            string dbName = "Mynfo.db3";
            string dbBinder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbRoot = Path.Combine(dbBinder, dbName);
            instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            mNfcAdapter = NfcAdapter.GetDefaultAdapter(this);
            
            //Color de tema
            Xamarin.Forms.Forms.SetFlags("AppTheme_Experimental");
            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //ShortcutBadger.ApplyCount();
            onCreate();
            LoadApplication(new App(dbRoot));
            
        }

        protected void HandleNFC(Intent intent, Boolean inForeground)
        {
            try 
            {
                NdefMessage[] msgs = null;
                IParcelable[] rawMsgs = intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);

                if (rawMsgs != null)
                {
                    Get_nfc get_nfc = null;
                    onCreate();
                    NdefMessage msg = (NdefMessage)rawMsgs[0];
                    var text = Encoding.UTF8.GetString(msg.GetRecords()[0].GetPayload());

                    string[] Json = text.ToString().Split('¡');
                    string mesaje = Json[1].ToString();
                    get_nfc = JsonConvert.DeserializeObject<Get_nfc>(mesaje);

                }
                else
                {
                    Console.WriteLine("No message");
                }
            }
            catch (Exception exep) 
            {
                Console.WriteLine(exep);
            }            
        }

        public string get_box() 
        {
            try
            {
                var Profile = new ProfileLocal();

                var Box_Local = new BoxLocal();
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.CreateTable<ProfileLocal>();
                    Profile = conn.Table<ProfileLocal>().FirstOrDefault();

                    conn.CreateTable<BoxLocal>();
                    Box_Local = conn.Table<BoxLocal>().FirstOrDefault();



                    /*json = "Box recibida correctamente!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +

                           "¡{" 
                              + @"""BoxId"":""" + Box_Local.BoxId + @""",
                                ""Name"":""" + Box_Local.Name + @""",
                                ""BoxDefault"":""" + Box_Local.BoxDefault + @""",
                                ""UserId"":""" + Box_Local.UserId + @""",
                                ""Time"":""" + Box_Local.Time + @""",
                                ""ImagePath"":""" + Box_Local.ImagePath + @""",
                                ""UserTypeId"":""" + Box_Local.UserTypeId + @""",
                                ""FirstName"":""" + Box_Local.FirstName + @""",
                                ""LastName"":""" + Box_Local.LastName + @""",
                                ""ImageFullPath"":""" + Box_Local.ImageFullPath + @""",
                                ""FullName"":""" + Box_Local.FullName + @""",
                                ""ProfileLocalId"":""" + Profile.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile.IdBox + @""",
                                ""UserId"":""" + Profile.UserId + @""",
                                ""ProfileName"":""" + Profile.ProfileName + @""",
                                ""value"":""" + Profile.value + @""",
                                ""ProfileType"":""" + Profile.ProfileType + @"""                                                              
                                }";*/

                    json = "Box recibida correctamente!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +

                               "¡{"
                                  + @"""BoxId"":""" + Box_Local.BoxId + @""",
                                ""Name"":""" + Box_Local.Name + @""",
                                ""BoxDefault"":""" + Box_Local.BoxDefault + @""",
                                ""UserId"":""" + Box_Local.UserId + @""",
                                ""Time"":""" + Box_Local.Time + @""",
                                ""ImagePath"":""" + Box_Local.ImagePath + @""",
                                ""UserTypeId"":""" + Box_Local.UserTypeId + @""",
                                ""FirstName"":""" + Box_Local.FirstName + @""",
                                ""LastName"":""" + Box_Local.LastName + @""",
                                ""ImageFullPath"":""" + Box_Local.ImageFullPath + @""",
                                ""FullName"":""" + Box_Local.FullName + @"""                                                                                           
                                },";
                }
            }
            catch (Exception exx)
            {
                Console.Write(exx);
                json = null;
            }

            return json;
        }        

        protected override void OnResume()
        {
            base.OnResume();
            try
            {
                if (mNfcAdapter != null)
                {
                    var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);//or try other Action type
                    var tagDetectednDef = new IntentFilter(NfcAdapter.ActionNdefDiscovered);
                    var tagDetectedtech = new IntentFilter(NfcAdapter.ActionTechDiscovered);
                    var filters = new[] { tagDetected, tagDetectednDef, tagDetectedtech };
                    var intent = new Intent(this, this.GetType()).AddFlags(ActivityFlags.SingleTop);
                    var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);
                    mNfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }              

        protected override void OnNewIntent(Intent intent)
        {            
            var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;

            if (tag == null)
            {
                return;
            }

            if (NfcAdapter.ExtraTag.Contains("nfc"))
            {
                HandleNFC(intent, true);
            }

        }

        protected override void OnPause()
        {
            base.OnPause();
            try
            {                
                if (mNfcAdapter != null) mNfcAdapter.DisableForegroundDispatch(this);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        protected void onCreate()
        {                         
            DateTime time = DateTime.Now;
            var text = ("Beam me up!\n\n" +
                            "Beam Time: " + time.ToString("DD/MM/YYYY HH:mm:ss"));
            get_box();
            try 
            {                
                NdefMessage msg = new NdefMessage(
                new NdefRecord[] { CreateMimeRecord (
                "application/com.example.android.beam", Encoding.UTF8.GetBytes(json.ToString()))

                });

                ndefMessage = msg;

                NfcAdapter nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
                if (nfcAdapter == null) return;  // NFC not available on this device
                nfcAdapter.SetNdefPushMessage(ndefMessage, this);

                nfcAdapter.SetNdefPushMessageCallback(null, this);


            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }            
        }

        public NdefRecord CreateMimeRecord(String mimeType, byte[] payload)
        {
            byte[] mimeBytes = Encoding.UTF8.GetBytes(mimeType);
            NdefRecord mimeRecord = new NdefRecord(
                NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
            return mimeRecord;
        }       
    }
}