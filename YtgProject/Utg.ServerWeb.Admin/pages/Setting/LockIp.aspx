<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockIp.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Setting.LockIp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>锁定IP</title>
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
                <h2 class="page-header">锁定IP</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        锁定IP列表
                        <input type="button" id="add" class="submitbtn" value="增加"  style="float:right;margin-top:-5px;" onclick="addLock();"/>    
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="Table1">
                                <thead>
                                    <tr>
                                        <th>IP</th>
                                        <th>位置</th>
                                        <th>锁定原因</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Ip") %></td>
                                                <td><%#Eval("IpCityName") %></td>
                                                <td><%#Eval("LockReason") %></td>
                                                <td>
                                                    <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%#Eval("Id") %>' Text="删除" OnCommand="btnDel_Command" OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
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
                    <div class="modal-content" style="background:#FFF;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">锁定IP&nbsp;&nbsp;<label id="msg" style="color:red;"></label></h4>
                        </div>
                        <div class="modal-body">
                            <table class="fromtable" id="Table2">
                                <tr class="odd gradeX">
                                   <td class="titleTd">IP地址：</td>
                                    <td id="mentionCode" class="contentTd" >
                                        <input  id="poundage" type="text" value="" />
                                    </td>
                                </tr>
                                <tr class="odd gradeX">
                                   <td class="titleTd">锁定原因：</td>
                                   <td  class="contentTd">
                                       <textarea id="des" rows="3" cols="45"></textarea>
                                   </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="btnSubmit" type="button" class="btn btn-default" onclick="appendLock(this)">确定</button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

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
    var isSubmit = true;
    function addLock() {
        $('#myModal').modal('show');
    }

    function appendLock() {
        if (confirm("确认锁定吗?")) {
            $.post("LockIp.aspx", { "action": "lock", "ip": $("#poundage").val(), "des": $("#des").val() }, function (data) {
                isSubmit = false;
                if (data == "0") {
                    alert("IP锁定成功！");
                    window.location.reload(true);
                }
                else if (data == "-1") {
                    alert("请填写需锁定的IP地址！");
                }
            });
        }
    }
</script>