﻿using System;
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

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace NetworkPOC.Droid
{
    public class ToastMessage : IToast
    {
        public void ShowToast(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}