<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.EditUser" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %></title>
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
    <link href="/Mobile/Css/layout.css" rel="stylesheet" />
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
    <link href="/Mobile/css/dialogUI.css" rel="stylesheet" type="text/css" />
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
        .tab-content dl, .div-content dl {line-height:20px;padding:2px 0px;}
        .tab-content dl dd {
    margin-left: 0px; 
    float: none;
}
        .tab-content dl dt {
    display: block;
    float: left;
    width: 25%;
    text-align: left;
    padding-left:0px;
    color: #333;
    font-weight: bold;
}
        .layout-slider {
    position: relative;
    width: 100%;
}
        .jslider {
    display: inline-block;
    width: 100%;
    height: 1em;
    position: relative;
    top: 0.6em;
    font-family: Arial, sans-serif;
}.input.normal {
    width: 98%; 
}
    </style>
</head>
<body>
     <nav class="col-xs-12 title" style="position:fixed;z-index:999;left:0px;top:0px;background:#ec2829;">
         <a id="J-goback" href="/wap/users/Main.aspx" class="go-back">返回</a>
         会员注册</nav>
    <form id="form1" runat="server">       
        <div  class="ctParent">
            <div class="tab-content" style="border-bottom: 1px solid #e1e1e1;">
                <dl>
                    <dt>用户级别 :</dt>
                    <dd>
                        <asp:Panel runat="server" ID="panelZd" Visible="false">
                            <input type="radio" value="0" id="dailiZong" checked="checked" name="level" /><label for="dailiZong">&nbsp;总代理</label>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="panelOther" style="text-align:left;">
                        <input type="radio" value="0" id="daili" checked="checked" name="level" /><label for="daili">&nbsp;代理用户&nbsp;&nbsp;</label>
                        <input type="radio" value="0" id="putong" name="level" /><label for="putong">&nbsp;会员用户</label>
                        </asp:Panel>
                    </dd>
                </dl>
                <dl id="playType" runat="server" visible="false">
                    <dt>奖金类型 :</dt>
                    <dd style="text-align:left;">
                        <input type="radio" value="0" id="playType1800" runat="server" checked="true" name="playType" /><label for="playType1800">&nbsp;1800</label>
                        <input type="radio" value="1" id="playType1700" runat="server" name="playType"  style="margin-left:27px;"/><label for="playType1700">&nbsp;1700</label>
                    </dd>
                </dl>
                <dl>
                    <dt>登陆名 :</dt>
                    <dd>
                        <input type="text" id="txtaccount" class="input normal" maxlength="16" datatype="*1-16" sucmsg=" " text="" nullmsg=" "  />
                        <br /><span class="Validform_checktip">*由字母或数字组成的6-16个字符,不能连续四位相同的字符</span>
                    </dd>
                </dl>
                <dl>
                    <dt>登陆密码 :</dt>
                    <dd>
                        <input type="password" id="txtPassword" class="input normal" maxlength="16" datatype="*1-16" sucmsg=" " text="" nullmsg=" " />
                        <br /><span class="Validform_checktip">*由字母和数字组成6-16个字符；且必须包含数字和字母</span>
                    </dd>
                </dl>
                <dl>
                    <dt>用户昵称 :</dt>
                    <dd>
                        <input type="text" id="nikeName" class="input normal" datatype="*1-6" maxlength="6" sucmsg=" " text="" nullmsg="" />
                        <br/><span class="Validform_checktip">*由2至6个字符组成</span>
                    </dd>
                </dl>
            </div>

            <div id="tabs-1" style="padding-left:50px;padding-right:50px;">
                <div style="height: 20px;"></div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="" style="padding: 10px 0 50px 0;">
                    <tr>
                        <td align="center" height="60">
                            <div class="layout-slider" id="SliderSingle_parent">
                                <div style="font-size: 12px; ">
                                    自身保留返点：<span id="keeppoint_pst">0</span>
                                    &nbsp;&nbsp;&nbsp;&nbsp;用户奖金组：<input type="text" id="userpointset" readonly="" name="keeppoint" style="width: 50px; height: 20px; border-color: transparent; text-align: left" value="1700" />
                                </div>
                                <input id="SliderSingle" type="slider" value="1700" style="width: 500px" />
                            </div>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hidValue" name="hidValue" />
                <div style="height: 55px;"></div>
                <div class="overflowHidden" style="display: block;"  id="customerUser1">
                <div class="market1_list" style="display: none;">
                    <ul  class="control_market">
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
                            <label id="Label7">北京快乐8</label>
                        </li>
                        <li>
                            <label id="Label8">福彩3D</label>
                        </li>
                        <li>
                            <label id="Label9">排列三</label>
                        </li>
                        <li>
                            <label id="Label10">天津时时彩</label>
                        </li>
                        <li>
                            <label id="Label11">幸运五分彩</label>
                        </li>
                        <li>
                            <label id="Label12">江苏快3</label>
                        </li>
                        <li>
                            <label id="Label13">幸运三分彩</label>
                        </li>
                        <li>
                            <label id="Label14">幸运分分彩</label>
                        </li>
                        <li>
                            <label id="Label15">香港六合彩</label>
                        </li>
                        <li>
                            <label id="Label16">幸运秒秒彩</label>
                        </li>
                        <li>
                            <label id="Label17">北京PK10</label>
                        </li>
                        <li>
                            <label id="Label18">五分11选5</label>
                        </li>
                        <li>
                            <label id="Label19">三分11选5</label>
                        </li>
                         <li>
                            <label id="Label20">埃及分分彩</label>
                        </li>
                        <li>
                            <label id="Label21">埃及二分彩</label>
                        </li>
                        <li>
                            <label id="Label22">埃及五分彩</label>
                        </li>
                         <li>
                            <label id="Label23">河内时时彩</label>
                        </li>
                        <li>
                            <label id="Label24">印尼时时彩</label>
                        </li>
                    </ul>
                </div>
            </div>
            </div>

            <div style="height: 20px;"></div>
            <div style="text-align:center;">
                <input  type="button" id="btnSubmit" value="确认注册" class="formWord"  style=""/>
            </div>
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
        var max1700=1700+<%=UserMaxRemo%>;
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

                if(userPoint==9.0)
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

</body>
</html>