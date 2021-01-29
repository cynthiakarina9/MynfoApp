using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Mynfo.Droid.Services
{
    public class MessageReceiver : BroadcastReceiver
    {
        public override async void OnReceive(Context context, Intent intent)
        {
            var message = intent.GetStringExtra("MSG_DATA");
         //   await App.DisplayAlertAsync(message);
        }
    }
}