<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Ytg.ServerWeb.Lottery.GroupBy.Details" %>

<html>
<head>
    <title>合买详情</title>
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/core.css" rel="stylesheet" />
    <link href="css/orderCore.css" rel="stylesheet" />
    <link href="css/groupDetail.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="/Content/Scripts/config.js"></script>
    <script src="/Content/Scripts/basic.js"></script>
    <script src="/Content/Scripts/comm.js"></script>
    <!--消息框代码开始-->
    <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
    <!--头部结束-->
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="js/local.js"></script>
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <script src="../../Content/Scripts/common.js"></script>
    <script src="js/item.js"></script>
    <script src="/Content/Scripts/playname.js"></script>
    <script src="js/hm.js"></script>
</head>
<body>
    <div class="docBody clearfix">

        <section id="mainBody">
            <header class="game_header clearfix">
                <div class="headerBox">
                    <div class="clearfix titleBox">
                        <script>
                            document.write(Show.LogoName('<%=Request.Params["lotteryCode"]%>'))
                        </script>
                        <div class="abstract">
                            <span>发起时间：<label id="occdate"></label></span><span>编号：<label id="betcode"></label></span><strong class="gameperiod"><strong>期号：<label id="issuecode"></label></strong></strong>
                        </div>
                    </div>
                </div>
            </header>
            <div class="topWrap clearfix">
                <div class="userInfoBox-details">
                    <div class="clearfix userInfo">
                        <img style="display: inline; float: left; margin: 0 15px 0 10px; border: 1px solid #C2C2C2; height: 60px; width: 60px;" src="/lottery/groupBy/images/default.png" />
                        <div class="userName">发起人<strong id="usernickname"></strong></div>
                    </div>
                    <ul class="list clearfix">
                        <li><span class="textL">个人战绩：</span>
                            <script>
                                document.write(Show.UserLevel(<%=Request.Params["id"]%>))
                            </script>
                            <em>&nbsp;</em></li>
                        <li style="display: none;"><span class="textL">中奖宣言：</span><strong>这里是中奖福地。</strong></li>
                    </ul>
                </div>
                <div class="scheme">
                    <ul class="list" id="fade">
                    </ul>
                    <ul class="ulTable clearfix">
                        <li><span>总金额</span><strong id="sumtotal">0元</strong></li>
                        <li><span>剩余金额</span><strong id="smpmonery">0.00元</strong></li>
                        <li style="display:none;"><span>盈利佣金</span><strong>0%</strong></li>
                        <li><span>认购金额</span><strong id="rengoumonery">0.00元</strong></li>
                        <li><span>注数</span><strong id="bettotal">0注</strong></li>
                    </ul>
                </div>
                <div id="tzot" style="padding: 10px">

                    <table style="height: 100px" width="100%" border="0" cellspacing="0" cellpadding="0" class="user_table" id="gaopinNumberTable">
                        <thead>
                            <tr>
                                <td width="90%">投注号码</td>
                                <td width="10%">注数</td>
                            </tr>
                        </thead>
                        <tbody id="bodycontentdetail">
                        </tbody>
                    </table>

                </div>
                <div id="paybox" class="paybox" style="display:none;">
                    <p>剩余 <strong class="c_ba2636 symm">0.00</strong> 元 我要买
                        <input style="ime-mode: disabled;" size="5" id="rgInput" name="participantBuyPieces" class="input" onpaste="return false" autocomplete="off">元。</p>
                    <div id="bettsub">
                        <a id="tzBtn" href="javascript:void(0);" rel="nofollow" class="betting_Btn" title="立即投注" tag="<%=Request.Params["id"] %>"></a>
                        <p class="treaty">
                            <input type="checkbox" name="checkbox" id="agreeRule" checked="checked">
                            <label for="agreeRule">我已经阅读并同意<a target="_blank" href="####">《委托投注规则》</a></label>
                        </p>
                    </div>
                </div>
                <div id="paybox_end" class="paybox" style="display:none;">
                        <a  href="javascript:;" rel="nofollow" title="方案已截止" id="end_btn_action"></a>
                </div>
            </div>
            <div class="number_user_wrap">
                <ul class="number_user_tab clearfix" id="numberUserTab">
                    <li class="active"><a href="javascript:void(0);">期号详情</a></li>
                    <li><a href="javascript:void(0);">参与用户</a></li>
                </ul>

                <div id="numberDetail">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="user_table" id="gaopinNumberTable">
                        <thead>
                            <tr>
                                <td width="10%">期号</td>
                                <td width="10%">金额</td>
                                <td width="10%">倍数</td>
                                <td width="10%">开奖号码</td>
                                <td width="10%">奖金</td>
                                <td width="10%">状态</td>
                                <td width="10%">操作</td>
                            </tr>
                        </thead>
                        <tbody id="contentbody">
                        </tbody>
                    </table>
                </div>


                <div class="hide" id="userBox">
                    <p></p>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="user_table" id="userTable">
                        <thead>
                            <tr>
                                <th width="15%">序号</th>
                                <th width="20%">用户名</th>
                                <th width="20%" class="align_right">认购金额（元）</th>
                                <th width="20%" class="align_right">中奖金额（元）</th>
                                <th width="25%">参与时间</th>
                            </tr>
                        </thead>
                        <tbody id="groupbyuserlst">

                        </tbody>
                    </table>
                </div>

            </div>
        </section>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#video_").addClass("cur");
            loaddata();
        });

        function loaddata() {
            $.ajax({
                url: "/Page/Lott/LotteryBetDetail.aspx",
                type: 'post',
                data: "&action=hmitem&id=<%=Request.Params["id"]%>",
                success: function (data) {

                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                      //  console.info(jsonData.Data);
                        $("#occdate").html(jsonData.Data.OccDate);
                        $("#betcode").html(jsonData.Data.BetCode);
                        $("#issuecode").html(jsonData.Data.IssueCode);
                        $("#usernickname").html(subnikenaem(jsonData.Data.NikeName));
                        $("#contentbody").append(createitem(jsonData.Data));

                        if (jsonData.Data.Secrecy == 0) {
                            $("#bodycontentdetail").append("<tr><td>[" + jsonData.Data.PlayTypeName + "-" + jsonData.Data.PlayTypeRadioName + "]" + Ytg.common.LottTool.ShowBetContent(jsonData.Data.BetContent, 1) + "</td><td>" + jsonData.Data.BetCount + "</td></tr>");
                        } else if (jsonData.Data.Secrecy == 1) {
                            //参与可见
                            $("#bodycontentdetail").append("<tr><td colspan='2' style='color:red;'>投注内容仅参与可见</td></tr>");
                        } else if (jsonData.Data.Secrecy == 2) {
                            //保密
                            $("#bodycontentdetail").append("<tr><td colspan='2' style='color:red;'>投注内容保密</td></tr>");
                        }

                        $("#sumtotal").html(decimalCt(Ytg.tools.moneyFormat(jsonData.Data.TotalAmt)) + "元");
                        $("#smpmonery,.symm").html(decimalCt(Ytg.tools.moneyFormat(jsonData.Data.SurplusMonery)) + "元");
                        $("#rgInput").attr("max", jsonData.Data.SurplusMonery);
                        $("#rgInput").attr("placeholder", "剩余" + jsonData.Data.SurplusMonery + "元");
                        $("#rengoumonery").html(decimalCt(Ytg.tools.moneyFormat(jsonData.Data.Subscription)) + "元");
                        $("#bettotal").html(decimalCt(Ytg.tools.moneyFormat(jsonData.Data.BetCount)) + "注");

                        var bdhtm = "<li class=\"caseStatus\">方案状态：<strong>" + Show.BuyStatus(jsonData.Data.Stauts, jsonData.Data.GroupByState) + "</strong></li>";
                        bdhtm += Show.LotPro(jsonData.Data.Bili, jsonData.Data.Stauts, jsonData.Data.GroupByState);
                        bdhtm += "<li>中奖金额：<strong id=\"winmonery\">" + decimalCt(Ytg.tools.moneyFormat(jsonData.Data.WinMoney)) + "元</strong> </li>";
                        $("#fade").append(bdhtm);
                        //end_Btn end_btn_action
                        if (jsonData.Data.Stauts == 3 && jsonData.Data.GroupByState!=1) {
                            $("#paybox").show();
                        } else {
                            $("#paybox_end").show();
                            if (jsonData.Data.Stauts != 3) {
                                $("#end_btn_action").addClass("end_Btn");
                            }
                            else {
                                $("#end_btn_action").addClass("full_Btn");
                            }

                        }
                    }
                }
            });
        }
        function subnikenaem(name) {
           
            if (name.length > 3)
                return name.substring(0, 2);
            return name;
        }

        function createitem(item) {

            var stateStr = "未开奖";
            if (item.Stauts == 1) {
                stateStr = "<span style='color:red;'>已中奖</span>";
            } else if (item.Stauts == 2) {
                stateStr = "<span style='color:#0032b8;'>未中奖</span>";
            } else if (item.Stauts == 4) {
                stateStr = "本人撤单";
            } else if (item.Stauts == 5) {
                stateStr = "系统撤单";
            }

            return "<tr><td>" + item.IssueCode + "</td><td>￥" + decimalCt(Ytg.tools.moneyFormat(item.TotalAmt)) + "</td><td>" + item.Multiple + "倍</td><td>" + (item.OpenResult == null ? "" : item.OpenResult) + "</td><td>" + decimalCt(Ytg.tools.moneyFormat(item.WinMoney)) + "</td><td>" + stateStr + "</td><td>--</td></tr>";
        }

        //获取跟投用户列表
        function loadGroupByUserLst() {
            $.ajax({
                url: "/Page/Lott/LotteryBetDetail.aspx",
                type: 'post',
                data: "&action=groupbyuserlst&bettid=<%=Request.Params["id"]%>",
                success: function (data) {
                    //{"Code":0,"Data":[{"UserId":12456,"BuyTogetherCode":"hB66008C1EEC6C869","NikeName":"六胖","Code":"heladiv3","BetDetailId":2277297,"Subscription":1000.0000,"WinMonery":0.0000,"Stauts":3,"Id":15,"OccDate":"2018-05-05 02:39:33"}],"Page":0,"Total":0,"ErrMsg":"","ResponseTime":"2018-05-06 02:39:12"}
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        $("#groupbyuserlst").children().remove();
                        for (var i = 0; i < jsonData.Data.length; i++) {
                            var item = jsonData.Data[i];
                            var winmoneryStr = "￥0";
                            if (item.WinMonery > 0) {
                                winmoneryStr = "<span style='color:red;'>￥" + item.WinMonery + "</span>";
                            }
                            var temp = " <tr><td>" + (i + 1) + "</td><td>" + subNickName(item.NikeName) + "</td><td>￥" + item.Subscription + "</td><td>" + winmoneryStr + "</td><td>" + item.OccDate + "</td></tr>";
                            $("#groupbyuserlst").append(temp);
                        }
                    }
                }
            });
        }
        function subNickName(nickname) {
            if (nickname.length > 2)
                return nickname + "***";
            else
                return nickname.substring(0, 2)+"***";
        }
    </script>
</body>
</html>
