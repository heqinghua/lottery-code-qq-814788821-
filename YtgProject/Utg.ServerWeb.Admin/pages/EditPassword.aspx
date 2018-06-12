<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPassword.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.EditPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>修改登陆密码</title>
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
                <h2 class="page-header">修改密码</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="fromtable">
            <tr>
                <td class="titleTd">登录名：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control autoBox" Enabled="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;由字母或数字组成的6-16个字符.
                    </p>
                </td>
            </tr>
             <tr>
                <td class="titleTd">旧登录密码：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                            pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                            data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;由字母或数字组成的6-16个字符.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="titleTd">登陆密码：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                            pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                            data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;由字母或数字组成的6-16个字符.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="titleTd">确认登录密码：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtRePwd" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                            pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                            data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;由字母或数字组成的6-16个字符.
                    </p>
                </td>
            </tr>
        </table>
        <div style="height: 20px;">
        </div>
        <asp:Button ID="btnSave" runat="server" class="submitbtn" Text="保存" OnClick="btnSave_Click" Style="margin-left: 150px;"/>
    </form>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    $(document).ready(function () {
        $('#defaultForm').bootstrapValidator();
        if (editId != "") {
            $("#pwdLab").hide();
            $("#txtpassword").hide();
            $("#pwd_block").hide();
            $("#txtpassword").val($("#txtCode").val())
        }

    });


</script>
