<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatchList.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.mine.CatchList" %>

<html lang="en">
<head>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <meta name="format-detection" content="telphone=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="mobile-web-app-capable" content="yes">
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css">
    <script src="/wap/statics/js/jquery.min.js?ver=4.4"></script>
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
     <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
     <script src="/Content/Scripts/playname.js"></script>
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

    <title>投注记录</title>
</head>
<body class="login-bg">
    <div class="bet_list header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-betting-title">
                <div class="bett-top-box">
                    <div class="bett-tit">
                        <span id="order_type">全部订单</span><i class="bett-attr" style="display: ''"></i>
                    </div>
                </div>
            </h1>
            <div class="ui-bett-refresh">
                <a class="bett-refresh" href="javascript:;"></a>
            </div>
        </div>
    </div>
    
    
    <div class="bet_list scorllmain-content scorll-order nobottom_bar paddingbutton" style="padding-top: 64px; padding-bottom: 64px;">
        <div class="sub_ScorllCont">
            <div class="mine-message" style="display: none;">
                <div class="mine-mess">
                    <img src="/wap/statics/images/message/wuxinxi.png"></div>
                <p>目前暂无记录哦！</p>
            </div>
            <div class="order-center">
                <ul id="bet_list">

                </ul>
                <a class="on-more" href="javascript:;" style="display: none;">点击加载更多</a>
            </div>
        </div>
    </div>

    <!-- 直选tips -->
    <div class="bet_list beet-tips" hidden="">
        <div class="clear"></div>
        <ul>
            <li><a class="beet-active" href="javascript:;" data-type="0">全部订单</a></li>
            <li><a href="javascript:;" data-type="0">进行中</a></li>
            <li><a href="javascript:;" data-type="1">已完成</a></li>
            <li><a href="javascript:;" data-type="2">已取消</a></li>
        </ul>
    </div>

    <!-- 加载中 -->
    <div class="loading" style="left: 50%; margin-left: -2em; display: none;">加载中...</div>
    <div class="loading-bg" style="display: none;"></div>

    <script>
        function loadingShow(tips, bg) {
            if (tips == "" || typeof (tips) == "undefined") {
                $(".loading").css("left", "50%");
                $(".loading").css("margin-left", "-2em");
                $(".loading").html("加载中...");
            } else {
                $(".loading").html(tips);
                $(".loading").css("left", Math.ceil(($(document).width() - $(".loading").width()) / 2));
                $(".loading").css("margin-left", 0);
            }

            bg = (bg == "" || typeof (bg) == "undefined") ? 1 : 0;
            if (bg == 1) {
                $(".loading-bg").show();
            } else {
                $(".loading-bg").hide();
            }
            $(".loading").show();
        }
        function loadingHide() {
            $(".loading").hide();
            $(".loading-bg").hide();
        }
    </script>

    <style>
        .center {
            text-align: center;
        }
    </style>


    <div id="tip_bg" class="tips-bg" style="display: none;"></div>

    <script>
        var func;
        function tipOk() {
            $('#tip_pop').hide();
            $('#tip_bg').hide();
            if (/系统维护/g.test($('div#tip_pop_content').html())) {
                location.href = '/index/index.html';
                return;
            }
            if (typeof (func) == "function") {
                func();
                func = "";
            } else {
                if (typeof (doTipOk) == "function") {
                    doTipOk();
                }
            }
        }
        function msgAlert(msg, funcParm) {
            $('div#tip_pop_content').html(msg);
            $('div#tip_pop').show();
            $('div#tip_bg').show();
            func = (funcParm == "" || typeof (funcParm) != "function") ? '' : funcParm;
        }
    </script>


    <div id="confirm_bg" class="tips-bg" style="display: none;"></div>

</body>
</html>
<script type="text/javascript">
    function initLoading() {
        $('a.on-more').hide();
        $('div.mine-message').hide();
    }
    $(function () {

        $('div.beet-tips a').click(function () {
            $('div.beet-tips a').removeClass('beet-active');
            $('div.beet-tips').hide();
            $(this).addClass('beet-active');
            $('span#order_type').text($(this).text());
            orderType = $(this).data('type');
            initLoading();
            getBetList();
        });
        getBetList(2);
    });

    function getQueryData() {
        var state = "";
        var st = $(".beet-active").attr("data-type");
        if (st == 0) {
            //全部订单
            state += "-1";
        } 
        else if (st == 2) {
            //进行中
            state += "2";
        } else if (st == 3) {
            //已完成
            state += "3";
        } else if (st == 4) {
            //已取消
            state += "4";
        }
       

        var filterData = {
            "BeginTime":"",
            "EndTime": "",
            "State": state,
            "LotteryCode":"",
            "PalyRadioCode": "-1",
            "IssueCode": "",
            "Mode": "-1",
            "CatchNumCode": "",
            "PalyUserCode": "",
            "UserScope": "-1",
            "tradeType": "-1"
        };
        return filterData;
    }

    function getBetList(more) {//1:更多，2:充值
        
        if (more == 1) {
            pageIndex++;
        } else if (more == 2) {
            pageIndex = 1;
        }
     
        $('a.on-more').html("正在获取数据中..");
        //
        $('ul#bet_list').children().remove();
        $.ajax({
            url: "/Page/Lott/LotteryBetDetail.aspx",
            type: 'post',
            data: "action=catchnumlist&data=" + JSON.stringify(getQueryData()) + "&pageIndex=" + pageIndex,
            success: function (data) {
               
                var jsonData = JSON.parse(data);

                //清除
                $('div.mine-message').hide();
                var htm = "";
                if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                    //分页

                    $("#bet_list").children().remove();

                    var sumwivalue = 0;
                    var sumnowinvalue = 0;
                    for (var c = 0; c < jsonData.Data.length; c++) {
                        var item = jsonData.Data[c];
                        var states = "";
                        if (item.Stauts == 1) {
                            states = "<span style='color:red;'>已完成</span>";
                        } else {
                            states = Ytg.common.LottTool.GetStateContent(item.Stauts + "_");
                        }

                        var betContent = Ytg.common.LottTool.ShowBetContent(item.BetContent);
                        var modelStr = "";
                        switch (item.Model) {
                            case 0:
                                modelStr = "元";
                                break;
                            case 1:
                                modelStr = "角";
                                break;
                            case 2:
                                modelStr = "分";
                                break;
                            case 3:
                                modelStr = "厘";
                                break;
                        }

                        sumwivalue += item.SumAmt;
                        sumnowinvalue += item.CompledMonery;
                       // sumzhuiinvalue += item.WinMoney;

                        var winAmt = decimalCt(Ytg.tools.moneyFormat(item.WinMoney));

                        var tolHtml = winAmt;
                        if (winAmt.toString().indexOf(',') >= 0 || winAmt > 0)
                            tolHtml = "<span class='winSpan'>" + winAmt + "</span>";

                        htm += '<li>'
                        + "<a href='javascript:showDetail(\"" + item.CatchNumCode + "\",\"-1\",2);' >"
                        + '<div class="order-list-tit">'
                        + '<span class="fr c-red">' + tolHtml + '元</span><span class="order-top-left">' + item.LotteryName + '</span>'
                        + '</div>'
                        + '<div class="c-gary"><span class="fr">' + states + '</span><p class="order-time">' + getDayTime(item.OccDate) + '</p></div>'
                        + '</a>'
                        + '</li>';

                    }
                    //小计

                } else {
                    $('div.mine-message').show();
                }

                if (more == 2) {//刷新
                    $('ul#bet_list').html(htm);
                } else {
                    $('ul#bet_list').append(htm);
                }
                $('a.on-more').html("点击加载更多");
                if (data.PageCount > pageIndex) {
                    $('a.on-more').show();
                } else {
                    $('a.on-more').hide();
                }

            }
        });

    }

    function showDetail(code, issueCode, betType) {

        var ul = "/wap/users/mine/bettDetails.aspx?betcode=" + code;
        if (betType == 1 && issueCode != -1) {
            ul = "/wap/users/mine/BettingDetail.aspx?catchCode=" + code + "&issueCode=" + issueCode;
        }
        if (betType == 2) {
            ul = "/wap/users/mine/CatchDetail.aspx?catchCode=" + code;
        }
        window.location = ul;
    }
</script>
