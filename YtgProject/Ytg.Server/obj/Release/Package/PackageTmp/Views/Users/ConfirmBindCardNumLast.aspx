<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmBindCardNumLast.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.ConfirmBindCardNumLast" EnableEventValidation="false" EnableViewStateMac= "false"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
      <link href="/Content/Css/keypad.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.keypad2.js"></script>
    <style type="text/css">
        #confirm span {font-size:14px;color:red;}
        .keypad-trigger {padding-top:3px;}
        .lsrd {float:right;font-weight:bold;}
        .vdtable tr td {height:28px;line-height:28px;}
    </style>
    <script type="text/javascript">
        jQuery(function () {

            jQuery("input.password").keypad({
                layout: [
                        $.keypad.SPACE + $.keypad.SPACE + $.keypad.SPACE + '1234567890',
                        'cdefghijklmab',
                                        "stuvwxyznopqr"/*+ $.keypad.CLEAR*/,
                                        $.keypad.SPACE + $.keypad.SPACE + $.keypad.SHIFT + $.keypad.CLEAR + $.keypad.BACK + $.keypad.CLOSE
                ],
                // 软键盘按键布局 
                buttonImage: '/content/images/skin/kb1.png',	// 弹出(关闭)软键盘按钮图片地址
                buttonImageOnly: true,	// True 表示已图片形式显示, false 表示已按钮形式显示
                buttonStatus: '打开/关闭软键盘', // 打开/关闭软键盘按钮说明文字
                showOn: 'button', // 'focus'表示已输入框焦点弹出, 
                // 'button'通过按钮点击弹出,或者 'both' 表示两者都可以弹出 

                keypadOnly: false, // True 表示只接受软件盘输入, false 表示可以通过键盘和软键盘输入  

                randomiseNumeric: true, // True 表示对所以数字位置进行随机排列, false 不随机排列
                randomiseAlphabetic: true, // True 表示对字母进行随机排列, false 不随机排列 

                clearText: '清空', // Display text for clear link 
                clearStatus: '', // Status text for clear l

                shiftText: '大小写', // SHIFT 按键功能的键的显示文字 
                shiftStatus: '转换字母大小写', // SHIFT按键功能的TITLE说明文字 

                closeText: '关闭', // 关闭按键功能的显示文字 
                closeStatus: '关闭软键盘', // 关闭按键功能的TITLE说明文字 

                backText: '退格', // 退格功能键的显示文字 
                backStatus: '退格', // 退格功能键的说明文字

                onClose: null	// 点击软键盘关闭是调用的函数
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="control">
            <div class="bindDiv" >
                <div style="height:30px;"></div>
                <h2 class="subctTitle">使用提示：</h2>
                <p>请仔细确认以下信息后, 点击 <span style="color:red;">“立即绑定”</span> 按钮。</p>
               <div style="height:10px;"></div>
            </div>
            <div class="tab-content" style="border-top: 1px solid #e1e1e1;">
                <div id="confirm" style="margin:auto;width:400px;">
                    <table style="width:300px;margin:auto;" class="vdtable">
                        <tr>
                            <td class="lsrd">开户银行 :</td>
                            <td><asp:Label CssClass="Validform_checktip" id="bankspan" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="lsrd">开户银行省份 :</td>
                            <td><asp:Label CssClass="Validform_checktip" id="opspan" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="lsrd">开户银行市 :</td>
                            <td><asp:Label CssClass="Validform_checktip" id="opscity" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="confirmzhih" runat="server">
                            <td class="lsrd">开户人支行 :</td>
                            <td><asp:Label CssClass="Validform_checktip" id="opszhihang" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="lsrd">开户人姓名 :</td>
                            <td><asp:Label CssClass="Validform_checktip" id="opsname" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="lsrd">银行账号 :</td>
                            <td><asp:Label CssClass="Validform_checktip" id="opsNo" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="lsrd">资金密码 :</td>
                            <td><asp:TextBox ID="txtZjPwd" TextMode="Password" runat="server" CssClass="password normal" style="margin-left:8px;" onpaste="return false"></asp:TextBox> <span class="Validform_checktip">*</span></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="formWord" Text="立即绑定" Style="margin-top:30px;margin-left:30px;" OnClick="btnSubmit_Click" />
                    <input type="button" value="返回" id="bankback" class="formReset" style="margin-left: 20px;margin-top:30px;" onclick="window.location = '/Views/Users/ConfirmBindCardNum.aspx?<%=BackParam%>'"/>
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
       
        $("#btnSubmit").click(function () {
            
            var zjmi = $("#txtZjPwd").val();
            if ($.trim(zjmi) == "") {
                $("#txtZjPwd").focus();
                $.alert("资金密码不能为空!");
                return false;
            }
            return true;
        });
    });
</script>
