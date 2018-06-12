<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateUserPwd.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Customer.UpdateUserPwd" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>编辑用户</title>
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="/resource/bsvd/dist/css/bootstrapValidator.css" />

    <script type="text/javascript" src="/resource/bsvd/vendor/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/vendor/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="defaultForm" runat="server" method="post" data-bv-message="This value is not valid"
        data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
        data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
        data-bv-feedbackicons-validating="glyphicon glyphicon-refresh">

        <!-- Nav tabs -->
        <ul class="nav nav-tabs">
            <li class="active"><a href="#home" data-toggle="tab">修改登陆密码</a>
            </li>
            <li><a href="#profile" data-toggle="tab">修改资金密码</a>
            </li>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div class="tab-pane fade in active" id="home">
                <table class="fromtable" id="lgoinpwd">
                    <tr>
                        <td class="titleTd">登录名：</td>
                        <td class="contentTd">
                            <p class="help-block">
                                <asp:TextBox ID="txtCode" runat="server" Enabled="false" CssClass="form-control autoBox" required data-bv-notempty-message="登录名由字母或数字组成的6-16个字符" pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录名由字母或数字组成的6-16个字符" data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录名由字母或数字组成的6-16个字符"></asp:TextBox>
                                &nbsp;&nbsp;由字母或数字组成的6-16个字符.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleTd">登录密码：</td>
                        <td class="contentTd">
                            <p class="help-block">
                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                                    pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                                    data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>
                                &nbsp;&nbsp;由字母或数字组成的6-16个字符.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleTd">确认密码：</td>
                        <td class="contentTd">
                            <p class="help-block">
                                <asp:TextBox ID="txtRePwd" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                                    pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                                    data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>
                                &nbsp;&nbsp;由字母或数字组成的6-16个字符.
                            </p>
                        </td>
                    </tr>
                </table>
                <div style="height: 20px;">
                </div>
                <asp:Button ID="btnSave" runat="server" class="submitbtn" Text="修改" OnClick="btnSave_Click" Style="margin-left: 150px;" OnClientClick="return updatyePwd();" />
            </div>
            <div class="tab-pane fade" id="profile">
                <table class="fromtable" id="zijpwd">
                    <tr>
                        <td class="titleTd">登录名：</td>
                        <td class="contentTd">
                            <p class="help-block">
                                <asp:TextBox ID="txtcodeZij" runat="server" Enabled="false" CssClass="form-control autoBox" required data-bv-notempty-message="登录名由字母或数字组成的6-16个字符" pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录名由字母或数字组成的6-16个字符" data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录名由字母或数字组成的6-16个字符"></asp:TextBox>
                                &nbsp;&nbsp;由字母或数字组成的6-16个字符.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleTd">资金密码：</td>
                        <td class="contentTd">
                            <p class="help-block">
                                <asp:TextBox ID="txtZiJPwd" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                                    pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                                    data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>
                                &nbsp;&nbsp;由字母或数字组成的6-16个字符.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleTd">确认密码：</td>
                        <td class="contentTd">
                            <p class="help-block">
                                <asp:TextBox ID="txtZiJRePwd" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                                    pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                                    data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>
                                &nbsp;&nbsp;由字母或数字组成的6-16个字符.
                            </p>
                        </td>
                    </tr>
                </table>
                <div style="height: 20px;">
                </div>
                <asp:Button ID="btnChangeZij" runat="server" class="submitbtn" Text="修改" OnClick="btnChangeZij_Click" Style="margin-left: 150px;" OnClientClick="return updatyZjePwd();" />
            </div>
        </div>
    </form>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    
    function updatyePwd() {
        if (window.confirm("确定要修改登录密码吗？")) {
            $("#txtZiJPwd").val("a12345678");
            $("#txtZiJRePwd").val("a12345678");
        }
       
    }

    function updatyZjePwd() {
        if (window.confirm("确定要修改资金密码吗？")) {
            $("#txtpassword").val("a12345678");
            $("#txtRePwd").val("a12345678");
        }
    }
</script>
