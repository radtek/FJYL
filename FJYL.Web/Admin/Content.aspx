<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="FJYL.Web.Admin.Content" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p>
            <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="100%" Height="600"></asp:TextBox>
        </p>
    </div>
    <div>
        <asp:LinkButton runat="server" ID="lnkbtnSave" OnClick="lnkbtnSave_Click">提交</asp:LinkButton>
    </div>

    <script type="text/javascript">
        window.UEDITOR_HOME_URL = '/Scripts/UEditor/';
        var ue = UE.getEditor('<%=txtContent.ClientID%>');
    </script>

</asp:Content>

