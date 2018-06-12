<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.Main" %>

<%@ Register Src="~/wap/Bottom.ascx" TagPrefix="uc1" TagName="Bottom" %>

<html lang="en">
<head>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <meta name="format-detection" content="telephone=no">
    <meta name="mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" href="/wap/statics/css/jquery-ui.min.css?ver=4.4" type="text/css">
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css">
    <script src="/wap/statics/js/jquery.min.js?ver=4.4"></script>
    <script src="/wap/statics/js/lazyload-min.js" type="text/javascript"></script>

    <script src="/wap/statics/js/jquery-ui.min.js?ver=4.4"></script>
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>

    <script>
        isLogin = true;
    </script>
    <script>
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

        function loadComplete() {
            _padding();
            checkItem(4);


            Ytg = $.extend(Ytg, {
                SITENAME: "<%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %>",
                SITEURL: window.location.host,
                RESOURCEURL: "/Content",
                BASEURL: "/",
                SERVICEURL: "",
                NOTEFREQUENCY: 10000
            });
            Ytg.common.user.info = {
                user_id: '<%=CookUserInfo.Id%>',
             username: '<%=CookUserInfo.Code%>',
             nickname: '<%=CookUserInfo.NikeName%>'
         };

         getCurBalance();
         getWinAmount();
     }

     function loadscript() {

         LazyLoad.loadOnce([
          '/wap/statics/js/iscro-lot.js?ver=4.4'
         ], loadComplete);
     }
     setTimeout(loadscript, 10);

    </script>
    <!-- hide address bar -->
    <title>个人中心</title>
</head>
<body class="login-bg" onload="loaded()">
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">个人中心</h1>
            <div class=" header-icon" style="display: none;">
                <a class="header-logout" href="javascript:;" id="indexlog_out"></a>
            </div>
        </div>
    </div>
    <div id="wrapper_1" class="scorllmain-content nobottom_bar" style="padding-top: 44px; padding-bottom: 67px;">
        <div class="sub_ScorllCont">
            <div class="mine-top">
                <div class="mine-head">
                    <div class="mine-img">
                        <img src="/wap/statics/images/message/geren_tou.png" alt="">
                    </div>
                    <div class="mine-name"><%=CookUserInfo.NikeName%></div>
                </div>
                <div class="mine-info">
                    <ul>
                        <li>
                            <div class="mine-tit"><span id="latewithdraw"></span></div>
                            <p><a href="#"></a></p>
                        </li>
                        <li>
                            <div class="mine-tit">￥<span id="balance">0</span></div>
                            <p><a href="/wap/users/mine/AmountChangeList.aspx">余额</a></p>

                        </li>
                        <li>
                            <a class="mine-refresh1" href="javascript:getCurBalance();" style="color: rgb(255, 255, 255);">刷新金额</a>
                        </li>
                        <!--<li>
                        <div class="mine-tit">￥<span id="wintotal">0</span></div>
                        <p><a href="/mine/betList.html?onlyWin=1">最近中奖金额</a></p>
                    </li>-->
                    </ul>
                </div>
            </div>

            <div class="mine-but">
                <a href="/Mobile/pay/Payment.aspx" class="recharge">
                    <img src="/wap/statics/images/message/geren_cz_01.png"></a>
                <a href="/Mobile/user/Mention.aspx" class="withdraw">
                    <img src="/wap/statics/images/message/geren_tixian_01.png" alt=""></a>
            </div>

            <div class="mine-list">
                <ul>
                    <li>
                        <a href="/Mobile/userCenter/EditUser.aspx">
                            <img src="/wap/statics/images/message/shezhi_04.png" alt="">新增用户
                        </a>
                    </li>
                    <li>
                        <a href="/wap/users/mine/accountDetail.aspx">
                            <img src="/wap/statics/images/message/geren_tubiao_13.png" alt="">投注记录
                        </a>
                    </li>
                    <li>
                        <a href="/wap/users/mine/CatchList.aspx">
                            <img src="/wap/statics/images/message/geren_tubiao_24.png" alt="">追号记录
                        </a>
                    </li>
                    <li>
                        <a href="/wap/users/mine/AmountChangeList.aspx">
                            <img src="/wap/statics/images/message/geren_tubiao_26.png" alt="">账户明细
                        </a>
                    </li>
                    <li style="display: none;">
                        <a href="/mine/depositList.html">
                            <img src="/wap/statics/images/message/geren_tubiao_04.png" alt="">充值记录
                        </a>
                    </li>
                    <li>
                        <a href="/wap/users/mine/MentionList.aspx">
                            <img src="/wap/statics/images/message/geren_tubiao_13.png" alt="">提款记录
                        </a>
                    </li>
                    <li>
                        <a onclick="gotoMsgList(this)" data-url="/wap/users/mine/Message.aspx">
                            <img src="/wap/statics/images/message/geren_tubiao_06.png" alt="">个人消息<span id="count_unread"></span> <i class="red-icon" id="flag_unread" style="display: none;"></i>
                        </a>
                    </li>
                    <li>
                        <a href="/Mobile/user/BindBankCard.aspx">
                            <img src="/wap/statics/images/message/geren_tubiao_13.png" alt="">绑定银行卡
                        </a>
                    </li>
                    <li>
                        <a href="/Mobile/userCenter/ProfitLossList.aspx">
                            <img src="/wap/statics/images/message/geren_tubiao_24.png" alt="">团队盈亏
                        </a>
                    </li>
                    <li>
                        <a href="/wap/users/mine/set.html">
                            <img src="/wap/statics/images/message/geren_tubiao_30.png" alt="">更多
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>


    <uc1:Bottom runat="server" ID="Bottom" />

    <input type="hidden" id="refresh_unread" value="0">


    <style>
        .center {
            text-align: center;
        }
    </style>



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




    <script>

        $('a.mine-refresh1').click(function () {
            getCurBalance();
            getWinAmount();
        });

        $("div.mine-img").click(function () {
            location.href = "/wap/users/mine/set.html";
        });

        //当前余额
        function getCurBalance() {
            $.ajax({
                type: 'POST',
                url: '/page/Initial.aspx?action=userbalance',
                data: { "uid": Ytg.common.user.info.user_id },
                timeout: 10000, success: function (data) {
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0) {
                        //获取成功
                        $('span#balance').text(Ytg.tools.moneyFormat(jsonData.Data.UserAmt));
                    } else if (jsonData.Code == 1009) {
                        reLogin(data.Desc);
                    } else {
                        return false;
                    }
                },
                error: function () {

                }
            });

        }

        //累计投注总额，最近出款总额
        function getWinAmount() {
            /* $.ajax({
                 url: '/mine/ajaxBalance.html',
                 type: 'POST',
                 dataType: 'json',
                 data: {
                 },
                 timeout: 30000,
                 success: function (data) {
                     $('a.mine-refresh1').css('color', '#fff');
                     if (data.Result == false) {
                         msgAlert(data.Desc);
                         reLogin(data.Desc);
                         return;
                     }
                   
                     if (data.fLatelyWithdraw != undefined) {
                         $('span#latewithdraw').text(data.fLatelyWithdraw);
                     }
                 }
             });*/
        }

    </script>

    <script>
        function gotoMsgList(obj) {
            //启动定时器
            var unReadCount = $('span#count_unread').text();
            if ($('span#count_unread').text().trim() != '') {//有未读
                $('#refresh_unread').val(1);//需要刷新未读信息数量
                setUnReadTimer($(obj).data('url'));
            } else {
                location.href = $(obj).data('url');
            }
        }
    </script>

    <script>
        var timernounread = '';
        function setUnReadTimer(url) {
            clearInterval(timernounread);//删除定时器
            localTimeUnread = 0;
            tmpLocalTime = new Date().getTime() + 0;//本机毫秒数
            timernounread = window.setInterval(function () {
                tmpLocalTime = new Date().getTime() + 0;//本机毫秒数
                if ((tmpLocalTime - localTimeUnread) > 500) {//倒计时已不准确，重新获取倒计时
                    clearInterval(timernounread);//删除定时器
                    getCurBalance();
                    getWinAmount();
                    if ($('#refresh_unread').val() == 1) {

                        $('#refresh_unread').val(0);
                    }
                }
                localTimeUnread = tmpLocalTime;
            }, 300);
            location.href = url;//跳转前，启动了定时器
        }
        $('#indexlog_out').click(function () {

        });
        function doConfirmOk() {

            $.ajax({
                url: "/Page/Initial.aspx",
                type: 'post',
                data: "action=logout",
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        window.location = "/wap/login.html";
                    }
                }
            });

        }
    </script>

</body>
</html>
