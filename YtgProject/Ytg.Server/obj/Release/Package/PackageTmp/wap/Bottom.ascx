<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Bottom.ascx.cs" Inherits="Ytg.ServerWeb.wap.Bottom" %>
<script  type="text/javascript">
    function checkItem(idx) {
        uncheckAll();
        switch (idx) {
            case 0:
                $("#menu-home-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_01.png");
                break;
            case 1:
                $("#menu-color-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_02.png");
                break;
            case 2:
                $("#menu-lot-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_03.png");
                break;
            case 3:
                $("#menu-news-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_04.png");
                break;
            case 4:
                $("#menu-my-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_05.png");
                break;
        }
    }

    function unCheckItem(idx) {

        switch (idx) {
            case 0:
                $("#menu-home-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_1.png");
                break;
            case 1:
                $("#menu-color-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_2.png");
                break;
            case 2:
                $("#menu-lot-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_3.png");
                break;
            case 3:
                $("#menu-news-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_4.png");
                break;
            case 4:
                $("#menu-my-img").attr("src", "/wap/statics/homeStyle3/images/index-img/nav_5.png");
                break;
        }
    }
    function uncheckAll() {
        unCheckItem(0);
        unCheckItem(1);
        unCheckItem(2);
        unCheckItem(3);
        unCheckItem(4);
    }
</script>
<link href="/Css/chartwin.css" rel="stylesheet" />
<div class="menu" id="fixnav">
        <ul>
            <li><a class="menu-home" href="/wap/home.aspx"><img src="/wap/statics/homeStyle3/images/index-img/nav_1.png" id="menu-home-img"></a></li>
            <li><a class="menu-color" href="/wap/lobby.aspx"><img src="/wap/statics/homeStyle3/images/index-img/nav_2.png" id="menu-color-img"></a></li>
            <li><a class="menu-lot" href="/wap/Opens.aspx"><img src="/wap/statics/homeStyle3/images/index-img/nav_3.png" id="menu-lot-img"></a></li>
            <li><a class="menu-news" href="/wap/trend/index.aspx"><img src="/wap/statics/homeStyle3/images/index-img/nav_4.png" id="menu-news-img"></a></li>
            <li><a class="menu-my" href="/wap/users/Main.aspx/Opens.aspx"><img src="/wap/statics/homeStyle3/images/index-img/nav_5.png" id="menu-my-img"></a></li>
        </ul>
    </div>
 <div id="pop" style="display: none;">
        <div id="popHead" >
            <a id="popClose" title="关闭">关闭</a>
            <h2>系统消息</h2>
        </div>
        <div id="popContent">
            <dl>
                <dd id="popIntro">内容</dd>
            </dl>
            <p id="popMore"></p>
        </div>
        <div style="margin-top: -3px">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: left; cursor: pointer;">&nbsp;&nbsp;<input type="button" class="noneState" id="popPre" value="上一条" /></td>
                    <td style="text-align: right; cursor: pointer;">
                        <input type="button" value="下一条" id="popNext" class="noneState" />&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>

<!--中奖提示-->
    <script type="text/javascript">

        //中奖提示框
        function Pop(url, data) {

            this.index = 0;
            this.count = data.length;
            this.url = url;
            this.dataArray = data;
            this.apearTime = 1000;
            this.hideTime = 10000;
            this.delay = 10000;
            //添加信息
            this.addInfo();
            //显示
            this.showDiv();
            //关闭
            this.closeDiv();
            this.Pre(this);
            this.Next(this);
            if (this.count <= 1)
                $("#popPre,#popNext").removeClass("noneState");
            $("#popPre,#popNext").addClass("useState");
        }
        Pop.prototype = {
            addInfo: function () {
                $("#popIntro").html(this.dataArray[this.index].MessageContent);
                $("#popMore a").attr('href', this.url);
                this.index++;
            },
            showDiv: function (time) {
                if (!($.browser.msie && ($.browser.version == "6.0") && !$.support.style)) {
                    $('#pop').slideDown(this.apearTime).delay(this.delay).fadeOut(this.hideTime);
                } else {//调用jquery.fixed.js,解决ie6不能用fixed
                    $('#pop').show();
                    jQuery(function ($j) {
                        $j('#pop').positionFixed()
                    })
                }
            },
            closeDiv: function () {
                $("#popClose").click(function () {
                    $('#pop').hide();
                }
                );
            },
            Pre: function (tag) {
                $("#popPre").click(function () {
                    if (tag.index <= 0) {
                        $("#popPre").removeClass("useState")
                        $("#popPre").addClass("noneState");
                        $("#popNext").removeClass("noneState");
                        $("#popNext").addClass("useState");
                        return;
                    }
                    tag.index--;
                    $("#popIntro").html(tag.dataArray[tag.index].MessageContent);
                });
            },
            Next: function (tag) {
                $("#popNext").click(function () {
                    if (tag.index < tag.count - 1) {
                        tag.index++;
                        $("#popIntro").html(tag.dataArray[tag.index].MessageContent);
                    } else {
                        $("#popNext").removeClass("useState")
                        $("#popNext").addClass("noneState");
                        $("#popPre").removeClass("noneState");
                        $("#popPre").addClass("useState");
                    }
                });
            }
        }
        var pop = null;

        function checkPrizeMsg() {

            $.ajax({
                type: 'POST',
                url: '/Page/Messages.aspx?action=noreadwinmsg',
                data: '',
                timeout: 10000,
                success: function (data) {
                    if (data == "")
                        return;
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0) {
                        if (jsonData.Data.length > 0) {
                            parent.Ytg.common.user.refreshBalance();
                        }
                        pop = new Pop("#", jsonData.Data);
                    } else if (jsonData.Code == 1009) {
                        alert("由于您长时间未操作,为确保安全,请重新登录！"); window.location = "/wap/login.html";
                    }

                },
                error: function () { }
            });
            setTimeout(arguments.callee.bind(this), 10000);
        }
        $(function () {
            setTimeout(checkPrizeMsg, 1000);
        });
      
      
    </script>