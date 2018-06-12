<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmBindCardNum.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.ConfirmBindCardNum" EnableEventValidation="false" EnableViewStateMac= "false"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <style type="text/css">
        .tab-content dl,.div-content dl{ clear:both; display:block; line-height:30px; }
    </style>
</head>
<body>
    <form id="form1" runat="server" action="/Views/Users/ConfirmBindCardNumLast.aspx" method="post">
        <input type="hidden" name="type" value="1"/>
        <input type="hidden" name="drpBanks_n" id="drpBanks_n" value=""/>
        <input type="hidden" name="drpPro_n" id="drpPro_n" value=""/>
        <input type="hidden" name="drpCity_n" id="drpCity_n" value=""/>
        <div class="control">
            <div class="bindDiv" id="bindfst" runat="server">
                <h2 class="subctTitle">使用提示：</h2>
                <p>1, 银行卡绑定成功后, 平台任何区域都<span>不会</span>出现您的完整银行账号, 开户姓名等信息。</p>
                <p>2,每个游戏账号最多绑定<span>5</span>张银行卡, 您已成功绑定<asp:Label ID="meBindNum" runat="server" Text="0"></asp:Label> 张。</p>
                <p>3,新绑定的提款银行卡需要绑定时间超过<span>2</span>小时才能正常提款。</p>
                <p>4, 一个账户只能绑定同一个开户人姓名的银行卡。</p>
                <div style="height:10px;"></div>
            </div>
          
            <div class="tab-content" style="border-top: 1px solid #e1e1e1;">
                <div id="invalue" >
                    <dl>
                        <dt>开户人银行 :</dt>
                        <dd>
                            <asp:DropDownList ID="drpBanks" runat="server" style="min-width:160px;"></asp:DropDownList>
                            <span class="Validform_checktip">*</span>
                        </dd>
                    </dl>
                    <dl>
                        <dt>开户省份 :</dt>
                        <dd>
                                <asp:DropDownList ID="drpPro" runat="server" style="min-width:160px;"></asp:DropDownList>
                            <span class="Validform_checktip">*</span>
                        </dd>
                    </dl>
                    <dl>
                        <dt>开户城市 :</dt>
                        <dd>
                                <asp:DropDownList ID="drpCity" runat="server" style="min-width:160px;">
                                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                                </asp:DropDownList>
                            <span class="Validform_checktip">*</span>
                        </dd>
                    </dl>
                    <dl id="zhihangdl" >
                        <dt>支行名称 :</dt>
                        <dd>
                            <asp:TextBox ID="txtZhiHang" runat="server" CssClass="input normal"></asp:TextBox>
                            <span class="Validform_checktip">*</span>
                        </dd>
                    </dl>
                    <dl>
                        <dt>开户人姓名 :</dt>
                        <dd>
                            <asp:TextBox ID="txtOpenUser" runat="server" CssClass="input normal"></asp:TextBox>
                            <span class="Validform_checktip">*</span>
                        </dd>
                    </dl>
                    <dl>
                        <dt>银行账号 :</dt>
                        <dd>
                            <asp:TextBox ID="txtCardNum" runat="server" CssClass="input normal" onpaste="return false"></asp:TextBox>
                            <span class="Validform_checktip">*</span>
                        </dd>
                    </dl>
                    <dl>
                        <dt>确认银行账号 :</dt>
                        <dd>
                            <asp:TextBox ID="txtConfirmCardNum" runat="server" CssClass="input normal" onpaste="return false"></asp:TextBox>
                            <span class="Validform_checktip">*</span>
                        </dd>
                    </dl>
                    <input type="submit" id="btnNext" value="下一步" class="formWord"  style="margin-left: 220px;margin-top:20px;"/>
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        var drpProId = "#<%=drpPro.ClientID%>";
        var drpCityid = "#<%=drpCity.ClientID%>";
        var drpBanksid = "#<%=drpBanks.ClientID%>";
        $(drpBanksid).change(function () {
            var value = $(this).val();
            var array = value.split('_');
            if (array[1].toLocaleLowerCase() == "true")
                $("#zhihangdl").show();
            else
                $("#zhihangdl").hide();
        });

        $(drpProId).change(function () {
            var pid = $(this).val();
            $.ajax({
                url: "/Page/Bank/Bank.aspx",
                type: 'post',
                data: "action=getcitys&pId=" + pid,
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    $(drpCityid).empty();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        for (var c = 0; c < jsonData.Data.length; c++) {
                            var item = jsonData.Data[c];
                            $(drpCityid).append("<option value='" + item.CityId + "'>" + item.CityName + "</option>");
                        }
                    } else {
                        $(drpCityid).empty();
                    }
                  //  $(".rule-single-select").ruleSingleSelect();
                }
            });
        });
        $("#btnNext").click(function () {

            var cityval = $(drpCityid).find("option:selected").val()
            var openName = $("#oldpwd").val();
            var card = $("#txtCard").val();

            if ($.trim(cityval) == "") {
                $.alert("请选择开户银行!");
                return false;
            }
            if ($("#zhihangdl").css("display") == "block") {
                if ($.trim($("#txtZhiHang").val()) == "") {
                    $("#txtZhiHang").focus();
                    $.alert("请输入支行名称!");
                    return false;
                }
            }
            $("#confirmzhih").css("display", $("#zhihangdl").css("display"));

            if ($.trim($("#txtOpenUser").val()) == "") {
                $("#txtOpenUser").focus();
                $.alert("请输入开户人姓名!");
                return false;
            }
            if ($.trim($("#txtCardNum").val()) == "") {
                $("#txtCardNum").focus();
                $.alert("请输入银行账号!");
                return false;
            }
            if ($.trim($("#txtConfirmCardNum").val()) == "") {
                $("#txtConfirmCardNum").focus();
                $.alert("请输入确认银行账号!");
                return false;
            }
            if ($.trim($("#txtConfirmCardNum").val()) != $.trim($("#txtCardNum").val())) {
                $("#txtConfirmCardNum").select();
                $.alert("两次卡号不一致!");
                return false;
            }

            $("#drpBanks_n").val($(drpBanksid).find("option:selected").text());
            $("#drpPro_n").val($(drpProId).find("option:selected").text());
            $("#drpCity_n").val($(drpCityid).find("option:selected").text());
            return true;
        });
       
    });
</script>
