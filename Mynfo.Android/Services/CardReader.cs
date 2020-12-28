using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using Mynfo.Domain;
using Mynfo.Models;
using Mynfo.Services;
using Mynfo.ViewModels;
using Mynfo.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mynfo.Droid.Services
{
    public class CardReader : Java.Lang.Object, NfcAdapter.IReaderCallback
    {
        // ISO-DEP command HEADER for selecting an AID.
        // Format: [Class | Instruction | Parameter 1 | Parameter 2]
        private static readonly byte[] SELECT_APDU_HEADER = new byte[] { 0x00, 0xA4, 0x04, 0x00 };

        // AID for our loyalty card service.
        private static readonly string SAMPLE_LOYALTY_CARD_AID = "F123456789";  //F123456789

        // "OK" status word sent in response to SELECT AID command (0x9000)
        private static readonly byte[] SELECT_OK_SW = new byte[] { 0x90, 0x00 };

        ApiService apiService = new ApiService();

        public async void OnTagDiscovered(Tag tag)
        {
            IsoDep isoDep = IsoDep.Get(tag);      
            
                
            
            
//            NfcA nfcA = NfcA.Get(tag);
          
//            //byte[] response = nfcA.Transceive(new byte[] { (byte)0x30, (byte)0x00 });

//            nfcA.Connect();            

//            var aidLength2 = (byte)(SAMPLE_LOYALTY_CARD_AID.Length / 2);
//            var aidBytes2 = StringToByteArray(SAMPLE_LOYALTY_CARD_AID);          
//            var command2 = SELECT_APDU_HEADER
//                        .Concat(new byte[] { aidLength2 })
//                        .Concat(aidBytes2)
//                        .ToArray();

//            var result2 = nfcA.Transceive(new byte[] {
//  (byte)0x30,  /* CMD = READ */
//  (byte)0x10   /* PAGE = 16  */
//});

//            string TagUid = ByteArrayToString(result2);

//            var resultLength2 = result2.Length;
//            byte[] statusWord2 = { result2[resultLength2 - 2], result2[resultLength2 - 1] };
//            var payload2 = new byte[resultLength2 - 2];
//            Array.Copy(result2, payload2, resultLength2 - 2);
//            var arrayEquals2 = SELECT_OK_SW.Length == statusWord2.Length;
//            var msg2 = Encoding.UTF8.GetString(payload2);
//            if (Enumerable.SequenceEqual(SELECT_OK_SW, statusWord2))
//            {
//                var msg = Encoding.UTF8.GetString(payload2);
//                await App.DisplayAlertAsync(msg);
//            }  
      

            
              
            if (isoDep != null)
            {
                try
                {
                    isoDep.Connect();

                    var aidLength = (byte)(SAMPLE_LOYALTY_CARD_AID.Length / 2);
                    var aidBytes = StringToByteArray(SAMPLE_LOYALTY_CARD_AID);
                    var command = SELECT_APDU_HEADER
                        .Concat(new byte[] { aidLength })
                        .Concat(aidBytes)
                        .ToArray();

                    var result = isoDep.Transceive(command);
                    var resultLength = result.Length;
                    byte[] statusWord = { result[resultLength - 2], result[resultLength - 1] };
                    var payload = new byte[resultLength - 2];
                    Array.Copy(result, payload, resultLength - 2);
                    var arrayEquals = SELECT_OK_SW.Length == statusWord.Length;

                    if (Enumerable.SequenceEqual(SELECT_OK_SW, statusWord))
                    {
                        var msg = Encoding.UTF8.GetString(payload);
                       
                        try 
                        {

                            
                            string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
                            string queryLastBoxCreated =

                            "select dbo.Users.FirstName, dbo.Users.LastName, dbo.Users.UserTypeId, dbo.Users.ImagePath, dbo.Boxes.BoxId from dbo.Users " +
                            "join dbo.Boxes on(dbo.Boxes.UserId = dbo.Users.UserId) " +
                            " where dbo.Users.UserId = " + msg +
                            " and dbo.Boxes.BoxDefault = 1";

                            System.Text.StringBuilder sb;

                            using (SqlConnection connection = new SqlConnection(cadenaConexion))
                            {
                                sb = new System.Text.StringBuilder();
                                sb.Append(queryLastBoxCreated);

                                string sql = sb.ToString();

                                using (SqlCommand command3 = new SqlCommand(sql, connection))
                                {
                                    connection.Open();
                                    using (SqlDataReader reader = command3.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {                                                                                       
                                            string get_FirstName = (string)reader["FirstName"];
                                            string  get_LastName = (string)reader["LastName"];
                                            string get_ImagePath = reader["ImagePath"].ToString();
                                            int get_box_id = (int)reader["BoxId"];
                                            int UserTypeId_get = (int)reader["UserTypeId"];

                                            //nfcData = (List<Get_nfc>)JsonConvert.DeserializeObject("", typeof(List<Get_nfc>));

                                            InsertForeignData(msg, get_box_id);

                                            //ForeingBox foreingBox;

                                            //foreingBox = new ForeingBox
                                            //{
                                            //    TagID = 001,
                                            //    UserId = Convert.ToInt32(msg),
                                            //    Time = DateTime.Now,
                                            //    BoxId = get_box_id,
                                            //    FirstName = get_FirstName,
                                            //    LastName = get_LastName,
                                            //    ImagePath = get_ImagePath,
                                            //    UserTypeId = UserTypeId_get
                                            //};                                            
                                        }
                                    }

                                    connection.Close();
                                }
                            }
                        }
                        catch (Exception ex) 
                        {
                            Console.WriteLine(ex);
                        }

                       
                        //await App.DisplayAlertAsync(msg);
                    }
                }
                catch (Exception e)
                {
                    await App.DisplayAlertAsync("Error communicating with card: " + e.Message);
                }
            }
        }

        private static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] StringToByteArray(string hex) =>
            Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();



        //Insercion NFC

        //public List<Get_nfc> nfcData = new List<Get_nfc>();

        public async void InsertForeignData(string user_id, int box_id)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            User box_detail = new User();
            box_detail = await apiService.GetUserId(apiSecurity,
                                                "/api",
                                                "/Users",
                                                Convert.ToInt32(user_id));

            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            {
                connSQLite.CreateTable<ForeingProfile>();
            }

            ForeingBox foreingBox;
            ForeingProfile foreingProfile;

            //Validar que la box no exista
            //using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            //{
            //    connSQLite.CreateTable<ForeingBox>();
            //}



            //Inicializar la box foranea
            foreingBox = new ForeingBox
            {
                BoxId = box_id,
                UserId = Convert.ToInt32(user_id),
                //Time = Convert.ToDateTime(nfcData[0].time).ToUniversalTime(),
                Time = DateTime.Now,
                ImagePath = box_detail.ImagePath,
                UserTypeId = box_detail.UserTypeId,
                FirstName = box_detail.FirstName,
                LastName = box_detail.LastName

            };

            //Insertar la box foranea
            using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
            {
                connSQLite.Insert(foreingBox);
            }
            try 
            {
                if (box_id != 0)
                {
                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                    {
                        connSQLite.CreateTable<Profile_get>();
                    }


                    System.Text.StringBuilder sb;
                    string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
                    //Creación de perfiles locales de box local
                    string queryGetBoxEmail = "select * from dbo.ProfileEmails " +
                                    "join dbo.Box_ProfileEmail on" +
                                    "(dbo.ProfileEmails.ProfileEmailId = dbo.Box_ProfileEmail.ProfileEmailId) " +
                                    "where dbo.Box_ProfileEmail.BoxId = " + box_id;
                    string queryGetBoxPhone = "select * from dbo.ProfilePhones " +
                                                "join dbo.Box_ProfilePhone on" +
                                                "(dbo.ProfilePhones.ProfilePhoneId = dbo.Box_ProfilePhone.ProfilePhoneId) " +
                                                "where dbo.Box_ProfilePhone.BoxId = " + box_id;
                    string queryGetBoxSMProfiles = "select * from dbo.ProfileSMs " +
                                                    "join dbo.Box_ProfileSM on" +
                                                    "(dbo.ProfileSMs.ProfileMSId = dbo.Box_ProfileSM.ProfileMSId) " +
                                                    "join dbo.RedSocials on(dbo.ProfileSMs.RedSocialId = dbo.RedSocials.RedSocialId) " +
                                                    "where dbo.Box_ProfileSM.BoxId = " + box_id;

                    //Consulta para obtener perfiles email
                    using (SqlConnection conn1 = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetBoxEmail);

                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, conn1))
                        {
                            conn1.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreingProfile = new ForeingProfile
                                    {
                                        BoxId = box_id,
                                        UserId = (int)reader["UserId"],
                                        ProfileName = (string)reader["Name"],
                                        value = (string)reader["Email"],
                                        ProfileType = "Email"
                                    };
                                    //Crear perfil de correo de box local predeterminada
                                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        connSQLite.Insert(foreingProfile);
                                    }
                                }
                            }

                            conn1.Close();
                        }
                    }

                    //Consulta para obtener perfiles teléfono
                    using (SqlConnection conn1 = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetBoxPhone);

                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, conn1))
                        {
                            conn1.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreingProfile = new ForeingProfile
                                    {
                                        BoxId = box_id,
                                        UserId = (int)reader["UserId"],
                                        ProfileName = (string)reader["Name"],
                                        value = (string)reader["Number"],
                                        ProfileType = "Phone"
                                    };
                                    //Crear perfil de teléfono de box local predeterminada
                                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        connSQLite.Insert(foreingProfile);
                                    }
                                }
                            }

                            conn1.Close();
                        }
                    }

                    //Consulta para obtener perfiles de redes sociales
                    using (SqlConnection conn1 = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetBoxSMProfiles);

                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, conn1))
                        {
                            conn1.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foreingProfile = new ForeingProfile
                                    {
                                        BoxId = box_id,
                                        UserId = (int)reader["UserId"],
                                        ProfileName = (string)reader["ProfileName"],
                                        value = (string)reader["link"],
                                        ProfileType = (string)reader["Name"]
                                    };
                                    //Crear perfil de teléfono de box local predeterminada
                                    using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                                    {
                                        connSQLite.Insert(foreingProfile);
                                    }
                                }
                            }

                            conn1.Close();
                        }
                    }
                }
                //Recorrer la lista de perfiles para insertarlos

                /*int coun = 0;
                var Profile = new Profile_get();
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    coun = conn.Table<Profile_get>().Count();

                    for (int k = 0; k < coun; k++)
                    {
                        Profile = conn.Table<Profile_get>().ElementAt(k);
                        foreingProfile = new ForeingProfile
                        {
                            BoxId = box_id,
                            UserId = Convert.ToInt32(user_id),
                            ProfileName = Profile.ProfileName,
                            value = Profile.value,
                            ProfileType = Profile.ProfileType
                        };

                        //Insertar la box foranea
                        using (var connSQLite = new SQLite.SQLiteConnection(App.root_db))
                        {
                            connSQLite.Insert(foreingProfile);
                        }
                    }
                }*/


                MainViewModel.GetInstance().ForeingBox = new ForeingBoxViewModel();
                //Application.Current.MainPage.Navigation.PushAsync(new ForeingBoxPage(foreingBox, true));
                App.Navigator.PushAsync(new ForeingBoxPage(foreingBox, true)); 
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            } 

           



            //Enviar a detalles de la box foranea cuando se inserta
            //App.Current.MainPage = new Xamarin.Forms.NavigationPage(new Mynfo.Views.ForeingBoxPage(foreingBox, true));
        }
    }
}