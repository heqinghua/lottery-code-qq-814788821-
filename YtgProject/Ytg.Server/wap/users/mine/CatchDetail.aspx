<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatchDetail.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.mine.CatchDetail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
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
                   
                    $(".loading,.loading-bg").show();
                    $.ajax({
                        url: "/Page/Lott/LotteryBetDetail.aspx",
                        type: 'post',
                        data: "action=cannelcatchNum&catchCode=<%=Request.Params["catchCode"]%>&lotteryid=<%=LotteryCode%>&data=" + data,
                        success: function (data) {
                           
                            $(".loading,.loading-bg").hide();
                            var jsonData = JSON.parse(data);
                            //清除
                            if (jsonData.Code == 0) {
                                alert("操作成功！");
                                $(".udinput").each(function () {
                                        if ($(this).attr("checked") != undefined)
                                            $(this).removeAttr("checked");
                                });
                                window.history.go(-1);
                            } else if (jsonData.Code == 1009) {
                                alert("由于您长时间未操作,为确保安全,请重新登录！");
                                parent.window.location = "/wap/login.html";
                            } else {
                                alert("撤单失败，请稍后再试！");
                            }
                        }
                    });
                }
            });
        });

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
    <style>
        body, #wrapper_1 {
            -webkit-overflow-scrolling: touch;
            overflow-scrolling: touch;
        }
        /*解决苹果滚动条卡顿的问题*/
        #wrapper_1 {
            overflow-y: visible !important;
        }
        .order-info ul li .grey {
            width:8em;
        }
      .ltable{ width:100%; border:1px solid #e1e1e1; font-family:"Microsoft YaHei"; font-size:12px; }
	.ltable th{ padding:8px 0; color:#333; font-size:12px; font-weight:500; background:#f3f3f3; border-bottom:1px solid #E1E1E1; line-height:1.5em;padding-left:1px;padding-right:1px;}
	.ltable td{ padding:8px 0; border-bottom:1px solid #e8e8e8; line-height:1.5em; padding-left:1px;padding-right:1px; }
	.ltable td .sort{ display:inline-block; padding:0 3px; border:1px solid #d7d7d7; width:40px; height:20px; line-height:18px; color:#666; font-size:12px; background:#fff; vertical-align:middle; }
	.ltable td .btn-tools{ display:inline-block; height:22px;  vertical-align:middle; }
		.ltable td .btn-tools a{ display:block; float:left; margin:0; padding:0; width:20px; height:20px; border:1px solid #e8e8e8; border-left:none; text-indent:-9999em; background:url(/Content/Images/Skin/skin_icons.png) no-repeat #fff; overflow:hidden; }
		.ltable td .btn-tools a:first-child{ border-left:1px solid #e8e8e8; }
		.ltable td .btn-tools a.msg{ background-position:3px -81px; }
		.ltable td .btn-tools a.msg.selected{ background-position:3px -109px; }
		.ltable td .btn-tools a.top{ background-position:-25px -81px; }
		.ltable td .btn-tools a.top.selected{ background-position:-25px -109px; }
		.ltable td .btn-tools a.red{ background-position:-53px -81px; }
		.ltable td .btn-tools a.red.selected{ background-position:-53px -109px; }
		.ltable td .btn-tools a.hot{ background-position:-81px -81px; }
		.ltable td .btn-tools a.hot.selected{ background-position:-81px -109px; }
		.ltable td .btn-tools a.pic{ background-position:-109px -81px; }
		.ltable td .btn-tools a.pic.selected{ background-position:-109px -109px; }
		.ltable td .folder-open{ display:inline-block; margin-right:2px; width:20px; height:20px; background:url(/Content/Images/Skin/skin_icons.png) -40px -196px no-repeat; vertical-align:middle; text-indent:-999em; *text-indent:0; }
		.ltable td .folder-line{display:inline-block; margin-right:2px; width:20px; height:20px; background:url(/Content/Images/Skin/skin_icons.png) -80px -196px no-repeat; vertical-align:middle; text-indent:-999em; *text-indent:0; }
	.ltable tr:hover{ background:#F2F7FB; }
	.ltable .odd_bg{ background:#fafafa; }
	.ltable td .user-avatar{ display:block; width:64px; height:64px; background:url(/Content/Images/Skin/skin_icons.png) 3px -498px no-repeat #fff;}
	.ltable td .user-box{ padding-left:10px; }
		.ltable td .user-box h4{ margin:0; padding:0; display:block; font-weight:normal; font-size:12px; height:16px; line-height:14px;}
		.ltable td .user-box h4 b{ color:#06F;}
		.ltable td .user-box i{ display:block; color:#999; font-style:normal; line-height:24px; height:24px; }
		.ltable td .user-box span{ display:block; padding:2px 0 0 0; height:14px; }
		.ltable td .user-box span a{ display:block; float:left; margin-right:5px; width:20px; height:20px; background:url(/Content/Images/Skin/skin_icons.png) no-repeat #fff; border:1px solid #E1E1E1; text-indent:-999em;}
		.ltable td .user-box span a.amount{ background-position:-331px -109px;}
		.ltable td .user-box span a.point{ background-position:-221px -109px; }
		.ltable td .user-box span a.msg{ background-position:-305px -109px;}
		.ltable td .user-box span a.sms{ background-position:-443px -110px;}
		
	.ltable td.comment{ padding:10px; line-height:1em; }
		.ltable td.comment .title{ margin-bottom:5px; line-height:180%; font-weight:bold; }
		.ltable td.comment .title .note{ float:right; font-weight:normal; }
		.ltable td.comment .title .note i{ margin-left:10px; font-style:normal; font-family:'Microsoft YaHei'; color:#999; }
		.ltable td.comment .title .note i.reply{ padding-left:18px; }
		.ltable td.comment .ask{ line-height:180%; font-family:'Microsoft YaHei'; }
		.ltable td.comment .ask .audit{ margin-right:3px; display:inline-block; vertical-align:middle; width:14px; height:14px; background:url(/Content/Images/Skin/skin_icons.png) -362px -112px  no-repeat; }
		.ltable td.comment .ask .answer{ margin-top:10px; padding:6px 10px; border:1px solid #f6e8b9; background:#fbf8e7; color:#666; }
		.ltable td.comment .ask .answer b{ color:#090; }
		.ltable td.comment .ask .answer .time{ display:block; float:right; color:#999; }
        .ltable td {text-align:center;}
    </style>
    <title>追号详情</title>
</head>
<body class="login-bg">
    <form id="form1" runat="server">
        <div class="bet_revokeok header">
            <div class="header">
                <div class="headerTop">
                    <div class="ui-toolbar-left">
                        <button class="reveal-left" onclick="window.history.go(-1);">reveal</button>
                    </div>
                    <h1 class="ui-toolbar-title"><span>追号详情</span></h1>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hidPostionName" runat="server" />
        <div id="wrapper_1" class="bet_detail scorllmain-content scorll-order nobottom_bar" style="padding-top: 44px;">
            <div class="sub_ScorllCont">
                <div class="order-box">
                
                    <div class="order-info">
                        <h3>订单内容</h3>
                        <ul>
                            <li><span class="grey">追号编号</span><asp:Label ID="lbcatchCode" runat="server" Text=""></asp:Label></li>
                            <li style="display: none;"><span class="grey">游戏用户</span><asp:Label ID="lbCode" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">追号时间</span><asp:Label ID="lbTime" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">游戏</span><asp:Label ID="lbGame" runat="server" Text=""></asp:Label><a style="color: red; font-weight: normal; text-decoration: underline; color: #f74848; margin-left: 10px" runat="server" id="palCatch">追号详情</a></li>
                            <li><span class="grey">玩法</span><asp:Label ID="lbbettCode" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">模式</span><asp:Label ID="lbPlayType" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">开始期号</span><asp:Label ID="lbBeginIssue" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">追号期数</span><asp:Label ID="lbIssueCount" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">完成期数</span><asp:Label ID="lbCompledIssue" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">取消期数</span><asp:Label ID="lbCannelIssue" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">追号总金额</span><asp:Label ID="lbMonerty" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">完成金额</span><asp:Label ID="lbCompledMonerty" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">中奖期数</span><asp:Label ID="lbWinIssue" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">派奖总金额</span><asp:Label ID="lbWinMonery" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">取消金额</span><asp:Label ID="lbCannelMonery" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">中奖后终止任务</span><asp:Label ID="lbWinStop" runat="server" Text=""></asp:Label></li>
                            <li><span class="grey">追号状态</span><asp:Label ID="lbState" runat="server" Text=""></asp:Label></li>
                        </ul>
                    </div>
                    <div class="order-info order-kit">
                        <h3>追号内容</h3>
                        <ul id="code" class="grey">
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="80" Style="width: 100%;" ReadOnly="true" CssClass="input"></asp:TextBox>
                        </ul>
                    </div>
                     <div id="callenTr" runat="server" visible="false">
                    <button class="order-btn"  type="button" data-key="" data-gid="" id="cannel">终止追号</button>
                </div>
                <div id="more" runat="server" visible="false">
                    <button class="order-btn" data-key="" data-gid=""  type="button" id="goBetting">再来一注</button>
                </div>
                    <div>
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
                    </div>
                 
                </div>


            </div>
        </div>


        <!-- 加载中 -->
        <div class="loading" style="left: 50%; margin-left: -2em; display: none;">加载中...</div>
        <div class="loading-bg" style="display: none;"></div>

    </form>
</body>
</html>
