<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountCenter.aspx.cs" Inherits="Ytg.ServerWeb.AccountCenter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="Css/help.css" rel="stylesheet" />
    <link href="Css/all.css" rel="stylesheet" />
    <script src="Css/js/common.js"></script>
    <script src="Css/js/jquery.js"></script>
    <script src="Css/js/message.js"></script>
    <script language='JavaScript'>function ResumeError() { return true; } window.onerror = ResumeError; </script>
    <script language="javascipt" type="text/javascript">
        ; (function ($) {
            $(document).ready(function () {
                $("span[id^='general_tab_']", "#tabbar-div-s2").click(function () {
                    $k = $(this).attr("id").replace("general_tab_", "");
                    $k = parseInt($k, 10);
                    $("span[id^='general_tab_']", "#tabbar-div-s2").attr("class", "tab-back");
                    $("div[id^='general_txt_']").hide();
                    $(this).attr("class", "tab-front");
                    $("#general_txt_" + $k).show();
                    $("span[id^='tabbar_tab_" + $k + "_']:first").click();
                });
                $("span[id^='tabbar_tab_']").click(function () {
                    $z = $(this).attr("id").replace("tabbar_tab_", "");
                    $("span[id^='tabbar_tab_']").attr("class", "tab-back");
                    $("table[id^='tabbar_txt_']").hide();
                    $(this).attr("class", "tab-front");
                    $("#tabbar_txt_" + $z).show();
                });
                $("span[id^='general_tab_']:first", "#tabbar-div-s2").click();
            });
        })(jQuery);
</script>
</head>
<body>
    
    <form id="form1" runat="server">
        <div class="top_menu">
            <div class="tm_left"></div>
            <div class="tm_title"></div>
            <div class="tm_right"></div>
            <div class="tm_menu">
                <a class="act" href="/users_info.shtml?check=">奖金详情</a>
                <a href="/users_message.shtml">我的消息</a>
                <a href="/account_banks.shtml?check=">我的银行卡</a>
                <a href="/account_update.shtml?check=">修改密码</a>
            </div>
        </div>
        <div class="rc_con betting">
            <div class="rc_con_lt"></div>
            <div class="rc_con_rt"></div>
            <div class="rc_con_lb userpoint"></div>
            <div class="rc_con_rb userpoint"></div>
            <h5>
                <div class="rc_con_title">奖金详情</div>
            </h5>
            <div class="rc_con_to">
                <div class="rc_con_ti">
                    <div class="rc_m_til">用户基本信息</div>
                    <div class="betting_input">
                        <table class="user_infc" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="100" align="right" class="n1">账号：</td>
                                <td class="c_bl"><asp:Label ID="Account" runat="server"></asp:Label></td>
                                <td width="100" align="right" class="n1">昵称：</td>
                                <td class="c_bl"><asp:Label ID="UserName" runat="server"></asp:Label></td>
                                <td width="100" align="right" class="n1">奖金限额：</th>
                                <td class="c_bl"><font color="#FF3300">高频彩：</font>400000元&nbsp;&nbsp;&nbsp;&nbsp;<font color="#8e3736">低频彩：</font>100000元</td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear"></div>
                    <div class="rc_list">
                        <div class="rl_list">
                            <table width='100%' cellspacing="0" cellpadding="0" class="tab_title">
                                <tr>
                                    <td id="tabbar-div-s2">
                                        <span class="tab-back" id="general_tab_1" title='重庆时时彩 (CQSSC)' alt='重庆时时彩 (CQSSC)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">重庆</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_6" title='黑龙江时时彩(HLJSSC)' alt='黑龙江时时彩(HLJSSC)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">黑龙江</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_5" title='新疆时时彩(XJSSC)' alt='新疆时时彩(XJSSC)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">新疆</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_4" title='上海时时乐(SSL)' alt='上海时时乐(SSL)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">时时乐</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_2" title='十一运夺金 (山东11选5,SD11-5)' alt='十一运夺金 (山东11选5,SD11-5)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">十一运</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_8" title='多乐彩(JX115)' alt='多乐彩(JX115)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">多乐彩</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_9" title='广东十一选五(GD115)' alt='广东十一选五(GD115)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">广东</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_10" title='重庆十一选五(CQ115)' alt='重庆十一选五(CQ115)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">重庆11X5</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_3" title='北京快乐八(BJKL8)' alt='北京快乐八(BJKL8)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">北京</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_11" title='福彩3D(3D)' alt='福彩3D(3D)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">3D</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_12" title='排列三、五(p3p5)' alt='排列三、五(p3p5)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">P3</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                        <span class="tab-back" id="general_tab_14" title='天津时时彩(TJSSC)' alt='天津时时彩(TJSSC)'>
                                            <span class="tabbar-left"></span>
                                            <span class="content">天津</span>
                                            <span class="tabbar-right"></span>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="width:100%;height:1600px">
            <iframe src="Content.aspx" width="100%" height="100%"></iframe>
        </div>
    </form>
</body>
</html>
