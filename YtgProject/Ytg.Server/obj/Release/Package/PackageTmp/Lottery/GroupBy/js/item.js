var hmsta = false;
$(function() {
    $('#numberUserTab li').click(function() {
        $('#numberUserTab li').removeClass('active');
        $(this).addClass('active');
        var ind = $(this).index();
        if (ind == 0) {
            $('#userBox').hide();
            $('#numberDetail').show();
        } else {
            $('#numberDetail').hide();
            $('#userBox').show();
            loadGroupByUserLst();//加载合买用户列表
        }
    });
    $('#rgInput').keyup(function() {
        $(this).val($(this).val().replace(/[^\d]/g, ''));
        var num = Number($(this).val() == 0 ? 1 : $(this).val());
        var max = Number($(this).attr('max'));
        num = num > max ? max : num;
        $(this).val(num);
    });
    $('#tzBtn').click(function() {
        if (hmsta) {
            alert('正在提交数据，请稍等。');
            return;
        }
        var mon = Number($('#rgInput').val());
        var max = Number($('#rgInput').attr('max'));
        if (mon == 0) {
            alert('请输入您要认购的金额。');
            return;
        } else if (mon > max) {
            alert('最多只能认购' + max + '元');
        }
        var item = $('#item').val();
        var lot = $('#lot').val();
        var fqh = $('#fqh').val();
        hmsta = false;
        subhm($(this).attr("tag"), mon);
        
    });
});


