;(function($){//start
    //选号区动态插入函数，可能是手动编辑
    $.fn.lt_selectarea = function (opts) {
        //切换玩法时，倍数为1
        $("#lt_sel_times").val(1);
        //
        var ps = {//默认参数
                type   : 'digital', //选号，'input':输入型,'digital':数字选号型,'dxds':大小单双类型
                layout : [
                           {title:'百位', no:'0|1|2|3|4|5|6|7|8|9', place:0, cols:1},
                           {title:'十位', no:'0|1|2|3|4|5|6|7|8|9', place:1, cols:1},
                           {title:'个位', no:'0|1|2|3|4|5|6|7|8|9', place:2, cols:1}
                          ],//数字型的号码排列
                noBigIndex : 5,    //前面多少个号码是小号,即大号是从多少个以后开始的
                isButton   : true,  //是否需要全大小奇偶清按钮
                imagedir   : './js/lottery/image/' //按钮图片文件夹位置
            };
        opts = $.extend( {}, ps, opts || {} ); //根据参数初始化默认配置
        var data_sel = [];//用户已选择或者已输入的数据
		var random_bets = false;//是否机选
		var minchosen = [];
        var max_place= 0; //总共的选择型排列数
        var otype = opts.type.toLowerCase();    //类型全部转换为小写
        var methodname = $.lt_method[$.lt_method_data.methodid];//玩法的简写,如:'ZX3'
        switch(methodname)
        {
            case 'BJRX1':
            case 'BJRX2':
            case 'BJRX3':
            case 'BJRX4':
            case 'BJRX5':
            case 'BJRX6':
            case 'BJRX7':    
                var html  = '<div class="grayContent_k8" id="right_05">';
                break;
            default:
                var html  = '<div class="grayContent" id="right_05">';
                break;
        }
        $("#right_03").css("display","block");
        $("#right_04").css("display","block");
//代码开始
		if(1){
			if( otype == 'input' ){//输入型，则载入输入型的数据
				//var html  = '<div class="grayContent" id="right_05_input">';
				var html  = '<div>';
				var tempdes    = '';
				switch( methodname ){
					case 'SDZX3' :
					case 'SDZU3' :
					case 'SDZX2' :
					case 'SDRX1' :
					case 'SDRX2' :
					case 'SDRX3' :
					case 'SDRX4' :
					case 'SDRX5' :
					case 'SDRX6' :
					case 'SDRX7' :
					case 'SDRX8' :
					case 'SDZU2' : tempdes = lot_lang.dec_s26; break;
					default      : tempdes = lot_lang.dec_s4; break;
				}
				$("#right_03").css("display","none");
				$("#right_04").css("display","none");
				html += '<div class="grayTop"></div><div class="grayContent clearfix">';
				html += '<textarea id="lt_write_box" class="textareaLong floatL"></textarea>';
				html += '<div class="floatL">';
				html += '<p class="marginb5px"><input id="lt_write_del" type="button" value="删除重复号" class="formWord" /></p><p class="marginb5px"><input id="lt_write_import" type="button" value="导入文件" class="formWord" /></p><p class="marginb5px"><input id="lt_write_empty" type="button" value="清空" class="formWord" /></p>';
				html += '</div>';
				html += '</div>';
				html += '<div class="yellow">'+tempdes+'</div><div class="grayBottom"></div>';
				data_sel[0] = []; //初始数据
				tempdes = null;
			}else if( otype == 'digital' ){//数字选号型
				$.each(opts.layout, function(i,n){
					if(typeof(n)=='object'){
						n.place  = parseInt(n.place,10);
						max_place = n.place > max_place ? n.place : max_place;
						data_sel[n.place] = [];//初始数据
						minchosen[n.place] = (typeof(n.minchosen) == 'undefined') ? 0 : n.minchosen;
                                               
							switch(methodname){
								case 'KSHZ'://2317和值-和值
									html += '<div class="tagContent"><div class="k3_list1 each clearbxd"><ul class="k3_list1_l">';
									numbers = n.no.split("|");
									for( i=0; i<numbers.length; i++ ){
										html += '<li name="lt_place_'+n.place+'" class="button1">'+numbers[i]+'</li>';
									}
									html +='</ul><ul class="btnList"><li class="selectType" name="clean"><font class="clean">清</font></li></ul></div></div>';
								break;
								case 'EBTH'://2318二不同号-标准选号
									html += '<div class="tagContent">';
										html += '<div class="x_t clearbxd">';
										html += '<h5 class="name">' + n.title + '</h5>';
										html += '<ul class="k3_list5">';
										numbers = n.no.split("|");
										for( i=0; i<numbers.length; i++ ){
											html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i]+'</em><span class="griddle'+numbers[i]+'">'+numbers[i]+'</span></li>';
										}
										html += '</ul>';
										html += '</div>';
									html += '</div>';
								break;
								case 'EBTHDT'://2319二不同号-胆拖选号
									if(i==0){
										html += '<div class="tagContent">';
									}
										html += '<div class="x_t clearbxd">';
										html += '<h5 class="name">' + n.title + '</h5>';
										html += '<ul class="k3_list5">';
										numbers = n.no.split("|");
										for( i=0; i<numbers.length; i++ ){
											html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i]+'</em><span class="griddle'+numbers[i]+'">'+numbers[i]+'</span></li>';
										}
										html += '</ul>';
										html += '</div>';
									if(i==opts.layout.length){
										html += '</div>';
									}
								break;
								case 'ETHDX'://2302二同号单选-标准选号
									if(i==0){
										html += '<div class="tagContent">';
										html += '<div class="x_t clearbxd">'
										html += '<h5 class="name m_t_h5">' + n.title + '</h5>';
												html += '<ul class="k3_list5 clearbxd">'
													numbers = n.no.split("|");
													for( i=0; i<numbers.length; i++ ){
													html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i]+'</em><a href="javascript:;" class="curr"><div class="t_img"><p class="tr2"><span class="griddle'+numbers[i].split("")[0]+'"></span><span class="griddle'+numbers[i].split("")[0]+'"></span></p></div><p class="b_text">'+numbers[i]+'</p></a></li>'
													}
												html += '</ul>'
										html += '</div>';
									}else{
										html += '<div class="x_b clearbxd">'
												html += '<h5 class="name">' + n.title + '</h5>';
														html += '<ul class="k3_list5 clearbxd">'
															numbers = n.no.split("|");
															for( i=0; i<numbers.length; i++ ){
															html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i]+'</em><span class="griddle'+numbers[i]+'"></span></li>'
															}
														html += '</ul>'
												html += '</div>';
										html += '</div>';
									}

								break;
								case 'ETHFX'://2303二同号复选-二同号复选
									html += '<div class="tagContent">';
										html += '<ul class="k3_list4 clearbxd">';
										numbers = n.no.split("|");
										for( i=0; i<numbers.length; i++ ){
											html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i].split("")[0]+'</em><a href="javascript:;"><div class="t_img"><p class="tr1"><span class="griddle"></span></p><p class="tr2"><span class="griddle'+numbers[i].split("")[0]+'"></span><span class="griddle'+numbers[i].split("")[1]+'"></span></p></div><p class="b_text">'+numbers[i]+'</p></a></li>';
										}
										html += '</ul>';
									html += '</div>';
								break;
								case 'SBTHTX'://2308三连号通选
									html += '<div class="tagContent">';
										if(n.no == "通选")
										html += '<ul class="k3_list4 clearbxd"><li name="lt_place_'+n.place+'" class="button1"><em>'+n.no+'</em>';
										for( i=1; i<=4; i++ ){
											html += '<a href="javascript:;" style="display:inline-block; margin-right:10px;"><div class="t_img"><p class="tr1"><span class="griddle'+i+'"></span></p><p class="tr2"><span class="griddle'+(i+1)+'"></span><span class="griddle'+(i+2)+'"></span></p></div><p class="b_text">'+i+(i+1)+(i+2)+'</p></a>';
										}
										html += '</li></ul>';
									html += '</div>';
								break;
								case 'SBTH'://2304三不同号-标准选号
									html += '<div class="tagContent">';
										html += '<div class="x_t clearbxd">';
										html += '<h5 class="name">' + n.title + '</h5>';
										html += '<ul class="k3_list5 clearbxd">';
										numbers = n.no.split("|");
										for( i=0; i<numbers.length; i++ ){
											html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i]+'</em><span class="griddle'+numbers[i]+'">'+numbers[i]+'</span></li>';
										}
										html += '</ul>';
										html += '</div>';
									html += '</div>';
								break;
								case 'SBTHDT'://2306三不同号-胆拖选号
									if(i==0){
										html += '<div class="tagContent">';
									}
										html += '<div class="x_t clearbxd">';
										html += '<h5 class="name">' + n.title + '</h5>';
										html += '<ul class="k3_list5 clearbxd">';
										numbers = n.no.split("|");
										for( i=0; i<numbers.length; i++ ){
											html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i]+'</em><span class="griddle'+numbers[i]+'">'+numbers[i]+'</span></li>';
										}
										html += '</ul>';
										html += '</div>';
									if(i==opts.layout.length){
										html += '</div>';
									}
								break;
								case 'SBTHHZ'://2307三不同号-和值选号
									html += '<div class="tagContent"><div class="k3_list1 each clearbxd"><ul class="k3_list1_l">';
									numbers = n.no.split("|");
									for( i=0; i<numbers.length; i++ ){
										html += '<li name="lt_place_'+n.place+'" class="button1">'+numbers[i]+'</li>';
									}
									html +='</ul><ul class="btnList"><li class="selectType" name="clean"><font class="clean">清</font></li></ul></div></div>';
								break;
								case 'STHDX'://2315三同号单选-三同号单选
									html += '<div class="tagContent">';
										html += '<ul class="k3_list4 clearbxd">';
										numbers = n.no.split("|");
										for( i=0; i<numbers.length; i++ ){
											html += '<li name="lt_place_'+n.place+'" class="button1"><em>'+numbers[i].split("")[0]+'</em><a href="javascript:;"><div class="t_img"><p class="tr1"><span class="griddle'+numbers[i].split("")[0]+'"></span></p><p class="tr2"><span class="griddle'+numbers[i].split("")[0]+'"></span><span class="griddle'+numbers[i].split("")[0]+'"></span></p></div><p class="b_text">'+numbers[i]+'</p></a></li>';
										}
										html += '</ul>';
									html += '</div>';
								break;
								case 'STHTX'://2316三同号通选-三同号通选
									html += '<div class="tagContent">';
										if(n.no == "通选"){
											html += '<ul class="k3_list4 clearbxd"><li name="lt_place_'+n.place+'" class="button1"><em>'+n.no+'</em>';
											for( i=1; i<=6; i++ ){
												html += '<a href="javascript:;" style="display:inline-block; margin-right:10px;"><div class="t_img"><p class="tr1"><span class="griddle'+i+'"></span></p><p class="tr2"><span class="griddle'+i+'"></span><span class="griddle'+i+'"></span></p></div><p class="b_text">'+i+i+i+'</p></a>';
											}
											html += '</li></ul>';
										}
									html += '</div>';
								break;
								default:
									//
								break;
							}                                                
                                                
					}
				});
			}else if( otype == 'dxds' ){//大小单双类型(北京快乐吧)
				$.each(opts.layout, function(i,n){
					if(n){
						n.place  = parseInt(n.place,10);
						max_place = n.place > max_place ? n.place : max_place;
						data_sel[n.place] = [];//初始数据
						html += '<div class="each clearfix">';
						if( n.cols > 0 ){//有标题
							//html += '<td align="left" width="100px;">';
								
							if( n.title.length > 0 ){
							
								
								html += '<h3 class="name">'+n.title+'</h3>';
							}
							//html += '</td>';
						}
						if(lotterytype==0||lotterytype==2){
						    html += '<ul class="numList floatL floatL_lottery">';
						}else if(lotterytype==3){
							if(n.no.indexOf('codedesc')==-1){
								html += '<ul class="numList floatL bigBt">';
							}else{
								html += '<ul class="numList floatL bigBt twoRow">';
							}
						}
						numbers = n.no.split("|");
						for( i=0; i<numbers.length; i++ ){
							html += '<li name="lt_place_'+n.place+'" class="button1" style="margin:2px 15px 2px 15px">'+numbers[i]+'</li>';
						}
						html += '</ul><ul class="btnList">';
						html += '<li name="all" class="selectType" style="margin:4px 12px 4px 10px">'+lot_lang.bt_sel_all+'</li><li class="selectType" name="clean" style="margin:4px 12px 4px 10px">'+lot_lang.bt_sel_clean+'</li></ul></div>';
					}
				});
			}else if( otype == 'dds' ){//<=-----[趣味型]
				$.each(opts.layout, function(i,n){
					n.place  = parseInt(n.place,10);
					max_place = n.place > max_place ? n.place : max_place;
					data_sel[n.place] = [];//初始数据
					html += '<div class="each clearfix">';
					if( n.cols > 0 ){//有标题
						//html += '<td rowspan="'+n.cols+'" class="'+(n.title.length<3 ? 'two' : (n.title.length>3 ? 'four' : 'three') )+'">';
						if( n.title.length > 0 ){
							alert(n.title);
							html += '<div class="seltitle"><div>'+n.title+'</div></div>';
						}
						//html += '</td>';
					}
					//html += '<td>';
					//==========趣味型按钮输出===============
					html += '<div class="selddsbox"><ul class="numList floatL bigBt">';
					numbers = n.no.split("|");
					temphtml= '';
					if( n.prize ){
						tmpprize = n.prize.split(",");
					}
					for( i=0; i<numbers.length; i++ ){
						html += '<li name="lt_place_'+n.place+'">'+numbers[i]+'</li>';
						if( n.prize ){
							alert("span_astest["+$.lt_method_data.prize[parseInt(tmpprize[i],10)]);
							temphtml += '<span>'+$.lt_method_data.prize[parseInt(tmpprize[i],10)]+'</span>';
						}
					}
					html += temphtml+'</ul></div>';//去掉了【<td></tr>】
				});
			}
			html += '</div>';//这个在最后都会加这个div结尾标签，所以开头一定要有一个<div>
		}
//代码结束
		//alert(html);
        $html = $(html)
        $(this).empty();
        $html.appendTo(this);
        var me = this;
        if($.lt_lottid == 22)
        {
				var _SortNum = function(a,b){//数字大小排序
                if( otype != 'input' ){
                    a = a.replace(/5单0双/g,0).replace(/4单1双/g,1).replace(/3单2双/g,2).replace(/2单3双/g,3).replace(/1单4双/g,4).replace(/0单5双/g,5);
                    if (methodname != "KSHZ" && methodname != "SBTHHZ") {
                        a = a.replace(/11/gi, 1).replace(/22/gi, 2).replace(/33/gi, 3).replace(/44/gi, 4).replace(/55/gi, 5).replace(/66/gi, 6)
                    }                
                    a = a.replace(/大/g,0).replace(/小/g,1).replace(/单/g,2).replace(/双/g,3).replace(/\s/g,"");
                    b = b.replace(/5单0双/g,0).replace(/4单1双/g,1).replace(/3单2双/g,2).replace(/2单3双/g,3).replace(/1单4双/g,4).replace(/0单5双/g,5);
                    b = b.replace(/111/gi, 1).replace(/222/gi, 2).replace(/333/gi, 3).replace(/444/gi, 4).replace(/555/gi, 5).replace(/666/gi, 6);
                    if (methodname != "KSHZ" && methodname != "SBTHHZ") {
                        b = b.replace(/11/gi, 1).replace(/22/gi, 2).replace(/33/gi, 3).replace(/44/gi, 4).replace(/55/gi, 5).replace(/66/gi, 6)
                    }                
                    b = b.replace(/大/g,0).replace(/小/g,1).replace(/单/g,2).replace(/双/g,3).replace(/\s/g,"");
                }
                a = parseInt(a,10);
                b = parseInt(b,10);
                if( isNaN(a) || isNaN(b) ){
                    return true;
                }
                return (a-b);
            };
        } else {
			var _SortNum = function(a,b){//数字大小排序
			if( otype != 'input' ){
					a = a.replace(/5单0双/g,0).replace(/4单1双/g,1).replace(/3单2双/g,2).replace(/2单3双/g,3).replace(/1单4双/g,4).replace(/0单5双/g,5);
					a = a.replace(/大/g,0).replace(/小/g,1).replace(/单/g,2).replace(/双/g,3).replace(/\s/g,"");
					b = b.replace(/5单0双/g,0).replace(/4单1双/g,1).replace(/3单2双/g,2).replace(/2单3双/g,3).replace(/1单4双/g,4).replace(/0单5双/g,5);
					b = b.replace(/大/g,0).replace(/小/g,1).replace(/单/g,2).replace(/双/g,3).replace(/\s/g,"");
				}
				a = parseInt(a,10);
				b = parseInt(b,10);
				if( isNaN(a) || isNaN(b) ){
					return true;
				}
				return (a-b);
			};
		}
        /************************ 验证号码合法性以及计算单笔投注注数以及金额 ***********************/
        //===================输入型检测
        var _HHZXcheck = function(n,len){//混合组选合法号码检测，合法返回TRUE，非法返回FALSE,n号码，len号码长度
            if( len == 2 ){//两位
                var a = ['00','11','22','33','44','55','66','77','88','99'];
            }else{//三位[默认]
                var a = ['000','111','222','333','444','555','666','777','888','999'];
            }
            n     = n.toString();
            if( $.inArray(n,a) == -1 ){//不在非法列表中
                return true;
            }
            return false;
        };
        var _SDinputCheck = function(n,len){//山东十一运的手动输入型的检测[不能重复，只能是01-11的数字]
            t = n.split(" ");
            l = t.length;
            for( i=0; i<l; i++ ){
                if( Number(t[i]) > 11 || Number(t[i]) < 1 ){//超过指定范围
                    return false;
                }
                for( j=i+1; j<l; j++ ){
                    if( Number(t[i]) == Number(t[j]) ){//重复的号码
                        return false;
                    }
                }
            }
            return true;
        };
		
		 var _EBTHDScheck = function(n, len) {
            if (len != 2) {
                return false
            }
            var first = "";
            var second = "";
            var i = 0;
            for (i = 0; i < len; i++) {
                if (i == 0) {
                    first = n.substr(i, 1)
                }
                if (i == 1) {
                    second = n.substr(i, 1)
                }
            }
            if (first == second) {
                return false
            }
            return true
        };
		
		 var _SBTHDScheck = function(n, len) {
            if (len != 3) {
                return false
            }
            var first = "";
            var second = "";
            var third = "";
            var i = 0;
            for (i = 0; i < len; i++) {
                if (i == 0) {
                    first = n.substr(i, 1)
                }
                if (i == 1) {
                    second = n.substr(i, 1)
                }
                if (i == 2) {
                    third = n.substr(i, 1)
                }
            }
            if (first == second || second == third || third == first) {
                return false
            } else {
                return true
            }
            return false
        };        
        
        
        var _ETHDXcheck = function(n, len) {
            if (len != 3) {
                return false
            }
            var first = "";
            var second = "";
            var third = "";
            var i = 0;
            for (i = 0; i < len; i++) {
                if (i == 0) {
                    first = n.substr(i, 1)
                }
                if (i == 1) {
                    second = n.substr(i, 1)
                }
                if (i == 2) {
                    third = n.substr(i, 1)
                }
            }

            if (first == second && second == third) {
                return false
            }
            if (first == second || second == third || third == first) {
                return true
            }
            return false
        };
		
        //号码检测,l:号码长度,e是否返回非法号码，true是,false返回合法注数,fun对每个号码的附加检测,sort:是否对每个号码排序
        var _inputCheck_Num = function(l,e,fun,sort){
            var nums = data_sel[0].length;
            var error= [];
            var newsel=[];
            var partn= "";
            l = parseInt(l,10);
			if($.lt_lottid == 22){
                switch(l){
                    case 2 : partn= /^[1-6]{2}$/;break;
                    case 3 : partn = /^[1-6]{3}$/;break;
                    default: partn= /^[1-6]{3}$/;break;
                } 
            }else{
				switch(l){
					case 2 : partn= /^[0-9]{2}$/;break;
					case 4 : partn= /^[0-9\s]{4}$/;break;
					case 5 : partn= /^[0-9\s]{5}$/;break;
					case 8 : partn= /^[0-9\s]{8}$/;break;
					case 11 : partn= /^[0-9\s]{11}$/;break;
					case 14 : partn= /^[0-9\s]{14}$/;break;
					case 17 : partn= /^[0-9\s]{17}$/;break;
					case 20 : partn= /^[0-9\s]{20}$/;break;
					case 23 : partn= /^[0-9\s]{23}$/;break;
					default: partn= /^[0-9]{3}$/;break;
				}
            }
            fun = $.isFunction(fun) ? fun : function(s){return true;};
            $.each(data_sel[0],function(i,n){
                n = $.trim(n);
                if( partn.test(n) && fun(n,l) ){//合格号码
                    if( sort ){
                        if( n.indexOf(" ") == -1 ){
                            n = n.split("");
                            n.sort(_SortNum);
                            n = n.join("");
                        }else{
                            n = n.split(" ");
                            n.sort(_SortNum);
                            n = n.join(" ");
                        }
                    }
                    data_sel[0][i] = n;
                    newsel.push(n);
                }else{//不合格则注数减
                    if( n.length>0 ){
                        error.push(n);
                    }
                    nums = nums - 1;
                }
            });
            if( e == true ){
                data_sel[0] = newsel;
                return error;
            }
            return nums;
        };
        function checkNum(){//实时计算投注注数与金额等
            var nums  = 0, mname = $.lt_method[$.lt_method_data.methodid];//玩法的简写,如:'ZX3'
            //var modes = parseInt($("#lt_project_modes").val(),10);//投注模式
			var modes = parseInt($("input[name='lt_project_modes']:checked").val(),10);//投注模式
			//alert(modes+'----'+mname);
            //01:验证号码合法性并计算注数
            if( otype == 'input' ){//输入框形式的检测
                if( data_sel[0].length > 0 ){//如果输入的有值
                    switch(mname){
                        case 'ZX3'  : nums = _inputCheck_Num(3,false); break;
                        case 'ZX4'  : nums = _inputCheck_Num(4,false); break;
                        case 'ZX5'  : nums = _inputCheck_Num(5,false); break;
                        case 'HHZX' : nums = _inputCheck_Num(3,false,_HHZXcheck,true); break;
                        case 'ZX2'  : nums = _inputCheck_Num(2,false); break;
                        case 'ZU2'  : nums = _inputCheck_Num(2,false,_HHZXcheck,true); break;
                        case 'SDZX3': nums = _inputCheck_Num(8,false,_SDinputCheck,false); break;
                        case 'SDZU3': nums = _inputCheck_Num(8,false,_SDinputCheck,true); break;
                        case 'SDZX2': nums = _inputCheck_Num(5,false,_SDinputCheck,false); break;
                        case 'SDZU2': nums = _inputCheck_Num(5,false,_SDinputCheck,true); break;
                        case 'SDRX1': nums = _inputCheck_Num(2,false,_SDinputCheck,false); break;
                        case 'SDRX2': nums = _inputCheck_Num(5,false,_SDinputCheck,true); break;
                        case 'SDRX3': nums = _inputCheck_Num(8,false,_SDinputCheck,true); break;
                        case 'SDRX4': nums = _inputCheck_Num(11,false,_SDinputCheck,true); break;
                        case 'SDRX5': nums = _inputCheck_Num(14,false,_SDinputCheck,true); break;
                        case 'SDRX6': nums = _inputCheck_Num(17,false,_SDinputCheck,true); break;
                        case 'SDRX7': nums = _inputCheck_Num(20,false,_SDinputCheck,true); break;
                        case 'SDRX8': nums = _inputCheck_Num(23,false,_SDinputCheck,true); break;
                        case 'ETHDX': nums = _inputCheck_Num(3,false,_ETHDXcheck,true); break;
                        case 'SBTH' : nums = _inputCheck_Num(3, false, _SBTHDScheck, true);break;
                        case 'EBTH' : nums = _inputCheck_Num(2, false, _EBTHDScheck, true);break;  
                        default   : break;
                    }
                }
            }else{//其他选择号码形式[暂时就数字型和大小单双]
                var tmp_nums = 1;
                switch(mname){//根据玩法分类不同做不同处理
					case"SBTH":
                        if (data_sel[0].length >= 3) {
                            nums += Combination(data_sel[0].length, 3)
                        }
                        break;  
                    case"SBTHDT":
                        var danlen = data_sel[0].length;
                        var tuolen = data_sel[1].length;
                        if (danlen + tuolen < 4) {
						//if (danlen < 1 || tuolen < 1 || danlen >= 3) {
                            nums = 0
                        } else {
                            nums = Combination(tuolen, 3 - danlen);
                        }
                        break;
                    case"SBTHHZ":
                        var cc = {6: 1, 7: 1, 8: 2, 9: 3, 10: 3, 11: 3, 12: 3, 13: 2, 14: 1, 15: 1};
                        var s = data_sel[0].length;
                        if (s > 0) {
                            for (j = 0; j < s; j++) {
                                nums += cc[parseInt(data_sel[0][j], 10)];
                            }
                        }
                        break;  
                    case"EBTH":
                        if (data_sel[0].length >= 2) {
                            nums += Combination(data_sel[0].length, 2);
                        }
                        break;
                    case"WXZU120":
                                var s=data_sel[0].length;if(s>4){
                                    nums+=Combination(s,5)
                                }break;
                    case"WXZU60":
                    case"WXZU30":
                    case"WXZU20":
                    case"WXZU10":
                    case"WXZU5":
                                if(data_sel[0].length>=minchosen[0]&&data_sel[1].length>=minchosen[1]){
                                    var h=Array.intersect(data_sel[0],data_sel[1]).length;
                                    tmp_nums=Combination(data_sel[0].length,minchosen[0])*Combination(data_sel[1].length,minchosen[1]);
                                    if(h>0){
                                        if(mname=="WXZU60"){
                                            tmp_nums-=Combination(h,1)*Combination(data_sel[1].length-1,2)
                                        }else{
                                            if(mname=="WXZU30"){
                                                tmp_nums-=Combination(h,2)*Combination(2,1);
                                                if(data_sel[0].length-h>0){
                                                    tmp_nums-=Combination(h,1)*Combination(data_sel[0].length-h,1)
                                                }}else{
                                                if(mname=="WXZU20"){
                                                    tmp_nums-=Combination(h,1)*Combination(data_sel[1].length-1,1)
                                                }else{
                                                    if(mname=="WXZU10"||mname=="WXZU5"){
                                                        tmp_nums-=Combination(h,1)
                                                    }
                                                }
                                            }}}
                                        nums+=tmp_nums
                                    }
                                    break;
                    case 'ZXHZ' :   //直选和值特殊算法
                                    var cc = {0:1,1:3,2:6,3:10,4:15,5:21,6:28,7:36,8:45,9:55,10:63,11:69,12:73,13:75,14:75,15:73,16:69,17:63,18:55,19:45,20:36,21:28,22:21,23:15,24:10,25:6,26:3,27:1};
                    case 'ZUHZ' :   //混合组选特殊算法
                                    if( mname == 'ZUHZ' ){
                                        cc = {1:1,2:2,3:2,4:4,5:5,6:6,7:8,8:10,9:11,10:13,11:14,12:14,13:15,14:15,15:14,16:14,17:13,18:11,19:10,20:8,21:6,22:5,23:4,24:2,25:2,26:1};
                                    }
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        for( j=0; j<s; j++ ){
                                            nums += cc[parseInt(data_sel[i][j],10)];
                                        }
                                    };break;
                    case 'ZUS'  :   //组三
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 1 ){//组三必须选两位或者以上
                                            nums += s*(s-1);
                                        }
                                    };break;
                    case 'ZUL'  :   //组六
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 2 ){//组六必须选三位或者以上
                                            nums += s*(s-1)*(s-2)/6;
                                        }
                                    };break;
                                        case 'ZH5':
					case 'ZH4'  :   //组合4
                                        case 'ZH3'  :
									for( i=0; i<=max_place; i++ )
									{
										if( data_sel[i].length == 0 )
										{//有位置上没有选择
											tmp_nums = 0;
											break;
										}
										tmp_nums *= data_sel[i].length;
									}
									nums = tmp_nums*parseInt(mname.charAt(mname.length-1));
									break;
					case "SXZU24":
									var s=data_sel[0].length;if(s>3){nums+=Combination(s,4)}
									break;
					case 'SXZU6':
									if( data_sel[0].length >= minchosen[0] )
									{
										//C(n,2)
										nums += Combination(data_sel[0].length, minchosen[0]);
									}
									break;
					case"SXZU12":
					case"SXZU4":
								if(data_sel[0].length>=minchosen[0]&&data_sel[1].length>=minchosen[1]){
									var h=Array.intersect(data_sel[0],data_sel[1]).length;
									tmp_nums=Combination(data_sel[0].length,minchosen[0])*Combination(data_sel[1].length,minchosen[1]);
									if(h>0){
										if(mname=="SXZU12"){
												tmp_nums-=Combination(h,1)*Combination(data_sel[1].length-1,1)
											}
											else{
												if(mname=="SXZU4"){
														tmp_nums-=Combination(h,1)
												}
												}
									}
									nums+=tmp_nums
								}
								break;
                    case 'BDW2'  :  //二码不定位
                    case 'ZU2'   :  //2位组选
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 1 ){//二码不定位必须选两位或者以上
                                            nums += s*(s-1)/2;
                                        }
                                    };break;
                                    
                    case 'ZXHZ2':	//直选和值2
                        
                        cc = {
                            0:1,
                            1:2,
                            2:3,
                            3:4,
                            4:5,
                            5:6,
                            6:7,
                            7:8,
                            8:9,
                            9:10,
                            10:9,
                            11:8,
                            12:7,
                            13:6,
                            14:5,
                            15:4,
                            16:3,
                            17:2,
                            18:1
                        };
                        for( i=0; i<=max_place; i++ ){
                            var s = data_sel[i].length;
                            for( j=0; j<s; j++ ){
                                nums += cc[parseInt(data_sel[i][j],10)];
                            }
                        };

                        break;
                    case 'ZUHZ2':	//组选和值2
                        cc = {
                            0:0,
                            1:1,
                            2:1,
                            3:2,
                            4:2,
                            5:3,
                            6:3,
                            7:4,
                            8:4,
                            9:5,
                            10:4,
                            11:4,
                            12:3,
                            13:3,
                            14:2,
                            15:2,
                            16:1,
                            17:1,
                            18:0
                        };
                        for( i=0; i<=max_place; i++ ){
                            var s = data_sel[i].length;
                            for( j=0; j<s; j++ ){
                                nums += cc[parseInt(data_sel[i][j],10)];
                            }
                        };
                        break;                                         
                    case 'DWD'  :   //定位胆所有在一起特殊处理
                                    for( i=0; i<=max_place; i++ ){
                                        nums += data_sel[i].length;
                                    };break;
                    case 'SDZX3': //山东11运前三直选
                                    nums = 0;
                                    if( data_sel[0].length > 0 && data_sel[1].length > 0 && data_sel[2].length > 0 ){
                                        for( i=0; i<data_sel[0].length; i++ ){
                                            for( j=0; j<data_sel[1].length; j++ ){
                                                for( k=0; k<data_sel[2].length; k++ ){
                                                    if( data_sel[0][i] != data_sel[1][j] && data_sel[0][i] != data_sel[2][k] && data_sel[1][j] != data_sel[2][k] ){
                                                        nums++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                    case 'SDZU3': //山东11运前三组选
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 2 ){//组六必须选三位或者以上
                                            nums += s*(s-1)*(s-2)/6;
                                        }
                                    };break;
                    case 'SDZX2': //山动十一运前二直选
                                  nums = 0;
                                    if( data_sel[0].length > 0 && data_sel[1].length > 0 ){
                                        for( i=0; i<data_sel[0].length; i++ ){
                                            for( j=0; j<data_sel[1].length; j++ ){
                                                if( data_sel[0][i] != data_sel[1][j]){
                                                    nums++;
                                                }
                                            }
                                        }
                                    }
                                    break;
                    case 'SDZU2': //山东十一运前二组选
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 1 ){//组六必须选三位或者以上
                                            nums += s*(s-1)/2;
                                        }
                                    };break;
                    case 'SDBDW':
                    case 'SDDWD':
                    case 'SDDDS':
                    case 'SDCZW':
                    case 'SDRX1': //任选1中1
                                    for( i=0; i<=max_place; i++ ){
                                        nums += data_sel[i].length;
                                    };break;
                    case 'SDRX2': //任选2中2
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 1 ){//二码不定位必须选两位或者以上
                                            nums += s*(s-1)/2;
                                        }
                                    };break;
                    case 'SDRX3': //任选3中3
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 2 ){//必须选三位或者以上
                                            nums += s*(s-1)*(s-2)/6;
                                        }
                                    };break;
                    case 'SDRX4': //任选4中4
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 3 ){//必须选四位或者以上
                                            nums += s*(s-1)*(s-2)*(s-3)/24;
                                        }
                                    };break;
                    case 'SDRX5': //任选5中5
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 4 ){//必须选四位或者以上
                                            nums += s*(s-1)*(s-2)*(s-3)*(s-4)/120;
                                        }
                                    };break;
                    case 'SDRX6': //任选6中6
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 5 ){//必须选四位或者以上
                                            nums += s*(s-1)*(s-2)*(s-3)*(s-4)*(s-5)/720;
                                        }
                                    };break;
                    case 'SDRX7': //任选7中7
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 6 ){//必须选四位或者以上
                                            nums += s*(s-1)*(s-2)*(s-3)*(s-4)*(s-5)*(s-6)/5040;
                                        }
                                    };break;
                    case 'SDRX8': //任选8中8
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 7 ){//必须选四位或者以上
                                            nums += s*(s-1)*(s-2)*(s-3)*(s-4)*(s-5)*(s-6)*(s-7)/40320;
                                        }
                                    };break;
//下面是北京快乐吧
                    case 'BJRX2': //北京快乐8 任选2
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 1 ){//必须选 两位到八位
                                            nums += s*(s-1)/2;
                                        }
                                    };break;
                    case 'BJRX3': //北京快乐8 任选3
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 2 ){//必须选 三位到八位
                                            nums += s*(s-1)*(s-2)/6;
                                        }
                                    };break;
                    case 'BJRX4': //北京快乐8 任选4
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 3 ){//必须选 四位到八位
                                            nums += s*(s-1)*(s-2)*(s-3)/24;
                                        }
                                    };break;
                    case 'BJRX5': //北京快乐8 任选5
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 4 ){//必须选 五位到八位
                                            nums += s*(s-1)*(s-2)*(s-3)*(s-4)/120;
                                        }
                                    };break;
                    case 'BJRX6': //北京快乐8 任选6
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 5 ){//必须选 六位到八位
                                            nums += s*(s-1)*(s-2)*(s-3)*(s-4)*(s-5)/720;
                                        }
                                    };break;
                    case 'BJRX7': //北京快乐8 任选7
                                    for( i=0; i<=max_place; i++ ){
                                        var s = data_sel[i].length;
                                        if( s > 6 ){//必须选 七位到八位
                                            nums += s*(s-1)*(s-2)*(s-3)*(s-4)*(s-5)*(s-6)/5040;
                                        }
                                    };break;
									
					case "RXZXSSC2": //任选二直选
						var wan = data_sel[0].length;
						var qian = data_sel[1].length;
						var bai = data_sel[2].length;
						var shi = data_sel[3].length;
						var ge = data_sel[4].length;
						//万*千 + 万*百 + 万*十 + 万*个 + 千*百 + 千*十 + 千*个 + 百*十 + 百*个 + 十*个
						nums += wan * qian + wan * bai + wan * shi + wan * ge + qian * bai + qian * shi + qian * ge + bai * shi + bai * ge + shi * ge;
						break;
					case "RXZUSSC2": //任选二组选
						for (i = 0; i <= max_place; i++) {
							var s = data_sel[i].length;
							if (s > 1) {
								nums += s * (s - 1) / 2;
							}
						};
						var select_num = $("input[name='pos[]']:checked").length;
						var multi = 0;
						switch (select_num) {
							case 0:multi = 0;break;
							case 1:multi = 0;break;
							case 2:multi = 1;break;
							case 3:multi = 3;break;
							case 4:multi = 6;break;
							case 5:multi = 10;break;
						}
						nums = nums * multi;
						var str = "";
						$("input[name='pos[]']:checked").each(function () {
							if ($(this).attr("checked")) {
								str += $(this).val() + ",";
							}
						})
						$("input[name='poschoose']").val(str.slice(0, -1));
						break;

                    default     : //默认情况
						for( i=0; i<=max_place; i++ ){
							if( data_sel[i].length == 0 ){//有位置上没有选择
								tmp_nums = 0;
								break;break;
							}
							tmp_nums *= data_sel[i].length;
						}
						nums = tmp_nums;
					break;
                }
            }
            //03:计算金额
            var times = parseInt($($.lt_id_data.id_sel_times).val(),10);
            if( isNaN(times) )
            {
                times = 1;
                $($.lt_id_data.id_sel_times).val(1);
            }
            var money = Math.round(times * nums * 2 * ($.lt_method_data.modes[modes].rate * 1000))/1000;//倍数*注数*单价 * 模式
            money = isNaN(money) ? 0 : money;
            $($.lt_id_data.id_sel_num).html(nums);   //写入临时的注数
            $($.lt_id_data.id_sel_money).html(money);//写临时单笔价格
        };
        //重复号处理
        var dumpNum = function(isdeal){
            var l   = data_sel[0].length;
            var err = [];
            var news= []; //除去重复号后的结果
            if( l == 0 ){
                return err;
            }
            for( i=0; i<l; i++ ){
                if( $.inArray(data_sel[0][i],err) != -1 ){
                    continue;
                }
                for( j=i+1; j<l; j++ ){
                    if( data_sel[0][i] == data_sel[0][j] ){
                        err.push(data_sel[0][i]);
                        break;
                    }
                }
                news.push(data_sel[0][i]);
            }
            if( isdeal ){//如果是做删除重复号的处理
                data_sel[0] = news;
            }
            return err;
        };
        //输入框的字符串处理
        function _inptu_deal(){
            var s = $.trim($("#lt_write_box",$(me)).val());
            s     = $.trim(s.replace(/[^\s\r,;，；　０１２３４５６７８９0-9]/g,""));
            var m = s;
            switch( methodname ){
                case 'SDZX3' :
                case 'SDZU3' :
                case 'SDZX2' :
                case 'SDRX1' :
                case 'SDRX2' :
                case 'SDRX3' :
                case 'SDRX4' :
                case 'SDRX5' :
                case 'SDRX6' :
                case 'SDRX7' :
                case 'SDRX8' :
                case 'SDZU2' : s = s.replace(/[\r\n,;，；]/g,"|").replace(/(\|)+/g,"|"); break;
                default      : s = s.replace(/[\s\r,;，；　]/g,"|").replace(/(\|)+/g,"|"); break;
            }
            s = s.replace(/０/g,"0").replace(/１/g,"1").replace(/２/g,"2").replace(/３/g,"3").replace(/４/g,"4").replace(/５/g,"5").replace(/６/g,"6").replace(/７/g,"7").replace(/８/g,"8").replace(/９/g,"9");
            if( s == "" ){
            	  data_sel[0] = []; //清空数据
            }else{
            	  data_sel[0] = s.split("|");
            }
            return m;
        };
        /************************ 事件触发处理 ****************************/
        if( otype == 'input' ){//手动输入型处理
            $("#lt_write_del",$(me)).click(function(){//删除重复号
                var err = dumpNum(true);
                if( err.length > 0 ){//如果有重复号码
                    checkNum();
                    switch( methodname ){
                        case 'SDZX3' :
                        case 'SDZU3' :
                        case 'SDZX2' :
                        case 'SDRX1' :
				                case 'SDRX2' :
				                case 'SDRX3' :
				                case 'SDRX4' :
				                case 'SDRX5' :
				                case 'SDRX6' :
				                case 'SDRX7' :
				                case 'SDRX8' :
                        case 'SDZU2' : $("#lt_write_box",$(me)).val(data_sel[0].join(";"));
                                       parent.$.alert(lot_lang.am_s3+'\r\n'+err.join(";"));
                                       break;
                        default      : $("#lt_write_box",$(me)).val(data_sel[0].join(" "));
                                       parent.$.alert(lot_lang.am_s3+'\r\n'+err.join(" "));
                                       break;
                    }
                }else{
                    parent.$.alert(lot_lang.am_s4);
                }
            });
            $("#lt_write_import",$(me)).click(function(){//载入文件处理
                $.ajaxUploadUI({
              title    : lot_lang.dec_s27,
        			url      : './?dialogUI=fileupload.php',//服务端处理的文件
        			loadok   : lot_lang.dec_s28,
        			filetype : ['txt','csv'],//允许载入的文件类型
        			success  : function(data){ $("#lt_write_box",$(me)).val(data).change(); },//数据处理
        			onfinish : function(){$("#lt_write_box",$(me)).focus();}
        		});
            });
            $("#lt_write_box",$(me)).change(function(){//输入框时时变动处理
                var s = _inptu_deal();
                $(this).val(s);
                checkNum();
             }).keyup(function(){
                _inptu_deal();
                checkNum();
            });
            $("#lt_write_empty",$(me)).click(function(){//清空处理
                data_sel[0] = []; //清空数据
                $("#lt_write_box",$(me)).val("");
                checkNum();
            });
        }
        
        //选中号码处理
        function selectNum( obj, isButton ){
            if( $.trim($(obj).attr("class")) == 'selected' ){//如果本身是已选中，则不做任何处理
                return;
            }
            $(obj).attr("class","selected");//样式改变为选中
            place = Number($(obj).attr("name").replace("lt_place_",""));
            var number = $.trim($(obj).html());
			if($(obj).children("em").length > 0){
				number = $(obj).children("em").html();
			}
			number=number.toLowerCase();
			number = number.replace(/\<div.*\<\/div>/g,"").replace(/\r\n/g,"");
			
			if($.lt_lottid == 22)
            {

                number = number.replace(/111/gi, 1).replace(/222/gi, 2).replace(/333/gi, 3).replace(/444/gi, 4).replace(/555/gi, 5).replace(/666/gi, 6);
                if (methodname != "KSHZ" && methodname != "SBTHHZ") {
                    number = number.replace(/11/gi, 1).replace(/22/gi, 2).replace(/33/gi, 3).replace(/44/gi, 4).replace(/55/gi, 5).replace(/66/gi, 6)
                }
                number = number.replace(/\*/gi, "");
                if (methodname == "EBTHDT" || methodname == "SBTHDT") {
                    var danlen = 1;
                    if (methodname == "SBTHDT") {
                        danlen = 2
                    }
                    if (data_sel[0].length + 1 > danlen && place == 0) {
                        var lastnumber = data_sel[0][data_sel[0].length - 1];
                        $.each($('li[name="lt_place_0"][class="selected"]'), function(i, n) {
                            if ($(this).children("em").html() == lastnumber) {
                                $(this).attr("class", "");
                                data_sel[0] = $.grep(data_sel[0], function(n, i) {
                                    return n == lastnumber
                                }, true)
                            }
                        })
                    }
                }
                if (methodname == "ETHDX" || methodname == "EBTHDT" || methodname == "SBTHDT") {

                    var unplace = 1 - place;

                    if (data_sel[unplace].contains(number)) {
                        $.each($('li[name="lt_place_' + unplace + '"][class="selected"]'), function(i, n) {
                            var tmphtml = $(this).children("em").html();
                            if (methodname == "ETHDX") {
                                tmphtml = tmphtml.replace(/11/gi, 1).replace(/22/gi, 2).replace(/33/gi, 3).replace(/44/gi, 4).replace(/55/gi, 5).replace(/66/gi, 6)
                            }
                            if (tmphtml == number) {
                                $(this).attr("class", "");
                                data_sel[unplace] = $.grep(data_sel[unplace], function(n, i) {
                                    return n == number
                                }, true)
                            }
                        })
                    }
                }
            }
			
            data_sel[place].push(number);//加入到数组中
            if( !isButton ){//如果不是按钮触发则进行统计，按钮的统一进行统计
				
				var numlimit = parseInt($.lt_method_data.maxcodecount);
				if( numlimit > 0 )
				{
					if( data_sel[place].length > numlimit )
					{
						parent.$.alert(lot_lang.am_s35.replace('%s',numlimit));
						unSelectNum(obj,false);
					}
				}
                checkNum();
            }
        };
        //取消选中号码处理
        function unSelectNum( obj, isButton ){
            if( $.trim($(obj).attr("class")) != 'selected' ){//如果本身是未选中，则不做任何处理
                return;
            }
            $(obj).attr("class","button1");//样式改变为未选中
            place = Number($(obj).attr("name").replace("lt_place_",""));
            var number = $.trim($(obj).html());
			if($(obj).children("em").length > 0){
				number = $(obj).children("em").html();
			}
			number=number.toLowerCase();
			number = number.replace(/\<div.*\<\/div>/g,"").replace(/\r\n/g,"");
			
            if($.lt_lottid == 22)
            {
                number = number.replace(/111/gi, 1).replace(/222/gi, 2).replace(/333/gi, 3).replace(/444/gi, 4).replace(/555/gi, 5).replace(/666/gi, 6);
                if (methodname != "KSHZ" && methodname != "SBTHHZ") {
                    number = number.replace(/11/gi, 1).replace(/22/gi, 2).replace(/33/gi, 3).replace(/44/gi, 4).replace(/55/gi, 5).replace(/66/gi, 6)
                }
                number = number.replace(/\*/gi, "");
            }
			
            data_sel[place] = $.grep(data_sel[place],function(n,i){//从选中数组中删除取消的号码
				return n == number;
            },true);
            if( !isButton ){//如果不是按钮触发则进行统计，按钮的统一进行统计
                checkNum();
            }
        };
        //选择与取消号码选择交替变化
        function changeNoCss(obj){
            if( $.trim($(obj).attr("class")) == 'selected' ){//如果本身是已选中，则做取消
                unSelectNum(obj,false);
            }else{
                selectNum(obj,false);
            }
        };
        //选择奇数号码
        function selectOdd(obj){
            if( Number($(obj).html()) % 2 == 1 ){
                 selectNum(obj,true);
            }else{
                 unSelectNum(obj,true);
            }
        };
        //选择偶数号码
        function selectEven(obj){
            if( Number($(obj).html()) % 2 == 0 ){
                 selectNum(obj,true);
            }else{
                 unSelectNum(obj,true);
            }
        };
        //选则大号
        function selectBig(i,obj){
            if( i >= opts.noBigIndex ){
                selectNum(obj,true);
            }else{
                unSelectNum(obj,true);
            }
        };
        //选择小号
        function selectSmall(i,obj){
            if( i < opts.noBigIndex ){
                selectNum(obj,true);
            }else{
                unSelectNum(obj,true);
            }
        };
        //设置号码事件
        $(this).find("[name^='lt_place_']").click(function(){
            changeNoCss(this);
        });
		
		$(".posChoose").change(function(){
			checkNum();
		});
		
		
        //全大小单双清按钮的动作行为处理
		//if( opts.isButton == true ){//如果有这几个按钮才做处理
		if( 1 ){
            $("[class='selectType']",$(this)).click(function(){//清除处理
            	var td = $(this).parent().parent().children('ul')[0];
            	td = $(td);
                switch( $(this).attr('name') ){
                    case 'all'   :
						$.each(td.children(),function(i,n){
							//alert(i+"|"+$(n).html());
							selectNum(n,true);
						});
						break;
                    case 'big'   :
                                 $.each(td.children(),function(i,n){
                                    selectBig(i,n);
                                 });break;
                    case 'small' :
                                 $.each(td.children(),function(i,n){
                                    selectSmall(i,n);
                                 });break;
                    case 'odd'   :
                                 $.each(td.children(),function(i,n){
                                    selectOdd(n);
                                 });break;
                    case 'even'  :
                                 $.each(td.children(),function(i,n){
                                    selectEven(n);
                                 });break;
                    case 'clean'  :
						$.each(td.children(),function(i,n){
							unSelectNum(n,true);
						});break;
                    default : break;
                }
                checkNum();
            });
        }else if( otype == 'dxds' ){//或者玩法为大小单双即有清按钮的处理
            $("div[class='selcleanbutton']",$(this)).click(function(){
                $.each($(this).parent().children(":first").children(),function(i,n){
                    unSelectNum(n,true);
                });
                checkNum();
            });
        }
        //倍数输入处理事件
        $($.lt_id_data.id_sel_times).keyup(function(){
            var times = $(this).val().replace(/[^0-9]/g,"").substring(0,5);
            $(this).val( times );
            if( times == "" ){
                times = 0;
            }else{
                times = parseInt(times,10);//取整倍数
            }
            var nums  = parseInt($($.lt_id_data.id_sel_num).html(),10);//投注注数取整
            //var modes = parseInt($("#lt_project_modes").val(),10);//投注模式
			var modes = parseInt($("input[name='lt_project_modes']:checked").val(),10);//投注模式
            var money = Math.round(times * nums * 2 * ($.lt_method_data.modes[modes].rate * 1000))/1000;//倍数*注数*单价 * 模式
            money = isNaN(money) ? 0 : money;
            $($.lt_id_data.id_sel_money).html(money);
        });
        $($.lt_id_data.id_beishuselect).change(function(){
            $($.lt_id_data.id_sel_times).val($(this).val()).keyup();
			//$(this)[0].selectedIndex=0;
        });
        //模式变换处理事件
        //$("#lt_project_modes").change(function(){
		$("input[name='lt_project_modes']").click(function(){

            var nums  = parseInt($($.lt_id_data.id_sel_num).html(),10);//投注注数取整
            var times = parseInt($($.lt_id_data.id_sel_times).val(),10);//投注倍数取整
            //var modes = parseInt($("#lt_project_modes").val(),10);//投注模式
			var modes = parseInt($("input[name='lt_project_modes']:checked").val(),10);//投注模式
            var money = Math.round(times * nums * 2 * ($.lt_method_data.modes[modes].rate * 1000))/1000;//倍数*注数*单价 * 模式
            money = isNaN(money) ? 0 : money;
            $($.lt_id_data.id_sel_money).html(money);
        });
		
		function newNumber(start,end){
			return Math.round(Math.random()*(end-start)+start);//生成在[start,end]范围内的随机数值，只支持不小于0的合法范围
		}
		function isHaveThisNumber(para,num){
			if(typeof(para) == "object")
			{
				if(para.length==0)
			    {
			        return false;
			    }
			}
			for(var i=0;i<para.length;i++){
				if(para[i]==num){
					return true;//与目标数组有重复
				}
			}
			return false;
		}
		function newRandomNumbersWithNoRepeat(start,end,size){
			var para=new Array();//目标随机数组
			var rnum;//当前随机数
			var currentIndex=0;//当前随机数组的索引
			if(start>end||start<0||end<0||size<0){
			    return;
			}
			if(end-start+1<size){//验证随机数个数是否超出随机数范围
			    return;
			}
			for(var i=0;i<size;i++){//生成 size 个不重复的随机数
				rnum=newNumber(start,end);//获取随机数
				if(isHaveThisNumber(para,rnum)){//是否已经存在
					while(isHaveThisNumber(para,rnum)){//获取新的随机数 直到不重复
			            rnum=newNumber(start,end);//重新获取随机数
			        }
				}
				para[currentIndex++]=rnum;//添加到现有数字集合中
			}
			return para;
		}
		//随机选N注
			$(".lt_random_bets_10,.lt_random_bets_5,.lt_random_bets_1").unbind("mouseover").mouseover(function(){
				random_bets = true;//当前为机选sam
			});
			
			$(".lt_random_bets_10,.lt_random_bets_5,.lt_random_bets_1").unbind("mouseout").mouseout(function(){
				random_bets = false;//当前不为机选sam
			});
		
			$(".lt_random_bets_10").unbind("click").click(function(){
				for (var i=0; i<10; i++) {
					$(".lt_random_bets_1").trigger("click");
				}
			});
			$(".lt_random_bets_5").unbind("click").click(function(){
				for (var i=0; i<5; i++) {
					$(".lt_random_bets_1").trigger("click"); 
				}
			});
			$(".lt_random_bets_1").unbind("click").click(function() {
				//当前为机选sam
				if(random_bets){
					for( i=0; i<data_sel.length; i++ ){//清空已选择数据
						data_sel[i] = [];
					}
					if( otype == 'input' ){//清空所有显示的数据
						$("#lt_write_box",$(me)).val("");
					}
					else if( otype == 'digital' || otype == 'dxds' || otype == 'dds' ){
						$.each($("li",$(me)).filter(".selected"),function(i,n){
							$(this).removeClass("selected").addClass("button1");
						});
					}
				}

				var randomcos = $("#randomcos").val(); //行数
				var randomcosvalue=$("#randomcosvalue").val(); //每行最少选择个数用|分割
				var totalnum=0;
				var minsize = randomcosvalue.split('|'); //每行最少选择个数分割后的数组
				for (var i=0; i<minsize.length; i++) {
					if (minsize[i]>0) {
						totalnum += parseInt(minsize[i]);//得到一共最少选择号码个数
					}
				}
				var end = $("li[name^='lt_place_0']").length;
				var para=newRandomNumbersWithNoRepeat(0,end-1,totalnum); //随机得到一个不重复的数字组成的数组
				var randomcos_arr = [];
				randomcos_arr.length = randomcos;
				$.each(randomcos_arr,function(i,v1){
					var minsize_arr = [];
					minsize_arr.length = minsize[i];
					$.each(minsize_arr,function(j,v2){
						$.each($("li[name^='lt_place_"+i+"']"), function(n, val){
							if (n == para[j]) {
								$(this).trigger("click");
							}
						});
					});
					para.shift();
				});
				para=[];

				$($.lt_id_data.id_sel_insert).trigger("click");
			});
        //添加按钮
        $($.lt_id_data.id_sel_insert).unbind("click").click(function(){
            var nums  = parseInt($($.lt_id_data.id_sel_num).html(),10);//投注注数取整
			
            var times = parseInt($($.lt_id_data.id_sel_times).val(),10);//投注倍数取整
            //var modes = parseInt($("#lt_project_modes").val(),10);//投注模式
			var modes = parseInt($("input[name='lt_project_modes']:checked").val(),10);//投注模式
            var money = Math.round(times * nums * 2 * ($.lt_method_data.modes[modes].rate * 1000))/1000;//倍数*注数*单价 * 模式
            var mid   = $.lt_method_data.methodid;
			
            if( isNaN(nums) || isNaN(times) || isNaN(money) || money <= 0 ){//如果没有任何投注内容
				parent.$.alert(otype == 'input' ? lot_lang.am_s29 : lot_lang.am_s19);
                return;
            }
            if( otype == 'input' ){//如果是输入型，则检测号码合法性，以及是否存在重复号
                var mname = $.lt_method[mid];//玩法的简写,如:'ZX3'
			
                var error = [];
                var edump = [];
                var ermsg = "";
                //检测重复号，并除去重复号
                edump = dumpNum(true);
                if( edump.length >0 ){//有重复号
                    ermsg += lot_lang.em_s2+'\n'+edump.join(", ")+"\n";
                    checkNum();//重新统计
                    nums  = parseInt($($.lt_id_data.id_sel_num).html(),10);//投注注数取整
                    money = Math.round(times * nums * 2 * ($.lt_method_data.modes[modes].rate * 1000))/1000;//倍数*注数*单价*模式
                }
                switch(mname){//根据类型不同做不同检测
                    case 'ZX3'  : error = _inputCheck_Num(3,true); break;
                    case 'HHZX' : error = _inputCheck_Num(3,true,_HHZXcheck,true); break;
                    case 'ZX2'  : error = _inputCheck_Num(2,true); break;
                    case 'ZU2'  : error = _inputCheck_Num(2,true,_HHZXcheck,true); break;
                    case 'ZU2'  : error = _inputCheck_Num(2,true,_HHZXcheck,true); break;
                    case 'ZX5'  : error = _inputCheck_Num(5,true); break;
                    case 'ZX4'  : error = _inputCheck_Num(4,true); break;
                    case 'ZUS'  : error = _inputCheck_Num(3,true, _ZUSDScheck, true); break;
                    case 'ZUL'  : error = _inputCheck_Num(3,true, _ZULDScheck, true); break;
                    case 'SDZX3': error = _inputCheck_Num(8,true,_SDinputCheck,false); break;
                    case 'SDZU3': error = _inputCheck_Num(8,true,_SDinputCheck,true); break;
                    case 'SDZX2': error = _inputCheck_Num(5,true,_SDinputCheck,false); break;
                    case 'SDZU2': error = _inputCheck_Num(5,true,_SDinputCheck,true); break;
                    case 'SDRX1': error = _inputCheck_Num(2,true,_SDinputCheck,false); break;
                    case 'SDRX2': error = _inputCheck_Num(5,true,_SDinputCheck,true); break;
                    case 'SDRX3': error = _inputCheck_Num(8,true,_SDinputCheck,true); break;
                    case 'SDRX4': error = _inputCheck_Num(11,true,_SDinputCheck,true); break;
                    case 'SDRX5': error = _inputCheck_Num(14,true,_SDinputCheck,true); break;
                    case 'SDRX6': error = _inputCheck_Num(17,true,_SDinputCheck,true); break;
                    case 'SDRX7': error = _inputCheck_Num(20,true,_SDinputCheck,true); break;
                    case 'SDRX8': error = _inputCheck_Num(23,true,_SDinputCheck,true); break;
					case 'ETHDX': error = _inputCheck_Num(3, true,_ETHDXcheck, true); break;
                    case 'SBTH' :error = _inputCheck_Num(3, true, _SBTHDScheck, true);break;
                    case 'EBTH' :error = _inputCheck_Num(2, true, _EBTHDScheck, true);break;  
                    default     : break;
                }
                if( error.length > 0 ){//如果存在错误的号码，则提示
                    ermsg += lot_lang.em_s1+'\n'+error.join(", ")+"\n";
                }
                
                if( ermsg.length > 1 ){
                    parent.$.alert(ermsg);
                }
            }
            var nos = $.lt_method_data.str;
		
            var serverdata = "{'type':'"+otype+"','methodid':"+mid+",'codes':'";
			
            var temp = [];
            for( i=0; i<data_sel.length; i++ ){
                nos = nos.replace('X',data_sel[i].sort(_SortNum).join($.lt_method_data.sp));
                temp.push( data_sel[i].sort(_SortNum).join("&") );
            }
            if( nos.length > 40 ){
				var rand=(Math.random()+'').substr(2,8);
                var nohtml = nos.substring(0,37)+' ......<a href="#" onclick="div_slow_show('+rand+');return(false);" style="color:#FFF;">'+lot_lang.dec_s5+'</a>';
				nohtml+='<div id="div_slow_id_'+rand+'" class="more" style="display:none;"><a class="close" href="#" onclick="div_slow_hide('+rand+');return(false);" ></a><br /><textarea class="code" readonly="readonly">'+nos+'</textarea></div>';
            }else{
                var nohtml = nos;
            }
			var pmodel = $("#pmode").val();//投注奖金模式
		
			if(typeof(pmodel) != "undefined"){
				switch(pmodel){
					case '2':
						stemp = $.lt_method_data.nfdprize.levs;
					break;
					default:
						stemp = $.lt_method_data.nfdprize.defaultprize;
				}
				stemp =  '模式:'+stemp 
				
			}
			else{
					stemp = "" ;
					pmodel = 1;
			}
			
		//	pmodel = stemp;
            //判断是否有重复相同的单
            if( $.lt_same_code[mid] != undefined && $.lt_same_code[mid][modes] != undefined && $.lt_same_code[mid][modes].length > 0 ){
                    if( $.inArray(temp.join("|"),$.lt_same_code[mid][modes]) != -1 ){//存在相同的
						//当前为机选sam
						if(random_bets){
							for( i=0; i<data_sel.length; i++ ){//清空已选择数据
								data_sel[i] = [];
							}
							if( otype == 'input' ){//清空所有显示的数据
								$("#lt_write_box",$(me)).val("");
							}
							else if( otype == 'digital' || otype == 'dxds' || otype == 'dds' ){
								$.each($("li",$(me)).filter(".selected"),function(i,n){
									$(this).removeClass("selected").addClass("button1");
								});
							}
						}
						checkNum();
                        parent.$.alert(lot_lang.am_s28);
                        return false;
                    }
            }
            nohtml  = '['+$.lt_method_data.title+'_'+$.lt_method_data.name+'] '+nohtml;
            noshtml = '['+$.lt_method_data.title+'_'+$.lt_method_data.name+'] '+nos.substring(0,37);
            //serverdata += temp.join("|")+"','nums':"+nums+",'omodel':"+pmodel+",'times':"+times+",'money':"+money+",'mode':"+modes+",'desc':'"+noshtml+"'}";
			if($("input[name='poschoose']").val())
            {
                serverdata += temp.join("|")+"','nums':"+nums+",'omodel':"+pmodel+",'times':"+times+",'money':"+money+",'mode':"+modes+",'desc':'"+noshtml+"','poschoose':'" + $("input[name='poschoose']").val() + "'}";
            } else {
                serverdata += temp.join("|")+"','nums':"+nums+",'omodel':"+pmodel+",'times':"+times+",'money':"+money+",'mode':"+modes+",'desc':'"+noshtml+"'}";
            }
            //CLIVE添加投注内容时候的东西要修改sean /Content/Images/skin/btn_08.jpg
			//alert(serverdata);
			var cfhtml = '<tr class="table_line"><th width="55%"> ' + nohtml + ' </th><td width="12%">' + stemp + '</td><td width="5%">[' + $.lt_method_data.modes[modes].name + ']</td><td width="10%">' + nums + lot_lang.dec_s1 + '</td><td width="8%">' + times + lot_lang.dec_s2 + '</td><td width="10%">' + money + lot_lang.dec_s3 + '</td><td class="del" width="45"><span><img src="/Content/images/skin/btn_08.jpg"></span><input type="hidden" name="lt_project[]" value="' + serverdata + '" /></td></tr>';
		   //<!--var cfhtml = '<tr><td><div class="table_line"><ul><li>&nbsp;<img src="images/skin/ico_03.png"> '+nohtml+'</li><li>模式:'+stemp+'</li><li>'+$.lt_method_data.modes[modes].name+'</li><li>'+nums+lot_lang.dec_s1+'</li><li>'+times+lot_lang.dec_s2+'</li><li>'+money+lot_lang.dec_s3+'</li><li class="del"><span><img src="images/skin/btn_08.jpg"></span><input type="hidden" name="lt_project[]" value="'+serverdata+'" /></li></ul></div></td></tr>';-->
            var $cfhtml = $(cfhtml);
            $cfhtml.prependTo($.lt_id_data.id_cf_content);
            //详情查看
/*
            $('span',$cfhtml).filter(".open").click(function(){
                var mme = this;
                var $h = $('<font>'+lot_lang.dec_s5+'</font><span class="close">['+lot_lang.dec_s6+']</span><br /><textarea class="code" readonly="readonly">'+nos+'</textarea>');
                $(this).openFloat($h,"more");
				$(this).show("slow");
                $("span",$(this).parent()).filter(".close").click(function(){
                    $(mme).closeFloat();
                });
            });
*/
            $.lt_total_nums  += nums;//总注数增加
            $.lt_total_money += money;//总金额增加
            $.lt_total_money  = Math.round($.lt_total_money*1000)/1000;
            basemoney         = Math.round(nums * 2 * ($.lt_method_data.modes[modes].rate * 1000))/1000;//注数*单价 * 模式
            $.lt_trace_base   = Math.round(($.lt_trace_base+basemoney)*1000)/1000;
            $($.lt_id_data.id_cf_num).html($.lt_total_nums);//更新总注数显示
            $($.lt_id_data.id_cf_money).html($.lt_total_money);//更新总金额显示
            $($.lt_id_data.id_cf_count).html(parseInt($($.lt_id_data.id_cf_count).html(),10)+1);//总投注项加1
            //计算奖金，并且判断是否支持利润率追号
            var pc = 0;
            var pz = 0;
            $.each($.lt_method_data.prize,function(i,n){
                n = isNaN(Number(n)) ? 0 : Number(n);
                pz = pz > n ? pz : n;
                pc++;
            });
            if( pc != 1 ){
                pz = 0;
            }
            pz = Math.round( pz * ($.lt_method_data.modes[modes].rate * 1000))/1000;
            $cfhtml.data('data',{methodid:mid,nums:nums,money:money,modes:modes,basemoney:basemoney,prize:pz,code:temp.join("|")});
            //把投注内容记录到临时数组中，用于判断是否有重复
            if( $.lt_same_code[mid] == undefined ){
                    $.lt_same_code[mid] = [];
            }
            if( $.lt_same_code[mid][modes] == undefined ){
                    $.lt_same_code[mid][modes] = [];
            }
            $.lt_same_code[mid][modes].push(temp.join("|"));
            $('td',$cfhtml).filter(".del").find("span").css("cursor",'pointer').attr("title",lot_lang.dec_s24).click(function(){
                var n = $cfhtml.data('data').nums;
                var m = $cfhtml.data('data').money;
                var b = $cfhtml.data('data').basemoney;
                var c = $cfhtml.data('data').code;
                var d = $cfhtml.data('data').methodid;
                var f = $cfhtml.data('data').modes;
                var i = null;
                //移除临时数组中该投注内容，用于判断是否有重复
                $.each($.lt_same_code[d][f],function(k,code){
                    if( code == c ){
                        i = k;
                    }
                });
                if( i != null ){
                    $.lt_same_code[d][f].splice(i,1);
                }else{
                    parent.$.alert(lot_lang.am_s27);
                    return;
                }
                $.lt_total_nums  -= n;//总注数减少
                $.lt_total_money -= m;//总金额减少
                $.lt_total_money = Math.round($.lt_total_money*1000)/1000;
                $.lt_trace_base  = Math.round(($.lt_trace_base-b)*1000)/1000;
                $(this).parent().parent().remove();
                $($.lt_id_data.id_cf_num).html($.lt_total_nums);//更新总注数显示
                $($.lt_id_data.id_cf_money).html($.lt_total_money);//更新总金额显示
                $($.lt_id_data.id_cf_count).html(parseInt($($.lt_id_data.id_cf_count).html(),10)-1);//总投注项减1
                cleanTraceIssue();//清空追号区数据
				
				/*全清功能*/
				showClearAll();
            });
            //把所选模式存入cookie里面
            SetCookie('modes',modes,86400);
            //成功添加以后清空选号区数据
            for( i=0; i<data_sel.length; i++ ){//清空已选择数据
                data_sel[i] = [];
            }
            if( otype == 'input' ){//清空所有显示的数据
                $("#lt_write_box",$(me)).val("");
            }
            else if( otype == 'digital' || otype == 'dxds' || otype == 'dds' ){
                $.each($("li",$(me)).filter(".selected"),function(i,n){
                    $(this).removeClass("selected").addClass("button1");
                });
            }
            //还原倍数为1倍
            //$($.lt_id_data.id_sel_times).val(1);sean倍数
			select_init();
            checkNum();
            //清空追号区数据
            cleanTraceIssue();
			
			/*全清功能*/
			showClearAll();
        });
		//select_init();
    };
	function select_init(){
		if(is_select){
			$($.lt_id_data.id_beishuselect)[0].selectedIndex=0;
		}else{
			//$("input[name='lt_project_modes']:eq(0)").attr("checked",'checked'); 
		}
	}

})(jQuery);

/*全清功能*/
function showClearAll(){
	var numtotal = $($.lt_id_data.id_cf_count).text();
	if(numtotal > 1){
		$(".lottery_list .cleanall").show();
		$(".lottery_list .cleanall").click(function () {
		    parent.$.confirm("确定要清除投注内容吗？", function () {
		        var num = $($.lt_id_data.id_cf_content + " tr td span[title=" + lot_lang.dec_s24 + "]").length;
		        if (num > 0) {
		            $.each($($.lt_id_data.id_cf_content + " tr td span[title=" + lot_lang.dec_s24 + "]"), function (i, v) {
		                $(v).trigger('click');
		            });
		            $(this).unbind().hide();
		        }
		    });
		});
	}else{
		$(".lottery_list .cleanall").hide();
	}
}
