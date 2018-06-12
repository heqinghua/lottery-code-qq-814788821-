<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lobby.aspx.cs" Inherits="Ytg.ServerWeb.wap.lobby" %>

<%@ Register Src="~/wap/Bottom.ascx" TagPrefix="uc1" TagName="Bottom" %>

<html lang="en">
<head>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <meta name="format-detection" content="telphone=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="mobile-web-app-capable" content="yes">
    <script src="/wap/statics/js/jquery.min.js?ver=4.3"></script>
    <script src="statics/js/lazyload-min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css">
   <%-- <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>--%>

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
            checkItem(1);
            getNextPeriod();
        }

        function loadscript() {

            LazyLoad.loadOnce([
             '/wap/statics/js/iscro-lot.js?ver=4.4',
             '/wap/statics/js/lobby.js?ver=4.4',
            ], loadComplete);
        }
        setTimeout(loadscript, 10);
    </script>
    <title>购彩大厅</title>
    <%--<script src="/wap/statics/js/lobby.js?ver=4.4"></script>--%>
    <script>
        try {
            gameIds = '';
        } catch (e) { console.log(e); }
    </script>
    <!-- start  16-09-30 : 下面解决iscroll自带 bug (bugID=1613) -->
    <style>
        body, #wrapper_1 {
            -webkit-overflow-scrolling: touch;
            overflow-scrolling: touch;
        }
        /*解决苹果滚动条卡顿的问题*/
        #wrapper_1 {
            overflow-y: visible !important;
        }

        body.login-bg {
            padding-top: 97px;
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
    <script>
        //覆盖（重写 ）iscro-lot.js 同名方法避免bug产生 (只有使用IScroll.js的地方才需要调用iscro-lot.js文件中的 loaded()方法 )
        function loaded() { }//空实现
        firstPage = 0;//常亮
        //  document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false); //uc浏览器滑动
    </script>

    <!-- end  16-09-30 :  下面解决iscroll自带 bug (bugID=1613) -->
</head>
<body style="">
    <!-- onload='loaded()' -->
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">购彩大厅</h1>
            <div class="ui-toolbar-right ui-head">
                <a class="head-list" href="javascript:;"></a>
                <a class="head-icon" href="javascript:;"></a>
            </div>
        </div>
    </div>
    <div class="lott-menu">
        <ul>
            <li class="nav_category_0" data-cat="0" style="display: none;"><a href="javascript:;">
                <img src="/wap/statics/images/goucai_nav_02.png"></a></li>
            <li class="nav_category_0 nav_sel_0" data-cat="0" style="display: list-item;"><a href="javascript:;">
                <img src="/wap/statics/images/goucai_navs_02.png"></a></li>
            <li class="nav_category_1" data-cat="1" style="display: list-item;"><a href="javascript:;">
                <img src="/wap/statics/images/goucai_nav_03.png"></a></li>
            <li class="nav_category_1 nav_sel_1" data-cat="1" style="display: none;"><a href="javascript:;">
                <img src="/wap/statics/images/goucai_navs_03.png"></a></li>
            <li class="nav_category_2" data-cat="2" style="display: list-item;"><a href="javascript:;">
                <img src="/wap/statics/images/goucai_nav_04.png"></a></li>
            <li class="nav_category_2 nav_sel_2" data-cat="2" style="display: none;"><a href="javascript:;">
                <img src="/wap/statics/images/goucai_navs_04.png"></a></li>
        </ul>
    </div>
    <div id="wrapper_1" class="nobottom_bar lott_bar" style="padding-top: 115px; padding-bottom: 71px;">
        <div class="sub_ScorllCont">
            <div class="lottery-list" hidden="">
                <ul>
                    <asp:Repeater ID="rptChildren" runat="server">
                        <ItemTemplate>
                            <li class="game_category_<%# GetLotteryCatay(Eval("Id")) %>" style="display: list-item;"><a href="/wap/<%# GetLotteryUrl(Eval("LotteryCode")) %>.aspx?ltcode=<%# Eval("LotteryCode") %>&ltid=<%# Eval("Id") %>&ln=<%# System.Web.HttpUtility.UrlEncode(Eval("LotteryName").ToString()) %>&ico=<%# Eval("ImageSource") %>">
                                <p class="hot_start" id="hot_start_<%# Eval("Id") %>" style="visibility: hidden">即将开奖...</p>
                                <div class="hot-icon">
                                    <img class="" src="/wap/statics/images/icon/<%# Eval("Id") %>.png">
                                </div>
                                <p class="hot-text"><%# Eval("LotteryName") %></p>
                                <p class="last-time" data-gid="<%# Eval("Id") %>" data-time=""><span class="time_show_2_<%# Eval("Id") %>">00:00:00</span></p>
                            </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="lottery-list-erect">
                <ul>
                    <asp:Repeater ID="rptChildrenerect" runat="server">
                        <ItemTemplate>
                            <li class="game_category_<%#  GetLotteryCatay(Eval("Id")) %>" style="display: list-item;"><a href="/wap/<%# GetLotteryUrl(Eval("LotteryCode")) %>.aspx?ltcode=<%# Eval("LotteryCode") %>&ltid=<%# Eval("Id") %>&ln=<%# System.Web.HttpUtility.UrlEncode(Eval("LotteryName").ToString()) %>&ico=<%# Eval("ImageSource") %>">
                                <div class="hot-icon">
                                    <img class="" src="/wap/statics/images/icon/<%# Eval("Id") %>.png">
                                </div>
                                <div class="erect-right erect-right-<%# Eval("Id") %>" data-gid="<%# Eval("Id") %>" data-time="">
                                    <div>
                                        <span class="fr red last_period_<%# Eval("Id") %>">第期</span><p class="hot-text f120"><%# Eval("LotteryName") %></p>
                                    </div>
                                    <p class="last-time last_open_<%# Eval("Id") %>">0 0 0 0 0</p>
                                    <p><span class="fr"><span class="time_show_1_<%# Eval("Id") %>" data-gid="<%# Eval("Id") %>">00:00:00</span></span><span style="font-size: 12px;">距第<span class="period_show_<%# Eval("Id") %>"></span>期 <span id="period_show_status_<%# Eval("Id") %>">截止</span>还有</span></p>
                                </div>
                            </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>

    <uc1:Bottom runat="server" ID="Bottom" />
   
    <style>
        .center {
            text-align: center;
        }
    </style>

    <script>
        nowIds = '<%=ids%>';// ',51,5,9,42,52,18,15,7,12,4,13,14,11,1,3,2,';
    </script>

    <script>
        gameShowType = 1;//1:列表，2:方格
        $('.head-list').click(function () {
            gameShowType = 2;
            $('.lottery-list').show();
            $('.lottery-list-erect').hide();
            $(this).parents('.ui-toolbar-right').removeClass('ui-head');
            loaded();
        });
        $('.head-icon').click(function () {
            gameShowType = 1;
            $('.lottery-list').hide();
            $('.lottery-list-erect').show();
            $(this).parents('.ui-toolbar-right').addClass('ui-head');
            loaded();
        });
    </script>



    <script>
        $('div.lott-menu li').click(function () {
            var cat = $(this).data('cat');
            if (cat == 1) {//高频
                $('div.lottery-list li.game_category_2').hide();
                $('div.lottery-list-erect li.game_category_2').hide();
                $('div.lottery-list li.game_category_1').show();
                $('div.lottery-list-erect li.game_category_1').show();
            } else if (cat == 2) {//低频
                $('div.lottery-list li.game_category_1').hide();
                $('div.lottery-list-erect li.game_category_1').hide();
                $('div.lottery-list li.game_category_2').show();
                $('div.lottery-list-erect li.game_category_2').show();
            } else {//全部
                $('div.lottery-list li').show();
                $('div.lottery-list-erect li').show();
            }
            for (var i = 0; i < 3; i++) {
                if (i == cat) {//选中
                    $('div.lott-menu li.nav_category_' + i).hide();
                    $('div.lott-menu li.nav_sel_' + i).show();
                } else {//未选中
                    $('div.lott-menu li.nav_category_' + i).show();
                    $('div.lott-menu li.nav_sel_' + i).hide();
                }
            }
            loaded();
        });
    </script>
