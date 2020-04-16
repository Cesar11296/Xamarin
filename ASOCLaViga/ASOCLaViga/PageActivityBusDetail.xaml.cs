using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageActivityBusDetail : ContentPage
    {
        public PageActivityBusDetail()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var message = "No permitido usted no esta registrado";
            DependencyService.Get<IMessage>().LongTime(message);
        }
    }
}