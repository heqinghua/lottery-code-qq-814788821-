<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proxy.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.Proxy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <title>代理中心</title>
    <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <style type="text/css">
	.fillet_ul{
		list-style-type:none;
		padding-top:20px;
		padding-left:0px;
	}
	.fillet_ul li{
		float:left;
		margin-top:10px;
		width:33%;
	}
	.fillet_ul li p{
		color:#64afa4;
	}
</style>
 
</head>
<body>
    <form id="form1" runat="server">
          <nav class="col-xs-12 title" style="position:fixed;z-index:999;">代理管理</nav>      
        <div class="fillet" >
            <div class="fillet container contents" style="padding-top: 60px;">
                <div class="RoundedCorner j panel panel-default" style="height: 350px">
                    <center>
		   				<ul class="fillet_ul">
					   		<li onclick="window.location.href='/Mobile/userCenter/EditUser.aspx'">
					   			<img src="/Mobile/images/register.png" width="60px">
					   			<p>会员注册</p>
					   		</li>
					   		<li onclick="window.location.href='/Mobile/userCenter/UsersList.aspx'">
					   			<img src="/Mobile/images/user2.png" width="60px">
					   			<p>下级会员</p>
					   		</li>
					   		<li onclick="window.location.href='/Mobile/userCenter/AutoRegistSetting.aspx'">
					   			<img src="/Mobile/images/share.png" width="60px">
					   			<p>注册推广</p>
					   		</li>
					   		<!-- <li onclick="window.location.href='/page/AgentPE.shtml'">
					   			<img src="/images/icon/moneymanage.png" width="60px"/>
					   			<p>配额管理</p>
					   		</li> -->
					   		<li onclick="window.location.href='/Mobile/userCenter/UserGroup.aspx'">
					   			<img src="/Mobile/images/balance.png" width="60px">
					   			<p>团队余额</p>
					   		</li>
					   		<li onclick="window.location.href='/Mobile/userCenter/ProfitLossList.aspx'">
					   			<img src="/Mobile/images/count.png" width="60px">
					   			<p>盈亏报表</p>
					   		</li>
					   		
							<li onclick="window.location.href='/Mobile/userCenter/customer.html?cl=Mobile&customerCode=<%=CookUserInfo.Code %>'">
					   			<img src="/Mobile/images/customer_service.png" width="60px">
					   			<p>在线客服</p>
					   		</li>
		   				</ul>
		   				</center>
                </div>
            </div>
        </div>
        <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
			      <ul class="nav navbar-nav col-xs-12">
                       <li class=" col-xs-4">
		        	    <a href="/Mobile/LotteryMain.aspx" style="color:#fff;">
		        		    <i class="fa fa-gamepad mr5" ></i>购彩大厅
		        	    </a>
		            </li>
                       <li class="col-xs-4 active" >
		        	    <a href="/Mobile/Proxy.aspx" style="color:#fff;">
		        		    <i class="fa fa-volume-up mr5"></i>会员管理
		        	    </a>
		            </li>
		            <li class="col-xs-4 ">
		        	    <a href="/Mobile/user_center.aspx" style="color:#fff;">
		        		    <i class="fa fa-user mr5" ></i>用户中心
		        	    </a>
		            </li>
		          </ul>
			 </nav>
    </form>
</body>
</html>
