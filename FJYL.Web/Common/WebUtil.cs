using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace FJYL.Web.DbManager
{
    public static class WebUtil
    {
        public static void RegisterStartupScript(Control ctrl, Type type, string key, string script, bool addScriptTags)
        {
            if (ScriptManager.GetCurrent(ctrl.Page) != null && ScriptManager.GetCurrent(ctrl.Page).IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(ctrl, type, key, script, addScriptTags);
            }
            else
            {
                ctrl.Page.ClientScript.RegisterStartupScript(type, key, string.Format("$(document).ready(function ($){{{0}}});", script), addScriptTags);
            }
        }

        public static void RegisterStartupScript(Page page, string script, bool addScriptTags)
        {
            var type = page.GetType();
            var key = Guid.NewGuid().ToString("N");

            if (ScriptManager.GetCurrent(page) != null && ScriptManager.GetCurrent(page).IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(page, type, key, script, addScriptTags);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(type, key, string.Format("$(document).ready(function ($){{{0}}});", script), addScriptTags);
            }
        }

        public static void RegisterStartupScript(Page page, Type type, string key, string script, bool addScriptTags)
        {
            if (ScriptManager.GetCurrent(page) != null && ScriptManager.GetCurrent(page).IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(page, type, key, script, addScriptTags);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(type, key, string.Format("$(document).ready(function ($){{{0}}});", script), addScriptTags);
            }
        }
    }
}