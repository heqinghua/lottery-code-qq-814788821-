<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mention.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.user.Mention"  %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="../css/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Mobile/Css/style1.css" rel="stylesheet" />
    <link href="/Mobile/Css/subpage1.css" rel="stylesheet" />
    <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <link href="/Content/Css/keypad.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.keypad2.js"></script>
     <script>
         Ytg = $.extend(Ytg, {
            SITENAME: "<%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %>",
            SITEURL: window.location.host,
            RESOURCEURL: "/Content",
            BASEURL: "/",
            SERVICEURL: "",
            NOTEFREQUENCY: 10000
        });
        // Ytg.namespace("Ytg.Lottery.user");
        Ytg.common.user.info = {
            user_id: '<%=CookUserInfo.Id%>',
            username: '<%=CookUserInfo.Code%>',
            nickname: '<%=CookUserInfo.NikeName%>'
        };
    </script>
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
        .tab-content dl dt {
    display: block;
    float: left;
    padding-left:20px;
    width: auto;
    text-align: right;
    color: #333;
    font-weight: bold;
}
    </style>
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css" />
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
    </head>
    <body>
      <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">提现申请</h1>
        </div>
    </div>
        <div class="ctParent">
    <div class="control">
        <div style="text-align:center;" runat="server" id="noShowTitle" visible="false">
            <img src="iocs/prompt_sanjiao_icon.png"/>
            <div style="color:red;font-size:16px;font-weight:bold;" id="divInfo" runat="server">提款时间为上午10:00至次日凌晨02:00</div>
        </div>
        <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all" runat="server">
            
            <form runat="server" id="from1">
                <div class="tab-content">
                    <dl>
                        <dt>提示信息 :</dt>
                        <dd class="ctdl">
                            <p style="color: #000;">每天限提<span>&nbsp;5&nbsp;</span>次，今天您已经成功发起了&nbsp;<asp:Label ID="lbCounr" runat="server"></asp:Label>&nbsp;次提现申请</p>
                            <p style="color: #000;">每天的提现处理时间为<span style="font-weight: bold">上午 10:00 至 次日凌晨2:00</span></p>
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
                        <dt>收款卡信息 :</dt>
                        <dd>
                            <asp:DropDownList ID="drpCards" runat="server"></asp:DropDownList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>&nbsp;&nbsp;&nbsp;&nbsp;提现金额 :</dt>
                        <dd>
                            <asp:TextBox ID="txtoutMonery" runat="server" CssClass="input normal" Style="width: 145px;"></asp:TextBox>                            
                        </dd>
                    </dl>
                    <dl>
                        <dt></dt>
                        <dd>
                             
                            <span>( 单笔提现限额：最低：</span><asp:Label ID="lbMin" runat="server" Style="color: #cd0228;"></asp:Label>
                            <span>元，最高：</span>
                            <asp:Label ID="lbMax" runat="server" Style="color: #cd0228;"></asp:Label>
                            <span>元 )</span>
                        </dd>
                    </dl>
                    <dl>
                        <dt>&nbsp;&nbsp;&nbsp;&nbsp;金额大写 :</dt>
                        <dd>
                            <span id="spanMaxNum" style="color: #cd0228;"></span>
                        </dd>
                    </dl>
                    <dl>
                        <dt style="line-height: 17px;">&nbsp;&nbsp;&nbsp;&nbsp;资金密码 :</dt>
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
            </div>
        <script type="text/javascript">
            jQuery(function () {
                if ($("#<%=drpCards.ClientID%>").find("option").length < 1 && <%=MaxShow%>>0) {
                    $.alert("您尚未绑定银行卡，请先绑定银行卡！", 1, function () {
                        window.location = "/Mobile/user/BindBankCard.aspx"
                    });
                }

            });
        </script>
</body>
</html>
