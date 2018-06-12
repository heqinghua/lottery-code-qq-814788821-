<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WxPay.aspx.cs" Inherits="CSharpTestPay.wechart._Default" ResponseEncoding="utf-8"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>微信支付</title>
    <link href="css.css" rel="stylesheet" />
    <style type="text/css">
        html,body {
            width:100%;height:100%;margin:0px;
        }
    </style>
</head>
<%--<body onload="sendServer();">--%>
<body style="background:#fff;">


    <div class="logo_dg" style="height:90px;line-height:90px;"><div class="p-w-hd" style="font-size:28px;width:1060px;margin:auto;">二维码支付</div>
      </div>
     <div class="payment" >
        <!-- 微信支付 -->
        <div class="pay-weixin" style="margin:auto;width:500px;margin-top:50px;" >
            
            <div class="p-w-bd" style="position: relative;">
                <div class="j_weixinInfo"style="left: 130px; top: -36px; position: absolute;font-weight:bold;font-size:16px;color:#333;">
                    <p style="height: 20px;">支付方式：微信</p>
                    <p>订单金额：<%=showProce %>元</p>
                </div>
                <div class="p-w-box">
                    <div class="pw-box-hd">
                        <img src="qrcode/<%=fileName %>" style="margin-top:20px;">  <!--此处将二维码显示到浏览器上-->
                    </div>
                    <div  style="color:red;font-size:16px;">
                        <p style="text-align:center;font-weight:bold;margin-right:40px;">请使用微信扫一扫</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="payment-change">
           
        </div>
    </div>
  
</body>
</html>