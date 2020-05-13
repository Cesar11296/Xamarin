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
    public partial class PageContact : ContentPage
    {
        public PageContact()
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
                List<User> listUser = await FirebaseHelper.GetAllUsers();
                var queryList = from us in listUser
                                where (us.type == 1)
                                select us;
                lw_Contact.ItemsSource = queryList;
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
    }
}