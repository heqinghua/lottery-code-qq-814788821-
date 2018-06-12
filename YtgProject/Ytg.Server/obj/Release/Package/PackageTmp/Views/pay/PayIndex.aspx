<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayIndex.aspx.cs" Inherits="Ytg.ServerWeb.Views.pay.PayIndex" MasterPageFile="/Views/ChongZhi/ChongZhi.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <script type="text/javascript">
        var MaxNum = 0;
        $(function () {
            $("#autoChongzhi").addClass("title_active");
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
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">在线充值</a></li>
        </ul>
        <!--内容-->

        <form runat="server" id="form1">

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
                function checkForm() {
                    if ($("#hidbid").val()=="") {
                        $.alert("请选择充值银行");
                        return false;
                    }
                   
                    if ($("#amount").val() == "") {
                        $.alert("输入金额超出了最高限额！");
                        $("#amount").focus();
                        return false;
                    }

                    var loadmin = document.getElementById("alertmin").value;
                    if (parseInt($("#amount").val(), 10) < parseInt(loadmin, 10)) {
                        $.alert("充值金额不能低于最低充值限额 ");
                        $("#amount").val(loadmin);
                        $("#chineseMoney").html(changeMoneyToChinese(loadmin));
                        return false;
                    }
                }
            </script>
            <div id="point">
                <div>
                    在线直充注意事项：<span style="color:#cd0228;">(充值时间：7*24小时自动到账)</span><br>
                    点击下一步根据提示完成支付，支付成功后一定要等待跳转到商家页面或等待自动跳转，显示充值订单成功后再关闭网页，如未自动到账请复制订单编号联系在线客服核查！
                </div>
            </div>
            <form  method="post" name="drawform" id="drawform"  >
                <div style="display: none"><input type="radio" name="bankradio" value="thkpay" id="thkpay" checked="checked"></div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable">
                    <tbody>
                        <tr></tr>
                        <tr>
                            <td align="right">充值金额：</td>
                            <td>
                                <input type="text" name="amount" id="amount" size="20" autocomplete="off" style="height:22px;">
                                <input  type="hidden" value="<%=Min %>" id="alertmin"/>
                                <input  type="hidden" value="" id="hidbid"/>
                                <span >&nbsp;&nbsp;(单笔充值限额：最低：<span id="loadmin" class="red"> <%=Min %></span> 元，最高：<span id="loadmax" class="red"> <%=Max %></span> 元)</span></td>
                        </tr>
                        <script language="javascript">
                            document.getElementById('amount').focus();
                        </script>
                        <tr>
                            <td align="right">充值金额(大写)：</td>
                            <td><span id="chineseMoney" style="color:#cd0228;"></span>
                        </tr>
                    </tbody>
                </table>

                <style>
                    .bank-list li {list-style: none;width: 180px;float: left;margin-bottom: 10px;}
                    .bank-list div {cursor: pointer;}
                    .radio-img-bank {float: left; margin-top:20px;}
                    .radio-img-abc {background: url('/Views/pay/mobao/images/zggsyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-icbc {background: url('/Views/pay/mobao/images/zsyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-ccb {background: url('/Views/pay/mobao/images/zgjsyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-psbc {background: url('/Views/pay/mobao/images/jtyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-boc {background: url('/Views/pay/mobao/images/zgnyyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-cmbc {background: url('/Views/pay/mobao/images/zgyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-bocom {background: url('/Views/pay/mobao/images/xyyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-spdb {background: url('/Views/pay/mobao/images/pdfzyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-cebbank {background: url('/Views/pay/mobao/images/zxyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-ecitic {background: url('/Views/pay/mobao/images/zggdyh.gif');float: left;width: 130px;height: 52px;}

                    .radio-img-pingan{background: url('/Views/pay/mobao/images/hxyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-cmbcs{background: url('/Views/pay/mobao/images/yzcxyh.gif');float: left;width: 130px;height: 52px;}

                    .radio-img-hxb {background: url('/Views/pay/mobao/images/gdfzyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-cgb {background: url('/Views/pay/mobao/images/payh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-sfyh {background: url('/Views/pay/mobao/images/sfyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-cgb {background: url('/Views/pay/mobao/images/gdfzyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-pinan{background: url('/Views/pay/mobao/images/payh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-shenfa{background: url('/Views/pay/mobao/images/sfyh.gif');float: left;width: 130px;height: 52px;}
                    .radio-img-huaxia{background: url('/Views/pay/mobao/images/hxyh.gif');float: left;width: 130px;height: 52px;}
                     .radio-img-guangda{background: url('/Views/pay/mobao/images/zggdyh.gif');float: left;width: 130px;height: 52px;}
                     .radio-img-mingshen{background: url('/Views/pay/mobao/images/zgmsyh.gif');float: left;width: 130px;height: 52px;}
                     .radio-img-zfb{background: url('/Views/pay/yiyoufu/images/zfb.png');float: left;width: 130px;height: 32px;}
                     

                    .formTable {border-left:1px solid #d8d8d8;}
                </style>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable">
                    <tbody>
                        <tr>
                            <td>
                                <h3>请选择支付银行</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul class="bank-list"  style="margin-top:10px;">
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="ICBC" tag="工商银行"><div class="radio-img-abc"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CMB" tag="招商银行"><div class="radio-img-icbc"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CCB" tag="建设银行"><div class="radio-img-ccb"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="COMM" tag="交通银行"><div class="radio-img-psbc"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="ABC" tag="农业银行"><div class="radio-img-boc"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="BOC" tag="中国银行"><div class="radio-img-cmbc"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CIB" tag="兴业银行"><div class="radio-img-bocom"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="SPDB" tag="浦发银行"><div class="radio-img-spdb"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CMBC" tag="民生银行"><div class="radio-img-mingshen"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CNCB" tag="中信银行"><div class="radio-img-cebbank"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CEB" tag="光大银行"><div class="radio-img-guangda"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="HXB" tag="华夏银行"><div class="radio-img-huaxia"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="PSBC" tag="邮政储蓄银行"><div class="radio-img-cmbcs"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="PAB"  tag="平安银行"><div class="radio-img-pinan"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="SDB"  tag="深发银行"><div class="radio-img-shenfa"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="GDB"  tag="广发银行"><div class="radio-img-cgb"></div>
                                    </li>
                                 <%--    <li >
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="ZFB"  tag="支付宝"><div class="radio-img-zfb" style="margin-top:15px;"></div>
                                    </li>--%>
                                </ul>
                               
                            </td>
                        </tr>
                    </tbody>
                </table>

                <div id="2tips" data="0"></div>
                <div align="center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="formNext" Text="下一步" OnClientClick="return checkForm(this);" OnClick="btnSubmit_Click"/>
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
        </form>
    </div>
</asp:Content>
