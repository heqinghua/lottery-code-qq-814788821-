<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RgzPage.aspx.cs" Inherits="Ytg.ServerWeb.Views.Activity.rgz.RgzPage"  MasterPageFile="~/lotterySite.Master"%>

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
        $(function () {
            $("#bottomState").remove();
            $("#actives").addClass("cur");
            $("#bottomState").remove();
        })
        function refchangemonery(){
            Ytg.common.user.refreshBalance();
        }
    </script>
    <style type="text/css">
        .yongbg {background:#ff0042;min-height:900px;}
        .yongbg .content_ {}
     
        .yongbg .content_ p {color:#fff;font-size:14px;text-align:left;line-height:28px;padding-left:10px;}
        .yongbg .content_ p span {font-size:18px;font-weight:bold;}
        .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#781c1f;margin-top:10px;}
        	*{padding:0;margin:0}
	 .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#fff;margin-top:10px;}
	

/*.btn:hover {background: #dc51df;}*/
.topcomm {background:url('http://cfapu.img48.wal8.com/img48/545266_20160510012323/14628208692.jpg') no-repeat;width:1002px;height:175px;margin:auto;}
.topcomm p {color:#fff;font-size:16px;width:630px;margin:auto;text-align:left;padding-left:20px;text-indent:30px;}
.topcomm div {height:60px;}
.contenttable {width:1000px;margin:auto;background:rgba(254, 254, 254, 0.25);margin-bottom:20px;}
.ctTitle {background:url('http://cfapu.img48.wal8.com/img48/545266_20160510012323/146282086836.jpg') no-repeat;height:73px;width:1000px;background-position:center;background-position-x:center;margin-top:30px;}
.ctTitle h1 {color:#ff0600;font-weight:bold;font-size:25px;text-align:center;line-height:55px;}
.contentSt {text-align:center;color:#fff;font-size:16px;padding:20px;text-align:left;width:600px;margin:auto;font-weight:500;}
.btn{height:51px;width:150px;font-size:16px; background:#0a8cfa;border:none;cursor:pointer;margin:auto;color:#fff;}
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style type="text/css">
        body {color:#fff;}
    </style>
    <form id="Form1" runat="server">
    <div id="content" class="yongbg">
       <div style="height:450px;text-align:center;background:url('/Views/Activity/rgz/content/image/rgztitle.png') no-repeat;background-position:center;background-position-x:center;"> </div>

        <table class="contenttable">
            <tr>
                <td colspan="2">
                    <div class="ctTitle">
                        <h1><asp:Literal ID="ltTl" Text="总代奖励" runat="server"></asp:Literal></h1>
                    </div>
                    <div class="contentSt" style="width:410px;">
                        <span>1、除自身消费量外，有明显团队结构并非刷量行为。</span><br />
                        <span>2、当天达到规定消费标准与对应人数不计亏损。</span><br />
                        <span>3、领取时间在次日<span style="font-weight:bold;">&nbsp;03:00&nbsp;</span>前自行领取，过期无效清零。</span><br />
                        <span>4、最终解释权归乐诚网所有。</span>
                    </div>
                     <div class="ctTitle">
                        <h1><asp:Literal ID="ltGz" Text="总代工资" runat="server"></asp:Literal></h1>
                    </div>
                    <div class="contentSt" style="width:600px;">
                        <table width="100%" class="grayTable" border="0" cellspacing="0" cellpadding="0" runat="server" id="ltZd" visible="false">
                            <tr>
                                <th>级别</th>
                                <th>团队日量</th>
                                <th>消费人数</th>
                                <th>奖励（元）</th>
                            </tr>
                            <tr>
                                <td>总代（1958）</td>
                                <td>10000元</td>
                                <td>≥3</td>
                                <td>≥100元</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="font-weight:bold;color:red;">工资以此类推无阶梯，无上限</td>
                            </tr>
                        </table>
                         <table width="100%" class="grayTable" border="0" cellspacing="0" cellpadding="0" runat="server" id="ltZs" visible="false">
                            <tr>
                                <th>级别</th>
                                <th>团队日量</th>
                                <th>消费人数</th>
                                <th>奖励（元）</th>
                            </tr>
                            <tr>
                                <td>总代（1960）</td>
                                <td>50000元</td>
                                <td>≥5</td>
                                <td>≥150元(0.3%)</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="font-weight:bold;color:red;">工资以此类推无阶梯，无上限</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td></td>
                <td>
                    <div class="content_" style="text-align:center;font-size:16px; ">
                        昨日团队消费量：<asp:Label ID="lbbetMonery" runat="server" Text="0.0000"></asp:Label> 元    消费人数:<asp:Label ID="lbCountSum" runat="server" Text="0"></asp:Label>   奖励金额:<asp:Label ID="lbMonery" runat="server" Text="0.0000"></asp:Label>
                        <div style="height:20px;"></div>
                        <asp:Button  ID="btnMe" runat="server" Text="领取工资" CssClass="btn"/>
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