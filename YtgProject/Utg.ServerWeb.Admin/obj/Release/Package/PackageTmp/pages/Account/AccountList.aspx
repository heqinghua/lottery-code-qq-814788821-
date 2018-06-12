<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Manager.AccountList" %>

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

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">管理员列表</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">登陆账号：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>
        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        管理员列表&nbsp;&nbsp;&nbsp;
                        <%if(actionModel.Add==true){%>
                        <input type="button" id="add" class="submitbtn" value="增加"  style="float:right;margin-top:-5px;" onclick="location.href = 'AddAccount.aspx'"/>
                        <%}%>
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>账号</th>
                                        <th>用户名</th>
                                        <th>状态</th>
                                        <th>创建日期</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Code") %></td>
                                                <td><%#Eval("Name") %></td>
                                                <td><%#(bool)Eval("IsEnabled")==true?"启用":"禁用" %></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd HH:mm:ss")%></td>
                                                <td>
                                                    <%if (actionModel.Disabled == true)
                                                      {%>
                                                    <asp:LinkButton ID="btnDisabled" runat="server" Text="禁用" CommandArgument='<%#Eval("id") %>' Visible='<%# GetVis(Eval("IsEnabled")) %>' OnCommand="btnDisabled_Command"></asp:LinkButton>
                                                    <%} %>
                                                    <%if (actionModel.Eabled == true)
                                                      {%>
                                                    <asp:LinkButton ID="btnEabled" runat="server" Text="启用" CommandArgument='<%#Eval("id") %>' Visible='<%# !GetVis(Eval("IsEnabled")) %>' OnCommand="btnEabled_Command"></asp:LinkButton>
                                                    <%} %>
                                                    <%if (actionModel.SetRole == true)
                                                      {%>
                                                    <a href="javascript:setRole(<%#Eval("id") %>)">设置角色</a>
                                                    <%}%>  
                                                </td>
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
                <asp:Button ID="btnRef" runat="server" Style="display: none;" OnClick="btnRef_Click" />
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
<script type="text/javascript">
    $("#btnAdd").click(function () {
        parent.openModal("新增管理员", "/pages/Account/AddAccount.aspx?dt" + encodeURI(new Date()), function () {
            $("#btnRef")[0].click();
        }, 650, 430);
    });

    function edit(id) {
        parent.openModal("编辑管理员信息", "/pages/Account/EditAccount.aspx?userId=" + id, function () {
            $("#btnRef")[0].click();
        }, 650, 430);
    }

    function setRole(id) {
        parent.openModal("设置角色", "/pages/Account/SetRole.aspx?userId=" + id, function () {
            $("#btnRef")[0].click();
        }, 650, 430);
    }

</script>


