<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JieSuanTable.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Stat.JieSuanTable" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>结算报表</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body>
    <form id="form2" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">结算报表</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">开始时间：</span></td>
                <td>
                    <asp:TextBox ID="txtBeginDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({startDate:'%y-%M-%d 03:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
                <td><span class="autoSpan">截止时间：</span></td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({startDate:'%y-%M-%d 03:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
                <td colspan="4" style="text-align: center;">
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>
        </table>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:Literal ID="ltTitle" runat="server" Text="日结算报表"></asp:Literal>
                        <span style="float: right; padding-right: 20px;">
                            <asp:LinkButton ID="lkDay" runat="server" Text="按日统计" OnClick="lkDay_Click"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="lkMon" runat="server" Text="按月统计" OnClick="lkMon_Click"></asp:LinkButton>
                        </span>
                    </div>

                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="Table1">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:Literal ID="ltHead" runat="server" Text="统计日期"></asp:Literal></th>
                                        <th>活动总礼金</th>
                                        <th>注册礼金</th>
                                        <th>签到礼金</th>
                                        <th>代理佣金</th>
                                        <th>充值总额</th>
                                        <th>提现总额</th>
                                        <th>投注总额</th>
                                        <th>返点总额</th>
                                        <th>中奖总额</th>
                                        <th>分红总额</th>
                                        <th>盈亏总额</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("OccDay") %></td>
                                                <td><%#Eval("Activity") %></td>
                                                <td><%#Eval("QianDao") %></td>
                                                <td><%#Eval("ZhuChe") %></td>
                                                <td><%#Eval("YongJing") %></td>
                                                <td><%#Eval("Chongzhi") %></td>
                                                <td><%#Eval("tiXian") %></td>
                                                <td><%#Eval("TouZhu") %></td>
                                                <td><%#Eval("Youxifandian") %></td>
                                                <td><%#Eval("Jiangjinpaisong") %></td>
                                                <td><%#Eval("FenHong") %></td>
                                                <td ><%#Eval("YingKui") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="RepeaterMonth" runat="server" Visible="false">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("OccMonth") %></td>
                                                <td><%#Eval("Activity") %></td>
                                                <td><%#Eval("QianDao") %></td>
                                                <td><%#Eval("ZhuChe") %></td>
                                                <td><%#Eval("YongJing") %></td>
                                                <td><%#Eval("Chongzhi") %></td>
                                                <td><%#Eval("tiXian") %></td>
                                                <td><%#Eval("TouZhu") %></td>
                                                <td><%#Eval("Youxifandian") %></td>
                                                <td><%#Eval("Jiangjinpaisong") %></td>
                                                <td><%#Eval("FenHong") %></td>
                                                <td><%#Eval("YingKui") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="rptSum" runat="server" >
                                        <ItemTemplate>
                                            <tr style="color: red;">
                                                <td>合计：
                                                </td>
                                                <td><%#Eval("Activity") %></td>
                                                <td><%#Eval("QianDao") %></td>
                                                <td><%#Eval("ZhuChe") %></td>
                                                <td><%#Eval("YongJing") %></td>
                                                <td><%#Eval("Chongzhi") %></td>
                                                <td><%#Eval("tiXian") %></td>
                                                <td><%#Eval("TouZhu") %></td>
                                                <td><%#Eval("Youxifandian") %></td>
                                                <td><%#Eval("Jiangjinpaisong") %></td>
                                                <td><%#Eval("FenHong") %></td>
                                                <td><%#Eval("YingKui") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
                <!-- /.col-lg-12 -->
                <%--<asp:Button ID="btnRef" runat="server" Style="display: none;" OnClick="btnRef_Click" />--%>
            </div>
            <!--/列表-->

        </div>
    </form>
</body>
</html>

<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<link href="/dist/css/font.css" rel="stylesheet" />
<script src="/resource/datepicker/WdatePicker.js"></script>
