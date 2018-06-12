$(function() {
	//tab切换
	function tabs(tabTit, on, tabCon) {
		$(tabCon).each(function() {
			$(this).children().eq(0).addClass(on).show();
		});
		$(tabTit).children().click(function() {
			$(this).addClass(on).siblings().removeClass(on);
			var index = $(tabTit).children().index(this);
			$(tabCon).children().eq(index).show().siblings().hide();
		});
	}
	tabs(".tabHd", "cur", ".tabBd");
	//单项选择样式
	$('.agenRadio label').click(function() {
		var radioId = $(this).attr('name');
		$('.agenRadio label').removeAttr('class') && $(this).attr('class', 'checked');
		$('input[type="radio"].inradio').removeAttr('checked') && $('#' + radioId).attr('checked', 'checked');
	});
	$('.zhangkai li').click(function() {
		$(this).addClass('cur').siblings().removeClass('cur');
	});
	//
	$('.hf-an .hui').click(function() {
		$(this).parent().siblings().toggle()
	})
});

// 固定导航
var nt = !2;
$(window).bind("scroll",
	function() {
		var sel = $("#J_m_nav");
		if(sel.length < 1) {
			return;
		}
		// 当页面高度 减去 浏览器可视区高度 小于等于 导航高度时取消导航固定。否则会出现导航固定与取消固定的循环。
		if($(document).height() - $(window).height() <= 38) {
			return false;
		}
		var st = $(document).scrollTop(); //往下滚的高度
		nt = nt ? nt : sel.offset().top;
		if (nt < st) {
			sel.addClass("xs");
		} else {
			sel.removeClass("xs");
		}
	});


//快速到账动画
$(document).ready(function() {
	$("#dztime i").animate({
		width: '65%'
	}, 400);
	$("#qktime i").animate({
		width: '50%'
	}, 1100);
    //彩种选择
	$('.caizxz').click(function(){
		$('.move-cz').toggle();
		});
	//玩法说明
	$('.cz-wanfa div.wf i').click(function(){
		$('div.sm-wfsm').toggle();
		});
	$('.cz-wanfa div.wf i').mouseover(function(){
		$('div.sm-wfsm').show();
		});
	$('.cz-wanfa div.wf i').mouseout(function(){
		$('div.sm-wfsm').hide();
		});
	//奖金调节
	$('.jjtj span.jine i').click(function() {
		$(".jifenqi").toggle(function() {
			$('jjtj span.jine i').removeClass("upcur");
		},
		 function() {
			$('jjtj span.jine i').addClass("upcur");
			var n = $(".jifenqi").css("display");
			if(n=="none"){
				$('.jjtj span.jine i').css("background", "url(../../resources/btx/css/icon/game/select.png) no-repeat 0 -4px");
			}else{
				$('.jjtj span.jine i').css("background", "url(../../resources/btx/css/icon/game/select.png) no-repeat 0 -37px"); 
			}	
		});
	});
});

//统一弹出框
var layerindex;

function pop(id) {
	var contents = $("#" + id);
	layer.open({
		type: 1,
		title: false, //不显示标题
		content: contents
	});
	layerindex = window.layer.index;
}
//关闭弹框
function closelayer() {
	window.layer.close(layerindex);
}
//屏蔽右键
function doNothing(e) {
	if (window.event) {
		window.event.returnValue = false;
	} else {
		e.preventDefault();
	}
}

//倍数填写下拉
function showAndHide(obj,types){
    var Layer=window.document.getElementById(obj);
    switch(types){
  case "show":
    Layer.style.display="block";
  break;
  case "hide":
    Layer.style.display="none";
  break;
  }
};
function getValue(obj,str){
    var input=window.document.getElementById(obj);
input.value=str;
};