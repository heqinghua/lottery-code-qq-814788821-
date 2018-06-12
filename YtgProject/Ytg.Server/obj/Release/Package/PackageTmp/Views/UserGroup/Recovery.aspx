<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recovery.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.Recovery"   MasterPageFile="~/Views/UserGroup/Group.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
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
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <style type="text/css">
        .tab-content dl, .div-content dl {line-height:20px;padding:2px 0px;}
        .ltable th {text-align:center;font-weight:bold;}
        .tab-content dl dt {width:110px;}
        .ltable td {
    padding: 8px 0;
    border-bottom: 1px solid #e8e8e8;
    line-height: 1.5em;
    padding-left: 1px;
    padding-right: 1px;
    border-right: 1px solid #e8e8e8;
}
    </style>
</asp:Content>

<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">用户回收</a></li>
        </ul>
    </div>
    
    <form id="form1" runat="server">
        <div class="control">
            <div class="tab-content" >
                <dl>
                    <dt>用户名&nbsp;:</dt>
                    <dd>
                        <asp:Label ID="lbUser" runat="server"></asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt>用户余额&nbsp;:</dt>
                    <dd>
                       <asp:Label ID="lbMonery" runat="server" ></asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt>回收配额&nbsp;:</dt>
                    <dd>
                        <asp:Literal ID="ltmePeie" runat="server"></asp:Literal>
                        <asp:CheckBox  ID="chkhuishou" runat="server" Text="&nbsp;勾选即可进行配额的回收[配额回到操作者账号] " Visible="false" style="color:red;"/>
                       <asp:Label ID="lb" runat="server" style="color:red;" Text="没有可回收的配额"></asp:Label>
                    </dd>
                </dl>
                <div id="noneDiv" runat="server" visible="false">
                  <dl>
                    <dt>降点处理&nbsp;:</dt>
                    <dd>
                        <label style="color:red;">返点已降到最低，不能再降点</label>
                    </dd>
                </dl>
                </div>
                <div id="otherDiv" runat="server">
                <dl>
                    <dt>返点级别&nbsp;:</dt>
                    <dd>
                       <asp:Label ID="lbLevel" runat="server"  Text=""></asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt>下级最大返点级别&nbsp;:</dt>
                    <dd>
                       <asp:Label ID="lbChildMax" runat="server"  Text="0"></asp:Label> <span style="color:red;">[降点后不能低于下级的最大返点级别] </span>
                    </dd>
                </dl>
                <dl>
                    <dt>降点处理&nbsp;:</dt>
                    <dd>
                        <span>是否降点：</span><input type="checkbox" id="chkJd" name="chkJd"  style="font-size:25px;"/><label for="chkJd" style="color:red;padding-left:2px;">勾选即可进行降点</label>
                        <span style="padding-left:10px;">降到：</span><asp:DropDownList ID="drpRemo" runat="server"  style="min-width:80px;"></asp:DropDownList>
                    </dd>
                </dl>
                <dl>
                    <dt>操作说明&nbsp;:</dt>
                    <dd style="color:red;padding-left:110px;">
                      <p style="color:blue;">1.选择类型：回收配额与降点操作可以任意选择:自行勾选需要的操作(一个或者两个),如果勾选降点需要输入对应的降点量</p>
                        <p style="color:blue;">2.回收配额：如果当前用户有配额，配额回到操作者账号.</p>
                        <p style="color:blue;">3.降点处理：从上一个返点级别区间降到下一个返点级别区间，总代在上个返点级别区间增加一个配额，下一个需要开户配额的返点级别区间扣减一个对应的配额</p>
                        <p style="color:blue;">比如：从 7.4 降点到 7.1，此时总代在 7.4 区间增加一个配额，7.1 区间扣减一个配额 </p>
                    </dd>
                </dl>
                <div style="text-align:center;">
                    <asp:Button  ID="btnSubmit" runat="server" CssClass="formWord" Text="立即回收"  OnClick="btnSubmit_Click"/>
                </div>
                <dl>
                    <dt>降点标准&nbsp;:</dt>
                    <dd style="text-align:center;">
                       <%=BuilderShowView() %>
                    </dd>
                </dl>
                
                <div style="text-align:center;margin-top:10px;">
                    平台保留对以上所有标准的解释权和修改权。
                </div>
                    </div>
            </div>
        </div>
    </form>
    
<script type="text/javascript">
    $(function () {
        $("#<%=btnSubmit.ClientID%>").click(function () {
            var iscl = ($("#<%=chkhuishou.ClientID%>").attr("checked") == undefined ? -1 : 0);
            var usckjd = ($("#chkJd").attr("checked") == undefined ? -1 : 0);
           
            if (iscl != 0 && usckjd != 0) {
                $.alert("请选择相关操作！");
                return false;
            }
            if (confirm("确定要回收此用户吗？")) {
                var param = "action=recovery";
                param += "&uid=<%=Request.Params["uid"]%>";
                param += "&rmb=" + $("#<%=drpRemo.ClientID%>").find("option:selected").val();
                param += "&quoClear=" + iscl;
                param += "&chkJd=" + usckjd;

                //回收
                $.ajax({
                    url: "/Page/Users.aspx",
                    type: 'post',
                    data: param,
                    success: function (data) {
                        var jsonData = JSON.parse(data);
                        //清除
                        if (jsonData.Code == 0) {
                            $.alert("回收用户成功!", 1, function () {
                                window.location = "/Views/UserGroup/UsersList.aspx?id=<%=Request.QueryString["uid"]%>&name=<%=Request.QueryString["name"]%>";
                            });
                        } else if (jsonData.Code == 1002) {
                            $.alert("回收用失败，选择的返点低于下级最高返点!")
                        } else if (jsonData.Code == 1004) {
                            $.alert("请选择相关操作!")
                        } else if (jsonData.Code == 1003) {
                            $.alert("十天内团队销量达到标准不能降点!")
                        } else {
                            $.alert("回收用失败，请关闭后重试!");
                        }
                    }
                });
            }
            //
            return false;
        });
    });

</script>
</asp:Content>
