<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yongj.aspx.cs" Inherits="Ytg.ServerWeb.wap.activity.yongj" %>

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
        .yongbg { }
        .yongbg .content {}
        .yongbg .content ul {margin:0px;padding:5px;margin:auto;}
        .yongbg .content ul li {list-style:none;width:100%;}
        .yongbg .content ul li p {color:#ee944d;font-size:12px;text-align:left;line-height:28px;}
        .yongbg .content ul li p span {font-size:18px;font-weight:bold;}
        .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#ee791d;margin-top:10px;}
        .btn {background: #bf1cb7;color: #fff;height: 30px;line-height: 30px;border: none;margin: 0px 10px;float: left;width: 120px;margin-top:50px;}
        .btn:hover {background: #dc51df;}
        .closeButton {background: url(/content/images/skin/small_pop_bts.png) left top;width: 84px;height: 34px;border: 0;cursor: pointer;display: inline-block;color: #fff;padding: 0 12px;  margin: 0 5px;font-family: "Microsoft Yahei";font-weight: bold;font-size:14px;}
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
     <img src="/wap/activity/yongj.png" style="width:100%;"/>
    <div id="content" class="yongbg">
        <div class="content">
            <ul>
                <li >
                    <h1 class="h1style">活动时间</h1>
                    <p>每日<span>07:30:00</span>——次日凌晨<span>02:00:00</span></p>
                    <h1 class="h1style">活动内容</h1>
                    <p>
                        乐诚网推出全新佣金返利系统，前所未有的返利规则，<br />
                        为所有代理用户带来全新福利！<br />
                        只要您的团队中得下级在前一天的投注达到活动要求，<br />
                        即可为您甚至您的上级累计可领取的福利，<br />
                        达到要求的人数越多，您能累计领取的福利也更多！
                    </p>
                    <h1 class="h1style">具体规则</h1>
                    <p>
                        下级投注量达到<span>10000</span>元，上级可领取<span>50</span>元，上上级可领取<span>20</span>元。<br />
                        下级投注量达到<span>3000</span>元，上级可领取<span>25</span>元，上上级可领取<span>15</span>元。<br />
                        下级投注量达到<span>2000</span>元，上级可领取<span>20</span>元，上上级可领取<span>10</span>元。<br />
                        下级投注量达到<span>1000</span>元，上级可领取<span>10</span>元，上上级可领取<span>5</span>元。

                    </p>
                </li>
                <li >
                    <div style="">
                        <h1 class="h1style">注意事项</h1>
                        <p>
                            1、 活动时间为每日07:30:00 – 次日凌晨02:00:00<br />
                            代理需要自行领取前一日团队表现累计到得返利总额，逾期不候。<br />
                            2、任何的对冲等刷量行为不计入有效投注，任何不正常获利，<br />
                            乐诚网保留取消收回赠送礼金的权利。<br />
                            3、乐诚网保留对此次活动做出的更改、终止权利，<br />
                            并享有最终解释权。<br />
                            4、领取时间:每日07:30:00——次日凌晨02:00:00。<br />
                            5、下级的具体投注情况可以通过“统计报表”查询。
                        </p>
                        <asp:Button ID="btnME" runat="server" OnClick="btnME_Click" CssClass="btn" Text="领 取 佣 金" style="height:45px;width:220px;font-size:16px;margin-bottom:10px;"/>
                   </div>
                </li>
            </ul>
        </div>
    </div>
   </form>
    <script type="text/javascript">
        $(function () {
            var ct = '<%=Content%>'
            if (ct != "") {
                ct = "<p style='color:#000;'><span style='color:#000;font-size:12px;ling-height:30px;'> " + ct + "</span></p><p style='text-align:center;'><input type='button' value='确定' class='closeButton' style='margin-top:15px;' onclick='dialog_close();'/></p>";
                $.dialog({
                    id: 'open_content',
                    fixed: true,
                    lock: false,
                    max: false,
                    min: false,
                    width: 400,
                    height: 130,
                    title: "提示",
                    content: ct
                });
            }
        });

        function dialog_close() {
            $.dialog({ id: 'open_content' }).close();
            Ytg.common.user.refreshBalance();
        }
    </script>
</body>
</html>
