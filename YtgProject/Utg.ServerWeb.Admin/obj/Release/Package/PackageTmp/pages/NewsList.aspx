<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.NewsList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>新闻管理</title>
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
                <h2 class="page-header">新闻管理</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table style="width:100%;">
            <tr>
                <td><table class="filterTable" style="text-align: right;">
                <tr>
                    <td><span class="autoSpan">新闻标题：</span></td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                    </td>
                </tr>
            </table></td>
                <td style="text-align:right;padding-right:10px;"> <%if (actionModel.Add == true)
                            {%>
                        <input type="button" id="add" class="btnf" value="增加新闻" />
                        <%} %></td>
            </tr>
        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        新闻列表&nbsp;&nbsp;&nbsp;
                         
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>新闻标题</th>
                                        <th>是否弹窗</th>
                                        <th>发布时间</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Title") %></td>
                                                <td><%#Eval("IsShow").ToString()=="1"?"是":"否"%></td>
                                                <td><%#Eval("OccDate") %></td>
                                                <td>
                                                    <%if (actionModel.Update == true)
                                                      {%>
                                                    <a href="javascript:edit(<%#Eval("id") %>);">修改</a>
                                                    <%}%>
                                                    <%if (actionModel.Delete == true)
                                                      {%>
                                                    <asp:LinkButton ID="btnDel" runat="server" Text="删除" CommandArgument='<%#Eval("id") %>' OnCommand="btnDel_Command" OnClientClick="return confirmDel();"></asp:LinkButton>
                                                    <%}%>
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
