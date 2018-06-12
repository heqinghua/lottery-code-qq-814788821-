<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QianDao.aspx.cs" Inherits="Ytg.ServerWeb.Views.Activity.QianDao.QianDao" MasterPageFile="~/lotterySite.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">

    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <script src="ContentDjp/rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
    <script src="ContentDjp/rotate/jquery.easing.min.js" type="text/javascript"></script>
    <style type="text/css">
        .ltable td {
            font-size: 12px;
        }
    </style>
    <style type="text/css">
        html, body {background:#fbc40f;}
        .ly-plate {
            position: relative;
            width: 509px;
            height: 509px;
            margin-left: -30px;
            margin-top: -250px;
        }

        .rotate-bg {
            width: 509px;
            height: 509px;
            background: url(ContentDjp/rotate/ly-plate.png);
            position: absolute;
            top: 0;
            left: 0;
        }

        .ly-plate div.lottery-star {
            width: 214px;
            height: 214px;
            position: absolute;
            top: 150px;
            left: 147px; /*text-indent:-999em;overflow:hidden;background:url(rotate-static.png);-webkit-transform:rotate(0deg);*/
            outline: none;
        }

            .ly-plate div.lottery-star #lotteryBtn {
                cursor: pointer;
                position: absolute;
                top: 0;
                left: 0;
                *left: -107px;
            }
    </style>
    <script type="text/javascript">
        var isautoRefbanner = <%=isautoRefbanner?"true":"false"%>;
        $(function () {
            $("#refff").html(Ytg.tools.moneyFormat('<%=UserAmt%>'));//old amt
            $("#lottery_activity").addClass("on");
            $("#bottomState").remove();
        })
        var issgin = false;
        function sgin(obj){
            if (issgin)
                return;
            issgin = true;
            $(obj).html("处理中...");
            $.ajax({
                url: "/Views/Activity/QianDao/QianDao.aspx",
                type: 'post',
                data: "action=sign&ajax=ajx",
                success: function (data) {
                    issgin = false;
                    $(obj).html("我要签到");
                    if (data == "0") {
                        $.alert("恭喜您，签到成功！",1,function(){
                            window.location.reload();
                        });
                    } else if (data == "2") {
                        $.alert("投注量还未达到要求，赶紧去投注，投的越多、奖励越多！");
                    } else if (data == "3") {
                        $.alert("您今天已经签到过啦，明天再来吧！");
                    } else if (data == "5") {
                        $.alert("活动还没开始呢！");
                    } else {
                        $.alert("未知错误，请稍后再试！");
                    }
                }
            });
        }
        function refchangemonery(){
            Ytg.common.user.refreshBalance();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="/Views/Activity/QianDao/Content/css/style.css" />
    <style>
        h2, h3, p, ul, li {
            margin: 0;
            padding: 0;
        }

        ul, li {
            list-style: none;
        }

        img {
            vertical-align: middle;
        }

        .left {
            float: left;
        }

        .right {
            float: right;
        }

        .clear {
            clear: both;
        }

        a {
            color: #cd0228;
            text-decoration: none;
        }


        .bannerNew {
            background: url(/Views/Activity/QianDao/Content/image/1469085237.jpg) no-repeat top center;
            height: 940px;
        }

        .NewbannerTwo {
            width: 1093px;
            margin: 60px auto;
        }

        .bannerTwo01 {
            width: 492px;
            height: 236px;
            background: url(/Views/Activity/QianDao/Content/image/NewAbanner2.jpg) no-repeat;
            color: #fff;
            padding: 240px 0 0 40px;
            font-size: 14px;
            line-height: 22px;
        }

            .bannerTwo01 h2 {
                font-size: 16px;
            }

        .bannerTwo02 {
            width: 519px;
            height: 312px;
            background: url(/Views/Activity/QianDao/Content/image/NewAbanner2.jpg) no-repeat -560px 0;
            padding: 164px 0 0 23px;
        }

            .bannerTwo02 .data {
                background: url(/Views/Activity/QianDao/Content/image/data.png) no-repeat;
                height: 294px;
                width: 476px;
                padding-top: 1px;
            }

                .bannerTwo02 .data .signBtn {
                    position: relative;
                    float: right;
                    margin-right: 20px;
                    margin-top: 6px;
                    display: block;
                    background: #ff6105;
                    color: #fff;
                    padding: 13px;
                    font-size: 14px;
                    font-weight: bold;
                    padding-bottom: 2px;
                    padding-top: 2px;
                    cursor: pointer;
                }

                    .bannerTwo02 .data .signBtn:hover {
                        background: #f77627;
                    }

                .bannerTwo02 .data .signPop {
                    background: url(/Views/Activity/QianDao/Content/image/popBox.png) no-repeat;
                    height: 96px;
                    width: 178px;
                    font-size: 16px;
                    line-height: 25px;
                    color: #a18b00;
                    padding: 15px;
                    position: absolute;
                    top: -175px;
                    right: -96px;
                }

                    .bannerTwo02 .data .signPop em {
                        text-decoration: underline;
                        padding: 0 5px;
                    }

                    .bannerTwo02 .data .signPop .closeBtn {
                        height: 26px;
                        width: 26px;
                        font-size: 0;
                        text-indent: -999em;
                        position: absolute;
                        right: 0;
                        top: 0;
                    }

                .bannerTwo02 .data .dataForm {
                    margin: 37px 0 0 8px;
                    height: 294px;
                    width: 460px;
                    border-radius: 8px;
                    overflow: hidden;
                    *margin: 37px 0 0 7px;
                }

                    .bannerTwo02 .data .dataForm ul li {
                        float: left;
                        font-weight: 600;
                        width: 86px;
                        padding-left: 5px;
                        padding-top: 5px;
                        text-align: center;
                        position: relative;
                    }

                        .bannerTwo02 .data .dataForm ul li p {
                            text-align: center;
                        }

                        .bannerTwo02 .data .dataForm ul li .mask {
                            display: none;
                            background: url(/Views/Activity/QianDao/Content/image/blueMask.png) repeat;
                            height: 100%;
                            width: 100%;
                            position: absolute;
                            top: 0;
                            left: 0;
                        }

                        .bannerTwo02 .data .dataForm ul li.active .mask {
                            display: block;
                        }

                        .bannerTwo02 .data .dataForm ul li.nor {
                            border-right: none;
                        }

                    .bannerTwo02 .data .dataForm .jf {
                        background-color: #f3f3f3;
                        color: #808080;
                        font-size: 15px;
                        line-height: 30px;
                        height: 30px;
                    }

                    .bannerTwo02 .data .dataForm .jfchk {
                        background-color: #f3f3f3;
                        color: #ed4020;
                        font-size: 15px;
                        line-height: 30px;
                        height: 30px;
                    }

                    .bannerTwo02 .data .dataForm .date {
                        font-size: 15px;
                        line-height: 40px;
                        height: 36px;
                        color: #777;
                    }

                    .bannerTwo02 .data .dataForm .hint {
                        font-size: 12px;
                        font-weight: 400;
                        line-height: 20px;
                        height: 24px;
                        color: #777;
                    }

        p {
            color: #fff;
        }

        .guize {
            width: 1200px;
            margin: 0 auto 40px auto;
            text-align: left;
        }

            .guize h2 {
                background: url(/Views/Activity/QianDao/Content/image/guize_title.jpg) no-repeat;
                width: 278px;
                height: 79px;
            }

            .guize div {
                background: #2ba1d9;
                padding: 30px 25px;
                line-height: 30px;
            }

            .guize span {
                color: #fff;
                font-size: 16px;
            }

            .guize div p {
                color: #fffe70;
                font-size: 16px;
                margin-bottom: 30px;
            }

                .guize div p b {
                    text-decoration: underline;
                    font-style: italic;
                    color: #fff;
                    padding-left: 5px;
                    padding-right: 5px;
                }

            .guize div li {
                font-size: 14px;
                color: #ace4ff;
                background: url(/Views/Activity/QianDao/Content/image/list.png) no-repeat 0 8px;
                padding-left: 20px;
                text-align: left;
            }

                .guize div li b {
                    color: #fac800;
                }

        b {
            font-size: 20px;
        }

        .btn {
            height: 81px;
            width: 286px;
            font-size: 16px;
            background: url('/Views/Activity/QianDao/Content/image/14628212284.jpg') no-repeat;
            border: none;
            cursor: pointer;
            margin: auto;
            margin-top: 480px;
        }
    </style>
    <form id="form1" runat="server">
        <div style="background: #fbc40f;">

            <div class="NewbannerTwo">
                <div class="left bannerTwo01">
                    <div class="ly-plate">
                        <div class="rotate-bg"></div>
                        <div class="lottery-star">
                            <img src="ContentDjp/rotate/rotate-static.png" id="lotteryBtn"></div>
                    </div>
                </div>
                <div class="right bannerTwo02">
                    <div class="data">
                        <span class="signBtn" onclick="sgin(this);">我要签到</span>
                        <div class="signPop" style="display: none;">
                            <a class="closeBtn" href="javascript:;" title="关闭">关闭</a>
                            <p style="padding-top: 16px;">恭喜您 , 已领取<em class="gotJifen"></em>积分</p>
                            <p style="font-size: 12px;" class="tjf">明日可领<em class="tomorrowJifen"></em>积分, 再接再励哦!</p>
                        </div>
                        <div class="dataForm">
                            <ul>
                                <%for (var i = 1; i <= MaxDay; i++)
                                        {%>
                                <li class="1">
                                    <p class="<%=sgins.Contains(Convert.ToInt32(DateTime.Now.ToString("yyyyMM")+i.ToString("d2")))?"jfchk":"jf" %>"><%=DateTime.Now.Month %>月<%=i %>日</p>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </div>

                </div>
                <div class=" clear"></div>
            </div>
            <div style="background: url(/Views/Activity/QianDao/Content/image/146282122797.png) no-repeat; background-position: center; background-position-x: center; height: 581px;">
                <asp:Button ID="btnME" runat="server" CssClass="btn" Text="" OnClick="btnME_Click" />
            </div>

        </div>
    </form>


    <script type="text/javascript">
  
    var isRotate = false;
    var timeOut = function () {  //超时函数
        $("#lotteryBtn").rotate({
            angle: 0,
            duration: 10000,
            animateTo: 2160, //这里是设置请求超时后返回的角度，所以应该还是回到最原始的位置，2160是因为我要让它转6圈，就是360*6得来的
            callback: function () {
                $.alert('网络超时')
            }
        });
    };
    isRotate = false;
    $("#lotteryBtn").rotate({
        bind:
          {
              click: function () {
                  goRotate();
              }
          }
    });

    var rotateFunc = function (awards, angle, text) {  //awards:奖项，angle:奖项对应的角度
        $('#lotteryBtn').stopRotate();
        $("#lotteryBtn").rotate({
            angle: 0,
            duration: 5000 * 2,
            animateTo: angle + 360 * 10, //angle是图片上各奖项对应的角度，1440是我要让指针旋转4圈。所以最后的结束的角度就是这样子^^
            callback: function () {
                isRotate = false;
                switch (awards) {
                    case 1:
                        $.alert("1元已经入包，刷新您的余额试试！");
                        break;
                    case 2:
                        $.alert("2元已经入包，刷新您的余额试试！");
                        break;
                    case 3:
                        $.alert("恭喜，3元已经入包，刷新您的余额试试！");
                        break;
                    case 4:
                        $.alert("恭喜，4元已经入包，刷新您的余额试试！");
                        break;
                    case 5:
                        $.alert("你太牛了竟然抽中了5块！");
                        break;
                }
            }
        });
    };

    function goRotate() {
      
        if (isRotate)
            return;
        isRotate = true;
        $.ajax({
            url: "/Views/Activity/QianDao/QianDao.aspx",
            type: 'post',
            data: "action=ajx",
            success: function (data) {
               
                if (data == "")
                    return;
                var dar = data.toString().split(',');
                data = parseInt(dar[0]);
                var p = dar[1];
                var p1 = dar[2];
                //$("#subSpan").html(p1);
                if (data == -1) {
                 //   $("#titleParent").removeAttr("style");
                    $.alert("您今天还没签到呢,签到了再来抽吧！");
                    isRotate = false;
                    return;
                } else if (data == -2) {
                    $.alert("活动还没开始呢！");
                    isRotate = false;
                    return;
                } else if (data == -3) {
                    $.alert("您今天已经抽过啦，明天再来试试手气吧！");
                    isRotate = false;
                    return;
                }
               
                switch (parseInt(p)) {
                    case 1://160
                      
                        rotateFunc(1, generateMixed(4), '')
                        break;
                    case 2://250
                        rotateFunc(2, 40 * 11, '');//2快
                        break;
                    case 3://250
                        rotateFunc(3, 40 * 4, '');//3块
                        break;
                    case 4://250
                        rotateFunc(4, 40 * 6, '');//4快
                        break;
                    case 5://250
                        rotateFunc(5, 40 * 8.5, '');//5快
                        break;
                }
            }
        });
    }

    var chars = [200, 200, 280, 380, 480,480];

    function generateMixed(n) {
        var res = "";
        var cx = 0;
        for (var i = 0; i < n ; i++) {
            var id = Math.ceil(Math.random() * 5);
             cx=chars[id];
        }
        return cx;
    }
    </script>
</asp:Content>

