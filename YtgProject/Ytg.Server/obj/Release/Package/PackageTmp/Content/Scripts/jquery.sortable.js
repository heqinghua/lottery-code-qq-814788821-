(function(b, f) {
    var a = 0,
        e = /^ui-id-\d+$/;
    b.ui = b.ui || {};
    b.extend(b.ui, {
        version: "1.10.3",
        keyCode: {
            BACKSPACE: 8,
            COMMA: 188,
            DELETE: 46,
            DOWN: 40,
            END: 35,
            ENTER: 13,
            ESCAPE: 27,
            HOME: 36,
            LEFT: 37,
            NUMPAD_ADD: 107,
            NUMPAD_DECIMAL: 110,
            NUMPAD_DIVIDE: 111,
            NUMPAD_ENTER: 108,
            NUMPAD_MULTIPLY: 106,
            NUMPAD_SUBTRACT: 109,
            PAGE_DOWN: 34,
            PAGE_UP: 33,
            PERIOD: 190,
            RIGHT: 39,
            SPACE: 32,
            TAB: 9,
            UP: 38
        }
    });
    b.fn.extend({
        focus: (function(g) {
            return function(h, i) {
                return typeof h === "number" ? this.each(function() {
                    var j = this;
                    setTimeout(function() {
                        b(j).focus();
                        if (i) {
                            i.call(j)
                        }
                    }, h)
                }) : g.apply(this, arguments)
            }
        })(b.fn.focus),
        scrollParent: function() {
            var g;
            if ((b.ui.ie && (/(static|relative)/).test(this.css("position"))) || (/absolute/).test(this.css("position"))) {
                g = this.parents().filter(function() {
                    return (/(relative|absolute|fixed)/).test(b.css(this, "position")) && (/(auto|scroll)/).test(b.css(this, "overflow") + b.css(this, "overflow-y") + b.css(this, "overflow-x"))
                }).eq(0)
            } else {
                g = this.parents().filter(function() {
                    return (/(auto|scroll)/).test(b.css(this, "overflow") + b.css(this, "overflow-y") + b.css(this, "overflow-x"))
                }).eq(0)
            }
            return (/fixed/).test(this.css("position")) || !g.length ? b(document) : g
        },
        zIndex: function(j) {
            if (j !== f) {
                return this.css("zIndex", j)
            }
            if (this.length) {
                var h = b(this[0]),
                    g, i;
                while (h.length && h[0] !== document) {
                    g = h.css("position");
                    if (g === "absolute" || g === "relative" || g === "fixed") {
                        i = parseInt(h.css("zIndex"), 10);
                        if (!isNaN(i) && i !== 0) {
                            return i
                        }
                    }
                    h = h.parent()
                }
            }
            return 0
        },
        uniqueId: function() {
            return this.each(function() {
                if (!this.id) {
                    this.id = "ui-id-" + (++a)
                }
            })
        },
        removeUniqueId: function() {
            return this.each(function() {
                if (e.test(this.id)) {
                    b(this).removeAttr("id")
                }
            })
        }
    });


    b.extend(b.expr[":"], {
        data: b.expr.createPseudo ? b.expr.createPseudo(function(g) {
            return function(h) {
                return !!b.data(h, g)
            }
        }) : function(j, h, g) {
            return !!b.data(j, g[3])
        },
        focusable: function(g) {
            return d(g, !isNaN(b.attr(g, "tabindex")))
        },
        tabbable: function(i) {
            var g = b.attr(i, "tabindex"),
                h = isNaN(g);
            return (h || g >= 0) && d(i, !h)
        }
    });
    if (!b("<a>").outerWidth(1).jquery) {
        b.each(["Width", "Height"], function(j, g) {
            var h = g === "Width" ? ["Left", "Right"] : ["Top", "Bottom"],
                k = g.toLowerCase(),
                m = {
                    innerWidth: b.fn.innerWidth,
                    innerHeight: b.fn.innerHeight,
                    outerWidth: b.fn.outerWidth,
                    outerHeight: b.fn.outerHeight
                };

            function l(o, n, i, p) {
                b.each(h, function() {
                    n -= parseFloat(b.css(o, "padding" + this)) || 0;
                    if (i) {
                        n -= parseFloat(b.css(o, "border" + this + "Width")) || 0
                    }
                    if (p) {
                        n -= parseFloat(b.css(o, "margin" + this)) || 0
                    }
                });
                return n
            }
            b.fn["inner" + g] = function(i) {
                if (i === f) {
                    return m["inner" + g].call(this)
                }
                return this.each(function() {
                    b(this).css(k, l(this, i) + "px")
                })
            };
            b.fn["outer" + g] = function(i, n) {
                if (typeof i !== "number") {
                    return m["outer" + g].call(this, i)
                }
                return this.each(function() {
                    b(this).css(k, l(this, i, true, n) + "px")
                })
            }
        })
    }
    if (!b.fn.addBack) {
        b.fn.addBack = function(g) {
            return this.add(g == null ? this.prevObject : this.prevObject.filter(g))
        }
    }
    if (b("<a>").data("a-b", "a").removeData("a-b").data("a-b")) {
        b.fn.removeData = (function(g) {
            return function(h) {
                if (arguments.length) {
                    return g.call(this, b.camelCase(h))
                } else {
                    return g.call(this)
                }
            }
        })(b.fn.removeData)
    }
    b.ui.ie = !! /msie [\w.]+/.exec(navigator.userAgent.toLowerCase());
    b.support.selectstart = "onselectstart" in document.createElement("div");
    b.fn.extend({
        disableSelection: function() {
            return this.bind((b.support.selectstart ? "selectstart" : "mousedown") + ".ui-disableSelection", function(g) {
                g.preventDefault()
            })
        },
        enableSelection: function() {
            return this.unbind(".ui-disableSelection")
        }
    });
    b.extend(b.ui, {
        plugin: {
            add: function(h, j, l) {
                var g, k = b.ui[h].prototype;
                for (g in l) {
                    k.plugins[g] = k.plugins[g] || [];
                    k.plugins[g].push([j, l[g]])
                }
            },
            call: function(g, j, h) {
                var k, l = g.plugins[j];
                if (!l || !g.element[0].parentNode || g.element[0].parentNode.nodeType === 11) {
                    return
                }
                for (k = 0; k < l.length; k++) {
                    if (g.options[l[k][0]]) {
                        l[k][1].apply(g.element, h)
                    }
                }
            }
        },
        hasScroll: function(j, h) {
            if (b(j).css("overflow") === "hidden") {
                return false
            }
            var g = (h && h === "left") ? "scrollLeft" : "scrollTop",
                i = false;
            if (j[g] > 0) {
                return true
            }
            j[g] = 1;
            i = (j[g] > 0);
            j[g] = 0;
            return i
        }
    })
})(jQuery);
(function(b, e) {
    var a = 0,
        d = Array.prototype.slice,
        c = b.cleanData;
    b.cleanData = function(f) {
        for (var g = 0, h;
        (h = f[g]) != null; g++) {
            try {
                b(h).triggerHandler("remove")
            } catch (j) {}
        }
        c(f)
    };
    b.widget = function(f, g, n) {
        var k, l, i, m, h = {},
            j = f.split(".")[0];
        f = f.split(".")[1];
        k = j + "-" + f;
        if (!n) {
            n = g;
            g = b.Widget
        }
        b.expr[":"][k.toLowerCase()] = function(o) {
            return !!b.data(o, k)
        };
        b[j] = b[j] || {};
        l = b[j][f];
        i = b[j][f] = function(o, p) {
            if (!this._createWidget) {
                return new i(o, p)
            }
            if (arguments.length) {
                this._createWidget(o, p)
            }
        };
        b.extend(i, l, {
            version: n.version,
            _proto: b.extend({}, n),
            _childConstructors: []
        });
        m = new g();
        m.options = b.widget.extend({}, m.options);
        b.each(n, function(p, o) {
            if (!b.isFunction(o)) {
                h[p] = o;
                return
            }
            h[p] = (function() {
                var q = function() {
                    return g.prototype[p].apply(this, arguments)
                },
                    r = function(s) {
                        return g.prototype[p].apply(this, s)
                    };
                return function() {
                    var u = this._super,
                        s = this._superApply,
                        t;
                    this._super = q;
                    this._superApply = r;
                    t = o.apply(this, arguments);
                    this._super = u;
                    this._superApply = s;
                    return t
                }
            })()
        });
        i.prototype = b.widget.extend(m, {
            widgetEventPrefix: l ? m.widgetEventPrefix : f
        }, h, {
            constructor: i,
            namespace: j,
            widgetName: f,
            widgetFullName: k
        });
        if (l) {
            b.each(l._childConstructors, function(p, q) {
                var o = q.prototype;
                b.widget(o.namespace + "." + o.widgetName, i, q._proto)
            });
            delete l._childConstructors
        } else {
            g._childConstructors.push(i)
        }
        b.widget.bridge(f, i)
    };
    b.widget.extend = function(k) {
        var g = d.call(arguments, 1),
            j = 0,
            f = g.length,
            h, i;
        for (; j < f; j++) {
            for (h in g[j]) {
                i = g[j][h];
                if (g[j].hasOwnProperty(h) && i !== e) {
                    if (b.isPlainObject(i)) {
                        k[h] = b.isPlainObject(k[h]) ? b.widget.extend({}, k[h], i) : b.widget.extend({}, i)
                    } else {
                        k[h] = i
                    }
                }
            }
        }
        return k
    };
    b.widget.bridge = function(g, f) {
        var h = f.prototype.widgetFullName || g;
        b.fn[g] = function(k) {
            var i = typeof k === "string",
                j = d.call(arguments, 1),
                l = this;
            k = !i && j.length ? b.widget.extend.apply(null, [k].concat(j)) : k;
            if (i) {
                this.each(function() {
                    var n, m = b.data(this, h);
                    if (!m) {
                        return b.error("cannot call methods on " + g + " prior to initialization; attempted to call method '" + k + "'")
                    }
                    if (!b.isFunction(m[k]) || k.charAt(0) === "_") {
                        return b.error("no such method '" + k + "' for " + g + " widget instance")
                    }
                    n = m[k].apply(m, j);
                    if (n !== m && n !== e) {
                        l = n && n.jquery ? l.pushStack(n.get()) : n;
                        return false
                    }
                })
            } else {
                this.each(function() {
                    var m = b.data(this, h);
                    if (m) {
                        m.option(k || {})._init()
                    } else {
                        b.data(this, h, new f(k, this))
                    }
                })
            }
            return l
        }
    };
    b.Widget = function() {};
    b.Widget._childConstructors = [];
    b.Widget.prototype = {
        widgetName: "widget",
        widgetEventPrefix: "",
        defaultElement: "<div>",
        options: {
            disabled: false,
            create: null
        },
        _createWidget: function(f, g) {
            g = b(g || this.defaultElement || this)[0];
            this.element = b(g);
            this.uuid = a++;
            this.eventNamespace = "." + this.widgetName + this.uuid;
            this.options = b.widget.extend({}, this.options, this._getCreateOptions(), f);
            this.bindings = b();
            this.hoverable = b();
            this.focusable = b();
            if (g !== this) {
                b.data(g, this.widgetFullName, this);
                this._on(true, this.element, {
                    remove: function(h) {
                        if (h.target === g) {
                            this.destroy()
                        }
                    }
                });
                this.document = b(g.style ? g.ownerDocument : g.document || g);
                this.window = b(this.document[0].defaultView || this.document[0].parentWindow)
            }
            this._create();
            this._trigger("create", null, this._getCreateEventData());
            this._init()
        },
        _getCreateOptions: b.noop,
        _getCreateEventData: b.noop,
        _create: b.noop,
        _init: b.noop,
        destroy: function() {
            this._destroy();
            this.element.unbind(this.eventNamespace).removeData(this.widgetName).removeData(this.widgetFullName).removeData(b.camelCase(this.widgetFullName));
            this.widget().unbind(this.eventNamespace).removeAttr("aria-disabled").removeClass(this.widgetFullName + "-disabled ui-state-disabled");
            this.bindings.unbind(this.eventNamespace);
            this.hoverable.removeClass("ui-state-hover");
            this.focusable.removeClass("ui-state-focus")
        },
        _destroy: b.noop,
        widget: function() {
            return this.element
        },
        option: function(j, k) {
            var f = j,
                l, h, g;
            if (arguments.length === 0) {
                return b.widget.extend({}, this.options)
            }
            if (typeof j === "string") {
                f = {};
                l = j.split(".");
                j = l.shift();
                if (l.length) {
                    h = f[j] = b.widget.extend({}, this.options[j]);
                    for (g = 0; g < l.length - 1; g++) {
                        h[l[g]] = h[l[g]] || {};
                        h = h[l[g]]
                    }
                    j = l.pop();
                    if (k === e) {
                        return h[j] === e ? null : h[j]
                    }
                    h[j] = k
                } else {
                    if (k === e) {
                        return this.options[j] === e ? null : this.options[j]
                    }
                    f[j] = k
                }
            }
            this._setOptions(f);
            return this
        },
        _setOptions: function(f) {
            var g;
            for (g in f) {
                this._setOption(g, f[g])
            }
            return this
        },
        _setOption: function(f, g) {
            this.options[f] = g;
            if (f === "disabled") {
                this.widget().toggleClass(this.widgetFullName + "-disabled ui-state-disabled", !! g).attr("aria-disabled", g);
                this.hoverable.removeClass("ui-state-hover");
                this.focusable.removeClass("ui-state-focus")
            }
            return this
        },
        enable: function() {
            return this._setOption("disabled", false)
        },
        disable: function() {
            return this._setOption("disabled", true)
        },
        _on: function(i, h, g) {
            var j, f = this;
            if (typeof i !== "boolean") {
                g = h;
                h = i;
                i = false
            }
            if (!g) {
                g = h;
                h = this.element;
                j = this.widget()
            } else {
                h = j = b(h);
                this.bindings = this.bindings.add(h)
            }
            b.each(g, function(p, o) {
                function m() {
                    if (!i && (f.options.disabled === true || b(this).hasClass("ui-state-disabled"))) {
                        return
                    }
                    return (typeof o === "string" ? f[o] : o).apply(f, arguments)
                }
                if (typeof o !== "string") {
                    m.guid = o.guid = o.guid || m.guid || b.guid++
                }
                var n = p.match(/^(\w+)\s*(.*)$/),
                    l = n[1] + f.eventNamespace,
                    k = n[2];
                if (k) {
                    j.delegate(k, l, m)
                } else {
                    h.bind(l, m)
                }
            })
        },
        _off: function(g, f) {
            f = (f || "").split(" ").join(this.eventNamespace + " ") + this.eventNamespace;
            g.unbind(f).undelegate(f)
        },
        _delay: function(i, h) {
            function g() {
                return (typeof i === "string" ? f[i] : i).apply(f, arguments)
            }
            var f = this;
            return setTimeout(g, h || 0)
        },
        _hoverable: function(f) {
            this.hoverable = this.hoverable.add(f);
            this._on(f, {
                mouseenter: function(g) {
                    b(g.currentTarget).addClass("ui-state-hover")
                },
                mouseleave: function(g) {
                    b(g.currentTarget).removeClass("ui-state-hover")
                }
            })
        },
        _focusable: function(f) {
            this.focusable = this.focusable.add(f);
            this._on(f, {
                focusin: function(g) {
                    b(g.currentTarget).addClass("ui-state-focus")
                },
                focusout: function(g) {
                    b(g.currentTarget).removeClass("ui-state-focus")
                }
            })
        },
        _trigger: function(f, g, h) {
            var k, j, i = this.options[f];
            h = h || {};
            g = b.Event(g);
            g.type = (f === this.widgetEventPrefix ? f : this.widgetEventPrefix + f).toLowerCase();
            g.target = this.element[0];
            j = g.originalEvent;
            if (j) {
                for (k in j) {
                    if (!(k in g)) {
                        g[k] = j[k]
                    }
                }
            }
            this.element.trigger(g, h);
            return !(b.isFunction(i) && i.apply(this.element[0], [g].concat(h)) === false || g.isDefaultPrevented())
        }
    };
    b.each({
        show: "fadeIn",
        hide: "fadeOut"
    }, function(g, f) {
        b.Widget.prototype["_" + g] = function(j, i, l) {
            if (typeof i === "string") {
                i = {
                    effect: i
                }
            }
            var k, h = !i ? g : i === true || typeof i === "number" ? f : i.effect || f;
            i = i || {};
            if (typeof i === "number") {
                i = {
                    duration: i
                }
            }
            k = !b.isEmptyObject(i);
            i.complete = l;
            if (i.delay) {
                j.delay(i.delay)
            }
            if (k && b.effects && b.effects.effect[h]) {
                j[g](i)
            } else {
                if (h !== g && j[h]) {
                    j[h](i.duration, i.easing, l)
                } else {
                    j.queue(function(m) {
                        b(this)[g]();
                        if (l) {
                            l.call(j[0])
                        }
                        m()
                    })
                }
            }
        }
    })
})(jQuery);
(function(b, c) {
    var a = false;
    b(document).mouseup(function() {
        a = false
    });
    b.widget("ui.mouse", {
        version: "1.10.3",
        options: {
            cancel: "input,textarea,button,select,option",
            distance: 1,
            delay: 0
        },
        _mouseInit: function() {
            var d = this;
            this.element.bind("mousedown." + this.widgetName, function(e) {
                return d._mouseDown(e)
            }).bind("click." + this.widgetName, function(e) {
                if (true === b.data(e.target, d.widgetName + ".preventClickEvent")) {
                    b.removeData(e.target, d.widgetName + ".preventClickEvent");
                    e.stopImmediatePropagation();
                    return false
                }
            });
            this.started = false
        },
        _mouseDestroy: function() {
            this.element.unbind("." + this.widgetName);
            if (this._mouseMoveDelegate) {
                b(document).unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate)
            }
        },
        _mouseDown: function(f) {
            if (a) {
                return
            }(this._mouseStarted && this._mouseUp(f));
            this._mouseDownEvent = f;
            var e = this,
                g = (f.which === 1),
                d = (typeof this.options.cancel === "string" && f.target.nodeName ? b(f.target).closest(this.options.cancel).length : false);
            if (!g || d || !this._mouseCapture(f)) {
                return true
            }
            this.mouseDelayMet = !this.options.delay;
            if (!this.mouseDelayMet) {
                this._mouseDelayTimer = setTimeout(function() {
                    e.mouseDelayMet = true
                }, this.options.delay)
            }
            if (this._mouseDistanceMet(f) && this._mouseDelayMet(f)) {
                this._mouseStarted = (this._mouseStart(f) !== false);
                if (!this._mouseStarted) {
                    f.preventDefault();
                    return true
                }
            }
            if (true === b.data(f.target, this.widgetName + ".preventClickEvent")) {
                b.removeData(f.target, this.widgetName + ".preventClickEvent")
            }
            this._mouseMoveDelegate = function(h) {
                return e._mouseMove(h)
            };
            this._mouseUpDelegate = function(h) {
                return e._mouseUp(h)
            };
            b(document).bind("mousemove." + this.widgetName, this._mouseMoveDelegate).bind("mouseup." + this.widgetName, this._mouseUpDelegate);
            f.preventDefault();
            a = true;
            return true
        },
        _mouseMove: function(d) {
            if (b.ui.ie && (!document.documentMode || document.documentMode < 9) && !d.button) {
                return this._mouseUp(d)
            }
            if (this._mouseStarted) {
                this._mouseDrag(d);
                return d.preventDefault()
            }
            if (this._mouseDistanceMet(d) && this._mouseDelayMet(d)) {
                this._mouseStarted = (this._mouseStart(this._mouseDownEvent, d) !== false);
                (this._mouseStarted ? this._mouseDrag(d) : this._mouseUp(d))
            }
            return !this._mouseStarted
        },
        _mouseUp: function(d) {
            b(document).unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate);
            if (this._mouseStarted) {
                this._mouseStarted = false;
                if (d.target === this._mouseDownEvent.target) {
                    b.data(d.target, this.widgetName + ".preventClickEvent", true)
                }
                this._mouseStop(d)
            }
            return false
        },
        _mouseDistanceMet: function(d) {
            return (Math.max(Math.abs(this._mouseDownEvent.pageX - d.pageX), Math.abs(this._mouseDownEvent.pageY - d.pageY)) >= this.options.distance)
        },
        _mouseDelayMet: function() {
            return this.mouseDelayMet
        },
        _mouseStart: function() {},
        _mouseDrag: function() {},
        _mouseStop: function() {},
        _mouseCapture: function() {
            return true
        }
    })
})(jQuery);
(function(b, d) {
    function a(f, e, g) {
        return (f > e) && (f < (e + g))
    }
    function c(e) {
        return (/left|right/).test(e.css("float")) || (/inline|table-cell/).test(e.css("display"))
    }
    b.widget("ui.sortable", b.ui.mouse, {
        version: "1.10.3",
        widgetEventPrefix: "sort",
        ready: false,
        options: {
            appendTo: "parent",
            axis: false,
            connectWith: false,
            containment: false,
            cursor: "auto",
            cursorAt: false,
            dropOnEmpty: true,
            forcePlaceholderSize: false,
            forceHelperSize: false,
            grid: false,
            handle: false,
            helper: "original",
            items: "> *",
            opacity: false,
            placeholder: false,
            revert: false,
            scroll: true,
            scrollSensitivity: 20,
            scrollSpeed: 20,
            scope: "default",
            tolerance: "intersect",
            zIndex: 1000,
            activate: null,
            beforeStop: null,
            change: null,
            deactivate: null,
            out: null,
            over: null,
            receive: null,
            remove: null,
            sort: null,
            start: null,
            stop: null,
            update: null
        },
        _create: function() {
            var e = this.options;
            this.containerCache = {};
            this.element.addClass("ui-sortable");
            this.refresh();
            this.floating = this.items.length ? e.axis === "x" || c(this.items[0].item) : false;
            this.offset = this.element.offset();
            this._mouseInit();
            this.ready = true
        },
        _destroy: function() {
            this.element.removeClass("ui-sortable ui-sortable-disabled");
            this._mouseDestroy();
            for (var e = this.items.length - 1; e >= 0; e--) {
                this.items[e].item.removeData(this.widgetName + "-item")
            }
            return this
        },
        _setOption: function(e, f) {
            if (e === "disabled") {
                this.options[e] = f;
                this.widget().toggleClass("ui-sortable-disabled", !! f)
            } else {
                b.Widget.prototype._setOption.apply(this, arguments)
            }
        },
        _mouseCapture: function(g, h) {
            var e = null,
                i = false,
                f = this;
            if (this.reverting) {
                return false
            }
            if (this.options.disabled || this.options.type === "static") {
                return false
            }
            this._refreshItems(g);
            b(g.target).parents().each(function() {
                if (b.data(this, f.widgetName + "-item") === f) {
                    e = b(this);
                    return false
                }
            });
            if (b.data(g.target, f.widgetName + "-item") === f) {
                e = b(g.target)
            }
            if (!e) {
                return false
            }
            if (this.options.handle && !h) {
                b(this.options.handle, e).find("*").addBack().each(function() {
                    if (this === g.target) {
                        i = true
                    }
                });
                if (!i) {
                    return false
                }
            }
            this.currentItem = e;
            this._removeCurrentsFromItems();
            return true
        },
        _mouseStart: function(h, j, f) {
            var g, e, k = this.options;
            this.currentContainer = this;
            this.refreshPositions();
            this.helper = this._createHelper(h);
            this._cacheHelperProportions();
            this._cacheMargins();
            this.scrollParent = this.helper.scrollParent();
            this.offset = this.currentItem.offset();
            this.offset = {
                top: this.offset.top - this.margins.top,
                left: this.offset.left - this.margins.left
            };
            b.extend(this.offset, {
                click: {
                    left: h.pageX - this.offset.left,
                    top: h.pageY - this.offset.top
                },
                parent: this._getParentOffset(),
                relative: this._getRelativeOffset()
            });
            this.helper.css("position", "absolute");
            this.cssPosition = this.helper.css("position");
            this.originalPosition = this._generatePosition(h);
            this.originalPageX = h.pageX;
            this.originalPageY = h.pageY;
            (k.cursorAt && this._adjustOffsetFromHelper(k.cursorAt));
            this.domPosition = {
                prev: this.currentItem.prev()[0],
                parent: this.currentItem.parent()[0]
            };
            if (this.helper[0] !== this.currentItem[0]) {
                this.currentItem.hide()
            }
            this._createPlaceholder();
            if (k.containment) {
                this._setContainment()
            }
            if (k.cursor && k.cursor !== "auto") {
                e = this.document.find("body");
                this.storedCursor = e.css("cursor");
                e.css("cursor", k.cursor);
                this.storedStylesheet = b("<style>*{ cursor: " + k.cursor + " !important; }</style>").appendTo(e)
            }
            if (k.opacity) {
                if (this.helper.css("opacity")) {
                    this._storedOpacity = this.helper.css("opacity")
                }
                this.helper.css("opacity", k.opacity)
            }
            if (k.zIndex) {
                if (this.helper.css("zIndex")) {
                    this._storedZIndex = this.helper.css("zIndex")
                }
                this.helper.css("zIndex", k.zIndex)
            }
            if (this.scrollParent[0] !== document && this.scrollParent[0].tagName !== "HTML") {
                this.overflowOffset = this.scrollParent.offset()
            }
            this._trigger("start", h, this._uiHash());
            if (!this._preserveHelperProportions) {
                this._cacheHelperProportions()
            }
            if (!f) {
                for (g = this.containers.length - 1; g >= 0; g--) {
                    this.containers[g]._trigger("activate", h, this._uiHash(this))
                }
            }
            if (b.ui.ddmanager) {
                b.ui.ddmanager.current = this
            }
            if (b.ui.ddmanager && !k.dropBehaviour) {
                b.ui.ddmanager.prepareOffsets(this, h)
            }
            this.dragging = true;
            this.helper.addClass("ui-sortable-helper");
            this._mouseDrag(h);
            return true
        },
        _mouseDrag: function(j) {
            var g, h, f, l, k = this.options,
                e = false;
            this.position = this._generatePosition(j);
            this.positionAbs = this._convertPositionTo("absolute");
            if (!this.lastPositionAbs) {
                this.lastPositionAbs = this.positionAbs
            }
            if (this.options.scroll) {
                if (this.scrollParent[0] !== document && this.scrollParent[0].tagName !== "HTML") {
                    if ((this.overflowOffset.top + this.scrollParent[0].offsetHeight) - j.pageY < k.scrollSensitivity) {
                        this.scrollParent[0].scrollTop = e = this.scrollParent[0].scrollTop + k.scrollSpeed
                    } else {
                        if (j.pageY - this.overflowOffset.top < k.scrollSensitivity) {
                            this.scrollParent[0].scrollTop = e = this.scrollParent[0].scrollTop - k.scrollSpeed
                        }
                    }
                    if ((this.overflowOffset.left + this.scrollParent[0].offsetWidth) - j.pageX < k.scrollSensitivity) {
                        this.scrollParent[0].scrollLeft = e = this.scrollParent[0].scrollLeft + k.scrollSpeed
                    } else {
                        if (j.pageX - this.overflowOffset.left < k.scrollSensitivity) {
                            this.scrollParent[0].scrollLeft = e = this.scrollParent[0].scrollLeft - k.scrollSpeed
                        }
                    }
                } else {
                    if (j.pageY - b(document).scrollTop() < k.scrollSensitivity) {
                        e = b(document).scrollTop(b(document).scrollTop() - k.scrollSpeed)
                    } else {
                        if (b(window).height() - (j.pageY - b(document).scrollTop()) < k.scrollSensitivity) {
                            e = b(document).scrollTop(b(document).scrollTop() + k.scrollSpeed)
                        }
                    }
                    if (j.pageX - b(document).scrollLeft() < k.scrollSensitivity) {
                        e = b(document).scrollLeft(b(document).scrollLeft() - k.scrollSpeed)
                    } else {
                        if (b(window).width() - (j.pageX - b(document).scrollLeft()) < k.scrollSensitivity) {
                            e = b(document).scrollLeft(b(document).scrollLeft() + k.scrollSpeed)
                        }
                    }
                }
                if (e !== false && b.ui.ddmanager && !k.dropBehaviour) {
                    b.ui.ddmanager.prepareOffsets(this, j)
                }
            }
            this.positionAbs = this._convertPositionTo("absolute");
            if (!this.options.axis || this.options.axis !== "y") {
                this.helper[0].style.left = this.position.left + "px"
            }
            if (!this.options.axis || this.options.axis !== "x") {
                this.helper[0].style.top = this.position.top + "px"
            }
            for (g = this.items.length - 1; g >= 0; g--) {
                h = this.items[g];
                f = h.item[0];
                l = this._intersectsWithPointer(h);
                if (!l) {
                    continue
                }
                if (h.instance !== this.currentContainer) {
                    continue
                }
                if (f !== this.currentItem[0] && this.placeholder[l === 1 ? "next" : "prev"]()[0] !== f && !b.contains(this.placeholder[0], f) && (this.options.type === "semi-dynamic" ? !b.contains(this.element[0], f) : true)) {
                    this.direction = l === 1 ? "down" : "up";
                    if (this.options.tolerance === "pointer" || this._intersectsWithSides(h)) {
                        this._rearrange(j, h)
                    } else {
                        break
                    }
                    this._trigger("change", j, this._uiHash());
                    break
                }
            }
            this._contactContainers(j);
            if (b.ui.ddmanager) {
                b.ui.ddmanager.drag(this, j)
            }
            this._trigger("sort", j, this._uiHash());
            this.lastPositionAbs = this.positionAbs;
            return false
        },
        _mouseStop: function(g, i) {
            if (!g) {
                return
            }
            if (b.ui.ddmanager && !this.options.dropBehaviour) {
                b.ui.ddmanager.drop(this, g)
            }
            if (this.options.revert) {
                var f = this,
                    j = this.placeholder.offset(),
                    e = this.options.axis,
                    h = {};
                if (!e || e === "x") {
                    h.left = j.left - this.offset.parent.left - this.margins.left + (this.offsetParent[0] === document.body ? 0 : this.offsetParent[0].scrollLeft)
                }
                if (!e || e === "y") {
                    h.top = j.top - this.offset.parent.top - this.margins.top + (this.offsetParent[0] === document.body ? 0 : this.offsetParent[0].scrollTop)
                }
                this.reverting = true;
                b(this.helper).animate(h, parseInt(this.options.revert, 10) || 500, function() {
                    f._clear(g)
                })
            } else {
                this._clear(g, i)
            }
			/* remeber cookie */
			var str = '';
			$(".menu_list").find("li").each(function(i) {
				str += $(this).html() + ",";
			});
			//setCookie("play_mode", str, -1);
            return false
        },
        cancel: function() {
            if (this.dragging) {
                this._mouseUp({
                    target: null
                });
                if (this.options.helper === "original") {
                    this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper")
                } else {
                    this.currentItem.show()
                }
                for (var e = this.containers.length - 1; e >= 0; e--) {
                    this.containers[e]._trigger("deactivate", null, this._uiHash(this));
                    if (this.containers[e].containerCache.over) {
                        this.containers[e]._trigger("out", null, this._uiHash(this));
                        this.containers[e].containerCache.over = 0
                    }
                }
            }
            if (this.placeholder) {
                if (this.placeholder[0].parentNode) {
                    this.placeholder[0].parentNode.removeChild(this.placeholder[0])
                }
                if (this.options.helper !== "original" && this.helper && this.helper[0].parentNode) {
                    this.helper.remove()
                }
                b.extend(this, {
                    helper: null,
                    dragging: false,
                    reverting: false,
                    _noFinalSort: null
                });
                if (this.domPosition.prev) {
                    b(this.domPosition.prev).after(this.currentItem)
                } else {
                    b(this.domPosition.parent).prepend(this.currentItem)
                }
            }
            return this
        },
        serialize: function(g) {
            var e = this._getItemsAsjQuery(g && g.connected),
                f = [];
            g = g || {};
            b(e).each(function() {
                var h = (b(g.item || this).attr(g.attribute || "id") || "").match(g.expression || (/(.+)[\-=_](.+)/));
                if (h) {
                    f.push((g.key || h[1] + "[]") + "=" + (g.key && g.expression ? h[1] : h[2]))
                }
            });
            if (!f.length && g.key) {
                f.push(g.key + "=")
            }
            return f.join("&")
        },
        toArray: function(g) {
            var e = this._getItemsAsjQuery(g && g.connected),
                f = [];
            g = g || {};
            e.each(function() {
                f.push(b(g.item || this).attr(g.attribute || "id") || "")
            });
            return f
        },
        _intersectsWith: function(q) {
            var g = this.positionAbs.left,
                f = g + this.helperProportions.width,
                o = this.positionAbs.top,
                n = o + this.helperProportions.height,
                h = q.left,
                e = h + q.width,
                s = q.top,
                m = s + q.height,
                u = this.offset.click.top,
                k = this.offset.click.left,
                j = (this.options.axis === "x") || ((o + u) > s && (o + u) < m),
                p = (this.options.axis === "y") || ((g + k) > h && (g + k) < e),
                i = j && p;
            if (this.options.tolerance === "pointer" || this.options.forcePointerForContainers || (this.options.tolerance !== "pointer" && this.helperProportions[this.floating ? "width" : "height"] > q[this.floating ? "width" : "height"])) {
                return i
            } else {
                return (h < g + (this.helperProportions.width / 2) && f - (this.helperProportions.width / 2) < e && s < o + (this.helperProportions.height / 2) && n - (this.helperProportions.height / 2) < m)
            }
        },
        _intersectsWithPointer: function(g) {
            var h = (this.options.axis === "x") || a(this.positionAbs.top + this.offset.click.top, g.top, g.height),
                f = (this.options.axis === "y") || a(this.positionAbs.left + this.offset.click.left, g.left, g.width),
                j = h && f,
                e = this._getDragVerticalDirection(),
                i = this._getDragHorizontalDirection();
            if (!j) {
                return false
            }
            return this.floating ? (((i && i === "right") || e === "down") ? 2 : 1) : (e && (e === "down" ? 2 : 1))
        },
        _intersectsWithSides: function(h) {
            var f = a(this.positionAbs.top + this.offset.click.top, h.top + (h.height / 2), h.height),
                g = a(this.positionAbs.left + this.offset.click.left, h.left + (h.width / 2), h.width),
                e = this._getDragVerticalDirection(),
                i = this._getDragHorizontalDirection();
            if (this.floating && i) {
                return ((i === "right" && g) || (i === "left" && !g))
            } else {
                return e && ((e === "down" && f) || (e === "up" && !f))
            }
        },
        _getDragVerticalDirection: function() {
            var e = this.positionAbs.top - this.lastPositionAbs.top;
            return e !== 0 && (e > 0 ? "down" : "up")
        },
        _getDragHorizontalDirection: function() {
            var e = this.positionAbs.left - this.lastPositionAbs.left;
            return e !== 0 && (e > 0 ? "right" : "left")
        },
        refresh: function(e) {
            this._refreshItems(e);
            this.refreshPositions();
            return this
        },
        _connectWith: function() {
            var e = this.options;
            return e.connectWith.constructor === String ? [e.connectWith] : e.connectWith
        },
        _getItemsAsjQuery: function(l) {
            var h, g, n, m, e = [],
                f = [],
                k = this._connectWith();
            if (k && l) {
                for (h = k.length - 1; h >= 0; h--) {
                    n = b(k[h]);
                    for (g = n.length - 1; g >= 0; g--) {
                        m = b.data(n[g], this.widgetFullName);
                        if (m && m !== this && !m.options.disabled) {
                            f.push([b.isFunction(m.options.items) ? m.options.items.call(m.element) : b(m.options.items, m.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), m])
                        }
                    }
                }
            }
            f.push([b.isFunction(this.options.items) ? this.options.items.call(this.element, null, {
                options: this.options,
                item: this.currentItem
            }) : b(this.options.items, this.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), this]);
            for (h = f.length - 1; h >= 0; h--) {
                f[h][0].each(function() {
                    e.push(this)
                })
            }
            return b(e)
        },
        _removeCurrentsFromItems: function() {
            var e = this.currentItem.find(":data(" + this.widgetName + "-item)");
            this.items = b.grep(this.items, function(g) {
                for (var f = 0; f < e.length; f++) {
                    if (e[f] === g.item[0]) {
                        return false
                    }
                }
                return true
            })
        },
        _refreshItems: function(e) {
            this.items = [];
            this.containers = [this];
            var k, g, p, l, o, f, r, q, m = this.items,
                h = [
                    [b.isFunction(this.options.items) ? this.options.items.call(this.element[0], e, {
                        item: this.currentItem
                    }) : b(this.options.items, this.element), this]
                ],
                n = this._connectWith();
            if (n && this.ready) {
                for (k = n.length - 1; k >= 0; k--) {
                    p = b(n[k]);
                    for (g = p.length - 1; g >= 0; g--) {
                        l = b.data(p[g], this.widgetFullName);
                        if (l && l !== this && !l.options.disabled) {
                            h.push([b.isFunction(l.options.items) ? l.options.items.call(l.element[0], e, {
                                item: this.currentItem
                            }) : b(l.options.items, l.element), l]);
                            this.containers.push(l)
                        }
                    }
                }
            }
            for (k = h.length - 1; k >= 0; k--) {
                o = h[k][1];
                f = h[k][0];
                for (g = 0, q = f.length; g < q; g++) {
                    r = b(f[g]);
                    r.data(this.widgetName + "-item", o);
                    m.push({
                        item: r,
                        instance: o,
                        width: 0,
                        height: 0,
                        left: 0,
                        top: 0
                    })
                }
            }
        },
        refreshPositions: function(e) {
            if (this.offsetParent && this.helper) {
                this.offset.parent = this._getParentOffset()
            }
            var g, h, f, j;
            for (g = this.items.length - 1; g >= 0; g--) {
                h = this.items[g];
                if (h.instance !== this.currentContainer && this.currentContainer && h.item[0] !== this.currentItem[0]) {
                    continue
                }
                f = this.options.toleranceElement ? b(this.options.toleranceElement, h.item) : h.item;
                if (!e) {
                    h.width = f.outerWidth();
                    h.height = f.outerHeight()
                }
                j = f.offset();
                h.left = j.left;
                h.top = j.top
            }
            if (this.options.custom && this.options.custom.refreshContainers) {
                this.options.custom.refreshContainers.call(this)
            } else {
                for (g = this.containers.length - 1; g >= 0; g--) {
                    j = this.containers[g].element.offset();
                    this.containers[g].containerCache.left = j.left;
                    this.containers[g].containerCache.top = j.top;
                    this.containers[g].containerCache.width = this.containers[g].element.outerWidth();
                    this.containers[g].containerCache.height = this.containers[g].element.outerHeight()
                }
            }
            return this
        },
        _createPlaceholder: function(f) {
            f = f || this;
            var e, g = f.options;
            if (!g.placeholder || g.placeholder.constructor === String) {
                e = g.placeholder;
                g.placeholder = {
                    element: function() {
                        var i = f.currentItem[0].nodeName.toLowerCase(),
                            h = b("<" + i + ">", f.document[0]).addClass(e || f.currentItem[0].className + " ui-sortable-placeholder").removeClass("ui-sortable-helper");
                        if (i === "tr") {
                            f.currentItem.children().each(function() {
                                b("<td> </td>", f.document[0]).attr("colspan", b(this).attr("colspan") || 1).appendTo(h)
                            })
                        } else {
                            if (i === "img") {
                                h.attr("src", f.currentItem.attr("src"))
                            }
                        }
                        if (!e) {
                            h.css("visibility", "hidden")
                        }
                        return h
                    },
                    update: function(h, i) {
                        if (e && !g.forcePlaceholderSize) {
                            return
                        }
                        if (!i.height()) {
                            i.height(f.currentItem.innerHeight() - parseInt(f.currentItem.css("paddingTop") || 0, 10) - parseInt(f.currentItem.css("paddingBottom") || 0, 10))
                        }
                        if (!i.width()) {
                            i.width(f.currentItem.innerWidth() - parseInt(f.currentItem.css("paddingLeft") || 0, 10) - parseInt(f.currentItem.css("paddingRight") || 0, 10))
                        }
                    }
                }
            }
            f.placeholder = b(g.placeholder.element.call(f.element, f.currentItem));
            f.currentItem.after(f.placeholder);
            g.placeholder.update(f, f.placeholder)
        },
        _contactContainers: function(e) {
            var l, h, p, m, n, r, f, s, k, o, g = null,
                q = null;
            for (l = this.containers.length - 1; l >= 0; l--) {
                if (b.contains(this.currentItem[0], this.containers[l].element[0])) {
                    continue
                }
                if (this._intersectsWith(this.containers[l].containerCache)) {
                    if (g && b.contains(this.containers[l].element[0], g.element[0])) {
                        continue
                    }
                    g = this.containers[l];
                    q = l
                } else {
                    if (this.containers[l].containerCache.over) {
                        this.containers[l]._trigger("out", e, this._uiHash(this));
                        this.containers[l].containerCache.over = 0
                    }
                }
            }
            if (!g) {
                return
            }
            if (this.containers.length === 1) {
                if (!this.containers[q].containerCache.over) {
                    this.containers[q]._trigger("over", e, this._uiHash(this));
                    this.containers[q].containerCache.over = 1
                }
            } else {
                p = 10000;
                m = null;
                o = g.floating || c(this.currentItem);
                n = o ? "left" : "top";
                r = o ? "width" : "height";
                f = this.positionAbs[n] + this.offset.click[n];
                for (h = this.items.length - 1; h >= 0; h--) {
                    if (!b.contains(this.containers[q].element[0], this.items[h].item[0])) {
                        continue
                    }
                    if (this.items[h].item[0] === this.currentItem[0]) {
                        continue
                    }
                    if (o && !a(this.positionAbs.top + this.offset.click.top, this.items[h].top, this.items[h].height)) {
                        continue
                    }
                    s = this.items[h].item.offset()[n];
                    k = false;
                    if (Math.abs(s - f) > Math.abs(s + this.items[h][r] - f)) {
                        k = true;
                        s += this.items[h][r]
                    }
                    if (Math.abs(s - f) < p) {
                        p = Math.abs(s - f);
                        m = this.items[h];
                        this.direction = k ? "up" : "down"
                    }
                }
                if (!m && !this.options.dropOnEmpty) {
                    return
                }
                if (this.currentContainer === this.containers[q]) {
                    return
                }
                m ? this._rearrange(e, m, null, true) : this._rearrange(e, null, this.containers[q].element, true);
                this._trigger("change", e, this._uiHash());
                this.containers[q]._trigger("change", e, this._uiHash(this));
                this.currentContainer = this.containers[q];
                this.options.placeholder.update(this.currentContainer, this.placeholder);
                this.containers[q]._trigger("over", e, this._uiHash(this));
                this.containers[q].containerCache.over = 1
            }
        },
        _createHelper: function(f) {
            var g = this.options,
                e = b.isFunction(g.helper) ? b(g.helper.apply(this.element[0], [f, this.currentItem])) : (g.helper === "clone" ? this.currentItem.clone() : this.currentItem);
            if (!e.parents("body").length) {
                b(g.appendTo !== "parent" ? g.appendTo : this.currentItem[0].parentNode)[0].appendChild(e[0])
            }
            if (e[0] === this.currentItem[0]) {
                this._storedCSS = {
                    width: this.currentItem[0].style.width,
                    height: this.currentItem[0].style.height,
                    position: this.currentItem.css("position"),
                    top: this.currentItem.css("top"),
                    left: this.currentItem.css("left")
                }
            }
            if (!e[0].style.width || g.forceHelperSize) {
                e.width(this.currentItem.width())
            }
            if (!e[0].style.height || g.forceHelperSize) {
                e.height(this.currentItem.height())
            }
            return e
        },
        _adjustOffsetFromHelper: function(e) {
            if (typeof e === "string") {
                e = e.split(" ")
            }
            if (b.isArray(e)) {
                e = {
                    left: +e[0],
                    top: +e[1] || 0
                }
            }
            if ("left" in e) {
                this.offset.click.left = e.left + this.margins.left
            }
            if ("right" in e) {
                this.offset.click.left = this.helperProportions.width - e.right + this.margins.left
            }
            if ("top" in e) {
                this.offset.click.top = e.top + this.margins.top
            }
            if ("bottom" in e) {
                this.offset.click.top = this.helperProportions.height - e.bottom + this.margins.top
            }
        },
        _getParentOffset: function() {
            this.offsetParent = this.helper.offsetParent();
            var e = this.offsetParent.offset();
            if (this.cssPosition === "absolute" && this.scrollParent[0] !== document && b.contains(this.scrollParent[0], this.offsetParent[0])) {
                e.left += this.scrollParent.scrollLeft();
                e.top += this.scrollParent.scrollTop()
            }
            if (this.offsetParent[0] === document.body || (this.offsetParent[0].tagName && this.offsetParent[0].tagName.toLowerCase() === "html" && b.ui.ie)) {
                e = {
                    top: 0,
                    left: 0
                }
            }
            return {
                top: e.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0),
                left: e.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0)
            }
        },
        _getRelativeOffset: function() {
            if (this.cssPosition === "relative") {
                var e = this.currentItem.position();
                return {
                    top: e.top - (parseInt(this.helper.css("top"), 10) || 0) + this.scrollParent.scrollTop(),
                    left: e.left - (parseInt(this.helper.css("left"), 10) || 0) + this.scrollParent.scrollLeft()
                }
            } else {
                return {
                    top: 0,
                    left: 0
                }
            }
        },
        _cacheMargins: function() {
            this.margins = {
                left: (parseInt(this.currentItem.css("marginLeft"), 10) || 0),
                top: (parseInt(this.currentItem.css("marginTop"), 10) || 0)
            }
        },
        _cacheHelperProportions: function() {
            this.helperProportions = {
                width: this.helper.outerWidth(),
                height: this.helper.outerHeight()
            }
        },
        _setContainment: function() {
            var f, h, e, g = this.options;
            if (g.containment === "parent") {
                g.containment = this.helper[0].parentNode
            }
            if (g.containment === "document" || g.containment === "window") {
                this.containment = [0 - this.offset.relative.left - this.offset.parent.left, 0 - this.offset.relative.top - this.offset.parent.top, b(g.containment === "document" ? document : window).width() - this.helperProportions.width - this.margins.left, (b(g.containment === "document" ? document : window).height() || document.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]
            }
            if (!(/^(document|window|parent)$/).test(g.containment)) {
                f = b(g.containment)[0];
                h = b(g.containment).offset();
                e = (b(f).css("overflow") !== "hidden");
                this.containment = [h.left + (parseInt(b(f).css("borderLeftWidth"), 10) || 0) + (parseInt(b(f).css("paddingLeft"), 10) || 0) - this.margins.left, h.top + (parseInt(b(f).css("borderTopWidth"), 10) || 0) + (parseInt(b(f).css("paddingTop"), 10) || 0) - this.margins.top, h.left + (e ? Math.max(f.scrollWidth, f.offsetWidth) : f.offsetWidth) - (parseInt(b(f).css("borderLeftWidth"), 10) || 0) - (parseInt(b(f).css("paddingRight"), 10) || 0) - this.helperProportions.width - this.margins.left, h.top + (e ? Math.max(f.scrollHeight, f.offsetHeight) : f.offsetHeight) - (parseInt(b(f).css("borderTopWidth"), 10) || 0) - (parseInt(b(f).css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top]
            }
        },
        _convertPositionTo: function(g, i) {
            if (!i) {
                i = this.position
            }
            var f = g === "absolute" ? 1 : -1,
                e = this.cssPosition === "absolute" && !(this.scrollParent[0] !== document && b.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent,
                h = (/(html|body)/i).test(e[0].tagName);
            return {
                top: (i.top + this.offset.relative.top * f + this.offset.parent.top * f - ((this.cssPosition === "fixed" ? -this.scrollParent.scrollTop() : (h ? 0 : e.scrollTop())) * f)),
                left: (i.left + this.offset.relative.left * f + this.offset.parent.left * f - ((this.cssPosition === "fixed" ? -this.scrollParent.scrollLeft() : h ? 0 : e.scrollLeft()) * f))
            }
        },
        _generatePosition: function(h) {
            var j, i, k = this.options,
                g = h.pageX,
                f = h.pageY,
                e = this.cssPosition === "absolute" && !(this.scrollParent[0] !== document && b.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent,
                l = (/(html|body)/i).test(e[0].tagName);
            if (this.cssPosition === "relative" && !(this.scrollParent[0] !== document && this.scrollParent[0] !== this.offsetParent[0])) {
                this.offset.relative = this._getRelativeOffset()
            }
            if (this.originalPosition) {
                if (this.containment) {
                    if (h.pageX - this.offset.click.left < this.containment[0]) {
                        g = this.containment[0] + this.offset.click.left
                    }
                    if (h.pageY - this.offset.click.top < this.containment[1]) {
                        f = this.containment[1] + this.offset.click.top
                    }
                    if (h.pageX - this.offset.click.left > this.containment[2]) {
                        g = this.containment[2] + this.offset.click.left
                    }
                    if (h.pageY - this.offset.click.top > this.containment[3]) {
                        f = this.containment[3] + this.offset.click.top
                    }
                }
                if (k.grid) {
                    j = this.originalPageY + Math.round((f - this.originalPageY) / k.grid[1]) * k.grid[1];
                    f = this.containment ? ((j - this.offset.click.top >= this.containment[1] && j - this.offset.click.top <= this.containment[3]) ? j : ((j - this.offset.click.top >= this.containment[1]) ? j - k.grid[1] : j + k.grid[1])) : j;
                    i = this.originalPageX + Math.round((g - this.originalPageX) / k.grid[0]) * k.grid[0];
                    g = this.containment ? ((i - this.offset.click.left >= this.containment[0] && i - this.offset.click.left <= this.containment[2]) ? i : ((i - this.offset.click.left >= this.containment[0]) ? i - k.grid[0] : i + k.grid[0])) : i
                }
            }
            return {
                top: (f - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + ((this.cssPosition === "fixed" ? -this.scrollParent.scrollTop() : (l ? 0 : e.scrollTop())))),
                left: (g - this.offset.click.left - this.offset.relative.left - this.offset.parent.left + ((this.cssPosition === "fixed" ? -this.scrollParent.scrollLeft() : l ? 0 : e.scrollLeft())))
            }
        },
        _rearrange: function(j, h, f, g) {
            f ? f[0].appendChild(this.placeholder[0]) : h.item[0].parentNode.insertBefore(this.placeholder[0], (this.direction === "down" ? h.item[0] : h.item[0].nextSibling));
            this.counter = this.counter ? ++this.counter : 1;
            var e = this.counter;
            this._delay(function() {
                if (e === this.counter) {
                    this.refreshPositions(!g)
                }
            })
        },
        _clear: function(f, g) {
            this.reverting = false;
            var e, h = [];
            if (!this._noFinalSort && this.currentItem.parent().length) {
                this.placeholder.before(this.currentItem)
            }
            this._noFinalSort = null;
            if (this.helper[0] === this.currentItem[0]) {
                for (e in this._storedCSS) {
                    if (this._storedCSS[e] === "auto" || this._storedCSS[e] === "static") {
                        this._storedCSS[e] = ""
                    }
                }
                this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper")
            } else {
                this.currentItem.show()
            }
            if (this.fromOutside && !g) {
                h.push(function(i) {
                    this._trigger("receive", i, this._uiHash(this.fromOutside))
                })
            }
            if ((this.fromOutside || this.domPosition.prev !== this.currentItem.prev().not(".ui-sortable-helper")[0] || this.domPosition.parent !== this.currentItem.parent()[0]) && !g) {
                h.push(function(i) {
                    this._trigger("update", i, this._uiHash())
                })
            }
            if (this !== this.currentContainer) {
                if (!g) {
                    h.push(function(i) {
                        this._trigger("remove", i, this._uiHash())
                    });
                    h.push((function(i) {
                        return function(j) {
                            i._trigger("receive", j, this._uiHash(this))
                        }
                    }).call(this, this.currentContainer));
                    h.push((function(i) {
                        return function(j) {
                            i._trigger("update", j, this._uiHash(this))
                        }
                    }).call(this, this.currentContainer))
                }
            }
            for (e = this.containers.length - 1; e >= 0; e--) {
                if (!g) {
                    h.push((function(i) {
                        return function(j) {
                            i._trigger("deactivate", j, this._uiHash(this))
                        }
                    }).call(this, this.containers[e]))
                }
                if (this.containers[e].containerCache.over) {
                    h.push((function(i) {
                        return function(j) {
                            i._trigger("out", j, this._uiHash(this))
                        }
                    }).call(this, this.containers[e]));
                    this.containers[e].containerCache.over = 0
                }
            }
            if (this.storedCursor) {
                this.document.find("body").css("cursor", this.storedCursor);
                this.storedStylesheet.remove()
            }
            if (this._storedOpacity) {
                this.helper.css("opacity", this._storedOpacity)
            }
            if (this._storedZIndex) {
                this.helper.css("zIndex", this._storedZIndex === "auto" ? "" : this._storedZIndex)
            }
            this.dragging = false;
            if (this.cancelHelperRemoval) {
                if (!g) {
                    this._trigger("beforeStop", f, this._uiHash());
                    for (e = 0; e < h.length; e++) {
                        h[e].call(this, f)
                    }
                    this._trigger("stop", f, this._uiHash())
                }
                this.fromOutside = false;
                return false
            }
            if (!g) {
                this._trigger("beforeStop", f, this._uiHash())
            }
            this.placeholder[0].parentNode.removeChild(this.placeholder[0]);
            if (this.helper[0] !== this.currentItem[0]) {
                this.helper.remove()
            }
            this.helper = null;
            if (!g) {
                for (e = 0; e < h.length; e++) {
                    h[e].call(this, f)
                }
                this._trigger("stop", f, this._uiHash())
            }
            this.fromOutside = false;
            return true
        },
        _trigger: function() {
            if (b.Widget.prototype._trigger.apply(this, arguments) === false) {
                this.cancel()
            }
        },
        _uiHash: function(e) {
            var f = e || this;
            return {
                helper: f.helper,
                placeholder: f.placeholder || b([]),
                position: f.position,
                originalPosition: f.originalPosition,
                offset: f.positionAbs,
                item: f.currentItem,
                sender: e ? e.element : null
            }
        }
    })
})(jQuery);
(function(a, c) {
    var b = "ui-effects-";
    a.effects = {
        effect: {}
    };
    (function(r, g) {
        var n = "backgroundColor borderBottomColor borderLeftColor borderRightColor borderTopColor color columnRuleColor outlineColor textDecorationColor textEmphasisColor",
            k = /^([\-+])=\s*(\d+\.?\d*)/,
            j = [{
                re: /rgba?\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
                parse: function(s) {
                    return [s[1], s[2], s[3], s[4]]
                }
            },
            {
                re: /rgba?\(\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
                parse: function(s) {
                    return [s[1] * 2.55, s[2] * 2.55, s[3] * 2.55, s[4]]
                }
            },
            {
                re: /#([a-f0-9]{2})([a-f0-9]{2})([a-f0-9]{2})/,
                parse: function(s) {
                    return [parseInt(s[1], 16), parseInt(s[2], 16), parseInt(s[3], 16)]
                }
            },
            {
                re: /#([a-f0-9])([a-f0-9])([a-f0-9])/,
                parse: function(s) {
                    return [parseInt(s[1] + s[1], 16), parseInt(s[2] + s[2], 16), parseInt(s[3] + s[3], 16)]
                }
            },
            {
                re: /hsla?\(\s*(\d+(?:\.\d+)?)\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,
                space: "hsla",
                parse: function(s) {
                    return [s[1], s[2] / 100, s[3] / 100, s[4]]
                }
            }],
            h = r.Color = function(t, u, s, v) {
                return new r.Color.fn.parse(t, u, s, v)
            },
            m = {
                rgba: {
                    props: {
                        red: {
                            idx: 0,
                            type: "byte"
                        },
                        green: {
                            idx: 1,
                            type: "byte"
                        },
                        blue: {
                            idx: 2,
                            type: "byte"
                        }
                    }
                },
                hsla: {
                    props: {
                        hue: {
                            idx: 0,
                            type: "degrees"
                        },
                        saturation: {
                            idx: 1,
                            type: "percent"
                        },
                        lightness: {
                            idx: 2,
                            type: "percent"
                        }
                    }
                }
            },
            q = {
                "byte": {
                    floor: true,
                    max: 255
                },
                percent: {
                    max: 1
                },
                degrees: {
                    mod: 360,
                    floor: true
                }
            },
            p = h.support = {},
            e = r("<p>")[0],
            d, o = r.each;
        e.style.cssText = "background-color:rgba(1,1,1,.5)";
        p.rgba = e.style.backgroundColor.indexOf("rgba") > -1;
        o(m, function(s, t) {
            t.cache = "_" + s;
            t.props.alpha = {
                idx: 3,
                type: "percent",
                def: 1
            }
        });

        function l(t, v, u) {
            var s = q[v.type] || {};
            if (t == null) {
                return (u || !v.def) ? null : v.def
            }
            t = s.floor ? ~~t : parseFloat(t);
            if (isNaN(t)) {
                return v.def
            }
            if (s.mod) {
                return (t + s.mod) % s.mod
            }
            return 0 > t ? 0 : s.max < t ? s.max : t
        }
        function i(s) {
            var u = h(),
                t = u._rgba = [];
            s = s.toLowerCase();
            o(j, function(z, A) {
                var x, y = A.re.exec(s),
                    w = y && A.parse(y),
                    v = A.space || "rgba";
                if (w) {
                    x = u[v](w);
                    u[m[v].cache] = x[m[v].cache];
                    t = u._rgba = x._rgba;
                    return false
                }
            });
            if (t.length) {
                if (t.join() === "0,0,0,0") {
                    r.extend(t, d.transparent)
                }
                return u
            }
            return d[s]
        }
        h.fn = r.extend(h.prototype, {
            parse: function(y, w, s, x) {
                if (y === g) {
                    this._rgba = [null, null, null, null];
                    return this
                }
                if (y.jquery || y.nodeType) {
                    y = r(y).css(w);
                    w = g
                }
                var v = this,
                    u = r.type(y),
                    t = this._rgba = [];
                if (w !== g) {
                    y = [y, w, s, x];
                    u = "array"
                }
                if (u === "string") {
                    return this.parse(i(y) || d._default)
                }
                if (u === "array") {
                    o(m.rgba.props, function(z, A) {
                        t[A.idx] = l(y[A.idx], A)
                    });
                    return this
                }
                if (u === "object") {
                    if (y instanceof h) {
                        o(m, function(z, A) {
                            if (y[A.cache]) {
                                v[A.cache] = y[A.cache].slice()
                            }
                        })
                    } else {
                        o(m, function(A, B) {
                            var z = B.cache;
                            o(B.props, function(C, D) {
                                if (!v[z] && B.to) {
                                    if (C === "alpha" || y[C] == null) {
                                        return
                                    }
                                    v[z] = B.to(v._rgba)
                                }
                                v[z][D.idx] = l(y[C], D, true)
                            });
                            if (v[z] && r.inArray(null, v[z].slice(0, 3)) < 0) {
                                v[z][3] = 1;
                                if (B.from) {
                                    v._rgba = B.from(v[z])
                                }
                            }
                        })
                    }
                    return this
                }
            },
            is: function(u) {
                var s = h(u),
                    v = true,
                    t = this;
                o(m, function(w, y) {
                    var z, x = s[y.cache];
                    if (x) {
                        z = t[y.cache] || y.to && y.to(t._rgba) || [];
                        o(y.props, function(A, B) {
                            if (x[B.idx] != null) {
                                v = (x[B.idx] === z[B.idx]);
                                return v
                            }
                        })
                    }
                    return v
                });
                return v
            },
            _space: function() {
                var s = [],
                    t = this;
                o(m, function(u, v) {
                    if (t[v.cache]) {
                        s.push(u)
                    }
                });
                return s.pop()
            },
            transition: function(t, z) {
                var u = h(t),
                    v = u._space(),
                    w = m[v],
                    x = this.alpha() === 0 ? h("transparent") : this,
                    y = x[w.cache] || w.to(x._rgba),
                    s = y.slice();
                u = u[w.cache];
                o(w.props, function(D, F) {
                    var C = F.idx,
                        B = y[C],
                        A = u[C],
                        E = q[F.type] || {};
                    if (A === null) {
                        return
                    }
                    if (B === null) {
                        s[C] = A
                    } else {
                        if (E.mod) {
                            if (A - B > E.mod / 2) {
                                B += E.mod
                            } else {
                                if (B - A > E.mod / 2) {
                                    B -= E.mod
                                }
                            }
                        }
                        s[C] = l((A - B) * z + B, F)
                    }
                });
                return this[v](s)
            },
            blend: function(v) {
                if (this._rgba[3] === 1) {
                    return this
                }
                var u = this._rgba.slice(),
                    t = u.pop(),
                    s = h(v)._rgba;
                return h(r.map(u, function(w, x) {
                    return (1 - t) * s[x] + t * w
                }))
            },
            toRgbaString: function() {
                var t = "rgba(",
                    s = r.map(this._rgba, function(u, w) {
                        return u == null ? (w > 2 ? 1 : 0) : u
                    });
                if (s[3] === 1) {
                    s.pop();
                    t = "rgb("
                }
                return t + s.join() + ")"
            },
            toHslaString: function() {
                var t = "hsla(",
                    s = r.map(this.hsla(), function(u, w) {
                        if (u == null) {
                            u = w > 2 ? 1 : 0
                        }
                        if (w && w < 3) {
                            u = Math.round(u * 100) + "%"
                        }
                        return u
                    });
                if (s[3] === 1) {
                    s.pop();
                    t = "hsl("
                }
                return t + s.join() + ")"
            },
            toHexString: function(s) {
                var t = this._rgba.slice(),
                    u = t.pop();
                if (s) {
                    t.push(~~ (u * 255))
                }
                return "#" + r.map(t, function(w) {
                    w = (w || 0).toString(16);
                    return w.length === 1 ? "0" + w : w
                }).join("")
            },
            toString: function() {
                return this._rgba[3] === 0 ? "transparent" : this.toRgbaString()
            }
        });
        h.fn.parse.prototype = h.fn;

        function f(u, t, s) {
            s = (s + 1) % 1;
            if (s * 6 < 1) {
                return u + (t - u) * s * 6
            }
            if (s * 2 < 1) {
                return t
            }
            if (s * 3 < 2) {
                return u + (t - u) * ((2 / 3) - s) * 6
            }
            return u
        }
        m.hsla.to = function(v) {
            if (v[0] == null || v[1] == null || v[2] == null) {
                return [null, null, null, v[3]]
            }
            var t = v[0] / 255,
                y = v[1] / 255,
                z = v[2] / 255,
                B = v[3],
                A = Math.max(t, y, z),
                w = Math.min(t, y, z),
                C = A - w,
                D = A + w,
                u = D * 0.5,
                x, E;
            if (w === A) {
                x = 0
            } else {
                if (t === A) {
                    x = (60 * (y - z) / C) + 360
                } else {
                    if (y === A) {
                        x = (60 * (z - t) / C) + 120
                    } else {
                        x = (60 * (t - y) / C) + 240
                    }
                }
            }
            if (C === 0) {
                E = 0
            } else {
                if (u <= 0.5) {
                    E = C / D
                } else {
                    E = C / (2 - D)
                }
            }
            return [Math.round(x) % 360, E, u, B == null ? 1 : B]
        };
        m.hsla.from = function(x) {
            if (x[0] == null || x[1] == null || x[2] == null) {
                return [null, null, null, x[3]]
            }
            var w = x[0] / 360,
                v = x[1],
                u = x[2],
                t = x[3],
                y = u <= 0.5 ? u * (1 + v) : u + v - u * v,
                z = 2 * u - y;
            return [Math.round(f(z, y, w + (1 / 3)) * 255), Math.round(f(z, y, w) * 255), Math.round(f(z, y, w - (1 / 3)) * 255), t]
        };
        o(m, function(t, v) {
            var u = v.props,
                s = v.cache,
                x = v.to,
                w = v.from;
            h.fn[t] = function(C) {
                if (x && !this[s]) {
                    this[s] = x(this._rgba)
                }
                if (C === g) {
                    return this[s].slice()
                }
                var z, B = r.type(C),
                    y = (B === "array" || B === "object") ? C : arguments,
                    A = this[s].slice();
                o(u, function(D, F) {
                    var E = y[B === "object" ? D : F.idx];
                    if (E == null) {
                        E = A[F.idx]
                    }
                    A[F.idx] = l(E, F)
                });
                if (w) {
                    z = h(w(A));
                    z[s] = A;
                    return z
                } else {
                    return h(A)
                }
            };
            o(u, function(y, z) {
                if (h.fn[y]) {
                    return
                }
                h.fn[y] = function(D) {
                    var F = r.type(D),
                        C = (y === "alpha" ? (this._hsla ? "hsla" : "rgba") : t),
                        B = this[C](),
                        E = B[z.idx],
                        A;
                    if (F === "undefined") {
                        return E
                    }
                    if (F === "function") {
                        D = D.call(this, E);
                        F = r.type(D)
                    }
                    if (D == null && z.empty) {
                        return this
                    }
                    if (F === "string") {
                        A = k.exec(D);
                        if (A) {
                            D = E + parseFloat(A[2]) * (A[1] === "+" ? 1 : -1)
                        }
                    }
                    B[z.idx] = D;
                    return this[C](B)
                }
            })
        });
        h.hook = function(t) {
            var s = t.split(" ");
            o(s, function(u, v) {
                r.cssHooks[v] = {
                    set: function(z, A) {
                        var x, y, w = "";
                        if (A !== "transparent" && (r.type(A) !== "string" || (x = i(A)))) {
                            A = h(x || A);
                            if (!p.rgba && A._rgba[3] !== 1) {
                                y = v === "backgroundColor" ? z.parentNode : z;
                                while ((w === "" || w === "transparent") && y && y.style) {
                                    try {
                                        w = r.css(y, "backgroundColor");
                                        y = y.parentNode
                                    } catch (B) {}
                                }
                                A = A.blend(w && w !== "transparent" ? w : "_default")
                            }
                            A = A.toRgbaString()
                        }
                        try {
                            z.style[v] = A
                        } catch (B) {}
                    }
                };
                r.fx.step[v] = function(w) {
                    if (!w.colorInit) {
                        w.start = h(w.elem, v);
                        w.end = h(w.end);
                        w.colorInit = true
                    }
                    r.cssHooks[v].set(w.elem, w.start.transition(w.end, w.pos))
                }
            })
        };
        h.hook(n);
        r.cssHooks.borderColor = {
            expand: function(t) {
                var s = {};
                o(["Top", "Right", "Bottom", "Left"], function(v, u) {
                    s["border" + u + "Color"] = t
                });
                return s
            }
        };
        d = r.Color.names = {
            aqua: "#00ffff",
            black: "#000000",
            blue: "#0000ff",
            fuchsia: "#ff00ff",
            gray: "#808080",
            green: "#008000",
            lime: "#00ff00",
            maroon: "#800000",
            navy: "#000080",
            olive: "#808000",
            purple: "#800080",
            red: "#ff0000",
            silver: "#c0c0c0",
            teal: "#008080",
            white: "#ffffff",
            yellow: "#ffff00",
            transparent: [null, null, null, 0],
            _default: "#ffffff"
        }
    })(jQuery);
    (function() {
        var e = ["add", "remove", "toggle"],
            f = {
                border: 1,
                borderBottom: 1,
                borderColor: 1,
                borderLeft: 1,
                borderRight: 1,
                borderTop: 1,
                borderWidth: 1,
                margin: 1,
                padding: 1
            };
        a.each(["borderLeftStyle", "borderRightStyle", "borderBottomStyle", "borderTopStyle"], function(h, i) {
            a.fx.step[i] = function(j) {
                if (j.end !== "none" && !j.setAttr || j.pos === 1 && !j.setAttr) {
                    jQuery.style(j.elem, i, j.end);
                    j.setAttr = true
                }
            }
        });

        function g(l) {
            var i, h, j = l.ownerDocument.defaultView ? l.ownerDocument.defaultView.getComputedStyle(l, null) : l.currentStyle,
                k = {};
            if (j && j.length && j[0] && j[j[0]]) {
                h = j.length;
                while (h--) {
                    i = j[h];
                    if (typeof j[i] === "string") {
                        k[a.camelCase(i)] = j[i]
                    }
                }
            } else {
                for (i in j) {
                    if (typeof j[i] === "string") {
                        k[i] = j[i]
                    }
                }
            }
            return k
        }
        function d(h, j) {
            var l = {},
                i, k;
            for (i in j) {
                k = j[i];
                if (h[i] !== k) {
                    if (!f[i]) {
                        if (a.fx.step[i] || !isNaN(parseFloat(k))) {
                            l[i] = k
                        }
                    }
                }
            }
            return l
        }
        if (!a.fn.addBack) {
            a.fn.addBack = function(h) {
                return this.add(h == null ? this.prevObject : this.prevObject.filter(h))
            }
        }
        a.effects.animateClass = function(h, i, l, k) {
            var j = a.speed(i, l, k);
            return this.queue(function() {
                var o = a(this),
                    m = o.attr("class") || "",
                    n, p = j.children ? o.find("*").addBack() : o;
                p = p.map(function() {
                    var q = a(this);
                    return {
                        el: q,
                        start: g(this)
                    }
                });
                n = function() {
                    a.each(e, function(q, r) {
                        if (h[r]) {
                            o[r + "Class"](h[r])
                        }
                    })
                };
                n();
                p = p.map(function() {
                    this.end = g(this.el[0]);
                    this.diff = d(this.start, this.end);
                    return this
                });
                o.attr("class", m);
                p = p.map(function() {
                    var s = this,
                        q = a.Deferred(),
                        r = a.extend({}, j, {
                            queue: false,
                            complete: function() {
                                q.resolve(s)
                            }
                        });
                    this.el.animate(this.diff, r);
                    return q.promise()
                });
                a.when.apply(a, p.get()).done(function() {
                    n();
                    a.each(arguments, function() {
                        var q = this.el;
                        a.each(this.diff, function(r) {
                            q.css(r, "")
                        })
                    });
                    j.complete.call(o[0])
                })
            })
        };
        a.fn.extend({
            addClass: (function(h) {
                return function(j, i, l, k) {
                    return i ? a.effects.animateClass.call(this, {
                        add: j
                    }, i, l, k) : h.apply(this, arguments)
                }
            })(a.fn.addClass),
            removeClass: (function(h) {
                return function(j, i, l, k) {
                    return arguments.length > 1 ? a.effects.animateClass.call(this, {
                        remove: j
                    }, i, l, k) : h.apply(this, arguments)
                }
            })(a.fn.removeClass),
            toggleClass: (function(h) {
                return function(k, j, i, m, l) {
                    if (typeof j === "boolean" || j === c) {
                        if (!i) {
                            return h.apply(this, arguments)
                        } else {
                            return a.effects.animateClass.call(this, (j ? {
                                add: k
                            } : {
                                remove: k
                            }), i, m, l)
                        }
                    } else {
                        return a.effects.animateClass.call(this, {
                            toggle: k
                        }, j, i, m)
                    }
                }
            })(a.fn.toggleClass),
            switchClass: function(h, j, i, l, k) {
                return a.effects.animateClass.call(this, {
                    add: j,
                    remove: h
                }, i, l, k)
            }
        })
    })();
    (function() {
        a.extend(a.effects, {
            version: "1.10.3",
            save: function(g, h) {
                for (var f = 0; f < h.length; f++) {
                    if (h[f] !== null) {
                        g.data(b + h[f], g[0].style[h[f]])
                    }
                }
            },
            restore: function(g, j) {
                var h, f;
                for (f = 0; f < j.length; f++) {
                    if (j[f] !== null) {
                        h = g.data(b + j[f]);
                        if (h === c) {
                            h = ""
                        }
                        g.css(j[f], h)
                    }
                }
            },
            setMode: function(f, g) {
                if (g === "toggle") {
                    g = f.is(":hidden") ? "show" : "hide"
                }
                return g
            },
            getBaseline: function(g, h) {
                var i, f;
                switch (g[0]) {
                case "top":
                    i = 0;
                    break;
                case "middle":
                    i = 0.5;
                    break;
                case "bottom":
                    i = 1;
                    break;
                default:
                    i = g[0] / h.height
                }
                switch (g[1]) {
                case "left":
                    f = 0;
                    break;
                case "center":
                    f = 0.5;
                    break;
                case "right":
                    f = 1;
                    break;
                default:
                    f = g[1] / h.width
                }
                return {
                    x: f,
                    y: i
                }
            },
            createWrapper: function(g) {
                if (g.parent().is(".ui-effects-wrapper")) {
                    return g.parent()
                }
                var h = {
                    width: g.outerWidth(true),
                    height: g.outerHeight(true),
                    "float": g.css("float")
                },
                    k = a("<div></div>").addClass("ui-effects-wrapper").css({
                        fontSize: "100%",
                        background: "transparent",
                        border: "none",
                        margin: 0,
                        padding: 0
                    }),
                    f = {
                        width: g.width(),
                        height: g.height()
                    },
                    j = document.activeElement;
                try {
                    j.id
                } catch (i) {
                    j = document.body
                }
                g.wrap(k);
                if (g[0] === j || a.contains(g[0], j)) {
                    a(j).focus()
                }
                k = g.parent();
                if (g.css("position") === "static") {
                    k.css({
                        position: "relative"
                    });
                    g.css({
                        position: "relative"
                    })
                } else {
                    a.extend(h, {
                        position: g.css("position"),
                        zIndex: g.css("z-index")
                    });
                    a.each(["top", "left", "bottom", "right"], function(l, m) {
                        h[m] = g.css(m);
                        if (isNaN(parseInt(h[m], 10))) {
                            h[m] = "auto"
                        }
                    });
                    g.css({
                        position: "relative",
                        top: 0,
                        left: 0,
                        right: "auto",
                        bottom: "auto"
                    })
                }
                g.css(f);
                return k.css(h).show()
            },
            removeWrapper: function(f) {
                var g = document.activeElement;
                if (f.parent().is(".ui-effects-wrapper")) {
                    f.parent().replaceWith(f);
                    if (f[0] === g || a.contains(f[0], g)) {
                        a(g).focus()
                    }
                }
                return f
            },
            setTransition: function(g, i, f, h) {
                h = h || {};
                a.each(i, function(k, j) {
                    var l = g.cssUnit(j);
                    if (l[0] > 0) {
                        h[j] = l[0] * f + l[1]
                    }
                });
                return h
            }
        });

        function d(g, f, h, i) {
            if (a.isPlainObject(g)) {
                f = g;
                g = g.effect
            }
            g = {
                effect: g
            };
            if (f == null) {
                f = {}
            }
            if (a.isFunction(f)) {
                i = f;
                h = null;
                f = {}
            }
            if (typeof f === "number" || a.fx.speeds[f]) {
                i = h;
                h = f;
                f = {}
            }
            if (a.isFunction(h)) {
                i = h;
                h = null
            }
            if (f) {
                a.extend(g, f)
            }
            h = h || f.duration;
            g.duration = a.fx.off ? 0 : typeof h === "number" ? h : h in a.fx.speeds ? a.fx.speeds[h] : a.fx.speeds._default;
            g.complete = i || f.complete;
            return g
        }
        function e(f) {
            if (!f || typeof f === "number" || a.fx.speeds[f]) {
                return true
            }
            if (typeof f === "string" && !a.effects.effect[f]) {
                return true
            }
            if (a.isFunction(f)) {
                return true
            }
            if (typeof f === "object" && !f.effect) {
                return true
            }
            return false
        }
        a.fn.extend({
            effect: function() {
                var h = d.apply(this, arguments),
                    j = h.mode,
                    f = h.queue,
                    g = a.effects.effect[h.effect];
                if (a.fx.off || !g) {
                    if (j) {
                        return this[j](h.duration, h.complete)
                    } else {
                        return this.each(function() {
                            if (h.complete) {
                                h.complete.call(this)
                            }
                        })
                    }
                }
                function i(m) {
                    var n = a(this),
                        l = h.complete,
                        o = h.mode;

                    function k() {
                        if (a.isFunction(l)) {
                            l.call(n[0])
                        }
                        if (a.isFunction(m)) {
                            m()
                        }
                    }
                    if (n.is(":hidden") ? o === "hide" : o === "show") {
                        n[o]();
                        k()
                    } else {
                        g.call(n[0], h, k)
                    }
                }
                return f === false ? this.each(i) : this.queue(f || "fx", i)
            },
            show: (function(f) {
                return function(h) {
                    if (e(h)) {
                        return f.apply(this, arguments)
                    } else {
                        var g = d.apply(this, arguments);
                        g.mode = "show";
                        return this.effect.call(this, g)
                    }
                }
            })(a.fn.show),
            hide: (function(f) {
                return function(h) {
                    if (e(h)) {
                        return f.apply(this, arguments)
                    } else {
                        var g = d.apply(this, arguments);
                        g.mode = "hide";
                        return this.effect.call(this, g)
                    }
                }
            })(a.fn.hide),
            toggle: (function(f) {
                return function(h) {
                    if (e(h) || typeof h === "boolean") {
                        return f.apply(this, arguments)
                    } else {
                        var g = d.apply(this, arguments);
                        g.mode = "toggle";
                        return this.effect.call(this, g)
                    }
                }
            })(a.fn.toggle),
            cssUnit: function(f) {
                var g = this.css(f),
                    h = [];
                a.each(["em", "px", "%", "pt"], function(j, k) {
                    if (g.indexOf(k) > 0) {
                        h = [parseFloat(g), k]
                    }
                });
                return h
            }
        })
    })();
    (function() {
        var d = {};
        a.each(["Quad", "Cubic", "Quart", "Quint", "Expo"], function(f, e) {
            d[e] = function(g) {
                return Math.pow(g, f + 2)
            }
        });
        a.extend(d, {
            Sine: function(e) {
                return 1 - Math.cos(e * Math.PI / 2)
            },
            Circ: function(e) {
                return 1 - Math.sqrt(1 - e * e)
            },
            Elastic: function(e) {
                return e === 0 || e === 1 ? e : -Math.pow(2, 8 * (e - 1)) * Math.sin(((e - 1) * 80 - 7.5) * Math.PI / 15)
            },
            Back: function(e) {
                return e * e * (3 * e - 2)
            },
            Bounce: function(g) {
                var e, f = 4;
                while (g < ((e = Math.pow(2, --f)) - 1) / 11) {}
                return 1 / Math.pow(4, 3 - f) - 7.5625 * Math.pow((e * 3 - 2) / 22 - g, 2)
            }
        });
        a.each(d, function(f, e) {
            a.easing["easeIn" + f] = e;
            a.easing["easeOut" + f] = function(g) {
                return 1 - e(1 - g)
            };
            a.easing["easeInOut" + f] = function(g) {
                return g < 0.5 ? e(g * 2) / 2 : 1 - e(g * -2 + 2) / 2
            }
        })
    })()
})(jQuery);