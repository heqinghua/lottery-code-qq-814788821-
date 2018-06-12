<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dzp.aspx.cs" Inherits="Ytg.ServerWeb.Views.Activity.Dzp.Dzp" MasterPageFile="~/lotterySite.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
  <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
      <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
     <script src="ContentDjp/rotate/jQueryRotate.2.2.js"  type="text/javascript"></script>
    <script src="ContentDjp/rotate/jquery.easing.min.js"  type="text/javascript"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/turntable.js"></script>
    <style type="text/css">
        html, body {
            background: #4253a4;
        }.yongbg .content_ {}
     
        .yongbg .content_ p {color:#fff;font-size:14px;text-align:left;line-height:28px;}
        .yongbg .content_ p span {font-size:18px;font-weight:bold;}
        .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#781c1f;margin-top:10px;}
        	*{padding:0;margin:0}
	 .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#fff;margin-top:10px;}
	.ly-plate{ position:relative; width:509px;height:509px;margin: 50px ;}
	.rotate-bg{width:509px;height:509px;background:url(Content/rotate/ly-plate.png);position:absolute;top:0;left:0}
	.ly-plate div.lottery-star{width:214px;height:214px;position:absolute;top:150px;left:147px;/*text-indent:-999em;overflow:hidden;background:url(rotate-static.png);-webkit-transform:rotate(0deg);*/outline:none}
	.ly-plate div.lottery-star #lotteryBtn{cursor: pointer;position: absolute;top:0;left:0;*left:-107px}

        .lottery {
            position: relative;
            display: inline-block;
            text-align: center;
        }

            .lottery img {
                position: absolute;
                top: 50%;
                left: 50%;
                margin-left: -76px;
                margin-top: -82px;
                cursor: pointer;
            }

        #message {
            position: absolute;
            top: 0px;
            left: 10%;
        }
    </style>
    <script type="text/javascript">

        $(function () {
            $("#bottomState").remove();
            $("#lottery_activity").addClass("cur");
            $("#bottomState").remove();
        })
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="Form1" runat="server">
        <div id="content" class="yongbg" style="padding-top:20px;">

            <div class="content_" id="titleParent">
                <p style="text-align: center; line-height: 50px;">您今天还有：<span id="subSpan"><%=SubCount %></span>&nbsp;次抽奖机会</p>
            </div>
            <table style="width: 1200px; margin: auto;">
                <tr>
                    <td>
                        <div class="lottery">
                            <canvas id="myCanvas" width="800" height="800">当前浏览器版本过低，请使用其他浏览器尝试
                            </canvas>
                            <p id="message"></p>
                            <img src="images/start.png" id="start">
                        </div>
                    </td>
                    <td>
                        <div class="content_">
                            <h1 class="h1style">活动内容</h1>
                            <p>
                                乐诚网推出全新嗨起来福利，转翻天，<br />
                                为所有用户带来全新福利！<br />
                            </p>
                            <h1 class="h1style">具体奖项</h1>
                            <p>
                                一等奖：<span>88</span>元&nbsp;&nbsp;&nbsp;&nbsp;
                            二等奖：<span>68</span>元&nbsp;&nbsp;&nbsp;&nbsp;
                            三等奖：<span>58</span>元&nbsp;&nbsp;&nbsp;&nbsp;
                            四等奖：<span>38</span>元&nbsp;&nbsp;&nbsp;&nbsp;
                            五等奖：<span>28</span>元&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                六等奖：<span>18</span>元&nbsp;&nbsp;&nbsp;&nbsp;
                            七等奖：<span>8.8</span>元&nbsp;&nbsp;&nbsp;&nbsp;
                            </p>
                            <h1 class="h1style">注意事项</h1>
                            <p>
                                1、 当日首冲200元以上，即可参与。<br />
                                2、点击抽奖后，或得相应金额且自动到达会员账户。<br />
                                3、用户投注注数不可大于该玩法总注数的70%，否则将不能参与该活动。<br />
                                4、平台风控将实时监控，禁止一切违反游戏规则的行为，一经发现平台有权冻结账号。<br />
                                4、本活动最终解释权为梦幻娱乐所有。<br />
                            </p>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script>
	var wheelSurf
	// 初始化装盘数据 正常情况下应该由后台返回
	var initData = {
		"success": true,
		"list": [
			{
				"id": 101,
				"name": "88元",
				"image": "images/3.png",
				"rank":2,
				"percent":5
			},
			{
				"id": 102,
				"name": "68元",
				"image": "images/3.png",
				"rank":3,
				"percent":2
			},
			{
				"id": 103,
				"name": "48元",
				"image": "images/3.png",
				"rank":4,
				"percent":49
			},
			{
				"id": 104,
				"name": "38元",
				"image": "images/3.png",
				"rank":5,
				"percent":30
			},
			{
				"id": 105,
				"name": "18元",
				"image": "images/3.png",
				"rank":6,
				"percent":1
			},
			{
				"id": 106,
				"name": "8.8",
				"image": "images/3.png",
				"rank":7,
				"percent":10
			},
			{
			    "id": 107,
			    "name": "谢谢参与",
			    "image": "images/3.png",
			    "rank":7,
			    "percent":10
			}
		]
	}

	// 计算分配获奖概率(前提所有奖品概率相加100%)
	function getGift(){
		var percent = Math.random()*100
		var totalPercent = 0
		for(var i = 0 ,l = initData.list.length;i<l;i++){
			totalPercent += initData.list[i].percent
			if(percent<=totalPercent){
				return initData.list[i]
			}
		}           
	}

	var list = {}
	
	var angel = 360 / initData.list.length
	// 格式化成插件需要的奖品列表格式
	for (var i = 0, l = initData.list.length; i < l; i++) {
		list[initData.list[i].rank] = {
			id:initData.list[i].id,
			name: initData.list[i].name,
			image: initData.list[i].image
		}
	}
	// 查看奖品列表格式
	
	// 定义转盘奖品
	wheelSurf = $('#myCanvas').WheelSurf({
		list: list, // 奖品 列表，(必填)
		outerCircle: {
			color: '#df1e15' // 外圈颜色(可选)
		},
		innerCircle: {
			color: '#f4ad26' // 里圈颜色(可选)
		},
		dots: ['#fbf0a9', '#fbb936'], // 装饰点颜色(可选)
		disk: ['#ffb933', '#ffe8b5', '#ffb933', '#ffd57c', '#ffb933', '#ffe8b5', '#ffd57c'], //中心奖盘的颜色，默认7彩(可选)
		title: {
			color: '#5c1e08',
			font: '19px Arial'
		} // 奖品标题样式(可选)
	})

	// 初始化转盘
	wheelSurf.init()
	// 抽奖
	var throttle = true;
	$("#start").on('click', function () {

		$.ajax({
		    url: "/Views/Activity/Dzp/Dzp.aspx",
		    type: 'post',
		    data: "action=ajx",
		    success: function (data) {

		        if (data == "")
		            return;
		        var dar = data.toString().split(',');
		        data = parseInt(dar[0]);
		        var p = dar[1];
		        var p1 = dar[2];
		        $("#subSpan").html(p1);
		        if (data == -1) {

		            $("#titleParent").removeAttr("style");
		            alert("您的剩余抽奖次数已经用完，无法继续抽奖！");
		            return;
		        } else if (data == -2) {
		            alert("活动尚未开始！");
		            return;
		        } else {

		            if(!throttle){
		                return false;
		            }
		            throttle = false;
		            var count = 0
		            // 计算奖品角度
		            var winData = null;
		            for (var i = 0; i < initData.list.length; i++) {
		              
		                if (p.toString() == initData.list[i].id.toString()) {
		                    winData = initData.list[i];
		                    break;
		                }
		                count++
		            }
		            if (winData == null)
                        count=6;
		            // 转盘抽奖，
		            wheelSurf.lottery((count* angel + angel / 2), function () {
		                //$("#message").html(winData)
		                if (winData == null) {
		                    alert("谢谢参与，请再接再厉！");
		                    throttle = true;
		                    return;
		                }
		                alert("恭喜抽中"+winData.name+"");
		                throttle = true;
		            })


		        }
		    }
		});
  
		
	})

	
</script>
</asp:Content>
