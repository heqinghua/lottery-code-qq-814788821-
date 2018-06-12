<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindBankCard.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.BindBankCard" MasterPageFile="/Views/Users/Users.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
     <link href="/Content/Css/subpage.css" rel="stylesheet" />
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
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/keypad.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.keypad2.js"></script>

    <script type="text/javascript">
        $(function () {
            $("#bindCard").addClass("title_active");
        });
        function subVd() {
            var id = "<%=oldpwd.ClientID%>"
            var pwd = $("#" + id).val();
            if ($.trim(pwd) == "") {
                $("#" + id).focus();
                $.alert("请输入资金密码!");
                return false;
            }
            if (!validateUserPss(pwd)) {
                $("#" + id).select();
                $.alert("请输入正确的资金密码!");
                return false;
            }
            return true;
        }

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
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">我的银行卡</a></li>
        </ul>
        <asp:Panel ID="panelPwd" runat="server">
            <form id="form1" runat="server">
                <div class="tab-content">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable">
                    <tbody>
                        <tr>
                            <td class="s_checkpass_td"><span>请输入资金密码：</span><asp:TextBox ID="oldpwd" CssClass="password normal" runat="server" TextMode="Password" ></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
                    <div class="page-footer" style="text-align: left; margin-left: 280px;">
                        <asp:Button ID="btnVd" runat="server" Text="提交验证" CssClass="formWord" OnClick="btnVd_Click" OnClientClick="return subVd();" />
                    </div>
                </div>
            </form>
        </asp:Panel>
        <asp:Panel runat="server" ID="panel" Visible="false">
            <!--内容-->
            <div class="bindDiv">
                <h2 class="subctTitle">使用提示：</h2>
                <p>1, 银行卡绑定成功后, 平台任何区域都<span>不会</span>出现您的完整银行账号, 开户姓名等信息。</p>
                <p>2,每个游戏账号最多绑定<span>5</span>张银行卡, 您已成功绑定<span id="meBindNum">0</span>张。</p>
                <p>3,新绑定的提款银行卡需要绑定时间超过<span>2</span>小时才能正常提款。</p>
                <p>4, 一个账户只能绑定同一个开户人姓名的银行卡。</p>
                <div style="margin-top: 10px; display: none;" id="unlock">
                    <span class="subctTitle">您的银行卡资料已锁定</span>
                    <input type="button" class="formWord" style="margin-left: 5px;" value="解锁定银行卡" id="unlockCard" />
                </div>
                <div style="margin-top: 10px; display: block;" id="lock">
                    <input type="button" class="formWord" style="margin-left: 5px;" value="绑定银行卡" id="bandCard" />
                    <input type="button" class="formWord" style="margin-left: 5px;" value="锁定银行卡" id="lockCard" />
                </div>
            </div>

            <div style="margin-top: 20px;">
                <!--列表-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
                    <thead>
                        <tr>
                            <th>银行名称</th>
                            <th >卡号</th>
                            <th>绑定时间</th>
                            <th >操作</th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
                        <tr>
                            <td align="center" colspan="10">暂无记录</td>
                        </tr>
                    </tbody>
                </table>
                <!--/列表-->
            </div>
            <script type="text/javascript">
                $(function () {
                    $("#bandCard").click(function () {
                        BindCardNum();
                    });
                    $("#lockCard").click(function () {
                        lockCard();
                    });
                    $("#unlockCard").click(function () {
                        unlockCard();
                    });
                });

                function loaddata() {
                    Ytg.common.loading();
                    $.ajax({
                        url: "/Page/Bank/Bank.aspx",
                        type: 'post',
                        data: "action=getuserbanks",
                        success: function (data) {

                            Ytg.common.cloading();
                            var jsonData = JSON.parse(data);
                            //清除
                            $(".ltbody").children().remove();
                            if (jsonData.Code == 0 && jsonData.Data.length > 0) {

                                var islock = jsonData.ErrMsg.toLocaleLowerCase() == "true";
                                for (var c = 0; c < jsonData.Data.length; c++) {
                                    var item = jsonData.Data[c];
                                    var htm = "<tr><td>" + item.BankName + "</td><td>" + item.BankNo + "</td><td>" + item.OccDate + "</td><td><a href='javascript:unBindCardNum(" + item.Id + ");'>解绑</a></td></tr>"
                                    $(".ltbody").append(htm);
                                }
                                $("#meBindNum").html(jsonData.Data.length);
                                //是否锁定
                                if (islock) {
                                    $("#lock").hide();
                                    $("#unlock").show();
                                } else {
                                    $("#lock").show();
                                    $("#unlock").hide();
                                }
                            } else {
                                $(".ltbody").Empty(9);
                            }
                        }
                    });
                }


                function BindCardNum() {

                    if (parseInt($("#meBindNum").html()) >= 5) {
                        $.alert("您已经绑定" + $("#meBindNum").html() + "张卡，无法继续绑定!");
                        return;
                    }
                    $.dialog({
                        id: 'selectIco',
                        fixed: true,
                        lock: true,
                        max: false,
                        min: false,
                        title: "验证银行卡",
                        content: "url:/Views/Users/Binding.aspx",
                        width: 600,
                        height: 400
                    });
                }

                function unBindCardNum(bingdCardId) {
                    $.alert("只能新增银行卡，不能解绑银行卡！");
                }

                function lockCard() {
                    $.dialog({
                        id: 'lockCard',
                        fixed: true,
                        lock: true,
                        max: false,
                        min: false,
                        title: "锁定银行卡",
                        content: "url:/Views/Users/LockCardNum.aspx",
                        width:650,
                        height:400
                    });
                }
                function unlockCard() {
                    $.dialog({
                        id: 'unlockCard',
                        fixed: true,
                        lock: true,
                        max: false,
                        min: false,
                        title: "解锁银行卡",
                        content: "url:/Views/Users/UnLockCardNum.aspx",
                        width: 800
                    });
                }
            </script>
        </asp:Panel>
    </div>
    <asp:Panel ID="panelSc" runat="server" Visible="false">
        <script type="text/javascript">
            $(function () { loaddata(); });
        </script>
    </asp:Panel>
</asp:Content>
