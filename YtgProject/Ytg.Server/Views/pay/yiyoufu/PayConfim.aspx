<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayConfim.aspx.cs" Inherits="Ytg.ServerWeb.Views.pay.yiyoufu.PayConfim"  MasterPageFile="/Views/ChongZhi/ChongZhi.master"%>



<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>

    <style  type="text/css">
        .submit {
    background: url(http://cfapu.img48.wal8.com/img48/545266_20160510012323/146282032326.gif) left top no-repeat;
    width: 84px;
    height: 34px;
    border: 0;
    cursor: pointer;
    display: inline-block;
    color: #fff;
}
        #point SPAN {color:red;}
    </style>
    <script type="text/javascript">
        $("#autoChongzhi").addClass("title_active");
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
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">在线充值</a></li>
        </ul>
        <!--内容-->

        <form runat="server" id="form1">
            <div id="point">
                <div>
                    充值提示：<br>
                    请务必<span>关闭所有“屏蔽弹出窗口”的工具或程序</span>，否则无法在支付中跳转至银行界面或无法显示“支付成功”页面！ 
请在显示“支付成功”页面后，再关闭当前支付页面，以免造成掉单或到账延迟! 
推荐使用<span>IE7 或以上版本浏览器</span>，以免因系统不兼容导致无法正常支付! 
支付多次出错，可在 IE 的工具菜单中选择“Internet选项”，点击“删除 cookies”和“删除文件”清除缓存后再重新支付！ 
支付前请记录<span style="font-size:14px;font-weight:bold;">商户订单号</span >，如因网络连接等原因导致未即时到账，可提交此<span style="font-size:14px;font-weight:bold;">商户订单号</span>给在线客服进行查询！ <br /><br />
                    <form  method="post" name="drawform" id="drawform"  >
               <input type="button" value="如果没有弹出支付窗口，请点此处"  class="button submit" style="width:300px; height:30px; "  onclick="window.open('<%=OpenUrl%>    ');">
            </form>
                </div>
            </div>
            
            ﻿<div style="clear: both"></div>

        </form>
    </div>
    <script type="text/javascript">
        window.open("<%=OpenUrl%>");
    </script>
</asp:Content>