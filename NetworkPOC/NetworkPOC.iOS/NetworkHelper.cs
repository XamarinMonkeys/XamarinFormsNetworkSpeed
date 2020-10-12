using System;
using System.Threading.Tasks;
using NetworkPOC.iOS;
using System.Net.Http;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(NetworkHelper))]
namespace NetworkPOC.iOS
{
    public class NetworkHelper: INetwork
    {

        public bool IsConnected()
        {
            return true;
        }

        public bool IsConnectedFast()
        {
            
             CheckInternetSpeed();
            return true;

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
