<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Role.RoleList" %>

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
                <h2 class="page-header">角色列表</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        角色列表&nbsp;&nbsp;&nbsp;
                        <%if (actionModel.Add == true){%>  
                        <%--<asp:Button runat="server" ID="btnAdd" CssClass="btn btn-info" Text="新增" /> --%>   
                        <input type="button" id="add" class="submitbtn" value="增加"  style="float:right;margin-top:-5px;" onclick="location.href = 'AddRole.aspx'"/>
                        <%}%>
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="Table1">
                                <thead>
                                    <tr>
                                        <th>角色名称</th>
                                        <th>描述</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Name") %></td>
                                                <td><%#Eval("Descript") %></td>
                                                <td>
                                                     <%if (actionModel.Update == true){%>  
                                                       <a href="EditRole.aspx?roleId=<%#Eval("id") %>" target="main">修改</a>
                                                       &nbsp;&nbsp;&nbsp;
                                                     <%}%>

                                                     <%if (actionModel.SetPermission == true){%>  
                                                       <a href="javascript:setPermission(<%#Eval("id") %>)">设置权限</a>
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
        parent.openModal("新增角色", "/pages/Role/AddRole.aspx?dt" + encodeURI(new Date()), function () {
            $("#btnRef")[0].click();
        }, 650, 430);
    });

    function edit(id) {
        parent.openModal("编辑角色信息", "/pages/Role/EditRole.aspx?roleId=" + id, function () {
            $("#btnRef")[0].click();
        }, 650, 430);
    }

    function setPermission(id) {
        window.location = "/pages/Role/setPermission.aspx?roleId=" + id;

    }
</script>


