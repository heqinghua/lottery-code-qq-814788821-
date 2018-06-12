<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenQuota.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.OpenQuota" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>开户额</title>
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
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
        td {text-align:center;}
    </style>
</head>
<body>
     <nav class="col-xs-12 title" style="position:fixed;z-index:999;left:0px;top:0px;">
         <a id="J-goback" href="/mobile/userCenter/UsersList.aspx" class="go-back">返回</a>
         开户额</nav>
    <form id="form1" runat="server">
         <div class="ctParent" >
            <div class="bindDiv">
                <div style="height:10px;"></div>
                <p style="padding-left:20px;">
                    用户名：<asp:Label ID="lbCode" runat="server"></asp:Label><span></span>
                    返点：<asp:Label ID="lbBackNum" runat="server"></asp:Label>
                </p>
            </div>
            <div style="height:20px;"></div>
             <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable" >
                <thead>
                    <tr>
                        <th >开户级别</th>
                        <th  >我剩余开户额</th>
                        <th >下级剩余开户额</th>
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
            </table>
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