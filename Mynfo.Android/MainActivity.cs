namespace Mynfo.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Nfc;
    using Android.Nfc.Tech;
    using Android.OS;
    using Android.Runtime;
    using Models;
    using Newtonsoft.Json;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;
    using Poz1.NFCForms.Abstract;
    using Poz1.NFCForms.Droid;
    using System;
    using System.IO;
    using System.Text;
    using Mynfo.ViewModels;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using SQLite;
    using Android.Graphics.Drawables;
    using System.Globalization;

    [Activity(Label = "Mynfo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    [IntentFilter(new[] { NfcAdapter.ActionNdefDiscovered }, 
    Categories = new[] { Intent.CategoryDefault }, 
    DataMimeType = "application/com.mynfo",
    DataScheme = "vnd.android.nfc", 
    DataPathPrefix = "/com.mynfo:letypetype", 
    DataHost = "ext")]   



    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, NfcAdapter.ICreateNdefMessageCallback, NfcAdapter.IOnNdefPushCompleteCallback
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
                        
        public NfcAdapter mNfcAdapter;
        public static string json;
        public NfcAdapter NFCdevice;
        public Activity activity;

        protected override void OnCreate(Bundle savedInstanceState)
        {            
            //Set DB root
            string dbName = "Mynfo.db3";
            string dbBinder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbRoot = Path.Combine(dbBinder, dbName);
            instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;            
            

            //popups
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            //Color de tema
            Xamarin.Forms.Forms.SetFlags("AppTheme_Experimental");
            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //ShortcutBadger.ApplyCount();            

            try
            {
                mNfcAdapter = NfcAdapter.GetDefaultAdapter(this);
                mNfcAdapter.SetNdefPushMessageCallback(this, this);               
                mNfcAdapter.SetOnNdefPushCompleteCallback(this, this);                
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
            DetectShakeTest();
            ToggleAccelerometer();

            LoadApplication(new App(dbRoot));            
        }                       

        //List<Get_nfc> nfcData = null;
        public List<Get_nfc> nfcData = new List<Get_nfc>();

        protected void HandleNFC(Intent intent, Boolean inForeground)
        {
            try 
            {
                NdefMessage[] msgs = null;
                IParcelable[] rawMsgs = intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);

                if (rawMsgs != null)
                {
                    Get_nfc get_nfc = null;                    
                    NdefMessage msg = (NdefMessage)rawMsgs[0];
                    var text = Encoding.UTF8.GetString(msg.GetRecords()[0].GetPayload());

                    string[] Json_parse = text.ToString().Split('¬');
                    string Json_value = Json_parse[1];

                    //nfcData almasena todo en una lista cuando obtiene los datos del telefono servidor 
                    nfcData = (List<Get_nfc>)JsonConvert.DeserializeObject(Json_value, typeof(List<Get_nfc>));

                    if(nfcData != null)
                    {
                        //Insertar método para poblar tabla de boxes foraneas
                        this.InsertForeignData();
                    }
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
                json = null;
                var Profile = new ProfileLocal();
                var Profile_1 = new ProfileLocal();
                var Box_Local = new BoxLocal();
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {                                        
                    Profile_1 = conn.Table<ProfileLocal>().FirstOrDefault();                    
                    Box_Local = conn.Table<BoxLocal>().FirstOrDefault();
                    int coun = conn.Table<ProfileLocal>().Count();
                    string json_header = null;
                    string json_body = null;
                    string json_value = null;
                    string json_fantasma = null;
                    

                    var finaldate = Box_Local.Time.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.CreateSpecificCulture("es-MX"));

                    //11/30/2020 6:09:07 p. m.

                    json_header = "Box recibida correctamente!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +
                           "¡";
                    
                    json_value = "{"
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
                                ""ProfileLocalId"":""" + Profile_1.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile_1.IdBox + @""",
                                ""UserId_p"":""" + Profile_1.UserId + @""",
                                ""ProfileName"":""" + Profile_1.ProfileName + @""",
                                ""value"":""" + Profile_1.value + @""",
                                ""ProfileType"":""" + Profile_1.ProfileType + @"""                                                              
                                }";
                    
                    json_fantasma = "{"
                              + @"""BoxId"":""-"",
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
                                ""ProfileLocalId"":""" + Profile_1.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile_1.IdBox + @""",
                                ""UserId_p"":""" + Profile_1.UserId + @""",
                                ""ProfileName"":""" + Profile_1.ProfileName + @""",
                                ""value"":""" + Profile_1.value + @""",
                                ""ProfileType"":""" + Profile_1.ProfileType + @"""                                                              
                                }";

                    if (coun > 1)
                    {
                        for (int i = 1; i < coun; i++)
                        {
                            Profile = conn.Table<ProfileLocal>().ElementAt(i);

                            json_body = "{"
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
                                ""UserId_p"":""" + Profile.UserId + @""",
                                ""ProfileName"":""" + Profile.ProfileName + @""",
                                ""value"":""" + Profile.value + @""",
                                ""ProfileType"":""" + Profile.ProfileType + @"""                                                              
                                }";

                            json_value = json_value + ",\n" + json_body;
                        }
                        json_value = "[" + json_value + "]";
                    }
                    else 
                    {
                        json_value = "[" + json_value + ",\n" + json_fantasma + "]";
                    }
                    
                    
                    json = "¬" + json_value;                   
                }
            } 
            catch (Exception exx)
            {
                Console.Write(exx);
                json = null;
            }
            var message = json;
            return message;
        }

        public NdefMessage CreateNdefMessage(NfcEvent e)
        {           
            NdefMessage message = null;            
            try
            {
                get_box();
                var Messaje = get_box();
                NdefRecord Record = NdefRecord.CreateTextRecord(null,Messaje);                
                message = new NdefMessage(new[] { Record });
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                message = null;
            }
            return message;
        }
        [Android.Runtime.Register("invokeBeam", "(Landroid/app/Activity;)Z", "", ApiSince = 21)]
        [Android.Runtime.RequiresPermission("android.permission.NFC")]
        public void InvokeBeam(Activity activity)
        {}

        public void OnNdefPushComplete(NfcEvent e)
        {
            Console.WriteLine("ok");
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
                    var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
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
                try
                {
                    get_box();
                    var Messaje = get_box();

                    NdefMessage msg = new NdefMessage(
                    new NdefRecord[] { CreateMimeRecord (
                    "application/com.example.android.beam", Encoding.UTF8.GetBytes (Messaje))
                    });

                    NfcAdapter nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
                    if (nfcAdapter == null) return;  // NFC not available on this device
                    nfcAdapter.SetNdefPushMessage(msg, this);

                    nfcAdapter.SetNdefPushMessageCallback(null, this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
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

        public NdefRecord CreateMimeRecord(String mimeType, byte[] payload)
        {
            byte[] mimeBytes = Encoding.UTF8.GetBytes(mimeType);
            NdefRecord mimeRecord = new NdefRecord(
                NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
            return mimeRecord;
        }       

        public void InsertForeignData()
        {
            ForeingBox      foreingBox;
            ForeingProfile  foreingProfile;

            //Validar que la box no exista
           /* using(var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            {
                connSQLite.FindWithQuery<ForeingBox>("")
            }*/

            //Inicializar la box foranea
            foreingBox = new ForeingBox
            {
                BoxId = Convert.ToInt32(nfcData[0].boxId),
                UserId = Convert.ToInt32(nfcData[0].userId),
                //Time = Convert.ToDateTime(nfcData[0].time).ToUniversalTime(),
                Time = DateTime.Now,
                ImagePath = nfcData[0].imagePath,
                UserTypeId = Convert.ToInt32(nfcData[0].userTypeId),
                FirstName = nfcData[0].firstName,
                LastName = nfcData[0].lastName
                //11/30/2020 6:09:07 p. m.
                //11/9/2020 6:57:29 p. m.
                //11/30/2020 7:56:29 p. m.
                //11/30/2020 7:56:29 p. m.
                //11-23-2020 11:55:16 p. m.
            };

            //Insertar la box foranea
            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            {
                connSQLite.Insert(foreingBox);
            }

            //Recorrer la lista de perfiles para insertarlos
            foreach(Get_nfc get_nfc in nfcData)
            {
                if (get_nfc.boxId != "-") 
                {
                    foreingProfile = new ForeingProfile
                    {
                        BoxId = Convert.ToInt32(get_nfc.boxId),
                        UserId = Convert.ToInt32(get_nfc.userId),
                        ProfileName = get_nfc.profileName,
                        value = get_nfc.value,
                        ProfileType = get_nfc.ProfileType
                    };

                    //Insertar la box foranea
                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                    {
                        connSQLite.Insert(foreingProfile);
                    }
                }                
            }
            //Enviar a detalles de la box foranea cuando se inserta
            App.Current.MainPage = new Xamarin.Forms.NavigationPage(new Mynfo.Views.ForeingBoxPage(foreingBox, true));
        }








        // Set speed delay for monitoring changes.
        Xamarin.Essentials.SensorSpeed speed = Xamarin.Essentials.SensorSpeed.Game;

        public void DetectShakeTest()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Xamarin.Essentials.Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
        }

        void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // Process shake event
            mNfcAdapter.InvokeBeam(this);
            Console.WriteLine("jala");
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
    }
}