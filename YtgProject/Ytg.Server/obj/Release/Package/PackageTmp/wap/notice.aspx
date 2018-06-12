<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notice.aspx.cs" Inherits="Ytg.ServerWeb.wap.notice" %>
<!DOCTYPE html>
<html>
<head>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <meta name="format-detection" content="telephone=no">
    <meta name="mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" href="/wap/statics/css/jquery-ui.min.css?ver=4.3" type="text/css">
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.3" type="text/css">
    <script src="/wap/statics/js/jquery.min.js?ver=4.3"></script>
    <script src="/wap/statics/js/jquery-ui.min.js?ver=4.3"></script>
    <script src="/wap/statics/js/iscro-lot.js?ver=4.3"></script>

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
    <title>公告</title>
</head>
<body>
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">公告</h1>
        </div>
    </div>
    <div style="height: 44px;" class="ipad-top"></div>

    <style>
        html, body, div, span, applet, object, iframe, h1, h2, h3, h4, h5, h6, p, blockquote, pre, a, abbr, acronym, address, big, cite, code, del, dfn, em, img, ins, kbd, q, s, samp, small, strike, strong, sub, sup, tt, var, b, u, i, center, dl, dt, dd, ol, ul, li, fieldset, form, label, legend, table, caption, tbody, tfoot, thead, tr, th, td, article, aside, canvas, details, figcaption, figure, footer, header, hgroup, menu, nav, section, summary, time, mark, audio, video, input {
            margin: 0;
            padding: 0;
            border: none;
            outline: 0;
            font-size: 100%;
            font: inherit;
            vertical-align: baseline;
        }

        html, body, form, fieldset, p, div, h1, h2, h3, h4, h5, h6 {
            -webkit-text-size-adjust: none;
        }

        article, aside, details, figcaption, figure, footer, header, hgroup, menu, nav, section {
            display: block;
        }

        ol, ul {
            list-style: none;
        }

        blockquote, q {
            quotes: none;
        }

            blockquote:before, blockquote:after, q:before, q:after {
                content: '';
                content: none;
            }

        ins {
            text-decoration: none;
        }

        del {
            text-decoration: line-through;
        }

        table {
            border-collapse: collapse;
            border-spacing: 0;
        }

        body {
            font-family: 'Helvetica Neue', Arial, "Hiragino Sans GB", 'Microsoft YaHei', sans-serif;
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            -webkit-text-size-adjust: none;
            -moz-text-size-adjust: none;
            -ms-text-size-adjust: none;
            -o-text-size-adjust: none;
            text-size-adjust: none;
        }

        a {
            color: #fff;
            text-decoration: none;
        }

            a:hover {
                color: #fff;
                text-decoration: none;
            }

        body {
            height: 100%;
            min-height: 100%;
        }

        .wrap {
            background: #f3f3f3;
            height: 100%;
            min-height: 100%;
            overflow: hidden;
            padding-bottom: 15px;
            background-size: cover;
        }

        .bull-box {
            position: relative;
        }

        .bull-line {
            background: #ddd;
            width: 2px;
            height: 100%;
            position: absolute;
            left: 8%;
            top: 0;
        }

        .bull-icon {
            background: url(/wap/statics/images/new_icon.png) #fff no-repeat center;
            width: 15px;
            height: 16px;
            background-size: 15px 16px;
            border: 2px solid #ddd;
            border-radius: 50%;
            padding: 8px;
            display: block;
            position: absolute;
            left: 8%;
            top: 5px;
            margin-left: -17px;
        }

        .bulletin-right {
            border: 1px solid #e7e7e7;
            box-shadow: 1px 2px 1px #e7e7e7;
            width: 73%;
            background: #fff;
            position: relative;
            left: 18%;
            font-size: 80%;
            margin-top: 20px;
            border-radius: 3px;
            padding: 10px;
            color: #333;
        }

            .bulletin-right h3 {
                color: #ec2929;
                font-size: 16px;
            }

        .san-icon {
            background: url(/wap/statics/images/san_icon.png) no-repeat;
            background-size: 12px 19px;
            width: 12px;
            height: 19px;
            position: absolute;
            left: -12px;
            top: 10px;
        }

        .bull-txt {
            margin: 5px 0;
        }

        .bull-bot {
            border-top: 1px solid #ddd;
            color: #999;
            text-indent: 1em;
            padding-top: 5px;
        }
    </style>

    <div class="wrap" id="div_content_list">
        <div class="bull-line"></div>
       
      
    </div>

</body>
</html>
<script type="text/javascript">
    $(function () {
        $.ajax({
            url: '/Page/Initial.aspx',
            type: 'POST',
            dataType: 'json',
            data: {
                'action': 'news'
            },
            timeout: 30000,
            success: function (data) {
                if (data.Code == 0) {
                    var htmltem = "";
                    for (var i = 0; i < data.Data.length; i++) {
                       
                        htmltem += "<div class=\"bull-box\">" +
                            "<div class=\"bulletin-left\">"+
                        "<div class=\"bull-icon\"></div>" +
                        "</div>" +
                        "<div class=\"bulletin-right\">" +
                        "<span class=\"san-icon\"></span><h3>" + data.Data[i].Title + "</h3>" +
                        "<p class=\"bull-txt\">" + decodeURI(data.Data[i].Content).replace("乐诚网", "").replace("－－专注信誉，服务为本－－", "") + "</p><p class=\"bull-bot\">" + (data.Data[i].OccDate.split(" ")[0]) + "</p></div></div>";

                    }
                    $("#div_content_list").html(htmltem);
                }
            },
            error: function () {
            }
        });

    });
</script>