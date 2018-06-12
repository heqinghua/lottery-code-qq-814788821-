<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Customer.EditUser" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>编辑用户</title>
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
                <h2 class="page-header">新增用户</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <table class="fromtable">
            <tr>
                <td class="titleTd">登录名：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="登录名由字母或数字组成的6-16个字符" pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录名由字母或数字组成的6-16个字符" data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录名由字母或数字组成的6-16个字符"></asp:TextBox>
                        &nbsp;&nbsp;由字母或数字组成的6-16个字符.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="titleTd">昵称：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="用户昵称不能为空"></asp:TextBox>&nbsp;&nbsp;由字母或数字组成的2-8个字符.
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
                <td class="titleTd">用户类型：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:DropDownList ID="drpUserType" runat="server" CssClass="form-control autoBox" Enabled="false">
                            <asp:ListItem Value="3" Text="总代理"></asp:ListItem>
                            <asp:ListItem Value="4" Text="主管" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </p>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <label style="display: none;">奖金类型</label>
                    <asp:DropDownList ID="drpjj" runat="server" CssClass="form-control autoBox" Style="display: none;">
                        <asp:ListItem Value="0" Text="1800" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="1700"></asp:ListItem>
                    </asp:DropDownList>
                    <p class="help-block" style="display: none;"></p>
                    <label style="display: none;">返点</label>
                    <asp:TextBox ID="txtBackNum" runat="server" CssClass="form-control" Text="0.0" required data-bv-notempty-message="会员返点不能为空" Style="display: none;"></asp:TextBox>
                    <p class="help-block"></p>
                </td>
            </tr>

        </table>
        <div style="height: 20px;">
        </div>
        <asp:Button ID="btnSave" runat="server" class="submitbtn" Text="保存" OnClick="btnSave_Click" Style="margin-left: 150px;" />
    </form>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    var isunque = false;
    var editId=<%=Request.Params["id"]%>;
    
    $(document).ready(function () {   
        $('#defaultForm').bootstrapValidator();
        if(editId!=""){
            $("#pwdLab").hide();
            $("#txtpassword").hide();
            $("#pwd_block").hide();
            $("#txtpassword").val($("#txtCode").val())
        }

        $("#txtCode").focusout(function () {
            if(editId!="")
                return;
            //检查
            var valx=$(this).val();
            $.ajax({
                url: "EditUser.aspx",
                type: 'post',
                data: "action=unique&code=" + valx,
                success: function (data) {
                    if (data == "-1" && valx!="") {
                        isunque = false;
                        $("#txtCode").val("");
                        $("#txtCode").focus();
                        alert("登录名已经存在！");
                    } else {
                        isunque = true;
                    }
                }
            });
        });
    });

    
</script>
