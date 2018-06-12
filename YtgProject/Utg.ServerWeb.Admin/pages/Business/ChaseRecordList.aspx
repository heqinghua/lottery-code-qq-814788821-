<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaseRecordList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Business.ChaseRecordList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>追号列表</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    
<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/resource/datepicker/WdatePicker.js"></script>
<link href="/dist/css/font.css" rel="stylesheet" />
 <script src="/js/playname.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">追号列表</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">追号时间：</span></td>
                <td>
                    <table>
                        <tr>
                            <td><asp:TextBox ID="txtBegin" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                            <td>&nbsp;至&nbsp;</td>
                            <td><asp:TextBox ID="txtEnd" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td><span class="autoSpan">登陆账号：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">单号：</span></td>
                <td>
                    <asp:TextBox ID="txtNum" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">游戏名称：</span></td>
                <td>
                    <asp:DropDownList ID="drpGame" runat="server" CssClass="form-control autoBox">
                    </asp:DropDownList>
                </td>
                 <td >
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>

        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        追号数据列表&nbsp;&nbsp;&nbsp;
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>追号编号</th>
                                        <th>用户名</th>
                                        <th>追号时间</th>
                                        <th>游戏</th>
                                        <th>玩法</th>
                                        <th>开始期数</th>
                                        <th>追号期数</th>
                                        <th>完成期数</th>
                                        <th>追号内容</th>
                                        <th>模式</th>
                                        <th>总金额</th>
                                        <th>完成金额</th>
                                        <th>状态</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("CatchNumCode") %></td>
                                                <td><%#Eval("Code") %></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd HH:mm:ss") %></td>
                                                <td><%#Eval("LotteryName") %></td>
                                                <td>
                                                    <script>
                                                        document.write(changePalyName('<%#Eval("PlayTypeNumName") %>', '<%#Eval("PlayTypeRadioName") %>'));
                                                    </script>
                                                </td>
                                                <td><%#Eval("BeginIssueCode") %></td>
                                                <td><%#Eval("CatchIssue") %></td>
                                                <td><%# Eval("CompledIssue") %></td>
                                                <td>
                                                    <a href="javascript:showbetDetail(<%#Eval("Id") %>,'<%#Eval("betContent") %>',<%#Eval("BeginIssueCode") %>)">查看详情</a>
                                                 </td>
                                                <td><%#Utg.ServerWeb.Admin.pages.CommTypeHelper.GetModelStr(Eval("model"))%></td>
                                                <td><%#Eval("SumAmt")%></td>
                                                <td><%#Eval("CompledMonery")%></td>
                                                <td><%#Utg.ServerWeb.Admin.pages.CommTypeHelper.GetCatchStauts(Eval("Stauts"))%></td>
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

            </div>
            <!--/列表-->
        </div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content" style="background: #FFF;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">追号内容</h4>
                    </div>
                    <div class="modal-body">
                        <input id="txtId" type="hidden" />
                        期号：<input id="txtIssueCode" type="text" class="form-control" style="width: 98%" />
                        <br />
                        内容：<textarea id="betContent" class="form-control" rows="5" style="width: 98%"></textarea>
                    </div>
                    <div class="modal-footer">
                        <%if (actionModel.Update == true)
                          {%>
                        <button type="button" onclick="javascript:edit()" class="btn btn-default">修改</button>
                        <%}%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </form>
</html>

<script type="text/javascript">
    function showbetDetail(id, details, issueCode) {
        $("#txtId").val(id);
        $("#betContent").val(details);
        $("#txtIssueCode").val(issueCode);
        $('#myModal').modal();
    }
</script>