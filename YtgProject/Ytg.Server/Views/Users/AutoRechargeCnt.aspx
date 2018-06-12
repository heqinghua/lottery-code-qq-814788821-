<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRechargeCnt.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.AutoRechargeCnt" MasterPageFile="/Views/ChongZhi/ChongZhi.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/jquery.zclip.min.js" type="text/javascript"></script>
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
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
            $("#copyuserWinMonery").zclip({
                path: "/Content/Scripts/ZeroClipboard.swf",
                copy: function () {
                    return $("#<%=lbMonery_.ClientID%>").val();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                    $.alert("复制充值金额成功!");
                }
            });

            
            intervalValue = setInterval(setTimes, 1000);
            $.alert("务必将“充值编号”正确填写到银行汇款页面的汇款附言栏中（复制->粘贴[CTRL+V]）,否则充值将无法到账。");
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
                window.location = "/Views/pay/Payment.aspx?det=" + new Date();
            }
        }

    </script>
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
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
   
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">支付宝充值</a></li>
        </ul>
        <form runat="server" id="from1">
             <asp:HiddenField ID="hidecztype" runat="server"/>
            <div class="tab-content" style="font-size:13px;">
                <dl>
                    <dt style="margin-top:10px;">充值银行 :</dt>
                    <dd>
                        <asp:HiddenField ID="hidBankid" runat="server" />
                        <asp:Image ID="imgLogo" runat="server" />
                        <asp:HyperLink ID="bankLink" runat="server" Text="点此进入支付宝充值" Target="_blank" CssClass="banka"></asp:HyperLink>
                        <span style="margin-left: 20px;">页面有效倒计时：</span>
                        <span style="color: red;" id="times">10:00:00</span>
                    </dd>
                </dl>
                <dl>
                    <dt>收款账户名 :</dt>
                    <dd>
                        <asp:TextBox ID="userName" ReadOnly ="true" runat="server" CssClass="input normal" Style="background: #dbd9d9;"></asp:TextBox>
                        <input type="button" id="copyUserName" value="复制" style="margin-left: 10px; height: 22px; width: 60px;" />
                    </dd>
                </dl>
                <dl style="display:none;" class="zfb_info">
                    <dt>收款账号 :</dt>
                    <dd>
                        <asp:TextBox ID="userCode" ReadOnly ="true" runat="server" CssClass="input normal" Style="background: #dbd9d9;"></asp:TextBox>
                        <input type="button" id="copyuserCode" value="复制" style="margin-left: 10px; height: 22px; width: 60px;" />
                    </dd>
                </dl>
                <dl>
                    <dt>充值金额 :</dt>
                    <dd>
                         <asp:TextBox ID="lbMonery_" ReadOnly ="true" runat="server" CssClass="input normal" Style="background: #dbd9d9;"></asp:TextBox>
                        <input type="button" id="copyuserWinMonery" value="复制" style="margin-left: 10px; height: 22px; width: 60px;" />
                    </dd>
                </dl>
                <dl >
                    <dt>附言(充值编号) :</dt>
                    <dd>
                        <asp:TextBox ID="txtNum" runat="server" CssClass="input normal" ReadOnly ="true" Style="background: #dbd9d9;"></asp:TextBox>
                        <input type="button" id="copytxtNum" value="复制" style="margin-left: 10px; height: 22px; width: 60px;" />
                        <span style="color:red;">&nbsp;&nbsp;务必将此充值编号正确填写到转账"备注"里</span>
                    </dd>
                </dl>
                <dl style="display:none;" class="cft_info">
                    <dt>充值二维码 :</dt>
                    <dd>
                        <img src="<%=wxqrcode %>" style="width:200px;"/>
                        <img  src="/views/users/images/wxhelper.png" />
                    </dd>
                </dl>
                <dl style="display:none;" class="zfb_info">
                    <dt>充值二维码 :</dt>
                    <dd>
                        <img src="<%=zbfqrcode %>" style="width:200px;"/>
                    </dd>
                </dl>
               
                <dl style="display:none;" class="zfb_info">
                    <dt>温馨提示 :</dt>
                    <dd class="ctdl" style=" margin-left:20px;">
                        <p>1、请务必复制<span>“充值编号”</span> 准确填写到支付宝转账页面的<span>“备注”</span> 栏中进行粘贴(键盘[CTRL+V])，否则充值将无法到账。</p>
                        <p>2、充值编号为随机生成，一个充值编号只能充值一次，<span>请在页面有效倒计时内完成充值</span>，过期或重复使用充值将无法到账。</p>
                        <p>3、收款账户名和收款E-mail会不定期更换，请在获取最新信息后充值，否则充值将无法到账。</p>
                        <p>4、<span>“充值金额”</span>与支付宝转账金额不符，充值将无法准确到账。</p>
                        <p>5、支付宝每天的充值处理时间为：<span>上午 09:00 至 次日凌晨2:00。</span></p>
                    </dd>
                </dl>
                <dl style="display:none;" class="cft_info">
                    <dt>温馨提示 :</dt>
                    <dd class="ctdl" style=" margin-left:20px;">
                        <p>1、请务必复制<span>“充值编号”</span> 准确填写到微信转账页面的<span>“备注”</span> 栏中进行粘贴(键盘[CTRL+V])，否则充值将无法到账。</p>
                        <p>2、充值编号为随机生成，一个充值编号只能充值一次，<span>请在页面有效倒计时内完成充值</span>，过期或重复使用充值将无法到账。</p>
                        <p>3、收款账户名和收款E-mail以及收款二维码会不定期更换，请在获取最新信息后充值，否则充值将无法到账。</p>
                        <p>4、<span>“充值金额”</span>与支付宝转账金额不符，充值将无法准确到账。</p>
                        <p>5、微信每天的充值处理时间为：<span>上午 09:00 至 次日凌晨2:00。</span></p>
                    </dd>
                </dl>

                <dl style="display:none;" >
                    <dt>充值帮助 :</dt>
                    <dd class="ctdl" style=" margin-left:120px;">
                        <table style="width:100%;">
                            <tr>
                                <td><img  src="iocs/01.png" style="width:100%;"/></td>
                                <td><img  src="iocs/02.png" style="width:100%;"/></td>
                               
                            </tr>
                            <tr>
                                 <td><img  src="iocs/03.png" style="width:100%;"/></td>
                                <td><img  src="iocs/04.png" style="width:100%;"/></td>
                            </tr>
                        </table>
                    </dd>
                </dl>

                <dl>
                    <dt></dt>
                    <dd style=" margin-left:100px;">
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
            <div class="page-footer" style="text-align:center;display:none;">
                <input name="btnSubmit" class="formWord" id="btnSubmit" type="button" value="上一步" onclick="history.go(-1);"></div>
        </form>
    </div>
    </span>
    <script type="text/javascript">
        $(function () {
            
            if ($("#<%=hidecztype.ClientID%>").val() == "zfb") {
                $(".zfb_info").show();
            } else {
                $(".cft_info").show();
            }
        });
    </script>
</asp:Content>
