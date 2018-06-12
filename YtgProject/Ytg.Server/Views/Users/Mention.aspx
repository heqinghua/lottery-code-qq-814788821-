<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mention.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.Mention" MasterPageFile="/Views/ChongZhi/ChongZhi.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/keypad.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.keypad2.js"></script>
    <script type="text/javascript">
        var drpid = '#<%=drpCards.ClientID%>';
        var min = 0;
        var max = 0;
        $(function () {
            min = parseFloat($("#<%=lbMin.ClientID%>").html());
            max = parseFloat($("#<%=lbMax.ClientID%>").html());
            $("#pingtaitixian").addClass("title_active");

            $("#<%=txtoutMonery.ClientID%>").keyup(function () {
                //onkeyup:根据用户输入的资金做检测并自动转换中文大写金额(用于充值和提现)
                //obj:检测对象元素，chineseid:要显示中文大小写金额的ID，maxnum：最大能输入金额
                checkWithdraw(this, "spanMaxNum", max);
            });
            /*
            $(drpid).change(function () {
                var selValue = $(drpid).find("option:selected").val();
                var array = selValue.split(",");
                min = parseFloat(array[1]);
                max = parseFloat(array[2]);
              
            });*/
            //changeCode();
        });
        //function changeCode() {
        //    $("#codeImg").attr("src", "/CheckImage.aspx?tp=recharge&dt=" + new Date());
        //}

        function clientClick() {
            var mid = '#<%=txtoutMonery.ClientID%>';
            var pid = '#<%=txtPwd.ClientID%>';
            var cid = '#code';
            if ($(mid).val() == "") {
                $(mid).select();
                $.alert("请输入提现金额!");
                return false;
            }
            var curM = parseFloat($(mid).val());
            if (curM < min || curM > max) {
                $(mid).select();
                $.alert("单笔提现限额：最低：" + min + " 元，最高： " + max + " 元 !");
                return false;
            }
            if (parseFloat($('#<%=lbMonery.ClientID%>').html()) < curM) {
                $(mid).focus();
                $.alert("提现金额超出余额!");
                return false;
            }
            if (!validateUserPss($(pid).val())) {
                $(pid).select();
                $.alert("请输入正确的资金密码!");
                return false;
            }
            return true;
        }
        $(function () {
            Ytg.common.loading();
            var cldt = setInterval(function () {
                Ytg.common.cloading();
                clearInterval(cldt);
            }, 1000)
        })
    </script>
    <style type="text/css">
        .tab-content dl, .div-content dl {
            clear: both;
            display: block;
            line-height: 25px;
        }

        .bankStyle li {
            list-style: none;
            float: left;
            padding-left: 20px;
        }

            .bankStyle li img {
                padding-left: 10px;
            }

        .ctdl {
            text-align: left;
        }

            .ctdl p span {
                color: #cd0228;
                text-align: left;
            }

        .keypad-trigger {
            padding-top: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div class="control">
        <div style="text-align:center;" runat="server" id="noShowTitle" visible="false">
            <img src="iocs/prompt_sanjiao_icon.png"/>
            <div style="color:red;font-size:16px;font-weight:bold;" id="divInfo" runat="server">提款时间为上午10:00至次日凌晨00:00</div>
        </div>
        <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all" runat="server">
            <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">平台提现</a></li>
            </ul>
            <form runat="server" id="from1">
                <div class="tab-content">
                    <dl>
                        <dt>提示信息 :</dt>
                        <dd class="ctdl">
                            <p style="color: #000;">每天限提<span>&nbsp;5&nbsp;</span>次，今天您已经成功发起了&nbsp;<asp:Label ID="lbCounr" runat="server"></asp:Label>&nbsp;次提现申请</p>
                            <p style="color: #000;">每天的提现处理时间为<span style="font-weight: bold">上午 10:00 至 次日凌晨0:00</span></p>
                            <p style="color: #000;">新绑定的提款银行卡需要绑定时间超过<span>&nbsp;2&nbsp;</span>小时才能正常提款。<span>（新）</span></p>
                        </dd>
                    </dl>
                    <dl>
                        <dt>可提现金额 :</dt>
                        <dd>
                            <asp:Label ID="lbMonery" runat="server" Style="color: #cd0228; font-size: 14px;"></asp:Label>
                        </dd>
                    </dl>
                    <dl>
                        <dt>收款银行卡信息 :</dt>
                        <dd>
                            <asp:DropDownList ID="drpCards" runat="server"></asp:DropDownList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>提现金额 :</dt>
                        <dd>
                            <asp:TextBox ID="txtoutMonery" runat="server" CssClass="input normal" Style="width: 145px;"></asp:TextBox>
                            <span>( 单笔提现限额：最低：</span><asp:Label ID="lbMin" runat="server" Style="color: #cd0228;"></asp:Label>
                            <span>元，最高：</span>
                            <asp:Label ID="lbMax" runat="server" Style="color: #cd0228;"></asp:Label>
                            <span>元 )</span>
                        </dd>
                    </dl>
                    <dl>
                        <dt>提现金额大写 :</dt>
                        <dd>
                            <span id="spanMaxNum" style="color: #cd0228;"></span>
                        </dd>
                    </dl>
                    <dl>
                        <dt style="line-height: 17px;">资金密码 :</dt>
                        <dd>
                            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="password normal input" Style="width: 145px;"></asp:TextBox>
                        </dd>
                    </dl>
                    <%-- <dl style="display:none;">
                        <dt>验证码 :</dt>
                        <dd>
                            <table style="padding: 0px;">
                                <tr>
                                    <td style="padding: 0px; margin: 0px;">
                                        <input id="code" name="code" type="text" class="input normal"  style="width:180px;"/></td>
                                    <td style="padding: 0px; margin: 0px;">
                                        <img width="78" id="codeImg" style="cursor: pointer; height: 28px;" onclick="changeCode();" /></td>
                                </tr>
                            </table>
                        </dd>
                    </dl>--%>
                </div>
                <div class="page-footer" style="text-align: center; margin-top: 20px;">
                    <asp:Button ID="btnSummit" runat="server" CssClass="formWord" Text="确认提交" OnClientClick="return clientClick();" OnClick="btnSummit_Click" />
                </div>
            </form>
        </div>
        </div>
        <script type="text/javascript">
            jQuery(function () {
                if ($("#<%=drpCards.ClientID%>").find("option").length < 1 && <%=MaxShow%>>0) {
                    $.alert("您尚未绑定银行卡，请先绑定银行卡！", 1, function () {
                        window.location = "/Views/Users/BindBankCard.aspx"
                    });
                }

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
</asp:Content>
