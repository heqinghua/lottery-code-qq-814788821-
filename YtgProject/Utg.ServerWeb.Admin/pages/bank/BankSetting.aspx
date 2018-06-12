<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankSetting.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.bank.BankSetting" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>公司账号设置</title>
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
                <h2 class="page-header">公司账号设置</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        公司账号列表&nbsp;&nbsp;&nbsp;
                        <%if (actionModel.Add == true){%>
                            <input type="button" id="add" class="submitbtn" value="增加"  style="float:right;margin-top:-5px;"/>
                        <%} %>
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>银行名称</th>
                                        <th>银行账号</th>
                                        <th>开户人</th>
                                        <th>开户省份</th>
                                        <th>开户支行</th>
                                        <th>状态</th>
                                        <th>是否入金账号</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server" OnItemDataBound="repList_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("BankName") %></td>
                                                <td><%#Eval("BankNo") %></td>
                                                <td><%#Eval("BankOwner") %></td>
                                                <td><%#Eval("Province") %></td>
                                                <td><%#Eval("Branch") %></td>
                                                <td><%#Eval("StatusDesc") %></td>
                                                <td><%#(bool)Eval("IsIncomingBank")==true?"<span style='color:red;'>是</span>":"否" %></td>
                                                <td>
                                                    <asp:LinkButton ID="lbSetIncomingBank" runat="server" Text="设置入金账号" CommandArgument='<%#Eval("id") %>' OnCommand="lbSetIncomingBank_Command"></asp:LinkButton>
                                                    <%if (actionModel.Update == true){%>
                                                       <a href="javascript:edit('<%#Eval("id") %>');">修改</a>
                                                    <%} %>
                                                    <%if (actionModel.Disabled == true){%>
                                                        <asp:LinkButton ID="btnDisabled" runat="server" Text="禁用" CommandArgument='<%#Eval("id") %>' Visible='<%# GetVis(Eval("StatusDesc")) %>' OnCommand="btnDisabled_Command"></asp:LinkButton>
                                                    <%} %>
                                                    <%if (actionModel.Eabled == true){%>
                                                        <asp:LinkButton ID="btnEabled" runat="server" Text="启用" CommandArgument='<%#Eval("id") %>' Visible='<%# !GetVis(Eval("StatusDesc")) %>' OnCommand="btnEabled_Command"></asp:LinkButton>
                                                    <%} %>
                                                    <%if (actionModel.Delete == true){%>
                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("id") %>' OnCommand="btnDelete_Command" OnClientClick="return confimDel();"></asp:LinkButton>
                                                    <%} %>
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
                <asp:Button ID="btnRef" runat="server" Style="display: none;" OnClick="btnRef_Click" />
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

    function confimDel() {
        return confirm("确定要删除吗？");
    }
    $("#add").click(function(){
        
        parent.openModal("增加公司账号", "/pages/bank/EditBankSetting.aspx?dt" + encodeURI(new Date()), function () {
   
            $("#btnRef")[0].click();
        },650,540);
    });

    function edit(id,nikename,code){

        parent.openModal("编辑公司账号", "/pages/bank/EditBankSetting.aspx?id=" + id, function () {
            $("#btnRef")[0].click();
        }, 650, 540);
    }
</script>
