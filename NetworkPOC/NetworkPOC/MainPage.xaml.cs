using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Http;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                ///To Do: Following Code for Android Native Method
                //bool isConnectionFast = DependencyService.Get<INetwork>().IsConnectedFast();
                //if (isConnectionFast)
                //    DependencyService.Get<IToast>().ShowToast("Network Connection is good");
                //else
                //    DependencyService.Get<IToast>().ShowToast("Network Connection is slow");

                ///To Do following method supports both
                var speed=await CheckInternetSpeed();
                
            }
            else
            {
                DependencyService.Get<IToast>().ShowToast("No Internet Connection");
            }
        }

        public async Task<string> CheckInternetSpeed()
        {
            DateTime dt1 = DateTime.Now;
            string internetSpeed;
            try
            {
                var client = new HttpClient();
                //Number Of Bytes Downloaded Are Stored In ‘data’
                byte[] data = await client.GetByteArrayAsync("http://xamarinmonkeys.blogspot.com/");
                //DateTime Variable To Store Download End Time.
                DateTime dt2 = DateTime.Now;
                //To Calculate Speed in Kb Divide Value Of data by 1024 And Then by End Time Subtract Start Time To Know Download Per Second.
                Console.WriteLine("ConnectionSpeed: DataSize (kb) " + data.Length / 1024);
                Console.WriteLine("ConnectionSpeed: ElapsedTime (secs) " + (dt2 - dt1).TotalSeconds);
                internetSpeed = "ConnectionSpeed: (kb/s) " + Math.Round((data.Length / 1024) / (dt2 - dt1).TotalSeconds, 2);
            }
            catch (Exception ex)
            {
                internetSpeed = "ConnectionSpeed:Unknown Exception-" + ex.Message;
            }
            Console.WriteLine(internetSpeed);
            return internetSpeed;
        }
    }
}
