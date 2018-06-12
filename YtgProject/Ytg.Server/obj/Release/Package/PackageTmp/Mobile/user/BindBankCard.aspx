<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindBankCard.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.user.BindBankCard" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>我的银行卡</title>
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
    <link href="/Mobile/Css/layout.css" rel="stylesheet" />
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
    <link href="../css/dialogUI.css" rel="stylesheet" />
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
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
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
    <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    
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

        $(function () {
            Ytg.common.loading();
            var cldt = setInterval(function () {
                Ytg.common.cloading();
                clearInterval(cldt);
            }, 1000)
        })
    </script>
     <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css" />
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
    <style>
        .formTable th, .formTable td {
            border-left: 0px solid #d8d8d8;
            border-top: 0px solid #d8d8d8;
            line-height: 20px;
        }
        .s_checkpass_td span {
            float: left;
            line-height:30px;
        }
        .bindDiv {
            padding:5px;
            text-align: left;
            margin-left:0px;
        }
            .bindDiv p {
                font-size: 14px;
                padding-left: 20px;
            }
    </style>
</head>
<body>
    <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
               <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">我的银行卡</h1>
        </div>
    </div>
    <div class="ctParent">
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
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
                    <div class="page-footer" style="text-align:center;">
                        <asp:Button ID="btnVd" runat="server" Text="提交验证" CssClass="formWord" OnClick="btnVd_Click" OnClientClick="return subVd();" />
                    </div>
                </div>
            </form>
        </asp:Panel>
        <asp:Panel runat="server" ID="panel" Visible="false">
            <!--内容-->
            <div class="bindDiv">
                <h2 class="subctTitle" style="font-size:14px;font-weight:bold;">使用提示：</h2>
                <p>1, 银行卡绑定成功后, 平台任何区域都<span>不会</span>出现您的完整银行账号, 开户姓名等信息。</p>
                <p>2,每个游戏账号最多绑定<span>5</span>张银行卡, 您已成功绑定<span id="meBindNum">0</span>张。</p>
                <p>3,新绑定的提款银行卡需要绑定时间超过<span>6</span>小时才能正常提款。</p>
                <p>4, 一个账户只能绑定同一个开户人姓名的银行卡。</p>
                <div style="margin-top: 10px; display: none;text-align:center;" id="unlock">
                    <span class="subctTitle">您的银行卡资料已锁定</span>
                    <input type="button" class="formWord" style="margin-left: 5px;" value="解锁定银行卡" id="unlockCard" />
                </div>
                <div style="margin-top: 10px; display: block;text-align:center;" id="lock">
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
                          <%--  <th >操作</th>--%>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
                        <tr>
                            <td align="center" colspan="3">暂无记录</td>
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
                                    var htm = "<tr><td>" + item.BankName + "</td><td>" + item.BankNo + "</td><td>" + item.OccDate + "</td></tr>";//<td><a href='javascript:unBindCardNum(" + item.Id + ");'>解绑</a></td>
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
               
                    window.location = "/Mobile/user/Binding.aspx";
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
        </div>
</body>
</html>
