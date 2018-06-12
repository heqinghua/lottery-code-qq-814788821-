<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserQuotasFilter.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Customer.UserQuotasFilter" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>配额查询</title>
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
                <h2 class="page-header">配额查询</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>

                <td><span class="autoSpan">登陆账号：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">用户类型：</span></td>
                <td>
                    <asp:DropDownList ID="drpuserType" runat="server" CssClass="form-control autoBox" style="width:auto;">
                        <asp:ListItem Value="-1">--全部--</asp:ListItem>
                        <asp:ListItem Value="0">普通会员</asp:ListItem>
                        <asp:ListItem Value="1">代理</asp:ListItem>
                        <asp:ListItem Value="3">总代理</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">奖金类型：</span></td>
                <td>
                    <asp:DropDownList ID="drpPlayType" runat="server" CssClass="form-control autoBox" style="width:auto;">
                        <asp:ListItem Value="-1">--全部--</asp:ListItem>
                        <asp:ListItem Value="0">1800</asp:ListItem>
                        <asp:ListItem Value="1">1700</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">配额区间：</span></td>
                <td colspan="6">
                    <table>
                        <tr>
                            <td >
                                <asp:DropDownList ID="drpuserState" runat="server" CssClass="form-control autoBox" style="width:auto;">
                                    <asp:ListItem Value="-1">--全部--</asp:ListItem>
                                    <asp:ListItem Value="7.7">7.7</asp:ListItem>
                                    <asp:ListItem Value="7.6">7.6</asp:ListItem>
                                    <asp:ListItem Value="7.5">7.5</asp:ListItem>
                                    <asp:ListItem Value="7.4">7.4</asp:ListItem>
                                    <asp:ListItem Value="7.3">7.3</asp:ListItem>
                                    <asp:ListItem Value="7.2">7.2</asp:ListItem>
                                    <asp:ListItem Value="7.1">7.1</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;&nbsp;剩余配额：</td>
                            <td style="width: 80px; text-align: center;">
                                <asp:TextBox ID="txtBegin" runat="server" CssClass="form-control autoBox" Width="75" Style="padding-left: 5px;"></asp:TextBox>
                            </td>
                            <td style="text-align: center;">&nbsp;至&nbsp;
                            </td>
                            <td style="width: 80px; text-align: center;">
                                <asp:TextBox ID="txtEnd" runat="server" CssClass="form-control autoBox" Width="80"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                </td>
                <td colspan="8" style="text-align: center;">
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>
        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        配额列表&nbsp;&nbsp;&nbsp;
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>登录名</th>
                                        <th>返点</th>
                                        <th>用户类型</th>
                                        <th>奖金类型</th>
                                        <th>8.0</th>
                                        <th>7.9</th>
                                        <th>7.8</th>
                                        <th>7.7</th>
                                        <th>7.6</th>
                                        <th>7.5</th>
                                        <th>7.4</th>
                                        <th>7.3</th>
                                        <th>7.2</th>
                                        <th>7.1</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("Code") %></td>
                                                <td>
                                                    <script>
                                                        if(<%# Eval("PlayType")%>==0)
                                                            document.write((8.0-<%# Eval("Rebate") %>).toFixed(1));
                                                        else
                                                            document.write((13-<%# Eval("Rebate") %>).toFixed(1));
                                                    </script>

                                                </td>
                                                <td><%# ToUserStateString(Eval("UserType").ToString()) %></td>
                                                <td><%#Eval("PlayType").ToString()=="0"?"1800":"1700" %></td>
                                                <td><%#Eval("_80") %></td>
                                                <td><%#Eval("_79") %></td>
                                                <td><%#Eval("_78") %></td>
                                                <td><%#Eval("_77") %></td>
                                                <td><%#Eval("_76") %></td>
                                                <td><%#Eval("_75") %></td>
                                                <td><%#Eval("_74") %></td>
                                                <td><%#Eval("_73") %></td>
                                                <td><%#Eval("_72") %></td>
                                                <td><%#Eval("_71") %></td>
                                                <td>
                                                    <a href="javascript:edit(<%#Eval("SysUserid") %>);">分配配额</a>
                                                    
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
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    function edit(id){

        parent.openModal("分配配额", "/pages/Customer/EditQuotas.aspx?id="+id, function () {
            location.reload();
            //$("#btnRef")[0].click();
        },600,500);
    }
</script>
