<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="touzhu.aspx.cs" Inherits="Ytg.ServerWeb.wap.activity.touzhu" %>

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
    
     <style type="text/css">
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

    <script type="text/javascript">
        var isautoRefbanner = <%=isautoRefbanner?"true":"false"%>;
        $(function () {
            $("#refff").html(Ytg.tools.moneyFormat('<%=UserAmt%>'));//old amt
            $("#bottomState").remove();
            $("#actives").addClass("cur");
            $("#bottomState").remove();
        })
        function refchangemonery(){
            Ytg.common.user.refreshBalance();
        }
    </script>
       <style type="text/css">
        .yongbg {background:#f66060;min-height:900px;}
        .yongbg .content_ {padding:5px;}
     
        .yongbg .content_ p {color:#fff;font-size:14px;text-align:left;line-height:28px;padding-left:10px;}
        .yongbg .content_ p span {font-size:18px;font-weight:bold;}
        .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#781c1f;margin-top:10px;}
        	*{padding:0;margin:0}
	 .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#fff;margin-top:10px;}
	.ly-plate{ position:relative; width:509px;height:509px;margin: 50px ;}
	.rotate-bg{width:509px;height:509px;background:url(Content/rotate/ly-plate.png);position:absolute;top:0;left:0}
	.ly-plate div.lottery-star{width:214px;height:214px;position:absolute;top:150px;left:147px;/*text-indent:-999em;overflow:hidden;background:url(rotate-static.png);-webkit-transform:rotate(0deg);*/outline:none}
	.ly-plate div.lottery-star #lotteryBtn{cursor: pointer;position: absolute;top:0;left:0;*left:-107px}
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
    </style>

    </head>
<body style="background: rgb(243, 243, 243);">
    
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
      <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
     <script src="ContentDjp/rotate/jQueryRotate.2.2.js"  type="text/javascript"></script>
    <script src="ContentDjp/rotate/jquery.easing.min.js"  type="text/javascript"></script>
 <form runat="server">
     <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">签到有你</h1>
        </div>
    </div>
    <div style="height: 44px;" class="ipad-top"></div>
    
    <div id="content" class="yongbg">
       <div style="background:#5b28ff;text-align:center;">
           <img src="/wap/activity/top.png"  style="width:100%;"/>
       </div>
        <table style="width:1000px;margin:auto;">
            <tr>
                <td>
                    
                </td>
                <td>
                    <div class="content_">
                        <h1 class="h1style">活动时间</h1>
                        <p>每日<span>07:30:00</span>——次日凌晨<span>02:00:00</span></p>
                        <h1 class="h1style">活动内容</h1>
                        <p>
                        乐诚网推出投注送礼包，前所未有的返利规则，为所有用户带来全新福利！
                            只要您在前一天的投注达到活动要求，即可领取相应的礼包，投的越多送的越多！
                        </p>
                        <h1 class="h1style">具体奖项</h1>
                        <p>
                            1、自身投注量达到1888，赠送礼包<span>8</span><br />
                            2、自身投注量达到18888，赠送礼包<span>68</span><br />
                            3、自身投注量达到188888，赠送礼包<span>688</span><br />
                            4、自身投注量达到888888，赠送礼包<span>2888</span><br />
                        </p>
                        <h1 class="h1style">注意事项</h1>
                        <p>
                            1、活动时间为每日07:30:00 – 次日凌晨02:00:00 每日需要自行领取前一日投注投注礼包，逾期不候。<br />
                            2、任何包号或者80%以上的刷量投注行为不计入有效投注（如：三星直选当期注单需小于800注，以此类推）。<br />
                            3、乐诚网保留对此次活动做出的更改、终止权利，并享有最终解释权。<br />
                            4、领取时间:每日07:30:00——次日凌晨02:00:00。<br />
                        </p>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height:50px;"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="content_" style="text-align:center;">
                        <asp:Button  ID="btnMe" runat="server" Text="领 取 礼 包" CssClass="btn" OnClick="btnMe_Click" style="height:45px;width:220px;font-size:16px;"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height:50px;"></td>
            </tr>
        </table>
         
    </div>
     </form>
</body>
</html>
