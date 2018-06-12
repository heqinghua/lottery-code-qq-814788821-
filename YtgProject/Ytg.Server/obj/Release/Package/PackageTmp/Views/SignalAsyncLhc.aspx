<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignalAsyncLhc.aspx.cs" Inherits="Ytg.ServerWeb.Views.SignalAsyncLhc" %>

<!--[if IE 8]><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"><![endif]-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>漏号分析—>查看历史号码</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        #title {
            width: 115px;
            text-align: center;
        }

        html {
            overflow: -moz-scrollbars-vertical;
            overflow-y: scroll;
        }
    </style>
    <link rel="stylesheet" href="/Content/anync/line.css" type="text/css" />
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Content/anync/line.js"></script>
    <script type="text/javascript" src="/Content/Scripts/datepicker/WdatePicker.js"></script>
</head>
<body>
    <link href="/Content/Css/liuhecai.css" rel="stylesheet" />
    <div class="six_result_content">
        <div class="six_result_tittle">
            <div class="six_t">香港六合彩开奖结果</div>
            <div class="six_tit">
                <div class="s1">期号</div>
                <div class="s2" style="margin-left: 30px">开奖日期</div>
                <div class="s3" style="margin-left: 90px">开奖号码</div>
            </div>
        </div>
        <form runat="server" id="form1">
            <div class="result_list">
                <ul>
                    <asp:Repeater ID="rep" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="s1"><%# Eval("IssueCode") %></div>
                                <div class="s2" style="margin-left: 30px"><%# Convert.ToDateTime(Eval("EndTime")).ToString("yyyy/MM/dd") %></div>
                                <div class="s3 number" style="margin-left: 90px">
                                    <%# BuilderNumHtml(Eval("Result")) %>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </form>
</body>
</html>
