using FJYL.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FJYL.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkbtnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUserName.Text.Trim();
            var password = txtPassword.Text.Trim();

            if (username == "" || password == "")
            {
                WebUtil.RegisterStartupScript(Page, "alert('请输入用户名 / 密码');", true);
                return;
            }

            var isAuthenticate = UserManager.SignIn(username, password);
            if (isAuthenticate)
            {
                Session["IsAuthorized"] = true;

                Response.Redirect("/admin/welcome", true);
            }
            else
            {
                WebUtil.RegisterStartupScript(Page, "alert('用户名 / 密码 错误');", true);
            }
        }
    }
}