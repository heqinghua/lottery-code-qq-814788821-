<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmountChangeList.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.AmountChangeList" %>

<%@ Register Src="~/Mobile/userCenter/AmountChangeListControl.ascx" TagPrefix="uc1" TagName="AmountChangeListControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %></title>
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
    <!--消息框代码开始-->
    <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
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
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <script src="/Content/Scripts/playname.js"></script>
</head>
<body>

    <style type="text/css">
        .l-list table {
            width: 100%;
        }

            .l-list table tr td {
                height: 40px;
                line-height: 40px;
            }

        .btn {
            background: #fff;
            border: 1px solid #e1e1e1;
            color: #000;
            height: 28px;
            line-height: 24px;
            width: 70px;
            padding-left: 10px;
        }

            .btn:hover {
                background: #fff;
                color: #cd0228;
                border: 1px solid #cd0228;
            }

        .selectedBtn {
            background: #fff;
            color: #cd0228;
            border: 1px solid #cd0228;
        }

        .ordertypeS {
            width: 120px;
            height: 150px;
            border: 1px solid #e1e1e1;
            padding-left: 5px;
            padding-top: 5px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#zhangbian").addClass("title_active");
        });
    </script>
    <form runat="server" id="form1">
        <nav class="col-xs-12 title" style="position:fixed;z-index:999;left:0px;top:0px;">
         <a id="J-goback" href="/mobile/user_center.aspx" class="go-back"></a>
         账变记录</nav>
        <div  class="ctParent">
            <uc1:AmountChangeListControl runat="server" ID="AmountChangeListControl" />
        </div>
    </form>
</body>
</html>
