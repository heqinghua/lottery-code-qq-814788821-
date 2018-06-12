<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.mine.Message" %>

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
    <script src="/wap/statics/js/jquery-ui.min.js?ver=4.4"></script>
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>

    <script>
        isLogin = true;
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
    <!-- hide address bar -->
    <title>会员消息</title>
</head>
<body class="login-bg" onload="loaded()" style="">
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-betting-title">
                <div class="bett-top-box">
                    <div class="bett-tit"><span id="msg_type">所有类型</span><i class="bett-attr"></i></div>
                </div>
            </h1>
            <div class="ui-bett-refresh">
                <a class="bett-refresh" href="javascript:;"></a>
            </div>
        </div>
    </div>

    <div id="wrapper_1" class="scorllmain-content scorll-order nobottom_bar" style="padding-top: 44px; padding-bottom: 44px;">
        <div class="sub_ScorllCont">
            <div class="mine-message" style="">
                <div class="mine-mess">
                    <img src="/wap/statics/images/message/wuxinxi.png"></div>
                <p>目前暂无记录哦！</p>
            </div>
            <div class="order-center">
                <ul id="msg_list">
                  
                </ul>
                <a class="on-more" href="javascript:;" style="display: none;">点击加载更多</a>
            </div>
        </div>
    </div>

    <!-- 直选tips -->
    <div class="beet-tips" hidden="">
        <!--<div class="beet-tips-tit"><span>普通玩法</span></div>-->
        <div class="clear"></div>
        <ul>
            <li><a class="beet-active" href="javascript:;" data-type="-1">所有类型</a></li>
            <li><a href="javascript:;" data-type="1">系统消息</a></li>
            <li><a href="javascript:;" data-type="2">私人消息</a></li>
            <li><a href="javascript:;" data-type="4">中奖消息</a></li>
            <li><a href="javascript:;" data-type="8">充提信息</a></li>
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


    <script>
        function initLoading() {
            $('a.on-more').hide();
            $('div.mine-message').hide();
        }
    </script>

    <script>
        $('a.on-more').click(function () {
            getMessage(1);
        });
    </script>

    <script>
        $('div.beet-tips a').click(function () {
            $('div.beet-tips a').removeClass('beet-active');
            $('div.beet-tips').hide();
            $(this).addClass('beet-active');
            $('span#msg_type').text($(this).text());
            msgType = $(this).data('type');
            initLoading();
            getMessage(2);
        });
    </script>

    <script>
        $('div.ui-bett-refresh').click(function () {
            initLoading();
            getMessage(2);
        });
    </script>

    <script>
        $(function () {
            getMessage();
        });
    </script>

    <script>
        //获取投注列表
        msgType = 0;
        var pageIndex = 1;
        var pagesize = 20;
        function getMessage(more) {
            if (more == 1) {
                pageIndex++;
            } else if (more == 2) {
                pageIndex = 1;
            }
            loadingShow();
            $('a.on-more').html("正在获取数据中..");
            var state = $(".beet-active").attr("data-type");
            var tp = -1;
            $.ajax({
                url: "/Page/Messages.aspx",
                type: 'post',
                data: "action=selectmessages&status=" + state + "&messageType=" + tp + "&pageIndex=" + pageIndex + "&pageSize=" + pagesize,
                success: function (data) {
                  
                    var jsonData = JSON.parse(data);
                    //清除
                    $('ul#msg_list li.loading').remove();
                    
                    var txtHtml = '';
                    var statusHtml = '';
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                      
                        
                        for (var c = 0; c < jsonData.Data.length; c++) {

                            var item = jsonData.Data[c];
                            var tdTitle = item.Title.length > 25 ? item.Title.substring(0, 25) : item.Title;//40
                            var tdMessage = item.MessageContent.length > 25 ? item.MessageContent.substring(0, 24) : item.MessageContent;

                            var statusMsg = item.Status == 0 ? "<span style='color:#cd0228;'>未读</span>" : "已读";

                            //var htm = "<tr id='tr_id_" + item.Id + "'><td>" + item.OccDate + "</td><td><a href=\"javascript:showDetails('" + " " + "','" + html_encode(item.MessageContent) + "'," + item.Id + "," + item.Status + ")\">" + tdTitle + "</a></td><td>" + tdMessage + "</td><td>" + statusMsg + "</td><td><a href='javascript:del(" + item.Id + ")'>删除</a></td></tr>"
                            //$(".ltbody").append(htm);
                           
                            txtHtml += '<li>'
                         + '<a   onclick="gotoDetail(this,' + item.Id + ',' + item.Status + ')" data-unread="' + item.Status + '" data-url="/wap/users/mine/msgDetail.aspx?id=' + item.Id + '">'
                             + '<div class="order-list-tit">'
                             + statusMsg + "&nbsp;&nbsp;&nbsp;" + tdTitle
                         + '</div>'
                         + '<div class="c-gary"><span class="fr"></span><p class="order-time">' + item.OccDate + '</p></div>'
                         + '</a>'
                     + '</li>';
                        }

                        $('div.mine-message').hide();

                        if (more == 2) {//刷新
                            $('ul#msg_list').html(txtHtml);
                        } else {
                            $('ul#msg_list').append(txtHtml);
                        }
                        $('a.on-more').html("点击加载更多");
                        var tot = jsonData.Total;
                        var totalPage = 1;
                        if (tot > pagesize)
                            totalPage = (tot % pagesize == 0 ? (tot / pagesize) : (tot / pagesize) + 1);

                        if (totalPage > pageIndex) {
                            $('a.on-more').show();
                        } else {
                            $('a.on-more').hide();
                        }

                    }
                    else {
                        $('ul#msg_list').html("");
                        $('div.mine-message').show();
                    }

                    loadingHide();
                    loaded();
                }
            });

        }

        function html_encode(str) {
            var s = "";
            if (str.length == 0) return "";
            s = str.replace(/&/g, ">");
            s = s.replace(/</g, "<");
            s = s.replace(/>/g, ">");
            s = s.replace(/ /g, " ");
            s = s.replace(/\'/g, "'");
            s = s.replace(/\"/g, "");
            s = s.replace(/\n/g, "<br>");
            return s;
        }

        //解码
        function html_decode(str) {
            var s = "";
            if (str.length == 0) return "";
            s = str.replace(/>/g, "&");
            s = s.replace(/</g, "<");
            s = s.replace(/>/g, ">");
            s = s.replace(/ /g, " ");
            s = s.replace(/'/g, "\'");
            s = s.replace(/"/g, "\"");
            s = s.replace(/<br>/g, "\n");
            return s;
        }
    </script>

    <script>
        function gotoDetail(obj,id, state) {
            //标记为已读消息
            if (state == 0) {
                $.ajax({
                    url: "/Page/Messages.aspx",
                    type: 'post',
                    data: "action=setread&mid=" + id,
                    success: function (data) {

                    }
                });
            } else {
                window.location = $(obj).attr("data-url");
            }
        }
    </script>

    
    
</body>
</html>
