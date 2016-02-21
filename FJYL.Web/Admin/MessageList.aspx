<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MessageList.aspx.cs" Inherits="FJYL.Web.Admin.MessageList" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="AdminContent/scripts/simplePagination/simplePagination.css" rel="stylesheet" />
    <script src="AdminContent/scripts/simplePagination/jquery.simplePagination.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <th>姓名</th>
                <th>主页</th>
                <th>QQ</th>
                <th>联系方式</th>
                <th>内容</th>
                <th>时间</th>
            </tr>
            <tbody id="tbodyMessage">
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="pager"></div>
    <script type="text/javascript">
        $(function () {
            $('#pager').pagination({
                items: 100,
                itemsOnPage: 10,
                cssStyle: 'light-theme',
                onPageClick: ClickPage
            });

            function ClickPage(pageNumber, event) {
                _message.getMessages(pageNumber);
            }

            _message.getMessages(1);
        })

        var _message = {
            getMessages: function (pageNumber) {
                $.get("/api/MessageBoard/" + pageNumber, function (res) {
                    console.log(res);
                    if (res.Success) {
                        $('#pager').pagination('updateItems', res.Data.RecordCount);
                        _message.bindMessage(res.Data.Messages);
                    } else {
                        console.log(res.Message);
                    }
                })
            },
            bindMessage: function (messages) {
                $('#tbodyMessage').empty();
                $.each(messages, function (index, item) {
                    var tr = '<tr>'
                            + '<td>' + item.Name + '</td>'
                            + '<td>' + item.Homepage + '</td>'
                            + '<td>' + item.QQ + '</td>'
                            + '<td>' + item.Phone + '</td>'
                            + '<td>' + item.Message + '</td>'
                            + '<td>' + item.CreateTime.replace('T',' ') + '</td>'
                            + '</tr>'
                    $('#tbodyMessage').append(tr);
                })
            },
        };
    </script>
</asp:Content>
