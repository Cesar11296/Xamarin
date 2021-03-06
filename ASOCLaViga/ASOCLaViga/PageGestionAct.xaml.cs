﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGestionAct : ContentPage
    {
        public PageGestionAct()
        {
            InitializeComponent();
            LoadListAsync();
            lw_Act.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadListAsync();
            lw_Act.SelectedItem = null;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            OnAppearing();
        }

        private async Task LoadListAsync()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                Actividad act = (Actividad)this.BindingContext;
                List<Actividad> list = await FirebaseHelper.GetActivities();
                lw_Act.ItemsSource = list;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource2.Dispose();
            }
        }

        private void lw_Act_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                OnAlert(e);
            }
        }

        async void OnAlert(SelectedItemChangedEventArgs e)
        {
            try
            {
                string answer = await DisplayActionSheet("¿Qué desea hacer?", "Cancel", null, "Modificar", "Ver usuarios", "Eliminar");
                if (answer.Equals("Modificar"))
                {
                    PageChangeAct p = new PageChangeAct((Actividad)e.SelectedItem);
                    Navigation.PushModalAsync(p);
                }
                else if (answer.Equals("Eliminar"))
                {
                    int id = ((Actividad)e.SelectedItem).ID;
                    doDeleteAsync(id);
                }
                else if (answer.Equals("Ver usuarios"))
                {
                    Navigation.PushModalAsync(new PageShowUsers((Actividad)e.SelectedItem));
                }
            }
            catch (SystemException ex)
            {
                LoadListAsync();
            }
        }

        private async Task doDeleteAsync(int id)
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                await FirebaseHelper.DeleteActividad(id);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource2.Dispose();
                LoadListAsync();
            }
        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageCreateAct());
        }
    }
}