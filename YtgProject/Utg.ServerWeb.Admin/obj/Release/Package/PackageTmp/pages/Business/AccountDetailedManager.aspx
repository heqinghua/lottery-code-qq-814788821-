<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountDetailedManager.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Business.AccountDetailedManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>账变明细</title>
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
                <h2 class="page-header">账变明细</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;margin-left:10px;">
            <tr>
                <td rowspan="8">
                    
                    <asp:DropDownList name="ordertype" id="ordertype" class="form-control" size="12" multiple="multiple" runat="server" style="font-size:12px;">
                        <asp:ListItem Value="-1" Selected="True" Text="所有"></asp:ListItem>
                        <asp:ListItem Value="1" Text="用户充值"></asp:ListItem>
                        <asp:ListItem Value="2" Text="用户提现"></asp:ListItem>
                        <asp:ListItem Value="3" Text="投注扣款"></asp:ListItem>

                        <asp:ListItem Value="4" Text="追号扣款"></asp:ListItem>
                        <asp:ListItem Value="5" Text="追号返款"></asp:ListItem>
                        <asp:ListItem Value="6" Text="游戏返点"></asp:ListItem>

                        <asp:ListItem Value="7" Text="奖金派送"></asp:ListItem>
                        <asp:ListItem Value="8" Text="撤单返款"></asp:ListItem>
                        <asp:ListItem Value="9" Text="系统撤单"></asp:ListItem>

                        <asp:ListItem Value="10" Text="撤销返点"></asp:ListItem>
                        <asp:ListItem Value="11" Text="撤销派奖"></asp:ListItem>
                        <asp:ListItem Value="12" Text="充值扣费"></asp:ListItem>

                        <asp:ListItem Value="13" Text="上级充值"></asp:ListItem>
                        <asp:ListItem Value="14" Text="活动礼金"></asp:ListItem>
                        <asp:ListItem Value="15" Text="分红"></asp:ListItem>
                        <asp:ListItem Value="26" Text="分红扣费"></asp:ListItem>

                        <asp:ListItem Value="16" Text="提现失败"></asp:ListItem>
                        <asp:ListItem Value="17" Text="撤销提现"></asp:ListItem>
                        <asp:ListItem Value="18" Text="满赠活动"></asp:ListItem>

                        <asp:ListItem Value="19" Text="签到有你"></asp:ListItem>
                        <asp:ListItem Value="20" Text="注册活动"></asp:ListItem>
                        <asp:ListItem Value="21" Text="充值活动"></asp:ListItem>
                        <asp:ListItem Value="22" Text="佣金大返利"></asp:ListItem>
                        <asp:ListItem Value="23" Text="幸运大转盘"></asp:ListItem>
                        <asp:ListItem Value="24" Text="系统充值"></asp:ListItem>
                        <asp:ListItem Value="25" Text="投注送礼包"></asp:ListItem>
                        <asp:ListItem Value="99" Text="其他"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td><span class="autoSpan">账变时间：</span></td>
                <td colspan="3">
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
                
            </tr>
            <tr>
                <td><span class="autoSpan">游戏名称：</span></td>
                <td>
                    <asp:DropDownList ID="drpGame" runat="server" CssClass="form-control autoBox" AutoPostBack="True" OnSelectedIndexChanged="drpGame_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">玩法：</span></td>
                <td>
                    <asp:DropDownList ID="drpPlayType" runat="server" CssClass="form-control autoBox">
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">模式：</span></td>
                <td>
                    <asp:DropDownList ID="drpModel" runat="server" CssClass="form-control autoBox">
                        <asp:ListItem Text="全部" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="元" Value="0"></asp:ListItem>
                        <asp:ListItem Text="角" Value="1"></asp:ListItem>
                        <asp:ListItem Text="分" Value="2"></asp:ListItem>
                        <asp:ListItem Text="厘" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">单号：</span></td>
                <td>
                    <asp:TextBox ID="txtNum" runat="server" CssClass="form-control autoBox"></asp:TextBox>
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
                        账变明细数据列表&nbsp;&nbsp;&nbsp;
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>账单编号</th>
                                        <th>用户名</th>
                                        <th>时间</th>
                                        <th>游戏</th>
                                        <th>玩法</th>
                                        <th>类型</th>
                                        <th>期号</th>
                                        <th>模式</th>
                                        <th>收入</th>
                                        <th>支出</th>
                                        <th>余额</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("SerialNo") %></td>
                                                <td><%#Eval("UserAccount") %></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd HH:mm:ss") %></td>
                                                <td><%#Eval("LotteryName") %></td>
                                                <td>
                                                    <script type="text/javascript">
                                                        var posName='<%#Eval("PostionName") %>'
                                                        document.write( posName==''?changePalyName('<%#Eval("PlayTypeName") %>','<%#Eval("PlayTypeRadioName") %>'):posName);
                                                    </script>
                                                </td>
                                                <td><%#Utg.ServerWeb.Admin.pages.CommTypeHelper.GetTradeTypeStr(Eval("TradeType")) %></td>
                                                <td><%#Eval("IssueCode") %></td>
                                                <td><%#Utg.ServerWeb.Admin.pages.CommTypeHelper.GetModelStr(Eval("model"))%></td>
                                                <td style="color: red; text-align: center;">
                                                    <script type="text/ecmascript">
                                                        var appSub= <%# Eval("InAmt")%>;
                                                        if(appSub>=0){
                                                            document.write("+"+appSub);
                                                        }else{
                                                            document.write("--");
                                                        }
                                                    </script>
                                                </td>
                                                <td style="color: blue; text-align: center;">
                                                    <script type="text/ecmascript">
                                                        var subSub= <%# Eval("OutAmt")%>;
                                                        if(subSub<0){
                                                            document.write(subSub);
                                                        }else{
                                                            document.write("--");
                                                        }
                                                    </script>
                                                </td>
                                                <td><%#Eval("UserAmt")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="odd gradeX">
                                        <td colspan="8" style="text-align:right;"><span style="font-weight: bold;">本次查询条件金额变动统计： </span>
                                            <asp:Label ID="lbSum" runat="server" Text="0.0" Style="font-weight: bold; color: red;"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td style="font-weight: bold; color: red; text-align: center;">
                                            <asp:Label ID="lbApp" runat="server" Text="0.0" Style="font-weight: bold; color: red;"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: red; text-align: center;">
                                            <asp:Label ID="lbSub" runat="server" Text="0.0" Style="font-weight: bold; color: blue;"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
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

    </form>
</html>

