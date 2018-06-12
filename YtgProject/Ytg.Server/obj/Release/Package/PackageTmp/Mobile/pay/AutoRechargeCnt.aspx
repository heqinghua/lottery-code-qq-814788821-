<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRechargeCnt.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.Users.AutoRechargeCnt" %>

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

    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
    <link href="/Mobile/css/subpage1.css" rel="stylesheet" />
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Mobile/css/style1.css" rel="stylesheet" />
    <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <!--消息框代码开始-->
    <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="../css/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
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

    <script src="/Content/Scripts/jquery.zclip.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#autoChongzhi1").addClass("title_active");

            $("#copyUserName").zclip({
                path: "/Content/Scripts/ZeroClipboard.swf",
                copy: function () {
                    return $("#<%=userName.ClientID%>").val();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                    $.alert("复制收款账户名成功!");
                }
            });

            $("#copyuserCode").zclip({
                path: "/Content/Scripts/ZeroClipboard.swf",
                copy: function () {
                    return $("#<%=userCode.ClientID%>").val();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                    $.alert("复制收款账号成功!");
                }
            });

            $("#copytxtNum").zclip({
                path: "/Content/Scripts/ZeroClipboard.swf",
                copy: function () {
                    return $("#<%=txtNum.ClientID%>").val();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                    $.alert("复制附言(充值编号)成功!");
                }
            });

            intervalValue = setInterval(setTimes, 1000);
            $.alert("务必将“充值编号”正确填写到支付平台汇款页面的汇款附言栏中（复制->粘贴[CTRL+V]）,否则充值将无法到账。");
        });

        var intervalValue;
        var alltimes = 600;
        function setTimes() {
            alltimes--;
            if (alltimes % 60 == 0) {
                $("#times").html((parseInt(alltimes / 60) + ":00"));
            } else {
                var tm = alltimes % 60;
                $("#times").html(parseInt(alltimes / 60) + ":" + (tm < 10 ? "0" + tm : tm));
            }
            if (alltimes <= 0) {
                clearInterval(intervalValue);
                window.location = "/Views/Users/AutoRecharge.aspx?det=" + new Date();
            }
        }

    </script>
    <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css">
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
    <style type="text/css">
        .tab-content dl,.div-content dl{ clear:both; display:block; line-height:25px; }
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
                color: red;
                text-align: left;
            }

        img {
            border: none;
        }

        .banka:link, .banka:visited {
            color: #2a72c5;
            text-decoration: none;
        }

        .banka:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">支付宝充值</h1>
        </div>
    </div>
    <div class="ctParent">
        <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
            <form runat="server" id="from1">
                <asp:HiddenField ID="hidecztype" runat="server" />
                <div class="tab-content" style="font-size: 13px;">
                    <table style="width: 90%; margin: auto;">
                        <tr>
                            <td colspan="2" style="text-align: center; font-size: 16px; font-weight: bold;">
                                <span style="margin-left: 20px;">页面有效倒计时：</span><span style="color: red;" id="times">10:00:00</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 30%;">充值方式：</td>
                            <td>
                                <asp:HiddenField ID="hidBankid" runat="server" />
                                <asp:Image ID="imgLogo" runat="server" />
                                <asp:HyperLink ID="bankLink" runat="server" Text="点此进入支付平台充值" Target="_blank" CssClass="banka" Visible="false"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 30%;">收款账户名：</td>
                            <td>
                                <asp:TextBox ID="userName" ReadOnly="true" runat="server" CssClass="input normal" Style="background: #dbd9d9; width: 65%;"></asp:TextBox>
                                <input type="button" id="copyUserName" value="复制" style="margin-left: 5px; height: 22px; width: 60px;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>
                        <tr style="display: none;" class="zfb_info">
                            <td style="text-align: center; width: 30%;">收款账号：</td>
                            <td>
                                <asp:TextBox ID="userCode" ReadOnly="true" runat="server" CssClass="input normal" Style="background: #dbd9d9; width: 65%;"></asp:TextBox>
                                <input type="button" id="copyuserCode" value="复制" style="margin-left: 5px; height: 22px; width: 60px;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 30%;">附言(充值编号)：</td>
                            <td>
                                <asp:TextBox ID="txtNum" runat="server" CssClass="input normal" ReadOnly="true" Style="background: #dbd9d9; width: 65%;"></asp:TextBox>
                                <input type="button" id="copytxtNum" value="复制" style="margin-left: 5px; height: 22px; width: 60px;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 30%;">充值金额：</td>
                            <td>
                                <asp:Label ID="lbMonery" runat="server" ReadOnly="true" Style="font-weight: bold; color: red;"></asp:Label>
                            </td>
                        </tr>

                        <tr style="display: none;" class="cft_info">
                            <td style="text-align: center; width: 30%;vertical-align:top;">充值二维码：</td>
                            <td>
                                <img src="/views/users/images/wechartmonery.png" style="width: 200px;" />
                                <img src="/views/users/images/wxhelper.png" />
                            </td>
                        </tr>
                        <tr style="display: none;" class="zfb_info">
                            <td style="text-align: center; width: 30%;vertical-align:top;" >充值二维码：</td>
                            <td>
                                <img src="/views/users/images/zfbmonery.png" style="width: 200px;" />
                            </td>
                        </tr>

                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>
                        <tr style="display: none;" class="zfb_info">
                            <td colspan="2" style="text-align: center; color: red;">
                                <strong>温馨提示 :</strong><br />
                                <p>请务必复制" <span>充值编号</span> "准确填写到" <span>支付平台</span> "汇款页面" <span>附言</span> "栏、"，否则充值将无法到账。</p>
                            </td>
                        </tr>
                        <tr style="display: none;" class="cft_info">
                            <td colspan="2" style="text-align: center; color: red;">
                                <strong>温馨提示 :</strong><br />
                                <p>1、请务必复制<span>“充值编号”</span> 准确填写到微信转账页面的<span>“备注”</span> 栏中进行粘贴(键盘[CTRL+V])，否则充值将无法到账。</p>
                                <p>2、充值编号为随机生成，一个充值编号只能充值一次，<span>请在页面有效倒计时内完成充值</span>，过期或重复使用充值将无法到账。</p>
                                <p>3、收款账户名和收款E-mail以及收款二维码会不定期更换，请在获取最新信息后充值，否则充值将无法到账。</p>
                                <p>4、<span>“充值金额”</span>与支付宝转账金额不符，充值将无法准确到账。</p>
                                <p>5、微信每天的充值处理时间为：<span>上午 09:00 至 次日凌晨2:00。</span></p>
                            </td>
                        </tr>

                    </table>

                    <dl style="display: none;">
                        <dt></dt>
                        <dd style="margin-left: 100px;">
                            <ul class="bankStyle">
                                <asp:Repeater ID="rpt" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a target="_blank" href="<%#Eval("BankWebUrl") %>">
                                                <img src="<%# Eval("BankLogo") %>" alt="<%# Eval("BankName") %>" /></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </dd>
                    </dl>
                </div>
                <div class="page-footer" style="text-align: center; display: none;">
                    <br />
                    <input name="btnSubmit" class="formWord" id="btnSubmit" type="button" value="上一步" onclick="history.go(-1);">
                </div>
            </form>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    $(function () {

        if ($("#<%=hidecztype.ClientID%>").val() == "zfb") {
            $(".zfb_info").show();
        } else {
            $(".cft_info").show();
        }
    });
</script>
