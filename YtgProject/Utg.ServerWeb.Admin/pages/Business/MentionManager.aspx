<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MentionManager.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Business.MentionManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>菜单管理</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style type="text/css">
        #body-content {
         font-size:12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">提现记录</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                 <td><span class="autoSpan">提现时间：</span></td>
                <td>
                    <table>
                        <tr>
                            <td><asp:TextBox ID="txtBeginDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                            <td>&nbsp;至&nbsp;</td>
                            <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td ><span class="autoSpan">提现状态：</span></td>
                <td >
                    <asp:DropDownList ID="drpStates" runat="server" CssClass="form-control autoBox" Style="width: auto;">
                        <asp:ListItem Text="所有状态" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="待处理" Value="0" ></asp:ListItem>
                        <asp:ListItem Text="提现成功" Value="1"></asp:ListItem>
                        <asp:ListItem Text="提现失败" Value="2"></asp:ListItem>
                        <asp:ListItem Text="用户撤销" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">用户名：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">提现编号：</span></td>
                <td>
                    <asp:TextBox ID="txtSeriaNo" runat="server" CssClass="form-control autoBox"></asp:TextBox>
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
                        提现记录数据列表
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>用户名</th>
                                        <th>提现编号</th>
                                        <th>提现银行</th>
                                        <th>开户省市</th>
                                        <th>支行名称</th>
                                        <th>开户姓名</th>
                                        <th>银行帐号</th>
                                        <th>提现金额</th>
                                        <th>手续费</th>
                                        <th>到账金额</th>
                                        <th>发起时间</th>
                                        <th>处理时间</th>
                                        <th>状态</th>
                                    </tr>
                                </thead>
                                <tbody id="body-content">
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Code") %></td>
                                                <%--<td><%#Eval("RelevanceNo")%></td>--%>
                                                <td><%#Eval("MentionCode") %></td>
                                                <td><%#Eval("BankName") %></td>
                                                <td><%#Eval("CityName") %></td>
                                                <td><%#(Eval("Branch")) %></td>
                                                <td><%#Eval("BankOwner") %></td>
                                                <td><%#Eval("BankNo")%></td>
                                                <td><%#string.Format("{0:N0}",Eval("MentionAmt"))%></td>
                                                <td><%#string.Format("{0:N0}", Eval("Poundage"))%></td>
                                                <td><%#string.Format("{0:N0}",Eval("RealAmt"))%></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd HH:mm:ss") %></td>
                                                <td><%#GetManagerTime(Eval("IsEnableDesc"),Eval("SendTime"))%></td>
                                                <td><%#Eval("IsEnableDesc")%></td>
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
