using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace NetworkPOC
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           // bool isConnected = DependencyService.Get<INetwork>().IsConnected();
            if (false)
            {
                bool isConnectionFast = DependencyService.Get<INetwork>().IsConnectedFast();
                if(isConnectionFast)
                    DependencyService.Get<IToast>().ShowToast("Network Connection is good");
                else
                    DependencyService.Get<IToast>().ShowToast("Network Connection is slow");
            }
            else
            {
                DependencyService.Get<IToast>().ShowToast("No Internet Connection");
            }
        }
    }
}
