
(function ($) {
    var loginUrl = 'index.php?a=login',
		$userName = $('.user-name'),
		$userCode = $('.user-code'),
		$userPass = $('.user-password'),
		$userToken = $('#token'),
		$nick = $('.user-nick'),
		$xx = $('.user-xx'),
		$form = $('#J-form-login'),
		$form1 = $('#J-form-login1'),
		$form2 = $('#J-form-login2'),
		$submit = $('.sub'),
		$loading = $('.login-loading'),
		$bgArea = $('.bg-area'),
		formData;

    var u, p, code, timer_ID;
  
    var checkEmpty = function () {

        var name = $userName.val(),
			code = $userCode.val(),
			pass = $userPass.val(),
			np = $('#newpwd').val(),
			np2 = $('#newpwd2').val(),
			nick = $nick.val(),
			xx = $xx.val();

        var md = $("#md").val();
        if (pass && md == 2) {
            $submit.removeAttr('disabled').removeClass('disabled');
            return true;
        } else if (name && code && md == 1) {
            $submit.removeAttr('disabled').removeClass('disabled');
            return true;
        } else if (nick && xx && md == 3) {//&& $("#form3 .nicheng").css('display')==''
            $submit.removeAttr('disabled').removeClass('disabled');
            return true;
        } else if (np2 && np && md == 3) {
            $submit.removeAttr('disabled').removeClass('disabled');
            return true;
        } else {
            $submit.attr('disabled', 'disabled').addClass('disabled');
            return false;
        }
    };


    $('.user-name,.user-code,.user-password,.user-nick,.user-xx').on('input propertychange', function () {
        checkEmpty();
    });

})(jQuery);
