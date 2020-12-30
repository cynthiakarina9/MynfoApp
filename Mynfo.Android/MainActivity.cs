namespace Mynfo.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Nfc;
    using Android.OS;
    using Android.Runtime;
    using Models;
    using Newtonsoft.Json;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Mynfo.ViewModels;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using SQLite;
    using Android.Graphics.Drawables;
    using System.Globalization;
    using Mynfo.Droid.Services;
    using System.Threading;

    [Activity(Label = "Mynfo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait), IntentFilter(new[] { "android.nfc.action.TECH_DISCOVERED" },
    Categories = new[] { "android.intent.category.DEFAULT" })]
    [MetaData("android.nfc.action.TECH_DISCOVERED", Resource = "@xml/techlist")]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public string json;
        public CardReader cardReader;
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
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            //Color de tema
            Xamarin.Forms.Forms.SetFlags("AppTheme_Experimental");
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            base.OnCreate(savedInstanceState);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //ShortcutBadger.ApplyCount();            

            DetectShakeTest();
            ToggleAccelerometer();

            var receiver = new MessageReceiver();
            RegisterReceiver(receiver, new IntentFilter("MSG_NAME"));
            cardReader = new CardReader();
            LoadApplication(new App(dbRoot));

            if (Intent?.Extras != null)
            {
                var message = Intent.Extras.GetString("MSG_DATA");
                await App.DisplayAlertAsync(message);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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
                if (nfc != null) nfc.DisableReaderMode(this);
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
                if (NfcAdapter.ActionTechDiscovered.Equals(Intent.Action))
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
                            string result = data.Substring(3);
                            Imprime_box.Consulta_user(result);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
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






        //Insercion NFC

        /*
          public void InsertForeignData()
          {
              ForeingBox      foreingBox;
              ForeingProfile  foreingProfile;

              //Validar que la box no exista
              //using(var connSQLite = new SQLite.SQLiteConnection(App.root_db))
              //{
              //    connSQLite.FindWithQuery<ForeingBox>("")
              //}

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
      */

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
            EnableReaderMode();
            Thread.Sleep(2000);
            DisableReaderMode();
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