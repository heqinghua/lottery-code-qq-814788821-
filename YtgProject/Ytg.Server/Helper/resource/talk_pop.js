$(function(){
	var win_width=$(window).width()/2;
	var win_height=$(window).height()/2;
	$(".tp_alert").css("left",win_width-376);
	$(".tp_alert").css("top",win_height-228);
	$(".promotion_code").css("left",win_width-250);
	$(".promotion_code").css("top",win_height-103);
	
	$(".prom_t").click(function(){
		$(".promotion_code").hide();
	})
	
	$(".tbp_show_bt").click(	
		function(){
			$(".talk_pop_list").toggle();
			if($(".talk_pop_list").is(":hidden"))
			{
				$(this).removeClass("tbp_hide_bt");
				
			}else{
				$(this).addClass("tbp_hide_bt");
			} 
		}	
	)
	
	$(".tpl_top").click(function(){
		$(".talk_pop_list").hide();
		$(".tbp_show_bt").removeClass("tbp_hide_bt");
	})	
	
	$(".tpll_first_link > a").click(function(){
		$(this).parent().next("ul").toggle();
		if($(this).parent().next("ul").is(":hidden")){
			$(this).removeClass("arr_right");	
		}else{
			$(this).addClass("arr_right");
		}
	})
	
	$(".tpll_p").find("li a").click(function(){
		//$(".tp_alert").show();
	})
	
	$(".tpa_per").hover(
		function(){$(this).next(".tpa_shutdown").show();},
		function(){$(this).next(".tpa_shutdown").hide();}
	)
	$(".tpa_shutdown").hover(
		function(){$(this).show();},
		function(){$(this).show();}
	)
	
	$(".expre_link").click(function(){
		$(".exp_list").toggle();
	})
	$(".upload_ph").click(function(){
		$(".upload_pop").show();
	})
	$(".upl_top").click(function(){
		$(".upload_pop").hide();
	})
	$(".ch_reco").click(function(){
		$(".chat_re_pop").show();
	})
	$(".ch_re_t").click(function(){
		$(".chat_re_pop").hide();
	})
	//$(".tpa_t").click(function(){
	//	$(".tp_alert").hide();
	//})
	//$(".tpll_gen").click(function(){
	//	$(".promotion_code").show();
	//})
	$(".prom_t").click(function(){
		$(".promotion_code").hide();
	})
	$(".tpll_p").find("li").hover(
		function(){$(this).addClass("tpl_li_hover")},
		function(){$(this).removeClass("tpl_li_hover")}
	)
	$(".state_link").click(function(){
		$(".state_ul").toggle();
	})
})