using CoreFoundation;
using CoreNFC;
using Foundation;
using Mynfo.iOS.Services;
using Mynfo.ViewModels;
using Mynfo.Views;
using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using static Mynfo.Views.TAGPage;

[assembly: Dependency(typeof(leerTag))]
namespace Mynfo.iOS.Services
{
    public class leerTag : NFCNdefReaderSessionDelegate, INFCNdefReaderSessionDelegate
    {      
        public NFCNdefReaderSession Session { get; set; }

        public void invoke_lector()
        {
            InvokeOnMainThread(() =>
            {
                Session = new NFCNdefReaderSession(this, null, true);
                if (Session != null)
                {
                    Session.BeginSession();
                }
            });
        }

        public override void DidDetect(NFCNdefReaderSession session, NFCNdefMessage[] messages)
        {
            int user_id = 0;

            try
            {
                if (messages != null && messages.Length > 0)
                {
                    var first = messages[0];
                    string messa = GetRecords(first.Records);
                    string[] variables = messa.Split('=');
                    string[] depura_userid = variables[1].Split('&');
                    string tag_id = variables[2];
                    user_id = Convert.ToInt32(depura_userid[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            AppDelegate.user_id_tag = user_id.ToString();
            write_tag myobject = new write_tag();
            Thread.Sleep(5000);
            myobject.ScanWriteAsync();           
        }

        public override void DidInvalidate(NFCNdefReaderSession session, NSError error)
        {

            var readerError = (NFCReaderError)(long)error.Code;

            if (readerError != NFCReaderError.ReaderSessionInvalidationErrorFirstNDEFTagRead &&
                readerError != NFCReaderError.ReaderSessionInvalidationErrorUserCanceled)
            {

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
    }
}