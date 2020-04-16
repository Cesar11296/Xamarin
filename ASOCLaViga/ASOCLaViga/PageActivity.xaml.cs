﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageActivity : ContentPage
    {
        public PageActivity()
        {
            InitializeComponent();
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            List<Actividad> act = db.Query<Actividad>("SELECT * FROM Actividad where bus = false ");
            listView.ItemsSource = act;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}