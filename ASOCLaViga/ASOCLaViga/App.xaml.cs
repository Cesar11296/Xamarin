using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    public partial class App : Application
    {
        static User usLog;

        public static User u
        {
            get
            {
                return usLog;
            }
            set
            {
                usLog = value;
            }
        }

        public App()
        { 
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
