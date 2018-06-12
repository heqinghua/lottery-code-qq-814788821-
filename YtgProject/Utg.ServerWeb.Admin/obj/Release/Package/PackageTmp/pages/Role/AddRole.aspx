<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Role.AddRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>新增角色</title>
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
                <h2 class="page-header">编辑角色</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="fromtable">
            <tr>
                <td class="titleTd">名称：</td>
                <td class="contentTd"> 
                       <p class="help-block"><asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="角色名称不能为空"
                                    data-bv-stringlength-max="16" data-bv-stringlength-message="角色名长度不能超过15个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;角色名称长度不能超过15个字符.</p>
                </td>
            </tr>
            <tr>
                <td class="titleTd">描述：</td>
                <td class="contentTd"> 
                       <p class="help-block">  <asp:TextBox ID="txtDescript" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="描述不能为空"
                                    data-bv-stringlength-max="100" data-bv-stringlength-message="描述长度不能超过15个字符"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;描述长度不能超过100个字符.</p>
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

    $(document).ready(function () {
        $('#defaultForm').bootstrapValidator();
        //$("#txtRoleName").focusout(function () {
        //    //检查
        //    var valx = $(this).val();
        //    $.ajax({
        //        url: "EditRole.aspx",
        //        type: 'post',
        //        data: "action=unique&code=" + valx,
        //        success: function (data) {
        //            if (data == "-1" && valx != "") {
        //                isunque = false;
        //                $("#txtRoleName").val("");
        //                $("#txtRoleName").focus();
        //                alert("角色名已经存在！");
        //            }
        //        }
        //    });
        //});
    });
</script>

