<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageEdit.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.MessageEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑消息</title>
 <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="/resource/bsvd/dist/css/bootstrapValidator.css" />

    <script type="text/javascript" src="/resource/bsvd/vendor/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/dist/js/bootstrapValidator.js"></script>

</head>
<body>

    <form id="Form1" runat="server" method="post" data-bv-message="This value is not valid"
        data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
        data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
        data-bv-feedbackicons-validating="glyphicon glyphicon-refresh">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">发送消息</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="fromtable">
            <tr>
                <td class="titleTd">消息标题：</td>
                <td class="contentTd">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="请填写消息标题"></asp:TextBox>
                </td>
            </tr>
            <asp:Panel ID="palel" runat="server">
                <tr>
                    <td class="titleTd">接收对象：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drpTo" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="普通会员"></asp:ListItem>
                            <asp:ListItem Value="1" Text="代理用户"></asp:ListItem>
                            <asp:ListItem Value="3" Text="总代用户"></asp:ListItem>
                            <asp:ListItem Value="4" Text="总管用户"></asp:ListItem>
                            <asp:ListItem Value="5" Text="所有用户"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td class="titleTd">消息类型：</td>
                <td class="contentTd">
                    <asp:DropDownList ID="drpIsShowDialog" runat="server" CssClass="form-control autoBox">
                        <asp:ListItem Value="1" Text="系统消息"></asp:ListItem>
                        <asp:ListItem Value="2" Text="私人消息"></asp:ListItem>
                        <asp:ListItem Value="4" Text="中奖消息"></asp:ListItem>
                        <asp:ListItem Value="8" Text="充提信息"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleTd">消息内容：</td>
                <td class="contentTd">
                    <asp:TextBox ID="txtContent" runat="server" CssClass="form-control " Text="" Rows="8" TextMode="MultiLine" style="width:400px;margin:2px;margin-left:0px;"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="height: 20px;">
        </div>
        <asp:Button ID="btnSave" runat="server" class="submitbtn" Text="发送" OnClick="btnSave_Click" OnClientClick="return onSubmit();" Style="margin-left: 150px;" />
    </form>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
