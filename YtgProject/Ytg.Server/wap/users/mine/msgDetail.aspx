<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="msgDetail.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.mine.msgDetail" %>

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
            <h1 class="ui-betting-title" id="title">
                消息详情
            </h1>

        </div>
    </div>
    <div id="wrapper_1" class="bet_detail scorllmain-content scorll-order nobottom_bar" style="padding-top: 44px;">
        <div class="sub_ScorllCont">

            <div class="order-info">
                <h3>消息内容</h3>
                <div id="msgContent">
                    <%=Request.QueryString["content"] %>
                </div>
            </div>

        </div>

    </div>
    <style>
        .center {
            text-align: center;
        }
    </style>

    <script>
        $(function () {

              $.ajax({
            url: "/Page/Messages.aspx",
            type: 'post',
            data: "action=getonemsg&mid=<%=Request.Params["id"]%>",
            success: function (data) {

                var jsonData = JSON.parse(data);
                //清除
                if (jsonData.Code == 0 ) {
                   // $("#title").html(jsonData.Data.Title);
                    $("#msgContent").html(jsonData.Data.MessageContent);
                }
                else {
                   
                }

            }
        });
      
        })

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


</body>
</html>
