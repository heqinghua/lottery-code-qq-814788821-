<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignalAsync.aspx.cs" Inherits="Ytg.ServerWeb.Views.SignalAsync" %>

<!--[if IE 8]><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"><![endif]-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>漏号分析—>查看历史号码</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        font, div, td {
            font-size: 13px;
        }

        #title {
            width: 115px;
            text-align: center;
        }

        html {
            overflow: -moz-scrollbars-vertical;
            overflow-y: scroll;
        }
        .auto-style1 {
            width: 16px;
            height: 11px;
        }
    </style>
    <link rel="stylesheet" href="/Content/anync/line.css" type="text/css" />
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Content/anync/line.js"></script>
    <script type="text/javascript" src="/Content/Scripts/datepicker/WdatePicker.js"></script>
</head>
<body style="background: none;">
    <div id="right_01">
        <div class="right_01_01"></div>
    </div>
</body>
</html>
<script language="javascript" type="text/javascript">
    fw.onReady(function () {
        Chart.init();
        DrawLine.bind("chartsTable", "has_line");
        <%if(isThree=="k3"){%>
        DrawLine.color('#499495');
        DrawLine.add((parseInt(0) * 6 + 3 + 1), 2, 6, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(1) * 6 + 3 + 1), 2, 6, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(2) * 6 + 3 + 1), 2, 6, 0);
        DrawLine.draw(Chart.ini.default_has_line);
        <%}else if(isThree=="ssc"){%>
        DrawLine.color('#499495');
        DrawLine.add((parseInt(0) * 10 + 5 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(1) * 10 + 5 + 1), 2, 10, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(2) * 10 + 5 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(3) * 10 + 5 + 1), 2, 10, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(4) * 10 + 5 + 1), 2, 10, 0);
        DrawLine.draw(Chart.ini.default_has_line);
        <%}else if(isThree=="ssl"){%>
        DrawLine.color('#499495');
        DrawLine.add((parseInt(0) * 10 + 3 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(1) * 10 + 3 + 1), 2, 10, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(2) * 10 + 3 + 1), 2, 10, 0);
        DrawLine.draw(Chart.ini.default_has_line);
        <%}else if(isThree=="pk10"){%>

        DrawLine.color('#499495');
        DrawLine.add((parseInt(0) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(1) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(2) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(3) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(4) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(5) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(6) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(7) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(8) * 10 + 10 + 1), 2, 10, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(9) * 10 + 10 + 1), 2, 10, 0);
        <%}else {%>
        DrawLine.color('#499495');
        DrawLine.add((parseInt(0) * 11 + 5 + 1), 2, 11, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(1) * 11 + 5 + 1), 2, 11, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(2) * 11 + 5 + 1), 2, 11, 0);
        DrawLine.color('#E4A8A8');
        DrawLine.add((parseInt(3) * 11 + 5 + 1), 2, 11, 0);
        DrawLine.color('#499495');
        DrawLine.add((parseInt(4) * 11 + 5 + 1), 2, 11, 0);
        DrawLine.draw(Chart.ini.default_has_line);
        <%}%>

        if ($("#chartsTable").width() > $('body').width()) {
            $('body').width($("#chartsTable").width() + "px");
        }
        $("#container").height($("#chartsTable").height() + "px");
        $("#missedTable").width($("#chartsTable").width() + "px");
        resize();

        function resize() {
            window.onresize = func;
            function func() {
                window.location.href = window.location.href;
            }
        }
       
        function toggleMiss() {
            $('#missedTable').toggle();
        }

    });
    function subFunc() {
        if (daysBetween($("#starttime").val(), $("#endtime").val()) > 1) {
            $("#starttime").val("");
            alert("输入的时间跨度不能超过2天！");
            return false;
        }
        return true;
    }

    function daysBetween(start, end) {
        var startY = start.substring(0, start.indexOf('-'));
        var startM = start.substring(start.indexOf('-') + 1, start.lastIndexOf('-'));
        var startD = start.substring(start.lastIndexOf('-') + 1, start.length);

        var endY = end.substring(0, end.indexOf('-'));
        var endM = end.substring(end.indexOf('-') + 1, end.lastIndexOf('-'));
        var endD = end.substring(end.lastIndexOf('-') + 1, end.length);

        var val = (Date.parse(endY + '/' + endM + '/' + endD) - Date.parse(startY + '/' + startM + '/' + startD)) / 86400000;
        return Math.abs(val);
    }
</script>
<div class="search">
    <b class="b5"></b>
    <b class="b6"></b>
    <b class="b7"></b>
    <b class="b8"></b>
    <table width="100%" id="titlemessage" border="0" cellpadding="0" cellspacing="0" style="background: #94badf;">
        <tr>
            <td><b><span class="redtext"><%=Request.Params["lottery"] %>基本走势</span></b></td>
            <td>
                <a href="/Views/SignalAsync.aspx?lotteryid=<%=Request.QueryString["lotteryid"] %>&issuecount=30">最近30期</a>
                <a href="/Views/SignalAsync.aspx?lotteryid=<%=Request.QueryString["lotteryid"] %>&issuecount=50">最近50期</a>
                <a href="/Views/SignalAsync.aspx?&lotteryid=<%=Request.QueryString["lotteryid"] %>&issuecount=100">最近100期</a>
            </td>
            <td>
                <form method="get" >
                    <input  type="hidden" name="lotteryid" value="<%=Request.QueryString["lotteryid"] %>"/>
                    <input  type="hidden" name="issuecount" value="<%=Request.QueryString["issuecount"] %>"/>
                    <input  type="hidden" name="lottery" value="<%=Request.QueryString["lottery"] %>"/>
                    <input type="text" value="" name="starttime" id="starttime" class="date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 80px;">
                    至
                    <input type="text" value="" name="endtime" id="endtime" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 80px;">
                    <input type="submit" value="查询" id="showissue1" onclick="return subFunc();">
                </form>
            </td>
        </tr>

    </table>
    <b class="b8"></b>
    <b class="b7"></b>
    <b class="b6"></b>
    <b class="b5"></b>
</div>
<table height="5">
    <tr>
        <td></td>
</table>
<table align="center">
    <tr>
        <td colspan="3" style="border: 0px;">标注形式选择&nbsp;<input type="checkbox" name="checkbox2" value="checkbox" id="has_line" />
            <span><b>
                <label for="has_line">显示走势折线</label></b></span><!--&nbsp;<input type="checkbox" name="checkbox" value="checkbox" id="no_miss" onclick="toggleMiss();" />
          <span><b><label for="no_miss">不带遗漏数据</label></b></span>-->
        </td>
    </tr>
</table>
<table height="5">
    <tr>
        <td></td>
</table>
<div style="position: relative; height: 950px;" id="container">
    <table id="chartsTable" width="100%" cellpadding="0" cellspacing="0" border="0" style="position: absolute; top: 0; left: 0;">
       <%=ContentStr %>
    </table>
</div>
