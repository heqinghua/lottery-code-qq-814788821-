$(document).ready(function() {
	//include
//	$.ajax({
//		url : '/xm/vendor/simple-top.html',
//		success : function(data) {
//			$(".simple-top_include").html(data);
//		}
//	});
//	$.ajax({
//		url : "/xm/vendor/footer.html",
//		success : function(data) {
//			$(".footer_include").html(data);
//		}
//	});
//	$.ajax({
//		url : "/xm/vendor/top.html",
//		success : function(data) {
//			$(".top_include").html(data);
//		}
//	});
	var tmpID = $(".full-tab .full-tab-items .active").attr("id");
	$(".full-tab-content div").hide();
	$("." + tmpID + "").show();
	$(".full-tab-items").find("li").click(function() {
		$(".full-tab-items li").removeClass("active");
		$(this).addClass("active");
		var tmp = $(this).attr("id");
		$(".full-tab-content div").hide();
		$("." + tmp + "").show();
	});
	$(".alert-panle").click(function() {
		$(this).hide();
	});

	$(".table tbody tr:odd ").css({
		background : "#f6f4f5"
	});

});
