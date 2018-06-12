<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lottery.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.Lottery" %>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>极速秒秒彩</title>
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <link class="component" href="/Mobile/res/eAELXT5DOhSIAQ!sA18_.shtml" rel="stylesheet" type="text/css">
    <link class="component" href="/Mobile/res/eAELXT5DOhSIAQ!sA18_(1).shtml" media="rich-extended-skinning" rel="stylesheet" type="text/css">
    <script type="text/javascript">window.RICH_FACES_EXTENDED_SKINNING_ON = true;</script>
    <script src="/Mobile/res/skinning.js.shtml" type="text/javascript"></script>
    <link rel="stylesheet" href="/Mobile/res/bootstrap.css">
    <link rel="stylesheet" href="v/res/bootstrap-theme.css">
    <link rel="stylesheet" href="/Mobile/res/lottery.css">
    <link rel="stylesheet" href="/Mobile/res/font-awesome.min.css">
    <script language="javascript" type="text/javascript" src="/Mobile/res/fastclick.js"></script>
    <script language="javascript" type="text/javascript" src="/Mobile/res/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="/Mobile/res/gamecommon.js"></script>
    <script language="javascript" src="/Mobile/res/jquery.dialogUI.js"></script>
    <script language="javascript" src="/Mobile/res/lang_zh.js"></script>
    <script language="javascript" src="/Mobile/res/face.15.min.js" type="text/javascript"></script>
    <script language="javascript" src="/Mobile/res/methods.15.js" type="text/javascript"></script>
    <script language="javascript" src="/Mobile/res/jquery.game.50.js"></script>
    <script language="javascript" src="/Mobile/res/extSsc.js"></script>
</head>
<body>
    <form id="mainForm" name="mainForm" method="post" action="http://gameofasia.com/mmc.shtml" enctype="application/x-www-form-urlencoded">
        <input type="hidden" name="mainForm" value="mainForm">
        <input type="hidden" name="lotteryid" id="lotteryid" value="50">
        <input type="hidden" name="flag" id="flag" value="save">
        <!-- TOP INFO -->
        <nav class="col-xs-12 title">
	    <a id="J-goback" href="http://gameofasia.com/main.shtml" class="go-back">返回</a>
	    <span class="desc">
	    	精彩游戏，尽在极速秒秒彩！
	    </span>
	</nav>
        <div class="result-ball-show" id="showcodebox">
            <div class="gg_bg">x</div>
            <div class="gg_bg">x</div>
            <div class="gg_bg">x</div>
            <div class="gg_bg">x</div>
            <div class="gg_bg">x</div>
        </div>
        <div id="c_top_leftban" style="height: 125px; display: none">
            <table border="0" style="height: 120px;">
                <tbody>
                    <tr>
                        <td width="200px" align="center">
                            <div class="bdmmcSign"></div>
                        </td>
                        <td>
                            <div class="s_mainsplit"></div>
                        </td>
                        <td class="c_top_rightban" style="color: #FFFFFF; font-size: 12px; text-align: center;">
                            <div class="c_top_righttitle">
                                <font color="#e4ff00">名人秒秒彩</font>
                            </div>
                        </td>
                        <td>
                            <div class="s_mainsplit"></div>
                        </td>
                        <td style="width: 25px;">
                            <table style="color: #75cdc9; margin-left: 3px;">
                                <tbody>
                                    <tr>
                                        <td>最</td>
                                    </tr>
                                    <tr>
                                        <td>新</td>
                                    </tr>
                                    <tr>
                                        <td>开</td>
                                    </tr>
                                    <tr>
                                        <td>奖</td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td style="width: 210px;">
                            <table width="100%" border="0" style="text-align: center;" id="ewinnumber">
                                <tbody>
                                    <tr>
                                        <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #90ff00;"></td>
                                    </tr>
                                    <tr>
                                        <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                                    </tr>
                                    <tr>
                                        <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                                    </tr>
                                    <tr>
                                        <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                                    </tr>
                                    <tr>
                                        <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--END TOP INFO -->
        <!--玩法选择面板-->
        <div class="select-panel clearfix">
            <div class="method-list-container">
                <div id="tabbar-div-s2" class="method-list-panel f-left"><span class="tab-back" value="0" tag="0" default="0"><span class="tabbar-left"></span><span class="content">五星</span><span class="tabbar-right"></span></span><span class="tab-back" value="1" tag="0" default="0"><span class="tabbar-left"></span><span class="content">四星</span><span class="tabbar-right"></span></span><span class="tab-front" value="2" tag="0" default="1"><span class="tabbar-left"></span><span class="content">后三</span><span class="tabbar-right"></span></span><span class="tab-back" value="3" tag="0" default="0"><span class="tabbar-left"></span><span class="content">前三</span><span class="tabbar-right"></span></span><span class="tab-back" value="4" tag="0" default="0"><span class="tabbar-left"></span><span class="content">后二</span><span class="tabbar-right"></span></span><span class="tab-back" value="5" tag="0" default="0"><span class="tabbar-left"></span><span class="content">前二</span><span class="tabbar-right"></span></span><span class="tab-back" value="6" tag="0" default="0"><span class="tabbar-left"></span><span class="content">定位胆</span><span class="tabbar-right"></span></span><span class="tab-back" value="7" tag="0" default="0"><span class="tabbar-left"></span><span class="content">不定位</span><span class="tabbar-right"></span></span><span class="tab-back" value="8" tag="0" default="0"><span class="tabbar-left"></span><span class="content">大小单双</span><span class="tabbar-right"></span></span><span class="tab-back" value="9" tag="1" default="0" style="display: none;"><span class="tabbar-left"></span><span class="content">任选二</span><span class="tabbar-right"></span></span><span class="tab-back" value="10" tag="1" default="0" style="display: none;"><span class="tabbar-left"></span><span class="content">任选三</span><span class="tabbar-right"></span></span><span class="tab-back" value="11" tag="1" default="0" style="display: none;"><span class="tabbar-left"></span><span class="content">任选四</span><span class="tabbar-right"></span></span><span class="tab-back" id="changemode"><span class="tabbar-left"></span><span class="content" title="点击切换"><font color="#1456b9">任选玩法</font></span><span class="tabbar-right"></span></span></div>
                <div class="method-details">
                    <ul id="tabbar-div-s3">
                        <li class="tz_li"><span class="tz_title">后三直选</span><div class="act"><span class="method-tab-front" id="smalllabel_0_0">直选复式</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_0_1">直选单式</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_0_2">后三组合</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_0_3">直选和值</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_0_4">直选跨度</span></div>
                        </li>
                        <li class="tz_li"><span class="tz_title">后三组选</span><div class="back"><span class="method-tab-back" id="smalllabel_1_0">组三复式</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_1_1">组三单式</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_1_2">组六复式</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_1_3">组六单式</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_1_4">混合组选</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_1_5">组选和值</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_1_6">组选包胆</span></div>
                        </li>
                        <li class="tz_li"><span class="tz_title">后三其它</span><div class="back"><span class="method-tab-back" id="smalllabel_2_0">和值尾数</span></div>
                            <div class="back"><span class="method-tab-back" id="smalllabel_2_1">特殊号</span></div>
                        </li>
                    </ul>
                </div>
            </div>
            <div id="J-method-name" class="method-name">后三直选复式</div>
            <div id="J-method-select" class="method-list-button">展开玩法</div>
        </div>
        <!--子集玩法列表-->
        <div id="c_list_label" class="main-ball-panel">
            <div class="c_msginfo">
                <div id="lt_descban" style="display: none">
                    <div id="lt_desc">从百位、十位、个位各选一个号码组成一注。</div>
                    <span id="lt_example">示例</span>
                    <div id="lt_help"></div>
                </div>
                <div class="tbn_c_s number-select-content">
                    <div id="lt_selector">
                        <div class="nbs">
                            <div class="ti">百位</div>
                            <div class="nb">
                                <div name="lt_place_0">0</div>
                                <div name="lt_place_0">1</div>
                                <div name="lt_place_0">2</div>
                                <div name="lt_place_0">3</div>
                                <div name="lt_place_0">4</div>
                                <div name="lt_place_0">5</div>
                                <div name="lt_place_0">6</div>
                                <div name="lt_place_0">7</div>
                                <div name="lt_place_0">8</div>
                                <div name="lt_place_0">9</div>
                            </div>
                            <div class="to">
                                <ul>
                                    <li class="l"></li>
                                    <li class="dxjoq" name="all">全</li>
                                    <li class="dxjoq" name="big">大</li>
                                    <li class="dxjoq" name="small">小</li>
                                    <li class="dxjoq" name="odd">奇</li>
                                    <li class="dxjoq" name="even">偶</li>
                                    <li class="dxjoq" name="clean">清</li>
                                    <li class="r"></li>
                                </ul>
                            </div>
                        </div>
                        <div class="nbs">
                            <div class="ti">十位</div>
                            <div class="nb">
                                <div name="lt_place_1">0</div>
                                <div name="lt_place_1">1</div>
                                <div name="lt_place_1">2</div>
                                <div name="lt_place_1">3</div>
                                <div name="lt_place_1">4</div>
                                <div name="lt_place_1">5</div>
                                <div name="lt_place_1">6</div>
                                <div name="lt_place_1">7</div>
                                <div name="lt_place_1">8</div>
                                <div name="lt_place_1">9</div>
                            </div>
                            <div class="to">
                                <ul>
                                    <li class="l"></li>
                                    <li class="dxjoq" name="all">全</li>
                                    <li class="dxjoq" name="big">大</li>
                                    <li class="dxjoq" name="small">小</li>
                                    <li class="dxjoq" name="odd">奇</li>
                                    <li class="dxjoq" name="even">偶</li>
                                    <li class="dxjoq" name="clean">清</li>
                                    <li class="r"></li>
                                </ul>
                            </div>
                        </div>
                        <div class="nbs">
                            <div class="ti">个位</div>
                            <div class="nb">
                                <div name="lt_place_2">0</div>
                                <div name="lt_place_2">1</div>
                                <div name="lt_place_2">2</div>
                                <div name="lt_place_2">3</div>
                                <div name="lt_place_2">4</div>
                                <div name="lt_place_2">5</div>
                                <div name="lt_place_2">6</div>
                                <div name="lt_place_2">7</div>
                                <div name="lt_place_2">8</div>
                                <div name="lt_place_2">9</div>
                            </div>
                            <div class="to">
                                <ul>
                                    <li class="l"></li>
                                    <li class="dxjoq" name="all">全</li>
                                    <li class="dxjoq" name="big">大</li>
                                    <li class="dxjoq" name="small">小</li>
                                    <li class="dxjoq" name="odd">奇</li>
                                    <li class="dxjoq" name="even">偶</li>
                                    <li class="dxjoq" name="clean">清</li>
                                    <li class="r"></li>
                                </ul>
                            </div>
                        </div>
                        <div class="c"></div>
                    </div>
                </div>
            </div>
        </div>
        <!--开奖记录-->
        <div id="J-resultrecord-container" class="recordMMC-info deitails-control" style="border-radius: 5px 0 0 0;">
            <div style="margin-top: 10px;">
                余额：
				
                <font color="#90ff00" size="2">16.0211</font>
                <div class="inner">
                    倍数
                    <input type="number" size="2" id="lt_sel_times" name="lt_sel_times" class="input mutil-bei">
                </div>
                <div class="inner">
                    模式
                    <select name="lt_sel_modes" id="lt_sel_modes" class="select3">
                        <option value="1">元</option>
                        <option value="2">角</option>
                        <option value="3">分</option>
                        <option value="4">厘</option>
                    </select>
                </div>
                <div class="inner">
                    <span id="lt_sel_prize" class="resend">返点<select name="lt_sel_dyprize" id="lt_sel_dyprize"><option value="1940|0">1940-0%</option>
                        <option value="1800|0.07">1800-7%</option>
                    </select></span>
                </div>
                <br>
                最近开奖结果：<br>

                <table style="cellspacing: 100; color: green; margin-left: 10px" id="result">
                    <tbody>
                        <tr>
                            <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #90ff00;"></td>
                        </tr>
                        <tr>
                            <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                        </tr>
                        <tr>
                            <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                        </tr>
                        <tr>
                            <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                        </tr>
                        <tr>
                            <td style="BORDER-BOTTOM: 1px dashed #5c8482; color: #FFFFFF;"></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="J-result-record" class="record-button">展开面板</div>
        </div>
        <!-- <div id="J-gamesetting-container" class="deitails-control">
		<div class="inner">
			倍数 
			<input type="number" size="2" value="" id="lt_sel_times" name="lt_sel_times" class="input mutil-bei" /> 
		</div>
		<div class="inner">
			模式
			<select name="lt_sel_modes" id="lt_sel_modes" class="select3"></select>
		</div>
		<div class="inner">
			<span id="lt_sel_prize" class="resend"></span>
		</div>
		<div id="J-game-setting" class="btn-parma">
			参数设置
		</div>
	</div> -->
        <!--最近投注记录-->
        <div class="new-history-record">
            <div class="close"><i class="fa fa-times"></i></div>
            <iframe class="new-record-content" src="./极速秒秒彩_files/NewBet.html" width="300px" id="ifNewBet" frameborder="0" height="200px" scrolling="auto"></iframe>
        </div>

        <!-- <div id="J-balance-info" class="balance-info">
		<div class="balance-area">
			<h:outputText value="16.0211" converter="DoubleConverter"/>
		</div>
		<div class="balance-button">余额</div>
	</div> -->
        <div class="c_betcontent ensure-info ensure-display-none">
            <div class="close"><i class="fa fa-times"></i></div>
            <!-- 添加投注 -->
            <table cellpadding="0" cellspacing="0" width="98%">
                <tbody>
                    <tr>
                        <td>
                            <!-- 投注Table记录 -->
                            <div class="c_betcontenttitle">
                                <span style="float: left; font-weight: bold;">投注项：　
					
                                    <span id="lt_cf_count" style="color: #e4ff00">0</span>
                                </span>
                                <span class="c_clearall" id="lt_cf_clear" title="删除全部"></span>
                            </div>
                            <div class="c_betRecodeList">
                                <table cellpadding="0" id="lt_cf_content" width="100%" cellspacing="0"></table>
                            </div>
                            <!-- CONFIRM -->
                            <div class="c_confirmArea">
                                <div>
                                    总注数 <span style="font-weight: bold; color: #e4ff00; font-size: 14px;" id="lt_cf_nums">0</span> 注
				
                                </div>
                                <div>
                                    总金额 ：￥<font style="font-weight: bold; color: #e4ff00; font-size: 14px;" id="lt_cf_money">0</font>
                                </div>
                                <div>
                                    连续购买：
					
                                    <span id="lt_issues" class="select select3">
                                        <select style="width: 70px" id="lt_issue_start" class="input" name="lt_issue_start">
                                            <option value="1">1期</option>
                                            <option value="2">2期</option>
                                            <option value="5">5期</option>
                                            <option value="8">8期</option>
                                            <option value="10">10期</option>
                                            <option value="15">15期</option>
                                            <option value="20">20期</option>
                                            <option value="25">25期</option>
                                            <option value="30">30期</option>
                                            <option value="40">40期</option>
                                            <option value="50">50期</option>
                                        </select>
                                        <input type="hidden" name="lt_total_nums" id="lt_total_nums" value="0">
                                        <input type="hidden" name="lt_total_money" id="lt_total_money" value="0">
                                    </span>
                                </div>
                                <div class="button-control-area">
                                    <input type="button" id="lt_buy" class="btn btn-success col-xs-6" value="投注">
                                    <input type="button" id="re-select-ball" class="btn btn-warning col-xs-5 re-select" value="再选一注">
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
      <div class="order-info">你选择了 <em id="lt_sel_nums">0</em> 注，费用为：<strong class="price" id="lt_sel_money">0</strong> 元</div> 
	    <ul class="nav navbar-nav chose-ball col-xs-12">
        <li class="col-xs-4"><a href="javascript:;" id="game-record-list"><i class="fa fa-list mr5"></i>历史投注</a></li>
        <li class="col-xs-4"><a href="javascript:;" id="game-order-details"><i class="fa fa-qrcode mr5"></i>选号篮</a></li>
        <li class="col-xs-4"><a href="javascript:;" id="lt_sel_insert"><i class="fa fa-plus-square mr5"></i>立即投注</a></li>
      </ul>
	</nav>
        <input type="hidden" name="javax.faces.ViewState" id="javax.faces.ViewState" value="j_id36" autocomplete="off">
    </form>
    <script type="text/javascript">
        var pri_user_data = [{ methodid: 654, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 865, prize: { 1: '180000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194000.00 }, { 'point': '0.07', 'prize': 180000.0 }] }] }, { methodid: 755, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 867, prize: { 1: '180000.0', 2: '18000.0', 3: '1800.0', 4: '180.0', 5: '18.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194000.00 }, { 'point': '0.07', 'prize': 180000.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 18000.0 }, { 'point': 0, 'prize': 19400.00 }] }, { 'level': 3, 'prize': [{ 'point': '0.07', 'prize': 1800.0 }, { 'point': 0, 'prize': 1940.00 }] }, { 'level': 4, 'prize': [{ 'point': '0.07', 'prize': 180.0 }, { 'point': 0, 'prize': 194.00 }] }, { 'level': 5, 'prize': [{ 'point': '0.07', 'prize': 18.0 }, { 'point': 0, 'prize': 19.40 }] }] }, { methodid: 664, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 869, prize: { 1: '1500.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1616.62 }, { 'point': '0.07', 'prize': 1500.0 }] }] }, { methodid: 765, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 870, prize: { 1: '3000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 3233.31 }, { 'point': '0.07', 'prize': 3000.0 }] }] }, { methodid: 676, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 871, prize: { 1: '6000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 6466.62 }, { 'point': '0.07', 'prize': 6000.0 }] }] }, { methodid: 708, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 872, prize: { 1: '9000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 9700.00 }, { 'point': '0.07', 'prize': 9000.0 }] }] }, { methodid: 686, prize: { 1: '600.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }] }, { methodid: 873, prize: { 1: '18000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19400.00 }, { 'point': '0.07', 'prize': 18000.0 }] }] }, { methodid: 696, prize: { 1: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 323.31 }, { 'point': '0.07', 'prize': 300.0 }] }] }, { methodid: 874, prize: { 1: '36000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 38800.00 }, { 'point': '0.07', 'prize': 36000.0 }] }] }, { methodid: 775, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 718, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 725, prize: { 1: '18000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19400.00 }, { 'point': '0.07', 'prize': 18000.0 }] }] }, { methodid: 730, prize: { 1: '750.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 808.31 }, { 'point': '0.07', 'prize': 750.0 }] }] }, { methodid: 735, prize: { 1: '1500.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1616.62 }, { 'point': '0.07', 'prize': 1500.0 }] }] }, { methodid: 740, prize: { 1: '3000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 3233.31 }, { 'point': '0.07', 'prize': 3000.0 }] }] }, { methodid: 745, prize: { 1: '4500.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 4850.00 }, { 'point': '0.07', 'prize': 4500.0 }] }] }, { methodid: 85, prize: { 1: '4.3' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 4.64 }, { 'point': '0.07', 'prize': 4.3 }] }] }, { methodid: 86, prize: { 1: '22.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 23.71 }, { 'point': '0.07', 'prize': 22.0 }] }] }, { methodid: 87, prize: { 1: '210.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 226.31 }, { 'point': '0.07', 'prize': 210.0 }] }] }, { methodid: 88, prize: { 1: '3900.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 4204.29 }, { 'point': '0.07', 'prize': 3900.0 }] }] }, { methodid: 2, prize: { 1: '18000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19400.00 }, { 'point': '0.07', 'prize': 18000.0 }] }] }, { methodid: 4, prize: { 1: '18000.0', 2: '1800.0', 3: '180.0', 4: '18.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19400.00 }, { 'point': '0.07', 'prize': 18000.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 1800.0 }, { 'point': 0, 'prize': 1940.00 }] }, { 'level': 3, 'prize': [{ 'point': '0.07', 'prize': 180.0 }, { 'point': 0, 'prize': 194.00 }] }, { 'level': 4, 'prize': [{ 'point': '0.07', 'prize': 18.0 }, { 'point': 0, 'prize': 19.40 }] }] }, { methodid: 6, prize: { 1: '750.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 808.31 }, { 'point': '0.07', 'prize': 750.0 }] }] }, { methodid: 7, prize: { 1: '1500.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1616.62 }, { 'point': '0.07', 'prize': 1500.0 }] }] }, { methodid: 8, prize: { 1: '3000.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 3233.31 }, { 'point': '0.07', 'prize': 3000.0 }] }] }, { methodid: 9, prize: { 1: '4500.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 4850.00 }, { 'point': '0.07', 'prize': 4500.0 }] }] }, { methodid: 11, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 12, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 13, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 15, prize: { 1: '1800.0', 2: '180.0', 3: '18.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 180.0 }, { 'point': 0, 'prize': 194.00 }] }, { 'level': 3, 'prize': [{ 'point': '0.07', 'prize': 18.0 }, { 'point': 0, 'prize': 19.40 }] }] }, { methodid: 17, prize: { 1: '600.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }] }, { methodid: 18, prize: { 1: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 323.31 }, { 'point': '0.07', 'prize': 300.0 }] }] }, { methodid: 19, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 20, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 21, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 23, prize: { 1: '18.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19.40 }, { 'point': '0.07', 'prize': 18.0 }] }] }, { methodid: 25, prize: { 1: '180.0', 2: '30.0', 3: '6.6' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 30.0 }, { 'point': 0, 'prize': 32.32 }] }, { 'level': 3, 'prize': [{ 'point': '0.07', 'prize': 6.6 }, { 'point': 0, 'prize': 7.10 }] }] }, { methodid: 27, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 28, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 29, prize: { 1: '1800.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }] }, { methodid: 31, prize: { 1: '1800.0', 2: '180.0', 3: '18.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 1940.00 }, { 'point': '0.07', 'prize': 1800.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 180.0 }, { 'point': 0, 'prize': 194.00 }] }, { 'level': 3, 'prize': [{ 'point': '0.07', 'prize': 18.0 }, { 'point': 0, 'prize': 19.40 }] }] }, { methodid: 33, prize: { 1: '600.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }] }, { methodid: 34, prize: { 1: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 323.31 }, { 'point': '0.07', 'prize': 300.0 }] }] }, { methodid: 35, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 36, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 37, prize: { 1: '600.0', 2: '300.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 646.62 }, { 'point': '0.07', 'prize': 600.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 300.0 }, { 'point': 0, 'prize': 323.31 }] }] }, { methodid: 39, prize: { 1: '18.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19.40 }, { 'point': '0.07', 'prize': 18.0 }] }] }, { methodid: 41, prize: { 1: '180.0', 2: '30.0', 3: '6.6' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }, { 'level': 2, 'prize': [{ 'point': '0.07', 'prize': 30.0 }, { 'point': 0, 'prize': 32.31 }] }, { 'level': 3, 'prize': [{ 'point': '0.07', 'prize': 6.6 }, { 'point': 0, 'prize': 7.09 }] }] }, { methodid: 43, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 44, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 45, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 47, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 48, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 49, prize: { 1: '180.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 194.00 }, { 'point': '0.07', 'prize': 180.0 }] }] }, { methodid: 51, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 52, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 53, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 55, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 56, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 57, prize: { 1: '90.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 97.00 }, { 'point': '0.07', 'prize': 90.0 }] }] }, { methodid: 59, prize: { 1: '18.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19.40 }, { 'point': '0.07', 'prize': 18.0 }] }] }, { methodid: 65, prize: { 1: '6.6' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 7.10 }, { 'point': '0.07', 'prize': 6.6 }] }] }, { methodid: 66, prize: { 1: '6.6' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 7.10 }, { 'point': '0.07', 'prize': 6.6 }] }] }, { methodid: 68, prize: { 1: '33.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 35.58 }, { 'point': '0.07', 'prize': 33.0 }] }] }, { methodid: 69, prize: { 1: '33.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 35.58 }, { 'point': '0.07', 'prize': 33.0 }] }] }, { methodid: 71, prize: { 1: '5.2' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 5.59 }, { 'point': '0.07', 'prize': 5.2 }] }] }, { methodid: 73, prize: { 1: '18.4' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 19.83 }, { 'point': '0.07', 'prize': 18.4 }] }] }, { methodid: 75, prize: { 1: '12.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 12.95 }, { 'point': '0.07', 'prize': 12.0 }] }] }, { methodid: 77, prize: { 1: '41.0' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 44.19 }, { 'point': '0.07', 'prize': 41.0 }] }] }, { methodid: 79, prize: { 1: '7.2' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 7.76 }, { 'point': '0.07', 'prize': 7.2 }] }] }, { methodid: 80, prize: { 1: '7.2' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 7.76 }, { 'point': '0.07', 'prize': 7.2 }] }] }, { methodid: 82, prize: { 1: '14.4' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 15.52 }, { 'point': '0.07', 'prize': 14.4 }] }] }, { methodid: 83, prize: { 1: '14.4' }, dyprize: [{ 'level': 1, 'prize': [{ 'point': 0, 'prize': 15.52 }, { 'point': '0.07', 'prize': 14.4 }] }] }];
        var pri_lotteryid = parseInt(50, 10);
        var pri_isdynamic = 1;
        var pri_ajaxurl = "/LotteryService.aspx";
        </script>

    <audio id="J-click-sound" src="images/click.mp3"></audio>
</body>
</html>
