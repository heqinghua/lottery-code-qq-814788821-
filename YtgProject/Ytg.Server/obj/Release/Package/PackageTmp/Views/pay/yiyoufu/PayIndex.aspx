<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayIndex.aspx.cs" Inherits="Ytg.ServerWeb.Views.pay.yiyoufu.PayIndex"  MasterPageFile="/Views/ChongZhi/ChongZhi.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <script type="text/javascript">
        var MaxNum = 0;
        $(function () {
            $("#autochongzhi0").addClass("title_active");
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
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">在线充值1</a></li>
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
                    在线直充注意事项：<span style="color:red;">(充值时间：7*24小时自动到账)</span><br>
                    点击下一步根据提示完成支付，支付成功后一定要等待跳转到商家页面或等待自动跳转，显示充值订单成功后再关闭网页，如未自动到账请复制订单编号联系在线客服核查！
                </div>
            </div>
            <form  method="post" name="drawform" id="drawform"  >
                <div style="display: none"><input type="radio" name="bankradio" value="thkpay" id="thkpay" checked="checked"></div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable">
                    <tbody>
                        <tr>
                        </tr>
                        <tr>
                            <td align="right">充值金额：</td>
                            <td>
                                <input type="text" name="amount" id="amount" size="20" autocomplete="off" style="height:22px;">
                                <input  type="hidden" value="<%=Min %>" id="alertmin"/>
                                <input  type="hidden" value="" id="hidbid"/>
                                <span >&nbsp;&nbsp;(单笔充值限额：最低：<span id="loadmin" class="red"> <%=Min %> </span>元，最高：<span id="loadmax" class="red"> <%=Max %></span> 元)</span></td>
                        </tr>
                        <script language="javascript">
                            document.getElementById('amount').focus();
                        </script>
                        <tr>
                            <td align="right">充值金额(大写)：</td>
                            <td><span id="chineseMoney" style="color:red;"></span>
                        </tr>
                    </tbody>
                </table>

                <style>
                    .bank-list li {list-style: none;width: 180px;float: left;margin-bottom: 10px;}
                    .bank-list div {cursor: pointer;}
                    .radio-img-bank {float: left; margin-top:10px;}
                    .radio-img-967 {float: left;width: 154px;height: 33px;}
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
                                <ul class="bank-list" style="margin-top:10px;">
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="967" tag="工商银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/gongshang.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="964" tag="农业银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/nongye.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="970" tag="招商银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/zhaohang.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="965" tag="建设银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/jianshe.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="981" tag="交通银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/jiaotong.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="980" tag="民生银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/minsheng.gif');"></div>
                                    </li>
                                     <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="971" tag="中国邮政储蓄"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/youzheng.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="974" tag="平安银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/pingan.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="963" tag="中国银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/zhongguo.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="962" tag="中信银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/zhongxin.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="972" tag="兴业银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/xingye.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="977" tag="浦发银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/shangpufa.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="986" tag="光大银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/guangda.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="989" tag="北京银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/beijing.gif');"></div>
                                    </li>
                                <%--    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="988" tag="渤海银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/buohai.gif');"></div>
                                    </li>--%>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="985" tag="广东发展银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/guangfa.gif');"></div>
                                    </li>
                               <%--     <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="984" tag="广州市农信社" ><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/guangnongxin.gif');"></div>
                                    </li>--%>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="975" tag="上海银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/shanghaibank.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="968" tag="浙商银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/zheshang.gif');"></div>
                                    </li>
                               <%--     <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="973" tag="顺德农信社"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/shundexin.gif');"></div>
                                    </li>--%>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="982" tag="华夏银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/huaxia.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="979" tag="南京银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/nanjing.gif');"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="990" tag="北京农商银行"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/nongcunshangye.gif');"></div>
                                    </li>
                                     <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="992" tag="支付宝"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/zfb.png') no-repeat;"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="1004" tag="微信支付"><div class="radio-img-967" style="background: url('/Views/pay/yiyoufu/images/weixin.jpg') no-repeat;"></div>
                                    </li>
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
