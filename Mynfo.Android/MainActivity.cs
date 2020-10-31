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
            onCreate();           
            LoadApplication(new App(dbRoot));
        }

        protected void HandleNFC(Intent intent, Boolean inForeground)
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
            /*try
            {                
                string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion))
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("select * from dbo.Boxes JOIN dbo.Box_ProfileSM on(dbo.Box_ProfileSM.BoxId= dbo.Boxes.BoxId) JOIN dbo.ProfileSMs on(dbo.Box_ProfileSM.Box_ProfileSMId = dbo.ProfileSMs.ProfileMSId) where dbo.Boxes.UserId = 11"); ;
                    string sql = sb.ToString();

                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var vari1_1 = reader.GetString(0);
                                var vari1_2 = reader.GetString(1);
                                var vari1_3 = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ec)
            {
                System.Console.WriteLine(ec);
            }*/

             
            DateTime time = DateTime.Now;
            var text = ("Beam me up!\n\n" +
                            "Beam Time: " + time.ToString("DD/MM/YYYY HH:mm:ss"));
            //string json = "Box recibida correctamente!";

            string json = "Box recibida correctamente!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +

                            "¡{"+ @"""nombre"":""Gerado"",""apellido_paterno"":""Daza"",""apellido_materno"":""Lopez"",""id_usuario"":""10253"",""id"":""253"",""url"":""https:www.facebook.com?id=243hi3r374h3""}";

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
                
                Console.WriteLine(msg);
                Console.WriteLine(msg);

                NfcAdapter nfcAdapter_call = NfcAdapter.GetDefaultAdapter(this);
                if (nfcAdapter_call == null) return;  // NFC not available on this device            

                NfcAdapter.ICreateNdefMessageCallback var_1 = null;
                nfcAdapter_call.SetNdefPushMessageCallback(var_1, this);                                           
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