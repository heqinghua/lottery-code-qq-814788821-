<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DnsManager.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Setting.DnsManager" %>

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
                <h2 class="page-header">域名管理</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        角色列表&nbsp;&nbsp;&nbsp;
                        <input type="button" id="add" class="submitbtn" value="增加" style="float: right; margin-top: -5px;" onclick="showAdd();" />
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="Table1">
                                <thead>
                                    <tr>
                                        <th>域名</th>
                                        <th>自主注册</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("SiteDnsUrl") %></td>
                                                <td><%#Eval("IsShowAutoRegist").ToString()=="True"?"是":"否" %></td>
                                                <td>
                                                  
                                                    <a href="javascript:update(<%#Eval("Id") %>,'<%#Eval("SiteDnsUrl") %>')" >修改</a>
                                                    &nbsp;&nbsp;&nbsp;
                                                 
                                                    <asp:LinkButton ID="lbBtn" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？');" CommandArgument='<%#Eval("id") %>' OnCommand="lbBtn_Command"></asp:LinkButton>
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
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content" style="background: #FFF;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">添加域名&nbsp;&nbsp;<label id="msg" style="color: red;"></label></h4>
                        </div>
                        <div class="modal-body">
                            <input  type="hidden" id="hidVal"/>
                            <table class="fromtable" id="Table2">
                                <tr class="odd gradeX">
                                    <td class="titleTd">自主注册：</td>
                                    <td id="Td1" class="contentTd">
                                        <select id="selAutoRegist">
                                            <option  value="0">是</option>
                                            <option  value="1">否</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">域名地址：</td>
                                    <td id="mentionCode" class="contentTd">
                                        <input id="poundage" type="text" value="" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="btnSubmit" type="button" class="btn btn-default" onclick="appendLock()">确定</button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
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
    function update(id,dns) {
        $('#myModal').modal('show');
        $("#hidVal").val(id);
        $("#poundage").val(dns);
    }
    function showAdd() {
        $('#myModal').modal('show');
    }
    var isSubmit = false;
    function appendLock() {
        var action = "add";
        var id = $("#hidVal").val();
        if (id != "") {
            action = "update";
            $("#myModalLabel").html("修改域名&nbsp;&nbsp;");
        }
        $.post("DnsManager.aspx", { "action": action, "id": id, "dns": $("#poundage").val(), "auto": $("#selAutoRegist").find("option:selected").val() }, function (data) {
            isSubmit = false;
            if (data == "0") {
                alert("保存成功！");
                window.location.reload();
            }
            else if (data == "-1") {
                alert("请填写域名地址！");
            }
        });
    }
</script>
