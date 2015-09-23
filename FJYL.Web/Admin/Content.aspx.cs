using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using FJYL.Web.Common;
using System.Text.RegularExpressions;

namespace FJYL.Web.Admin
{
    public partial class Content : System.Web.UI.Page
    {
        private const string Replace_Start = "<!--ReplaceStart-->";
        private const string Replace_End = "<!--ReplaceEnd-->";
        private const string Replace_Pattern = Replace_Start + @"[\s\S]*" + Replace_End;

        private string[] PageNames = new string[] { "about", "case", "job", "contact" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAuthorized"] == null)
            {
                Response.Redirect("/admin/login");
            }

            if (!Page.IsPostBack)
            {
                LoadContent();
            }
            //ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(lnkbtnSave);
        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            var content = HttpUtility.HtmlDecode(txtContent.Text);

            GenerateHtml(content);

            WebUtil.RegisterStartupScript(this.Page, "alert('保存成功！');", true);

            //var pageName = GetPageName();
            //var pagePath = GetPagePath(pageName);
            //Response.Redirect(pagePath, true);
        }

        #region Private Mothod

        private void LoadContent()
        {
            var pageName = GetPageName();
            if (!string.IsNullOrEmpty(pageName))
            {
                // 加载html到editor
                var pagePath = GetPagePath(pageName);
                var html = HtmlUtil.Read(pagePath);
                var content = Regex.Match(html, Replace_Pattern).Value.Replace(Replace_Start, "").Replace(Replace_End, "");
                txtContent.Text = content;
            }
        }

        private string GetPageName()
        {
            var segments = Request.GetFriendlyUrlSegments();
            var pageName = segments.Count > 0 ? segments[0] : PageNames[0];

            return pageName;
        }

        private string GetPagePath(string pageName)
        {
            return HttpContext.Current.Server.MapPath(string.Format("~/{0}.html", pageName));
        }

        private void GenerateHtml(string content)
        {
            var pageName = GetPageName();

            if (!string.IsNullOrEmpty(pageName))
            {
                var pagePath = GetPagePath(pageName);
                var html = HtmlUtil.Read(pagePath);
                string newHtml = Regex.Replace(html, Replace_Pattern, string.Format("{0} \r\n {1} \r\n {2}", Replace_Start, content, Replace_End));

                HtmlUtil.Write(pagePath, newHtml);
            }
        }

        #endregion
    }
}