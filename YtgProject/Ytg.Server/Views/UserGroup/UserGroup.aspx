<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserGroup.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.UserGroup" MasterPageFile="~/Views/UserGroup/Group.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
     <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#groupUser_p").addClass("title_active");

            //onkeyup:根据用户输入的资金做检测并自动转换中文大写金额(用于充值和提现)
            //obj:检测对象元素，chineseid:要显示中文大小写金额的ID，maxnum：最大能输入金额
            $("#chineseMoney").html(changeMoneyToChinese(<%=Monery.Replace(",","") %>));
        })
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
    <div class="list-div" id="listDiv">
        <table class="formTable" cellspacing="1" cellpadding="3" id="list-table"  style="margin:auto;width:50%;" >
            <tbody>
                <tr>
                    <td width="150" align="right">帐号:</td>
                    <td width="200"><%=Code %></td>
                </tr>
                <tr>
                    <td width="150" align="right">昵称:</td>
                    <td><%=NockName %></td>
                </tr>
                <tr>
                    <td width="150" align="right">团队余额:</td>
                    <td colspan="3" style="color:red;"><%=Monery %>&nbsp;&nbsp;元</td>
                </tr>
                <tr>
                    <td width="150" align="right">团队余额(大写):</td>
                    <td colspan="3" style="color:red;"><span id="chineseMoney"></span></td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
