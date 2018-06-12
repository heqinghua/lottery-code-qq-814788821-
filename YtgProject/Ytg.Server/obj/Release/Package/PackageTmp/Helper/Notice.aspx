<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notice.aspx.cs" Inherits="Ytg.ServerWeb.Helper.Notice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="resource/cwlobby.css" rel="stylesheet" type="text/css"/>	
    <link href="resource/help.css" rel="stylesheet" type="text/css"/> 
    <link href="resource/group.css" rel="stylesheet" type="text/css"/>     
    <script src="resource/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="resource/help.js" type="text/javascript"></script>   
    <script type="text/javascript">
			jQuery(document).ready(function() {
			
				$("#main-nav-panle").hover(function() {
				}, function() {
					$("#main-nav-panle").hide();
					$("#main-nav-panle-iframe").hide();
				});
				$("#sub-nav-items li ul li").hide();
				$("#sub-nav-items li ").hover(function() {
					$(this).find("ul li").show();
				}, function() {
					$(this).find("ul li").hide();
				});
			});
			function show_no(id) {
				$("#code_" + id).show("slow");
			}

			function show_nocode(id) {
				$("#ncode_" + id).show("slow");
			}

			function close_no(id) {
				$("#code_" + id).hide("slow");
			}

			function nclose_no(id) {
				$("#ncode_" + id).hide("slow");
			}

			function getUrlParam(name) {
				var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
				//构造一个含有目标参数的正则表达式对象
				var r = window.location.search.substr(1).match(reg);
				//匹配目标参数
				if (r != null)
					return unescape(r[2]);
				return null;
				//返回参数值
			}

			$(document).ready(function() {
				var a = getUrlParam("hnm");
				if (a != null) {
					$("#right-main-content ul li").removeClass("active").find("p").hide(100).stop();
					$("#right-main-content ul ").find("#" + a).addClass('active').find("*").show('slow');
				}
			});

		</script>        
    <style type="text/css">
        .ctp {text-indent:25px;}
    </style>
</head>
<body>
  
<div id="right-main">
<div class="xgnc_mid">
<div id="right-main-content">
    <ul>
        <asp:Repeater ID="rptNews" runat="server">
            <ItemTemplate>
                <li id='<%# Eval("Id") %>' <%# GetIsOpend(Container.ItemIndex,Eval("Id"))%>>
                    <h3 style='<%# GetState(Eval("IsShow"))%>'><%#Container.ItemIndex+1 %>• &nbsp;<%# Eval("Title") %></h3>
                    <p class="ctp">
                        <%#  System.Web.HttpUtility.UrlDecode(Eval("Content").ToString()) %>
                    </p>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div></div>
</body>
</html>
