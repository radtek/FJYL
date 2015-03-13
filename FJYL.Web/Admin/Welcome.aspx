<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="FJYL.Web.Admin.Welcome" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height:100px;">
        <p style="padding-top: 50px;">
            您好, <%=Page.User.Identity.Name %> !
        </p>
    </div>
</asp:Content>

