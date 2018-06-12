<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Opens.aspx.cs" Inherits="Ytg.ServerWeb.wap.Opens" %>

<%@ Register Src="~/wap/Bottom.ascx" TagPrefix="uc1" TagName="Bottom" %>

<html lang="en">
<head>
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

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <meta name="format-detection" content="telphone=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="mobile-web-app-capable" content="yes">
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css">
    <script src="/wap/statics/js/jquery.min.js?ver=4.3"></script>
    <script src="statics/js/lazyload-min.js" type="text/javascript"></script>

    <script>
    try{
        isLogin = false;
        }catch(e){console.log(e);}
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
            checkItem(2);
            getResultList();
        }

        function loadscript() {

            LazyLoad.loadOnce([
             '/wap/statics/js/iscro-lot.js?ver=4.4',
             '/wap/statics/js/lobby.js?ver=4.4',
            ], loadComplete);
        }
        setTimeout(loadscript, 10);

    </script>
    <script>
        function loaded(){}//空实现
    </script>
    <title>开奖大厅</title>
</head>
<body class="login-bg" onload="loaded()">
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">开奖大厅</h1>
        </div>
    </div>
    <div id="wrapper_1" class="scorllmain-content nobottom_bar" style="padding-top: 44px; padding-bottom: 67px;">
        <div class="sub_ScorllCont">
            <div class="lott-list">
                <ul id="draw_list"></ul>
            </div>
        </div>
    </div>
    <!-- 加载中 -->
    <div class="loading" style="left: 50%; margin-left: -2em; display: none;">加载中...</div>
    <div class="loading-bg" style="display: none;"></div>

    <script>
    function loadingShow(tips,bg) {
        if(tips == ""||typeof(tips) == "undefined"){
            $(".loading").css("left","50%");
            $(".loading").css("margin-left","-2em");
            $(".loading").html("加载中...");
        }else{
            $(".loading").html(tips);
            $(".loading").css("left",Math.ceil(($(document).width() - $(".loading").width())/2));
            $(".loading").css("margin-left",0);
        }

        bg   = (bg == ""||typeof(bg) == "undefined")?1:0;
        if (bg == 1){
            $(".loading-bg").show();
        }else{
            $(".loading-bg").hide();
        }
        $(".loading").show();
    }
    function loadingHide() {
        $(".loading").hide();
        $(".loading-bg").hide();
    }
    </script>
    <uc1:Bottom runat="server" ID="Bottom" />
 
  

    <script>
    $(document).on('click', 'ul#draw_list a', function() {
        var gid = $(this).data('gid');
        var gname = $(this).attr('data-gname');
        if (gid == '' || gid == 0) {
            return;
        }
        setDrawTimer('/wap/OpensList.aspx?gameId=' + gid + "&gname=" + gname);
    });
    </script>

    <script>
    function getResultList() {
        loadingShow();
        $.ajax({
            url: '/Page/Lott/lottery.aspx?action=opens',
            type: 'POST',
            dataType: 'json',
            data: {
            },
            timeout: 30000,
            success: function (data) {
                loadingHide();
                
                if (data.Code ==1) {
                    return false;
                }
                var txtHtml = '';
                var ksgame = ',22,';
                var clsArr = {1:'nums-ing',2:'nums-open',3:'nums-pcdd',4:'nums-jsks'};
                for (var i = 0; i < data.Data.length; i++) {
                  
                    var numArr = data.Data[i].Result;
                  
                    numArr = (numArr != undefined && numArr != '') ? numArr.split(',') : [];
                    var numHtml = '';
                    var openType = 2;//1:正在开奖,2:有开奖号码,3:PC蛋蛋开奖号码(n1+n2+n3=n4),4:江苏快三(绿色背景)
                    if (ksgame.indexOf(',' + data.Data[i].LotteryId + ',') > -1) {//快三
                        var tmpHtml = '';
                        for (var j = 0; j < numArr.length; j++) {
                            tmpHtml += '<li><img src="/wap/statics/images/lottery/touzi_0'+numArr[j]+'.png"></li>';
                        }
                        if (tmpHtml == '') {
                            tmpHtml = '正在开奖';
                        }
                        openType = 4;
                        numHtml = '<div class="lott-k3 '+ clsArr[openType] +'">'
                            + '<div class="k3-number k3-lottery">'
                                + '<ul class="k3-lot"><span class="nums">' + tmpHtml + '</span></ul>'
                            + '</div>'
                        + '</div>';
                    } else {
                        var tmpHtml = '';
                        for (var j = 0; j < numArr.length; j++) {
                            tmpHtml += '<i>'+numArr[j]+'</i>';
                        }
                        if (tmpHtml == '') {
                            openType = 1;
                            tmpHtml = '正在开奖';
                        } else if (data.Data[i].LotteryId == 41 || data.Data[i].LotteryId == 42) {
                            openType = 3;
                        }
                        numHtml = '<div class="two-ball two-lottery '+ clsArr[openType] +'"><span class="nums">' + tmpHtml + '</span></div>';
                        if ((data.Data[i].LotteryId == 41 || data.Data[i].LotteryId == 42) && numArr.length > 0) {
                            numHtml = '<div class="two-ball tow-ball-cont '+ clsArr[openType] +'" style="font-size:140% !important;">'+numArr[0]+' + '+numArr[1]+' + '+numArr[2]+' = '+numArr[3]+'</div>';
                        }
                    }
                    txtHtml += '<li class="list-k3">'
                        + '<a href="javascript:;" data-gid="' + data.Data[i].LotteryId + '" data-gname="' + data.Data[i].LotteryName + '">'
                            + '<div class="lott-list-tit">'
                            + '<span>' + data.Data[i].LotteryName + '</span><span class="gray" style="font-size: 11px;">第' + data.Data[i].IssueCode + '期 ' + data.Data[i].EndTime + '</span>'
                            + '</div>'
                            + numHtml
                        + '</a>'
                    + '</li>';
                }
                $('ul#draw_list').html(txtHtml);
                loaded();
            }
        });
    }
    </script>

    <script>
    var timernounread = '';
    function setDrawTimer(url) {
        clearInterval(timernounread);//删除定时器
        localTimeUnread = 0;
        tmpLocalTime = new Date().getTime() + 0;//本机毫秒数
        timernounread = window.setInterval(function () {
            tmpLocalTime = new Date().getTime() + 0;//本机毫秒数
            if ((tmpLocalTime-localTimeUnread) > 500) {//倒计时已不准确，重新获取倒计时
                clearInterval(timernounread);//删除定时器
                getResultList();
            }
            localTimeUnread = tmpLocalTime;
        }, 300);
        location.href = url;//跳转前，启动了定时器
    }
    </script>
</body>
</html>