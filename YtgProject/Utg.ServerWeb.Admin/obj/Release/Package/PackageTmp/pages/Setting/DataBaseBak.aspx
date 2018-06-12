<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBaseBak.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Setting.DataBaseBak" %>

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
    <form id="form2" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">数据库备份</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        文件列表&nbsp;&nbsp;&nbsp;
                        <asp:Button  ID="btnBack" runat="server" CssClass="submitbtn" OnClick="lkBack_Click" Text="开始备份" style="float:right;margin-top:-5px;width:auto;" />
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="Table1">
                                <thead>
                                    <tr>
                                        <th>备份文件</th>
                                        <th>备份时间</th>
                                        <th>操作者</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><a href='/bak/<%#Eval("FileName")%>.rar'><%#Eval("FileName") %></a></td>
                                                <td><%#Eval("OccDate") %></td>
                                                <td><%#Eval("OpenUser") %></td>
                                                <td>
                                                       <a href='/bak/<%#Eval("FileName")%>.rar'>下载</a>
                                                       &nbsp;&nbsp;&nbsp;
                                                       <asp:LinkButton ID="lkDel" runat="server" Text="删除" OnClick="lkDel_Click" CommandArgument='<%#Eval("Id") %>' OnClientClick="return onSubmitfunc();"></asp:LinkButton>
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
    function onSubmitfunc() {
        return window.confirm("确定要删除吗？");
    }
</script>