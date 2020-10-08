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
using NetworkPOC.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkHelper))]
namespace NetworkPOC.Droid
{
    public class NetworkHelper : INetwork
    {
        Context context = Android.App.Application.Context;
        public bool IsConnected()
        {
            return NetworkConnectivity.IsConnected(context);
        }

        public bool IsConnectedFast()
        {
            return NetworkConnectivity.IsConnectedFast(context);
        }
    }
}