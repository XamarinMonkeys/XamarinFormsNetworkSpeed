using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Telephony;
using Java.IO;
using Java.Net;

namespace NetworkPOC.Droid
{
    public class NetworkConnectivity
    {
		public static NetworkInfo GetNetworkInfo(Context context)
		{
			ConnectivityManager cm = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
			return cm.ActiveNetworkInfo;
		}

		/**
	     * Check if there is any connectivity
	     * @param context
	     * @return
	     */
		public static bool IsConnected(Context context)
		{
			NetworkInfo info = NetworkConnectivity.GetNetworkInfo(context);
			return (info != null && info.IsConnected);
		}

		/**
	     * Check if there is any connectivity to a Wifi network
	     * @param context
	     // @param type
	     * @return
	     */
		public static bool IsConnectedWifi(Context context)
		{
			NetworkInfo info = NetworkConnectivity.GetNetworkInfo(context);
			return (info != null && info.IsConnected && info.Type == ConnectivityType.Wifi);
		}

		/**
	     * Check if there is any connectivity to a mobile network
	     * @param context
	     // @param type
	     * @return
	     */
		public static bool IsConnectedMobile(Context context)
		{
			NetworkInfo info = NetworkConnectivity.GetNetworkInfo(context);
			return (info != null && info.IsConnected && info.Type == ConnectivityType.Mobile);
		}

		/**
	     * Check if there is fast connectivity
	     * @param context
	     * @return
	     */
		public static bool IsConnectedFast(Context context)
		{
			NetworkInfo info = NetworkConnectivity.GetNetworkInfo(context);
			TelephonyManager tm = TelephonyManager.FromContext(context);
			return (info != null && info.IsConnected && NetworkConnectivity.IsConnectionFast(info.Type, tm.NetworkType));
		}

		/**
	     * Check if the connection is fast
	     * @param type
	     * @param subType
	     * @return
	     */
		public static bool IsConnectionFast(ConnectivityType type, NetworkType subType)
		{
			if (type == ConnectivityType.Wifi)
			{
				return true;
			}
			else if (type == ConnectivityType.Mobile)
			{
				switch (subType)
				{
					//case TelephonyManager.NETWORK_TYPE_1xRTT:
					case NetworkType.OneXrtt:
						return false; // ~ 50-100 kbps
									  //case TelephonyManager.NETWORK_TYPE_CDMA:
					case NetworkType.Cdma:
						return false; // ~ 14-64 kbps
									  //case TelephonyManager.NETWORK_TYPE_EDGE:
					case NetworkType.Edge:
						return false; // ~ 50-100 kbps
									  //case TelephonyManager.NETWORK_TYPE_EVDO_0:
					case NetworkType.Evdo0:
						return true; // ~ 400-1000 kbps
									 //case TelephonyManager.NETWORK_TYPE_EVDO_A:
					case NetworkType.EvdoA:
						return true; // ~ 600-1400 kbps
									 //case TelephonyManager.NETWORK_TYPE_GPRS:
					case NetworkType.Gprs:
						return false; // ~ 100 kbps
									  //case TelephonyManager.NETWORK_TYPE_HSDPA:
					case NetworkType.Hsdpa:
						return true; // ~ 2-14 Mbps
									 //case TelephonyManager.NETWORK_TYPE_HSPA:
					case NetworkType.Hspa:
						return true; // ~ 700-1700 kbps
									 //case TelephonyManager.NETWORK_TYPE_HSUPA:
					case NetworkType.Hsupa:
						return true; // ~ 1-23 Mbps
									 //case TelephonyManager.NETWORK_TYPE_UMTS:
					case NetworkType.Umts:
						return true; // ~ 400-7000 kbps
					/*
	                 * Above API level 7, make sure to set android:targetSdkVersion
	                 * to appropriate level to use these
	                 */
					//case TelephonyManager.NETWORK_TYPE_EHRPD: // API level 11
					case NetworkType.Ehrpd:
						return true; // ~ 1-2 Mbps
									 //case TelephonyManager.NETWORK_TYPE_EVDO_B: // API level 9
					case NetworkType.EvdoB:
						return true; // ~ 5 Mbps
									 //case TelephonyManager.NETWORK_TYPE_HSPAP: // API level 13
					case NetworkType.Hspap:
						return true; // ~ 10-20 Mbps
									 //case TelephonyManager.NETWORK_TYPE_IDEN: // API level 8
					case NetworkType.Iden:
						return false; // ~25 kbps
									  //case TelephonyManager.NETWORK_TYPE_LTE: // API level 11
					case NetworkType.Lte:
						return true; // ~ 10+ Mbps
									 // Unknown
									 //case TelephonyManager.NETWORK_TYPE_UNKNOWN:
					case NetworkType.Unknown:
						return false;
					default:
						return false;
				}
			}
			else
			{
				return false;
			}
		}

		public static bool IsHostReachable(string host)
		{
			if (string.IsNullOrEmpty(host))
				return false;

			bool isReachable = true;

			Thread thread = new Thread(() =>
			{
				try
				{
					//isReachable = InetAddress.GetByName(host).IsReachable(2000);

					/* 
					 * It's important to note that isReachable tries ICMP ping and then TCP echo (port 7).
					 * These are often closed down on HTTP servers.
					 * So a perfectly good working API with a web server on port 80 will be reported as unreachable
					 * if ICMP and TCP port 7 are filtered out!
					 */

					//if (!isReachable){
					URL url = new URL("http://" + host);

					URLConnection connection = url.OpenConnection();

					//if(connection.ContentLength != -1){
					//isReachable = true;
					if (connection.ContentLength == -1)
					{
						isReachable = false;
					}
					//}

				}
				catch (UnknownHostException e)
				{
					isReachable = false;
				}
				catch (IOException e)
				{
					isReachable = false;
				}

			});
			thread.Start();

			return isReachable;
		}

	}
}