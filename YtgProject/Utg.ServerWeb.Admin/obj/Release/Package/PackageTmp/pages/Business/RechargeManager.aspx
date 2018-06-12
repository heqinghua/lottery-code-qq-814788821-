<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeManager.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Business.RechargeManager" %>

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
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">充值时间：</span></td>
                <td>
                    <table>
                        <tr>
                            <td><asp:TextBox ID="txtBeginDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                            <td>&nbsp;至&nbsp;</td>
                            <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
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
                    <asp:DropDownList ID="drpState" runat="server" CssClass="form-control autoBox" Style="width: auto;">
                        <asp:ListItem Text="所有状态" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="已完成" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未完成" Value="0"></asp:ListItem>
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
                        在线充值数据列表
                        <span style="float: right; padding-right: 20px;">
                            <a href="#">在线充值记录</a>&nbsp;&nbsp;
                            <a href="/pages/Business/RechargeManagerUser.aspx">手动充值记录</a>&nbsp;&nbsp;
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
                                        <th>流水号</th>
                                        <th>到账金额</th>
                                        <th>充值银行</th>
                                        <th>状态</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody id="body-content">
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("MY18FY") %></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd HH:mm:ss") %></td>
                                                <td><%#Eval("Code") %></td>
                                                <td><%#Eval("TradeAmt") %></td>
                                                <td><%#(Eval("MY18DT")) %>-<%#(Eval("My18oid")) %></td>
                                                <td><%#Eval("MY18M") %></td>
                                                <td><%#Eval("MY18PT")%></td>
                                                <td><%#Eval("IsCompled").ToString()=="False"?"<span style='color:blue;'>未完成</span>":"<span style='color:red;'>已完成</span>" %></td>
                                                <td>
                                                    <asp:LinkButton ID="lkDel" runat="server" Text="删除" OnCommand="lkDel_Command" CommandArgument='<%# Eval("Id") %>' OnClientClick="return ondel();"></asp:LinkButton>
                                                </td>
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
<script type="text/javascript">
    function ondel(){
        return confirm("确定要删除吗？");
    }
</script>
<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/resource/datepicker/WdatePicker.js"></script>
<link href="/resource/Pager/kkpager_orange.css" rel="stylesheet" />
<script src="/resource/Pager/kkpager.min.js"></script>
<link href="/Content/Css/subpage.css" rel="stylesheet" />
<link href="/dist/css/font.css" rel="stylesheet" />
