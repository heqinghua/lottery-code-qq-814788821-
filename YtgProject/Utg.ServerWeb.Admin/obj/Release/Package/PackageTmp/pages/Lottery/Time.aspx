<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Time.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Lottery.Time" %>
<%--<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>--%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>时间管理</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">时间管理</h2>
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
                            <%
                                int mLotteryId = 0;
                                string mLotteryName = "";
                                for (int i = 0; i < lotteryTypeList.Count; i++)
                                {
                                    mLotteryId = lotteryTypeList[i].Id;
                                    mLotteryName = lotteryTypeList[i].LotteryName;
                                %>
                                     <a href="/pages/Lottery/Time.aspx?lotteryId=<%=mLotteryId %>" class="<%=SetActive(mLotteryId) %>" target="main"><%=mLotteryName %></a>
                                <% }
                                %>
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
                        时间管理
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <div id="myTabContent" class="tab-content">
                               <div class="tab-pane fade in active" id="content">
                                     <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                        <thead>
                                            <tr>
                                                <th class="auto-style1">彩种</th>
                                                <th class="auto-style1">期数</th>
                                                <th class="auto-style1">起售时间</th>
                                                <th class="auto-style1">停售时间</th>
                                                <th class="auto-style1">开奖时间</th>
                                                <th class="auto-style1">操作</th>
                                            </tr>
                                        </thead>
                                        <tbody id="body-content">
                                            <asp:Repeater ID="repList" runat="server">
                                                <ItemTemplate>
                                                        <td><%#Eval("LotteryName") %></td>
                                                        <td><%#Eval("IssueCode") %></td>
                                                        <td><asp:TextBox ID="txtStartTime" style="text-align:center;"  runat="server" Text='<%# ((DateTime)Eval("StartSaleTime")).ToString("HH:mm:ss") %>'></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtEndTime" style="text-align:center;" runat="server" Text='<%# ((DateTime)Eval("EndSaleTime")).ToString("HH:mm:ss") %>'></asp:TextBox></td>
                                                        <td><asp:TextBox ID="TxtLotteryTime" style="text-align:center;" runat="server" Text='<%# ((DateTime)Eval("EndTime")).ToString("HH:mm:ss") %>'></asp:TextBox></td>
                                                        <td><asp:LinkButton ID="btnUpdate" Text="修改" CommandArgument='<%#Eval("Id")%>' OnCommand="btnUpdate_Command" runat="server" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                               </div>
                            </div>
                        </div>
                       <%-- <!-- /.table-responsive -->
                        <div class="row" style="text-align: right; padding-right: 50px;">
                            <webdiyer:AspNetPager ID="pagerControl" runat="server" PageSize="20" OnPageChanged="pagerControl_PageChanged"></webdiyer:AspNetPager>
                        </div>--%>
                   </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
                <!-- /.col-lg-12 -->
            </div>
            <!--/列表-->
        </div>
        <asp:HiddenField ID="txtLotteryId" runat="server" />
    </form>
   </body>
</html>


<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/resource/datepicker/WdatePicker.js"></script>
<link href="/Content/Css/subpage.css" rel="stylesheet" />
<link href="/dist/css/font.css" rel="stylesheet" />
