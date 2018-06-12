function subhm(betid, subscription) {
    $.ajax({
        url: "/Page/Lott/LotteryBetDetail.aspx",
        type: 'post',
        data: "&action=groupby&betid="+ betid +"&subscription=" + subscription,
        success: function (data) {
            console.info(data);
            var jsonData = JSON.parse(data);
            if (jsonData.Code == 0) {
                $.alert("购买成功");
            } else if (jsonData.Code==1007) {
                $.alert("余额不足！");
            } else if (jsonData.Code == 1005) {
                $.alert("该期已经开，购买失败！");
            } else {
                $.alert("购买失败，请刷新后重试！");
            }
            hmsta = false;
        }
    });
}