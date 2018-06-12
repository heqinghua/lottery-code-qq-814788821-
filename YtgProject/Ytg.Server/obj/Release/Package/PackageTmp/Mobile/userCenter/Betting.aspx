<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Betting.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.Betting"  %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %></title>
   <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="/Content/Css/feile/comn.css" rel="stylesheet" />
    <link href="/Content/Css/feile/keyframes.css" rel="stylesheet" />
    <link href="/Content/Css/feile/main.css" rel="stylesheet" />
    <link href="/Content/Css/feile/homepg.css" rel="stylesheet" />
    <link href="/Content/Css/base.css" rel="stylesheet" type="text/css" media="all" />
    <link href="/Content/Css/layout.css" rel="stylesheet" type="text/css" media="all" />
    <link href="/Content/Css/home.css" rel="stylesheet" type="text/css" media="all" />
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="/Content/Scripts/jquery.sortable.js"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <!--消息框代码开始-->
    <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
    <!--头部结束-->
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
    <link href="/Mobile/css/subpage1.css" rel="stylesheet" />
     <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Mobile/css/style1.css" rel="stylesheet" />
      <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
     <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
     <script src="/Content/Scripts/playname.js"></script>

    <style type="text/css">
        .xk_input_head td {
            padding-left: 2px !important;
        }
    </style>
    <style type="text/css">
        .l-list table {
            width: 100%;
        }

            .l-list table tr td {
                height: 40px;
                line-height: 40px;
            }
        
    </style>
    <script type="text/javascript">
        var gpid = '<%=drpGames.ClientID%>';
        var queryType = GetQueryString("type");
        var pageIndex = 1;
        var filterFuntion;

        $(function () {
            $("#proTitle").html(queryType == "catch" ? "追号记录" : "投注记录");
            if (queryType == "catch") {
                $("#catch").show();
                $("#zhuihao").addClass("title_active");
                $("#buyTypeDiv,#buyTypeSpan").hide();
                $("#states").append("<option value='0'>进行中</option>");
                $("#states").append("<option value='1'>已完成</option>");
                $("#states").append("<option value='2'>已取消</option>");
                filterFuntion = loadCatchData;

            }
            else {
                $("#betting").show();
                $("#touzhu").addClass("title_active");
                $("#states").append("<option value='1'>已中奖</option>");
                $("#states").append("<option value='2'>未中奖</option>");
                $("#states").append("<option value='3'>未开奖</option>");
                $("#states").append("<option value='4'>已撤单</option>");
                filterFuntion = loaddata;
            }
            $(".rule-single-select").ruleSingleSelect();
            //
            $("#txtstart").val('<%=FilterSdate%>');

            $("#txtend").val('<%=FiltetEDate%>');


            $("#" + gpid).change(function () {
                var value = $(this).val();
                var lid = value.split(',');
                $.ajax({
                    url: "/Page/Lott/Lottery.aspx",
                    type: 'post',
                    data: "action=htmlradio&lotteryid=" + lid[0],
                    success: function (data) {
                        $("#selPlayTypes").empty();
                        $("#selIssue").empty();
                        var jsonData = JSON.parse(data);
                        $("#selPlayTypes").append("<option value='-1' selected='selected'>所有</option>");
                        $("#selIssue").append("<option value='' selected='selected'>所有奖期</option>");
                        //清除
                        if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                            for (var c = 0; c < jsonData.Data.length; c++) {
                                var item = jsonData.Data[c];
                                $("#selPlayTypes").append("<option value='" + item.RadioCode + "'>" + item.playTypeRadioName + "</option>");
                            }

                            //期号
                            if (jsonData.ErrMsg != "") {
                                var array = jsonData.ErrMsg.split(',');
                                for (var i = 0; i < array.length; i++) {
                                    var item = array[i];
                                    $("#selIssue").append("<option value='" + item + "'>" + item + "</option>");
                                }
                            }

                            $(".rule-single-select").ruleSingleSelect();
                        }

                    }
                });

            });
            //page
            $("#lbtnSearch").click(function () {
                filterFuntion();//加载数据
                
            });
            filterFuntion();//加载数据
        });

        function getQueryData() {
         
            var gmValue = $("#" + gpid).find("option:selected").val();
           
            var filterData = "startTime=" + $("#txtstart").val() + "&endTime=" + $("#txtend").val();
            filterData += "&status="+ $("#states").find("option:selected").val();
            filterData += "&tradeType=" + $("#buyType").find("option:selected").val();
            filterData += "&SellotteryCode=" + (gmValue == "" ? "" : gmValue.split(',')[1]);
            filterData += "&lotteryid=" + (gmValue == "" ? "" : gmValue.split(',')[0]);
            filterData += "&palyRadioCode=" + $("#selPlayTypes").find("option:selected").val();
            filterData += "&issueCode=" + $("#selIssue").find("option:selected").val();
            filterData += "&model=" + $("#selModel").find("option:selected").val();
            filterData += "&betCode=" + $("#num").val();
            filterData += "&userAccount=" + $("#txtAccount").val();
            filterData += "&userType=" + $("#selArea").find("option:selected").val();
            filterData += "&pageIndex=" + pageIndex;

            return filterData;
        }
        
        function loaddata() {
            Ytg.common.loading();
            $.ajax({
                url: "/Page/Lott/LotteryBetDetail.aspx",
                type: 'post',
                data: getQueryData() + "&action=betlist",
                success: function (data) {

                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    //清除
                    $(".ltbody").children().remove();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        //分页
                       
                        inintpager(pageIndex, jsonData.Total, function (n) {

                            pageIndex = n;
                            filterFuntion();

                        });

                        var sumwivalue = 0;
                        var sumnowinvalue = 0;
                        for (var c = 0; c < jsonData.Data.length; c++) {
                            var item = jsonData.Data[c];
                            var states = Ytg.common.LottTool.GetStateContent(item.Stauts + "_")
                            var betContent = Ytg.common.LottTool.ShowBetContent(item.BetContent);
                            var modelStr = "";
                            switch (item.Model) {
                                case 0:
                                    modelStr = "元";
                                    break;
                                case 1:
                                    modelStr = "角";
                                    break;
                                case 2:
                                    modelStr = "分";
                                    break;
                                case 2:
                                    modelStr = "厘";
                                    break;
                            }

                            sumwivalue += item.TotalAmt;
                            sumnowinvalue += item.WinMoney;

                            var winAmt = decimalCt(Ytg.tools.moneyFormat(item.WinMoney));

                            var tolHtml = winAmt;
                            if (winAmt.toString().indexOf(',') >= 0 || winAmt > 0)
                                tolHtml = "<span class='winSpan'>" + winAmt + "</span>";
                          
                            var psName = item.PostionName == null ? "" : item.PostionName;
                           
                            var showRadioName = psName + changePalyName(item.PlayTypeName + "" + item.PlayTypeRadioName, '');
                        
                            var htm = "<tr onclick='showDetail(\"" + item.BetCode + "\",\"" + item.IssueCode + "\"," + item.tp + ");'><td>" + item.Code + "</td>";
                            htm += "<td>" + getDayTime(item.OccDate) + "</td>";
                            htm += "<td>" + item.LotteryName + "</td>";
                            //htm += "<td>" + showRadioName + "</td>";
                            htm += "<td>" + item.IssueCode + "</td>";
                            //htm += "<td><a href='javascript:showDetail(\"" + item.BetCode + "\",\"" + item.IssueCode + "\"," + item.tp + ");'>投注详情</a></td>";
                            //htm += "<td>" + item.Multiple + "</td>";
                            // htm += "<td>" + modelStr + "</td>";
                            //htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.TotalAmt)) + "</td>";
                            htm += "<td>" + tolHtml + "</td>";
                            //htm += "<td>" + (item.OpenResult == null ? "" : item.OpenResult) + "</td>";
                            htm += "<td>" + states + "</td></tr>";
                            $(".ltbody").append(htm);
                            
                        }
                        //小计
                        /**小结*/
                        var htm = "<tr><td colspan='4' style='text-align:left;padding-left:20px;'> 小结: 本页盈亏:<span style='color:red;font-weight:bold;'>&nbsp;" + decimalCt(Ytg.tools.moneyFormat((sumnowinvalue-sumwivalue))) + "</span></td>";
                      //  htm += "<td><span  style='font-weight:bold;color:#26a0da;'>" + decimalCt(Ytg.tools.moneyFormat(sumwivalue)) + "</span></td>";
                        htm += "<td colspan='2' style='font-weight:bold;color:red;' class='winSpan'>" + decimalCt(Ytg.tools.moneyFormat(sumnowinvalue)) + "</td>";
                       // htm += "<td></td>";
                       // htm += "<td></td>";
                        htm += "</tr>";
                        $(".ltbody").append(htm);
                    } else {
                        $(".ltbody").Empty(12);
                        inintpager(0, 0);
                    }
                }

            });
        }

        function goDetails() {

        }

        function loadCatchData() {//追号查询
            Ytg.common.loading();
            var gmValue = $("#" + gpid).find("option:selected").val();
            var filterData = {
                "BeginTime": $("#txtstart").val(),
                "EndTime": $("#txtend").val(),
                "State": $("#states").find("option:selected").val(),
                "LotteryCode": gmValue == "" ? "" : gmValue.split(',')[1],
                "PalyRadioCode": $("#selPlayTypes").find("option:selected").val(),
                "IssueCode": $("#selIssue").find("option:selected").val(),
                "Mode": $("#selModel").find("option:selected").val(),
                "CatchNumCode": $("#num").val(),
                "PalyUserCode": $("#txtAccount").val(),
                "UserScope": $("#selArea").find("option:selected").val(),
                "tradeType": $("#buyType").find("option:selected").val(),
            };
            //状态 -1 全部 1 已中奖、2 未中奖、3 未开奖、4 已撤单
            //状态 -1 全部 1 已中奖、2 未中奖、3 未开奖、4 已撤单
            $.ajax({
                url: "/Page/Lott/LotteryBetDetail.aspx",
                type: 'post',
                data: "action=catchnumlist&data=" + JSON.stringify(filterData)+"&pageIndex="+pageIndex,
                success: function (data) {
                   
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    //清除
                    $("#catchBody").children().remove();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        //分页
                     
                        inintpager(pageIndex, jsonData.Total, function (n) {
                            pageIndex = n;
                            filterFuntion();
                        });

                        var sumwivalue = 0;
                        var sumnowinvalue = 0;
                        var sumzhuiinvalue = 0;
                        for (var c = 0; c < jsonData.Data.length; c++) {
                            var item = jsonData.Data[c];
                            var states="";
                            if (item.Stauts == 1) {
                                states = "<span style='color:red;'>已完成</span>";
                            } else {
                                states = Ytg.common.LottTool.GetStateContent(item.Stauts + "_");
                            }
                           
                            var betContent = Ytg.common.LottTool.ShowBetContent(item.BetContent);
                            var modelStr = "";
                            switch (item.Model) {
                                case 0:
                                    modelStr = "元";
                                    break;
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

                            sumwivalue += item.SumAmt;
                            sumnowinvalue += item.CompledMonery;
                            sumzhuiinvalue += item.WinMoney;

                            var winAmt = decimalCt(Ytg.tools.moneyFormat(item.WinMoney));

                            var tolHtml = winAmt;
                            if (winAmt.toString().indexOf(',') >= 0 || winAmt > 0)
                                tolHtml = "<span class='winSpan'>" + winAmt + "</span>";
                            
                            var htm = "<tr><td>" + item.Code + "</td>";
                            //htm += "<td>" + getDayTime(item.OccDate) + "</td>";
                            htm += "<td>" + item.LotteryName + "</td>";
                            //htm += "<td>" + (item.PostionName==null?"":item.PostionName) + changePalyName(item.PlayTypeName + "" + item.PlayTypeRadioName, '') + "</td>";
                            //htm += "<td>" + item.BeginIssueCode + "</td>";
                            //htm += "<td>" + item.CatchIssue + "</td>";
                            //htm += "<td>" + item.CompledIssue + "</td>";
                            htm += "<td><a href='javascript:showDetail(\"" + item.CatchNumCode + "\",\"-1\",2);'>追号详情</a></td>";
                            //htm += "<td>" + modelStr + "</td>";
                            htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.SumAmt)) + "</td>";
                            htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.CompledMonery)) + "</td>";
                            htm += "<td>" + tolHtml + "</td>";
                            htm += "<td>" + states + "</td></tr>";
                            $(".ltbody").append(htm);
                        }

                        /**小结*/
                        var htm = "<tr><td colspan='5' style='text-align:left;padding-left:20px;'> 小结: 本页盈亏:<span style='color:red;font-weight:bold;'>&nbsp;" + decimalCt(Ytg.tools.moneyFormat((sumzhuiinvalue - sumnowinvalue))) + "</span></td>";
                        htm += "<td><span  style='font-weight:bold;color:#26a0da;'>" + decimalCt(Ytg.tools.moneyFormat(sumwivalue)) + "</span></td>";
                        htm += "<td style='color:#26a0da;font-weight:bold;'>" + decimalCt(Ytg.tools.moneyFormat(sumnowinvalue)) + "</td>";
                        htm += "<td style='color:red;font-weight:bold;'>" + decimalCt(Ytg.tools.moneyFormat(sumzhuiinvalue)) + "</td>";
                        htm += "<td></td>";
                        htm += "</tr>";
                        $(".ltbody").append(htm);

                    } else {
                        $("#catchBody").Empty(13);
                        inintpager(0, 0);
                    }
                }
            });
        }

        function showDetail(code, issueCode, betType) {
          
            var ul = "/Mobile/userCenter/BettingDetail.aspx?betcode=" + code;
            if (betType == 1 && issueCode != -1) {
                ul = "/Mobile/userCenter/BettingDetail.aspx?catchCode=" + code + "&issueCode=" + issueCode;
            }
            if (betType == 2) {
                openHeight = winHeight - 100;
                ul = "/Mobile/userCenter/CatchDetail.aspx?catchCode=" + code;
            }
            window.location = ul;
        }
    </script>
</head>
    <body>
         <nav class="col-xs-12 title" style="position:fixed;z-index:999;left:0px;top:0px;">
         <a id="J-goback" href="/mobile/user_center.aspx" class="go-back">返回</a>
         <%=string.IsNullOrEmpty(Request.Params["type"])?"投注记录":"追号记录" %></nav>
        <div  class="ctParent">
    <form runat="server" id="form1">
        <div>
            <!--工具栏-->
            <div class="toolbar-wrap">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable ">
                    <tbody>
                        <tr>
                            <td style="text-align: right;width:100px;">时间：</td>
                            <td   colspan="4">
                                <input id="txtstart" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                                <span>至 </span>
                                <input id="txtend" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td style="text-align: right;width:100px">游戏名称：</td>
                            <td style="width:180px">
                               <asp:DropDownList ID="drpGames" runat="server"></asp:DropDownList>
                            </td>
                          
                            <td style="text-align: right;width:100px">游戏奖期：</td>
                            <td style="width:200px;">
                                    <select id="selIssue" ">
                                        <option selected="selected" value="">所有奖期</option>
                                    </select>
                            </td>
                            <td style="text-align: right;width:100px">游戏模式：
                            </td>
                            <td >
                                    <select id="selModel">
                                        <option selected="selected" value="-1">所有模式</option>
                                        <option value="0">元</option>
                                        <option value="1">角</option>
                                        <option value="2">分</option>
                                    </select>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">范围：</td>
                            <td>
                                    <select id="selArea" >
                                        <option value="-1">所有</option>
                                        <option value="1"  selected="selected">自己</option>
                                        <option value="2">直接下级</option>
                                        <option value="3">所有下级</option>
                                    </select>
                            </td>
                            <td style="text-align: right;display:none;">交易类型：</td>
                            <td style="display:none;">
                                    <select id="buyType" >
                                        <option value="1">当天交易</option>
                                        <option value="2"  selected="selected">历史记录</option>
                                    </select>
                            </td>
                            <td style="text-align: right;width:100px;">状态：</td>
                            <td style="width: 280px;">
                                    <select id="states">
                                        <option selected="selected" value="-1">所有状态</option>
                                    </select>
                            </td>
                        </tr>
                          <tr>
                            <td style="text-align: right;display:none;">注单编号：</td>
                            <td style="display:none;">
                                <input type="text" id="num" class="input normal"  style="width:120px;"/>
                            </td>
                            <td style="text-align: right;">用户：
                            </td>
                            <td colspan="3">
                                <input type="text" id="txtAccount" class="input normal" style="width:120px;"/>
                            </td>
                                <td style="text-align: right;width:100px;display:none;">游戏玩法：
                            </td>
                            <td style="width:240px;display:none;">
                                    <select id="selPlayTypes">
                                        <option selected="selected" value="-1">所有</option>
                                    </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <input name="" id="lbtnSearch" type="button" value="查询" class="formCheck"></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!--/工具栏-->

            <!--列表-->
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable" id="betting" style="display: none;">
                <thead>
                    <tr>
                        <th>用户</th>
                        <th>投注时间</th>
                        <th >游戏</th>
                       <%-- <th >玩法</th>--%>
                        <th>期号</th>
                      <%--  <th>投注内容</th>--%>
                        <%--<th width="8%">倍数</th>--%>
                        <%--<th width="8%">模式</th>--%>
                       <%-- <th >总金额</th>--%>
                        <th>奖金</th>
                        <%--<th>开奖号码</th>--%>
                        <th>状态</th>
                    </tr>
                </thead>
                <tbody class="ltbody" id="bettingBody">
                    <tr>
                        <td align="center" colspan="12">暂无记录</td>
                    </tr>
                </tbody>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable" style="display: none;" id="catch">
                <thead>
                    <tr>
                        <th style="padding: 0px;">用户</th>
                      <%--  <th  style="padding: 0px;">追号时间</th>--%>
                        <th  style="padding: 0px;">游戏</th>
                        <%--<th  style="padding: 0px;">玩法</th>--%>
                        <%-- <th width="5%"  style="padding:0px;">开始<br />
                            期号</th>
                        <th width="5%"  style="padding:0px;">追号<br />
                            期数</th>
                        <th width="5%"  style="padding:0px;">完成<br />
                            期数</th>--%>
                        <th>追号内容</th>
                       <%-- <th width="5%" style="padding: 0px;">模式</th>--%>
                        <th style="padding: 0px;">总金额</th>
                        <th  style="padding: 0px;">完成金额</th>
                        <th style="padding: 0px;">中奖金额</th>
                        <th  style="padding: 0px;">状态</th>
                    </tr>
                </thead>
                <tbody class="ltbody" id="catchBody">
                    <tr>
                        <td align="center" colspan="13">暂无记录</td>
                    </tr>
                </tbody>
            </table>
            <!--/列表-->
            <div id="kkpager" style=""></div>
        </div>
    </form>
</div>
</body>
</html>