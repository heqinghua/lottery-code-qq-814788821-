<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qiandao.aspx.cs" Inherits="Ytg.ServerWeb.wap.activity.qiandao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport"/>
    <meta name="format-detection" content="telephone=no"/>
    <meta name="mobile-web-app-capable" content="yes"/>
    <meta name="apple-mobile-web-app-capable" content="yes"/>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport">
    <meta name="format-detection" content="telephone=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
</head>
<body style="background: rgb(243, 243, 243);">
    
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
      <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
     <script src="ContentDjp/rotate/jQueryRotate.2.2.js"  type="text/javascript"></script>
    <script src="ContentDjp/rotate/jquery.easing.min.js"  type="text/javascript"></script>
    <style type="text/css">
        .ltable td {font-size:12px;}
    </style>
      <style type="text/css">
	.ly-plate{ position:relative; width:300px;height:300px; }
	.rotate-bg{width:300px;height:300px;background:url(/views/Activity/QianDao/ContentDjp/rotate/ly-plate.png);background-size:300px 300px; position:absolute;top:0;left:0}
	.ly-plate div.lottery-star{width:54px;height:54px;position:absolute;top:75px;left:75px;/*text-indent:-999em;overflow:hidden;background:url(rotate-static.png);-webkit-transform:rotate(0deg);*/outline:none}
	.ly-plate div.lottery-star #lotteryBtn{cursor: pointer;position: absolute;top:0;left:0;*left:-107px}
        
    </style>
    <script type="text/javascript">
        var isautoRefbanner = <%=isautoRefbanner?"true":"false"%>;
        $(function () {
            $("#refff").html(Ytg.tools.moneyFormat('<%=UserAmt%>'));//old amt
            $("#actives").addClass("cur");
            $("#bottomState").remove();
        })
        var issgin = false;
        function sgin(obj){
            if (issgin)
                return;
            issgin = true;
            $(obj).html("处理中...");
            $.ajax({
                url: "/Views/Activity/QianDao/QianDao.aspx",
                type: 'post',
                data: "action=sign&ajax=ajx",
                success: function (data) {
                    issgin = false;
                    $(obj).html("我要签到");
                    if (data == "0") {
                        $.alert("恭喜您，签到成功！",1,function(){
                            window.location.reload();
                        });
                    } else if (data == "2") {
                        $.alert("投注量还未达到要求，赶紧去投注，投的越多、奖励越多！");
                    } else if (data == "3") {
                        $.alert("您今天已经签到过啦，明天再来吧！");
                    } else if (data == "5") {
                        $.alert("活动还没开始呢！");
                    } else {
                        $.alert("未知错误，请稍后再试！");
                    }
                }
            });
        }
        function refchangemonery(){
            Ytg.common.user.refreshBalance();
        }
    </script>
    <link rel="stylesheet" type="text/css" href="/Views/Activity/QianDao/Content/css/style.css"/>
 <style>
    
h2,h3,p,ul,li{ margin:0; padding:0;}
ul,li{ list-style:none;}
img{ vertical-align:middle;}
.left{ float:left;}
.right{ float:right;}
.clear{ clear:both;}
a{ color:#cd0228; text-decoration:none;}

.bannerTwo01{  height:236px; background:url(/Views/Activity/QianDao/Content/image/NewAbanner2.jpg) no-repeat; color:#fff; padding:40px 0 0 40px; font-size:14px; line-height:22px;}
.bannerTwo01 h2{ font-size:16px;}
.bannerTwo02{ height:312px; background:url(/Views/Activity/QianDao/Content/image/NewAbanner2.jpg) no-repeat -610px 0; padding:164px 0 0 0px; }
.bannerTwo02 .data{ background: url(/Views/Activity/QianDao/Content/image/data.png) no-repeat ; background-size:520px auto; height: 320px; padding-top:1px;}
.bannerTwo02 .data .signBtn{ position:relative;float:right;margin-right:20px;margin-top:6px; display:block;background:#ff6105;color:#fff;padding:13px;font-size:14px;font-weight:bold;padding-bottom:2px;padding-top:2px;cursor:pointer; }
.bannerTwo02 .data .signBtn:hover {background:#f77627;}
.bannerTwo02 .data .signPop{ background:url(/Views/Activity/QianDao/Content/image/popBox.png) no-repeat; height: 96px; width: 178px; font-size: 16px; line-height: 25px; color: #a18b00; padding:15px; position: absolute; top: -175px; right: -96px;}
.bannerTwo02 .data .signPop em{ text-decoration: underline; padding:0 5px; }
.bannerTwo02 .data .signPop .closeBtn{ height: 26px; width: 26px; font-size: 0; text-indent: -999em; position: absolute; right: 0; top: 0;}
.bannerTwo02 .data .dataForm{ margin: 30px 0 0 5px; height: 294px;  border-radius: 8px; overflow: hidden; *margin: 37px 0 0 7px;}
.bannerTwo02 .data .dataForm ul li{ float: left; font-weight: 200;width: 96px; padding-left:5px;padding-top:5px; text-align: center; position: relative;}
.bannerTwo02 .data .dataForm ul li p {text-align:center;}
.bannerTwo02 .data .dataForm ul li .mask{ display: none; background: url(/Views/Activity/QianDao/Content/image/blueMask.png) repeat; height: 100%; width: 100%; position: absolute; top: 0; left: 0;}
.bannerTwo02 .data .dataForm ul li.active .mask{ display: block;}
.bannerTwo02 .data .dataForm ul li.nor{ border-right: none;}
.bannerTwo02 .data .dataForm .jf{ background-color: #f3f3f3; color: #808080; font-size: 15px; line-height: 30px; height: 30px;}
.bannerTwo02 .data .dataForm .jfchk{ background-color: #f3f3f3; color: #ed4020; font-size: 15px; line-height: 30px; height: 30px;}
.bannerTwo02 .data .dataForm .date{ font-size: 15px; line-height: 40px; height: 36px; color: #777;}
.bannerTwo02 .data .dataForm .hint{ font-size: 12px; font-weight: 400; line-height: 20px; height: 24px; color: #777;}
p {color:#fff;text-align:left;}
.guize{ margin:0 auto 40px auto;text-align:left;}
.guize h2{ background:url(/Views/Activity/QianDao/Content/image/guize_title.jpg) no-repeat; width:278px; height:79px;}
.guize div{ background:#2ba1d9; padding:30px 25px; line-height:30px;}
 .guize span {color:#fff;font-size:16px;}
.guize div p{ color:#fffe70; font-size:16px; margin-bottom:30px;}
.guize div p b{ text-decoration:underline;font-style:italic;color:#fff;padding-left:5px;padding-right:5px;}
.guize div li{ font-size:14px; color:#ace4ff; background:url(/Views/Activity/QianDao/Content/image/list.png) no-repeat 0 8px; padding-left:20px;text-align:left;}
.guize div li b{ color:#fac800;}
b {font-size:20px;}
.btn {
    background: #bf1cb7;
    color: #fff;
    height: 30px;
    line-height: 30px;
    border: none;
    margin: 0px 10px;
    float: left;
    width: 120px;
}
.btn:hover {background: #dc51df;}
/*-- 头部 --*/
.header { background: #ec2829; width: 100%; height: 44px; position:fixed; z-index: 100; top: 0; left: 0; right: 0; }
.header #reveal-left, .header .reveal-left, .header #reveal-right { color: #fff; width: 36px; height: 36px; display: block; text-indent: -99999px; border: none; position: absolute; cursor: pointer; outline: none; }
.ui-toolbar-left, .ui-toolbar-right { position: absolute; z-index: 2 }
.ui-toolbar-right { right:10px; background: url(/wap/statics/images/goucai_qiehua_07.png) no-repeat; top: 7px; width: 65px; background-size: 65px 30px; height: 30px;}
.ui-toolbar-left {background: url(/wap/statics/images/goucai_qiehua_07.png) no-repeat;}
.ui-head { background: url(/wap/statics/images/goucai_qiehua_03.png) no-repeat; width: 65px; background-size: 65px 30px; height: 30px;}
.ui-toolbar-right a {width:32.5px; height: 30px; display: block; float: left;}
.header #reveal-left, .header .reveal-left { background: url(/wap/statics/images/blank_01.png) no-repeat; background-size: 42.5px; height: 44px; width:42.5px; height:44px; display: block;}
.header h1 { width: 100%; text-align: center; font-size: 22px; line-height: 44px; color: #fff; }
.header-logout { background: url(/wap/statics/images/logout.png) no-repeat; line-height: 44px;  width: 70px; height:25px;  background-size: 70px 25px;  position: absolute; right: 10px;  top: 8px;  z-index: 9;}
/*.bett-top-box { width:320px; position: absolute; left: 50%; margin-left: -160px;}*/
.bett-top-box { position: relative;display: flex;flex-wrap: wrap;justify-content: center;align-items: center;}
.bett-top-box div {margin: 0 5px;float: left;}
.header .ui-betting-title { position: absolute; left: 0; top: 5px; z-index: 1; width: 100%; text-align: center;}
/*.header .ui-betting-title .bett-top { margin-left: -50%; }*/
.bett-top-box  {display: inline-table;}
.header .ui-betting-title .bett-play { font-size:12px; line-height: 14px; width:12px; height: 34px; display: inline-block; padding-top: 5px; padding-right: 8px; }
.bett-attr { background: url(/wap/statics/images/betting/top_zhong_sanjiao.png) no-repeat right center; width: 12.5px; height: 8.5px; background-size:12.5px 8.5px; display: inline-block; padding-right: 5px; }
.bett-tit { border:1px solid #fff; border-radius: 5px; height: 28px; line-height: 22px; padding: 0 10px; font-size:90%; display: inline-block; margin-top: 4px !important;}
.bett-top .bett-tit {position: relative;top: -4px;}
.ui-text-right { position: absolute; right:10px; top: 0; line-height: 44px; }
.ui-text-right a { color: #fff; }
 </style>
    <script src="/wap/statics/js/jquery.min.js?ver=3.6"></script>
    <script src="/wap/statics/js/jquery-ui.min.js?ver=3.6"></script>
    <script src="/wap/statics/js/iscro-lot.js?ver=3.6"></script>
     <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">签到有你</h1>
        </div>
    </div>
    <div style="height: 44px;" class="ipad-top"></div>
    <form id="form1" runat="server">

<div style="background:#2077c4;">
<div class="bannerNew">
    <img src="/wap/activity/banner.png" style="width:100%;"/>
</div>
<div class="NewbannerTwo" >
  <div class=" bannerTwo01" style="display:none;">
      <div class="ly-plate" >
                      <div class="rotate-bg"></div>
                      <div class="lottery-star"><img src="/views/Activity/QianDao/ContentDjp/rotate/rotate-static.png" id="lotteryBtn" style="width:150px;height:150px;"></div>
       </div>
  </div>
  <div class=" bannerTwo02">
    <div class="data"><span  class="signBtn" onclick="sgin(this);">我要签到</span>
      <div class="signPop" style="display: none;"> <a class="closeBtn" href="javascript:;" title="关闭">关闭</a>
        <p style="padding-top:16px;">恭喜您 , 已领取<em class="gotJifen"></em>积分</p>
        <p style="font-size: 12px;" class="tjf">明日可领<em class="tomorrowJifen"></em>积分, 再接再励哦!</p>
      </div>
      <div class="dataForm">
        <ul>
          <%for (var i = 1; i <= MaxDay; i++)
            {%>
            <li class="1">
            <p class="<%=sgins.Contains(Convert.ToInt32(DateTime.Now.ToString("yyyyMM")+i.ToString("d2")))?"jfchk":"jf" %>"><%=DateTime.Now.Month %>月<%=i %>日</p>
          </li>
          <%} %>
        </ul>
      </div>
    </div>
   
  </div>
  <div class=" clear"></div>
</div>
<div class="guize" style="margin-top:30px;">
	<h2></h2>
    <div>
    <p>活动时间：每日07:30:00——次日凌晨02:00:00。活动期间,登录平台并投注<b>1000</b>元,即可激活签到按钮,点击签到可参与每日签到抽奖活动,可抽取1元-5元不等金额的现金奖励(100%中奖)！累计签到天数，还可以获得更多礼包！</p>
    <ul>
    	<li>累计签到达到7天，可额外领取礼包<b>&nbsp;18&nbsp;</b>元；</li>
        <li>累计签到达到14天，可额外领取礼包<b>&nbsp;58&nbsp;</b>元；</li>
        <li>累计签到达到20天，可额外领取礼包<b>&nbsp;88&nbsp;</b>元；</li>
        <li>累计签到达到28天，可额外领取礼包<b>&nbsp;188&nbsp;</b>元；</li>
    </ul>
    <span>一旦领取额外礼包，累计签到的天数重新开始累计。</span>
<div><asp:Button ID="btnME" runat="server"  CssClass="btn" Text="领 取 礼 包" style="height:45px;width:220px;font-size:16px;" OnClick="btnME_Click"/></div>
</div>
     
</div>
    <div style="height:100px;font-size:14px;background:#2077c4;" >
        <div style="float:right;">
            <div style=" margin: 0 auto;padding-top:10px;">
                
            </div>
        </div>
    </div>  
    </div></form>

    
<script type="text/javascript">
  
    var isRotate = false;
    var timeOut = function () {  //超时函数
        $("#lotteryBtn").rotate({
            angle: 0,
            duration: 10000,
            animateTo: 2160, //这里是设置请求超时后返回的角度，所以应该还是回到最原始的位置，2160是因为我要让它转6圈，就是360*6得来的
            callback: function () {
                $.alert('网络超时')
            }
        });
    };
    isRotate = false;
    $("#lotteryBtn").rotate({
        bind:
          {
              click: function () {
                  goRotate();
              }
          }
    });

    var rotateFunc = function (awards, angle, text) {  //awards:奖项，angle:奖项对应的角度
        $('#lotteryBtn').stopRotate();
        $("#lotteryBtn").rotate({
            angle: 0,
            duration: 5000 * 2,
            animateTo: angle + 360 * 10, //angle是图片上各奖项对应的角度，1440是我要让指针旋转4圈。所以最后的结束的角度就是这样子^^
            callback: function () {
                isRotate = false;
                switch (awards) {
                    case 1:
                        $.alert("1元已经入包，刷新您的余额试试！");
                        break;
                    case 2:
                        $.alert("2元已经入包，刷新您的余额试试！");
                        break;
                    case 3:
                        $.alert("牛逼，3元已经入包，刷新您的余额试试！");
                        break;
                    case 4:
                        $.alert("牛逼，4元已经入包，刷新您的余额试试！");
                        break;
                    case 5:
                        $.alert("你太牛逼了竟然抽中了5块！");
                        break;
                }
            }
        });
    };

    function goRotate() {
      
        if (isRotate)
            return;
        isRotate = true;
        $.ajax({
            url: "/Views/Activity/QianDao/QianDao.aspx",
            type: 'post',
            data: "action=ajx",
            success: function (data) {
               
                if (data == "")
                    return;
                var dar = data.toString().split(',');
                data = parseInt(dar[0]);
                var p = dar[1];
                var p1 = dar[2];
                //$("#subSpan").html(p1);
                if (data == -1) {
                 //   $("#titleParent").removeAttr("style");
                    $.alert("您今天还没签到呢,签到了再来抽吧！");
                    isRotate = false;
                    return;
                } else if (data == -2) {
                    $.alert("活动还没开始呢！");
                    isRotate = false;
                    return;
                } else if (data == -3) {
                    $.alert("您今天已经抽过啦，明天再来试试手气吧！");
                    isRotate = false;
                    return;
                }
               
                switch (parseInt(p)) {
                    case 1://160
                      
                        rotateFunc(1, generateMixed(4), '')
                        break;
                    case 2://250
                        rotateFunc(2, 40 * 11, '');//2快
                        break;
                    case 3://250
                        rotateFunc(3, 40 * 4, '');//3块
                        break;
                    case 4://250
                        rotateFunc(4, 40 * 6, '');//4快
                        break;
                    case 5://250
                        rotateFunc(5, 40 * 8.5, '');//5快
                        break;
                }
            }
        });
    }

    var chars = [200, 200, 280, 380, 480,480];

    function generateMixed(n) {
        var res = "";
        var cx = 0;
        for (var i = 0; i < n ; i++) {
            var id = Math.ceil(Math.random() * 5);
             cx=chars[id];
        }
        return cx;
    }
</script>
</body>
</html>
