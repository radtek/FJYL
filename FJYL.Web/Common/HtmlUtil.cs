using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FJYL.Web.Common
{
    public static class HtmlUtil
    {
        public static string Read(string fileName)
        {
            var html = "";

            using (StreamReader stream = new StreamReader(fileName))
            {
                html = stream.ReadToEnd();
            }

            return html;
        }

        public static void Write(string fileName, string html)
        {
            StreamWriter sw = null;
            try
            {
                //string path = HttpContext.Current.Server.MapPath(fileName);
                sw = new StreamWriter(fileName, false);
                sw.Write(html);
                sw.Flush();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}