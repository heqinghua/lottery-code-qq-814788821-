<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPoints.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.UserPoints"  MasterPageFile="~/Views/UserGroup/Group.master" %>
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
    </style>
</asp:Content>

<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">用户升点</a></li>
        </ul>
    </div>
    
    <form id="form1" runat="server">
        <div class="control">
            <div class="tab-content"  runat="server" id="us" visible="false">
                <dl>
                    <dt>用户名&nbsp;:</dt>
                    <dd>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt>用户余额&nbsp;:</dt>
                    <dd>
                       <asp:Label ID="Label2" runat="server"> </asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt>返点级别&nbsp;:</dt>
                    <dd>
                       <asp:Label ID="Label3" runat="server"  Text=""></asp:Label>
                    </dd>
                </dl>
             </div>
            <div class="tab-content" runat="server" id="ct">
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
                    <dt>返点级别&nbsp;:</dt>
                    <dd>
                       <asp:Label ID="lbLevel" runat="server"  Text=""></asp:Label>
                    </dd>
                </dl>
                
                <dl>
                    <dt>上级最大返点级别&nbsp;:</dt>
                    <dd>
                       <asp:Label ID="lbChildMax" runat="server"  Text="0"></asp:Label><span style="color:red;"> [升点后不能高于上级的最大返点级别] </span>
                    </dd>
                </dl>
                <dl style="margin-top:4px;">
                    <dt>升点处理&nbsp;:</dt>
                    <dd>
                       <span>升到：</span>
                        <asp:DropDownList ID="drpRemo" runat="server" style="min-width:65px;"></asp:DropDownList>
                    </dd>
                </dl>
                <dl style="margin-top:5px;">
                    <dt>升点类型&nbsp;:</dt>
                    <dd ><asp:DropDownList ID="droptype" runat="server" style="min-width:80px;">
                           <asp:ListItem Value="-1" Text="请选择" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="以量升点"></asp:ListItem>
                            <asp:ListItem Value="1" Text="配额升点"></asp:ListItem>
                       </asp:DropDownList>
                       <p style="color:red;">任意选择一种升点类型，以量升点需要在指定时间段内达到指定的量才能升点，配额升点:系统调整对应级别的配额</p>
                    </dd>
                </dl>
                <dl style="display:none;margin-top:5px;" id="pe">
                    <dt>配额信息&nbsp;:</dt>
                    <dd>
                        <asp:Literal ID="ltmePeie" runat="server"></asp:Literal>
                    </dd>
                </dl>
                <dl id="liang" style="display:none;margin-top:5px;">
                    <dt>统计时间&nbsp;:</dt>
                    <dd>
                        <div style="margin-left:110px;margin-top:-20px;">
                            <span>统计天数：</span><select id="staDay" style="min-width:80px;"> <option value="0">3天量</option><option value="1">7天量</option><option value="2">10天量</option></select>
                            <span style="margin-left:10px;">截止日期：</span>
                                    <input id="txtstart" type="text" class="input" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate: '<%=DateTime.Now.ToString("yyyy-MM-dd") %>' })"  value="<%=DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") %>" style="width:100px;"/>
                        </div>
                       <p style="color:red;margin-left:110px; ">例如：选择统计天数为3天，截止日期为2016-03-12,刚表示统计在<span style="color:blue;font-weight:bold;">2016-03-10 03:00</span> 到<span style="color:blue;font-weight:bold;">2016-03-13 03:00</span>内需要升点用户的团队销量,默认为前一天. </p>
                    </dd>
                </dl>
                <dl>
                    <dt>操作说明&nbsp;:</dt>
                    <dd style="color:red;">
                       <p style="margin-left:110px;color:blue;">1.配额升点处理：从上一个返点级别区间升到下一个返点级别区间，总代在上个返点级别区间增加一个配额，下一个需要开户配额的返点级别区间扣减一个对应的配额
                           比如：从7.1升点到7.4，此时总代在7.4]区间扣减一个配额，7.1区间增加一个配额</p>
                           <p style="margin-left:110px;color:blue;">2.按量升点：提交数据需要严格按照以下的升点标准处理，并且最多统计一个月之内的量，请仔细核对. </p>
                    </dd>
                </dl>
                  <div style="text-align:center;margin-top:5px;">
                    <asp:Button  ID="btnSubmit" runat="server" CssClass="formWord" Text="立即升点"  OnClick="btnSubmit_Click"/>
                </div>
                <dl>
                    <dt>升点标准&nbsp;:</dt>
                    <dd>
                      <%=Builder3Rule() %>
                    </dd>
                </dl>
              
                <div style="text-align:center;margin-top:10px;">
                    平台保留对以上所有标准的解释权和修改权。
                </div>
            </div>
        </div>
    </form>
    
<script type="text/javascript">
    $(function () {
        $("#users").addClass("title_active");
       
        $("#<%=droptype.ClientID%>").change(function () {
            var selValue = $(this).find("option:selected").val();
            if (selValue == 1)//配额
            {
                $("#liang").hide();
                $("#pe").show();

            } else {//
                $("#pe").hide();
                $("#liang").show();
            }
        });
        $("#<%=btnSubmit.ClientID%>").click(function () {
            if ($("#<%=droptype.ClientID%>").find("option:selected").val() == -1) {
                $.alert("请选择升点类型!");
                return false;
            }
            var param = "action=addpoints";
            param += "&uid=<%=Request.Params["uid"]%>";
            param += "&rmb=" + $("#<%=drpRemo.ClientID%>").find("option:selected").val();
            param += "&pointsType=" + $("#<%=droptype.ClientID%>").find("option:selected").val();
            param += "&statcount=" + $("#staDay").find("option:selected").val();
            param += "&enddate=" + $("#txtstart").val();
            //回收
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: param,
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        //window.location.reload();
                        $.alert("用户升点成功!", 1, function () {
                            window.location = "/Views/UserGroup/UsersList.aspx?id=<%=Request.QueryString["uid"]%>&name=<%=Request.QueryString["name"]%>";
                        });
                    } else if (jsonData.Code == 1002) {
                        var selValue = $("#<%=droptype.ClientID%>").find("option:selected").val();
                        if (selValue == 1)
                            $.alert("配额不够，请联系上级代理!");
                        else
                            $.alert("未达到指定销量，无法提升返点!");
                    }
                    else {
                        $.alert("用户升点失败，请关闭后重试!");
                    }
                }
            });
            //
            return false;
        });
    });

</script>
</asp:Content>