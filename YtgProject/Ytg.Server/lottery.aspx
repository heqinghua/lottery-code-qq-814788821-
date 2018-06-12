<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lottery.aspx.cs" Inherits="Ytg.ServerWeb.lottery" MasterPageFile="~/lotterySite.Master" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
      <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <script type="text/javascript">
        $(function () {
            $("#lottery_game").addClass("on");
            $("#bottomState").css("margin-top","40");
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="UserInfoBox wrap_footerbg">
         <div class="clearfix"></div>
        <div class="wrap_bg wrap" style="width: 100%;">
            <!--个人信息-->
            <div id="content" style=" padding-top: 0px; position: relative;">
                <div class="left_frame" style=" position: absolute; z-index: 999;">
                    <div class="left_content">
                        <div class="sidebar_menu">
                            <asp:Repeater ID="rptMenus" runat="server" OnItemDataBound="rptMenus_ItemDataBound">
                                <ItemTemplate>
                                <dl class="ff-tow2">
                                      <dt style="display:none;"><%#Eval("ShowTitle") %></dt>
                                    <dd class="on">
                                        <asp:Repeater ID="rptChildren" runat="server">
                                            <ItemTemplate>
                                            <ul class="con_ul noBorder">
                                                <li><a href="/Lottery/<%# GetLotteryUrl(Eval("LotteryCode")) %>.aspx?ltcode=<%# Eval("LotteryCode") %>&ltid=<%# Eval("Id") %>&ln=<%# System.Web.HttpUtility.UrlEncode(Eval("LotteryName").ToString()) %>&ico=<%# Eval("ImageSource") %>" target="main" onclick="setLastLottery(this);">
                                                    <p><span class="<%#Eval("Remark") %>"><%# Eval("LotteryName") %></span></p>
                                                </a></li>
                                            </ul>
                                                </ItemTemplate>
                                        </asp:Repeater>
                                </dl>
                                    </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="leftsidebotcon"></div>
                    <script>
                        $(".ff-one").click(function () {
                            $(this).addClass("ff-tow").siblings().removeClass("ff-tow")
                        });
                        $(".ff-tow").click(function () {
                            $(this).slideDown();
                        });
                    </script>
                    <div style="clear:both;"></div>
                </div>

                <div id="con_right">
                    <div class="right_box" style="padding: 0px; border: 0px currentColor; border-image: none; display: inline;">
                        <iframe id="main" name="main" allowtransparency="true" width="100%" height="800"  scrolling="no" frameborder="0" src="/Lottery/<%=GetLotteryUrl(Request.QueryString["LotteryCode"])%>.aspx?ltcode=<%=Request.QueryString["LotteryCode"] %>&ltid=<%=Request.QueryString["Id"]%>&ln=<%=Request.QueryString["LotteryName"] %>&ico=<%=Request.QueryString["ImageSource"]%>" border="0" noresize="noresize" framespacing="0" ></iframe>
                        <div style="clear:both;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('.con_ul li').each(function () {
            $(this).find("a").bind("click", function () {
                $(this).find("p").addClass("title_active")
                        .parents().siblings().find("a > p").removeClass("title_active");
            });
        });
        window.onresize = function () {
            $("#con_right").css({ "width": ($(window).width() - 172) });
        }
        $(function () {
            $("#con_right").css({"width":($(window).width()-172)});
            getLastLottery();
        });
        function getLastLottery() {
          
            var url = getCookie("LastLottery");
            url = (url == null || url == "") ? ("/Lottery/GameCenter.aspx?ltcode=cqssc&ltid=1&ln=<%=System.Web.HttpUtility.UrlEncode("重庆时时彩")%>&ico=lottery_ssc.png") : url;
            $("#main").attr("src", url);
            $(".con_ul a").each(function () {
                $(this).find("p").removeClass("title_active");
                if ($(this).attr("href") == url) {
                    $(this).find("p").addClass("title_active");
                }
            });
        }
        function setLastLottery(obj) {
            setCookie("LastLottery", $(obj).attr("href"), 30 * 24 * 60 * 60 * 1000);
        }
        //----获取url-----
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        }

        function AutoScroll(obj) {
            $(obj).find("ul:first").animate({ marginTop: "-14px" }, 500, function () {
                $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this);
            });
        }
        (function ($j) {
            $j.positionFixed = function (el) {
                $j(el).each(function () {
                    new fixed(this)
                })
                return el;
            }
            $j.fn.positionFixed = function () {
                return $j.positionFixed(this)
            }
            var fixed = $j.positionFixed.impl = function (el) {
                var o = this;
                o.sts = {
                    target: $j(el).css('position', 'fixed'),
                    container: $j(window)
                }
                o.sts.currentCss = {
                    top: o.sts.target.css('top'),
                    right: o.sts.target.css('right'),
                    bottom: o.sts.target.css('bottom'),
                    left: o.sts.target.css('left')
                }
                if (!o.ie6) return;
                o.bindEvent();
            }
            $j.extend(fixed.prototype, {
                ie6: $.browser.msie && $.browser.version < 7.0,
                bindEvent: function () {
                    var o = this;
                    o.sts.target.css('position', 'absolute')
                    o.overRelative().initBasePos();
                    o.sts.target.css(o.sts.basePos)
                    o.sts.container.scroll(o.scrollEvent()).resize(o.resizeEvent());
                    o.setPos();
                },
                overRelative: function () {
                    var o = this;
                    var relative = o.sts.target.parents().filter(function () {
                        if ($j(this).css('position') == 'relative') return this;
                    })
                    if (relative.size() > 0) relative.after(o.sts.target)
                    return o;
                },
                initBasePos: function () {
                    var o = this;
                    o.sts.basePos = {
                        top: o.sts.target.offset().top - (o.sts.currentCss.top == 'auto' ? o.sts.container.scrollTop() : 0),
                        left: o.sts.target.offset().left - (o.sts.currentCss.left == 'auto' ? o.sts.container.scrollLeft() : 0)
                    }
                    return o;
                },
                setPos: function () {
                    var o = this;
                    o.sts.target.css({
                        top: o.sts.container.scrollTop() + o.sts.basePos.top,
                        left: o.sts.container.scrollLeft() + o.sts.basePos.left
                    })
                },
                scrollEvent: function () {
                    var o = this;
                    return function () {
                        o.setPos();
                    }
                },
                resizeEvent: function () {
                    var o = this;
                    return function () {
                        setTimeout(function () {
                            o.sts.target.css(o.sts.currentCss)
                            o.initBasePos();
                            o.setPos()
                        }, 1)
                    }
                }
            })
        })(jQuery)

        function showDetail(code, betType,issuecode) {
            var winHeight = $(window).height();
            var openHeight = winHeight - 200;
            var ul = "url:/Lottery/BettingDetail.aspx?betcode=" + code;
            if (betType == 1) {
                ul = "url:/Lottery/BettingDetail.aspx?catchCode=" + code + "&issueCode=" + issuecode;
            }
            $.dialog({
                id: 'betdetail',
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "查看详情",
                content: ul,
                width: 820,
                height:568
            });
        }

    </script>
</asp:Content>
