using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TerminoHTML : ContentPage
    {
        public TerminoHTML()
        {
            InitializeComponent();
            WV.Source = "file:///android_asset/Termino.html";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
             Navigation.PopModalAsync();
        }
    }
}