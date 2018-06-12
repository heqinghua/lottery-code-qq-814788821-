<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserBanks.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.SysUserBanks" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>用户银行卡</title>
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
                <h2 class="page-header">用户绑定银行卡</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <table class="filterTable" style="text-align: right;">
                        <tr>
                            <td><span class="autoSpan">账号：</span></td>
                            <td>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                            </td>
                            <td><span class="autoSpan">银行：</span></td>
                            <td>
                                <asp:DropDownList ID="drpBanks" runat="server" CssClass="form-control autoBox"></asp:DropDownList>
                            </td>
                            <td><span class="autoSpan">开户人：</span></td>
                            <td>
                                <asp:TextBox ID="txtOwenName" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                            </td>
                            <td><span class="autoSpan">卡号：</span></td>
                            <td>
                                <asp:TextBox ID="txtCardNum" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        绑定银行卡列表&nbsp;&nbsp;&nbsp;
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>登录账号</th>
                                        <th>开户银行</th>
                                        <th>开户支行</th>
                                        <th>开户省份</th>
                                        <th>开户人</th>
                                        <th>银行账号</th>
                                        <th>锁定次数</th>
                                        <th>是否锁定</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Code") %></td>
                                                <td><%#Eval("BankName")%></td>
                                                <td><%#Eval("Branch") %></td>
                                                <td><%#Eval("ProvinceName") %>-<%#Eval("CityName") %></td>
                                                <td><%#Eval("BankOwner") %></td>
                                                <td><%#Eval("BankNo") %></td>
                                                <td><%#Eval("UserLockCount") %></td>
                                                <td><%#Eval("IsLockCards").ToString()=="False"?"正常":"锁定" %></td>
                                               <td>
                                                   <asp:LinkButton ID="lbLock" runat="server" Text="解锁" OnCommand="lbLock_Command" CommandArgument='<%# Eval("UserId") %>' OnClientClick="return confirm('确定要解锁吗？')"></asp:LinkButton>&nbsp;&nbsp;
                                                   <asp:LinkButton ID="lbUnLock" runat="server" Text="解绑" OnCommand="lbUnLock_Command" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('确定要解绑吗？')"></asp:LinkButton>
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
                <asp:Button ID="btnRef" runat="server" Style="display: none;" OnClick="btnFilter_Click" />
            </div>
            <!--/列表-->
        </div>

    </form>
</html>

<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    $("#add").click(function () {
        window.location = "/pages/NewsEdit.aspx?dt" + encodeURI(new Date());
    });

    function edit(id, nikename, code) {
        window.location = "/pages/NewsEdit.aspx?id=" + id;
    }
    function confirmDel() {
        return window.confirm("确定要删除吗？");
    }
</script>
