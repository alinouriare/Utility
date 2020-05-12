using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Fhs.Bulletin.E_Utility.CrawlUtility.Models;

namespace Fhs.Bulletin.E_Utility.CrawlUtility
{
    public class WebCrawlerUtilities
    {
        #region variables
        #endregion

        #region Constructors
        #endregion

        #region Static Methods
        public static string DownloadFile(string url, string outputPath, out Exception exc)
        {
            try
            {
                string fileName = GetFileName(url);
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), outputPath);
                }
                exc = null;
                return outputPath;
            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }

        public static List<ImageLink> CrawlImages(string url, out Exception exc)
        {
            try
            {
                string html = "";
                var listOfImgdata = new List<ImageLink>();
                using (WebClient client = new WebClient())
                {
                    html = client.DownloadString(url);
                }

                Uri uri = new Uri(url);
                string prefix = uri.Scheme + "://" + uri.Host + ":" + uri.Port + "/";

                MatchCollection matchesImgSrc = Regex.Matches(html, @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                foreach (Match m in matchesImgSrc)
                {
                    string href = m.Groups[1].Value;
                    string addr = href;
                    if (!href.ToLower().StartsWith("http") &&
                        !href.ToLower().StartsWith("https"))
                        href = prefix + href;
                    var imgInfo = GetImageInfo(href);
                    if (imgInfo == null) continue;
                    imgInfo.AddressInSite = addr;
                    listOfImgdata.Add(imgInfo);
                }

                exc = null;
                return listOfImgdata;
            }
            catch (Exception ex)
            {
                exc = ex;
                return new List<ImageLink>();
            }
        }

        public static List<string> CrawlImagesFast(string url, out Exception exc)
        {
            try
            {
                string html = "";
                var listOfImgdata = new List<string>();
                using (WebClient client = new WebClient())
                {
                    html = client.DownloadString(url);
                }

                Uri uri = new Uri(url);
                string prefix = uri.Scheme + "://" + uri.Host + ":" + uri.Port + "/";

                MatchCollection matchesImgSrc = Regex.Matches(html, @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                foreach (Match m in matchesImgSrc)
                {
                    string href = m.Groups[1].Value;
                    if (!href.ToLower().StartsWith("http") &&
                        !href.ToLower().StartsWith("https"))
                        href = prefix + href;
                    listOfImgdata.Add(href);
                }

                exc = null;
                return listOfImgdata;
            }
            catch (Exception ex)
            {
                exc = ex;
                return new List<string>();
            }
        }
        #endregion

        #region Helper Methods
        private static string GetFileName(string url)
        {
            int index = url.LastIndexOf("/");
            if (index < 0)
                index = url.LastIndexOf("\\");
            if (index < 0)
                throw new Exception("Invalid Url");
            string fileName = url.Substring(index + 1, url.Length - index - 1);
            return fileName;
        }

        public static ImageLink GetImageInfo(string url)
        {
            try
            {
                System.Net.WebRequest req = System.Net.HttpWebRequest.Create(url);
                req.Method = "HEAD";
                ImageLink obj = new ImageLink();
                int ContentLength = 0;
                using (System.Net.WebResponse resp = req.GetResponse())
                {
                    int.TryParse(resp.Headers.Get("Content-Length"), out ContentLength);
                    obj.FileSizeBytes = ContentLength;
                    obj.FileSizeKB = ContentLength > 0 && ContentLength > 1024 ? ContentLength / 1024 : 0;
                    obj.FileType = resp.Headers.Get("Content-Type") ?? "";
                    obj.Date = resp.Headers.Get("Date") ?? "";
                    obj.LastModified = resp.Headers.Get("Last-Modified") ?? "";
                }
                obj.FileName = GetFileName(url);
                obj.Link = url;
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
