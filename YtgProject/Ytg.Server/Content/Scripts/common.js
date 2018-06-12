var showLotteryCodename = {
    "cqssc": "重庆时时彩",
    "JXSSC": "江西时时彩",
    "hljssc": "黑龙江时时彩",
    "xjssc": "新疆时时彩",
    "tjssc": "天津时时彩",
    "gd11x5": "广东11选5",
    "fc3d": "福彩3D",
    "shssl": "上海时时乐",
    "pl5": "排列三、五",
    "bjkl8": "北京快乐8",
    "yifencai": "加拿大一分",
    "erfencai": "加拿大三分",
    "FFC1": "埃及分分彩",
    "VIFFC5": "河内时时彩",
    "krkeno": "韩国1.5分彩",
    "sf11x5": "三分11选5",
    "wf11x5": "五分11选5",
    "sd11x5": "山东11选5",
    "jx11x5": "江西11选5",
    "hk6": "香港六合彩",
    "jsk3": "江苏快三",
    "INFFC5": "印尼时时彩",
    "FFC2": "埃及二分彩",
    "twbingo": "台湾五分彩",
    "bjpk10": "北京PK10",
    "tenct": "腾讯分分彩",
};
//全选，取消全选
function selectAll(obj)
{
	jQuery(":checkbox[id!='"+obj+"']").attr("checked",jQuery("#"+obj).attr("checked"));
}
$(function () {

    $(".baomi_box>button").click(function () {
        $(".baomi_box>button").each(function () {
            if ($(this).hasClass("btn_org")) {
                $(this).addClass("btn_gray");
                $(this).removeClass("btn_org");
            }
        });
        $(this).removeClass("btn_org");
        $(this).addClass("btn_org");
        $("#baomi_hidden").val($(this).attr("tag"));
    });

    $(".dg").click(function(){
        $(".dg").removeClass("active");
        $(".dg >input:radio").removeAttr("checked");
        $(this).addClass("active");
        $(this).find("input:radio").attr("checked", "checked");
        if ($(this).attr("id") == "hemai_box")
            $(".more_operate").show();
        else
            $(".more_operate").hide();
        $('#main', parent.document).height($('#main', parent.document).contents().find("body").height()); //重设 iframe 高度
    });
    $("#share_cnt").keyup(function () {
        var times = $(this).val().replace(/[^0-9]/g, "").substring(0, 5);
        var minmonery = parseFloat($("#game_total_fen").html());
        var maxmonery = parseFloat($("#game_tzallmon").html())

        var bymonery = parseFloat(times);
        if (bymonery < minmonery) {
            //$("#share_cnt").val($("#game_total_fen").html());
            return;
        } else if (bymonery > maxmonery)
        {
            $("#share_cnt").val($("#game_tzallmon").html());
            return;
        }
        $(this).val(times);
        
    });
    $("#share_cnt").focusout(function () {
        var times = $(this).val().replace(/[^0-9]/g, "").substring(0, 5);
        var minmonery = parseFloat($("#game_total_fen").html());
        var maxmonery = parseFloat($("#game_tzallmon").html())
        var bymonery = parseFloat(times);
        if (bymonery < minmonery) {
            $("#share_cnt").val($("#game_total_fen").html());
            return;
        }
    });

});
//组合数计算
function Combination(n, m)
{
	m = parseInt(m);
	n = parseInt(n);
	if( m<0 || n<0 )
	{
		return false;
	}
	if( m==0 || n == 0 )
	{
		return 1;
	}
	if( m > n )
	{
		return 0;
	}
	if( m > n/2.0)
	{
		m = n - m;
	}

	var result = 0.0;
	for(i=n; i>=(n-m+1);i--) {
		result += Math.log(i);			
	}
	for(i=m; i >= 1; i--) {
		result -= Math.log(i);
	}
	result = Math.exp(result);
	return Math.round(result);
}

Array.prototype.each = function(fn){
    fn = fn || Function.K;
    var a = [];
    var args = Array.prototype.slice.call(arguments, 1);
    for(var i = 0; i < this.length; i++){
        var res = fn.apply(this,[this[i],i].concat(args));
        if(res != null) a.push(res);
    }
    return a;
};

/**
 * 得到一个数组不重复的元素集合<br/>
 * 唯一化一个数组
 * @returns {Array} 由不重复元素构成的数组
 */
Array.prototype.uniquelize = function(){
    var ra = new Array();
    for(var i = 0; i < this.length; i ++){
        if(!ra.contains(this[i])){
            ra.push(this[i]);
        }
    }
    return ra;
};

/**
 * 求两个集合的补集
{%example
<script>
    var a = [1,2,3,4];
    var b = [3,4,5,6];
    alert(Array.complement(a,b));
</script>
 %}
 * @param {Array} a 集合A
 * @param {Array} b 集合B
 * @returns {Array} 两个集合的补集
 */
Array.complement = function(a, b){
    return Array.minus(Array.union(a, b),Array.intersect(a, b));
};

/**
 * 求两个集合的交集
{%example
<script>
    var a = [1,2,3,4];
    var b = [3,4,5,6];
    alert(Array.intersect(a,b));
</script>
 %}
 * @param {Array} a 集合A
 * @param {Array} b 集合B
 * @returns {Array} 两个集合的交集
 */
Array.intersect = function(a, b){
    return a.uniquelize().each(function(o){return b.contains(o) ? o : null});
};

/**
 * 求两个集合的差集
{%example
<script>
    var a = [1,2,3,4];
    var b = [3,4,5,6];
    alert(Array.minus(a,b));
</script>
 %}
 * @param {Array} a 集合A
 * @param {Array} b 集合B
 * @returns {Array} 两个集合的差集
 */
Array.minus = function(a, b){
    return a.uniquelize().each(function(o){return b.contains(o) ? null : o});
};

/**
 * 求两个集合的并集
{%example
<script>
    var a = [1,2,3,4];
    var b = [3,4,5,6];
    alert(Array.union(a,b));
</script>
 %}
 * @param {Array} a 集合A
 * @param {Array} b 集合B
 * @returns {Array} 两个集合的并集
 */
Array.union = function(a, b){
    return a.concat(b).uniquelize();
};

Array.prototype.contains = function (element) {
	for (var i = 0; i < this.length; i++) {
		if (this[i] == element) {
			return true;
		}
	}
	return false;
}
/**
* 计算排列组合的个数
*
* @author mark
*
* @param integer $iBaseNumber   基数
* @param integer $iSelectNumber 选择数
*
* @return mixed
*
*/
function GetCombinCount( iBaseNumber, iSelectNumber ){
    if(iSelectNumber > iBaseNumber){
        return 0;
    }
    if( iBaseNumber == iSelectNumber || iSelectNumber == 0 ){
        return 1;//全选
    }
    if( iSelectNumber == 1 ){
        return iBaseNumber;//选一个数
    }
    var iNumerator = 1;//分子
    var iDenominator = 1;//分母
    for(var i = 0; i < iSelectNumber; i++){
        iNumerator *= iBaseNumber - i;//n*(n-1)...(n-m+1)
        iDenominator *= iSelectNumber - i;//(n-m)....*2*1
    }
    return iNumerator / iDenominator;
}


//移动字符串
function movestring(sString){
    var top = '';//开始符串
    var middle = '01'//中间字符串
    var bottom = '';//结束符串
    var tmpone = '';
    var tmptwo = '';
    var find = false;//是否找到分隔字符串
    var findfirst = false;//找到第一个字符
    for (var m=0;m<sString.length;m++){
        if(find == false){
           top += sString.substr(m,1); 
        }
        if( find == false && sString.substr(m,1) == '1'){
            findfirst = true;
        }else if(find == false && findfirst == true && sString.substr(m,1) == '0'){
            find = true;
        }else if(find == true){
           bottom += sString.substr(m,1); 
        }
    }
    top = top.substr(0,top.length-2);
    for (var n=0;n<top.length;n++){
        if(top.substr(n,1) == '1'){
            tmpone += top.substr(n,1);
        }else if(top.substr(n,1) == '0'){
            tmptwo += top.substr(n,1);
        }
    }
    top = tmpone+tmptwo;
    return top+middle+bottom;
}


/**
* 获取指定组合的所有可能性
*
* 例子：5选3
* $aBaseArray = array('01','02','03','04','05');
* ----getCombination($aBaseArray,3)
* 1.初始化一个字符串：11100;--------1的个数表示需要选出的组合
* 2.将1依次向后移动造成不同的01字符串，构成不同的组合，1全部移动到最后面，移动完成：00111.
* 3.移动方法：每次遇到第一个10字符串时，将其变成01,在此子字符串前面的字符串进行倒序排列,后面的不变：形成一个不同的组合.
*            如：11100->11010->10110->01110->11001->10101->01101->10011->01011->00111
*            一共形成十个不同的组合:每一个01字符串对应一个组合---如11100对应组合01 02 03;01101对应组合02 03 05
*
*
* @param  array aBaseArray 基数数组
* @param  int   iSelectNum 选数
* @author mark
*
*/
function getCombination( aBaseArray, iSelectNum ){
    var iBaseNum = aBaseArray.length;
    var aResult = new Array();
    var aString = new Array();
    if(iSelectNum > iBaseNum){
        return aResult;
    }
    if( iSelectNum == 1 ){
        return aBaseArray;
    }
    if( iBaseNum == iSelectNum ){
        aResult[0] = aBaseArray.join(",");
        return aResult;
    }
    var sString = '';
    var sLastString = '';
    var sTempStr = '';
    for (var i=0; i<iSelectNum; i++){
        sString +='1';
        sLastString +='1';
    }
    for (var j=0; j<iBaseNum-iSelectNum; j++){
        sString +='0';
    }
    for (var k=0; k<iSelectNum; k++){
        sTempStr += aBaseArray[k]+',';
    }
    aResult[0] = sTempStr.substr(0,sTempStr.length-1);
    var count = 1;
    sTempStr = '';
    while (sString.substr(sString.length-iSelectNum,iSelectNum) != sLastString){
        sString = movestring(sString);
        for (var k=0; k<iBaseNum; k++){
            if( sString.substr(k,1) == '1' ){
                sTempStr += aBaseArray[k]+',';
            }
        }
        aResult[count] = sTempStr.substr(0, sTempStr.length-1);
        sTempStr = '';
        count++;
    }
    return aResult;
}
//用户名验证(由0-9,a-z,A-Z组成的6~16个字符组成)
function validateUserName( str )
{
	var patrn = /^[0-9a-zA-Z]{6,16}$/;
	if( patrn.exec(str) ){
		return true;	
	}else{
		return false;
	}
}


//密码验证(6－16位数字和字母，不能只是数字，或者只是字母，不能连续三位相同)
function validateUserPss( str )
{
	var patrn = /^[0-9a-zA-Z]{6,16}$/;
	if( !patrn.exec(str) ){
		return false;
	}
	patrn = /^\d+$/;
	if( patrn.exec(str) ){
		return false;
	}
	patrn = /^[a-zA-Z]+$/;
	if( patrn.exec(str) ){
		return false;
	}
	patrn = /(.)\1{2,}/;
	if( patrn.exec(str) ){
		return false;
	}
	return true;
}

//呢称验证
function validateNickName( str )
{
	var patrn = /^(.){2,6}$/;	
	if( patrn.exec(str) )
	{
		return true;	
	}
	else
	{
		return false;
	}
}

//日期输入验证
// str : 要验证的日期字符串[格式包括 Y-M-D|Y/M/D|YMD [H[:I][:S]]]
function validateInputDate( str )
{
	str = jQuery.trim(str);
	if( str == "" || str == null )
	{
		return true;
	}
	var tempArr = str.split(" ");
	var dateArr = new Array();
	var timeArr = new Array();
	if( tempArr[0].indexOf("-") != -1 )
	{
	    //2009-06-12
		dateArr = tempArr[0].split("-");
	}
	else if( tempArr[0].indexOf("/") != -1 )
	{
	    //2009/06/12
		dateArr = tempArr[0].split("/");
	}
	else
	{
	    // 20090612
		if( tempArr[0].toString().length < 8 )
		{
			return false;
		}
		dateArr[0] = tempArr[0].substring(0,4);
		dateArr[1] = tempArr[0].substring(4,6);
		dateArr[2] = tempArr[0].substring(6,8);
	}
	if( tempArr[1] == undefined || tempArr[1] == null )
	{
		tempArr[1] = "00:00:00";
	}
	if( tempArr[1].indexOf(":") != -1 )
	{
		timeArr = tempArr[1].split(":");
	}
	if( dateArr[2] != undefined && ( dateArr[0] == "" || dateArr[1] == "" ) )
	{
		return false;
	}
	if( dateArr[1] != undefined && dateArr[0] == "" )
	{
		return false;
	}
	if( timeArr[2] != undefined && ( timeArr[0] == "" || timeArr[1] == "" ) )
	{
		return false;
	}
	if( timeArr[1] != undefined && timeArr[0] == "" )
	{
		return false;
	}
	dateArr[0]  = (dateArr[0]==undefined || dateArr[0] == "") ? 1970 : dateArr[0];
	dateArr[1]  = (dateArr[1]==undefined || dateArr[1] == "") ? 0 : (dateArr[1]-1);
	dateArr[2]  = (dateArr[2]==undefined || dateArr[2] == "") ? 0 : dateArr[2];
	timeArr[0]  = (timeArr[0]==undefined || timeArr[0] == "") ? 0 : timeArr[0];
	timeArr[1]  = (timeArr[1]==undefined || timeArr[1] == "") ? 0 : timeArr[1];
	timeArr[2]  = (timeArr[2]==undefined || timeArr[2] == "") ? 0 : timeArr[2];
	var newDate = new Date(dateArr[0],dateArr[1],dateArr[2],timeArr[0],timeArr[1],timeArr[2]); 
	if(
	   newDate.getFullYear()==dateArr[0] && newDate.getMonth()==dateArr[1] && newDate.getDate()==dateArr[2] 
	   && newDate.getHours()==timeArr[0] && newDate.getMinutes()==timeArr[1] && newDate.getSeconds()==timeArr[2]
	)
	{
		return true;
	}
	else
	{
		return false;
	}
	return true;
}

//获取日期时分秒
function getdateTime(str) {
    if (str == "")
        return "";
    var array = str.split(' ');
    var ymd = array[0].split('-');
    var sfm = array[1].split(':');
    //  alert(ymd[0], ymd[1], ymd[2], sfm[0], sfm[1], sfm[2]);
    var date = new Date(ymd[0], ymd[1], ymd[2], sfm[0], sfm[1], sfm[2]);
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hour = date.getHours();
    var minute = date.getMinutes();
    var second = date.getSeconds();

    return (hour > 9 ? hour+"" : "0" + hour) + ":" + (minute > 9 ? minute : "0" + minute) + ":" + (second>9?second:"0"+second);
}
//获取日期月，日，时分秒
function getDayTime(str) {
    if (str == "")
        return "";
   
    //if (str.indexOf("-") != -1)
    //    str = str.replace("-", "/");
    //2015-06-07 00:00:00
    //new date("Year","Month","Day","Hour","Minutes","Seconds");
   // alert(str);
    var array = str.split(' ');
    var ymd = array[0].split('-');
    var sfm = array[1].split(':');
  //  alert(ymd[0], ymd[1], ymd[2], sfm[0], sfm[1], sfm[2]);
    var date = new Date(ymd[0], ymd[1], ymd[2],sfm[0],sfm[1],sfm[2]);
    var year = date.getFullYear();
    var month = date.getMonth() ;
    var day = date.getDate();
    var hour = date.getHours();
    var minute = date.getMinutes();
    var second = date.getSeconds();
    
   // var pstr= month + "-" + day;
    //  return pstr+" "+(hour > 9 ? hour + "" : "0" + hour) + ":" + (minute > 9 ? minute : "0" + minute) + ":" + (second > 9 ? second : "0" + second);
    return ymd[1] + "-" + ymd[2] + " " + sfm[0] + ":" + sfm[1] + ":" + sfm[2];
}

//获取日期时分秒
function getdate(date) {
   
    var year = date.getFullYear();
    var month = (date.getMonth() + 1);
    month = month >9? month : "0" + month;
    var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();
    var hour = date.getHours();
    var minute = date.getMinutes();
    var second = date.getSeconds();

    //var tm = (hour > 9 ? hour + "" : "0" + hour) + ":" + (minute > 9 ? minute : "0" + minute) + ":" + (second > 9 ? second : "0" + second);
    return year + "-" + month + "-" + day;
}

//onkeyup:限制用户资金输入只能输入浮点数，并且小数点后只能跟四位
function checkMoney( obj )
{
	obj.value = formatFloat(obj.value);
}

//onkeyup:根据用户输入的资金做检测并自动转换中文大写金额(用于充值和提现)
//obj:检测对象元素，chineseid:要显示中文大小写金额的ID，maxnum：最大能输入金额
function checkWithdraw( obj,chineseid,maxnum )
{
	obj.value = formatFloat(obj.value);
	if( parseFloat(obj.value) > parseFloat(maxnum) )
	{
		alert("输入金额超出了可用余额");	
		obj.value = maxnum;
	}
	jQuery("#"+chineseid).html( changeMoneyToChinese(obj.value) );
}

//格式化浮点数形式(只能输入正浮点数，且小数点后只能跟四位,总体数值不能大于999999999999999共15位:数值999兆)
function formatFloat( num )
{
	num = num.replace(/^[^\d]/g,'');
	num = num.replace(/[^\d.]/g,'');
	num = num.replace(/\.{2,}/g,'.');
	num = num.replace(".","$#$").replace(/\./g,"").replace("$#$",".");
	if( num.indexOf(".") != -1 )
	{
		var data = num.split('.');
		num = (data[0].substr(0,15))+'.'+(data[1].substr(0,4));
	}
	else
	{
		num = num.substr(0,15);
	}
	return num;
}

function moneyFormat(num)
{
	var sign = Number(num) < 0 ? "-" : "";
    num = num.toString();
	if( num.indexOf(".") == -1 )
	{
		num = "" + num + ".0000";
	}
	var data = num.split('.');
	data[0] = data[0].toString().replace(/[^\d]/g,"").substr(0,15);
	data[0] = Number(data[0]).toString();
	var newnum = [];
	for( var i=data[0].length; i>0; i-=3 )
	{
		newnum.unshift(data[0].substring(i,i-3));
	}
	data[0] = newnum.join(",");
	data[1] = data[1].toString().substr(0,4);
	return sign+""+data[0] + "." + data[1];
}
//四舍五入到指定精度,支持到整数位,类似C#的round函数
function JsRound( num, len, keep )
{
    len = parseInt(len,10);
    if( len < 0 )
	{
	    len = Math.abs(len);
	    return Math.round(Number(num)/Math.pow(10,len))*Math.pow(10,len);
	}
	else if( len == 0 )
	{
	    return Math.round(Number(num));
    }
    num = Math.round(Number(num)*Math.pow(10,len))/Math.pow(10,len);
    if( keep && keep == true )
    {
        var t = '',i=0;
        num = num.toString();
        if( num.indexOf(".") == -1 )
    	{
    	    num = "" + num + ".0";
    	}
    	data = num.split('.');
    	for( i=data[1].length; i<len; i++ )
    	{
    	    t += ''+'0';
    	}
    	return ''+num+''+t;
    }
    return num;
	
	
}

//自动转换数字金额为大小写中文字符,返回大小写中文字符串，最大处理到999兆
function changeMoneyToChinese( money )
{
	var cnNums	= new Array("零","壹","贰","叁","肆","伍","陆","柒","捌","玖");	//汉字的数字
	var cnIntRadice = new Array("","拾","佰","仟");	//基本单位
	var cnIntUnits = new Array("","万","亿","兆");	//对应整数部分扩展单位
	var cnDecUnits = new Array("角","分","厘","毫");	//对应小数部分单位
	var cnInteger = "整";	//整数金额时后面跟的字符
	var cnIntLast = "元";	//整型完以后的单位
	var maxNum = 999999999999999.9999;	//最大处理的数字
	
	var IntegerNum;		//金额整数部分
	var DecimalNum;		//金额小数部分
	var ChineseStr="";	//输出的中文金额字符串
	var parts;		//分离金额后用的数组，预定义
	var i,m;
	
	if( money == "" )
	{
		return "";	
	}
	
	money = parseFloat(money);
	//alert(money);
	if( money >= maxNum )
	{
		alert('超出最大处理数字');
		return "";
	}
	if( money == 0 )
	{
		ChineseStr = cnNums[0]+cnIntLast+cnInteger;
		//document.getElementById("show").value=ChineseStr;
		return ChineseStr;
	}
	money = money.toString(); //转换为字符串
	if( money.indexOf(".") == -1 )
	{
		IntegerNum = money;
		DecimalNum = '';
	}
	else
	{
		parts = money.split(".");
		IntegerNum = parts[0];
		DecimalNum = parts[1].substr(0,4);
	}
	if( parseInt(IntegerNum,10) > 0 )
	{
	    //获取整型部分转换
		zeroCount = 0;
		IntLen = IntegerNum.length;
		for( i=0;i<IntLen;i++ )
		{
			n = IntegerNum.substr(i,1);
			p = IntLen - i - 1;
			q = p / 4;
            m = p % 4;
			if( n == "0" )
			{
				zeroCount++;
			}
			else
			{
				if( zeroCount > 0 )
				{
					ChineseStr += cnNums[0];
				}
				zeroCount = 0;	//归零
				ChineseStr += cnNums[parseInt(n)]+cnIntRadice[m];
			}
			if( m==0 && zeroCount<4 )
			{
				ChineseStr += cnIntUnits[q];
			}
		}
		ChineseStr += cnIntLast;
	//整型部分处理完毕
	}
	if( DecimalNum!= '' )
	{
	    //小数部分
		decLen = DecimalNum.length;
		for( i=0; i<decLen; i++ )
		{
			n = DecimalNum.substr(i,1);
			if( n != '0' )
			{
				ChineseStr += cnNums[Number(n)]+cnDecUnits[i];
			}
		}
	}
	if( ChineseStr == '' )
	{
		ChineseStr += cnNums[0]+cnIntLast+cnInteger;
	}
	else if( DecimalNum == '' )
	{
		ChineseStr += cnInteger;
	}
	return ChineseStr;
	
}

//转换HTML标签为标准代码(类似PHP的htmlspecialchars函数)
function replaceHTML( str )
{
	str = str.replace(/[&]/g,'&amp;');
	str = str.replace(/[\"]/g,'&quot;');
	str = str.replace(/[\']/g,'&#039;');
	str = str.replace(/[<]/g,'&lt;');
	str = str.replace(/[>]/g,'&gt;');
	str = str.replace(/[ ]/g,'&nbsp;');
	return str;
}

//转换HTML标准代码为显示代码（类似PHP的htmlspecialchars_decode函数）
function replaceHTML_DECODE( str )
{
	str = str.replace(/&amp;/g,'&');
	str = str.replace(/&quot;/g,'"');
	str = str.replace(/&#039;/g,'\'');
	str = str.replace(/&lt;/g,'<');
	str = str.replace(/&gt;/g,'>');
	str = str.replace(/&nbsp;/g,' ');
	return str;
}
//设置cookie
function SetCookie(name, value, expire) {
    var exp  = new Date();   
    exp.setTime(exp.getTime() + expire);   
    document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();   
}
//获取cookie
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));   
    if(arr != null) return unescape(arr[2]); return null;   
}
//删除cookie
function delCookie(name){
    var exp = new Date();   
    exp.setTime(exp.getTime() - 1);   
    var cval=getCookie(name);   
    if(cval!=null) document.cookie= name + "="+cval+";expires="+exp.toGMTString();   
}

//复制内容到剪贴板
function copyToClipboard(obj)
{
	txt = jQuery("#"+obj).html();
	if(window.clipboardData)
	{
		window.clipboardData.clearData();
		window.clipboardData.setData("Text", txt);
	}
	else if(navigator.userAgent.indexOf("Opera") != -1)
	{
		window.location = txt;
	}
	else if (window.netscape) 
	{
		try {
			netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
		} 
		catch (e) 
		{
			alert("您的firefox安全限制限制您进行剪贴板操作，请在地址栏中输入“about:config”将“signed.applets.codebase_principal_support”设置为“true”之后重试");
			return false;
		}
		var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
		if (!clip)
		{
		    return;
		}
		var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
		if (!trans)
		{
		    return;
		}
		trans.addDataFlavor('text/unicode');
		var str = new Object();
		var len = new Object();
		var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
		var copytext = txt;
		str.data = copytext;
		trans.setTransferData("text/unicode",str,copytext.length*2);
		var clipid = Components.interfaces.nsIClipboard;
		if (!clip)
		{
		    return false;
		}
		clip.setData(trans,null,clipid.kGlobalClipboard);
	}
}
//同上，只是做整数限制
function checkIntWithdraw( obj,chineseid,maxnum ){
	obj.value = parseInt(obj.value,10);
	obj.value = isNaN(obj.value) ? 0 : obj.value;
	if( parseFloat(obj.value) > parseFloat(maxnum) ){
		//$.alert("输入金额超出了可用余额");	
		//alert("输入金额超出了可用余额");	
		obj.value = parseInt(maxnum,10);
	}
	jQuery("#"+chineseid).html( changeMoneyToChinese(obj.value) );
}

function checkemailWithdraw( obj,chineseid,maxnum ){
	obj.value = formatFloat(obj.value);
	if( parseFloat(obj.value) > parseFloat(maxnum) ){
		alert("充值金额不能高于最高充值限额");
		obj.value = maxnum;
	}
	//alert(obj.value.substr(0,1));
	if(obj.value.substr(0,1)=='0'){obj.value=obj.value.substr(1);}
	jQuery("#"+chineseid).html( changeMoneyToChinese(obj.value) );
}
//*分页*/

function inintpager(pageIndex, totalCount, cannback) {
    //生成分页
    var pageCount = parseInt((totalCount % 20 == 0 ? totalCount / 20 : totalCount / 20 + 1));
    $("#kkpager").empty();
    //有些参数是可选的，比如lang，若不传有默认值
    kkpager.generPageHtml({
        pno: pageIndex,
        //总页码
        total: pageCount,
        ////总数据条数
        totalRecords: totalCount,
        mode: 'click',//默认值是link，可选link或者click
        click: function (n) {
            if (undefined != cannback)
                cannback(n);
            this.selectPage(n);
            return false;
        }
		,lang				: {
			firstPageText			: '首页',
			firstPageTipText		: '首页',
			lastPageText			: '尾页',
			lastPageTipText			: '尾页',
			prePageText				: '上一页',
			prePageTipText			: '上一页',
			nextPageText			: '下一页',
			nextPageTipText			: '下一页',
			totalPageBeforeText		: '共',
			totalPageAfterText		: '页',
			currPageBeforeText		: '当前第',
			currPageAfterText		: '页',
			totalInfoSplitStr		: '/',
			totalRecordsBeforeText	: '共',
			totalRecordsAfterText	: '条数据',
			gopageBeforeText		: ' 转到',
			gopageButtonOkText		: 'GO',
			gopageAfterText			: '页',
			buttonTipBeforeText		: '第',
			buttonTipAfterText		: '页'
		}

    });
}
/**奖金转换*/
function decimalCt(value)
{
    //0.7600
    var x = value.toString();
    var array = x.split('.');
    if (array.length != 2)
        return value;
    var newv = "";
    var isOk = false;
    var eachValue = array[1];
    var newv = "";
    
    for(var i=eachValue.length-1; i>=0; i--) {
        var s = eachValue[i];
        //console.info(s + "  ----");
        if (s != '0' || isOk)
        {
            isOk = true;
            
            newv=s+newv ;
        }
    }
    
    return newv == "" ? array[0]+".00" : (array[0] + "." + newv);
}
/**获取url参数*/
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}

function lotteryShowLoading() {
    //显示loading 
    var width = $(document).width()-5;
    var height = $(document).height()-5;
    var loadHtml = "<div style=' margin:auto;margin-top:" + height / 2 + "px; width:110px;height:auto; text-align:center;background:#fff;  border: #0172C5 1px solid;' >" +
        "<div style='margin:5px;'>" +
        "<span style='background:url(/Content/images/loading.gif) no-repeat;display:block;width:16px;height:16px;float:left;'/>" +
        "<span style='margin-left:0px;'>正在处理......</span></div></div>";
    $(this).append("<div id='m_show_loading' style=';width:" + width + "px;height:" + height + "px;opacity: 0.6;filter: alpha(opacity = 60);background-color: #ccc; position:absolute;left:0;top:0;'>" + loadHtml + "<div/>");
}