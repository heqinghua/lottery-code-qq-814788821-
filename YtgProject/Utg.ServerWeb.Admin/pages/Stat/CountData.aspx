<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountData.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Stat.CountData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>统计概况</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="/bower_components/morrisjs/morris.css" rel="stylesheet" />
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style type="text/css">
        .radio_stat {
        }

            .radio_stat li {
                list-style: none;
                width: 33%;
                float: left;
                height: 30px;
                line-height: 30px;
            }

                .radio_stat li .title {
                    width: 55%;
                    font-weight: normal;
                    text-align: right;
                }

                .radio_stat li .sub {
                    color: red;
                    font-weight: normal;
                }

            .radio_stat .otherLi {
                height: 80px;
                width: 12.5%;
            }

                .radio_stat .otherLi .toolTip {
                    font-weight: bold;
                }

                .radio_stat .otherLi div {
                    text-align: center;
                }

        #dataTables-content {
        }
        <% if(!actionModel.Add){%>
        .hiderow{display:none;}
        <%} %>
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="page-wrapper">
            <div class="row" style="padding-left: 20px;">
                <br />
                <br />
                您好，&nbsp;尊敬的&nbsp;<asp:Literal ID="ltLoginCode" runat="server"></asp:Literal><span style="color: red;">【<asp:Literal ID="ltRole" runat="server"></asp:Literal>】</span>
                <br />
                <div>
                    <ul class="radio_stat">
                        <li class="otherLi" style="height: 50px;width:auto;margin-left:20px;">
                            <div class="toolTip">本次登录IP：<asp:Label ID="lbIp" runat="server"></asp:Label></div>
                        </li>
                        <li class="otherLi" style="height: 50px;width:auto;margin-left:20px;">
                            <div class="toolTip">上次登录IP：<asp:Label ID="lbPreIp" runat="server"></asp:Label></div>
                        </li>
                        <li class="otherLi" style="height: 50px;width:auto;margin-left:20px;">
                            <div class="toolTip">上次登录时间：<asp:Label ID="lbPreLoginTime" runat="server"></asp:Label></div>
                        </li>
                    </ul>
                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row hiderow">
            <div class="col-lg-6" style="width: 100%;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        盈亏统计
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="Table1">
                                <thead>
                                    <tr>
                                        <th>时间日期</th>
                                        <th>活动礼金</th>
                                        <th>注册礼金</th>
                                        <th>签到礼金</th>
                                        <th>佣金礼金</th>
                                        <th>充值总额</th>
                                        <th>提现总额</th>
                                        <th>投注总额</th>
                                        <th>返点总额</th>
                                        <th>中奖总额</th>
                                        <th>分红总额</th>
                                        <th>盈亏总额</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repListYingKui" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("OccDay") %></td>
                                                <td><%#Eval("Activity") %></td>
                                                <td><%#Eval("QianDao") %></td>
                                                <td><%#Eval("ZhuChe") %></td>
                                                <td><%#Eval("YongJing") %></td>
                                                <td><%#Eval("Chongzhi") %></td>
                                                <td><%#Eval("tiXian") %></td>
                                                <td><%#Eval("TouZhu") %></td>
                                                <td><%#Eval("Youxifandian") %></td>
                                                <td><%#Eval("Jiangjinpaisong") %></td>
                                                <td><%#Eval("FenHong") %></td>
                                                <td style="color: red;"><%#Eval("YingKui") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
            </div>
        </div>
        <!-- /.row -->
        <div class="row hiderow" style="display: none;">
            <div class="col-lg-6" style="width: 100%;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        盈亏统计
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-content">
                                <thead>
                                    <tr id="dataTables-content-head">
                                        <th style="width: 80px;">统计日期</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="dataTables-content-body-tz">
                                        <td>投注额</td>
                                    </tr>
                                    <tr id="dataTables-content-body-yk">
                                        <td>总盈亏</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
            </div>
        </div>
        <!---->

        <div class="row hiderow">
            <div class="col-lg-6" style="width: 100%;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        用户统计
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <ul class="radio_stat">
                            <li class="otherLi">
                                <div class="toolTip">今日注册</div>
                                <div><%=mUserData.DayRegisterNum %></div>
                            </li>
                            <li class="otherLi">
                                <div class="toolTip">本月注册</div>
                                <div><%=mUserData.MonRegisterNum %></div>
                            </li>
                            <li class="otherLi">
                                <div class="toolTip">上月注册</div>
                                <div><%=mUserData.PreRegisterNum %></div>
                            </li>
                            <li class="otherLi">
                                <div class="toolTip">会员总数</div>
                                <div><%=mUserData.MemberNum %></div>
                            </li>
                            <li class="otherLi">
                                <div class="toolTip">代理总数</div>
                                <div><%=mUserData.ProxyNum %></div>
                            </li>
                            <li class="otherLi">
                                <div class="toolTip">当前在线</div>
                                <div><%=mUserData.OnLineNum %></div>
                            </li>
                            <li class="otherLi">
                                <div class="toolTip">用户总数</div>
                                <div><%=mUserData.UserCount %></div>
                            </li>
                            <li class="otherLi">
                                <div class="toolTip">总余额</div>
                                <div><%=mUserData.RemainingBalance %></div>
                            </li>
                        </ul>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
            </div>
        </div>
      
        <div class="row hiderow">
            <div class="col-lg-6" style="width: 100%;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        彩种投注金额统计（彩种名称：投注金额）
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <ul class="radio_stat">
                            <asp:Repeater ID="rptlotteryType" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <label class="title"><%#Eval("LotteryName") %>：</label><label class="sub">￥<%#Eval("BetMoney") %> </label>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
            </div>
        </div>


       <%-- <asp:Repeater runat="server" ID="rptList" OnItemDataBound="rptList_ItemDataBound">
            <ItemTemplate>
                <div class="row">
                    <div class="col-lg-6" style="width: 100%;">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <%#Eval("LotteryName") %>
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <ul class="radio_stat">
                                    <asp:Repeater ID="rptList1" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <label class="title">
                                                    <%# ((string)Eval("PlayTypeNumName")).Trim()== "" ? "特码"+(string)Eval("RadioName") :((string)Eval("PlayTypeNumName")+"-"+(string)Eval("RadioName"))  %>：</label><label class="sub">￥<%# string.Format("{0:N}",Eval("BetMoney"))%> / <%#Eval("BetNum") %>注</label></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                        <!-- /.panel -->
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>



        <%--<div class="row">
                <div class="col-lg-6" style="width: 100%;">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            玩法统计（玩法名称：投注金额/注数）
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <ul class="radio_stat">
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <li><label class="title"><%#Eval("PlayTypeNumName") %>-<%#Eval("RadioName") %>：</label><label class="sub">￥<%# string.Format("{0:N}",Eval("BetMoney"))%> / <%#Eval("BetNum") %>注</label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
            </div>--%>
    </form>
</body>
</html>
<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/resource/datepicker/WdatePicker.js"></script>
<script src="/bower_components/raphael/raphael-min.js"></script>
<script src="/bower_components/morrisjs/morris.min.js"></script>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    // var jsonData=<%=mStatDataStr%>;
    /* $(function(){
         
         for(var i=0;i<jsonData.data.length;i++){
             var item=jsonData.data[i];
             $("#dataTables-content-head").append("<th>"+item.period+"</th>")
             $("#dataTables-content-body-tz").append("<td>"+item.tz+"</td>")
             $("#dataTables-content-body-yk").append("<td>"+item.yk+"</td>")
         }
     });*/
</script>
