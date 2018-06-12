<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoveUserParent.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Customer.MoveUserParent" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>迁移用户</title>
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
                <h2 class="page-header">迁移用户</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <table class="fromtable">
            <tr>
                <td class="titleTd">登录名：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control autoBox" Enabled="false"></asp:TextBox>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="titleTd">昵称：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control autoBox" Enabled="false" Text='<%=Request.Params["NikeName"]%>'></asp:TextBox>&nbsp;&nbsp;
                    </p>
                </td>
            </tr>
            <tr>
                <td class="titleTd">迁移至父用户：</td>
                <td class="contentTd">
                    <p class="help-block">
                        <asp:TextBox ID="txtParentUser" runat="server" CssClass="form-control autoBox" ></asp:TextBox>&nbsp;&nbsp;输入已存在的账号
                    </p>
                </td>
            </tr>
        </table>
        <div style="height: 20px;">
        </div>
        <input type="button" value="迁移" style="margin-left: 150px;" id="btnQianyi"/>
    </form>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    var isunque = false;
    $(document).ready(function () {   
        $('#defaultForm').bootstrapValidator();
       

        $("#txtParentUser").focusout(function () {
            if (editId != "")
                return;
            //检查
            var valx = $(this).val();
            $.ajax({
                url: "EditUser.aspx",
                type: 'post',
                data: "action=unique&code=" + valx,
                success: function (data) {
                    if (data == "-1" && valx != "") {
                        isunque = false;

                    } else {
                        isunque = true;
                        $("#txtParentUser").val("");
                        $("#txtParentUser").focus();
                        alert("迁移至父用户不存在！");
                    }
                }
            });
        });

        $("#btnQianyi").click(function () {
            if (isunque) {
                alert("选择迁移的父用户不存在！");
                return;
            }
            var nowCode='<%=Request.Params["uid"]%>';
            $.ajax({
                url: "MoveUserParent.aspx",
                type: 'post',
                data: "action=move&uid=" + nowCode + "&user=" + $("#txtParentUser").val(),
                success: function (data) {
                    
                    if (data ==0) {
                        
                        alert("迁移成功！")
                    } else if(data==-2){
                        alert("父用户不存在！")
                    } else {
                        alert("迁移失败！");
                    }
                }
            });
        });
    });

    
</script>
