<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LotteryStat.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Stat.LotteryStat" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>彩种统计</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>
    <style type="text/css">
        .radio_stat {
        }

            .radio_stat li {
                list-style: none;
                width: 33%;
                float: left;
                height: 30px;
                line-height: 30px;
            }

                .radio_stat li .title {
                    width: 55%;
                    font-weight: normal;
                    text-align: right;
                }

                .radio_stat li .sub {
                    color: red;
                    font-weight: normal;
                }

            .radio_stat .otherLi {
                height: 80px;
                width: 12.5%;
            }

                .radio_stat .otherLi .toolTip {
                    font-weight: bold;
                }

                .radio_stat .otherLi div {
                    text-align: center;
                }

        #dataTables-content {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">彩种统计</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        查询条件
                    </div>
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <%
                                string mLotteryId = "";
                                string mLotteryName = "";
                                for (int i = 0; i < lotteryTypeList.Count; i++)
                                {
                                    mLotteryId = lotteryTypeList[i].LotteryCode;
                                    mLotteryName = lotteryTypeList[i].LotteryName;
                            %>
                            <a href="/pages/Stat/LotteryStat.aspx?lotteryId=<%=mLotteryId %>" class="<%=SetActive(mLotteryId) %>" target="main"><%=mLotteryName %></a>
                            <% }
                            %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">开始时间：</span></td>
                <td>
                    <asp:TextBox ID="txtBeginDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
                <td><span class="autoSpan">截止时间：</span></td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
                <td colspan="4" style="text-align: center;">
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>
        </table>
        <div class="row">
            <div class="col-lg-6" style="width: 100%;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        统计列表
                    </div>
                    <div class="panel-body">
                        <ul class="radio_stat">
                            <asp:Repeater ID="rptList1" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <label class="title">
                                            <%# ((string)Eval("PlayTypeNumName")).Trim()== "" ? "特码"+(string)Eval("RadioName") :((string)Eval("PlayTypeNumName")+"-"+(string)Eval("RadioName"))  %>：</label><label class="sub">￥<%# string.Format("{0:N}",Eval("BetMoney"))%> / <%#Eval("BetNum") %>注</label></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="txtLotteryId" runat="server" />
    </form>
</body>
</html>


<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/resource/datepicker/WdatePicker.js"></script>
<link href="/Content/Css/subpage.css" rel="stylesheet" />
<link href="/dist/css/font.css" rel="stylesheet" />
