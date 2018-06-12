<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUserRemo.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.EditUserRemo" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>编辑返点</title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <script src="/Content/Scripts/config.js"></script>
    <script src="/Content/Scripts/basic.js"></script>
    <script src="/Content/Scripts/comm.js"></script>
    <script src="/Content/Scripts/common.js"></script>
    <style type="text/css">
        .tab-content p {font-size:16px;color:#000;}
        .tab-content p span {font-weight:bold;}
        .marginSpan {margin-left:30px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="control">
           <div class="bindDiv">
                <h2 class="subctTitle">基本信息：</h2>
           </div>
            <div class="tab-content" style="border-top:1px solid #e1e1e1;">
              <p >
                 <span class="marginSpan"></span> 账号：<asp:Label ID="lbAccount" runat="server"></asp:Label> <span class="marginSpan"></span> 昵称：<asp:Label ID="lbNickName" runat="server"></asp:Label> <span class="marginSpan"></span>返点级别：<asp:Label ID="lbLevel" runat="server"></asp:Label>
              </p>
            </div>
            <div class="bindDiv">
                <h2 class="subctTitle">返点信息：</h2>
                <p>例如：<span style="font-size:14px;">您自身返点为0.8， 而您在下面 【保留返点】处选择 0.5，您开设新账户的返点为0.3。</span></p>
                <p>剩余开户额：<asp:Literal ID="ltKaihu" runat="server"></asp:Literal></p>
                <p>备注：<span style="font-size:14px;">开通返点级别为【6.5】以下的用户不需要开户配额。</span></p>
                <table style="font-size:14px;margin-left:20px;">
                    <tr>
                        <td>您的返点级别：<asp:Label ID="lbMeRemo" runat="server" style="color:red;font-weight:bold;">0.0</asp:Label></td>
                        <td>&nbsp;&nbsp;&nbsp;
                            保留返点：
                            <asp:DropDownList ID="drpBackNum" runat="server" style="width:80px"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lbfanwei" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
               
            </div>
            <div style="height:20px;"></div>
                <asp:Button  ID="btnSubmit" runat="server" CssClass="btn" Text="保存" style="margin-left:220px;" OnClick="btnSubmit_Click"/>
            <div style="height:20px;"></div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        var usRebate=<%=CookUserInfo.Rebate %>;
        $("#btnSubmit").click(function () {
            var param = "action=updateuserremo";
            param += "&uid=<%=Request.Params["id"]%>";
            param += "&rmb=" + (usRebate + parseFloat($("#drpBackNum").find("option:selected").val()));
            //充值
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: param,
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        $.dialog.tips("保存成功!", 2.0, '32X32/succ.png', function () { });
                        parent.UserRemoClose();
                    } else {
                        $.dialog.tips("保存失败，请关闭后重试!", 2.0, '32X32/succ.png', function () { });
                    }
                }
            });
            //
            return false;

        });
    });
    $(function () {
        Ytg.common.loading();
        var cldt = setInterval(function () {
            Ytg.common.cloading();
            clearInterval(cldt);
        }, 1000)
    })
</script>