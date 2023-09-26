using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace Geo_Location
{
	class Program
	{
		static void Main(string[] args)
		{
			RootObject rootObject = getAddress(23.5270797, 77.2548046);
			Console.WriteLine("Address: " + rootObject.display_name);
		}

		public static RootObject getAddress(double lat, double lon)
		{
			WebClient webClient = new WebClient();
			webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			webClient.Headers.Add("Referer", "http://www.microsoft.com");
			var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon);
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
			RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));
			return rootObject;
		}
	}
}
