<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeSetting.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.bank.RechargeSetting" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>充值提现设置</title>
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
                <h2 class="page-header">充值提现设置</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        查询条件
                    </div>
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table  table-bordered" style="text-align: right;">
                                
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        充值提现设置列表&nbsp;&nbsp;&nbsp;
                            <input type="button" id="add" class="btn btn-info" value="增加" />
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
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Code") %></td>
                                                <td><%#Eval("NikeName") %></td>
                                                <td><%#Eval("ParentCode") %></td>
                                                <td><%#Eval("UserAmt") %></td>
                                                <td><%#Eval("TotalChongzhi") %></td>
                                                <td><%#Eval("TotalXiaofei") %></td>
                                                <td>
                                                    <%#ToUserStateString(Eval("UserType").ToString()) %>
                                                </td>
                                                <td>
                                                    <script>
                                                        document.write(7.5-<%# Eval("Rebate") %>);
                                                    </script>

                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("IsDelete")%>' ToolTip='<%# Eval("Id")%>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("Status").ToString()=="0"?false:true%>' ToolTip='<%# Eval("Id")%>' />
                                                </td>
                                                <td><%#Eval("LoginCount") %></td>
                                                <td><%#Convert.ToDateTime(Eval("LastLoginTime")).ToString("yyyy/MM/dd")%></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd")%></td>
                                                <td>
                                                    <a href="javascript:edit(<%#Eval("id") %>,'<%#Eval("NikeName") %>','<%#Eval("Code") %>');">修改</a>
                                                    <a href="javascript:recharge(<%#Eval("id") %>,'<%#Eval("NikeName") %>','<%#Eval("Code") %>');">充值</a>
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
    $("#add").click(function(){
        
        parent.openModal("新增会员", "/pages/Customer/EditUser.aspx?dt"+encodeURI(new Date()),function () {
            
            $("#btnRef")[0].click();
        },650,580);
    });

    function edit(id,nikename,code){

        parent.openModal("编辑"+(nikename!=""?nikename:code)+"信息", "/pages/Customer/EditUser.aspx?id="+id, function () {
            $("#btnRef")[0].click();
        },650,580);
    }
    /**充值*/
    function recharge(id,nikename,code){
        parent.openModal((nikename!=""?nikename:code)+" 充值金额", "/pages/Customer/UserRecharge.aspx?id="+id, function () {
            $("#btnRef")[0].click();
        },650,400);
    }
</script>
