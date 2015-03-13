using FJYL.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FJYL.Web.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/admin/login");
                    return;
                }
            }
        }

        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {
            UserManager.SignOut();
            Response.Redirect("~/admin/login");
        }
    }
}