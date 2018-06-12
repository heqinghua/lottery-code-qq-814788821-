$(document).ready(function() {
	$("#right-main-content ul li p").stop().slideUp();
	$("#right-main-content ul li table ").hide();
	$("#right-main-content ul .active p").slideDown();
	
	$("#right-main-content ul li h3").click(function () {

		$("#right-main-content ul li p").stop().slideUp(300);
		$("#right-main-content ul li table").stop().slideUp(300);
		
		$(this).parent().find("p").stop().slideDown(300, function () {
		    $(parent.$('#main')).height($("#right-main-content").height()); //重设 iframe 高度
		});
		$(this).parent().find("table").stop().slideDown(300, function () {
		   
		    $(parent.$('#main')).height($("#right-main-content").height()); //重设 iframe 高度
		});
		
		$("#right-main-content ul li ").removeClass("active");
		$(this).parent().addClass("active");
		
		
	});
});
