<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataClear.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Setting.DataClear" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>数据清理</title>
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="/resource/bsvd/dist/css/bootstrapValidator.css" />

    <script type="text/javascript" src="/resource/bsvd/vendor/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/dist/js/bootstrapValidator.js"></script>
    <link href="../../dist/css/font.css" rel="stylesheet" />
    <style type="text/css">
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
        line-height:normal;
        font-size:12px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server" method="post" data-bv-message="This value is not valid"
        data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
        data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
        data-bv-feedbackicons-validating="glyphicon glyphicon-refresh">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">数据清理</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <table class="table table-striped table-bordered table-hover" id="Table1">
                    <tr class="odd gradeX">
                        <td>会员</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbCustomer" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="mentionCode">
                            <asp:DropDownList ID="drpCode" runat="server">
                                <asp:ListItem Text="6个月未登陆" Value="0"></asp:ListItem>
                                <asp:ListItem Text="3个月未登陆" Value="1"></asp:ListItem>
                                <asp:ListItem Text="1个月未登陆" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <span>&nbsp;&nbsp;余额小于：</span>
                            <asp:TextBox ID="txtMinMoner" runat="server" Text="0.1" Style="width: 50px;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnClear" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>

                    <tr class="odd gradeX">
                        <td>投注、追号、账变</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbBettcount" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td1">
                            <asp:DropDownList ID="drpBet" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnClearBett" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>

                    <tr class="odd gradeX">
                        <td>账变</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbZhanBian" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td9">
                            <asp:DropDownList ID="drpZhangBian" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnZhanBian" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>

                    <tr class="odd gradeX">
                        <td>会员登录日志</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbCusLoginLog" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td2">
                            <asp:DropDownList ID="drpCusLog" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnCusLog" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>


                    <tr class="odd gradeX">
                        <td>开奖期数</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbIssue" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td3">
                            <asp:DropDownList ID="drpIssue" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnIssue" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>

                    <tr class="odd gradeX">
                        <td>管理员登录日志</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbManLogCount" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td4">
                            <asp:DropDownList ID="drpMaLog" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnManLog" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>

                    <tr class="odd gradeX">
                        <td>客户消息</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbCusChartCount" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td5">
                            <asp:DropDownList ID="drpChartCount" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnChartCount" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>

                     <tr class="odd gradeX">
                        <td>聊天消息</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbsockChart" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td6">
                            <asp:DropDownList ID="drpSockChart" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSockChart" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>


                    <tr class="odd gradeX">
                        <td>充值记录</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbrechangeCount" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td7">
                            <asp:DropDownList ID="drprechangeCount" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnrechangeCount" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>

                    <tr class="odd gradeX">
                        <td>提现记录</td>
                        <td>
                            <span>当前数据</span>
                            <asp:Label ID="lbTiXian" runat="server" Style="color: red;"></asp:Label>
                            <span>条</span>
                        </td>
                        <td id="Td8">
                            <asp:DropDownList ID="drpTiXian" runat="server">
                                <asp:ListItem Text="保留6个月" Value="0"></asp:ListItem>
                                <asp:ListItem Text="保留3个月" Value="1"></asp:ListItem>
                                <asp:ListItem Text="保留1个月" Value="2"></asp:ListItem>
                                <asp:ListItem Text="保留15天" Value="3"></asp:ListItem>
                                <asp:ListItem Text="保留7天" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnTiXian" runat="server" Text="清理" CssClass="submitbtn" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
