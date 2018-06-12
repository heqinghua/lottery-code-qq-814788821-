<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Ytg.ServerWeb.Helper.Help" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="css/base.css" />
    <link rel="Stylesheet" href="css/help.css" />
    <script type="text/javascript" src="../Content/Scripts/jquery-1.7.min.js"></script>
    <script type="text/javascript">
        function GetHelpText(title,action)
        {
            try
            {
                $.post("../Views/Help/help.ashx", { 'title':title,'action': action }, function (data)
                {
                    $(".help-list-title").text(title);
                    $("#help").html(data);

                }, "html");
            } catch (ex)
            {
                alert(ex.message);  
            }
        }

        $(function () {
            GetHelpText('注册事项', 'zc');
        })

    </script>
</head>
<body>
   <div class="g_33">
	        <div class="help-main clearfix">
		        <div class="help-menu">
                    <div class="title">帮助中心</div>
    	            <div class="help-menu-inner">
		                <dl>
			                <dt>注册/登录</dt>
			                <dd>&nbsp;&nbsp;&nbsp;<a href="#" onclick="GetHelpText('注册事项','zc')">注册事项</a></dd>
			                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('登录事项','dl')">登录事项</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('验证码','yzm')">验证码</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('账号清除规则','zhqc')">账号清除规则</a></dd>
		                </dl>
		
		                <dl>
			                <dt>安全</dt>
			                <dd>&nbsp;&nbsp;&nbsp;<a href="#" onclick="GetHelpText('资金安全','zjaq')">资金安全</a></dd>
			                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('资金密码','zjmm')">资金密码</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('账号安全','zhaq')">账号安全</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('账号安全','wsyhaq')">网上银行安全</a></dd>
		                </dl>
		                <%--<dl>
			                    <dt>充值帮助</dt>
				                <dd>&nbsp;&nbsp;&nbsp;<a href="#" onclick="GetHelpText('充值注意事项','czzysx')">注意事项</a></dd>
				                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('充值限额','czxe')">充值限额</a></dd>
                                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('充值服务时间','czfwsj')">充值服务时间</a></dd>
                                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('银行卡','yhk')">银行卡</a></dd>
                                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('充值流程','czlc')">充值流程</a></dd>
		                </dl>
		                <dl>
	                            <dt>提款帮助</dt>
				                <dd>&nbsp;&nbsp;&nbsp;<a href="#" onclick="GetHelpText('提款','tk')">提款</a></dd>
				                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('提款规定','tkgd')">提款规定</a></dd>
		                </dl>--%>
                        <dl>
		                    <dt>投注帮助</dt>
			                <dd>&nbsp;&nbsp;&nbsp;<a href="#" onclick="GetHelpText('彩票代购','cpdg')">彩票代购</a></dd>
			                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('投注撤单','tzcd')">投注撤单</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('赔付限额','tzxe')">投注限额</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('销售时间','xsrj')">销售时间</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('购买记录','gmjl')">购买记录</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('恶意投注','eytz')">恶意投注</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('追号投注','zhtz')">追号投注</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('单式投注','dstz')">单式投注</a></dd>
                            <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('未开奖处理','wkjcl')">未开奖处理</a></dd>
		                </dl>
                         <dl>
		                    <dt>团队管理</dt>
			                <dd>&nbsp;&nbsp;&nbsp;<a href="#" onclick="GetHelpText('圈子管理','qzgl')">圈子管理</a></dd>
			                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('增加用户','zjyh')">增加用户</a></dd>
		                </dl>
                        <dl>
		                    <dt>在线帮助</dt>
			                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('聊天工具','ltgj')">聊天工具</a></dd>
			                <dd>|&nbsp;&nbsp;<a href="#" onclick="GetHelpText('在线客服','zxkf')">在线客服</a></dd>
		                </dl>
	                </div>
		        </div>
		        <div class="help-container">
			        <!-- 从被装饰页面获取body标签内容 -->	
	                <div class="clearfix">
		                <div id="help" class="help-container help-article-container" style="overflow:auto; max-height:700px;"></div>
	                </div>
		        </div>
	        </div>
        </div>
</body>
</html>
