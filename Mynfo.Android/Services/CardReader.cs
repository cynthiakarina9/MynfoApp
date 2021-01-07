using Android.Nfc;
using Android.Nfc.Tech;
using Mynfo.Services;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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

                        Imprime_box.Consulta_user(msg);
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
    }
}