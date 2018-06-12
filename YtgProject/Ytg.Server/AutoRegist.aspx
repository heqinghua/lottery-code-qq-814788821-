<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRegist.aspx.cs" Inherits="Ytg.ServerWeb.AutoRegist" %>


<html >
<head>
    <title>中盛娱乐-自动注册</title>
     <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/style.css" />
    <link rel="stylesheet" type="text/css" href="/Content/Css/feile/main.css" />
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Css/login_comm.css" rel="stylesheet" />
     <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
    <!--头部结束-->
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
     <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>


</head>
<body style="background: #fff url('/css/14675605689.jpg');">
    <div id="login_header">
        <div class="login_header_in">
            <div class="logo_f"><a href="#">
                <img src="/Content/Images/baofa.png" /></a></div>
            <div class="header_right">
                <!--<a title="点击进入访问网络测试" class="a00" href="http://speed.zzz.ph/speed.html" target="_blank">测网速</a>-->
                <a class="a01" onclick="window.open('https://f18.livechatvalue.com/chat/chatClient/chatbox.jsp?companyID=650939&configID=59932&jid=1894921654&s=1', 'chatwindow', 'height=510,width=850');" href="#">在线客服</a>
                <a class="a02" onmouseover="phoneover();" onmouseout="phoneout();" href="javascript:;" target="_blank">手机端</a>
                <!-- <a class="a03" href="/apk/boyue.rar">客户端</a>-->
                <div class="equipment_sub" id="phone_id" style="margin: 10px 440px 0px 0px; display: none;">
                    <div class="equipment_sub_l">
                        <p>
                            Android版<br />
                            <span>扫描二维码下载</span>
                        </p>
                        <img src="/Css/apk.png" style="width: 100px;" />
                    </div>
                    <div class="equipment_sub_l">
                        <p>
                            iPhone版<br />
                            <span>扫描二维码下载</span>
                        </p>
                        <img src="/Css/wap.png" style="width: 100px;" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="login_conn" style="height: 470px; margin-top: 100px;">
        <div class="minghao">
            <img src="/Content/Images/baofa.png" alt="中盛娱乐" /></div>
        <div class="con condiv">
            <div class="left">
                <div class="banner-area" style="margin-top: 45px; margin-left: 38px;">
                    <script type="text/javascript" src="Content/Banner/BannerHtml.js"></script>
                </div>
            </div>
            <div class="right">
                <h1 style="">自动注册
                </h1>
                <div id="J-errorBox" class="sl-error sl-error-display">
                    <img src="/Content/Images/error.png" />
                    <span class="sl-error-text">dds</span>
                </div>
                <div id="loginfst">
                    <p>帐户</p>
                    <ul>
                        <li>
                            <input id="txtCode" name="txtCode" class="user" type="text" placeholder="请输入您的登录账号" /></li>
                        <li>
                            <p>昵称</p>
                            <input id="txtNickName" name="txtNickName" class="user" type="text" placeholder="请输入您的昵称" /></li>
                        <li>
                            <p>密码</p>
                            <input id="lgpwd" name="lgpwd" class="paw" type="password" placeholder="请输入您的账号密码" /></li>
                        <li>
                            <p>资金密码</p>
                            <input id="zjpwd" name="zjpwd" class="paw" type="password" placeholder="请输入资金密码" /></li>
                        <li id="li_qq">
                            <p>QQ</p>
                            <input id="txtQQ" name="txtQQ" class="user" type="text" placeholder="请输入您的联系QQ" onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " /></li>
                        <li id="li_phone">
                            <p>电话</p>
                            <input id="txtPhone" name="txtPhone" class="user" type="text" placeholder="请输入您的联系电话" onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " /></l>
                        <li>
                        <li>
                            <p>验证码</p>
                            <table style="padding: 0px; margin-left: -2px;">
                                <tr>
                                    <td style="padding: 0px; margin: 0px;">
                                        <input id="txtReCode" name="txtReCode" type="text" placeholder="请输入验证码" class="paw code" /></td>
                                    <td style="padding: 0px; margin: 0px;">
                                        <img width="78" id="vcode" style="cursor: pointer; height: 34px;" /></td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                    <p>
                        <input type="button" class="btn" id="nextbtn" value="确 认 注 册" style="margin: 0px; margin-top: 10px;" />
                        <!--<span style="color:red;font-weight:bold;font-size:13px;">默认密码：bf888888</span>-->
                    </p>
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>
    </div>

    <!-- <div id="footer" class="foot_s" style="position:absolute;margin-top:100px;">
               <div class="ft-shadow"></div>
               <div class="fs_c clearfix">
                   <div class="col-1" id="footer-col-1">
                       <p class="p1">运营资质&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 博彩责任</p>

                   </div>
                   <div class="ft-vline"></div>
               </div>
           </div>-->
</body>

</html>

<script type="text/javascript">
    var type = <%=inputType%>;
    $(document).ready(function () {
       
        if (type != 0) {
            $("#li_qq").show();
            $("#li_phone").show();
        }
        if($(window).height()>=700)
            $("#footer").css("top", ($(window).height() - 140));
        $("#vcode").attr("src", "CheckImage.aspx?date=" + new Date() + "&tp=autoRegist");
        $("#vcode").click(function () {
            $("#txtReCode").val("");
            $("#vcode").attr("src", "CheckImage.aspx?date=" + new Date() + "&tp=autoRegist");
        });

    });

    if($(window).height()>=700)
        window.onresize = function () {
            $("#footer").css("top", ($(window).height() - 140));
        }

    var isLogining = false;
    function getPar(par) {
        //获取当前URL
        var local_url = document.location.href;
        //获取要取得的get参数位置
        var get = local_url.indexOf(par + "=");
        if (get == -1) {
            return false;
        }
        //截取字符串
        var get_par = local_url.slice(par.length + get + 1);
        //判断截取后的字符串是否还有其他get参数
        var nextPar = get_par.indexOf("&");
        if (nextPar != -1) {
            get_par = get_par.slice(0, nextPar);
        }
        return get_par;
    }

    window.onkeydown = function (e) {
        var keycode = e.which;
        if (keycode == 13) {
            regist();
        }

    };
    $("#nextbtn").click(function () {
        
        regist();
    });

    function regist() {
        if (isLogining)
            return;

        if ($("#txtCode").val() == "") {
          
            alert("请填写登录名！");
                $("#txtCode").focus(); 

            return;
        }
        if ($("#txtCode").val().length < 6) {
            alert("登录名长度必须为6位！");
            $("#txtCode").focus();
            return;
        }

        if ($("#txtPwd").val() == "") {
            alert("请填写登录昵称！");
            $("#txtPwd").focus();
            return;
        }
        if ($("#lgpwd").val() == "") {
            alert("请填写登录密码！");$("#lgpwd").focus();

            return;
        }
        if ($("#zjpwd").val() == "") {
            alert("请填写资金密码！");$("#zjpwd").focus();

            return;
        }

        if($("#zjpwd").val()==$("#lgpwd").val()){
            alert("登录密码不能与资金密码相同！");$("#zjpwd").focus();

            return;
        }
        

        if ($("#txtReCode").val() == "") {
           alert("请填写验证码！");
           $("#txtReCode").focus();
            return;
        }
        $("#divmsg").html("");
        
        //if($("#li_qq").css("display")!="none"){
            if($("#txtQQ").val()==""){
               alert("请填写QQ号码！");
               $("#txtQQ").focus();
                return;
            }

            if($("#txtPhone").val()==""){
                alert("请填写电话号码！");
                $("#txtPhone").focus();
                return;
            }
            var rrs= checkmobile($("#txtPhone").val());
            if (rrs == 1) {
                alert("请填写电话号码！");
                $("#txtPhone").focus();
                return ;
            }

            if (rrs == 2) {
           alert("手机号码格式错误！");$("#txtPhone").focus();
            return;
            }

        //}

        var data = { "action": "autoregist", "Code": $("#txtCode").val(), "NickName": $("#txtNickName").val(), "VdaCode": $("#txtReCode").val(), "params": getPar("regsionUnqiue"),"qq":$("#txtQQ").val(),"phone":$("#txtPhone").val(),"lgpwd":$("#lgpwd").val(),"zjpwd":$("#zjpwd").val() };
        var dataStr = JSON.stringify(data);
        isLogining = true;
        $("#btnLogin").val("正在提交...")
        $.ajax({
            type: "POST",
            url: "/Page/Initial.aspx",
            data: data,
            success: function (result) {
                isLogining = false;
                $("#btnLogin").val("注册");
                var data = JSON.parse(result);
                if (data.Code == 0) {
                    //成功
                    alert("恭喜您，注册成功，正在进入平台！");
                    window.location = "/login.html";

                } else if (data.Code == 1) {
                    alert("非法请求！");
                } else if (data.Code == 1001) {
                    //账号禁用
                    alert("验证码错误！");
                } else if (data.Code == 1002) {
                    alert("账号已存在！");
                    $("#txtCode").select();
                    $("#txtCode").focus();
                }
                $("#txtReCode").val("");
                $("#vcode").attr("src", "CheckImage.aspx?date=" + new Date() + "&tp=autoRegist");
            },
            error: function () {
                $("#txtReCode").val("");
                $("#vcode").attr("src", "CheckImage.aspx?date=" + new Date() + "&tp=autoRegist");

                isLogining = false;
                alert("未知错误！");
                $("#btnLogin").val("注册");
            }
        })
    }

    function checkmobile(mobile) {
        var partten = /^1[0-9]{10}$/;
        if (mobile.length <= 0) {
            return 1;
        }

        if (!partten.test(mobile)) {
            return 2;
        }
        return 0;

    }
</script>
