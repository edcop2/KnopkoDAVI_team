using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UltimateWuxiaDownloader
{
    class Program
    {
        static string getResponse(string uri)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    sb.Append(Encoding.Default.GetString(buf, 0, count));
                }
            }
            while (count > 0);
            return sb.ToString();
        }

        static string MainArticle(string htmlText)
        {
            string article = "";
            string temp = htmlText.Remove(0, htmlText.IndexOf("Next Chapter</a>"));
            temp = temp.Remove(0, temp.IndexOf("Chapter "));
            article = temp.Substring(0, temp.IndexOf("<hr/>"));
            return article;

        }

        static void Converter(string path)
        {
            var word = new Microsoft.Office.Interop.Word.Application();
            word.Visible = false;

            var filePath = path + ".html";
            var savePathDocx = path + ".docx";
            var wordDoc = word.Documents.Open(FileName: filePath, ReadOnly: false);
            wordDoc.SaveAs2(FileName: savePathDocx, FileFormat: WdSaveFormat.wdFormatXMLDocument);
            wordDoc.Close();
            word.Documents.Close();
        }


        static void Main(string[] args)
        {
            for (int i = 1270; i < 1319; i++)
            {
                string htmlResponse = getResponse("http://www.wuxiaworld.com/mga-index/mga-chapter-"+i+"/");
                string path = "d:\\mga\\mga-chapter"+i;
                string res = MainArticle(htmlResponse);
                string html = "<html><head><meta charset=\"UTF-8\"></head><body><p><strong> ";
                html += res;
                html += "</body></html> ";
                byte[] b = Encoding.Default.GetBytes(html);
                System.IO.File.WriteAllBytes(path+".html", b);
                Converter(path);
              //  Console.WriteLine(res);
            }
        }
    }
}
