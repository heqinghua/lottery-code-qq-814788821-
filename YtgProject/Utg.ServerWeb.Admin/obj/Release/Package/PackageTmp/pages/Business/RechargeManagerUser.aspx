<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeManagerUser.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Business.RechargeManagerUser" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>充值记录</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">充值记录</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;margin-left:10px;">
            <tr>
                <td >充值时间：</td>
                <td colspan="5">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtBeginDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                            </td>
                            <td><span class="autoSpan">&nbsp;至&nbsp;</span></td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr>
                <td><span class="autoSpan">用户名：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">充值编号：</span></td>
                <td>
                    <asp:TextBox ID="txtSeriaNo" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">充值状态：</span></td>
                <td>
                    <asp:DropDownList ID="drpState" runat="server" CssClass="form-control autoBox" style="width:auto;">
                        <asp:ListItem Text="所有状态" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="已完成" Value="0"></asp:ListItem>
                        <asp:ListItem Text="未完成" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">充值类型：</span></td>
                <td>
                    <asp:DropDownList ID="drpType" runat="server" CssClass="form-control autoBox" style="width:auto;">
                        <asp:ListItem Text="所有状态" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="分红" Value="15"></asp:ListItem>
                        <asp:ListItem Text="系统充值" Value="24"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="8" style="text-align: center;">
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>
        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        手动充值数据列表
                        <span style="float: right; padding-right: 20px;">
                            <a href="/pages/Business/RechargeManager.aspx?menuId=10#">在线充值记录</a>&nbsp;&nbsp;
                            <a href="#">手动充值记录</a>&nbsp;&nbsp;
                        </span>
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>充值编号</th>
                                        <th>充值时间</th>
                                        <th>用户名</th>
                                        <th>充值金额</th>
                                        <th>到账金额</th>
                                        <th>充值类型</th>
                                        <th>状态</th>
                                    </tr>
                                </thead>
                                <tbody id="body-content">
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("SerialNo") %></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd HH:mm:ss") %></td>
                                                <td><%#Eval("Code") %></td>
                                                <td><%#Eval("TradeAmt") %></td>
                                                <td><%#(Eval("TradeAmt")) %></td>
                                                <td><%#Eval("TradeType") %></td>
                                                <td><%#Eval("Status").ToString()=="1"?"<span style='color:blue;'>未完成</span>":"<span style='color:red;'>已完成</span>" %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                        <div class="row" style="text-align: right; padding-right: 50px;">
                            <webdiyer:AspNetPager ID="pagerControl" runat="server" PageSize="20" OnPageChanged="pagerControl_PageChanged"></webdiyer:AspNetPager>
                        </div>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
                <!-- /.col-lg-12 -->

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
<script src="/resource/datepicker/WdatePicker.js"></script>
<link href="/resource/Pager/kkpager_orange.css" rel="stylesheet" />
<script src="/resource/Pager/kkpager.min.js"></script>
<link href="/Content/Css/subpage.css" rel="stylesheet" />
<link href="/dist/css/font.css" rel="stylesheet" />
