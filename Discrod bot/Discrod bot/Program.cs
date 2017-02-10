using System;
using System.IO;
using System.Net;

namespace Discrod_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            while (s != "EXIT")
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/webhooks/279675368674164736/bJ9tuXcpj7AzIJg5vETSttyza6_ly-RRbr0qRWbd4U97K5L1kFVla8TQjy5TfCNfNgdP");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                s = Console.ReadLine();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"username\":\"4to-to_s_Gujhvoi\"," +
                                  "\"avatar_url\":\"http://asu.kh.ua/wp-content/uploads/2012/01/Guzhva.jpg \"," +
                                  "\"content\":\"" + s + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
        }
    }
}
