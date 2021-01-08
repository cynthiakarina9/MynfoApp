using CoreNFC;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace Mynfo.iOS.Services
{
    class ReadTag : NSObject, INFCNdefReaderSessionDelegate
    {
        public string ErrorText { get; private set; }

        public static NFCNdefReaderSession _session;
        public static TaskCompletionSource<string> _tcs;

        public async Task<string> ScanAsync()
        {
            if (!NFCNdefReaderSession.ReadingAvailable)
            {
                throw new InvalidOperationException("Reading NDEF is not available");
            }

            _tcs = new TaskCompletionSource<string>();
            _session = new NFCNdefReaderSession(this, null, true);
            _session.BeginSession();

            return await _tcs.Task;
        }

        public void DidInvalidate(NFCNdefReaderSession session, NSError error)
        {
            Console.WriteLine("ServiceToolStandard DidInvalidate: " + error.ToString());
            _tcs.TrySetException(new Exception(error?.LocalizedFailureReason));
        }

        public void DidDetect(NFCNdefReaderSession session, NFCNdefMessage[] messages)
        {
            Console.WriteLine("ServiceToolStandard DidDetect msgs " + messages.Length);
            var bytes = messages[0].Records[0].Payload.Skip(3).ToArray();
            var message = Encoding.UTF8.GetString(bytes);
            Console.WriteLine("ServiceToolStandard DidDetect msg " + message);
            _tcs.SetResult(message);
        }

        public IntPtr Handle { get; }

        public void Dispose()
        {
            Console.WriteLine("ServiceToolStandard Dispose");
        }
    }
}