<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="Ytg.ServerWeb.Views.Activity.Activity"  MasterPageFile="~/lotterySite.Master"%>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <style type="text/css">
        .ltable td {font-size:12px;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#actives").addClass("cur");
            var hg = $(window).height() - 250;
            $("#content").css("min-height", hg);
            $(".left_frame,.right_box").css("min-height", (hg - 52));
        })
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  class="UserInfoBox wrap_footerbg" style="background:#fff;padding-bottom:50px;">
        <div class="wrap_bg wrap" style="width: 1300px;">
            <!--个人信息-->
            <div id="content" style="padding-top: 0px; position: relative;">
                <div class="left_frame" style="position: absolute; z-index: 999;top:50px;width:200px;">
                    <div class="left_content">
                        <div  class="lottery_type"><span style="margin-left:-30px;width:78px;">活动中心</span></div>
                        <div class="sidebar_menu">
                            <dl class="ff-tow2">
                                <dd class="on">
                                    <asp:Repeater ID="rptActivitys" runat="server">
                                        <ItemTemplate>
                                            <ul class="con_ul noBorder">
                                                <li><a href="<%# Eval("ActivityUrl") %>" >
                                                    <p id="touzhu"><span class="hot"><%# Eval("ActivityName") %></span></p>
                                                    </a>
                                                </li>
                                            </ul>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </dl>
                        </div>
                    </div>
                    <div class="leftsidebotcon"></div>
                    <script>
                        $(".ff-one").click(function () {
                            $(this).addClass("ff-tow").siblings().removeClass("ff-tow")
                        });
                        $(".ff-tow").click(function () {
                            $(this).slideDown();
                        });
                    </script>
                </div>
                <div id="con_right">
                    <div class="right_box" style="padding-left:10px;padding-right:10px;padding-bottom:10px; width:1050px;float:right; margin: 0px;margin-top:50px;  border-image: none; ">
                        <iframe id="main" name="main" allowtransparency="true"   width="100%" height="800" scrolling="auto" frameborder="0" src="QianDao/QianDao.aspx" border="0" noresize="noresize" framespacing="0" style="min-height: 1082px;"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>