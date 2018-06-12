<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bettDetails.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.mine.bettDetails" %>

<html lang="en">
<head>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <meta name="format-detection" content="telphone=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="mobile-web-app-capable" content="yes">
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css">
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/wap/statics/js/iscro-lot.js?ver=4.5"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="../../../Mobile/css/dialogUI.css" rel="stylesheet" />

    <script>
        Ytg = $.extend(Ytg, {
            SITENAME: "<%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %>",
            SITEURL: window.location.host,
            RESOURCEURL: "/Content",
            BASEURL: "/",
            SERVICEURL: "",
            NOTEFREQUENCY: 10000
        });
        // Ytg.namespace("Ytg.Lottery.user");
        Ytg.common.user.info = {
            user_id: '<%=CookUserInfo.Id%>',
             username: '<%=CookUserInfo.Code%>',
             nickname: '<%=CookUserInfo.NikeName%>'
         };
    </script>
    <script>
        $(function () {
            var _padding = function () {
                try {
                    var l = $("body>.header").height();
                    if ($("body>.lott-menu").length > 0) {
                        l += $("body>.lott-menu").height();
                    }
                    $("#wrapper_1").css("paddingTop", l + "px");
                } catch (e) { }
                try {
                    if ($("body>.menu").length > 0) {
                        var l = $("body>.menu").height();
                    }
                    $("#wrapper_1").css("paddingBottom", l + "px");
                } catch (e) { }
            };
            (function () {
                _padding();
            })();
            $(window).bind("load", _padding);

        });

    </script>
    <script type="text/javascript">
        $(function () {
            $("#lbMonerty").html(decimalCt(Ytg.tools.moneyFormat($("#lbMonerty").html())));
            $("#lbSumMonery").html(decimalCt(Ytg.tools.moneyFormat($("#lbSumMonery").html())));
            $("#lbBackNum").html(decimalCt(Ytg.tools.moneyFormat($("#lbBackNum").html())));
            var dwdname = $("#lbPlayType").html();
            $("#lbPlayType").html($("#hidPostionName").val() + changePalyName($("#lbPlayType").html(), ''));
            $("#txtContent").val(Ytg.common.LottTool.ShowBetContent($("#txtContent").val(), 1));
            //当为定位胆时候
            //if (dwdname == '定位胆定位胆') {
            //    $("#txtContent").val(parseDingWeiDanShow($("#txtContent").val()));
            //}

            //前往投注、、
            $("#goBetting").click(function () {

            });


            $("#cannel").click(function () {
                var catchCode = "<%=Request.QueryString["catchCode"]%>";
                var issCode = "<%=Request.QueryString["issueCode"]%>";
                var url = "action=cannelbethnum&bettCode=<%=Request.Params["betcode"]%>&lotteryid=<%=LotteryCode%>";
                if (catchCode != "") {
                    url = "action=cannelcatchNum&catchCode=" + catchCode + "&lotteryid=<%=LotteryCode%>&data=" + issCode;
                }
                $.confirm("确定要撤单吗?", function () {
                    $.ajax({
                        url: "/Page/Lott/LotteryBetDetail.aspx",
                        type: 'post',
                        data: url,
                        success: function (data) {

                            var jsonData = JSON.parse(data);
                            //清除
                            if (jsonData.Code == 0) {
                                $("#cannelCompled").show();
                                $("#wrapper_1").hide();
                                $("#exitTd").remove();
                            } else if (jsonData.Code == 1002) {

                                $.alert("对不起，当期投注时间已截止，撤单失败！");
                            } else if (jsonData.Code == 1009) {
                                $.alert("由于您长时间未操作,为确保安全,请重新登录！");
                                parent.window.location = "/wap/login.html";
                            }
                            else {
                                $.alert("撤单失败，请刷新后重试！");

                            }
                        }
                    });
                   
                });
                
            });
        });

    </script>
    <!-- start  16-09-30 : 下面解决iscroll自带 bug (bugID=1858) -->
    <style>
        body, #wrapper_1 {
            -webkit-overflow-scrolling: touch;
            overflow-scrolling: touch;
        }
        /*解决苹果滚动条卡顿的问题*/
        #wrapper_1 {
            overflow-y: visible !important;
        }
    </style>
    <script src="/wap/statics/js/bet.list.js?ver=4.5"></script>
    <script>
        resUrl = '/wap/statics';
    </script>

    <title>投注详情</title>
</head>
<body class="login-bg">
    <form id="form1" runat="server">
        <div class="bet_revokeok header">
            <div class="header">
                <div class="headerTop">
                    <div class="ui-toolbar-left">
                        <button class="reveal-left" onclick="window.history.go(-1);" type="button">reveal</button>
                    </div>
                    <h1 class="ui-toolbar-title"><span >注单详情</span></h1>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hidPostionName" runat="server" />
        <div id="wrapper_1" class="bet_detail scorllmain-content scorll-order nobottom_bar" style="padding-top: 44px;">
            <div class="sub_ScorllCont">
                <div class="order-box">
                    <div class="order-tit">

                        <div class="order-top-right">
                            <span class="order-name f120"></span><span class="grey f80"></span>
                            <div class="lot-number">
                                <span class="grey fl">开奖号码：</span>
                                <div id="open_num" class="red">
                                    <asp:Label ID="lbOpenTime" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="order-info">
                        <h3>订单内容</h3>
                        <ul>
                            <li><span class="grey">注单编号</span><asp:Label ID="lbbettCode" runat="server" Text=""></asp:Label></li>
                            <li style="display: none;"><span class="grey">游戏用户</span><asp:Label ID="lbCode" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">游戏</span><asp:Label ID="lbGame" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">玩法</span><asp:Label ID="lbPlayType" runat="server" Text=""></asp:Label><a style="color: red; font-weight: normal; text-decoration: underline; color: #f74848; margin-left: 10px" runat="server" id="palCatch">追号详情</a></li>
                            <li><span class="grey">游戏奖期</span><asp:Label ID="lbIssue" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">倍数模式</span><asp:Label ID="lbModel" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">注单状态</span><asp:Label ID="lbState" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">订单金额</span><asp:Label ID="lbSumMonery" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">注单奖金</span><asp:Label ID="lbMonerty" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">投注时间</span><asp:Label ID="lbbetTime" runat="server" Text=""></asp:Label></li>
                            <li id="dongtaiMonery" runat="server"><span class="grey">动态奖金返点</span><asp:Label ID="lbBackNum" runat="server" Text=""></asp:Label><asp:Label ID="lbBackNumlst" runat="server"></asp:Label></li>
                        </ul>
                    </div>
                    <div class="order-info order-kit">
                        <h3>投注号码</h3>
                        <ul id="code" class="grey">
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="80" Style="width: 100%;" ReadOnly="true" CssClass="input"></asp:TextBox>
                        </ul>
                    </div>
                    <div style="display: none;">
                        <table class="ltable" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th>奖级名称</th>
                                <th>号码</th>
                                <th>倍数</th>
                                <th>奖金</th>
                            </tr>
                            <asp:Repeater ID="rptWins" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("PlayTypeRadioName") %></td>
                                        <td>
                                            <script type="text/javascript">
                                                document.write(Ytg.common.LottTool.ShowBetContent('<%# Eval("itemContent") %>'))
                                            </script>
                                        </td>
                                        <td><%# Eval("Multiple") %></td>
                                        <td><%# String.Format("{0:N4}",Convert.ToDecimal(Eval("meWinMoney")))  %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>

                <div id="exitTd" runat="server" visible="false">
                    <button class="order-btn"  type="button" data-key="" data-gid="" id="cannel">撤单</button>
                </div>
                <div id="more" runat="server" visible="false">
                    <button class="order-btn" data-key="" data-gid=""  type="button" id="goBetting">再来一注</button>
                </div>
            </div>
        </div>

        <div class="bet_revokeok scorllmain-content scorll-order nobottom_bar" id="cannelCompled" style="display: none; padding-top: 44px;">
            <div class="sub_ScorllCont">
                <div class="with-end text-center">
                    <div class="wiht-img">
                        <img src="/wap/statics/images/gou_wancheng.png" alt="">
                    </div>
                    撤单成功，预祝您中奖
                </div>
                <button class="charge-btn" onclick="window.history.go(-1);" type="button" style="margin: 20px auto">确认</button>
            </div>
        </div>

        <!-- 加载中 -->
        <div class="loading" style="left: 50%; margin-left: -2em; display: none;">加载中...</div>
        <div class="loading-bg" style="display: none;"></div>

    </form>
</body>
</html>
