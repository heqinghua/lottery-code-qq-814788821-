<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenQuota.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.OpenQuota" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>开户额</title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <style type="text/css">
        td {text-align:center;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="control">
            
            <div class="bindDiv">
                <div style="height:10px;"></div>
                <p style="padding-left:20px;">
                    用户名：<asp:Label ID="lbCode" runat="server"></asp:Label><span></span>
                    返点：<asp:Label ID="lbBackNum" runat="server"></asp:Label>
                </p>
            </div>
            <div style="height:20px;"></div>
            <div style="height:500px;overflow:auto;">
             <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable" >
                <thead>
                    <tr>
                        <th width="20%" >开户级别</th>
                        <th  width="20%">我的剩余开户额</th>
                        <th width="20%" >下级剩余开户额</th>
                        <th>为下级增加开户额</th>
                    </tr>
                </thead>
                 <tbody>
                     <asp:Repeater ID="rpt" runat="server">
                         <ItemTemplate>
                             <tr>
                             <td style="font-weight:bold;"><%# Eval("QuoType") %></td>
                             <td><%# Eval("QuoValue") %></td>
                             <td><%# Eval("ChildQuoValue") %></td>
                             <td>
                                 <input type="text" id="<%# Eval("QuoId") %>" class="input" name="<%# Eval("QuoId") %>" max="<%# Eval("QuoValue") %>"/>
                             </td>
                                 </tr>
                         </ItemTemplate>
                     </asp:Repeater>
                 </tbody>
            </table></div>
            <div style="text-align:center;margin-top:20px;"><asp:Button  ID="btnSubmit" runat="server" CssClass="formCheck" Text="分配开户额" OnClick="btnSubmit_Click"/>
                </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    var partten = /^\d+$/;
    $(function () {
        $('input[type=text]').keyup(
            function () {
                if (!partten.test($(this).val())) {
                    $(this).val('');
                }
            });

        $("#btnSubmit").click(function () {
            //验证
            var inputLength = $('input[type=text]').length;
            var isVd = false;
            var isempty = true;
            var data="";
            $('input[type=text]').each(function () {
                if ($(this).val() != "") {
                    //验证是否为数字
                    if (!partten.test($(this).val())) {
                        $.alert("开户额只能为数字!");
                        $(this).select();
                        return false;
                    }
                    isempty = false;
                    //
                    var kh = parseInt($(this).val());
                    if (kh > parseInt($(this).attr("max")) || kh<1) {
                        $.alert("下级增加开户额不能大于自身开户额!");
                        $(this).select();
                        return false;
                    }
                    data+=$(this).attr("id")+"_"+$(this).val()+","
                    isVd = true;
                }
            });
            if (isempty)
            {
                $.alert("请填写为下级增加的开户额!");
            }
            if (!isVd) { 
                return false;
            }
            
            //充值
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: "action=fenpei&data=" + data,
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        parent.$.alert("配额分配成功!");
                        parent.quoclose();
                    } else {
                        $.alert("配额分配失败，请关闭后重试!");
                    }
                }
            });
            //
            return false;

        });

    });
</script>