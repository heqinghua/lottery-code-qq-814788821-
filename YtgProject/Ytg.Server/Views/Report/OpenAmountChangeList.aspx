<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenAmountChangeList.aspx.cs" Inherits="Ytg.ServerWeb.Views.Report.OpenAmountChangeList" %>

<%@ Register Src="~/Views/Report/AmountChangeListControl.ascx" TagPrefix="uc1" TagName="AmountChangeListControl" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <link href="/Content/images/skin/favicon.ico" rel="shortcut icon" type="images/x-icon"/>
    <link href="/Content/Css/feile/comn.css" rel="stylesheet"/>
    <link href="/Content/Css/feile/keyframes.css" rel="stylesheet"/>
    <link href="/Content/Css/feile/main.css" rel="stylesheet"/>
    <link href="/Content/Css/feile/homepg.css" rel="stylesheet"/>
    <link href="/Content/Css/base.css" rel="stylesheet" type="text/css" media="all"/>
    <link href="/Content/Css/layout.css" rel="stylesheet" type="text/css" media="all"/>
    <link href="/Content/Css/home.css" rel="stylesheet" type="text/css" media="all"/>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/jquery.sortable.js"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js" type="text/javascript"></script>


    <style type="text/css">
        .l-list table {width:100%;}
        .btn {background:#fff;border:1px solid #e1e1e1;color:#000;height:28px;line-height:24px;width:70px;padding-left:10px;}
        .btn:hover {background:#fff;color:#cd0228;border:1px solid #cd0228;}
        .selectedBtn{background:#fff;color:#cd0228;border:1px solid #cd0228;}
        .ordertypeS {width: 120px;border:1px solid #e1e1e1;padding-left:5px;padding-top:5px; height:150px;}
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding:10px;">
        <uc1:AmountChangeListControl runat="server" ID="AmountChangeListControl" />
        </div>
    </form>
</body>
</html>
