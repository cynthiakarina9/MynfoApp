using CoreFoundation;
using CoreNFC;
using Foundation;
using Mynfo.iOS.Services;
using Mynfo.ViewModels;
using Mynfo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using static Mynfo.Views.TAGPage;

[assembly: Dependency(typeof(write_tag))]
namespace Mynfo.iOS.Services
{
    public class write_tag : NFCNdefReaderSessionDelegate, IBackgroundDependency
    {        
        public static NFCNdefReaderSession _tagSession;        
        public static TaskCompletionSource<string> _tcs;        
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
        public override void DidDetect(NFCNdefReaderSession session, NFCNdefMessage[] messages)
        {
            //add code here
        }        
        [Foundation.Export("readerSession:didDetectTags:")]
        [Foundation.Preserve(Conditional = true)]
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
                session.InvalidateSession();
                session.Dispose();                   
            });    
        }
        public static void CompletionHandler(NSError obj)
        {
            //add code here
        }
        public override void DidInvalidate(NFCNdefReaderSession session, NSError error)
        {
            //add code here
        }
        public void ExecuteCommand()
        {
            ScanWriteAsync();
        }
    }
}