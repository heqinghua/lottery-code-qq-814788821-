(function(g) {
	if (/^1.2/.test(g.fn.jquery) || /^1.1/.test(g.fn.jquery)) {
		alert("requires jQuery v1.3 or later !  You are using v" + g.fn.jquery);
		return
	}

	g.blockUI_lang = {
		button_sure: "\u786e\u5b9a",
		button_cancel: "\u53d6\u6d88",
		button_upload: "\u8f7d\u5165\u6587\u4ef6",
		button_uploading: "\u6b63\u5728\u8f7d\u5165....",
		button_uploadend: "\u5b8c\u6210",
//title_warn: "\u6e29\u99a8\u63d0\u793a",
//title_confirm: "\u6e29\u99a8\u63d0\u793a",
		title_warn: "\u63d0\u793a",
		title_confirm: "\u63d0\u793a",
		title_upload: "Ajax Upload",
		desc_updefaultmsg: "\u8bf7\u9009\u62e9\u4f60\u8981\u8f7d\u5165\u7684\u6587\u4ef6",
		desc_uploaderror: "\u6587\u4ef6\u683c\u5f0f\u9519\u8bef\uff0c\u53ea\u652f\u6301[%str%]\u7c7b\u578b\u7684\u6587\u4ef6",
		desc_uploadingerror: "\u8f7d\u5165\u6587\u4ef6\u5931\u8d25,\u8bf7\u91cd\u8bd5",
		img_dir: "../dialogUI/"
	};
	g.blockUI = function(o) {
		e(window, o)
	};
	g.unblockUI = function(o) {
		f(window, o)
	};
	g.fn.block = function(o) {
		return this.unblock({
			fadeOut: 0
		}).each(function() {
			if (g.css(this, "position") == "static") {
				this.style.position = "relative"
			}
			if (g.browser.msie) {
				this.style.zoom = 1
			}
			e(this, o)
		})
	};
	g.fn.unblock = function(o) {
		return this.each(function() {
			f(this, o)
		})
	};
	g.alert = function(t, r, q, p) {
		r = (r && r.length > 1) ? r: g.blockUI_lang.title_warn;
		t = t ? t: undefined;
		q = (q && typeof(q) == "function") ? q: function() {};
		p = parseInt(p, 10);
		p = isNaN(p) ? 0 : p;
		if (t == undefined) {
			return
		}
		if (typeof(t) == "string") {
			t = t.replace(/\r\n/g, "<br />").replace(/\r/g, "<br />")
		}
		var o = m({
		    cl_box: "block_alert",
		    width: p,
		    isbottom: true,
		    title: r,
		    msg: t,
		    total: "",
		    bt_text: '<input type="button" value="' + g.blockUI_lang.button_sure + '" id="alert_close_button" class="submit" />',
            isAlert:true
		});
		$html = g(o);
		
		g.blockUI({
			message: $html,
			fadeInTime: 0,
			fadeOutTime: 0,
			overlayCSS: {
				backgroundColor: "#000000",
				opacity: 0.5
			}
		});
		$(".blockMsg").css({ width: "300",left:($(window).width()/2-150)});
		$(".bottom,.content").css({ width: "294" });
		
		g("#block_close", $html).add(g("#alert_close_button", $html)).click(function() {
			g.unblockUI({
				fadeInTime: 0,
				fadeOutTime: 0,
				onUnblock: q
			})
		});
	};
	g.confirm = function (u, total, o, r, t, q) {
	    if ($.isFunction(total)) {
	        o = total;
	        total = "";
	    }
	    t = (t && t.length > 1) ? t : g.blockUI_lang.title_confirm;
		u = u ? u: undefined;
		o = (o && typeof(o) == "function") ? o: function() {};
		r = (r && typeof(r) == "function") ? r: function() {};
		q = parseInt(q, 10);
		q = isNaN(q) ? 0 : q;
		if (u == undefined) {
			return
		}
		if (typeof(u) == "string") {
			u = u.replace(/\r\n/g, "<br />").replace(/\r/g, "<br />").replace(/\n/g, "<br />");
		}
		var p = m({
			cl_box: "block_confirm",
			width: q,
			isbottom: true,
			title: t,
			msg: u,
			total:total,
			bt_text: '<input type="button" value="' + g.blockUI_lang.button_cancel + '" id="confirm_no" class="cancel" />&nbsp;&nbsp;<input type="button" value="' + g.blockUI_lang.button_sure + '" id="confirm_yes" width="50%" height="43" align="center" class="submit" />'
		});
		$html = g(p);
		g.blockUI({
			message: $html,
			fadeInTime: 0,
			fadeOutTime: 0,
			overlayCSS: {
				backgroundColor: "#000000",
				opacity: 0.5
			}
		});
		g("#block_close", $html).add(g("#confirm_no", $html)).click(function() {
			g.unblockUI({
				fadeInTime: 0,
				fadeOutTime: 0,
				onUnblock: r
			})
		});
		g("#confirm_yes", $html).click(function() {
			g.unblockUI({
				fadeInTime: 0,
				fadeOutTime: 0,
				onUnblock: o
			})
		});
		g("#JS_blockPage").DragDrop({
			handler: "#block_draghandler"
		})
	};
	g.ajaxUploadUI = function(p) {
		var o = {
			title: g.blockUI_lang.title_upload,
			message: g.blockUI_lang.desc_updefaultmsg,
			filetype: ["txt", "csv", "gif", "jpg", "png"],
			loadhtml: "loading.....",
			loadok: '<img src="' + g.blockUI_lang.img_dir + 'ok.png" />&nbsp;load has already ok..',
			inputfile: "ajaxUploadFile",
			onfinish: function() {},
			url: "",
			dataType: "text"
		};
		p = g.extend({},
		o, p || {});
		var z = p.message;
		if (z && z != null) {
			z += "<br />"
		} else {
			z = ""
		}
		z = '<form action="' + p.url + '" id="block_ajaxUploadForm" method="POST" enctype="multipart/form-data" target="block_ajaxUploadIframe"><div id="block_ajaxUploadArea">' + z + '<input type="file" name="' + p.inputfile + '" id="block_ajaxUploadFile" size="20"></div></form><div id="block_ajaxUploading">' + p.loadhtml + '</div><div id="block_ajaxUploadError"></div><iframe name="block_ajaxUploadIframe" id="block_ajaxUploadIframe" style="width:0px; height:0px;display:none;"></iframe>';
		var v = m({
			cl_box: "block_ajaxUpload",
			isbottom: true,
			title: p.title,
			msg: z,
			bt_text: '<input type="button" value="' + g.blockUI_lang.button_upload + '" id="block_ajaxConfirm" /><input type="button" value="' + g.blockUI_lang.button_cancel + '" id="block_close" />'
		});
		$html = g(v);
		g.blockUI({
			message: $html,
			fadeInTime: 0,
			overlayCSS: {
				backgroundColor: "#000000",
				opacity: 0.5
			}
		});
		g("#block_close", $html).click(function() {
			g.unblockUI({
				fadeOutTime: 0
			})
		});
		g("#JS_blockPage").DragDrop({
			handler: "#block_draghandler"
		});
		s = g.extend({},
		g.ajaxSettings, p);
		var w = {};
		var r = false;
		g("#block_ajaxConfirm").click(function() {
			filepath = g("#block_ajaxUploadFile").val();
			if (filepath == "" && filepath == null || filepath.length < 1) {
				return
			}
			filetype = filepath.substr(filepath.lastIndexOf(".") + 1).toLowerCase();
			if (g.inArray(filetype, p.filetype) == -1) {
				u(g.blockUI_lang.desc_uploaderror.replace("%str%", p.filetype.join(", ")));
				return false
			}
			t();
			if (s.timeout > 0) {
				setTimeout(function() {
					if (!r) {
						x("timeout")
					}
				},
				s.timeout)
			}
			try {
				var A = g("#block_ajaxUploadForm");
				g(A).attr("method", "POST");
				if (A.encoding) {
					A.encoding = "multipart/form-data"
				} else {
					A.enctype = "multipart/form-data"
				}
				g(A).submit()
			} catch(B) {
				y(g.blockUI_lang.desc_uploadingerror);
				g.handleError(s, w, "error", B)
			}
			if (window.attachEvent) {
				document.getElementById("block_ajaxUploadIframe").attachEvent("onload", x)
			} else {
				document.getElementById("block_ajaxUploadIframe").addEventListener("load", x, false)
			}
			return {
				abort: function() {}
			}
		});
		function u(A) {
			g("#block_ajaxUploadError").html(A).show().delay(3000).fadeOut(400)
		}
		function y(A) {
			g("#block_ajaxConfirm").val(g.blockUI_lang.button_upload).attr("disabled", false);
			g("#block_ajaxUploading").hide();
			g("#block_ajaxUploadArea").show();
			u(A)
		}
		function t() {
			r = false;
			g("#block_ajaxConfirm").val(g.blockUI_lang.button_uploading).attr("disabled", true);
			g("#block_ajaxUploadArea").hide();
			g("#block_ajaxUploading").show();
			if (s.global && !g.active++) {
				g.event.trigger("ajaxStart")
			}
		}
		function x(A) {
			var C = document.getElementById("block_ajaxUploadIframe");
			try {
				if (C.contentWindow) {
					w.responseText = C.contentWindow.document.body ? C.contentWindow.document.body.innerHTML: null;
					w.responseXML = C.contentWindow.document.XMLDocument ? C.contentWindow.document.XMLDocument: C.contentWindow.document
				} else {
					if (C.contentDocument) {
						w.responseText = C.contentDocument.document.body ? C.contentDocument.document.body.innerHTML: null;
						w.responseXML = C.contentDocument.document.XMLDocument ? C.contentDocument.document.XMLDocument: C.contentDocument.document
					}
				}
			} catch(E) {
				y(g.blockUI_lang.desc_uploadingerror);
				g.handleError(s, w, null, E)
			}
			if (w || A == "timeout") {
				r = true;
				var B;
				try {
					B = A != "timeout" ? "success": "error";
					if (B != "error") {
						var D = q(w, s.dataType);
						g("#block_ajaxUploading").html(p.loadok);
						g("#block_ajaxConfirm").val(g.blockUI_lang.button_uploadend).attr("disabled", false).die("click").click(function() {
							g.unblockUI({
								fadeOutTime: 0,
								onUnblock: p.onfinish
							})
						});
						g("#block_close", $html).die("click").click(function() {
							g.unblockUI({
								fadeOutTime: 0,
								onUnblock: p.onfinish
							})
						});
						if (s.success) {
							s.success(D, B)
						}
						if (s.global) {
							g.event.trigger("ajaxSuccess", [w, s])
						}
					} else {
						y(g.blockUI_lang.desc_uploadingerror);
						g.handleError(s, w, B)
					}
				} catch(E) {
					y(g.blockUI_lang.desc_uploadingerror);
					B = "error";
					g.handleError(s, w, B, E)
				}
				if (s.global) {
					g.event.trigger("ajaxComplete", [w, s])
				}
				if (s.global && !--g.active) {
					g.event.trigger("ajaxStop")
				}
				if (s.complete) {
					s.complete(w, B)
				}
				g(C).unbind();
				w = null
			}
		}
		function q(A, B) {
			var C = !B;
			C = (B == "xml" || C) ? A.responseXML: A.responseText;
			if (B == "script") {
				g.globalEval(C)
			}
			if (B == "json") {
				if (/^[\],:{}\s]*$/.test(C.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, "@").replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, "]").replace(/(?:^|:|,)(?:\s*\[)+/g, ""))) {
					if (window.JSON && window.JSON.parse) {
						C = window.JSON.parse(C)
					} else {
						C = (new Function("return " + C))()
					}
				} else {
					throw "Invalid JSON: " + C
				}
			}
			return C
		}
	};
	function m(p) {
		var q = {
			cl_box: "block_box",
			cl_title: "block_title",
			cl_close: "block_title_close",
			cl_c_box: "block_content_box",
			cl_content: "block_content",
			cl_bottom: "block_bottom",
			isbottom: false,
			title: "tip",
			msg: "",
			bt_text: "",
			width: 0
		};
		p = g.extend({},
		q, p || {});
		if (p.total == undefined)
		    p.total = "";
		var ctHtml = "";
		if (p.isAlert != undefined && p.isAlert)
		{
		    ctHtml = "text-align:center;";
		}
		//if (q.isAlert)
            

		
		//\u6e29\u99a8\u63d0\u793a(温馨提示)
		var o = '<h3 class="title">\u63d0\u793a</h3><div id="block_content"  class="content"><div class="detail">' + p.msg + '</div></div><div id="block_bottom" class="bottom" style="' + ctHtml + '"><span style="float:left;padding-left:10px;color:#000;padding-top:5px;">' + p.total + '</span>' + p.bt_text + "</div>";
		return o
	}
	g.blockUI.defaults = {
		message: "<h1>Please wait...</h1>",
		baseZ: 2000,
		fadeInTime: 200,
		fadeOutTime: 400,
		timeout: 0,
		overlayCSS: {
			backgroundColor: "#CCCCCC",
			opacity: 0.6,
			cursor: "default"
		},
		centerX: true,
		centerY: true,
		showOverlay: true,
		focusInput: true,
		onUnblock: null,
		quirksmodeOffsetHack: 4
	};
	g.blockUI.version = "1.0.0";
	g.blockUI.params = {
		pageBlock: null,
		pageBlockEls: []
	};
	var h = document.documentMode || 0;
	var c = g.browser.msie && ((g.browser.version < 8 && !h) || h < 8);
	var d = g.browser.msie && /MSIE 6.0/.test(navigator.userAgent) && !h;
	//var k = c && (!g.boxModel || g("object,embed", full ? null: el).length > 0);
	var k = c;
	function e(q, o) {
		var C = q == window ? true: false;
		var E = (o && o.message !== undefined) ? o.message: g.blockUI.defaults.message;
		o = g.extend({},
		g.blockUI.defaults, o || {});
		if (C && g.blockUI.params.pageBlock) {
			f(window, {
				fadeOut: 0
			})
		}
		if (E && typeof(E) != "string" && (E.parentNode || E.jquery)) {
			var w = E.jquery ? E[0] : E;
			var B = {};
			g(q).data("blockUI.history", B);
			B.el = w;
			B.parent = w.parentNode;
			B.display = w.style.display;
			B.position = w.style.position;
			if (B.parent) {
				B.parent.removeChild(w)
			}
		}
		var D = o.baseZ;
		var y = g.browser.msie ? g('<iframe class="blockUI" id="JS_blockUI" style="z-index:' + (D++) + ';"></iframe>') : g('<div class="blockUI" id="JS_blockUI" style="z-index:' + (D++) + ';"></div>');
		var v = g('<div class="blockOverlay" id="JS_blockOverlay" style="z-index:' + (D++) + ';"></div>');
		var r = C ? g('<div class="blockMsg" id="JS_blockPage" style="z-index:' + D + ';position:fixed"></div>') : g('<div class="blockMsg" id="JS_blockElement" style="z-index:' + D + ';position:absolute"></div>');
		v.css(o.overlayCSS).css("position", C ? "fixed": "absolute");
		if (g.browser.msie || o.forceIframe) {
			y.css("opacity", 0)
		}
		var A = [y, v, r];
		var u = C ? g("body") : g(q);
		g.each(A,
		function() {
			this.appendTo(u)
		});
		if (d || k) {
			if (C && g.support.boxModel) {
				g("html,body").css("height", "100%")
			}
			if ((d || !g.boxModel) && !C) {
				var H = j(q, "borderTopWidth");
				var x = j(q, "borderLeftWidth");
				var G = H ? "(0 - " + H + ")": 0;
				var p = x ? "(0 - " + x + ")": 0
			}
			g.each([y, v, r],
			function(t, K) {

				var z = K[0].style;
				z.position = "absolute";
				if (t < 2) {
					C ? z.setExpression("height", "Math.max(document.body.scrollHeight, document.body.offsetHeight) - (jQuery.boxModel?0:" + o.quirksmodeOffsetHack + ') + "px"') : z.setExpression("height", 'this.parentNode.offsetHeight + "px"');
					C ? z.setExpression("width", 'jQuery.boxModel && document.documentElement.clientWidth || document.body.clientWidth + "px"') : z.setExpression("width", 'this.parentNode.offsetWidth + "px"');
					if (p) {
						z.setExpression("left", p)
					}
					if (G) {
						z.setExpression("top", G)
					}
				} else {

					if (o.centerY) {
						if (C) {
							z.setExpression("top", '(document.documentElement.clientHeight || document.body.clientHeight) / 2 - (this.offsetHeight / 2) + (blah = document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + "px"')
						}
						z.marginTop = 0
					} else {
						if (!o.centerY && C) {
							var I = 0;
							var J = "((document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + " + I + ') + "px"';
							z.setExpression("top", J)
						}
					}
				}
			})
		}
		if (E) {
			r.append(E);
			if (E.jquery || E.nodeType) {
				g(E).show()
			}
		}
		if (g.browser.msie && o.showOverlay) {
			y.show()
		}
		if (o.fadeInTime) {
			if (o.showOverlay) {
				v.fadeIn(o.fadeInTime)
			}
			if (E) {
				r.fadeIn(o.fadeInTime)
			}
		} else {
			if (o.showOverlay) {
				v.show()
			}
			if (E) {
				r.show()
			}
		}
		if (C) {
			l(r[0]);
			g.blockUI.params.pageBlock = r[0];
			g.blockUI.params.pageBlockEls = g(":input:enabled:visible", r[0]);
			if (o.focusInput) {
				setTimeout(n, 20)
			}
		} else {
			a(r[0], o.centerX, o.centerY)
		}
		if (o.timeout) {
			var F = setTimeout(function() {
				C ? g.unblockUI(o) : g(q).unblock(o)
			},
			o.timeout);
			g(q).data("blockUI.timeout", F)
		}
	}
    //点击确定后
	function f(r, t) {
		var q = r == window ? true: false;
		var p = g(r);
		var u = p.data("blockUI.history");
		var v = p.data("blockUI.timeout");
		t = g.extend({},
		g.blockUI.defaults, t || {});
		if (v) {
			clearTimeout(v);
			p.removeData("blockUI.timeout")
		}
		var o = q ? g("body").children().filter("[id^='JS_block']") : g("[id^='JS_block']", r);
		if (q) {
			g.blockUI.params.pageBlock = g.blockUI.params.pageBlockEls = null
		}
		if (t.fadeOutTime) {
			o.fadeOut(t.fadeOutTime);
			setTimeout(function() {
				i(o, u, t, r)
			},
			t.fadeOut)
		} else {
			i(o, u, t, r)
		}
	}
	function i(o, r, q, p) {

		o.each(function(t, u) {
			if (this.parentNode) {
				this.parentNode.removeChild(this)
			}
		});
		if (r && r.el) {
			r.el.style.display = r.display;
			r.el.style.position = r.position;
			if (r.parent) {
				r.parent.appendChild(r.el)
			}
			g(p).removeData("blockUI.history")
		}
		if (typeof(q.onUnblock) == "function") {
			q.onUnblock(p, q)
		}
	}
	function j(o, q) {
		return parseInt(g.css(o, q)) || 0
	}
	function a(v, o, z) {
		var w = v.parentNode,
		u = v.style;
		var q = ((w.offsetWidth - v.offsetWidth) / 2) - j(w, "borderLeftWidth");
		var r = ((w.offsetHeight - v.offsetHeight) / 2) - j(w, "borderTopWidth");
		if (o) {
			u.left = q > 0 ? (q + "px") : "0"
		}
		if (z) {
			u.top = r > 0 ? (r + "px") : "0"
		}
	}
	function n(o) {
		if (!g.blockUI.params.pageBlockEls) {
			return
		}
		var p = g.blockUI.params.pageBlockEls[o === true ? g.blockUI.params.pageBlockEls.length - 1 : 0];
		if (p) {
			p.focus()
		}
	}
    //计算位置
	function l(o) {
		var p = g(window).height() / 2 - g(o).height() / 2 + (d ? document.documentElement.scrollTop: 0);
		var q = g(window).width() / 2 - g(o).width() / 2 + (d ? document.documentElement.scrollLeft: 0);
		g(o).css({
			left: q + "px",
            top:p+"px"
		})
	}
	function b(q, p, o) {
		if (g.inArray(o, ["auto", "hidden", "inherit", "scroll", "visible"]) == -1) {
			o = "auto"
		}
		if (g(q).height() > p) {
			g(q).css({
				height: p,
				overflow: o,
				"overflow-x": "hidden"
			})
		}
	}
	g.fn.openFloat = function(u, o) {
		if (g("#JS_openFloat", g(this).parent()).length > 0) {
			g(this).closeFloat();
			return
		}
		var t = g(this).offset();
		var r = t.left;
		var q = t.top + g(this).height();
		var p = g('<div id="JS_openFloat" style="display:none;position:absolute;z-index:200;left:' + r + "px;top:" + q + 'px"></div>');
		p.addClass(o);
		p.append(u);
		if (u.jquery || u.nodeType) {
			g(u).show()
		}
		p.appendTo(g(this).parent());
		p.show("normal")
	};
	g.fn.closeFloat = function() {
		g("#JS_openFloat", g(this).parent()).remove()
	}
})(jQuery);