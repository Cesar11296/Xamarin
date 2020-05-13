using SQLite;
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
    public partial class PageActivity : ContentPage
    {
        public PageActivity()
        {
            InitializeComponent();
            loadListAsync();
        }

        private async Task loadListAsync()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                List<Actividad> list = await FirebaseHelper.GetActivities();
                var listFilter = from act in list
                                 where (act.bus == "No")
                                 select act;
                listView.ItemsSource = listFilter;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.SelectedItem = null;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PageActivityDetail p = new PageActivityDetail();
                p.BindingContext = e.SelectedItem;
                Navigation.PushModalAsync(p);
            }
        }
    }
}