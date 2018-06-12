<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserGroup.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.UserGroup"  %>
<html>
    <head>
        <title>团队余额</title>
<meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="/Content/Css/feile/comn.css" rel="stylesheet" />
    <link href="/Content/Css/feile/keyframes.css" rel="stylesheet" />
    <link href="/Content/Css/feile/main.css" rel="stylesheet" />
    <link href="/Content/Css/feile/homepg.css" rel="stylesheet" />
    <link href="/Content/Css/base.css" rel="stylesheet" type="text/css" media="all" />
    <link href="/Content/Css/layout.css" rel="stylesheet" type="text/css" media="all" />
    <link href="/Content/Css/home.css" rel="stylesheet" type="text/css" media="all" />
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="/Content/Scripts/jquery.sortable.js"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
     <!--头部结束-->
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
    <link href="/Mobile/css/subpage1.css" rel="stylesheet" />
     <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Mobile/css/style1.css" rel="stylesheet" />
      <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <!--消息框代码开始-->
    <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.blue.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.plastic.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.round.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.round.plastic.css" rel="stylesheet" />
    <script src="/Content/Scripts/jslider/jshashtable-2.1_src.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/tmpl.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.dependClass-0.1.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/draggable-0.1.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.slider.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#groupUser_p").addClass("title_active");

            //onkeyup:根据用户输入的资金做检测并自动转换中文大写金额(用于充值和提现)
            //obj:检测对象元素，chineseid:要显示中文大小写金额的ID，maxnum：最大能输入金额
            $("#chineseMoney").html(changeMoneyToChinese(<%=Monery.Replace(",","") %>));
        })
        $(function () {
            Ytg.common.loading();
            var cldt = setInterval(function () {
                Ytg.common.cloading();
                clearInterval(cldt);
            }, 1000)
        })
    </script>
</head>
<body>
    <nav class="col-xs-12 title" style="position:fixed;z-index:999;left:0px;top:0px;">
         <a id="J-goback" href="/mobile/Proxy.aspx" class="go-back">返回</a>
         团队余额</nav>
    <div class="ctParent">
    <div class="list-div" id="listDiv" style="margin:5px;">
        <table class="formTable"  id="list-table" align="center"  >
            <tbody>
                <tr>
                    <td width="150" align="right">帐号:</td>
                    <td width="200"><%=Code %></td>
                </tr>
                <tr>
                    <td width="150" align="right">昵称:</td>
                    <td><%=NockName %></td>
                </tr>
                <tr>
                    <td width="150" align="right">团队余额:</td>
                    <td colspan="3" style="color:red;"><%=Monery %>&nbsp;&nbsp;元</td>
                </tr>
                <tr>
                    <td width="150" align="right">团队余额(大写):</td>
                    <td colspan="3" style="color:red;"><span id="chineseMoney"></span></td>
                </tr>
            </tbody>
        </table>
    </div>
        </div>
</body>
</html>