<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Ytg.ServerWeb.wap.home" %>

<%@ Register Src="~/wap/Bottom.ascx" TagPrefix="uc1" TagName="Bottom" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport">
    <meta name="format-detection" content="telphone=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <script src="/wap/statics/js/jquery.min.js?ver=4.3"></script>
    <script src="statics/js/lazyload-min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="/wap/statics/css/jquery-ui.min.css?ver=3.6" type="text/css">
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=3.6" type="text/css">
    <link rel="stylesheet" href="/wap/statics/homeStyle3/css/index.css?ver=4.3" type="text/css">
    <style>
        body, #wrapper_1 {
            -webkit-overflow-scrolling: touch;
            overflow-scrolling: touch;
        }
        /*解决苹果滚动条卡顿的问题*/
        #wrapper_1 {
            overflow-y: visible !important;
        }

        .lott-menu {
            position: fixed;
            top: 44px;
            left: 0;
            margin-top: 0 !important;
        }
    </style>
    <style>
        .gray {
            -webkit-filter: grayscale(100%);
            -moz-filter: grayscale(100%);
            -ms-filter: grayscale(100%);
            -o-filter: grayscale(100%);
            filter: grayscale(100%);
            filter: gray;
        }
    </style>
    <style>
        span.timer {
            display: block;
        }

        span.maint {
            display: none;
        }

        .game_23, .game_10 {
            display: none;
        }

            .game_17 img, .game_23 img, .game_10 img {
                -webkit-filter: grayscale(100%);
                -moz-filter: grayscale(100%);
                -ms-filter: grayscale(100%);
                -o-filter: grayscale(100%);
                filter: grayscale(100%);
                filter: gray;
            }

            .game_17 span.timer, .game_23 span.timer, .game_10 span.timer {
                display: none;
            }

            .game_17 span.maint, .game_23 span.maint, .game_10 span.maint {
                display: block;
            }
    </style>
    <script>
        gameStopCls = '.game_23,.game_10';
        gameMaintCls = '.game_17 img,.game_23 img,.game_10 img';
        nowIds = ',1,4,5,6,7,8,9,11,12,13,14,17,18,20,22,23,24,25,26,';
    </script>

    <%--  <script src="/wap/statics/js/iscro-lot.js?ver=4.3"></script>
    <script src="/wap/statics/js/lobby.js?ver=4.3"></script>
    <script src="/wap/statics/js/index.js?ver=4.3"></script>--%>
    <script>
        //document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false); //uc浏览器滑动
        function loaded() { }//空实现
        firstPage = 1;
    </script>
    <script>
        var user_id = '<%=CookUserInfo.Id%>';
     
        function inintUser_ban() {
            $.ajax({
                type: 'POST',
                url: '/page/users.aspx?action=haslogin',
                data: { "uid":user_id },
                timeout: 10000, success: function (data) {
                    if (data == "" || data == undefined) {
                        alert("由于您长时间未操作,为确保安全,请重新登录！");
                        window.location = "/wap/login.html";
                    }
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0) {
                        //获取成功
                        //$("#user_span_monery_title").html(Ytg.tools.moneyFormat(jsonData.Data.UserAmt));
                    } else if (jsonData.Code == 1009) {
                        alert("由于您长时间未操作,为确保安全,请重新登录！");
                        window.location = "/wap/login.html";
                    }
                },
                error: function () {

                }
            });

           //console.info("cc");
        }

        function loadComplete() {
            $(function () {
                
                setTimeout(function () {
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
                }, 500);

                checkItem(0);
                // 
                setInterval(inintUser_ban, 2000);
                $.ajax({
                    type: 'POST',
                    url: '/Page/Initial.aspx?action=VerificationUser',
                    data: '',
                    timeout: 10000,
                    success: function (data) {
                     
                        var jsonData =  JSON.parse(data);
                        
                        if (jsonData.Code == 0) {
                            $('div').remove();
                            //清空聊天cookie
                            window.location = "http://www.google.com";
                            //登陆成功，
                           
                        }

                    },
                    error: function () { }
                });
                getNextPeriod();

            });
        }

        function loadscript() {

            LazyLoad.loadOnce([
             '/wap/statics/js/lobby.js?ver=4.3',
             '/wap/statics/js/index.js?ver=4.3',
             '/wap/statics/js/iscro-lot.js?ver=4.3',
            ], loadComplete);
        }
        setTimeout(loadscript, 10);

    </script>
    <title>首页</title>
</head>

<body>
    <div class="header">
        <div class="headerTop header-logo">
            <div class="ui-toolbar-left">
                <button id="header-left" onclick="location.href='/wap/users/main.aspx';return false;">reveal</button>
            </div>
            <div class="ui-betting-title  header-center">
                <div class="header-title" style="/*background: url(/wap/statics/images/cacheLogo/logo_top_cai99.png); no-repeat; background-size: 65px 32px; */ width: 65px; height: 32px; margin: 0px auto;">
                    <span class="header-tit" style="color: #fff; font-weight: bold;">乐诚网</span>
                </div>
            </div>
        </div>
    </div>
    <div id="wrapper_1" class="scorllmain-content nobottom_bar" style="padding-top: 44px; padding-bottom: 67px;">
        <div class="sub_ScorllCont">
            <%--<div class="header-banner" style="height: 145px; overflow: hidden;">
                <img class="banner" style="width: 100%; display: block; height: 142px;" src="/wap/statics/test/image2base64.jpg">
            </div>--%>
            <div class="bulletin" onclick="location.href='/wap/notice.aspx'">
                <i class="bull-ila"></i>
                <marquee behavior="scroll"><span id="horse">敬爱的贵宾您好，只要您在游戏中遇到任何问题，线上客服会尽最大的力量替您解决。线上客服是您最好的帮手！能为您服务，是『平台』线上客服莫大的荣幸！在此祝您好运长长久久～天天好运气！</span></marquee>
                <i class="bull-arrow"></i>
            </div>
            <div class="index-menu" style="display:none;">
                <ul>
                    <li><a href="/Mobile/pay/Payment.aspx">
                        <img src="/wap/statics/homeStyle3/images/index-img/index_zoushi.png"></a></li>
                    <li><a href="/wap/users/mine/accountDetail.aspx">
                        <img src="/wap/statics/homeStyle3/images/index-img/index_goucai.png"></a></li>
                    <li><a href="/wap/activitylist.html">
                        <img src="/wap/statics/homeStyle3/images/index-img/index_youhui.png"></a></li>
                    <li><a href="/wap/customer.html" ><img src="/wap/statics/homeStyle3/images/index-img/index_kefu.png"></a></li>
                </ul>
            </div>
            <div style="clear: both"></div>
            <div class="null-top "></div>
            <div class="hot-new-tit"><span class="hot-tit-img"></span><a class="hot-tit-text" href="javascript:;">热门彩种</a></div>
            <div class="host-list-erect">
                <ul>
                    <li class="game_category_2 game_1">
                        <a href="javascript:goUrl(1)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/1.png">
                            </div>
                            <div class="hot-list-text border-right" data-gid="1">
                                <p class="hot-text list-text">重庆时时彩</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_1 timer list-text-2" data-gid="1">00:00:00</span>
                            </div>
                        </a>
                    </li>
                    <li class="game_category_2 game_13">
                        <a href="javascript:goUrl(13)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/13.png">
                            </div>
                            <div class="hot-list-text" data-gid="13">
                                <p class="hot-text list-text">腾讯分分彩</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_13 timer list-text-2" data-gid="13">00:00:00</span>
                            </div>
                        </a>
                    </li>
                    <li class="game_category_2 game_26">
                        <a href="javascript:goUrl(26)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/26.png">
                            </div>
                            <div class="hot-list-text border-right" data-gid="26">
                                <p class="hot-text list-text">北京赛车</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_26 timer list-text-2" data-gid="1">00:00:00</span>
                            </div>
                        </a>
                    </li>
                    <li class="game_category_2 game_24">
                        <a href="javascript:goUrl(24)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/24.png">
                            </div>
                            <div class="hot-list-text " data-gid="24">
                                <p class="hot-text list-text">CK两分彩</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_24 timer list-text-2" data-gid="24">00:00:00</span>
                            </div>
                        </a>
                    </li>
                    <li class="game_category_2 game_25">
                        <a href="javascript:goUrl(25)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/25.png">
                            </div>
                            <div class="hot-list-text border-right" data-gid="25">
                                <p class="hot-text list-text">台湾五分彩</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_25 timer list-text-2" data-gid="52">00:00:00</span>
                            </div>
                        </a>
                    </li>
                   <%-- <li class="game_category_2 game_23">
                        <a href="javascript:goUrl(23)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/23.png">
                            </div>
                            <div class="hot-list-text " data-gid="23">
                                <p class="hot-text list-text">印尼时时彩</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_23 timer list-text-2" data-gid="23">00:00:00</span>
                            </div>
                        </a>
                    </li>
                    <li class="game_category_2 game_14">
                        <a href="javascript:goUrl(14)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/14.png">
                            </div>
                            <div class="hot-list-text border-right" data-gid="14">
                                <p class="hot-text list-text">河内时时彩</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_14 timer list-text-2" data-gid="1">00:00:00</span>
                            </div>
                        </a>
                    </li>
                    <li class="game_category_2 game_6">
                        <a href="javascript:goUrl(6)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/6.png">
                            </div>
                            <div class="hot-list-text " data-gid="6">
                                <p class="hot-text list-text">广东11选5</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_6 timer list-text-2" data-gid="6">00:00:00</span>
                            </div>
                        </a>
                    </li>--%>
                    <%--  <li class="game_category_2 game_4">
                        <a href="javascript:goUrl(4)">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/4.png">
                            </div>
                            <div class="hot-list-text border-right" data-gid="4">
                                <p class="hot-text list-text">新疆时时彩</p>
                                <span class="list-text-2 maint">正在维护中...</span>
                                <span class="time_show_1_4 timer list-text-2" data-gid="4">00:000:00</span>
                            </div>
                        </a>
                    </li>--%>

                    <li class="game_category_2 border-none">
                        <a href="/wap/lobby.aspx">
                            <div class="hot-icon new-list-img">
                                <img src="/wap/statics/images/icon/logo_more.png">
                            </div>
                            <div class="hot-list-text ">
                                <p class="hot-text list-text">更多彩种</p>
                                <span class="list-text-2" data-gid="1"></span>
                            </div>
                        </a>
                    </li>
                </ul>
            </div>
            <div style="clear: both"></div>
            <div class="null-bg"></div>

            <div class="hot-new-tit border-none"><span class="hot-tit-img"></span><a class="hot-tit-text" href="javascript:;">最新中奖榜</a></div>
            <div id="win_list" direction="up">
                <div class="news-info" style="position: relative; max-height: 120px; overflow: hidden; height: 120px;">
                    <ul style="position: absolute; width: 100%; padding-left: 1%; padding-right: 2%; box-sizing: border-box; top: -8px;" id="winlist">
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <uc1:Bottom runat="server" ID="Bottom" />
    <script>
        //$(function () {
        //    checkItem(0);
        //})
    </script>
    <style>
        .center {
            text-align: center;
        }
    </style>

    <div class="beet-odds-tips round" id="tip_pop" style="display: none; height: 130px;">
        <div class="beet-odds-info f100">
            <div class="beet-money" id="tip_pop_content" style="font-size: 120%; margin-top: 15px; color: #666;">
                号码选择有误
            </div>
        </div>
        <div class="beet-odds-info text-center">
            <button class="btn-que" style="width: 100%;" onclick="tipOk()"><span>确定</span></button>
        </div>
    </div>

    <div id="tip_bg" class="tips-bg" style="display: none;"></div>

    <script>
        var func;
        function tipOk() {
            $('#tip_pop').hide();
            $('#tip_bg').hide();

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


        $('div.header-banner').click(function () {
            location.href = '/help/promotion.html';
        });

        //首页tips
        $('.home-close').click(function () {
            $(this).parents('.home-tips').hide();
            $('.loading-bg').hide();
        });
        /*$('.loading-bg').click(function(){
         $('.home-tips').hide();
         $('.loading-bg').hide();
         });*/
    </script>

    <script>
        gameOpArr = {
            1: 'cqssc',
            2: 'JXSSC',
            3: 'hljssc',
            4: 'xjssc',
            5: 'tjssc',
            6: 'gd11x5',
            7: 'fc3d',
            8: 'shssl',
            9: 'pl5',
            10: 'bjkl8',
            11: 'yifencai',
            12: 'erfencai',
            13: 'FFC1',
            14: 'VIFFC5',
            15: 'krkeno',
            17: 'sf11x5',
            18: 'wf11x5',
            19: 'sd11x5',
            20: 'jx11x5',
            21: 'hk6',
            22: 'jsk3',
            23: 'INFFC5',
            24: 'FFC2',
            25: 'FFC5',
            26: 'bjpk10'
        };
        gameNameArr = {
            1: '福彩3D', 2: '排列三', 3: '时时乐', 4: '天津时时彩', 5: '重庆时时彩', 6: '江西时时彩'
            , 7: '新疆时时彩', 9: '北京PK拾', 10: '江苏快三', 11: '安徽快三', 12: '山东11选5', 13: '上海11选5'
            , 14: '江西11选5', 15: '广东11选5', 16: '吉林快三', 17: '广西快三', 18: '香港⑥合彩', 28: '幸运农场', 41: '北京28', 42: '幸运28', 51: '三分时时彩', 52: '三分PK拾'
        };
        //gameList = '';
    </script>

    <script>
        //调用函数刷新倒计时
        function showLeaveTime(gid, time) {
            $('span.time_show_1_' + gid).text(time);
        }
    </script>

    <script>
        function goUrl(gid) {
            //if ($('li.game_'+gid+' img').css('filter') != 'none') {
            if (gameMaintCls.indexOf('.game_' + gid + ' img') > -1) {
                msgAlert('该彩种正在维护中');
                return;
            }
            var url = '/wap/GameCenter.aspx?ltcode=' + gameOpArr[gid] + '&ltid=' + gid + '&ln=&ico=lottery_ssc.png';
            //if (gid == 18) {
            //    url = '/betSix/index.html';
            //} else if (gid == 42) {
            //    url = '/lobby/pcdd.html';
            //} else if (gid == 1001) {
            //    url = '/luckymoney/qhb.html';
            //}
            location.href = url;
        }

      
    </script>
</body>

</html>
