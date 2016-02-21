using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FJYL.Web.DbManager
{
    public static class AccessSqlUtil
    {
        public static string GetStringValue(string val)
        {
            return val != null ? val : string.Empty;
        }
    }
}