<%@ Page Title="" Language="C#" MasterPageFile="~/Views/ChongZhi/ChongZhi.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Ytg.ServerWeb.Views.pay.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <script type="text/javascript">
        var MaxNum = 0;
        $(function () {
            $("#autoChongzhi1").addClass("title_active");
            var minAmt = <%=Min%>;
            var maxAmt = <%=Max%>;
            MaxNum = maxAmt;
            $("#amtMin").html(decimalCt(Ytg.tools.moneyFormat(minAmt)));
            $("#amtMax").html(decimalCt(Ytg.tools.moneyFormat(maxAmt)));

            $("#amount").keyup(function () {
                //onkeyup:根据用户输入的资金做检测并自动转换中文大写金额(用于充值和提现)
                //obj:检测对象元素，chineseid:要显示中文大小写金额的ID，maxnum：最大能输入金额
                checkWithdraw(this, "chineseMoney", MaxNum);
            });
        });
        $(function () {
            Ytg.common.loading();
            var cldt = setInterval(function () {
                Ytg.common.cloading();
                clearInterval(cldt);
            }, 1000)
        })
    </script>

</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div style="text-align:center;" runat="server" id="noShowTitle" visible="false">
            <img src="/Views/Users/iocs/prompt_sanjiao_icon.png"/>
            <div style="color:red;font-size:16px;font-weight:bold;">支付宝充值时间为上午09:00至次日凌晨02:00</div>
        </div>
    <div id="tabs" runat="server" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">支付宝充值</a></li>
        </ul>
        <!--内容-->

        <!--消息框代码结束-->
        <script type="text/javascript">

            $(function () {
                $("input[name='bankCode']").click(function () {
                    $("#hidbid").val($(this).val());
                });
                //图片点击事件
                $(".radio-img-bank").parent().find("div").click(function(){
                    var rad=$(this).parent().children().first();
                    rad.attr("checked","checked");
                    $("#hidbid").val(rad.val());
                });

            });
            var result = 0;
            function checkForm() 
            {
                if ($("#amount").val() == "") {
                    $.alert("输入充值金额！");
                    $("#amount").focus();
                    return false;
                }

                var loadmin = document.getElementById("alertmin").value;
                if (parseInt($("#amount").val(), 10) < parseInt(loadmin, 10)) {
                    $.alert("充值金额不能低于最低充值限额！");
                    $("#amount").val(loadmin);
                    $("#chineseMoney").html(changeMoneyToChinese(loadmin));
                    return false;
                }

                if ($("#hidbid").val()=="") {
                    $.alert("请选择充值方式！");
                    return false;
                }
            }
        </script>
        <div id="point">
            <div>
                支付宝充值注意事项：<span class="red">(充值时间：上午9:00 至 次日凌晨2:00)</span><br>
                点击下一步根据提示完成支付，支付成功后, 请手动刷新您的余额及查看相关帐变信息,若超过1分钟未上分,请与客服联系！
            </div>
        </div>
        <form action="/Views/Users/AutoRechargeCnt.aspx" method="post" name="drawform" id="drawform">
            <div style="display: none">
                <input type="radio" name="bankradio" value="thkpay" id="thkpay" checked="checked"></div>
            <div class="tab-content">
                <dl>
                    <dt>充值金额 :</dt>
                    <dd class="ctdl">
                        <input type="text" name="amount" id="amount" size="20" autocomplete="off" style="height: 22px;">
                        <input type="hidden" value="<%=Min %>" id="alertmin" />
                        <input type="hidden" value="" id="hidbid" />
                        <span>&nbsp;&nbsp;(单笔充值限额：最低：<span id="loadmin" class="red"> <%=Min %></span> 元，最高：<span id="loadmax" class="red"> <%=Max %></span> 元)</span>
                    </dd>
                </dl>
                <dl><dt>&nbsp;</dt></dl>
                <dl>
                    <dt>充值金额(大写) :</dt>
                    <dd class="ctdl">
                        <span id="chineseMoney" style="color: #cd0228;"></span>
                    </dd>
                </dl>
                <dl><dt>&nbsp;</dt></dl>
            </div>
            <script language="javascript">
                document.getElementById('amount').focus();
            </script>

            <style>
                .bank-list li {
                    list-style: none;
                    width: 180px;
                    float: left;
                    margin-bottom: 10px;
                }

                .bank-list div {
                    cursor: pointer;
                }

                .radio-img-bank {
                    float: left;
                    margin-top: 20px;
                    margin-right: 3px;
                }

                .radio-img-cft {
                    background: url('/Views/pay/mobao/images/cft.jpg');
                    float: left;
                    width: 130px;
                    height: 52px;
                    border: 1px solid #d8d8d8;
                }

                .radio-img-zfb {
                    background: url('/Views/pay/mobao/images/zfb.jpg');
                    float: left;
                    width: 130px;
                    height: 52px;
                    border: 1px solid #d8d8d8;
                }

                .formTable {
                    border-left: 1px solid #d8d8d8;
                }
            </style>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable">
                <tbody>
                    <tr>
                        <td>
                            <h3>请选择支付方式</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ul class="bank-list" style="margin-top: 10px;">
                                <li >
                                    <input type="radio" name="bankCode" class="radio-img-bank" value="cft" tag="财付通"><div class="radio-img-cft"></div>
                                </li>
                                <li>
                                    <input type="radio" name="bankCode" class="radio-img-bank" value="zfb" tag="支付宝"><div class="radio-img-zfb"></div>
                                </li>
                            </ul>

                        </td>
                    </tr>
                </tbody>
            </table>

            <div id="2tips" data="0"></div>
            <div align="center">
                <input id="btnSubmit" type="submit" class="formNext" value="下一步" onclick="return checkForm(this);" />
                <input name="" type="button" value="返回" class="formBack" onclick="history.back(-1);">
            </div>

        </form>

        <script>
            try {
                changeInfo(document.getElementById("thkpay"));
                checkemailWithdraw(document.getElementById("amount"), 'chineseMoney', 0);
            } catch (e) { }
        </script>


        ﻿<div style="clear: both"></div>
        <script>
            function setHeight() {
                var _height = $("body").height() < 520 ? 520 : $("body").height();
                $('#mainFrame', parent.document).height(_height);
            }
            window.onload = function () {
                setHeight();
                if ($('#contentBox', parent.document).length > 0) {
                    $('#contentBox', parent.document).scrollTop(0);
                }
            }
        </script>
    </div>
</asp:Content>
