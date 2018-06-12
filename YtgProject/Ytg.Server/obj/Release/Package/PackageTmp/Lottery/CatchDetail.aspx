<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatchDetail.aspx.cs" Inherits="Ytg.ServerWeb.Lottery.CatchDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>投注详情</title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/config.js"></script>
    <script src="/Content/Scripts/basic.js"></script>
    <script src="/Content/Scripts/comm.js"></script>
    <script src="/Content/Scripts/common.js"></script>
    <script src="/Content/Scripts/playname.js"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <style type="text/css">
        .ltable1{ width:100%; border:0px solid #e1e1e1; font-family:"Microsoft YaHei"; font-size:12px; }
        .ltable1 th{ padding:3px 0;  font-size:12px; font-weight:500; background:#fff; border-bottom:0px solid #E1E1E1; line-height:1.5em;padding-left:1px;padding-right:1px;}
        .ltable1 td{ padding:3px 0; border-bottom:1px solid #fff; line-height:1.5em; padding-left:1px;padding-right:1px; color:#000;font-weight:bold; padding-left:0px;padding-right:0px;}
        .ltable1 tr:hover{ background:#fff; }
        .ltable1 .odd_bg{ background:#fff; }
        .ltable1 td span {font-weight:normal;color:#0066c8;}

        .ltable td {text-align:center;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#lbMonerty").html(decimalCt(Ytg.tools.moneyFormat($("#lbMonerty").html())));
            $("#lbCompledMonerty").html(decimalCt(Ytg.tools.moneyFormat($("#lbCompledMonerty").html())));
            $("#lbWinMonery").html(decimalCt(Ytg.tools.moneyFormat($("#lbWinMonery").html())));
            $("#lbCannelMonery").html(decimalCt(Ytg.tools.moneyFormat($("#lbCannelMonery").html())));

            $("#lbbettCode").html(changePalyName($("#lbbettCode").html(), ''));

            $("#txtContent").val(Ytg.common.LottTool.ShowBetContent($("#txtContent").val(), 1));

            $("#cannel").click(function () {
                if (confirm("确定要终止追号吗？")) {
                    var data = "";
                    $("input[name=chekeval]").each(function () {
                        if ($(this).attr("checked"))
                            data += $(this).val() + ",";
                    });
                    Ytg.common.loading();
                    //LotteryCode
                    $.ajax({
                        url: "/Page/Lott/LotteryBetDetail.aspx",
                        type: 'post',
                        data: "action=cannelcatchNum&catchCode=<%=Request.Params["catchCode"]%>&lotteryid=<%=LotteryCode%>&data=" + data,
                        success: function (data) {
                            Ytg.common.cloading();
                            var jsonData = JSON.parse(data);
                            //清除
                            if (jsonData.Code == 0) {
                                alert("操作成功！");
                                $(".udinput").each(function () {
                                        if ($(this).attr("checked") != undefined)
                                            $(this).removeAttr("checked");
                                });
                                window.location.reload();
                            } else if (jsonData.Code == 1009) {
                                alert("由于您长时间未操作,为确保安全,请重新登录！");
                                parent.window.location = "/login.html";
                            } else {
                                alert("撤单失败，请关闭后重试！");
                            }
                        }
                    });
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding:5px;padding-bottom:10px;">
        <table class="ltable1" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width:25%">追号编号：<asp:Label ID="lbcatchCode" runat="server" Text=""></asp:Label></td>
                <td style="width:25%">游戏用户：<asp:Label ID="lbCode" runat="server" Text=""></asp:Label></td>
                <td style="width:25%">追号时间：<asp:Label ID="lbTime" runat="server" Text=""></asp:Label></td>
                <td style="width:25%">游戏：<asp:Label ID="lbGame" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>玩法：<asp:Label ID="lbbettCode" runat="server" Text=""></asp:Label></td>
                <td>模式：<asp:Label ID="lbPlayType" runat="server" Text=""></asp:Label></td>
                <td>开始期号：<asp:Label ID="lbBeginIssue" runat="server" Text=""></asp:Label></td>
                <td>追号期数：<asp:Label ID="lbIssueCount" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>完成期数：<asp:Label ID="lbCompledIssue" runat="server" Text=""></asp:Label></td>
                <td>取消期数：<asp:Label ID="lbCannelIssue" runat="server" Text=""></asp:Label></td>
                <td>追号总金额：<asp:Label ID="lbMonerty" runat="server" Text=""></asp:Label></td>
                <td>完成金额：<asp:Label ID="lbCompledMonerty" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>中奖期数：<asp:Label ID="lbWinIssue" runat="server" Text=""></asp:Label></td>
                <td>派奖总金额：<asp:Label ID="lbWinMonery" runat="server" Text=""></asp:Label></td>
                <td>取消金额：<asp:Label ID="lbCannelMonery" runat="server" Text=""></asp:Label></td>
                <td>中奖后终止任务：<asp:Label ID="lbWinStop" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4">追号状态：<asp:Label ID="lbState" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4">追号内容：</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="80" style="width:100%;" ReadOnly="true" CssClass="input"></asp:TextBox>
                </td>
            </tr>
           <tr id="callenTr" runat="server" visible="false">
                <td colspan="4"><input type="button" value="终止追号" id="cannel"/></td>
            </tr>
            <tr>
                <td colspan="4">
                    可能中奖情况：
                </td>
            </tr>
        </table>
        <table class="ltable" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <th><input type="checkbox" id="allCheckBox"/></th>
                            <th>追号期数</th>
                            <th>追号倍数</th>
                            <th>追号状态</th>
                            <th>注单详情</th>
                        </tr>
            <asp:Repeater ID="rptWins" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><input type="checkbox" name="chekeval" value="<%# Eval("IssueCode") %>"  <%# GetStateCheck(Eval("Stauts")) %>  class="udinput"/></td>
                        <td><%# Eval("IssueCode") %></td>
                        <td><%# Eval("Multiple") %></td>
                        <td><%# GetItemStateStr(Eval("Stauts")) %></td>
                        <td>
                            <a href="/Lottery/BettingDetail.aspx?catchCode=<%=Request.Params["catchCode"] %>&issueCode=<%#Eval("IssueCode") %>">查看详情</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
                    </table>
            </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $("#allCheckBox").click(function () {
        //
        var hasChecked = $(this).attr("checked") != undefined;
      
        $(".udinput").each(function () {
            
            if ($(this).attr("disabled") == undefined) {
                hasChecked == true ? $(this).attr("checked", "checked") : $(this).removeAttr("checked");
            } else {
                if ($(this).attr("checked") != undefined)
                    $(this).removeAttr("checked");
            }
        });
    });
</script>