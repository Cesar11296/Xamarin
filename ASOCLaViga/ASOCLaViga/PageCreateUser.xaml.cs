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
    public partial class PageCreateUser : ContentPage
    {
        string tipo = "";
        bool eName, eApellido, eDNI, ePicker;
        int opt;

        public PageCreateUser()
        {
            InitializeComponent();
        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            if (tipo.Length > 1)
            {
                if (tipo.Equals("Administrador"))
                {
                    opt = 1;
                }
                else if (tipo.Equals("Básico"))
                {
                    opt = 0;
                }
                doInsertAsync(opt);
                
            }
        }

        private async Task doInsertAsync(int opt)
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                await FirebaseHelper.AddUser(entryName.Text, entryApellidos.Text, entryDNI.Text, "12345", opt);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource2.Dispose();
                Navigation.PopModalAsync();
            }
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry texto = (Entry)sender;
            if (texto.FindByName("entryName").Equals(sender))
                eName = (entryName.Text.Length > 0);
            else if (texto.FindByName("entryApellidos").Equals(sender))
                eApellido = (entryApellidos.Text.Length > 0);
            else if (texto.FindByName("entryDNI").Equals(sender))
                eDNI = (entryDNI.Text.Length > 8) && (entryDNI.Text.Length < 10);
            Validar();
        }

        private void pickerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                tipo = (string)picker.ItemsSource[selectedIndex];
            }
            Validar();
        }

        private void Validar()
        {
            if (tipo.Length > 1)
            {
                ePicker = true;
                string visualState = (eName && eApellido && eDNI && ePicker) ? "Valido" : "NoValido";
                VisualStateManager.GoToState(bAdd, visualState);
            }
        }
    }
}