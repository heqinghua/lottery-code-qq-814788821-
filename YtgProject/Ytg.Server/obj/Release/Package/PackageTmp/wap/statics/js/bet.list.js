

function doConfirmOk() {
    var key = $('#revokeKey').val();
    loadingShow();
    $.ajax({
        url: '/mine/ajaxRevoke.html',
        type: 'POST',
        dataType: 'json',
        data: {
            'key': key
        },
        timeout: 30000,
        success: function (data) {
            loadingHide();
            $('#revokeKey').val('');
            if (data.Result == false) {
                if (/系统错误/g.test(data.Desc)) {
                    data.Desc = '撤单失败，请重试！';
                }
                msgAlert(data.Desc);
                reLogin(data.Desc);
                return;
            }
            $('span#win_state').text('手动退码');
            //改页面参数
            $('a.' + key).data('needrevoke', '0');
            $('a.' + key).data('detail', $('a.' + key).data('detail').replace(/status=[0-9]*/g, 'status=2').replace(/win=[0-9]*/g, 'win=2'));
            $('a.' + key + ' div.c-gary span').text('已取消');
            showStep('revokeok');
        }
    });
}

function initLoading() {
    $('a.on-more').hide();
    $('div.mine-message').hide();
}
var pageIndex = 1;

function getQueryData() {
    var st = $(".beet-active").attr("data-type");
    var filterData = "startTime=&endTime=";
    if (st == 0) {
        //全部订单
        filterData += "&status=";
    } else if (st == 1) {
       
    }
    else if (st == 2) {
        //我的中奖
        filterData += "&status=1";
    } else if (st == 3) {
        //待开奖
        filterData += "&status=3";
    } else if (st == 4) {
        //我的撤单
        filterData += "&status=4";
    }
 
    filterData += "&tradeType=" ;
    filterData += "&SellotteryCode=";
    filterData += "&lotteryid=" ;
    filterData += "&palyRadioCode=" ;
    filterData += "&issueCode=" ;
    filterData += "&model=" ;
    filterData += "&betCode=" ;
    filterData += "&userAccount=" ;
    filterData += "&userType="  ;
    filterData += "&pageIndex=" + pageIndex;

    return filterData;
}

function getBetList(more) {//1:更多，2:充值
  
    if (more == 1) {
        pageIndex++;
    } else if (more == 2) {
        pageIndex = 1;
    }
   
    $('a.on-more').html("正在获取数据中..");
    //
    $.ajax({
        url: "/Page/Lott/LotteryBetDetail.aspx",
        type: 'post',
        data: getQueryData() + "&action=betlist",
        success: function (data) {
           
            var jsonData = JSON.parse(data);
         
            //清除
            $('div.mine-message').hide();
            var htm = "";
            if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                //分页

                $("#bet_list").children().remove();

                var sumwivalue = 0;
                var sumnowinvalue = 0;
                for (var c = 0; c < jsonData.Data.length; c++) {
                    var item = jsonData.Data[c];
                    var states = Ytg.common.LottTool.GetStateContent(item.Stauts + "_")
                    var betContent = Ytg.common.LottTool.ShowBetContent(item.BetContent);
                    var modelStr = "";
                    switch (item.Model) {
                        case 0:
                            modelStr = "元";
                            break;
                        case 1:
                            modelStr = "角";
                            break;
                        case 2:
                            modelStr = "分";
                            break;
                        case 2:
                            modelStr = "厘";
                            break;
                    }

                    sumwivalue += item.TotalAmt;
                    sumnowinvalue += item.WinMoney;

                    var winAmt = decimalCt(Ytg.tools.moneyFormat(item.WinMoney));

                    var tolHtml = winAmt;
                    if (winAmt.toString().indexOf(',') >= 0 || winAmt > 0)
                        tolHtml = "<span class='winSpan'>" + winAmt + "</span>";

                    var psName = item.PostionName == null ? "" : item.PostionName;

                    var showRadioName = psName + changePalyName(item.PlayTypeName + "" + item.PlayTypeRadioName, '');

                    var showOpenresult = (item.OpenResult == null ? "" : item.OpenResult);
                    var showTitle = showOpenresult;
                    if (showOpenresult != "") {
                        if (showOpenresult.length > 14) {
                            showOpenresult = showOpenresult.substring(0, 14);
                        }
                    }
                  
                    htm += '<li>'
                    + "<a href='javascript:showDetail(\"" + item.BetCode + "\",\"" + item.IssueCode + "\"," + item.tp + ")' >"
                    + '<div class="order-list-tit">'
                    + '<span class="fr c-red">-' +decimalCt(Ytg.tools.moneyFormat(item.TotalAmt)) + '元</span><span class="order-top-left">' + item.LotteryName + '</span>第' + item.IssueCode + '期'
                    + '</div>'
                    + '<div class="c-gary"><span class="fr">' + states + '</span><p class="order-time">' +getDayTime(item.OccDate) + '</p></div>'
                    + '</a>'
                    + '</li>';

                }
                //小计
               
            } else {
                $('div.mine-message').show();
            }

            if (more == 2) {//刷新
                $('ul#bet_list').html(htm);
            } else {
                $('ul#bet_list').append(htm);
            }
            $('a.on-more').html("点击加载更多");
            if (data.PageCount > pageIndex) {
                $('a.on-more').show();
            } else {
                $('a.on-more').hide();
            }
            
        }
    });

}


function showDetail(code, issueCode, betType) {
    
    var ul = "/wap/users/mine/bettDetails.aspx?betcode=" + code;
    if (betType == 1 && issueCode != -1) {
        ul = "/wap/users/mine/BettingDetail.aspx?catchCode=" + code + "&issueCode=" + issueCode;
    }
    if (betType == 2) {
        ul = "/wap/users/mine/CatchDetail.aspx?catchCode=" + code;
    }
    window.location = ul;
}


$(function () {
    $('div.ui-bett-refresh').click(function () {
        initLoading();
        getBetList(2);
    });

    $('a.on-more').click(function () {
        getBetList(1);
    });

    $('div.beet-tips a').click(function () {
        $('div.beet-tips a').removeClass('beet-active');
        $('div.beet-tips').hide();
        $(this).addClass('beet-active');
        $('span#order_type').text($(this).text());
        orderType = $(this).data('type');
        initLoading();
        getBetList(2);
    });

    $('div.bett-top').click(function (event) {
        if (onlyWin == 1) {
            event.stopPropagation();
            getBetList();
        }
    });

   

    getBetList();
});