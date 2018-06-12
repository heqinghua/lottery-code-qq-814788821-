<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankBaseSetting.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.bank.BankBaseSetting" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>银行基础设置</title>
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
                <h2 class="page-header">银行基础设置</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        银行基础列表&nbsp;&nbsp;&nbsp;
                        <%if (actionModel.Add == true){%>
                            <input type="button" id="add" class="submitbtn" value="增加"  style="float:right;margin-top:-5px;"/>
                        <%}%>
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>银行名称</th>
                                        <th>字母缩写</th>
                                        <th>启用状态</th>
                                        <th style="display:none;">Logo</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("BankName") %></td>
                                                <td><%#Eval("BankDesc") %></td>
                                                <td>
                                                    <%#Eval("IsDelete").ToString()=="False"?"正常":"禁用" %>
                                                </td>
                                                <td style="display:none;"><img src="<%#Eval("BankLogo") %>" style=" max-height:32px;"/></td>
                                                <td>
                                                    <%if (actionModel.Update == true){%>
                                                    <a href="javascript:edit('<%#Eval("id") %>');">修改</a>
                                                    <%}%>
                                                   <asp:LinkButton ID="lbDel" runat="server" Text="删除" CommandArgument='<%#Eval("id") %>' OnClick="lbDel_Click" OnClientClick="return confimDel();"></asp:LinkButton>
                                                    <a href="<%#Eval("BankWebUrl") %>" target="_blank">打开银行网站</a>
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
    $("#add").click(function () {

        parent.openModal("增加公司账号", "/pages/bank/EditBankBaseSetting.aspx?dt" + encodeURI(new Date()), function () {

            $("#btnRef")[0].click();
        }, 650, 540);
    });

    function edit(id, nikename, code) {

        parent.openModal("编辑公司账号", "/pages/bank/EditBankBaseSetting.aspx?id=" + id, function () {
            $("#btnRef")[0].click();
        }, 650, 540);
    }
</script>
