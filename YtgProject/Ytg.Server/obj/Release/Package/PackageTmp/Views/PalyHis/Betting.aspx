<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Betting.aspx.cs" Inherits="Ytg.ServerWeb.Views.PalyHis.Betting" MasterPageFile="~/Views/PalyHis/Play.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
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

                            var showOpenresult = (item.OpenResult == null ? "" : item.OpenResult);
                            var showTitle=showOpenresult;
                            if (showOpenresult!="") {
                                if (showOpenresult.length > 14) {
                                    showOpenresult = showOpenresult.substring(0, 14);
                                }
                            }
                        
                            var htm = "<tr><td>" + item.Code + "</td>";
                            htm += "<td>" + getDayTime(item.OccDate) + "</td>";
                            htm += "<td>" + item.LotteryName + "</td>";
                            htm += "<td>" + showRadioName + "</td>";
                            htm += "<td>" + item.IssueCode + "</td>";
                            htm += "<td><a href='javascript:showDetail(\"" + item.BetCode + "\",\"" + item.IssueCode + "\"," + item.tp + ");'>投注详情</a></td>";
                            //htm += "<td>" + item.Multiple + "</td>";
                            // htm += "<td>" + modelStr + "</td>";
                            htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.TotalAmt)) + "</td>";
                            htm += "<td>" + tolHtml + "</td>";
                            htm += "<td title='" + showTitle + "' style='cursor: pointer;'>" + showOpenresult + "</td>";
                            htm += "<td>" + states + "</td></tr>";
                            $(".ltbody").append(htm);
                            
                        }
                        //小计
                        /**小结*/
                        var htm = "<tr><td colspan='6' style='text-align:left;padding-left:20px;'> 小结: 本页盈亏:<span style='color:red;font-weight:bold;'>&nbsp;" + decimalCt(Ytg.tools.moneyFormat((sumnowinvalue-sumwivalue))) + "</span></td>";
                        htm += "<td><span  style='font-weight:bold;color:#26a0da;'>" + decimalCt(Ytg.tools.moneyFormat(sumwivalue)) + "</span></td>";
                        htm += "<td style='font-weight:bold;color:red;' class='winSpan'>" + decimalCt(Ytg.tools.moneyFormat(sumnowinvalue)) + "</td>";
                        htm += "<td></td>";
                        htm += "<td></td>";
                        htm += "</tr>";
                        $(".ltbody").append(htm);
                    } else {
                        $(".ltbody").Empty(12);
                        inintpager(0, 0);
                    }
                }
            });
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
                            htm += "<td>" + getDayTime(item.OccDate) + "</td>";
                            htm += "<td>" + item.LotteryName + "</td>";
                            htm += "<td>" + (item.PostionName==null?"":item.PostionName) + changePalyName(item.PlayTypeName + "" + item.PlayTypeRadioName, '') + "</td>";
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

        function showDetail(code,issueCode, betType) {
            var winHeight = $(window).height();
            var openHeight = 520;
            var ul = "url:/Lottery/BettingDetail.aspx?betcode=" + code;
            if (betType == 1 && issueCode != -1) {
                ul = "url:/Lottery/BettingDetail.aspx?catchCode=" + code + "&issueCode=" + issueCode;
            }
            if (betType == 2) {
                openHeight = winHeight - 100;
                ul = "url:/Lottery/CatchDetail.aspx?catchCode=" + code;
            }
            $.dialog({
                id: 'betdetail',
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "查看详情",
                content: ul,
                width: 820,
                height: openHeight
            });
        }
    </script>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <form runat="server" id="form1">
        <div>
            <!--工具栏-->
            <div class="toolbar-wrap">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable ">
                    <tbody>
                        <tr>
                            <td style="text-align: right;width:80px;">游戏时间：</td>
                            <td   colspan="5">
                                <input id="txtstart" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                                <span>至 </span>
                                <input id="txtend" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                            </td>
                        </tr>
                        <tr>
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
                            <td style="text-align: right;">交易类型：</td>
                            <td>
                                    <select id="buyType" >
                                        <option value="1" selected="selected">当天交易</option>
                                        <option value="2">历史记录</option>
                                    </select>
                            </td>
                            <td style="text-align: right;">状态：</td>
                            <td style="width: 290px;">
                                    <select id="states">
                                        <option selected="selected" value="-1">所有状态</option>
                                    </select>
                            </td>
                        </tr>
                          <tr>
                            
                            <td style="text-align: right;">注单编号：</td>
                            <td>
                                <input type="text" id="num" class="input normal"  style="width:120px;"/>
                            </td>
                            <td style="text-align: right;">游戏用户：
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
                            <td align="center" colspan="6">
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
                        <th >玩法</th>
                        <th>期号</th>
                        <th>投注内容</th>
                        <%--<th width="8%">倍数</th>--%>
                        <%--<th width="8%">模式</th>--%>
                        <th >总金额</th>
                        <th>奖金</th>
                        <th>开奖号码</th>
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
                        <th  style="padding: 0px;">追号时间</th>
                        <th  style="padding: 0px;">游戏</th>
                        <th  style="padding: 0px;">玩法</th>
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
</asp:Content>
