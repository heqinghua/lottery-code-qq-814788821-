var myScroll;
var myScroll2;
function loaded () {
	try{
		//myScroll = new IScroll('#wrapper_1', { mouseWheel: true,click: false });
	}catch(e){
		console.log(e);
	}

   // myScroll2 = new IScroll('#wrapper_2', { mouseWheel: true,click: true });
}
//下面代码影响页面所有的点击事件

//弹窗
$(function () {
    //$('.ui-betting-title').click(function(){
    $('.ui-betting-title').bind('touchend', function () {
        event.preventDefault();
        if (location.href.indexOf('/mine/betList.html?onlyWin=1') > -1) {
            return;
        }
        if (location.href.indexOf('/bet/') > -1 && $('.beet-odds-tips').css('display') != 'none') {
            return;
        }
        $('.beet-tips').toggle();
        $('.beet-rig').hide();
    });

    $('.bett-heads').click(function () {
        $('.tips-bg').toggle();
    });

    $('.bett-head').click(function () {
        $('.beet-tips').hide();
        $('.beet-rig').toggle();
        return false;
    });

    $('.beet-tips').bind("click", function () {
        //console.log($(this).css('display'));
        if ($(this).css('display') != 'none') {
            event.stopPropagation();
        }
    });

    // click anywhere toggle off
    $(window).bind("load", function () {
        $(document).on("click", function () {
            if ($('.beet-rig').is(':visible')) {
                $('.beet-rig').toggle();
            }
            else {
                $('.beet-rig').css('display', 'none');
            }
            if ($('.beet-tips').is(':visible')) {
                $('.beet-tips').toggle();
            }
            else {
                $('.beet-tips').css('display', 'none');
            }

        });

        $('li.specific-cell-o>span').bind('touchend', function () {
            if ($('.beet-rig').is(':visible')) {
                $('.beet-rig').toggle();
            }
            else {
                $('.beet-rig').css('display', 'none');
            }
            if ($('.beet-tips').is(':visible')) {
                $('.beet-tips').toggle();
            }
            else {
                $('.beet-tips').css('display', 'none');
            }
        });

        $('#wrapper_1').bind('touchend', function () {
            if ($('.beet-rig').is(':visible')) {
                $('.beet-rig').toggle();
            }
            else {
                $('.beet-rig').css('display', 'none');
            }
            if ($('.beet-tips').is(':visible')) {
                $('.beet-tips').toggle();
            }
            else {
                $('.beet-tips').css('display', 'none');
            }
        });
    });
    // end

    // scroll toggle off
    $(document).scroll(function () {
        if ($('.beet-rig').is(':visible')) {
            $('.beet-rig').toggle();
        }
        else {
            $('.beet-rig').css('display', 'none');
        }
        if ($('.beet-tips').is(':visible')) {
            $('.beet-tips').toggle();
        }
        else {
            $('.beet-tips').css('display', 'none');
        }
    });
    // end

    //  hide address bar
    window.addEventListener("load", function () {
        // Set a timeout...
        /*setTimeout(function(){
        // Hide the address bar!
        window.scrollTo(0, 1);
        }, 0);*/
    });

    $('.bett-odd a').click(function () {
        $('.bett-odd').toggleClass('bett-odd-r')
    });
    timeOld = 0; //记录上次点击事件
    timeNew = 0; //判断是否恶意点击

    $('button#reveal-left, button#back_to_bet').click(function () {
       
        timeNew = new Date().getTime() + 0;
        if (timeNew - timeOld < 1000) {
            return;
        }
        timeOld = timeNew;
       
        if (location.href.indexOf('/mine/accountDetail.aspx') ||
        location.href.indexOf('/trend/index.aspx') ||
        location.href.indexOf('/wap/Opens.aspx') ||
        location.href.indexOf('/wap/Opens.aspx')) {
            window.history.back(-1);
            return;
        }
        if (location.href.indexOf('GameCenter.aspx') > -1) {
            //返回游戏中心 add
            //history.back(-1)
            window.history.back(-1);
            return;
        }

        if (location.href.indexOf('mine') > -1) {
            //返回个人中心首页 add
            location.href = '/wap/users/Main.aspx';
            return;
        }

        window.history.back(-1);
        return;
    });

    function reLogin(desc) {
        if (/未指定具体帐号|帐号不存在|该帐号需要验证|请重新登录|请重新登陆|请登录|请登陆/g.test(desc)) {
            location.href = '/wap/login.html';
            return true;
        }
    }

    function goBackOfBetPage() {
        if (document.referrer.indexOf('/index/login.html') > -1 && isLogin == true) {
            location.href = '/mine/index.html';
        } else {
            history.go(-1);
        }
    }
});