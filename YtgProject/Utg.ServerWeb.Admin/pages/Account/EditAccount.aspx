<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAccount.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Manager.EditAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑用户</title>
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="/resource/bsvd/dist/css/bootstrapValidator.css" />

    <script type="text/javascript" src="/resource/bsvd/vendor/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/dist/js/bootstrapValidator.js"></script>
</head>
<body>
    <div class="container">
        <div id="al_container"></div>
        <div class="row">
            <!-- /.col-lg-12 -->
            <section>
                <div class="col-lg-8 col-lg-offset-2">
                    <form id="defaultForm" runat="server" method="post" data-bv-message="This value is not valid"
                        data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
                        data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
                        data-bv-feedbackicons-validating="glyphicon glyphicon-refresh">
                        <table class="fromtable">
                            <tr>
                                <td class="titleTd">登录名：</td>
                                <td class="contentTd">
                                    <p class="help-block">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="登录名由字母或数字组成的6-16个字符" pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录名由字母或数字组成的6-16个字符"
                                            data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录名由字母或数字组成的6-16个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;由字母或数字组成的6-16个字符.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleTd">昵称：</td>
                                <td class="contentTd">
                                    <p class="help-block">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="用户昵称不能为空"
                                            data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录名由字母或数字组成的2-8个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;由字母或数字组成的2-8个字符.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleTd">登录密码：</td>
                                <td class="contentTd">
                                    <p class="help-block">
                                        <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control autoBox" required data-bv-notempty-message="登录密码由字母或数字组成的6-16个字符"
                                            pattern="^[a-zA-Z0-9]+$" data-bv-regexp-message="登录密码由字母或数字组成的6-16个字符"
                                            data-bv-stringlength="true" data-bv-stringlength-min="6" data-bv-stringlength-max="16" data-bv-stringlength-message="登录密码由字母或数字组成的6-16个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;由字母或数字组成的6-16个字符.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleTd">用户状态：</td>
                                <td class="contentTd">
                                    <p class="help-block">
                                        <asp:DropDownList ID="drpUserType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="可用" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="停用"></asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                </td>
                            </tr>
                        </table>
                        <div style="height: 20px;"></div>
                        <asp:Button ID="btnSave" runat="server" class="submitbtn" Text="保存" OnClick="btnSave_Click" Style="margin-left: 150px;"/>
                    </form>
                </div>
            </section>
        </div>
    </div>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    $(document).ready(function () {
        $('#defaultForm').bootstrapValidator();
        //$("#txtCode").focusout(function ()
        //{
        //    //检查
        //    var valx=$(this).val();
        //    $.ajax({
        //        url: "EditAccount.aspx",
        //        type: 'post',
        //        data: "action=unique&code=" + valx,
        //        success: function (data) 
        //        {
        //            if (data == "-1" && valx!="") 
        //            {
        //                isunque = false;
        //                $("#txtCode").val("");
        //                $("#txtCode").focus();
        //                alert("登录名已经存在！");
        //            }
        //        }
        //    });
        //});
    });


</script>
