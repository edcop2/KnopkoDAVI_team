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
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/webhooks/279983798077095936/gSgY0d6ycQyVdO1CZdv8OecuPo3Lk11WRIKU9LOc3QxwkjKF3GdifyDqkUmT-DLL3IaY");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                s = Console.ReadLine();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"username\":\"Professor\"," +
                                  "\"avatar_url\":\"http://asu.kh.ua/wp-content/uploads/2012/01/Guzhva.jpg \"," +
                                  "\"content\":\"" + s + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                    //namek

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
