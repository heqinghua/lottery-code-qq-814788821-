<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.pay.Payment" %>

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
     <!--头部结束-->
    <link href="../css/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
    <link href="/Mobile/css/subpage1.css" rel="stylesheet" />
     <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Mobile/css/style1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <!--消息框代码开始-->
    <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
    <link href="/Content/Css/subpage1.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.blue.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.plastic.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.round.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.round.plastic.css" rel="stylesheet" />
    <script src="/Content/Scripts/jslider/jshashtable-2.1_src.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/tmpl.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.dependClass-0.1.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/draggable-0.1.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.slider.js" type="text/javascript"></script>
</head>
<body>
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
    <style>
        #point {padding-left:0px;padding:5px;}
        
    </style>
     <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css">
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">充值中心</h1>
        </div>
    </div>

     <div  class="ctParent">
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
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
                <div style="text-align:left;">
                    在线直充注意事项：<span style="color:#cd0228;">(充值时间：7*24小时自动到账)</span><br>
                    点击下一步根据提示完成支付，支付成功后一定要等待跳转到商家页面或等待自动跳转，显示充值订单成功后再关闭网页，如未自动到账请复制订单编号联系在线客服核查！
                </div>
            </div>
            <form  action="/Mobile/pay/AutoRechargeCnt.aspx"  method="post" name="drawform" id="drawform">
                <div style="display: none"><input type="radio" name="bankradio" value="thkpay" id="thkpay" checked="checked"></div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable">
                    <tbody>
                        <tr>
                        </tr>
                        <tr>
                            <td align="right">充值金额：</td>
                            <td>
                                <input type="text" name="amount" id="amount" size="20" autocomplete="off" style="height:22px;border:1px solid #d8d8d8;">
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
                    .bank-list li {list-style: none;width: 48%;float: left;margin-bottom: 10px;}
                    .bank-list div {cursor: pointer;}
                    .radio-img-bank {float: left; margin-top:20px; margin-right:3px;}
                    .radio-img-cft {background: url('/Views/pay/mobao/images/cft.jpg');float: left;width: 130px;height: 52px; border:1px solid #d8d8d8;}
                    .radio-img-zfb {background: url('/mobile/images/146282032466.jpg');float: left;width: 130px;height: 52px; border:1px solid #d8d8d8;}
                    .formTable {border-left:1px solid #d8d8d8;}
                    .radio-img-bank {float: left; margin-top:15px;}
                    .radio-img-abc {background: url('/Views/pay/mobao/images/zggsyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-icbc {background: url('/Views/pay/mobao/images/zsyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-ccb {background: url('/Views/pay/mobao/images/zgjsyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-psbc {background: url('/Views/pay/mobao/images/jtyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-boc {background: url('/Views/pay/mobao/images/zgnyyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-cmbc {background: url('/Views/pay/mobao/images/zgyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-bocom {background: url('/Views/pay/mobao/images/xyyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-spdb {background: url('/Views/pay/mobao/images/pdfzyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-cebbank {background: url('/Views/pay/mobao/images/zxyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-ecitic {background: url('/Views/pay/mobao/images/zggdyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}

                    .radio-img-pingan{background: url('/Views/pay/mobao/images/hxyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-cmbcs{background: url('/Views/pay/mobao/images/yzcxyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}

                    .radio-img-hxb {background: url('/Views/pay/mobao/images/gdfzyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-cgb {background: url('/Views/pay/mobao/images/payh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-sfyh {background: url('/Views/pay/mobao/images/sfyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-cgb {background: url('/Views/pay/mobao/images/gdfzyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-pinan{background: url('/Views/pay/mobao/images/payh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-shenfa{background: url('https://cdnpay.dinpay.com/pay/images/SHB.png');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-huaxia{background: url('/Views/pay/mobao/images/hxyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                     .radio-img-guangda{background: url('/Views/pay/mobao/images/zggdyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                     .radio-img-mingshen{background: url('/Views/pay/mobao/images/zgmsyh.gif');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                     .radio-img-zfb{background: url('/Views/pay/yiyoufu/images/zfb.png');float: left;width: 130px;height: 32px;background-position:center;background-position-x:center;}
                     .radio-img-cft{background: url('/Views/pay/mobao/images/cft.jpg');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                     .radio-img-HZB{background: url('https://cdnpay.dinpay.com/pay/images/HZB.png');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-bj{background: url('https://cdnpay.dinpay.com/pay/images/BOB.png');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-zs{background: url('https://cdnpay.dinpay.com/pay/images/CZB.png');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;}
                    .radio-img-nb{background: url('https://cdnpay.dinpay.com/pay/images/NBB.png');float: left;width: 130px;height: 42px;background-position:center;background-position-x:center;} 
                </style>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable" style="margin:auto;">
                    <tbody>
                        <tr>
                            <td>
                                <h3>请选择支付方式</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul class="bank-list"  style="margin-top:10px;">
                                   
<%--                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="ICBC" tag="工商银行"><div class="radio-img-abc"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CMB" tag="招商银行"><div class="radio-img-icbc"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CCB" tag="建设银行"><div class="radio-img-ccb"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="BCOM" tag="交通银行"><div class="radio-img-psbc"></div>
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
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="ECITIC" tag="中信银行"><div class="radio-img-cebbank"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CEBB" tag="光大银行"><div class="radio-img-guangda"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="HXB" tag="华夏银行"><div class="radio-img-huaxia"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="PSBC" tag="邮政储蓄银行"><div class="radio-img-cmbcs"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="SPABANK"  tag="平安银行"><div class="radio-img-pinan"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="SHB"  tag="上海银行"><div class="radio-img-shenfa"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="GDB"  tag="广发银行"><div class="radio-img-cgb"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="HZB"  tag="杭州银行"><div class="radio-img-HZB"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="BOB"  tag="北京银行"><div class="radio-img-bj"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="CZB"  tag="浙商银行"><div class="radio-img-zs"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="NBB"  tag="宁波银行"><div class="radio-img-nb"></div>
                                    </li>--%>
                                     <li >
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="cft" tag="微信"><div class="radio-img-cft"></div>
                                    </li>
                                    <li>
                                        <input type="radio" name="bankCode" class="radio-img-bank" value="zfb" tag="支付宝"><div class="radio-img-zfb"></div>
                                    </li>
                                </ul>
                               
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                <div style="padding:10px;color:#000;text-align:left;">
							<strong>温馨提示:<strong/><br />
							1、微信充值金额请控制在<%=Min %>~<%=Max %>之间;<br>
							2、<span style="color:red;">更多充值方式请登录电脑版</span>;
			   </div></div>
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
         </div>
</body>
</html>