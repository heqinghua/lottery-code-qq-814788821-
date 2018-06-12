<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Ytg.ServerWeb.Views.videoGame.index"  MasterPageFile="~/Views/videoGame/video.master"%>


<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="content/css/all.css" rel="stylesheet" />
    <link href="content/css/base.css" rel="stylesheet" />
    <link href="content/css/index.css" rel="stylesheet" />
    <script src="content/js/common.js"></script>
    <script type="text/javascript">
        $(function () {
        });

    </script>
   
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">

    <DIV class="main" >
        <DIV class="magbp img_block" id="slideshow">
            <UL class="bjqs main-banner">
              
                <LI>
                    <A href="javascript:;">
                        <IMG width="1920" height="460" src="/Views/videoGame/content/banner-big3.jpg">
                    </A>
                </LI>
                <LI style="display: none;">
                    <A href="javascript:;">
                        <IMG width="1920" height="460"
                             src="/Views/videoGame/content/banner-big4.jpg">
                    </A>
                </LI>
                <LI style="display: none;">
                    <A href="javascript:;">
                        <IMG width="1920" height="460"
                             src="/Views/videoGame/content/banner-big5.jpg">
                    </A>
                </LI>
              
                <LI style="display: none;">
                    <A href="javascript:;">
                        <IMG width="1920" height="460"
                             src="/Views/videoGame/content/banner-big9.jpg">
                    </A>
                </LI>
                <LI style="display: none;">
                    <A href="javascript:;">
                        <IMG width="1920" height="460"
                             src="/Views/videoGame/content/banner-big10.jpg">
                    </A>
                </LI>
            </UL>
        </DIV>
        <DIV class="main-bj" style="margin-left:50px;height:761px;">
            <DIV class="rea-game index-game tab center ym-grid">
                <DIV class="i-g-hd">
                    <UL class="tabHd-item tabHd">
                        <LI class="cur">
                            <I title="AG"></I><A title="AG" href="javascript:;">
                                <IMG width="165" height="112" alt="" src="/Views/videoGame/content/re-icon1.png">
                                <SPAN class="ssc">AG</SPAN>
                            </A>
                        </LI>
                        <LI>
                            <I title="BBIN"></I>						 <A title="BBIN" href="javascript:;">
                                <IMG width="165"
                                     height="112" alt="" src="/Views/videoGame/content/re-icon2.png">							 <SPAN class="kl">BBIN</SPAN>
                            </A>
                        </LI>

                        <LI>
                            <I title="OG"></I>						 <A title="OG" href="javascript:;">
                                <IMG width="165"
                                     height="112" alt="" src="/Views/videoGame/content/re-icon5.png">							 <SPAN class="ty">OG</SPAN>
                            </A>
                        </LI>
                        <LI style="display:none;">
                            <I title="欧博"></I>						 <A title="欧博" href="javascript:">
                                <IMG width="165"
                                     height="112" alt="" src="/Views/videoGame/content/re-icon7.png">							 <SPAN class="xpj">OG</SPAN>
                            </A>
                        </LI>
                        <LI>
                            <I title="UC8"></I>						 <A title="UC8" href="javascript:">
                                <IMG width="165"
                                     height="112" alt="" src="/Views/videoGame/content/re-icon8.png">							 <SPAN class="uc8">UC8</SPAN>
                            </A>
                        </LI>
                    </UL>
                </DIV>
                <DIV class="re-ga-Bd">
                    <DIV class="i-g-bd tabBd">
                        <DIV class="cur tabBd-item">
                            <DIV class="ym-gl">
                                <DIV class="jianjie">
                                    <H5>AG平台 <SPAN>Asia Gaming</SPAN></H5>
                                    <P>
                                        乐诚网已与多家平台商进行全方面的技术合作，旨在打造真人游戏品种最多、最全的一站式博彩体验。多平台自主选择，乐诚网让您的娱乐更加便捷、轻松、高效。所有游戏经由TST
                                        Game国际认证并保证公平公正。
                                    </P>
                                </DIV>
                                <UL>
                                    <LI><A onclick="transferGame('0004', true)" href="javascript:;">进入游戏</A></LI>
                                </UL>
                            </DIV>
                            <DIV class="zrimg ym-gr"><IMG width="520" height="260" alt="" src="/Views/videoGame/content/zr-banner-x1.jpg"></DIV>
                        </DIV>
                        <DIV class="tabBd-item">
                            <DIV class="ym-gl">
                                <DIV class="jianjie">
                                    <H5>BBIN平台 <SPAN>Bbin Casino</SPAN></H5>
                                    <P>
                                        乐诚网已与多家平台商进行全方面的技术合作，旨在打造真人游戏品种最多、最全的一站式博彩体验。多平台自主选择，乐诚网让您的娱乐更加便捷、轻松、高效。所有游戏经由TST
                                        Game国际认证并保证公平公正。
                                    </P>
                                </DIV>
                                <UL>
                                    <LI>
                                        <A onclick="transferGame('0003', true)"
                                           href="javascript:;">进入游戏</A>
                                    </LI>
                                </UL>
                            </DIV>
                            <DIV class="zrimg ym-gr"><IMG width="520" height="260" alt="" src="/Views/videoGame/content/zr-banner-x2.jpg"></DIV>
                        </DIV>

                        <DIV class="tabBd-item">
                            <DIV class="ym-gl">
                                <DIV class="jianjie">
                                    <H5>OG平台 <SPAN>Biental Club</SPAN></H5>
                                    <P>
                                        乐诚网已与多家平台商进行全方面的技术合作，旨在打造真人游戏品种最多、最全的一站式博彩体验。乐诚网让您的娱乐更加便捷、轻松、高效。所有游戏经由TST
                                        Game国际认证并保证公平公正。
                                    </P>
                                </DIV>
                                <UL>
                                    <LI>
                                        <A onclick="transferGame('0001', true)"
                                           href="javascript:;">进入游戏</A>
                                    </LI>
                                </UL>
                            </DIV>
                            <DIV class="zrimg ym-gr"><IMG width="520" height="260" alt="" src="/Views/videoGame/content/zr-banner-x5.jpg"></DIV>
                        </DIV>
                        <DIV class="tabBd-item">
                            <DIV class="ym-gl">
                                <DIV class="jianjie">
                                    <H5>欧博平台 <SPAN>Allbet</SPAN></H5>
                                    <P>
                                        乐诚网已与多家平台商进行全方面的技术合作，旨在打造真人游戏品种最多、最全的一站式博彩体验。多平台自主选择，乐诚网让您的娱乐更加便捷、轻松、高效。所有游戏经由TST
                                        Game国际认证并保证公平公正。
                                    </P>
                                </DIV>
                                <UL>
                                    <LI>
                                        <A onclick="transferGame('0005', true)"
                                           href="javascript:;">进入游戏</A>
                                    </LI>
                                </UL>
                            </DIV>
                            <DIV class="zrimg ym-gr"><IMG width="520" height="260" alt="" src="/Views/videoGame/content/zr-banner-x6.jpg"></DIV>
                        </DIV>
                        <DIV class="tabBd-item">
                            <DIV class="ym-gl">
                                <DIV class="jianjie">
                                    <H5>UC8平台<SPAN>&nbsp;&nbsp;&nbsp;YOUXIBA</SPAN></H5>
                                    <P>作为一家私人控股的全球网络博彩软件供应商，环球股份有限公司致力于提供高端品质的游戏产品。四十余载博彩业的丰富经验赋予了我们足够的信心：我们所做的一切都是正确的！</P>
                                    <P>
                                        我们的所有软件均已通过国际游戏实验室（GLI）、技术系统测试（TST）以及游戏评测机构BMM
                                        Testlabs等世界顶尖游戏测试实验室的测试。同时我们还拥有由富思特卡加延休闲度假集团（First Cagayan Leisure and Resort
                                        Corporation）签发的授权经营许可牌照。这些举措确保了我们游戏软件及相关业务的公平性及透明度。
                                    </P>
                                    <P>我们所有的产品均可与不同设备兼容。您只需告知您的需求，我们即可提供适合您的解决方案！</P>
                                </DIV>
                                <UL>
                                    <LI>
                                        <A onclick="transferGame('0010', true)"
                                           href="javascript:">进入游戏</A>
                                    </LI>
                                </UL>
                            </DIV>
                            <DIV class="zrimg ym-gr"><IMG width="520" height="260" alt="" src="/Views/videoGame/content/zr-banner-x8.jpg"></DIV>
                        </DIV>
                    </DIV>
                </DIV>
            </DIV>
        </DIV>
    </DIV>
    <script type="text/javascript">
    function transferGame() {
        $.alert("平台暂未开通，敬请期待！");
    }
</script>
</asp:Content>
