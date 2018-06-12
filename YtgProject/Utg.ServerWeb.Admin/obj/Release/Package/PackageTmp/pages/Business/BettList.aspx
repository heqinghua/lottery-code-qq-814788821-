<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BettList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Business.BettList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>菜单管理</title>
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
<link href="/resource/Pager/kkpager_orange.css" rel="stylesheet" />
<script src="/resource/Pager/kkpager.min.js"></script>
<link href="/Content/Css/subpage.css" rel="stylesheet" />
<link href="/dist/css/font.css" rel="stylesheet" />
 <script src="/js/playname.js" type="text/javascript"></script>
    <script src="../../js/formart/config.js"></script>
    <script src="../../js/formart/basic.js"></script>
    <script src="../../js/formart/comm.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">投注列表</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable">
            <tr>
                <td ><span class="autoSpan">投注时间：</span></td>
                <td colspan="6">
                    <table>
                        <tr>
                            <td><asp:TextBox ID="txtBegin" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                            <td>&nbsp;至&nbsp;</td>
                            <td><asp:TextBox ID="txtEnd" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><span class="autoSpan">登陆账号：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">期数：</span></td>
                <td>
                    <asp:TextBox ID="txtIssue" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">单号：</span></td>
                <td>
                    <asp:TextBox ID="txtNum" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td style="text-align:right;"><span class="autoSpan">状态：</span></td>
                <td>
                    <asp:DropDownList ID="drpState" runat="server" CssClass="form-control autoBox">
                        <asp:ListItem Text="全部" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="已中奖" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未中奖" Value="2"></asp:ListItem>
                        <asp:ListItem Text="未开奖" Value="3"></asp:ListItem>
                        <asp:ListItem Text="已撤单" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="text-align:right;"><span class="autoSpan">游戏：</span></td>
                <td>
                    <asp:DropDownList ID="drpGame" runat="server" CssClass="form-control autoBox">
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
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Text="全部" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="元" Value="0"></asp:ListItem>
                        <asp:ListItem Text="角" Value="1"></asp:ListItem>
                        <asp:ListItem Text="分" Value="2"></asp:ListItem>
                        <asp:ListItem Text="厘" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">用户类型：</span></td>
                <td>
                    <asp:DropDownList ID="drpUserType" runat="server" CssClass="form-control autoBox">
                        <asp:ListItem Text="全部" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="总代理" Value="0"></asp:ListItem>
                        <asp:ListItem Text="代理" Value="1"></asp:ListItem>
                        <asp:ListItem Text="会员" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <input id="btnFilter" class="btnf" value="查询" type="button" />
                </td>
            </tr>
            

        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        投注数据列表
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>单号</th>
                                        <th>用户名</th>
                                        <th>投注时间</th>
                                        <th>彩种</th>
                                        <th>玩法</th>
                                        <th>期号</th>
                                        <th>倍数</th>
                                        <th>模式</th>
                                        <th>投注内容</th>
                                        <th>开奖号码</th>
                                        <th>投注金额</th>
                                        <th>中奖金额</th>
                                        <th>状态</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody id="body-content">
                                    <tr class="odd gradeX">
                                        <td colspan="13" style="text-align: center;">暂时无数据</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                        <div class="row" style="text-align: right; padding-right: 50px;">
                            <div id="kkpager"></div>
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
                        <h4 class="modal-title" id="myModalLabel">投注内容</h4>
                    </div>
                    <div class="modal-body">
                        <input id="txtId" type="hidden" />
                        <input id="catcNo" type="hidden" />
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


        <!-- /.modal -->
    </form>
</html>

<script type="text/javascript">
    var pageIndex = 1;
    var pageSize = 20;

    function inintpager(pageIndex, totalCount, cannback) {
        //生成分页
        var pageCount = parseInt((totalCount % 20 == 0 ? totalCount / 20 : totalCount / 20 + 1));
        $("#kkpager").empty();
        //有些参数是可选的，比如lang，若不传有默认值
        kkpager.generPageHtml({
            pno: pageIndex,
            //总页码
            total: pageCount,
            ////总数据条数
            totalRecords: totalCount,
            mode: 'click',//默认值是link，可选link或者click
            click: function (n) {
                if (undefined != cannback)
                    cannback(n);
                this.selectPage(n);
                return false;
            }
            , lang: {
                firstPageText: '首页',
                firstPageTipText: '首页',
                lastPageText: '尾页',
                lastPageTipText: '尾页',
                prePageText: '上一页',
                prePageTipText: '上一页',
                nextPageText: '下一页',
                nextPageTipText: '下一页',
                totalPageBeforeText: '共',
                totalPageAfterText: '页',
                currPageBeforeText: '当前第',
                currPageAfterText: '页',
                totalInfoSplitStr: '/',
                totalRecordsBeforeText: '共',
                totalRecordsAfterText: '条数据',
                gopageBeforeText: ' 转到',
                gopageButtonOkText: 'GO',
                gopageAfterText: '页',
                buttonTipBeforeText: '第',
                buttonTipAfterText: '页'
            }

        });
    }
    $("#add").click(function () {

        parent.openModal("新增会员", "/pages/Customer/EditUser.aspx?dt" + encodeURI(new Date()), function () {

            $("#btnRef")[0].click();
        }, 650, 580);
    });

    $(function () {
        $("#drpGame").change(function () {
            var lotteryCode = $(this).find("option:selected").val();
            if (lotteryCode == "")
                return;
            $.ajax({
                url: "/pages/Helper.aspx",
                type: 'post',
                data: "action=lotteryRadio&lotteryCode=" + lotteryCode,
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        $("#drpPlayType").find("option").remove();
                        $("#drpPlayType").append("<option value=''>全部</option>");
                        for (var o = 0; o < jsonData.Data.length; o++) {
                            var item = jsonData.Data[o];
                            $("#drpPlayType").append("<option value=" + item.RadioCode + ">" + (item.PlayTypeRadioName) + "</option>")
                        }
                    } else {

                    }
                }
            });
        });

        loaddata();
        $("#btnFilter").click(function () {
            pageIndex = 1;
            loaddata();
        });
    });

    function loaddata() {
        var params = {
            action: "filterBetting",
            beginTime: $("#txtBegin").val(),
            endTime: $("#txtEnd").val(),
            status: $("#drpState").val(),
            palyRadioCode: $("#drpPlayType").find("option:selected").val(),
            model: $("#drpModel").find("option:selected").val(),
            userType: $("#drpUserType").find("option:selected").val(),
            lotteryCode: $("#drpGame").find("option:selected").val(),
            issueCode: $("#txtIssue").val(),
            betCode: $("#txtNum").val(),
            userCode: $("#txtUserCode").val(),
            pageIndex: pageIndex,
        };
        $.ajax({
            url: "/pages/Helper.aspx",
            type: 'post',
            data: params,
            success: function (data) {
                var jsonData = JSON.parse(data);
                $("#body-content").children().remove();
                //清除
                if (jsonData.Code == 0) {
                    if (jsonData.Data != undefined && jsonData.Data.length > 0) {
                        //分页
                        inintpager(pageIndex, jsonData.Total, function (n) {
                         
                            pageIndex = n;
                            loaddata();
                        });

                        for (var o = 0; o < jsonData.Data.length; o++) {
                            var item = jsonData.Data[o];
                            for (var o = 0; o < jsonData.Data.length; o++) {
                                var item = jsonData.Data[o];
                                var modelStr = "元";
                                switch (item.Model) {
                                    case 1:
                                        modelStr = "角";
                                        break;
                                    case 2:
                                        modelStr = "分";
                                        break;
                                    case 3:
                                        modelStr = "厘";
                                        break;
                                }
                                var staStr = "已中奖";
                                switch (item.Stauts) {
                                    case 2:
                                        staStr = "未中奖";
                                        break;
                                    case 3:
                                        staStr = "未开奖";
                                        break;
                                    case 4:
                                        staStr = "本人撤单";
                                        break;
                                    case 5:
                                        staStr = "系统撤单";
                                        break;
                                }
                                var showContent = "查看详情";
                                if (showContent.length > 50)
                                    showContent = showContent.substring(0, 50);
                                var showPName = item.PostionName == "" ? changePalyName(item.PlayTypeNumName, item.PlayTypeRadioName) : item.PostionName;
                                var html = "<tr class='odd gradeX' >";
                                html += "<td>" + item.BetCode +"</td>";
                                html += "<td>" + item.Code + "</td>";
                                html += "<td>" + item.OccDate + "</td>";
                                html += "<td>" + item.LotteryName + "</td>";
                                html += "<td>" + (showPName) + "</td>";
                                html += "<td>" + item.IssueCode + "</td>";
                                html += "<td>" + item.Multiple + "</td>";
                                html += "<td>" + modelStr + "</td>";
                                html += "<td ><a href=\"javascript:showbetDetail(" + item.Id + ",'" + item.BetContent + "','" + item.IssueCode + "','" + (item.BetCode) + "');\" >" + showContent + "</a></td>";
                                html += "<td>" + (item.OpenResult == null ? "" : item.OpenResult) + "</td>";
                                html += "<td>" + item.TotalAmt + "</td>";
                                html += "<td>" + item.WinMoney + "</td>";
                                html += "<td>" + staStr + "</td>";
                                html += "<td>";
                                <% if (actionModel.Revocation == true)
                                   {%>
                                if (item.Stauts != 4 && item.Stauts!=5)
                                html += "<a href='javascript:undo(\"" + (item.CatchNumIssueCode == "" ? item.BetCode : item.CatchNumIssueCode) + "\",\"" + item.Stauts + "\",\"" + item.BetCode + "\");'>撤销</a>&nbsp;";
                                <%}%>
                                //html += "<a href='javascript:del(" + item.Id + ");'>修改</a>&nbsp;";          
                                html += "</td>";
                                html += "</tr>";

                                $("#body-content").append(html);
                            }
                        }
                        $(".tooltip-hide").tooltip();
                    } else {
                        $("#body-content").html("<tr class=\"odd gradeX\"><td colspan=\"13\" style=\"text-align:center;\">暂时无数据</td></tr>");
                    }
                } else {
                    $("#body-content").html("<tr class=\"odd gradeX\"><td colspan=\"13\" style=\"text-align:center;\">暂时无数据</td></tr>");
                }
            }
        });
    }

    function undo(betCode, st, catchNum) {
        //if (st == 3) {
        //    alert("尚未开奖，无法撤销！");
        //    return;
        //}
        if (confirm("确定要撤销吗？")) {
            $.ajax({
                url: "/pages/Helper.aspx",
                type: 'post',
                data: "action=undo&betCode=" + betCode + "&catchNum=" + catchNum,
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        //loaddata();
                        loaddata();
                        alert("撤销成功！");
                    } else {
                        alert("撤销失败，请刷新后重试！");
                    }
                }
            });
        }
    }

    function del(id) {
        if (confirm("确定要删单吗？")) {
            $.ajax({
                url: "/pages/Helper.aspx",
                type: 'post',
                data: "action=delete&id=" + id,
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        loaddata();
                    } else {
                        alert("删单失败，请刷新后重试！");
                    }
                }
            });
        }
    }

    function edit() {
        if (confirm("确定要修改单？")) {
            var id = $.trim($("#txtId").val());
            var issueCode = $.trim($("#txtIssueCode").val());
            var betContent = $.trim($("#betContent").val());

            if (issueCode == "") {
                alert("请输入期号!");
                return;
            }

            if (betContent == "") {
                alert("请输入投注内容!");
                return;
            }
            //$("#catcNo").val(catchNo);
            $.ajax({
                url: "/pages/Helper.aspx",
                type: 'post',
                data: "action=edit&id=" + id + "&issueCode=" + issueCode + "&betContent=" + betContent+"&catNuo="+$("#catcNo").val(),
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        $('#myModal').modal("hide");
                        loaddata();
                    } else {
                        alert("修改失败，请刷新后重试！");
                    }
                }
            });
        }
    }

    function showbetDetail(id, details, issueCode,catchNo) {
        $("#txtId").val(id);
        
        $("#betContent").val(Ytg.common.LottTool.ShowBetContent(details, 1));
        $("#txtIssueCode").val(issueCode);
        $('#myModal').modal();
        $("#catcNo").val(catchNo);
    }
</script>
