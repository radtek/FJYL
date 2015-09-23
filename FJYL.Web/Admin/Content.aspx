<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="FJYL.Web.Admin.Content" ValidateRequest="false" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <%: Scripts.Render("~/scripts/ckeditor/ckeditor.js") %>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p>
            <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="1000" Height="600" class="ckeditor"></asp:TextBox>
        </p>
    </div>
    <div style="padding-left:500px;">
        <asp:LinkButton runat="server" ID="lnkbtnSave" OnClick="lnkbtnSave_Click" CssClass="button"><span> 提 交 </span></asp:LinkButton>
    </div>

    <script type="text/javascript">
        CKEDITOR.replace('<%=txtContent.ClientID%>', { height: 800 });
    </script>
</asp:Content>

