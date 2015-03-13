using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FJYL.Web.Common
{
    public static class JsonUtil
    {
        public static T GetJsonData<T>(string fileName)
        {
            var json = File.ReadAllText(fileName);

            return new JavaScriptSerializer().Deserialize<T>(json);
        }
    }
}