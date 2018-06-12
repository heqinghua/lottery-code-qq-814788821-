<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TouZhu.aspx.cs" Inherits="Ytg.ServerWeb.Views.Activity.TouZhu.TouZhu" MasterPageFile="~/lotterySite.Master"%>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="Content/rotate/jQueryRotate.2.2.js"  type="text/javascript"></script>
<script src="Content/rotate/jquery.easing.min.js"  type="text/javascript"></script>
       <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <style type="text/css">
        .ltable td {font-size:12px;}
    </style>
    <script type="text/javascript">
        var isautoRefbanner = <%=isautoRefbanner?"true":"false"%>;
        $(function () {
            $("#refff").html(Ytg.tools.moneyFormat('<%=UserAmt%>'));//old amt
            $("#bottomState").remove();
            $("#actives").addClass("cur");
            $("#bottomState").remove();
        })
        function refchangemonery(){
            Ytg.common.user.refreshBalance();
        }
    </script>
    <style type="text/css">
        .yongbg {background:#e91f1a;min-height:900px;}
        .yongbg .content_ {}
     
        .yongbg .content_ p {color:#fff;font-size:14px;text-align:left;line-height:28px;padding-left:10px;}
        .yongbg .content_ p span {font-size:18px;font-weight:bold;}
        .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#781c1f;margin-top:10px;}
        	*{padding:0;margin:0}
	 .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#fff;margin-top:10px;}
	.ly-plate{ position:relative; width:509px;height:509px;margin: 50px ;}
	.rotate-bg{width:509px;height:509px;background:url(Content/rotate/ly-plate.png);position:absolute;top:0;left:0}
	.ly-plate div.lottery-star{width:214px;height:214px;position:absolute;top:150px;left:147px;/*text-indent:-999em;overflow:hidden;background:url(rotate-static.png);-webkit-transform:rotate(0deg);*/outline:none}
	.ly-plate div.lottery-star #lotteryBtn{cursor: pointer;position: absolute;top:0;left:0;*left:-107px}

/*.btn:hover {background: #dc51df;}*/
.topcomm {background:url('Content/Image/14628208692.jpg') no-repeat;width:1002px;height:175px;margin:auto;}
.topcomm p {color:#fff;font-size:16px;width:630px;margin:auto;text-align:left;padding-left:20px;text-indent:30px;}
.topcomm div {height:60px;}
.contenttable {width:1000px;margin:auto;background:rgba(254, 254, 254, 0.25);margin-bottom:20px;}
.ctTitle {background:url('Content/Image/146282086836.jpg') no-repeat;height:73px;width:1000px;background-position:center;background-position-x:center;margin-top:30px;}
.ctTitle h1 {color:#ff0600;font-weight:bold;font-size:25px;text-align:center;line-height:55px;}
.contentSt {text-align:center;color:#fff;font-size:16px;padding:20px;text-align:left;width:600px;margin:auto;font-weight:500;}
.btn{height:81px;width:286px;font-size:16px; background:url('Content/Image/146282086793.png') no-repeat;border:none;cursor:pointer;margin:auto;}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style type="text/css">
        body {color:#fff;}
    </style>
    <form id="Form1" runat="server">
    <div id="content" class="yongbg">
       <div style="height:450px;text-align:center;background:url('Content/Image/146282086875.jpg') no-repeat;background-position:center;background-position-x:center;"> </div>
       <div class="topcomm">
           <div ></div>
           <p>乐诚网推出投注送礼，前所未有的返利规则，为所有用户带来全新福利！</p>
           <p>只要您在前一天的投注达到活动要求，即可领取相应的礼包 ，投的也越多送的越多！</p>
       </div>
        
        <table class="contenttable">
            <tr>
                <td colspan="2">
                    <div class="ctTitle">
                        <h1>活动时间</h1>
                    </div>
                    <div class="contentSt" style="width:270px;">
                        每日<span>07:30:00</span>——次日凌晨<span>02:00:00</span>
                    </div>
                     <div class="ctTitle">
                        <h1>活动内容</h1>
                    </div>
                    <div class="contentSt" style="width:600px;">
                        乐诚网推出投注送礼包，前所未有的返利规则，为所有用户带来全新福利！<br />
                        只要您在前一天的投注达到活动要求，即可领取相应的礼包，投的越多送的越多！
                    </div>
                     <div class="ctTitle">
                        <h1>具体奖项</h1>
                    </div>
                    <div class="contentSt" style="width:310px;">
                            1、自身投注量达到1888，赠送礼包<span>8</span><br />
                            2、自身投注量达到18888，赠送礼包<span>68</span><br />
                            3、自身投注量达到188888，赠送礼包<span>688</span><br />
                            4、自身投注量达到888888，赠送礼包<span>2888</span><br />
                    </div>
                    <div class="ctTitle">
                        <h1>注意事项</h1>
                    </div>
                    <div class="contentSt">
                            1、活动时间为每日07:30:00 – 次日凌晨02:00:00 每日需要自行领取前一日投注投注礼包，逾期不候。<br />
                            2、任何包号或者80%以上的刷量投注行为不计入有效投注（<span style="color:#e5ed0f;">如：三星直选当期注单需小于800注，以此类推</span>）。<br />
                            3、乐诚网保留对此次活动做出的更改、终止权利，并享有最终解释权。<br />
                            4、领取时间:每日07:30:00——次日凌晨02:00:00。<br />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height:50px;"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="content_" style="text-align:center; ">
                        <asp:Button  ID="btnMe" runat="server" Text="" CssClass="btn" OnClick="btnMe_Click"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height:50px;"></td>
            </tr>
        </table>
         
    </div>
   </form>
     
</asp:Content>
