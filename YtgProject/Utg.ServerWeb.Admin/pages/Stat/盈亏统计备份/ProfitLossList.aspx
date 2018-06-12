<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfitLossList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Stat.ProfitLossList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>盈亏统计</title>
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
                <h2 class="page-header">盈亏统计报表</h2>
            </div>
            <!-- /.col-lg-12 -->
            <asp:HiddenField ID="hidOldUser" runat="server" Value="" />
            <asp:HiddenField ID="hidUserId" runat="server" Value="-1" />
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">开始时间：</span></td>
                <td>
                    <asp:TextBox ID="txtBegin" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
                <td><span class="autoSpan">结束时间：</span></td>
                <td>
                    <asp:TextBox ID="txtEnd" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                </td>
                <td><span class="autoSpan">登录账号：</span></td>
                <td colspan="2">
                    <asp:TextBox ID="txtUserCode" MaxLength="15" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
               <td  >
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            var sumChongzhi=0;
            var sumChongzhikoufei=0;
            var sumtx=0;
            var sumhd=0;
            var sumfenhong=0;
            var sumTouZhu=0;
            var sumYouxifandian=0;
            var sumJiangjinpaisong=0;
            var sumYk=0;
        </script>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        盈亏统计数据列表&nbsp;&nbsp;&nbsp;
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>登录名</th>
                                        <th>返点</th>
                                        <th>在线充值</th>
                                        <th>上级充值</th>
                                        <th>提现总额</th>
                                        <th>活动礼金</th>
                                        <th>分红</th>
                                        <th>投注总额</th>
                                        <th>游戏返点</th>
                                        <%--<th>撤单返现</th>--%>
                                        <th>中奖总额</th>
                                        <th>总盈亏</th>
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
                                                            document.write((12.7-<%# Eval("Rebate") %>).toFixed(1));
                                                    </script>
                                                </td>
                                                <td>
                                                    <script>
                                                        var cz=<%#Eval("Chongzhi")??" 0.0000"%>
                                                        sumChongzhi+=cz;
                                                        document.write(cz.toFixed(2));
                                                    </script>
                                                </td>
                                                <td> 
                                                    <script>
                                                        var fx =  <%#GetShangJiChongZhi(Eval("ShangjiChongzhi")??"0",Eval("Chongzhikoufei")??"0")%>;
                                                            sumChongzhikoufei+=fx;
                                                        document.write(fx.toFixed(2));
                                                    </script>
                                                </td>
                                                <td>
                                                    <script>
                                                        var tx=Math.abs(<%# Eval("Tixian")??"0.0000" %>);
                                                        sumtx+=tx;
                                                        document.write(tx.toFixed(2));
                                                    </script>
                                                    

                                                </td>
                                                <td>
                                                    <script>
                                                        var sx=<%#Eval("Huodonglijin")??"0"%>+<%#Eval("ManZheng")??"0"%>+<%#Eval("QianDao")??"0"%>+<%#Eval("ZhuChe")??"0"%>+<%#Eval("ChongZhiActivity")??"0"%>+<%#Eval("YongJing")??"0"%>+<%#Eval("XinYunDaZhuanPan")??"0"%>
                                                        sumhd+=sx;
                                                       document.write(sx.toFixed(2));
                                                    </script>
                                                </td>
                                                <td>
                                                    <script>
                                                        var fh=<%#Eval("FenHong")??"0.0000"%>
                                                            sumfenhong+=fh;
                                                        document.write(fh.toFixed(2));
                                                    </script>
                                                </td>
                                                <td>
                                                    <script>
                                                        var irs=<%#Eval("Touzhu")??"0"%>+<%#Eval("Zhuihaokoukuan")??"0"%>+<%#Eval("ZhuiHaoFanKuan")??"0" %>+<%#Eval("ChedanFanKuan")??"0" %>;
                                                        irs=Math.abs(irs);
                                                        sumTouZhu+=irs;
                                                        document.write(irs.toFixed(2));
                                                    </script>
                                                </td>
                                                <td><script>
                                                        var Youxifandian=<%#Eval("Youxifandian")??"0.0000" %>+<%#Eval("Chexiaofandian")??"0.0000" %>;
                                                        sumYouxifandian+=Youxifandian;
                                                            document.write(Youxifandian.toFixed(2));
                                                        </script>
                                                </td>
                                                <%--<td><%# (decimal)Eval("ZhuiHaoFanKuan")+(decimal)Eval("ChedanFanKuan") %></td>--%>
                                                <td>
                                                    <script>
                                                        var Jiangjinpaisong=<%# Eval("Jiangjinpaisong")??"0.0000" %>+<%# Eval("Chexiaopaijiang")??"0.0000" %>;
                                                        sumJiangjinpaisong+=Jiangjinpaisong;
                                                        document.write(Jiangjinpaisong.toFixed(2));
                                                    </script>
                                                </td>
                                                <td>
                                                    <script>
                                                        var vx=<%# Eval("Jiangjinpaisong")??"0" %>+<%#Eval("Touzhu")??"0"%>+<%#Eval("Zhuihaokoukuan")??"0"%>+<%#Eval("Youxifandian")??"0"%>+<%#Eval("ZhuiHaoFanKuan")??"0"%>+<%#Eval("ChedanFanKuan")??"0"%>+<%#Eval("Chexiaofandian")??"0.0000" %>+<%# Eval("Chexiaopaijiang")??"0.0000" %>;//+<%#Eval("FenHong")??"0" %>;
                                                        var activity=<%#Eval("Huodonglijin")??"0"%>+<%#Eval("ManZheng")??"0"%>+<%#Eval("QianDao")??"0"%>+<%#Eval("ZhuChe")??"0"%>+<%#Eval("ChongZhiActivity")??"0"%>+<%#Eval("YongJing")??"0"%>+<%#Eval("XinYunDaZhuanPan")??"0"%>
                                                        var yk=vx;//(vx+activity);
                                                        if(yk<0)
                                                            yk=Math.abs(yk);
                                                        else
                                                            yk=0-yk;
                                                        sumYk+=yk;
                                                       document.write(yk.toFixed(2));
                                                    </script>
                                                </td>
                                                <td>
                                                    <%if (actionModel.Detail == true)
                                                      {%> <a href="javascript:showdetails('<%#Eval("Code") %>');">查看明细</a>&nbsp;&nbsp;<%}%>
                                                    <%if (actionModel.Subordinate == true)
                                                      {%>
                                                    <asp:LinkButton ID="children" runat="server" Text="查看下级" CommandArgument='<%#Eval("Id") %>' OnCommand="children_Command" Style='<%#ShowChrenden(Eval("Id")) %>'></asp:LinkButton>&nbsp;&nbsp;<%}%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr style="color:red;">
                                        <td colspan="2" style="text-align:right;">
                                            合计：
                                        </td>
                                        <td>
                                            <script type="text/javascript">
                                                document.write(sumChongzhi.toFixed(2));
                                            </script>
                                        </td>
                                        <td>
                                             <script type="text/javascript">
                                                 document.write(sumChongzhikoufei.toFixed(2));
                                            </script>
                                        </td>
                                        <td>
                                             <script type="text/javascript">
                                                 document.write(sumtx.toFixed(2));
                                            </script>
                                        </td>
                                        <td>
                                             <script type="text/javascript">
                                                 document.write(sumhd.toFixed(2));
                                            </script>
                                        </td>
                                        <td>
                                             <script type="text/javascript">
                                                 document.write(sumfenhong.toFixed(2));
                                            </script>
                                        </td>
                                        <td>
                                             <script type="text/javascript">
                                                 document.write(sumTouZhu.toFixed(2));
                                            </script>
                                        </td>
                                        <td>
                                             <script type="text/javascript">
                                                 document.write(sumYouxifandian.toFixed(2));
                                            </script>
                                        </td>
                                        <td>
                                             <script type="text/javascript">
                                                 document.write(sumJiangjinpaisong.toFixed(2));
                                            </script>
                                        </td>
                                        <td >
                                             <script type="text/javascript">
                                                 document.write(sumYk.toFixed(2));
                                            </script>
                                        </td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                        <div class="row" style="text-align: right; padding-right: 50px;">
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

<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/resource/datepicker/WdatePicker.js"></script>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    function showdetails(code){
        window.location="/pages/Business/AccountDetailedManager.aspx?code="+code+"&bt="+$("#txtBegin").val()+"&end="+$("#txtEnd").val();
    }
    $(function(){
        $("a[name='children']").each(function(){
            if($(this).css("display")=="none")
                $(this).parent().parent().css("color","red");
        });
    });
</script>
