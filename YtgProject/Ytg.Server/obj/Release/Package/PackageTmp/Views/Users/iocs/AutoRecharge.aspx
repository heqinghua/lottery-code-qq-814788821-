<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRecharge.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.AutoRecharge" MasterPageFile="/Views/ChongZhi/ChongZhi.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <script type="text/javascript">
        var MaxNum = 0;
        $(function () {
            $("#autoChongzhi").addClass("title_active");
            var fs = $(".bankStyle").children().eq(0).find("input[type=radio]");
            fs.attr("checked", "checked")
            var minAmt = fs.attr("MinAmt");
            var maxAmt = fs.attr("MaxAmt");
            MaxNum = maxAmt;
            $("#amtMin").html(decimalCt(Ytg.tools.moneyFormat(minAmt)));
            $("#amtMax").html(decimalCt(Ytg.tools.moneyFormat(maxAmt)));
            $("#<%=hidLogo.ClientID%>").val(fs.attr("Logo"));
            changeCode();

            $("#monery").keyup(function () {
                //onkeyup:根据用户输入的资金做检测并自动转换中文大写金额(用于充值和提现)
                //obj:检测对象元素，chineseid:要显示中文大小写金额的ID，maxnum：最大能输入金额
                checkWithdraw(this, "spanMaxNum", MaxNum);
            });
            $("input[name=radBank]").click(function () {
                $("#<%=hidLogo.ClientID%>").val($(this).attr("Logo"));
            });

        });
        function changeCode() {
            $("#codeImg").attr("src", "/CheckImage.aspx?tp=recharge&dt=" + new Date());
        }
        function imgClick(bid) {
            $("#bk_" + bid).attr("checked", "checked");
            var minAmt = $("#bk_" + bid).attr("MinAmt");
            var maxAmt = $("#bk_" + bid).attr("MaxAmt");
            MaxNum = maxAmt;
            $("#amtMin").html(decimalCt(Ytg.tools.moneyFormat(minAmt)));
            $("#amtMax").html(decimalCt(Ytg.tools.moneyFormat(maxAmt)));
            $("#<%=hidLogo.ClientID%>").val($("#bk_" + bid).attr("Logo"));
        }

        function showDialog() {
            $.dialog({
                id: 'showDialog',
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "充值",
                content: "url:/Views/Users/LockCardNum.aspx",
                width: 750
            });
        }

        function clientVd() {
            var monery = $("#monery").val();//充值金额
            var code = $("#code").val();//验证码
            var isNum = /^\d+(\.\d+)?$/;
            if (!isNum.test(monery) || parseFloat(monery) < 1) {
                $.alert("请输入正确的充值金额！");
                return false;
            }
           
            if (code == "") {
                $.alert("请输入验证码！");
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .bankStyle li {
            list-style: none;
            float: left;
        }

            .bankStyle li img {
                padding-left: 10px;
            }

        .ctdl {
            text-align: left;
        }
       .ctdl p {padding-left:120px;}

            .ctdl p span {
                color: red;
                text-align: left;
            }
    </style>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">自动充值</a></li>
        </ul>
        <!--内容-->
       
        <form runat="server" id="form1">
            <asp:HiddenField ID="hidLogo" runat="server" />
            <div class="tab-content">
                <dl>
                    <dt style="padding-top:10px;">选择银行 :</dt>
                    <dd>
                        <ul class="bankStyle">
                            <asp:Repeater ID="rpt" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <input type="radio" id="bk_<%# Eval("BankId") %>" name="radBank" logo="<%# Eval("BankLogo") %>" value="<%# Eval("BankId") %>" minamt="<%# Eval("MinAmt") %>" maxamt="<%# Eval("MaxAmt") %>" />
                                        <img src="<%# Eval("BankLogo") %>" onclick="imgClick(<%# Eval("BankId") %>)" alt="<%# Eval("BankName") %>" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </dd>
                </dl>
                <dl>
                    <dt style="padding-top:5px;">充值金额 :</dt>
                    <dd>
                        <input type="text" id="monery"  value="<%=inStr %>" name="monery" class="input normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" " style="width:180px;" />
                        ( 单笔充值限额：最低： <span id="amtMin" style="font-size: 18px; color: red;">100</span> 元，最高： <span id="amtMax" style="font-size: 18px; color: red;">10000</span> 元 ) 
                    </dd>
                </dl>
                <dl>
                    <dt style="padding-top:5px;">充值金额大写 :</dt>
                    <dd>
                        <span id="spanMaxNum" style="color: red; font-size: 16px;"></span>
                    </dd>
                </dl>
                <dl>
                    <dt style="padding-top:5px;">验证码 :</dt>
                    <dd>
                        <table style="">
                            <tr>
                                <td style="padding: 0px; margin: 0px;">
                                    <input id="code" name="code" type="text" class="input normal" style="width: 180px;" /></td>
                                <td style="padding: 0px; margin: 0px;">
                                    <img width="78" id="codeImg" style="cursor: pointer; height: 28px;" onclick="changeCode();" /></td>
                            </tr>
                        </table>
                    </dd>
                </dl>
                <dl>
                    <dt style="padding-top:10px;">充值说明 :</dt>
                    <dd class="ctdl">
                        <p>每天的充值处理时间为：<span>早上 9:00 至 次日凌晨2:00</span></p>
                        <p>请手动刷新您的余额<span>及查看相关帐变信息,若超过1分钟未上分,请与客服联系</span></p>
                        <p>请务必<span>“屏蔽弹出窗口”的工具或程序</span>，否则无法在支付中跳转至银行界面或无法显示“支付成功”页面！</p>
                        <p>请在显示“支付成功”页面后，再关闭当前支付页面，以免造成掉单或到账延迟!</p>
                        <p>推荐使用<span>IE8 或以上版本浏览器、Mozilla Firefox、Chrome</span>，以免因系统不兼容导致无法正常支付!</p>
                        <p>支付多次出错，可在 IE 的工具菜单中选择“Internet选项”，点击“删除 cookies”和“删除文件”清除缓存后再重新支付！</p>
                        <p>支付前请记录订单号，如因网络连接等原因导致未即时到账，可提交此订单号给在线客服进行查询！</p>
                    </dd>
                </dl>
            </div>
            <div class="page-footer" style="text-align:center;padding-top:10px;">
                <asp:Button ID="btnNext" runat="server" Text="下一步" CssClass="formWord" OnClick="btnNext_Click" OnClientClick="return clientVd();"/></div>
        </form>
    </div>
</asp:Content>
