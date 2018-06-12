<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adduser.aspx.cs" Inherits="Ytg.ServerWeb.AutoRegist" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>乐诚网</title>
    <meta name="keywords" content="" />
    <meta name="Description" content="" />
    <link rel="stylesheet" type="text/css" href="/Content/Css/style.css" />
    <link rel="stylesheet" type="text/css" href="/Content/Css/feile/main.css" />
    <script src="Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!--[if lte IE 6]><script src="Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <link href="templates/base.css" rel="stylesheet" />
    <link href="templates/login.css" rel="stylesheet" />
    <style type="text/css">
		
	.head{
		height:150px;
		background-color: #475ab3;
	}
  .yzm {
    position: absolute;
    right: 0;
    top: 0;
    width: 120px;
    height: 42px;
    line-height: 42px;
    text-align: center;
    color: #fff;
}
	.head h1{
		color: #fff;
	    font-size: 24px;
	    text-align: center;
	    margin-left: -80px;
	    height: 30px;
	    line-height: 30px;
	    margin-top: -90px;
	}
	.text {
	    width: 320px;
	  
	    padding: 11px 5px;
	    border: 1px solid #eae2e3;
	    border-radius: 2px;
	    box-shadow: -1px 1px 5px #d0d0d0 inset;
		background:none;
		color:#000;
	}
	/*邮箱提示*/
	ul, li {
		list-style:none;
	}
	.inputElem {
		width:198px;
		line-height:22px;
		border:1px solid #ccc;
	}
	.parentCls {
		display: inline-block;
	}
	.auto-tip{
		background-color: #fff;
		overflow: hidden;
	}
	.auto-tip li {
		width:100%;
		height:22px;
		line-height:22px;
		font-size:14px;
	    padding: 10px;
	    margin-bottom: 0px;
	}
	.auto-tip li.hoverBg {
		background:#ddd;
		cursor:pointer;
	}
	.red {
		color:#333;
	}
	.hidden {
		display:none;
	}
	.iconbox {
		position: absolute;
	    bottom: -40px;
	    width: 625px;
	    height: 66px;
	    left: 50%;
	    margin-left: -312.5px;
	    pointer-events: none;
		z-index: 999;
	}
	.icon_1 {
	    display: block;
	    float: left;
	    margin-right: 170px;
	    width: 28px;
	    height: 66px;
	    background: url(https://web.ba66666.com/client/templates/ba/images/newimg/icon_1.png) no-repeat;
	}
	.iconbox .icon_1.last {
	    margin-right: 0;
	}
	.main{
	    border-radius: 2px;
	    margin-top: 14px;
/* 	    background-color:#e6e6e6;
		border:1px solid #a1a1a1; */
		position: relative;
		overflow: hidden;
	}
	ul.v-line{
		width: 25px;
	    position: absolute;
	    right: 0;
	}	    
	ul.v-line li{
	    width: 100%;
	    height: 10px;
	    background-color: #929292;
		margin: 10px 0;
	}
	.pwd_tips {
	    width: 158px;
	    height: 71px;
	    padding-top: 6px;
	    position: absolute;
	    top: -6px;
	    background: url(//6.url.cn/zc/chs/img/pwd_sprite.png?v=10079) no-repeat;
	    right: 55px;
		display:none;
	}
	.pwd_tips div {
	    height: 22px;
	    line-height: 22px;
	    margin-left: 15px;
	    padding-left: 18px;
	}
	.pwd_tips .default {
	    background: url(//6.url.cn/zc/chs/img/pwd_sprite.png?v=10079) 0 -215px no-repeat;
	}
	.pwd_tips .no {
	    background: url(//6.url.cn/zc/chs/img/pwd_sprite.png?v=10079) 0 -281px no-repeat;
	}
	.red {
	    color: #F66;
	}
	.pwd_tips .yes {
    	background: url(//6.url.cn/zc/chs/img/pwd_sprite.png?v=10079) 0 -247px no-repeat;
	}
	.page_login .tip{
		position: absolute;	
	}
	.login-wrap{    
		margin-left: 265px;
	}
	.login-wrap a{
		color: #e4902e;
	}
</style>

</head>
<body >
    <div class="head">
		<img src="/images/logo/logo2.png" />
		<h1>乐诚网欢迎您的加入，请创建您的ID</h1>
	</div>
	<div class="main">
		<form autocomplete="off" id="form">
			<input type="hidden" name="id" value="NzAyNzYyXzE2NTQxMzA=">
			<ul class="reg_form">
				<li>
					<label><span class="red">*&nbsp;</span>用户名</label>
					<input type="text" class="text" name="txtCode" id="txtCode" placeholder="6-20位数字和字母组成，区分大小写" autocomplete="off" />
					<span class="tip" id="userts"><i></i></span>
				</li>
                <li>
					<label><span class="red">*&nbsp;</span>昵称</label>
					<input type="text" class="text" name="txtNickName" id="txtNickName" placeholder="6-20位数字和字母组成，区分大小写" autocomplete="off" />
					<span class="tip" id="userts"><i></i></span>
				</li>
				<li>
					<label><span class="red">*&nbsp;</span>密码</label>
					<input type="text" onfocus="this.type='password'" class="text" name="lgpwd" id="lgpwd" placeholder="6-16位数字和字母组成，区分大小写" autocomplete="off" />
					<!-- <span class="tip" id="tsmsg1"><i></i></span> -->
					<div class="pwd_tips" id="pwd_tips"> 
						<div class="default" id="pwd_tip1">长度为6-16个字符</div>
						<div class="default" id="pwd_tip2">数字和字母组成</div>
						<div class="default" id="pwd_tip3">不能包含空格</div>
					</div>
				</li>
				<li>
					<label><span class="red">*&nbsp;</span>资金密码</label>
					<input type="text" onfocus="this.type='password'" class="text" name="zjpwd" id="zjpwd" placeholder="资金密码不能与登录密码一致" autocomplete="off" />
					<span class="tip" id="tsmsg2"><i></i></span>
				</li>
				<li>
					<label><span class="red">*&nbsp;</span>QQ</label>
					<div class="parentCls">
						<input type="text" class="text inputElem" name="txtQQ" id="txtQQ" placeholder="填写真实QQ有助于找回密码" autocomplete="off" />
					</div>
					<span class="tip" id="tsmsg4"><i></i></span>
				</li>
				<li>
					<label><span class="red">*&nbsp;</span>手机</label>
					<input type="text" class="text" name="txtPhone" id="txtPhone" placeholder="填写真实手机号有助于找回密码" autocomplete="off" />
					<span class="tip" id="tsmsg5"><i></i></span>
				</li>
				<li>
					<label><span class="red">*&nbsp;</span>验证码</label>
					<input type="text" class="text" name="txtReCode" id="txtReCode" maxlength="4" autocomplete="off" placeholder="请填写右边的验证码"/>
					<div class="yzm" style="left:365px" ><img id='vcode'  style='cursor: pointer; float: right;' title="点击刷新" />
					</div>
					<span class="tip" id="tsmsg6"><i></i></span>
				</li>
				<li>
					<label>&nbsp;</label>
					<label style="width:auto;">
						<input type="checkbox" name="accept_provisions" value="1" checked="checked">
						我已届满合法游戏年龄，且同意各项开户条约。
					</label>
				</li>
				<li class=""><input type="button" id="nextbtn" class="btn"  value="立即注册" /></li>
				<li class=""><div class="login-wrap">已有账号?<a href="/login.html" target="_blank">直接登录</a></div></li>
			</ul>
		</form>

	</div>

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
            $.alert("请填写登录名！", 1, function () { $("#txtCode").focus(); });

            return;
        }
        if ($("#txtCode").val().length < 6) {
            $.alert("登录名长度必须为6位！", 1, function () {
                $("#txtCode").focus();
            });
            $("#txtCode").focus();
            return;
        }

        if ($("#txtPwd").val() == "") {
            $.alert("请填写登录昵称！", 1, function () {
                $("#txtPwd").focus();
            });

            return;
        }
        if ($("#lgpwd").val() == "") {
            $.alert("请填写登录密码！", 1, function () {
                $("#lgpwd").focus();
            });

            return;
        }
        if ($("#zjpwd").val() == "") {
            $.alert("请填写资金密码！", 1, function () {
                $("#zjpwd").focus();
            });

            return;
        }
        if($("#zjpwd").val()==$("#lgpwd").val() ){
            $.alert("资金密码不能与登录密码一致！", 1, function () {
                $("#zjpwd").focus();
            });

            return;
        }
        

        if ($("#txtReCode").val() == "") {
            $.alert("请填写验证码！", 1, function () {
                $("#txtReCode").focus();
            });

            return;
        }
        $("#divmsg").html("");
        
        if($("#li_qq").css("display")!="none"){
            if($("#txtQQ").val()==""){
                $.alert("请填写QQ号码！", 1, function () {
                    $("#txtQQ").focus();
                });
                return;
            }

            if($("#txtPhone").val()==""){
                $.alert("请填写电话号码！", 1, function () {
                    $("#txtPhone").focus();
                });
                return;
            }
            var rrs= checkmobile($("#txtPhone").val());
            if (rrs == 1) {
                $.alert("请填写电话号码！", 1, function () {
                    $("#txtPhone").focus();
                });
                return ;
            }

            if (rrs == 2) {
            $.alert("手机号码格式错误！", 1, function () {
                $("#txtPhone").focus();
            });
            return;
            }

        }

        var data = { "action": "autoregist", "Code": $("#txtCode").val(), "NickName": $("#txtNickName").val(), "VdaCode": $("#txtReCode").val(), "params": getPar("usercode"),"qq":$("#txtQQ").val(),"phone":$("#txtPhone").val(),"lgpwd":$("#lgpwd").val(),"zjpwd":$("#zjpwd").val() };
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
                    $.alert("非法请求！");
                } else if (data.Code == 1001) {
                    //账号禁用
                    $.alert("验证码错误！");
                } else if (data.Code == 1002) {
                    $.alert("账号已存在！", 1, function () {
                        $("#txtCode").select();
                        $("#txtCode").focus();
                    });
                }
                $("#txtReCode").val("");
                $("#vcode").attr("src", "CheckImage.aspx?date=" + new Date() + "&tp=autoRegist");
            },
            error: function () {
                $("#txtReCode").val("");
                $("#vcode").attr("src", "CheckImage.aspx?date=" + new Date() + "&tp=autoRegist");

                isLogining = false;
                $.alert("未知错误！");
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
