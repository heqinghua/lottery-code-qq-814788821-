<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Binding.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.Binding" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <style type="text/css">
        .tab-content dl,.div-content dl{ clear:both; display:block; line-height:30px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="control">
            <div class="bindDiv">
                <div style="height:30px;"></div>
                <h2 class="subctTitle">使用提示：</h2>
                <p>请输入您已经绑定银行卡的相关信息进行安全验证。</p>
                <p></p>
                <p>已绑定的<span id="bankSpan"><asp:Label ID="lbbankName" runat="server"></asp:Label></span>卡：<span id="cardNum"><asp:Label ID="lbBankNo" runat="server"></asp:Label></span></p>
                <div style="height:10px;"></div>
            </div>
            <div class="tab-content" style="border-top:1px solid #e1e1e1; text-align:center;padding-left:75px;">
                <dl>
                    <dt>开户人姓名</dt>
                    <dd>
                        <input type="text" id="oldpwd" name="oldpwd" class="input normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" " style="width:210px;" />
                        <span class="Validform_checktip">*</span>
                    </dd>
                </dl>
                <dl>
                    <dt>银行卡号</dt>
                    <dd>
                        <input type="text" id="txtCard" name="txtCard" class="input normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" " style="width:210px;"  onpaste="return false"/>
                        <span class="Validform_checktip">*</span>
                    </dd>
                </dl>
                <asp:Button  ID="btnSubmit" runat="server" CssClass="formWord" Text="下一步" style="margin-top:30px;margin-left:-80px;" OnClick="btnSubmit_Click"/>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#<%=btnSubmit.ClientID%>").click(function () {
            
            var openName = $("#oldpwd").val();
            var card = $("#txtCard").val();
            if ($.trim(openName) == "") {
                $("#oldpwd").focus();
                $.alert("请输入开户人姓名!");
                return false;
            }
            if ($.trim(card) == "") {
                $("#txtCard").focus();
                $.alert("请输入银行卡号!");
                return false;
            }

            return true;
        });
    });
</script>
