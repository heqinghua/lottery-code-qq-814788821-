<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerDinpayUTF-8.aspx.cs" Inherits="CSharpTestPay._Default" ResponseEncoding="utf-8"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form action="https://pay.dinpay.com/gateway?input_charset=UTF-8" id="dinpayForm" name="dinpayForm" method="POST" runat="server">

            <input type="hidden" name="sign" id="sign"  runat="server" />

            <input type="hidden" name="merchant_code" id="merchant_code" runat="server" />

            <input type="hidden" name="bank_code" id="bank_code" runat="server" />

            <input type="hidden" name="order_no" id="order_no" runat="server" />

            <input type="hidden" name="order_amount" id="order_amount" runat="server" />

            <input type="hidden" name="service_type" id="service_type" runat="server" />

            <input type="hidden" name="input_charset" id="input_charset" runat="server" />

            <input type="hidden" name="notify_url" id="notify_url" runat="server" />

            <input type="hidden" name="interface_version" id="interface_version" runat="server" />

            <input type="hidden" name="sign_type" id="sign_type" runat="server" />

            <input type="hidden" name="order_time" id="order_time" runat="server" />

            <input type="hidden" name="product_name" id="product_name" runat="server"  />

            <input type="hidden" name="client_ip" id="client_ip" runat="server" />

            <input type="hidden" name="extend_param" id="extend_param" runat="server" />

            <input type="hidden" name="extra_return_param" id="extra_return_param"  runat="server" />

            <input type="hidden" name="product_code" id="product_code" runat="server" />

            <input type="hidden" name="product_desc" id="product_desc" runat="server" />

            <input type="hidden" name="product_num" id="product_num" runat="server" />

            <input type="hidden" name="return_url" id="return_url" runat="server" />

            <input type="hidden" name="show_url" id="show_url" runat="server" />

            <input type="hidden" name="redo_flag" id="redo_flag" runat="server" />

             <input type="hidden" name="pay_type" id="pay_type" runat="server" />

            <script type="text/javascript">
                document.getElementById("dinpayForm").submit();
            </script>
    </form>
</body>
</html>
