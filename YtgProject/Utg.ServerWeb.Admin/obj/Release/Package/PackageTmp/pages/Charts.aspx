<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Charts.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Charts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>统计图</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="/bower_components/morrisjs/morris.css" rel="stylesheet" />
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">

        <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="page-header">数据统计</h2>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row" runat="server" id="rowDay">
                <div class="col-lg-6" style="width: 100%;">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            用户天访问统计
                             <span style="float: right; padding-right: 20px;">
                                 <asp:LinkButton ID="lkDay" runat="server" Text="按天统计" OnClick="lkDay_Click"></asp:LinkButton>&nbsp;&nbsp;
                                 <asp:LinkButton ID="lkMon" runat="server" Text="按月统计" OnClick="lkMon_Click"></asp:LinkButton>&nbsp;&nbsp;
                                 <asp:LinkButton ID="lkYear" runat="server" Text="按年统计" OnClick="lkYear_Click"></asp:LinkButton>
                             </span>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="dataTable_wrapper">
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>统计日期</th>
                                            <th>会员</th>
                                            <th>代理</th>
                                            <th>总代理</th>
                                            <th>合计</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rprDay" runat="server">
                                            <ItemTemplate>
                                                <tr class="odd gradeX">
                                                    <td><%# Eval("OccDate") %></td>
                                                    <td><%# Eval("MemberCount") %></td>
                                                    <td><%# Eval("ProxyCount") %></td>
                                                    <td><%# Eval("BasicProyCount") %></td>
                                                    <td style="color: red;"><%# Eval("SumCount") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
            </div>
            <div class="row" runat="server" id="rowMonth" visible="false">
                <div class="col-lg-6" style="width: 100%;">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            用户月访问统计图
                            <span style="float: right; padding-right: 20px;">
                                 <asp:LinkButton ID="LinkButton1" runat="server" Text="按天统计" OnClick="lkDay_Click"></asp:LinkButton>&nbsp;&nbsp;
                                 <asp:LinkButton ID="LinkButton2" runat="server" Text="按月统计" OnClick="lkMon_Click"></asp:LinkButton>&nbsp;&nbsp;
                                 <asp:LinkButton ID="LinkButton3" runat="server" Text="按年统计" OnClick="lkYear_Click"></asp:LinkButton>
                             </span>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="dataTable_wrapper">
                                <table class="table table-striped table-bordered table-hover" id="Table1">
                                    <thead>
                                        <tr>
                                            <th>统计日期</th>
                                            <th>会员</th>
                                            <th>代理</th>
                                            <th>总代理</th>
                                            <th>合计</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptMonth" runat="server">
                                            <ItemTemplate>
                                                <tr class="odd gradeX">
                                                    <td><%# Eval("OccDate") %></td>
                                                    <td><%# Eval("MemberCount") %></td>
                                                    <td><%# Eval("ProxyCount") %></td>
                                                    <td><%# Eval("BasicProyCount") %></td>
                                                    <td style="color: red;"><%# Eval("SumCount") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->

                </div>
            </div>
            <!-- /.col-lg-6 -->
            <div class="row" runat="server" id="rowYear" visible="false">
                <div class="col-lg-6" style="width: 100%;">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            用户年访问统计图
                            <span style="float: right; padding-right: 20px;">
                                 <asp:LinkButton ID="LinkButton4" runat="server" Text="按天统计" OnClick="lkDay_Click"></asp:LinkButton>&nbsp;&nbsp;
                                 <asp:LinkButton ID="LinkButton5" runat="server" Text="按月统计" OnClick="lkMon_Click"></asp:LinkButton>&nbsp;&nbsp;
                                 <asp:LinkButton ID="LinkButton6" runat="server" Text="按年统计" OnClick="lkYear_Click"></asp:LinkButton>
                             </span>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="dataTable_wrapper">
                                <table class="table table-striped table-bordered table-hover" id="Table2">
                                    <thead>
                                        <tr>
                                            <th>统计日期</th>
                                            <th>会员</th>
                                            <th>代理</th>
                                            <th>总代理</th>
                                            <th>合计</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="repYear" runat="server">
                                            <ItemTemplate>
                                                <tr class="odd gradeX">
                                                    <td><%# Eval("OccDate") %></td>
                                                    <td><%# Eval("MemberCount") %></td>
                                                    <td><%# Eval("ProxyCount") %></td>
                                                    <td><%# Eval("BasicProyCount") %></td>
                                                    <td style="color: red;"><%# Eval("SumCount") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <!-- /.panel -->
            </div>
    </form>
</body>
</html>
<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/bower_components/raphael/raphael-min.js"></script>
<script src="/bower_components/morrisjs/morris.min.js"></script>
<link href="/dist/css/font.css" rel="stylesheet" />
