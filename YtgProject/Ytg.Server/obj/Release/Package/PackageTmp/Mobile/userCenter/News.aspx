<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.News" %>

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
        <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>

    <script type="text/javascript">
        function showNewDetails(title, id) {
         
            $.dialog({
                id: 'open_news',
                fixed: true,
                lock: false,
                max: false,
                min: false,
                width: 600,
                height: 310,
                title: title,
                content: 'url:/views/NewsDetails.aspx?id=' + id
            });
        }
    </script>
    <style>
        .ui_content .ui_state_full {
        margin:10px;
        }
    </style>
</head>
<body>
    <nav class="col-xs-12 title" style="position: fixed; z-index: 999; left: 0px; top: 0px;">
        <a id="J-goback" href="/mobile/user_center.aspx" class="go-back">返回</a>
        系统公告
    </nav>
    <div class="ctParent">
        <div>
            <table class="table table-bordered">
							<tbody>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate><tr>
                                        <td class="w100"><span><%# Convert.ToDateTime(Eval("OccDate")).ToString("yyyy-MM-dd")%></span></td>
                                        <td><a  href="javascript:showNewDetails('<%# Eval("Title")%>',<%# Eval("id")%>)" title="点击查看">[公告]<%# Eval("Title")%></a></td></tr>
                                    </ItemTemplate>
                                </asp:Repeater>
							
					</tbody></table>
        </div>
    </div>
</body>
</html>
