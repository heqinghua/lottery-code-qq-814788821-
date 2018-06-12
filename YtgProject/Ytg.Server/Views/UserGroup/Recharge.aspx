<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recharge.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.Recharge" MasterPageFile="~/Views/UserGroup/Group.master" %>
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
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
         <link href="/Content/Css/keypad.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.keypad2.js"></script>
    <style type="text/css">
        .tab-content dl dt {height:30px;}
    </style>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">用户充值</a></li>
        </ul>
    </div>
    <form id="form1" runat="server">
        <div class="control" >
            <div class="tab-content" style="margin-left:90px;">
                 <dl>
                    <dt></dt>
                    <dd>
                        
                    </dd>
                </dl>
                <dl>
                    <dt>充值限额 :</dt>
                    <dd>
                        单笔充值最低：<span style="color:red;" runat="server" id="spanBengin">10</span> 元， 最高：<span style="color:red;" id="spanEnd" runat="server">10000</span> 元。
                    </dd>
                </dl>
                <asp:Panel ID="PanelType" runat="server" Visible="false">
                    <dl>
                    <dt >充值类型 :</dt>
                    <dd>
                        <input type="radio" value="0" id="radDefault" name="cq" checked="checked"/><label for="radDefault">普通充值</label>
                        <input type="radio" value="0" id="radlast" name="cq"/><label for="radlast">分红充值</label>
                    </dd>
                   </dl>
                </asp:Panel>
                <dl>
                    <dt >您当前余额 :</dt>
                    <dd>
                        <asp:Label ID="lbMonery" runat="server" ></asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt >充值账户 :</dt>
                    <dd>
                      <asp:Label ID="lbInCode" runat="server"  ></asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt style="padding-top:3px;">充值金额 :</dt>
                    <dd>
                      <input type="text" id="txtInMonery" class=" normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" "  style="width:200px;height:22px;"/><span class="Validform_checktip">*</span>
                    </dd>
                </dl>
                <dl>
                    <dt>充值限额（大写） :</dt>
                    <dd>
                       <span id="chineseMoney" style="color:red;"></span>
                    </dd>
                </dl>
               <dl>
                    <dt style="padding-top:3px;">资金密码 :</dt>
                    <dd>
                      <input type="password" id="txtPassword" class=" normal password" datatype="*1-16" sucmsg=" " text="" nullmsg=" " onpaste="return false"  style="width:200px;height:22px;"/><span class="Validform_checktip">*</span>
                    </dd>
                </dl>
            </div>
             <asp:Button  ID="btnSubmit" runat="server" CssClass="closeButton" Text="确认充值" style="margin-left:-200px;" />
        </div>
     <%--   <div style="text-align:center;display:none;margin-top:100px;font-size:14px;" id="showCompled_div">
            恭喜，充值成功！<span id="ctSpan" style="color:red;font-weight:bold;">5</span>&nbsp;秒后跳转至用户列表！
        </div>--%>
    </form>

<script type="text/javascript">
    var isSubmiting = false;
    $(function () {
        $("#users").addClass("title_active");

        var meMonery = $("#<%=lbMonery.ClientID%>").html();
        $("#<%=lbMonery.ClientID%>").html(decimalCt(Ytg.tools.moneyFormat(meMonery)))

        $("#txtInMonery").keyup(function () {
            //onkeyup:根据用户输入的资金做检测并自动转换中文大写金额(用于充值和提现)
            //obj:检测对象元素，chineseid:要显示中文大小写金额的ID，maxnum：最大能输入金额
            checkWithdraw(this, "chineseMoney", <%=BaseMaxMonery%>);
        });

        $("#<%=btnSubmit.ClientID%>").click(function () {
            if (isSubmiting)
                return;
            if ($("#txtInMonery").val() == "" || isNaN($("#txtInMonery").val())) {
                $("#txtInMonery").focus();
                $.alert("充值金额只能为数字!");
                return false;
            }

            var value = parseFloat($("#txtInMonery").val());
            if (value < 10 || value > <%=BaseMaxMonery%>) {
                $("#txtInMonery").focus();

                $.alert("单笔充值最低：10 元, 最高：<%=BaseMaxMonery%> 元!");
                return false;
            }
            if (value > meMonery) {
                $("#txtInMonery").focus();

                $.alert("充值金额不能超过自身余额!");
                return false;
            }
            if ($("#txtPassword").val() == "" || $("#txtPassword").val().length < 6) {
                $("#txtPassword").focus();

                $.alert("请填写6-16位的资金密码!");
                return false;
            }
            var czpq = "0";
            if ($("#radlast") != undefined && $("#radlast").attr("checked") == "checked") {
                czpq = "1";
            }
            isSubmiting = true;
            //充值
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: "action=recharge&incode=" + $("#<%=lbInCode.ClientID%>").html() + "&inmonery=" + value + "&pwd=" + $("#txtPassword").val() + "&czpq=" + czpq,
                success: function (data) {
                    isSubmiting = false;
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        $("#showCompled_div").show();
                        $.alert("充值成功！", 1, function () {
                            window.location = "/Views/UserGroup/UsersList.aspx?id=<%=Request.QueryString["id"]%>&name=<%=Request.QueryString["name"]%>";
                        })
                        
                    } else if (jsonData.Code == 1004) {
                        //验证资金密码错误
                        $.alert("资金密码验证失败!");
                    } else if (jsonData.Code == 2001) {
                        //
                        $.alert("资金禁用!");
                    } else {

                        $.alert("充值失败，请刷新后重试!");
                    }
                }
            });
            //
            return false;

        });
    });

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
