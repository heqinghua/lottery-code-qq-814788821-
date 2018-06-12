<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ytg.ServerWeb.Default" MasterPageFile="~/lotterySite.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/Scripts/slider.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.event.move.js"></script>
    <script src="/Content/Scripts/jquery.event.swipe.js"></script>
    <script src="/Content/Scripts/unslider.min.js"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <script type="text/javascript">
        $(function () {
            $("#home_").addClass("cur");
            var hg = $(window).height() - 100;
            $("#content").css("min-height", hg);
            $("#bottomState").remove();
            $(".page2Ul").css({ "padding-top": 20, "padding-left": ($(window).width() / 2 - $(".page2Ul").width() / 2) });

            if (window.chrome) {
                $('.banner li').css('background-size', '100% 100%');
            }

            $('.banner').unslider({
                arrows: false,
                fluid: true,
                dots: true
            });
           
            if ("<%=newsid%>" != "-1") {
                //弹出新闻
                showNewDetails('<%=title%>', <%=newsid%>);
            }
        });

        function closeDialog(){
            
            $.dialog({id:'open_news'}).close();
        }


        function showNewDetails(title,id){
            $.dialog({
                id: 'open_news',
                fixed: true,
                lock: false,
                max: false,
                min: false,
                width:600,
                height:310,
                title: title,
                content: 'url:/views/NewsDetails.aspx?id=' + id
             });
        }

    </script>
    <style type="text/css">
.flUltable {width:1000px;margin:auto;text-align:center;}
.controldiv {border: 1px solid #e6e6e6;width:230px;height:320px;}
.controldiv img {width:220px;}
.controldiv .title {color: #000;font-size: 25px;height: 50px;line-height: 50px;}
.controldiv p {margin: auto;text-align: left;padding: 5px;}


 .mod-broadcast {background: #e6e6e6;height: 40px;line-height: 40px;margin-top:415px;}
.mod-broadcast .mod-inner {padding: 0;margin: 0 auto;max-width:1200px;}
.sec1about { margin-top: 80px;}
.sec1about div {color: #000;font-size: 15px;}
.fssubTitle {font-size: 25px;font-family: 'Microsoft YaHei';color: #000;}
.media {margin: auto;width: 1000px;}
.media li {list-style: disc;float: left;text-align: center;margin-left: 10px;margin-top: 5px;}
 .news {}
 .news li {width:49%;text-align:left;}
 .news li .rigtdic {float:right;padding-right:50px;}
 .news li a {color:#515151;font-size:13px;}
 .news li a:hover {color:#cc0228;}
 .mod-inner ul li {float:left;}
.bg_color div a img:hover {opacity:0.6;filter:alpha(opacity=60);}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content" style="background: #fff;">
        <div id="sec">
            <div class="banner" style="border: 0px solid #fff;">
                <ul>
                    <li style="background-image: url('/Css/new/1462819179.jpg');">
                        <a href="/Views/Activity/QianDao/QianDao.aspx">
                            <div class="inner" style="height: 419px;">
                            </div>
                        </a>
                    </li>
                    <li style="background-image: url('/Css/new/146908360984.jpg');">
                        <a href="/Views/Activity/TouZhu/TouZhu.aspx">
                            <div class="inner" style="height: 419px;"></div>
                        </a>
                    </li>
                    <li style="background-image: url('/Css/new/146908322041.jpg');">
                        <a href="/Views/Activity/YongJin/YongJin.aspx">
                            <div class="inner" style="height: 419px;">
                            </div>
                        </a>
                    </li>

                </ul>
            </div>
        </div>
        
        <%--<div class="mod-wrap mod-broadcast" style="background-image:url(Content/Images/eycbgIcon.png)">--%>
        <div class="mod-wrap mod-broadcast">
            <div class="mod-inner">
            </div>
        </div>

            
        <script type="text/javascript">

            $(function(){
                $("#ssc").mouseenter(function(){
                    $(this).hide();  
                    $("#ssc1").show().mouseleave(function(){
                        $(this).hide();  
                        $("#ssc").show();
                    })
                });

                $("#syxw").mouseenter(function(){
                    $(this).hide();  
                    $("#syxw1").show().mouseleave(function(){
                        $(this).hide();  
                        $("#syxw").show();
                    })
                });

                $("#klb").mouseenter(function(){
                    $(this).hide();  
                    $("#klb1").show().mouseleave(function(){
                        $(this).hide();  
                        $("#klb").show();
                    })
                });

                $("#jwgp").mouseenter(function(){
                    $(this).hide();  
                    $("#jwgp1").show().mouseleave(function()
                    {
                        $(this).hide();  
                        $("#jwgp").show();
                    })
                });
            });

        </script>

             <div style="margin-right:auto;margin-left:auto; width:1024px; vertical-align:middle;  margin-top:20px;">
                 <div id="caizhong">
                     <div style="float:left;">
                         <div id="ssc" style="background-image:url(/Css/new/146281931118.png); background-position: -130px 0px; width:255px; height:350px; cursor:pointer; display:block"></div>
                             <div class="bg_color" id="ssc1" style="background-image:url(/Css/new/146281931076.png); background-position: -130px -3px; width:255px; height:350px; cursor:pointer; display:none">
                                 <br />
                                 <div>
                                      <a title="重庆时时彩" href="<%=cqssc %>">
                                          <img id="cqssc" style="width:95px; height:95px;" src="/Css/new/146281931041.png" />
                                      </a> 
                                      <a title="新疆时时彩" href="<%=xjssc %>">
                                          <img id="xjssc" style="width:95px; height:95px;" src="/Css/new/146281931709.png" />
                                      </a>
                                 </div>
                                 <div>
                                     <a title="天津时时彩" href="<%=tjssc %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/14628193161.png" />
                                     </a>
                                     <a title="幸运分分彩" href="<%=xyffc %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931742.png" />
                                     </a>
                                 </div>
                                 <div>
                                     <a title="幸运三分彩" href="<%=xysfc %>">
                                          <img style="width:95px; height:95px;" src="/Css/new/146281931775.png" />
                                     </a>
                                     <a title="幸运五分彩" href="<%=xywfc %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931811.png" />
                                     </a>
                                 </div>
                             </div>
                     </div>
                     <div style=" float:left;">
                        <div id="syxw" style="background-image:url(/Css/new/146281931167.png); background-position: -130px 0px; width:255px; height:350px; cursor:pointer;"></div>
                               <div class="bg_color" id="syxw1" style="background-image:url(/Css/new/146281931076.png); background-position: -130px -3px; width:255px; height:350px; cursor:pointer; display:none">
                                 <br />
                                 <div >
                                      <a title="广东11选5" href="<%=gd11x5 %>">
                                          <img style="width:95px; height:95px;" src="/Css/new/146281931302.png" />
                                      </a>

                                      <a title="江西11选5" href="<%=jx11x5 %>">
                                      <img style="width:95px; height:95px;" src="/Css/new/146281931402.png" />
                                      </a>
                                 </div>
                                 <div>
                                     <a title="山东11选5" href="<%=sd11x5 %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931508.png" />
                                     </a>

                                     <a title="三分11选5" href="<%=sf11x5 %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931541.png" />
                                     </a>
                                 </div>
                                 <div>
                                     <a title="五分11选" href="<%=wf11x5 %>">
                                          <img style="width:95px; height:95px;" src="/Css/new/146281931643.png" />
                                     </a>
                                 </div>
                             </div>
                     </div>
                     <div style=" float:left;">
                         <div id="klb" style="background-image:url(/Css/new/146281931217.png); background-position: -130px 0px; width:255px; height:350px; cursor:pointer;"></div>
                             <div class="bg_color" id="klb1" style="background-image:url(/Css/new/146281931076.png); background-position: -130px -3px; width:255px; height:350px; cursor:pointer; display:none">
                                 <br />
                                 <div>
                                     <a title="福彩3D" href="<%=fc3d %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931476.png" />
                                     </a>

                                      <a title="排列三、五" href="<%=pl5 %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931434.png" />
                                      </a>
                                 </div>
                                 <div>
                                      <a title="上海时时乐" href="<%=shssl %>">
                                      <img style="width:95px; height:95px;" src="/Css/new/146281931575.png" />
                                       </a>

                                      <a title="香港六合彩" href="<%=xglhc %>">
                                      <img style="width:95px; height:95px;" src="/Css/new/146281931675.png" />
                                      </a>
                                 </div>
                                 <div>
                                     <a title="江苏快三" href="<%=jsk3 %>">
                                        <img style="width:95px; height:95px;" src="/Css/new/146281931368.png" />
                                     </a>
                                 </div>  
                             </div>
                     </div>
                     <div style=" float:left;">
                         <div id="jwgp" style="background-image:url(/Css/new/146281931265.png); background-position: -130px 0px; width:255px; height:350px; cursor:pointer;"></div>
                              <div class="bg_color" id="jwgp1" style="background-image:url(/Css/new/146281931076.png); background-position: -130px -3px; width:255px; height:350px; cursor:pointer; display:none">
                                 <br />
                                 <div>
                                      <a title="埃及分分彩" href="<%=ajffc %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281930977.png" />
                                      </a>

                                      <a title="埃及二分彩" href="<%=ajefc %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281930944.png" />
                                      </a>
                                 </div>
                                 <div>
                                      <a title="埃及五分彩" href="<%=ajwfc %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931007.png" />
                                      </a>

                                      <a title="河内时时彩" href="<%=hlssc %>">
                                         <img style="width:95px; height:95px;" src="/Css/new/146281931336.png" />
                                      </a>
                                 </div>
                                 <div>
                                     <a title="印尼时时彩" href="<%=ynssc %>">
                                       <img style="width:95px; height:95px;" src="/Css/new/146281931843.png" />
                                     </a>
                                 </div>
                             </div>
                     </div>
                </div>
                
                <div>
                     <div style="margin-right:5px; margin-left:5px; margin-top:8px;">
                        <div style="font-size:18px; text-align:left;"><br />&nbsp;&nbsp;&nbsp;<span style="color:black; ">公告</span></div>
                        <div style="height:2px; margin-top:2px; background-color:#cccccc"></div>
                        <div style="height:2px; margin-top:2px; width:65px; margin-left:6px; background-color:#cc0228; position:relative; left:8px; top:-4px;"></div>
                    </div>
                    <div style="width:100%; ">
                        <br />
                        <ul class="media news">
                            <asp:Repeater ID="rptnews" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <a href="javascript:showNewDetails('<%# Eval("Title")%>',<%# Eval("id")%>)" >
                                        <div style="float:left; width:320px;">[公告]<%# Eval("Title")%></div>
                                        <div class="rigtdic"><%# Convert.ToDateTime(Eval("OccDate")).ToString("yyyy-MM-dd")%></span></div>  
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li style="list-style:none;"><br /><br /></li>
                        </ul>
                        <%--<div class="cleanall"></div>--%>
                    </div>
                </div>
       </div>
    </div>
    <style type="text/css">
        #popHead {height:32px;}
    </style>
</asp:Content>
