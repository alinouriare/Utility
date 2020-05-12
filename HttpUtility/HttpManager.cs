using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Fhs.Bulletin.E_Utility.HttpUtility
{
    public static class HttpManager
    {
        public static TOutput PostRequest<TInput, TOutput>(string url, TInput request)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string str = JsonConvert.SerializeObject(request);
                streamWriter.Write(str);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string end;
            using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                    end = streamReader.ReadToEnd();
            }
            return new JavaScriptSerializer().Deserialize<TOutput>(end);
        }
    }
}
