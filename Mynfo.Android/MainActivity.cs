namespace Mynfo.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Nfc;
    using Android.Nfc.Tech;
    using Android.OS;
    using Android.Runtime;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;
    using Poz1.NFCForms.Abstract;
    using Poz1.NFCForms.Droid;
    using System;
    using System.IO;
    using System.Text;

    [Activity(Label = "Mynfo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    [IntentFilter(new[] { NfcAdapter.ActionTechDiscovered }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "application/com.companyname.Mynfo",DataScheme = "vnd.android.nfc", DataPathPrefix = "/com.Mynfo:letypetype", DataHost = "ext")]    

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

        public NfcAdapter NFCdevice;
        public NfcAdapter nfcAdapter_call;        
        private NfcAdapter.ICreateNdefMessageCallback callback;
        private NdefMessage ndefMessage;
        public NfcAdapter mNfcAdapter;
        public NfcForms x;        
        public string nfc_mesage_;

        


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
            if (mNfcAdapter == null)
            {
                Console.WriteLine("");
            }
            else
            {
                // Register callback to set NDEF message
                mNfcAdapter.SetNdefPushMessageCallback(null, this);
                // Register callback to listen for message-sent success
                mNfcAdapter.SetOnNdefPushCompleteCallback(null, this);
            }
            //Color de tema
            Xamarin.Forms.Forms.SetFlags("AppTheme_Experimental");
            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);            
            onCreate();           
            LoadApplication(new App(dbRoot));
        }
        [Android.Runtime.Register("ACTION_NDEF_DISCOVERED", ApiSince = 10)]
        //public string ActionNdefDiscovered;
        IntentFilter[] nfcIntentFiltersArray;
        PendingIntent nfcPendingIntent;
        protected override void OnResume()
        {
            base.OnResume();
            try
            {
                if (NfcAdapter.ActionNdefDiscovered == Intent.Action)
                {
                    IParcelable[] rawMsgs = Intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
                    // only one message sent during the beam
                    NdefMessage msg = (NdefMessage)rawMsgs[0];
                    // record 0 contains the MIME type, record 1 is the AAR, if present
                    nfc_mesage_ = Encoding.UTF8.GetString(msg.GetRecords()[0].GetPayload());
                }                         
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }                      
        } 
       
        protected override void OnPause()
        {
            base.OnPause();
            try
            {
                NFCdevice.DisableForegroundDispatch(this);                
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
            string json = "Box recibida correctamente!";
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

                var vari = nfcAdapter_call;

                nfcAdapter_call.SetNdefPushMessageCallback(null, this);                                
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