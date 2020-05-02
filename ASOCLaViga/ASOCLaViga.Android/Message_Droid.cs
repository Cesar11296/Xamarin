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
using ASOCLaViga.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Message_Droid))]
namespace ASOCLaViga.Droid
{
    public class Message_Droid : IMessage
    {
        public void LongTime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortTime(string message)
        {
           Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }

        public void Up(string message)
        {
            Toast t = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);
            t.SetGravity(GravityFlags.Top | GravityFlags.Center, 0, 0);
            t.Show();
        }
    }
}