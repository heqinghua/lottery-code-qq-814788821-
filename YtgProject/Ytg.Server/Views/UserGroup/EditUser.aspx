<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.EditUser" MasterPageFile="~/Views/UserGroup/Group.master" %>

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
    <style type="text/css">
        .tab-content dl, .div-content dl {
            line-height: 20px;
            padding: 2px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
        <ul id="tabs-ul" class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">注册管理</a></li>
        </ul>
    </div>
    <form id="form1" runat="server">
        <div class="control">
            <div class="tab-content" style="border-bottom: 1px solid #e1e1e1;">
                <dl>
                    <dt>用户级别 :</dt>
                    <dd>
                        <asp:Panel runat="server" ID="panelZd" Visible="false">
                            <input type="radio" value="0" id="dailiZong" checked="checked" name="level" /><label for="dailiZong">&nbsp;总代理</label>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="panelOther">
                            <input type="radio" value="0" id="daili" checked="checked" name="level" /><label for="daili">&nbsp;代理用户&nbsp;&nbsp;</label>
                            <input type="radio" value="0" id="putong" name="level" /><label for="putong">&nbsp;会员用户</label>
                        </asp:Panel>
                    </dd>
                </dl>
                <dl id="playType" runat="server" visible="false" style="display:none;">
                    <dt>奖金类型 :</dt>
                    <dd>
                        <input type="radio" value="0" id="playType1800" runat="server" checked="true" name="playType" /><label for="playType1800">&nbsp;1800</label>
                        <input type="radio" value="1" id="playType1700" runat="server" name="playType" style="margin-left: 27px;" /><label for="playType1700">&nbsp;1700</label>
                    </dd>
                </dl>
                <dl>
                    <dt>输入开户账号 :</dt>
                    <dd>
                        <input type="text" id="txtaccount" class="input normal" maxlength="16" datatype="*1-16" sucmsg=" " text="" nullmsg=" " style="width: 200px;" /><span class="Validform_checktip">*由字母或数字组成的6-16个字符,不能连续四位相同的字符,首字不能以0或者o开头</span>
                    </dd>
                </dl>
                <dl>
                    <dt>默认密码 :</dt>
                    <dd>
                        <input type="password" id="txtPassword" class="input normal" maxlength="16" datatype="*1-16" sucmsg=" " text="" nullmsg=" " style="width: 200px;" /><span class="Validform_checktip">*由字母和数字组成6-16个字符；且必须包含数字和字母</span>
                    </dd>
                </dl>
                <dl>
                    <dt>开户昵称 :</dt>
                    <dd>
                        <input type="text" id="nikeName" class="input normal" datatype="*1-6" maxlength="6" sucmsg=" " text="" nullmsg=" " style="width: 200px;" /><span class="Validform_checktip">*由2至6个字符组成</span>
                    </dd>
                </dl>
                <dl>
                    <dt>返点设置：</dt>
                    <dt>
                        <table  border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td align="center" height="60">
                                    <div class="layout-slider" id="SliderSingle_parent">
                                        <div >
                                            自身保留返点：<span id="keeppoint_pst">0</span>用户奖金组：<input type="text" id="userpointset" readonly="" name="keeppoint" style="width: 50px; height: 20px; border-color: transparent; text-align: left" value="1700" />
                                        </div>
                                        <input id="SliderSingle" type="slider" value="1700" style="width: 500px" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </dt>
                </dl>
                
            </div>

            <div style="text-align:left;margin-left:120px;">
                <div style="height:80px;"></div>
                <input type="button" id="btnSubmit" value="确认注册" class="formWord" />
            </div>
            <div id="tabs-1" style="display:none;">
                <div style="height: 20px;"></div>
                
                <input type="hidden" id="hidValue" name="hidValue" />
                <div style="height: 55px;"></div>
                <div class="overflowHidden" style="display: block;" id="customerUser1">
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
                                <label id="Label21">CK两份彩</label>
                            </li>
                            <li>
                                <label id="Label22">台湾五分彩</label>
                            </li>
                            <li>
                                <label id="Label23">韩国1.5分彩</label>
                            </li>

                        </ul>
                    </div>
                </div>
            </div>

            <div style="height: 20px;"></div>
           
            <div style="height: 20px;"></div>
            ﻿<div style="clear: both"></div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#usersRegist").addClass("title_active");
            $("#btnSubmit").click(function () {
                if (!validateUserName($("#txtaccount").val())) {
                    $("#txtaccount").focus();
                    $.alert("登录名由0-9,a-z,A-Z组成的6~16个字符组成!");
                    return false;
                }

                if (!validateUserPss($("#txtPassword").val())) {
                    $("#txtPassword").focus();
                    // $.dialog.tips("6－16位数字和字母，不能只是数字，或者只是字母，不能连续三位相同!",2, '32X32/succ.png', function () { });
                    $.alert("登录密码6－16位数字和字母，不能只是数字，或者只是字母，不能连续三位相同!");
                    return false;
                }
                if($("#nikeName").val()=="" || $("#nikeName").val().length<2 || $("#nikeName").val().length>6){
                    $.alert("昵称由2至6个字符组成!");
                    return false;
                }
                var remos=$("#keeppoint_pst").html().replace("%","");
                
                var playType=$("#<%=playType1800.ClientID%>")!=undefined?($("#<%=playType1800.ClientID%>").attr("checked")!=undefined?0:1):-1;
                var userType=-1;
                
                if(document.getElementById("dailiZong")!=null && document.getElementById("dailiZong")!=undefined)
                {userType=3;
                }else{
                    userType=($("#daili").attr("checked")!=undefined?1:0)
                }
                var param = "action=adduser";
                param += "&userType="+userType;
                param += "&code="+$("#txtaccount").val();
                param += "&password="+$("#txtPassword").val();
                param += "&nickName="+$("#nikeName").val();
                param += "&rmb="+parseFloat(remos);
                param+="&playType="+playType
                //充值
                $.ajax({
                    url: "/Page/Users.aspx",
                    type: 'post',
                    data: param,
                    success: function (data) {
                        var jsonData = JSON.parse(data);
                        //清除
                        if (jsonData.Code == 0) {
                            // $.dialog.tips("保存成功!", 2.0, '32X32/succ.png', function () { });
                            $.alert("注册成功!");
                            $("#txtaccount").val("");
                            $("#txtPassword").val("");
                            $("#nikeName").val("");
                            //parent.userClose();
                        }else if(jsonData.Code==1002){
                            //$.dialog.tips("指定的返点级别无足够的配额，请联系上级获取配额!", 2.0, '32X32/succ.png', function () { });
                            $.alert("此返点级别配额不足，请联系上级获取!");
                        }else if(jsonData.Code==1011){
                            $("#txtaccount").focus();
                            // $.dialog.tips("账号已被注册!", 2.0, '32X32/succ.png', function () { });
                            $.alert("账号已被注册!");
                        }
                        else {
                            //$.dialog.tips("保存失败，请关闭后重试!", 2.0, '32X32/succ.png', function () { });
                            $.alert("保存失败，请关闭后重试!");
                        }
                    }
                });
                //
                return false;

            });
        });

        var max=1800+<%=UserMaxRemo%>;
        var max800=1800+<%=UserMaxRemo%>;
        var max1700=1700+<%=UserMaxRemo1700%>;
        var min=<%=minPlayType%>;
        var defUserPoint =<%= loginUserRebate%>;
        var userPoint =<%= loginUserRebate%>;
        $(function(){
            if(max==min)
                $("#tabs-1").hide();

            $("#<%=playType1800.ClientID%>").click(function(){
                min=1800;
                max=max800;
                if(userPoint==14)
                    userPoint=9.0;
                if(userPoint==13.9)
                    userPoint=8.9;
                if(userPoint==13.8)
                    userPoint=8.8;
                if(userPoint==13.7)
                    userPoint=8.7;
                if(userPoint==13.6)
                    userPoint=8.6;
                if(userPoint==13.5)
                    userPoint=8.5;
                if(userPoint==13.4)
                    userPoint=8.4;
                if(userPoint==13.3)
                    userPoint=8.3;
                if(userPoint==13.2)
                    userPoint=8.2;
                if(userPoint==13.1)
                    userPoint=8.1;
                if(userPoint==13)
                    userPoint=8.0;
                if(userPoint==12.9)
                    userPoint=7.9;
                if(userPoint==12.8)
                    userPoint=7.8;
                if(userPoint==12.7)
                    userPoint=7.7;
                else if(userPoint==12.6)
                    userPoint=7.6;
                else if(userPoint==12.5)
                    userPoint=7.5;
                else if(userPoint==12.4)
                    userPoint=7.4;
                else if(userPoint==12.3)
                    userPoint=7.3;
                else if(userPoint==12.2)
                    userPoint=7.2;
                else if(userPoint==12.1)
                    userPoint=7.1;

                $(".jslider").remove();
                $("#SliderSingle").remove();
                $("#SliderSingle_parent").append("<input id=\"SliderSingle\" type=\"slider\" value=\"1700\" style=\"width: 500px\" />");

                jQuery("#SliderSingle").slider({ from: min, to: max, step: 2, round: 5, format: {format: '##', locale: 'zh'}, dimension: '&nbsp;', skin: "round" ,
                    onstatechange: function( value ){
                        siderMove(value); 
                    }
                });

            });

            $("#<%=playType1700.ClientID%>").click(function(){
                min=1700;
                max=max1700;
                if(userPoint==9)
                    userPoint=14;
                if(userPoint==8.9)
                    userPoint=13.9;
                if(userPoint==8.8)
                    userPoint=13.8;
                if(userPoint==8.7)
                    userPoint=13.7;
                if(userPoint==8.6)
                    userPoint=13.6;
                if(userPoint==8.5)
                    userPoint=13.5;
                if(userPoint==8.4)
                    userPoint=13.4;
                if(userPoint==8.3)
                    userPoint=13.3;
                if(userPoint==8.2)
                    userPoint=13.2;
                if(userPoint==8.1)
                    userPoint=13.1;
                if(userPoint==8.0)
                    userPoint=13;
                if(userPoint==7.9)
                    userPoint=12.9;
                if(userPoint==7.8)
                    userPoint=12.8;
                if(userPoint==7.7)
                    userPoint=12.7;
                else if(userPoint==7.6)
                    userPoint=12.6;
                else if(userPoint==7.5)
                    userPoint=12.5;
                else if(userPoint==7.4)
                    userPoint=12.4;
                else if(userPoint==7.3)
                    userPoint=12.3;
                else if(userPoint==7.2)
                    userPoint=12.2;
                else if(userPoint==7.1)
                    userPoint=12.1;

                $(".jslider").remove();
                $("#SliderSingle").remove();
                $("#SliderSingle_parent").append("<input id=\"SliderSingle\" type=\"slider\" value=\"1700\" style=\"width: 500px\" />");

                jQuery("#SliderSingle").slider({ from: min, to: max, step: 2, round: 5, format: {format: '##', locale: 'zh'}, dimension: '&nbsp;', skin: "round" ,
                    onstatechange: function( value ){
                        siderMove(value); 
                    }
                });

            });
        });
       
        jQuery("#SliderSingle").slider({ from: min, to: max, step: 2, round: 5, format: {format: '##', locale: 'zh'}, dimension: '&nbsp;', skin: "round" ,
            onstatechange: function( value ){
                siderMove(value); 
            }
        });
        $("#userpointset").blur(function(){
            var reg1 =  /^\d+$/;
            if($(this).val().match(reg1) == null)
                jQuery("#SliderSingle").slider("value", min);
            else
                jQuery("#SliderSingle").slider("value", $(this).val());
        })
        function siderMove(number){
          
            if(number=="")return;
            $("#userpointset").val( number );
            $("#keeppoint_pst").html((toDecimal(userPoint-(number-min)/2000*100.00))+"%");
            $(".lotterypoint").html(number);
            $("#line-length").width($(".jslider-pointer").css("left"));
        }
        function toDecimal(x) {  
            var f = parseFloat(x);  
            if (isNaN(f)) {  
                return;  
            }  
            f = Math.round(x*100)/100;  
            return f;  
        }  
        $(function () {
            Ytg.common.loading();
            var cldt = setInterval(function () {
                Ytg.common.cloading();
                clearInterval(cldt);
            }, 1000)
        })
    </script>

</asp:Content>
