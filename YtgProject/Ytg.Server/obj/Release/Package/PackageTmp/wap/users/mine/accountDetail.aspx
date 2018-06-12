<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accountDetail.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.mine.accountDetail" %>

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
    <script src="/wap/statics/js/bet.list.js?ver=4.4"></script>
    <script>
        resUrl = '/wap/statics';
    </script>
    <!-- end  16-09-30 :  下面解决iscroll自带 bug (bugID=1858) -->
    <script>
        //获取投注列表参数
        orderType = 0;
        pageIndex = 1;//初始第一页
        onlyWin = '0';
        if (onlyWin == 1) {
            $('span#order_type').text('中奖记录');
            orderType = 2;
        }
    </script>
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
        <!--<div class="beet-tips-tit"><span>普通玩法</span></div>-->
        <div class="clear"></div>
        <ul>
            <li><a class="beet-active" href="javascript:;" data-type="0">全部订单</a></li>
            <li><a href="javascript:;" data-type="2">我的中奖</a></li>
            <li><a href="javascript:;" data-type="3">待开奖</a></li>
            <li><a href="javascript:;" data-type="4">我的撤单</a></li>
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

    <script>
        var confirmFunc;
        function confirmOk() {
            $('#confirm_pop').hide();
            $('#confirm_bg').hide();
            if (/系统维护/g.test($('div#confirm_pop_content').html())) {
                location.href = '/index/index.html';
                return;
            }
            if (typeof (confirmFunc) == "function") {
                confirmFunc();
                confirmFunc = "";
            } else {
                if (typeof (doConfirmOk) == "function") {
                    doConfirmOk();
                }
            }
        }
        function confirmCancel() {
            $('#confirm_pop').hide();
            $('#confirm_bg').hide();
            if (/系统维护/g.test($('div#confirm_pop_content').html())) {
                location.href = '/index/index.html';
                return;
            }
            if (typeof (doConfirmCancel) == "function") {
                doConfirmCancel();
            }
        }
        function msgConfirm(msg, funcParm, textConfirm, textCancel) {
            textConfirm = (textConfirm == undefined) ? '' : textConfirm;
            textCancel = (textCancel == undefined) ? '' : textCancel;
            if (textConfirm != '') {
                $('span#confirm_pop_ok').text(textConfirm);
            }
            if (textCancel != '') {
                $('span#confirm_pop_cancel').text(textCancel);
            }
            $('div#confirm_pop_content').text(msg);
            $('div#confirm_pop_cancel').html(textCancel);
            $('div#confirm_pop').show();
            $('div#confirm_bg').show();
            confirmFunc = (funcParm == "" || typeof (funcParm) != "function") ? '' : funcParm;
        }
    </script>
    <input type="hidden" id="revokeKey">

    <script>
        function showStep(step, key) {
            if (step == undefined) {
                return;
            }
            $('.bet_list').hide();
            $('.bet_detail').hide();
            $('.bet_revokeok').hide();
            $('.bet_' + step).show();
            if (step == 'detail') {
                showDetail(key);
            }
        }
    </script>
</body>
</html>
