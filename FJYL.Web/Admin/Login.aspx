<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FJYL.Web.Admin.Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FJYL - 后台登陆</title>
    <%: Styles.Render("~/Admin/AdminContent/css/style.css") %>
    <%: Scripts.Render("~/scripts/jquery-1.8.3.min.js") %>
    <%: Scripts.Render("~/admin/adminContent/scripts/admin.js") %>
</head>
<body>
    <div id="wrapper_login">
	    <div id="menu">
		    <div id="left"></div>
		    <div id="right"></div>
		    <h2>LJ 管理员登陆</h2>
		    <div class="clear"></div>		
	    </div>
	    <div id="desc">
		    <div class="body">
			    <div class="col w10 last bottomlast">
				    <form id="form1" runat="server">
                        <asp:scriptmanager runat="server"></asp:scriptmanager>
					    <p>
						    <label for="username">用户名:</label>
                            <asp:TextBox runat="server" ID="txtUserName" CssClass="text" size="40"></asp:TextBox>
						    <br />
					    </p>
					    <p>
						    <label for="password">密码:</label>
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="text" size="40"></asp:TextBox>
						    <br />
					    </p>
					    <p class="last">
                            <asp:LinkButton runat="server" ID="lnkbtnLogin" CssClass="button form_submit" OnClick="lnkbtnLogin_Click">
                                <small class="icon play"></small><span>登陆</span>
                            </asp:LinkButton>
						    <br />
					    </p>
					    <div class="clear"></div>
				    </form>
			    </div>
			    <div class="clear"></div>
		    </div>
		    <div class="clear"></div>
		    <div id="body_footer">
			    <div id="bottom_left"><div id="bottom_right"></div></div>
		    </div>
	    </div>		
    </div>  
    <script type="text/javascript">
        $(function () {
            $("#<%=lnkbtnLogin.ClientID%>").click(function (e) {
                var username = $.trim($("#<%=txtUserName.ClientID%>").val());
                var password = $.trim($("#<%=txtPassword.ClientID%>").val());
                if (username === '' || password === '') {
                    alert('请输入用户名 / 密码');
                    e.preventDefault();
                }
            })
            $.submitOnEnterKey("<%=txtPassword.ClientID%>", "<%=lnkbtnLogin.ClientID%>");
        })
    </script>
</body>
</html>
