<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRegistSetting.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.AutoRegistSetting" MasterPageFile="~/Views/UserGroup/Group.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/jquery.zclip.min.js" type="text/javascript"></script>   
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
   
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <form runat="server" id="form1">
        <div class="list-div">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="" style="padding:10px 0 50px 0;"  id="customerUser">
            <tr>
                <td align="center">
                    <div class="layout-slider" style="position: relative; padding: 5px 0">
                        <div style="font-size:14px;font-weight: bold;">
                            自身保留返点：<span id="keeppoint">0%</span> <span style="display:none;">范围<%=minPlayType %>-<%=Ytg.Comm.Utils.MaxBonus %>之间的偶数</span>
                            &nbsp;&nbsp;&nbsp;&nbsp;<label>用户奖金组：</label><input type="text" id="userpointset1" readonly="readonly" name="changepoint" value="<%=this.curBackNum %>" style="width:50px;border-color: transparent; " />
                            </div>
                        <div class="big-lider"><input id="SliderSingle" type="slider" value="1700"  style="display:none;"/>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
            <input  type="hidden" id="hidValue" name="hidValue"/>
            <div style="height:55px;"></div>
            <div class="overflowHidden" style="display: block;"  id="customerUser1">
                <div class="market1_list">
                    <ul class="control_market">
                        <li>
                            <label id="agent_">重庆时时彩</label>
                        </li>
                        <li>
                            <label id="Label1">江西时时彩</label>
                        </li>
                        <li>
                            <label id="Label2">上海时时乐</label>
                        </li>
                        <li>
                            <label id="Label3">山东11选5</label>
                        </li>
                        <li>
                            <label id="Label4">新疆时时彩</label>
                        </li>
                        <li>
                            <label id="Label5">江西11选5</label>
                        </li>
                        <li>
                            <label id="Label6">广东11选5</label>
                        </li>
                        <li>
                            <label id="Label8">福彩3D</label>
                        </li>
                        <li>
                            <label id="Label9">排列35</label>
                        </li>
                        <li>
                            <label id="Label10">天津时时彩</label>
                        </li>
                        <li>
                            <label id="Label17">北京赛车</label>
                        </li>
                         <li>
                            <label id="Label20">扣扣分分彩</label>
                        </li>
                        <li>
                            <label id="Label21">CK两分彩</label>
                        </li>
                        <li>
                            <label id="Label22">台湾五分彩</label>
                        </li>
                         <li>
                            <label id="Label23">韩国1.5分彩</label>
                        </li>
                       <%-- <li>
                            <label id="Label24">印尼时时彩</label>
                        </li>--%>
                    </ul>
                </div>
            </div>


            <table class="grayTable_marketing" width="100%" border="0" cellspacing="0" cellpadding="0" >
                <tr id="customerUser3">
                    <td colspan="8">
                        <asp:Button  ID="btnSave" runat="server" CssClass="formCheck" Text="保存" OnClick="btnSave_Click" OnClientClick="return onClientClick();"/></td>
                </tr>
                <tr>
                    <td colspan="6">您的推广注册地址为:
                                    <%--    <select id="fe_text12" >
                                            <option id="tuiguang10" value="<%=registUrl %>"><%=registUrl %></option>
                                        </select>--%>
                        <asp:DropDownList ID="fe_text12" runat="server">
                        </asp:DropDownList>
                        <button id="btnCopy" type="button" style="border:1px solid #808080;padding:8px; padding-top:2px;padding-bottom:2px;color:#000;position:relative;" >复制</button>
                        <p class="daili_zc_hint">如果浏览器阻止您复制链接，请用ctrl+c 复制下面连接：</p>
                        <div id="hidtxt"></div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript" charset="utf-8">
        var max=<%=UserMaxRemo%>;
        var min=<%=minPlayType%>;

        var checkText = $("#<%=fe_text12.ClientID%>").find("option:selected").text();  //获取Select选择的Text
    
        $("#hidtxt").html(checkText);
        $(document).ready(function () {
            if(max==min)
            {
                $("#customerUser").hide();
                $("#customerUser1").hide();
                $("#customerUser3").hide();
            }

            $("#autoRegistSetting").addClass("title_active");

            $("#<%=fe_text12.ClientID%>").change(function () {//code...});   //为Select添加事件，当选择其中一项时触发
                var checkText = $("#<%=fe_text12.ClientID%>").find("option:selected").text();  //获取Select选择的Text
                $("#hidtxt").html(checkText);
            });

            $("#btnCopy").zclip({
                path: "/Content/Scripts/ZeroClipboard.swf",
                copy: function () {
                    return $("#<%=fe_text12.ClientID%>").find("option:selected").text();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                  //  $.dialog.tips("复制链接成功!", 2, '32X32/succ.png', function () { });
                    $.alert("复制链接成功!");
                }
            });

            var curBack=<%=curBackNum%>/100*2000+min;
            
            $( "#SliderSingle" ).slider( "value", curBack );
        });

        
        var maxpoint1 = (max - min) / 2000;
        jQuery("#SliderSingle").slider({
            from: min, to: max, step: 2, round: 5, format: { format: '##', locale: 'zh' }, dimension: '&nbsp;', skin: "round",
            onstatechange: function (value) {

                _sider(value);
            }
        });
        function _sider(value) {
            $("#userpointset1").val(value);
            $(".showpoint").html(value);
            $("#keeppoint").html((toDecimal((maxpoint1 - (value - min) / 2000) * 100.00)) + "%");
            $(".lotterypoint").html(value);
            getleft();
        }
        function getleft() {
            var _w = $(".jslider-pointer").attr("style").split(";");
            for (var i = 0; i < _w.length; i++) {
                if (_w[i].indexOf('left') >= 0) {
                    _w = _w[i].split(":")[1];
                    break;
                }
            }
            $(".jslider-scale").width(_w);
        }
        function toDecimal(x) {
            var f = parseFloat(x);
            if (isNaN(f)) {
                return;
            }
            f = Math.round(x * 100) / 100;
            return f;
        }

        function onClientClick(){
            $("#hidValue").val($("#keeppoint").html());
            return true;
        }
        $(function () {
            Ytg.common.loading();
            var cldt = setInterval(function () {
                Ytg.common.cloading();
                clearInterval(cldt);
            }, 1000)
        })
    </script>

    ﻿<div style="clear: both"></div>
</asp:Content>
