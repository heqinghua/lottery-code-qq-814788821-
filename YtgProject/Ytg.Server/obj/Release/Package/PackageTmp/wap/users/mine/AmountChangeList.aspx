<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmountChangeList.aspx.cs" Inherits="Ytg.ServerWeb.wap.users.mine.AmountChangeList" %>
<html lang="en"><head>
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
    <script src="/wap/statics/js/jquery.min.js?ver=4.4"></script>
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
       <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>

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
    <title>账变列表</title>
</head>
<body class="login-bg" onload="loaded()">
<div class="header">
    <div class="headerTop">
        <div class="ui-toolbar-left">
            <button id="reveal-left" type="button">reveal</button>
        </div>
        <h1 class="ui-betting-title">
            <div class="bett-top-box">    
                <div class="bett-tit"><span id="account_type">全部类型</span><i class="bett-attr"></i></div>
            </div>
        </h1>
        <div class="ui-bett-refresh">
            <a class="bett-refresh" href="javascript:;"></a>
        </div>
    </div>
</div>

<div id="wrapper_1" class="scorllmain-content scorll-order nobottom_bar" style="padding-top: 44px; padding-bottom: 44px;">
    <div class="sub_ScorllCont">
        <div class="mine-message" style="display: none;">
            <div class="mine-mess"><img src="/wap/statics/images/message/wuxinxi.png"></div>
            <p>目前暂无记录哦！</p>
        </div>
        <div class="order-center">
            <ul id="account_list"></ul>
            <a class="on-more" href="javascript:;" style="display: none;">点击加载更多</a>
        </div>
    </div>
</div>

<!-- 直选tips -->
<div class="beet-tips" hidden="" style="display: none;">
   
    <div class="clear"></div>
    <ul>
        <li><a class="beet-active" href="javascript:;" data-type="-1">全部类型</a></li>
        <li><a href="javascript:;" data-type="1" class="">用户充值</a></li>
        <li><a href="javascript:;" data-type="2" class="">用户提现</a></li>
        <li><a href="javascript:;" data-type="3">投注扣款</a></li>
        <li><a href="javascript:;" data-type="4" class="">追号扣款</a></li>
        <li><a href="javascript:;" data-type="5">追号返款</a></li>
        <li><a href="javascript:;" data-type="6">游戏返点</a></li>
        <li><a href="javascript:;" data-type="7">奖金派送</a></li>
        <li><a href="javascript:;" data-type="8">撤单返款</a></li>
        <li><a href="javascript:;" data-type="9">系统撤单</a></li>
        <li><a href="javascript:;" data-type="10">撤销返点</a></li>
        <li><a href="javascript:;" data-type="11">撤销派奖</a></li>
        <li><a href="javascript:;" data-type="12">充值扣费</a></li>
        <li><a href="javascript:;" data-type="13">上级充值</a></li>
        <li><a href="javascript:;" data-type="14">活动礼金</a></li>
        <li><a href="javascript:;" data-type="16">提现失败</a></li>
        <li><a href="javascript:;" data-type="17">撤销提现</a></li>
        <li><a href="javascript:;" data-type="18">满赠活动</a></li>
        <li><a href="javascript:;" data-type="19">签到有你</a></li>
        <li><a href="javascript:;" data-type="20">注册活动</a></li>
        <li><a href="javascript:;" data-type="21">充值活动</a></li>
        <li><a href="javascript:;" data-type="22">佣金大返利</a></li>
        <li><a href="javascript:;" data-type="23">幸运大转盘</a></li>
        <li><a href="javascript:;" data-type="24">系统充值</a></li>
        <li><a href="javascript:;" data-type="25">投注送礼包</a></li>
        <li><a href="javascript:;" data-type="99">其他</a></li>
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
    function initLoading() {
        $('a.on-more').hide();
        $('div.mine-message').hide();
    }
</script>

<script>
    $('div.beet-tips a').click(function () {
        $('div.beet-tips a').removeClass('beet-active');
        $('div.beet-tips').hide();
        $(this).addClass('beet-active');
        $('span#account_type').text($(this).text());
        dataType = $(this).data('type');
        initLoading();
        getAccountDetail(2);
    });
</script>

<script>
    $(function () {
        getAccountDetail();
    });
</script>

<script>
    $('div.ui-bett-refresh').click(function () {
        initLoading();
        getAccountDetail(2);
    });
</script>

<script>
    $('a.on-more').click(function () {
        getAccountDetail(1);
    });
</script>

<script>
   var pageIndex = 1;//初始第一页
   var pagesize = 20;
    
    function getAccountDetail(more) {
        if (more == 1) {
            pageIndex++;
        } else if (more == 2) {
            pageIndex = 1;
        }
        var tradeTypevalue = "";
        $('#ordertype :selected').each(function (i, selected) {
            tradeTypevalue += $(selected).val() + ",";
        });
        var st = $(".beet-active").attr("data-type");
        if (st == undefined )
            st = -1;
      
        var paramData = {
            "tradeType": st,
            "startTime": "<%=DateTime.Now.AddDays(-10)%>",
            "endTime": "<%=DateTime.Now%>",
            "tradeDateTime":"2",
            "account": "",
            "userType": "-1",
            "codeType": "-1",
            "code": "",
            "SellotteryCode":"",
            "palyRadioCode":"-1",
            "issueCode": "",
            "model": "-1",
            "pageIndex": pageIndex,
            "pageSize": pagesize,
            "action": "selectamountchanglist"
        };
        loadingShow();
        $('a.on-more').html("正在获取数据中..");
        $.ajax({
            url: "/Page/Repot/AmtChange.aspx",
            type: 'post',
            data: paramData,
            success: function (data) {
                $('ul#account_list li.loading').remove();
                loadingHide();
                loaded();

                var jsonData = JSON.parse(data);
                //清除
                var txtHtml = '';
                if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                    //分页
                    var txtHtml = '';
                    var sumwivalue = 0;
                    var sumnowinvalue = 0;
                    for (var c = 0; c < jsonData.Data.length; c++) {
                        var item = jsonData.Data[c];
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
                        sumwivalue += item.InAmt;
                        sumnowinvalue += item.OutAmt;

                        var sumUserAmt = item.UserAmt;
                        var wivalue = decimalCt(Ytg.tools.moneyFormat(item.InAmt));
                        if (wivalue <= 0) {
                            wivalue = "——";
                        }
                        else {
                            //  sumUserAmt += item.InAmt;
                            wivalue = "+" + wivalue;
                        }

                        var nowinvalue = decimalCt(Ytg.tools.moneyFormat(item.OutAmt));
                        if (nowinvalue >= 0) {
                            nowinvalue = "——";
                        }
                        else {
                            nowinvalue = nowinvalue;
                            // sumUserAmt += item.OutAmt;
                        }
                        var posName = item.PostionName == null ? "" : item.PostionName;
                        var plName = (item.PlayTypeName == null ? "" : (item.PlayTypeName + item.PlayTypeRadioName));
                        plName = posName + changePalyName(plName, '', '')

                        var relevanceNo = item.RelevanceNo;//b c
                        var opendHtm = "";
                        if (item.PlayTypeName != null)
                            opendHtm = "<td><a href=\"javascript:openDetails('" + relevanceNo + "');\">" + plName + "</a></td>";
                        else
                            opendHtm = "<td>" + plName + "</td>";
                        //

                      /*  var htm = "<tr>";//"<tr><td>" + item.SerialNo + "</td>";
                        htm += "<td>" + item.UserAccount + "</td>";
                        htm += "<td>" + getDayTime(item.OccDate) + "</td>";
                        htm += "<td>" + Ytg.common.SpecialConvert.AmtTradeType(item.TradeType) + "</td>";
                        htm += "<td>" + (item.LotteryName == null ? "" : item.LotteryName) + "</td>";
                        htm += opendHtm;//"<td>" + plName + "</td>";
                        htm += "<td>" + (item.IssueCode == null ? "" : item.IssueCode) + "</td>";
                        htm += "<td>" + modelStr + "</td>";
                        htm += "<td><span class='winSpan'>" + wivalue + "</span></td>";
                        htm += "<td style='color:#26a0da;'>" + nowinvalue + "</td>";
                        htm += "<td>" + (Ytg.tools.moneyFormat(sumUserAmt)) + "</td>";
                        htm += "</tr>";*/
                        var info = "游戏：" + (item.LotteryName == null ? "" : item.LotteryName) + "  期数：" + (item.IssueCode == null ? "" : item.IssueCode);
                        txtColor = (wivalue == "——") ? 'style="color: #00bf16;"' : '-';
                        txtHtml += '<li>'
                            + '<div class="order-list-tit">'
                                + '<span class="fr c-red" ' + txtColor + '>' + (wivalue == "——" ? nowinvalue : wivalue) + '元</span><span class="order-top-left">' + Ytg.common.SpecialConvert.AmtTradeType(item.TradeType) + '</span>'
                            + '</div>'
                            + '<div class="c-gary"><span class="fr" style="font-size: 12px;">余额 ' + (Ytg.tools.moneyFormat(sumUserAmt)) + '</span><p class="order-time" style="font-size: 12px;"> ' + info + '</p></div>'
                        + '</li>';
                    }
                    /**小结*/
                   /* var htm = "<tr><td colspan='7' style='text-align:left;padding-left:20px;'> 小结: 本页变动金额:<span style='color:red;font-weight:bold;'>&nbsp;" + decimalCt(Ytg.tools.moneyFormat((sumwivalue + sumnowinvalue))) + "</span></td>";
                    htm += "<td><span class='winSpan' style='font-weight:bold;'>+" + decimalCt(Ytg.tools.moneyFormat(sumwivalue)) + "</span></td>";
                    htm += "<td style='color:#26a0da;font-weight:bold;'>" + decimalCt(Ytg.tools.moneyFormat(sumnowinvalue)) + "</td>";
                    htm += "<td></td>";
                    htm += "</tr>";
                    $(".ltbody").append(htm);*/
                    $('div.mine-message').hide();
                    
                  
                } else {
                    $('div.mine-message').show();
                }

                if (more == 2) {//刷新
                    $('ul#account_list').html(txtHtml);
                } else {
                    $('ul#account_list').append(txtHtml);
                }
                $('a.on-more').html("点击加载更多");
                
                var tot = jsonData.Total;
                var totalPage = 1;
                if (tot > pagesize)
                    totalPage=(tot % pagesize == 0 ? (tot / pagesize) : (tot / pagesize) + 1);
               
                if (totalPage > pageIndex) {
                    $('a.on-more').show();
                } else {
                    $('a.on-more').hide();
                }
            }
        });
    }
</script>


</body></html>