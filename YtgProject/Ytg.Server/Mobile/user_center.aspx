<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_center.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.user_center" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <title>用户中心</title>
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="/Content/Scripts/jquery.sortable.js"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
     <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />

    <style type="text/css">
	.option{
		width:89%;
		margin-top:10px;
		margin-left:20px;
		background:rgba(255, 255, 255, 1) none repeat scroll 0 0 !important;
		filter:Alpha(opacity=80);
		background:#fff;
		border-radius:5px 5px 5px 5px ;
		color:green;
		font-size:18px;
		font-family:'微软雅黑';
		height:38px;
		line-height:38px;
	}
	</style>
    <script type="text/javascript">
        Ytg = $.extend(Ytg, {
            SITENAME: "<%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %>",
             SITEURL: window.location.host,
             RESOURCEURL: "/Content",
             BASEURL: "/",
             SERVICEURL: "",
             NOTEFREQUENCY: 10000
         });
         // Ytg.namespace("Ytg.Lottery.user");
         Ytg.common.user.info = {
             user_id: '<%=CookUserInfo.Id%>',
            username: '<%=CookUserInfo.Code%>',
            nickname: '<%=CookUserInfo.NikeName%>'
         };
     function inintUser_ban() {
         $.ajax({
             type: 'POST',
             url: '/page/Initial.aspx?action=userbalance',
             data: { "uid": Ytg.common.user.info.user_id},
             timeout: 10000, success: function (data) {
                 var jsonData = JSON.parse(data);                    
                 if (jsonData.Code == 0) {
                     //获取成功
                     $("#xd_user_bake").html(Ytg.tools.moneyFormat(jsonData.Data.UserAmt));
                 } else if (jsonData.Code == 1009) {
                     //$.alert("由于您长时间未操作,为确保安全,请重新登录！", 1, function () {
                     //    window.location = "/login.html";
                     //});
                 } else {
                     return false;
                 }
             },
             error: function () {

             }
         });
     }
     $(function () {
         inintUser_ban();
     })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="col-xs-12 title" style="position:fixed;z-index:999;">用户中心</nav>
        <div style="height:50px;"></div>

        <table width="89%" height="70px" style="margin-top: 20px; margin-left: 20px; background: rgba(255, 255, 255, 0.8) none repeat scroll 0 0 !important; filter: Alpha(opacity=80); background: #fff; border-radius: 5px 5px 5px 5px;">
            <tbody>
                <tr>
                    <td rowspan="4" style="height: 80px">
                        <img style="margin-left: 10px; margin-top: 3px;" src="/Mobile/images/man2.png" width="70px"></td>
                    <td colspan="2" height="5px"></td>
                </tr>
                <tr>
                    <td height="30px" style="font-size: 14px; font-weight: 900; text-indent: 5px; letter-spacing: 3px; color: #136e61">会员账号:</td>
                    <td align="left">
                        <font color="blue"><%=CookUserInfo.NikeName%></font>
                    </td>
                </tr>
                <tr>
                    <td height="30px" style="font-size: 14px; font-weight: 900; text-indent: 5px; letter-spacing: 3px; color: #136e61">可用余额:</td>
                    <td align="left">
                        <font color="blue" id="xd_user_bake"></font>
                    </td>
                </tr>
                <tr style="BORDER-BOTTOM: 1px dashed #64afa4;">
                    <td colspan="2" height="5px"></td>
                </tr>
                <tr>
                    <td align="center" colspan="3" height="50px">
                        <input onclick="window.location.href = '/Mobile/pay/Payment.aspx'" type="button" value="充值" style="color: #FFFFFF; font-size: 15px; background-color: #5f728d; font-family: Microsoft YaHei,'宋体' , Tahoma, Helvetica, Arial, '\5b8b\4f53', sans-serif; border: 0; height: 30px; width: 100px; border-radius: 5px 5px 5px 5px; margin-top: 10px;">
                        <input onclick="window.location.href = '/Mobile/user/Mention.aspx'" type="button" value="提现" style="color: #FFFFFF; font-size: 15px; background-color: #5f728d; font-family: Microsoft YaHei,'宋体' , Tahoma, Helvetica, Arial, '\5b8b\4f53', sans-serif; border: 0; height: 30px; width: 100px; border-radius: 5px 5px 5px 5px; margin-left: 30px;">
                    </td>
                </tr>
            </tbody>
        </table>
         <div class="option" style="margin-top: 25px;">
            <a href="/wap/activitylist.html" style="font-size: 18px; font-family: '微软雅黑'; height: 38px; line-height: 38px;">
                <div>
                    <img src="/Mobile/images/balance.png" width="28px" style="margin-left: 15px; margin-right: 15px">活动中心
				   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px; margin-top: 8px; float: right; filter: alpha(opacity=50); opacity: 0.5;">
                </div>
            </a>
        </div>
       
        <div class="option" style="margin-top: 25px;">
            <a href="/Mobile/userCenter/UpdatePwd.aspx" style="font-size: 18px; font-family: '微软雅黑'; height: 38px; line-height: 38px;">
                <div>
                    <img src="/Mobile/images/userUpdate.png" width="28px" style="margin-left: 15px; margin-right: 15px">修改资料
				   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px; margin-top: 8px; float: right; filter: alpha(opacity=50); opacity: 0.5;">
                </div>
            </a>
        </div>
        <div class="option">
		   		<a href="/Mobile/userCenter/MyMessage.aspx" style="font-size:18px;font-family:'微软雅黑';height:38px;line-height:38px;"><div>
		   		<img src="/Mobile/images/message.png" width="28px" style="margin-left: 15px;margin-right: 15px">我的消息
		   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px;margin-top: 8px;float: right;filter:alpha(opacity=50);opacity:0.5;">
		   		</div></a>
		</div>

        <div class="option">
		   		<a href="/Mobile//userCenter/Betting.aspx" style="font-size:18px;font-family:'微软雅黑';height:38px;line-height:38px;"><div>
		   		<img src="/Mobile/images/betSearch.png" width="28px" style="margin-left: 15px;margin-right: 15px">投注记录
		   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px;margin-top: 8px;float: right;filter:alpha(opacity=50);opacity:0.5;">
		   		</div></a>
		   	</div>

        <div class="option">
		   		<a href="/Mobile//userCenter/Betting.aspx?type=catch" style="font-size:18px;font-family:'微软雅黑';height:38px;line-height:38px;"><div>
		   		<img src="/Mobile/images/search.png" width="28px" style="margin-left: 15px;margin-right: 15px">追号记录
		   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px;margin-top: 8px;float: right;filter:alpha(opacity=50);opacity:0.5;">
		   		</div></a>
		   	</div>


        <div class="option">
		   		<a href="/Mobile/user/BindBankCard.aspx" style="font-size:18px;font-family:'微软雅黑';height:38px;line-height:38px;"><div>
		   		<img src="/Mobile/images/card.png" width="28px" style="margin-left: 15px;margin-right: 15px">银行卡管理
		   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px;margin-top: 8px;float: right;filter:alpha(opacity=50);opacity:0.5;">
		   		</div></a>
		   	</div>

        <div class="option">
		   		<a href="/Mobile/userCenter/AmountChangeList.aspx" style="font-size:18px;font-family:'微软雅黑';height:38px;line-height:38px;"><div>
		   		<img src="/Mobile/images/stat.png" width="28px" style="margin-left: 15px;margin-right: 15px">帐变信息
		   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px;margin-top: 8px;float: right;filter:alpha(opacity=50);opacity:0.5;">
		   		</div></a>
		   	</div>

        <div class="option">
		   		<a href="/Mobile/userCenter/News.aspx"><div>
		   		<img src="/Mobile/images/date.png" width="28px" style="margin-left: 15px;margin-right: 15px">系统公告
		   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px;margin-top: 8px;float: right;filter:alpha(opacity=50);opacity:0.5;">
		   		</div></a>
		   	</div>

        <div class="option"><a href="#" id="userCenter:j_id7" name="userCenter:j_id7" onclick="javascript:logout();" title="注销登录">
			   		<div>
				   		<img src="/Mobile/images/exit.png" width="28px" style="margin-left: 15px;margin-right: 15px">注销登录
				   		<img src="/Mobile/images/right.jpg" width="28px" style="margin-right: 5px;margin-top: 8px;float: right;filter:alpha(opacity=50);opacity:0.5;">
			   		</div></a>
		   	</div>

        <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
			      <ul class="nav navbar-nav col-xs-12">
                       <li class=" col-xs-4">
		        	    <a href="/Mobile/LotteryMain.aspx" style="color:#fff;">
		        		    <i class="fa fa-gamepad mr5" ></i>购彩大厅
		        	    </a>
		            </li>
                       <li class="col-xs-4" >
		        	    <a href="/Mobile/Proxy.aspx" style="color:#fff;">
		        		    <i class="fa fa-volume-up mr5"></i>会员管理
		        	    </a>
		            </li>
		            <li class="col-xs-4 active">
		        	    <a href="/Mobile/user_center.aspx" style="color:#fff;">
		        		    <i class="fa fa-user mr5" ></i>用户中心
		        	    </a>
		            </li>
		          </ul>
			 </nav>
    </form>
</body>
</html>
<script type="text/javascript">
    function logout() {
        //
        if (confirm("确定要退出吗？")) {
            $.ajax({
                url: "/Page/Initial.aspx",
                type: 'post',
                data: "action=logout",
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    if (jsonData.Code == 0) {
                        window.location = "/Mobile/login.html";
                    }
                }
            });
        }
        window.location = "/Mobile/login.html";
    }
</script>