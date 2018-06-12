<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LotteryMain.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.LotteryMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>美亚娱乐、为梦想加速</title>
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; padding: 0px; margin: 0px;">
        <nav class="col-xs-12 title" style="position:fixed;z-index:999;">美亚娱乐、为梦想加速</nav>
        <div style="height:50px;"></div>
        <div class="container list-lottery" style="text-align:center;padding-top:50px;" id="lt_title_s" runat="server" visible="false">
            <span style="font-size:16px;">您的账号身份无法进行投注！</span>
        </div>
        <div class="container list-lottery" id="lotteryCenter" runat="server">

		    	<ul class="col-xs-12 list-demo">
                     <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=yifencai&ltid=11&ln=幸运分分彩&ico=lottery_ssc.png">
			    			<div class="thumb hot">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">幸运</span>
			    				<span class="lottery-name">分分彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">幸运分分彩</span>
			    				<span class="length-desc">每分钟一期，期期超幸运</span>
			    			</p>
			    		</a>
		    		</li>

		    		<li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=FFC1&ltid=13&ln=埃及分分彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">埃及</span>
			    				<span class="lottery-name">分分彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">埃及分分彩</span>
			    				<span class="length-desc">每分钟一期，期期超快感</span>
			    			</p>
			    		</a>
		    		</li>

                   <%-- <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=krkeno&ltid=15&ln=韩国1.5分彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">韩国</span>
			    				<span class="lottery-name">1.5分彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">韩国1.5分彩</span>
			    				<span class="length-desc">每一分半钟一期，期期超快感</span>
			    			</p>
			    		</a>
		    		</li>--%>

                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=FFC2&ltid=24&ln=埃及二分彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">埃及</span>
			    				<span class="lottery-name">二分彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">埃及二分彩</span>
			    				<span class="length-desc">两分钟一期，期期有惊喜</span>
			    			</p>
			    		</a>
		    		</li>
                   <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=FFC5&ltid=25&ln=埃及五分彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">埃及</span>
			    				<span class="lottery-name">五分彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">埃及五分彩</span>
			    				<span class="length-desc">五分钟一期，期期有惊喜</span>
			    			</p>
			    		</a>
		    		</li>
		    		<li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=cqssc&ltid=1&ln=重庆时时彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">重庆</span>
			    				<span class="lottery-name">时时彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">重庆时时彩</span>
			    				<span class="length-desc">一天120期</span>
			    			</p>
			    		</a>
		    		</li>
                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=VIFFC5&ltid=14&ln=河内时时彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">河内</span>
			    				<span class="lottery-name">时时彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">河内时时彩</span>
			    				<span class="length-desc">境外彩种，一天288期</span>
			    			</p>
			    		</a>
		    		</li>
                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=INFFC5&ltid=23&ln=印尼时时彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">印尼</span>
			    				<span class="lottery-name">时时彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">印尼时时彩</span>
			    				<span class="length-desc">境外彩种，一天288期</span>
			    			</p>
			    		</a>
		    		</li>
                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=xjssc&ltid=4&ln=新疆时时彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">新疆</span>
			    				<span class="lottery-name">时时彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">新疆时时彩</span>
			    				<span class="length-desc">一天96期</span>
			    			</p>
			    		</a>
		    		</li>
                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=tjssc&ltid=5&ln=天津时时彩&ico=lottery_ssc.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">天津</span>
			    				<span class="lottery-name">时时彩</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">天津时时彩</span>
			    				<span class="length-desc">一天120期</span>
			    			</p>
			    		</a>
		    		</li>

                    <li class="col-xs-12">
		    			<a href="/GameCenter.aspx?ltcode=sf11x5&ltid=17&ln=三分11选5&ico=lottery_11_5.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">三分</span>
			    				<span class="lottery-name">11选5</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">三分11选5</span>
			    				<span class="length-desc">三分钟/期，全天480期</span>
			    			</p>
			    		</a>
		    		</li>

                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=gd11x5&ltid=6&ln=广东11选5&ico=lottery_11_5.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">广东</span>
			    				<span class="lottery-name">11选5</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">广东11选5</span>
			    				<span class="length-desc">十分钟/期，全天84期</span>
			    			</p>
			    		</a>
		    		</li>

                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=jx11x5&ltid=20&ln=江西11选5&ico=lottery_11_5.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">江西</span>
			    				<span class="lottery-name">11选5</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">江西11选5</span>
			    				<span class="length-desc">十分钟/期，全天78期</span>
			    			</p>
			    		</a>
		    		</li>

                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=sd11x5&ltid=19&ln=山东11选5&ico=lottery_11_5.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">山东</span>
			    				<span class="lottery-name">11选5</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">山东11选5</span>
			    				<span class="length-desc">十分钟/期，全天78期</span>
			    			</p>
			    		</a>
		    		</li>

                   <%-- <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=fc3d&ltid=7&ln=福彩3D&ico=lottery_3d.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">福彩</span>
			    				<span class="lottery-name">3D</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">福彩3D</span>
			    				<span class="length-desc">一天/1期</span>
			    			</p>
			    		</a>
		    		</li>

                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=pl5&ltid=9&ln=排列三、五&ico=pl35.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">排列</span>
			    				<span class="lottery-name">三、五</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">排列三、五</span>
			    				<span class="length-desc">一天/1期</span>
			    			</p>
			    		</a>
		    		</li>--%>

                    <li class="col-xs-12">
		    			<a href="/mobile/GameCenter.aspx?ltcode=shssl&ltid=8&ln=上海时时乐&ico=lottery_ssl.png">
			    			<div class="thumb1 suggest">
			    				<div class="high-lights"></div>
			    				<span class="lottery-title">上海</span>
			    				<span class="lottery-name">时时乐</span>
			    			</div>
			    			<p>
			    				<span class="title-desc">上海时时乐</span>
			    				<span class="length-desc">全天23期</span>
			    			</p>
			    		</a>
		    		</li>

		    	</ul>
		    </div>
            <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
			      <ul class="nav navbar-nav col-xs-12">
                       <li class="active col-xs-4">
		        	    <a href="/Mobile/LotteryMain.aspx" style="color:#fff;">
		        		    <i class="fa fa-gamepad mr5" ></i>购彩大厅
		        	    </a>
		            </li>
                       <li class="col-xs-4"  id="proxy" runat="server">
		        	    <a href="/Mobile/Proxy.aspx" style="color:#fff;">
		        		    <i class="fa fa-volume-up mr5"></i>会员管理
		        	    </a>
		            </li>
		            <li class="col-xs-4">
		        	    <a href="/Mobile/user_center.aspx" style="color:#fff;">
		        		    <i class="fa fa-user mr5" ></i>用户中心
		        	    </a>
		            </li>
		          </ul>
			 </nav>
    </form>
</body>
</html>
