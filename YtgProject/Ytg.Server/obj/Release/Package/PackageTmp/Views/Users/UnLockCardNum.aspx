<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnLockCardNum.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.UnLockCardNum" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
      <link href="/Content/Css/keypad.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.keypad2.js"></script>
      <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <style>
         .keypad-trigger {padding-top:3px;}
    </style>
</head>
<body>
    <form id="form1" runat="server"  method="post">
        <input type="hidden" name="type" value="1"/>
        <input type="hidden" name="drpBanks_n" id="drpBanks_n" value=""/>
        <input type="hidden" name="drpPro_n" id="drpPro_n" value=""/>
        <input type="hidden" name="drpCity_n" id="drpCity_n" value=""/>
        <div class="control">
            <div class="bindDiv" id="bindfst" runat="server">
                 <div style="height:30px;"></div>
                <h2 class="subctTitle">使用提示：</h2>
                <p>每次锁定银行卡后，只可解锁 <span>1</span>次，请正确填写已绑定在平台的所有银行卡信息。</p>        
                <div style="height:10px;"></div>        
            </div>
            <div class="tab-content" style="border-top: 1px solid #e1e1e1;text-align:center;">
                <div style="width:700px;margin:auto;">
                    <table class="vdtable">
                         <%=BindCardNums %>
                        <tr>
                            <td class="lsrd">资金密码 :</td>
                            <td colspan="3"><asp:TextBox ID="txtpwd" runat="server" TextMode="Password" CssClass="password normal input" style="width:150px;"></asp:TextBox><span class="Validform_checktip">*</span></td>
                            
                        </tr>
                    </table>
                  
                   
                    <input type="button" id="btnNext" value="解除锁定" class="formWord"  style="margin:auto;"/>
                    <asp:Button  ID="btnSubmit" runat="server" style="display:none;" OnClick="btnSubmit_Click"/>
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>
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

    $(function () {

        $("#btnNext").click(function () {
            var isBankOwner = true;
            $("input[name=BankOwner]").each(function () {
                    if ($.trim($(this).val()) == "" && isBankOwner) {
                        $(this).focus();
                        $.alert("请输入开户名!", 1.5, '32X32/succ.png', function () { });
                        isBankOwner = false;
                    }
            });
            if (!isBankOwner)
                return;
            var iscardNum = true;
            $("input[name='cardNum']").each(function () {
                if ($.trim($(this).val()) == "" && iscardNum) {
                    $(this).focus();
                    $.alert("请输入账号!", 1.5, '32X32/succ.png', function () { });
                    iscardNum = false;
                    return;
                }
            });
            if(!iscardNum)
            return;

            if ($.trim($("#txtpwd").val()) == "") {
                $("#txtpwd").focus();
                $.alert("请输入资金密码!", 1.5, '32X32/succ.png', function () { });
                return;
            }
            if (!validateUserPss($("#txtpwd").val())) {
                $("#txtpwd").focus();
                $.alert("请输入正确的资金密码!", 1.5, '32X32/succ.png', function () { });
                return;
            }

            $.dialog.confirm("确定要解除锁定吗？", function () {
                $("#btnSubmit").click();
            }, function () {

            });
            return;
        });

    });
</script>
