<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AmountChangeListControl.ascx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.AmountChangeListControl" %>
<link href="/Mobile/css/subpage1.css" rel="stylesheet" />
<script src="/Content/Scripts/playname.js"></script>
<script type="text/javascript">
    var gpid = '<%=drpGames.ClientID%>';
    var pageIndex = 1;
 
    $(function () {
        var queryAccount = GetQueryString("account");
        if (queryAccount != "") {
            $("#userCode").val(queryAccount);
            $("#cmbUserType").find("option").first().attr("selected", "selected");
        }

        //
       
        $("#txtstart").val('<%=FilterSdate%>');
       
        $("#txtend").val('<%=FiltetEDate%>');
        //
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
                    $("#selPlayTypes").append("<option value='-1' selected='selected'>所有玩法</option>");
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
        //按钮
        $(".action").each(function () {
            $(this).click(function () {
                var aid = $(this).attr("id");
                var ft = "-1";
                switch (aid) {
                    case "btnCz"://我的充值
                        ft = "1";
                        break;
                    case "btnTx"://我的提现
                        ft = "2";
                        break;
                    case "btnTz"://我的投注
                        ft = "3";
                        break;
                    case "btnZh"://我的追号
                        ft = "4";
                        break;
                    case "btnJj"://我的奖金
                        ft = "7";
                        break;
                    case "btnFd"://我的返点
                        ft = "6";
                        break;
                }
                loaddata(ft);
            });
        });
        //page
        $("#lbtnSearch").click(function () {
            loaddata();
        });
        loaddata();
    });

    function loaddata(ft) {

        var tradeTypevalue = "";
        $('#ordertype :selected').each(function (i, selected) {
            tradeTypevalue += $(selected).val() + ",";
        });
        var gmValue = $("#" + gpid).find("option:selected").val();

        var paramData = {
            "tradeType": ft != undefined ? ft : tradeTypevalue,
            "startTime": $("#txtstart").val(),
            "endTime": $("#txtend").val(),
            "tradeDateTime": $("#cmbTradeType").find("option:selected").val(),
            "account": $("#userCode").val(),
            "userType": $("#cmbUserType").find("option:selected").val(),
            "codeType": $("#cmbCodeType").find("option:selected").val(),
            "code": $("#txtCode").val(),
            "SellotteryCode": (gmValue == "" ? "" : gmValue.split(',')[1]),
            "palyRadioCode": $("#selPlayTypes").find("option:selected").val(),
            "issueCode": $("#selIssue").find("option:selected").val(),
            "model": $("#selModel").find("option:selected").val(),
            "pageIndex": pageIndex,
            "action": "selectamountchanglist"
        };
        Ytg.common.loading();
        $.ajax({
            url: "/Page/Repot/AmtChange.aspx",
            type: 'post',
            data: paramData,
            success: function (data) {
                Ytg.common.cloading();
                var jsonData = JSON.parse(data);
                //清除
                $(".ltbody").children().remove();
                if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                    //分页
                    inintpager(pageIndex, jsonData.Total, function (n) {
                        pageIndex = n;
                        loaddata();
                    });
                    var sumwivalue = 0;
                    var sumnowinvalue = 0;
                    for (var c = 0; c < jsonData.Data.length; c++) {
                        var item = jsonData.Data[c];
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
                        sumwivalue += item.InAmt;
                        sumnowinvalue += item.OutAmt;

                        var sumUserAmt = item.UserAmt;
                        var wivalue = decimalCt(Ytg.tools.moneyFormat(item.InAmt));
                        if (wivalue <= 0) {
                            wivalue = "——";
                        }
                        else {
                          //  sumUserAmt += item.InAmt;
                            wivalue = "+" + wivalue;
                        }

                        var nowinvalue = decimalCt(Ytg.tools.moneyFormat(item.OutAmt));
                        if (nowinvalue >= 0) {
                            nowinvalue = "——";
                        }
                        else {
                            nowinvalue = nowinvalue;
                           // sumUserAmt += item.OutAmt;
                        }
                        var posName = item.PostionName == null ? "" : item.PostionName;
                        var plName = (item.PlayTypeName == null ? "" : (item.PlayTypeName + item.PlayTypeRadioName));
                        plName = posName+changePalyName(plName, '', '')
                      
                        var relevanceNo = item.RelevanceNo;//b c
                        var opendHtm = "";
                        if (item.PlayTypeName != null)
                            opendHtm = "<td><a href=\"javascript:openDetails('" + relevanceNo + "');\">" + plName + "</a></td>";
                        else
                            opendHtm="<td>" + plName + "</td>";
                        //
                      
                        var htm = "<tr>";//"<tr><td>" + item.SerialNo + "</td>";
                        htm += "<td>" + item.UserAccount + "</td>";
                        htm += "<td>" + getDayTime(item.OccDate) + "</td>";
                        htm += "<td>" + Ytg.common.SpecialConvert.AmtTradeType(item.TradeType) + "</td>";
                       // htm += "<td>" + (item.LotteryName == null ? "" : item.LotteryName)+ "</td>";
                        htm += opendHtm;//"<td>" + plName + "</td>";
                        htm += "<td>" + (item.IssueCode == null ? "" : item.IssueCode) + "</td>";
                       // htm += "<td>" + modelStr + "</td>";
                        htm += "<td><span class='winSpan'>" + wivalue + "</span></td>";
                        htm += "<td style='color:#26a0da;'>" + nowinvalue + "</td>";
                        htm += "<td>" +  (Ytg.tools.moneyFormat(sumUserAmt)) + "</td>";
                        htm += "</tr>";
                        $(".ltbody").append(htm);
                    }
                    /**小结*/
                    var htm = "<tr><td colspan='6' style='text-align:left;padding-left:20px;'> 小结: 本页变动金额:<span style='color:red;font-weight:bold;'>&nbsp;" + decimalCt(Ytg.tools.moneyFormat((sumwivalue + sumnowinvalue))) + "</span></td>";
                    htm += "<td><span class='winSpan' style='font-weight:bold;'>+" + decimalCt(Ytg.tools.moneyFormat(sumwivalue)) + "</span></td>";
                    htm += "<td style='color:#26a0da;font-weight:bold;'>" + decimalCt(Ytg.tools.moneyFormat(sumnowinvalue)) + "</td>";
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

    function openDetails(relevanceNo) {
        
        var ul = "url:/Lottery/BettingDetail.aspx?betcode=" + relevanceNo;
        //投注详情
        var winHeight = $(window).height();
        var openHeight = winHeight - 100;
     
        if (relevanceNo[0] == "b" || relevanceNo[0] == "i") {
            openHeight = 520;
        } else {
            //追号详情
            ul = "url:/Lottery/CatchDetail.aspx?catchCode=" + relevanceNo;
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
<style>
    .formTable td {
    color: #363636;
    /* background: #fdf7ea; */
    padding: 4px 5px;
    vertical-align: middle;
}
</style>

<!--工具栏-->
<div class="toolbar-wrap">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable xk_input_head">
        <tbody>
            <tr>
                <td rowspan="5" style="padding-left: 5px !important; display:none;">
                    <select name="ordertype" id="ordertype" class="ordertypeS" size="13" multiple="multiple">
                        <option value="-1" selected="">所有</option>
                        <option value="1">用户充值</option>
                        <option value="2">用户提现</option>
                        <option value="3">投注扣款</option>

                        <option value="4">追号扣款</option>
                        <option value="5">追号返款</option>
                        <option value="6">游戏返点</option>

                        <option value="7">奖金派送</option>
                        <option value="8">撤单返款</option>
                        <option value="9">系统撤单</option>

                        <option value="10">撤销返点</option>
                        <option value="11">撤销派奖</option>
                        <option value="12">充值扣费</option>

                        <option value="13">上级充值</option>
                        <option value="14">活动礼金</option>
                     
                        <option value="16">提现失败</option>
                        <option value="17">撤销提现</option>
                        <option value="18">满赠活动</option>

                        <option value="19">签到有你</option>
                        <option value="20">注册活动</option>
                        <option value="21">充值活动</option>
                        <option value="22">佣金大返利</option>
                        <option value="23">幸运大转盘</option>
                        <option value="24">系统充值</option>
                        <option value="25">投注送礼包</option>
                        <option value="99">其他</option>
                    </select>
                </td>
                <td style="text-align:right;">时间：
                </td>
                <td colspan="5">
                    <input id="txtstart" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />&nbsp;至&nbsp;
                    <input id="txtend" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                </td>
               
            </tr>
            <tr>
                 <td style="text-align:right;">类型：</td>
                <td style="width:130px;">
                        <select id="cmbTradeType">
                            <option  value="1">当天交易</option>
                            <option value="2" selected="selected">历史记录</option>
                        </select>
                </td>
                <td style="text-align:right;">用户名：</td><td style="width:130px;"><input type="text" id="userCode" class="input normal" style="width: 120px;" /></td>
                <td style="text-align:right;display:none;">范围：</td><td style="width:130px;display:none;">
                        <select id="cmbUserType" >
                            <option  value="-1">所有范围</option>
                            <option value="1" selected="selected">自己</option>
                            <option value="2">直接下级</option>
                            <option value="3">所有下级</option>
                        </select>
                </td>
               
            </tr>
            <tr>
                <td style="text-align:right;">游戏： </td><td>
                        <asp:DropDownList ID="drpGames" runat="server"></asp:DropDownList>
                </td>
                <td style="text-align:right;display:none;">玩法：</td><td style="display:none;">
                    <select id="selPlayTypes">
                        <option selected="selected" value="-1">所有玩法</option>
                    </select>
                </td>
                <td style="text-align:right;">奖期：</td><td>
                        <select id="selIssue">
                            <option selected="selected" value="">所有奖期</option>
                        </select>
                </td>
                 <td style="text-align:right;display:none;">模式：</td><td style="display:none;">
                    <select id="selModel" >
                        <option selected="selected" value="-1">所有模式</option>
                        <option value="0">元</option>
                        <option value="1">角</option>
                        <option value="2">分</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="display:none;">
                            <select id="cmbCodeType" >
                                <option selected="selected" value="-1">所有</option>
                                <option value="1">注单编号</option>
                                <option value="2">追号编号</option>
                                <option value="3">帐变编号</option>
                            </select>
                        <input type="text" id="txtCode" class="input normal" style="width: 120px;" /></td>
              
            </tr>
            <tr>
                <td style="text-align:right;display:none;">
                    快速查询：
                </td>
            <td colspan="5" style="display:none;">
                 <div class="l-list">
                   <input type="button" class="btn action" id="btnCz" value="我的充值" />
                                <input type="button" class="btn action" id="btnTx" value="我的提现" />
                                <input type="button" class="btn action" id="btnTz" value="我的投注" />
                                <input type="button" class="btn action" id="btnZh" value="我的追号" />
                                <input type="button" class="btn action" id="btnJj" value="我的奖金" />
                                <input type="button" class="btn action" id="btnFd" value="我的返点" />
                </div>
            </td>
            </tr>
            <tr>
                <td align="center" colspan="7">
                    <input name="" id="lbtnSearch" type="button" value="查询" class="formCheck"></td>
            </tr>
        </tbody>
    </table>
</div>
<!--/工具栏-->

<!--列表-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
    <thead>
        <tr>
            <%--<th width="10%">编码</th>--%>
            <th>用户名</th>
            <th >时间</th>
            <th >类型</th>
            <th >游戏</th>
            <%--<th>玩法</th>--%>
            <th>期号</th>
            <%--<th>模式</th>--%>
            <th >收入</th>
            <th>支出</th>
            <th>余额</th>
        </tr>
    </thead>
    <tbody class="ltbody">
        <tr>
            <td align="center" colspan="12">暂无记录</td>
        </tr>
    </tbody>
</table>

<!--/列表-->
<div id="kkpager"></div>
