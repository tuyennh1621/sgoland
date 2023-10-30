
$(function () {
	"use strict";

	//Loader	
	$(function preloaderLoad() {
		if ($('.preloader').length) {
			$('.preloader').delay(200).fadeOut(300);
		}
		$(".preloader_disabler").on('click', function () {
			$("#preloader").hide();
		});
	});
	// Example starter JavaScript for disabling form submissions if there are invalid fields
	(() => {
		'use strict'

		// Fetch all the forms we want to apply custom Bootstrap validation styles to
		const forms = document.querySelectorAll('.needs-validation')

		// Loop over them and prevent submission
		Array.from(forms).forEach(form => {
			form.addEventListener('submit', event => {
				if (!form.checkValidity()) {
					event.preventDefault()
					event.stopPropagation()
				}

				form.classList.add('was-validated')
			}, false)
		})
	})()



	// Script Navigation
	! function (n, e, i, a) {
		n.navigation = function (t, s) {
			var o = {
				responsive: !0,
				mobileBreakpoint: 992,
				showDuration: 300,
				hideDuration: 300,
				showDelayDuration: 0,
				hideDelayDuration: 0,
				submenuTrigger: "hover",
				effect: "fade",
				submenuIndicator: !0,
				hideSubWhenGoOut: !0,
				visibleSubmenusOnMobile: !1,
				fixed: !1,
				overlay: !0,
				overlayColor: "rgba(0, 0, 0, 0.5)",
				hidden: !1,
				offCanvasSide: "left",
				onInit: function () { },
				onShowOffCanvas: function () { },
				onHideOffCanvas: function () { }
			},
				u = this,
				r = Number.MAX_VALUE,
				d = 1,
				f = "click.nav touchstart.nav",
				l = "mouseenter.nav",
				c = "mouseleave.nav";
			u.settings = {};
			var t = (n(t), t);
			n(t).find(".nav-menus-wrapper").prepend("<span class='nav-menus-wrapper-close-button'>✕</span>"), n(t).find(".nav-search").length > 0 && n(t).find(".nav-search").find("form").prepend("<span class='nav-search-close-button'>✕</span>"), u.init = function () {
				u.settings = n.extend({}, o, s), "right" == u.settings.offCanvasSide && n(t).find(".nav-menus-wrapper").addClass("nav-menus-wrapper-right"), u.settings.hidden && (n(t).addClass("navigation-hidden"), u.settings.mobileBreakpoint = 99999), v(), u.settings.fixed && n(t).addClass("navigation-fixed"), n(t).find(".nav-toggle").on("click touchstart", function (n) {
					n.stopPropagation(), n.preventDefault(), u.showOffcanvas(), s !== a && u.callback("onShowOffCanvas")
				}), n(t).find(".nav-menus-wrapper-close-button").on("click touchstart", function () {
					u.hideOffcanvas(), s !== a && u.callback("onHideOffCanvas")
				}), n(t).find(".nav-search-button").on("click touchstart", function (n) {
					n.stopPropagation(), n.preventDefault(), u.toggleSearch()
				}), n(t).find(".nav-search-close-button").on("click touchstart", function () {
					u.toggleSearch()
				}), n(t).find(".megamenu-tabs").length > 0 && y(), n(e).resize(function () {
					m(), C()
				}), m(), s !== a && u.callback("onInit")
			};
			var v = function () {
				n(t).find("li").each(function () {
					n(this).children(".nav-dropdown,.megamenu-panel").length > 0 && (n(this).children(".nav-dropdown,.megamenu-panel").addClass("nav-submenu"), u.settings.submenuIndicator && n(this).children("a").append("<span class='submenu-indicator'><span class='submenu-indicator-chevron'></span></span>"))
				})
			};
			u.showSubmenu = function (e, i) {
				g() > u.settings.mobileBreakpoint && n(t).find(".nav-search").find("form").slideUp(), "fade" == i ? n(e).children(".nav-submenu").stop(!0, !0).delay(u.settings.showDelayDuration).fadeIn(u.settings.showDuration) : n(e).children(".nav-submenu").stop(!0, !0).delay(u.settings.showDelayDuration).slideDown(u.settings.showDuration), n(e).addClass("nav-submenu-open")
			}, u.hideSubmenu = function (e, i) {
				"fade" == i ? n(e).find(".nav-submenu").stop(!0, !0).delay(u.settings.hideDelayDuration).fadeOut(u.settings.hideDuration) : n(e).find(".nav-submenu").stop(!0, !0).delay(u.settings.hideDelayDuration).slideUp(u.settings.hideDuration), n(e).removeClass("nav-submenu-open").find(".nav-submenu-open").removeClass("nav-submenu-open")
			};
			var h = function () {
				n("body").addClass("no-scroll"), u.settings.overlay && (n(t).append("<div class='nav-overlay-panel'></div>"), n(t).find(".nav-overlay-panel").css("background-color", u.settings.overlayColor).fadeIn(300).on("click touchstart", function (n) {
					u.hideOffcanvas()
				}))
			},
				p = function () {
					n("body").removeClass("no-scroll"), u.settings.overlay && n(t).find(".nav-overlay-panel").fadeOut(400, function () {
						n(this).remove()
					})
				};
			u.showOffcanvas = function () {
				h(), "left" == u.settings.offCanvasSide ? n(t).find(".nav-menus-wrapper").css("transition-property", "left").addClass("nav-menus-wrapper-open") : n(t).find(".nav-menus-wrapper").css("transition-property", "right").addClass("nav-menus-wrapper-open")
			}, u.hideOffcanvas = function () {
				n(t).find(".nav-menus-wrapper").removeClass("nav-menus-wrapper-open").on("webkitTransitionEnd moztransitionend transitionend oTransitionEnd", function () {
					n(t).find(".nav-menus-wrapper").css("transition-property", "none").off()
				}), p()
			}, u.toggleOffcanvas = function () {
				g() <= u.settings.mobileBreakpoint && (n(t).find(".nav-menus-wrapper").hasClass("nav-menus-wrapper-open") ? (u.hideOffcanvas(), s !== a && u.callback("onHideOffCanvas")) : (u.showOffcanvas(), s !== a && u.callback("onShowOffCanvas")))
			}, u.toggleSearch = function () {
				"none" == n(t).find(".nav-search").find("form").css("display") ? (n(t).find(".nav-search").find("form").slideDown(), n(t).find(".nav-submenu").fadeOut(200)) : n(t).find(".nav-search").find("form").slideUp()
			};
			var m = function () {
				u.settings.responsive ? (g() <= u.settings.mobileBreakpoint && r > u.settings.mobileBreakpoint && (n(t).addClass("navigation-portrait").removeClass("navigation-landscape"), D()), g() > u.settings.mobileBreakpoint && d <= u.settings.mobileBreakpoint && (n(t).addClass("navigation-landscape").removeClass("navigation-portrait"), k(), p(), u.hideOffcanvas()), r = g(), d = g()) : k()
			},
				b = function () {
					n("body").on("click.body touchstart.body", function (e) {
						0 === n(e.target).closest(".navigation").length && (n(t).find(".nav-submenu").fadeOut(), n(t).find(".nav-submenu-open").removeClass("nav-submenu-open"), n(t).find(".nav-search").find("form").slideUp())
					})
				},
				g = function () {
					return e.innerWidth || i.documentElement.clientWidth || i.body.clientWidth
				},
				w = function () {
					n(t).find(".nav-menu").find("li, a").off(f).off(l).off(c)
				},
				C = function () {
					if (g() > u.settings.mobileBreakpoint) {
						var e = n(t).outerWidth(!0);
						n(t).find(".nav-menu").children("li").children(".nav-submenu").each(function () {
							n(this).parent().position().left + n(this).outerWidth() > e ? n(this).css("right", 0) : n(this).css("right", "auto")
						})
					}
				},
				y = function () {
					function e(e) {
						var i = n(e).children(".megamenu-tabs-nav").children("li"),
							a = n(e).children(".megamenu-tabs-pane");
						n(i).on("click.tabs touchstart.tabs", function (e) {
							e.stopPropagation(), e.preventDefault(), n(i).removeClass("active"), n(this).addClass("active"), n(a).hide(0).removeClass("active"), n(a[n(this).index()]).show(0).addClass("active")
						})
					}
					if (n(t).find(".megamenu-tabs").length > 0)
						for (var i = n(t).find(".megamenu-tabs"), a = 0; a < i.length; a++) e(i[a])
				},
				k = function () {
					w(), n(t).find(".nav-submenu").hide(0), navigator.userAgent.match(/Mobi/i) || navigator.maxTouchPoints > 0 || "click" == u.settings.submenuTrigger ? n(t).find(".nav-menu, .nav-dropdown").children("li").children("a").on(f, function (i) {
						if (u.hideSubmenu(n(this).parent("li").siblings("li"), u.settings.effect), n(this).closest(".nav-menu").siblings(".nav-menu").find(".nav-submenu").fadeOut(u.settings.hideDuration), n(this).siblings(".nav-submenu").length > 0) {
							if (i.stopPropagation(), i.preventDefault(), "none" == n(this).siblings(".nav-submenu").css("display")) return u.showSubmenu(n(this).parent("li"), u.settings.effect), C(), !1;
							if (u.hideSubmenu(n(this).parent("li"), u.settings.effect), "_blank" == n(this).attr("target") || "blank" == n(this).attr("target")) e.open(n(this).attr("href"));
							else {
								if ("#" == n(this).attr("href") || "" == n(this).attr("href")) return !1;
								e.location.href = n(this).attr("href")
							}
						}
					}) : n(t).find(".nav-menu").find("li").on(l, function () {
						u.showSubmenu(this, u.settings.effect), C()
					}).on(c, function () {
						u.hideSubmenu(this, u.settings.effect)
					}), u.settings.hideSubWhenGoOut && b()
				},
				D = function () {
					w(), n(t).find(".nav-submenu").hide(0), u.settings.visibleSubmenusOnMobile ? n(t).find(".nav-submenu").show(0) : (n(t).find(".nav-submenu").hide(0), n(t).find(".submenu-indicator").removeClass("submenu-indicator-up"), u.settings.submenuIndicator ? n(t).find(".submenu-indicator").on(f, function (e) {
						return e.stopPropagation(), e.preventDefault(), u.hideSubmenu(n(this).parent("a").parent("li").siblings("li"), "slide"), u.hideSubmenu(n(this).closest(".nav-menu").siblings(".nav-menu").children("li"), "slide"), "none" == n(this).parent("a").siblings(".nav-submenu").css("display") ? (n(this).addClass("submenu-indicator-up"), n(this).parent("a").parent("li").siblings("li").find(".submenu-indicator").removeClass("submenu-indicator-up"), n(this).closest(".nav-menu").siblings(".nav-menu").find(".submenu-indicator").removeClass("submenu-indicator-up"), u.showSubmenu(n(this).parent("a").parent("li"), "slide"), !1) : (n(this).parent("a").parent("li").find(".submenu-indicator").removeClass("submenu-indicator-up"), void u.hideSubmenu(n(this).parent("a").parent("li"), "slide"))
					}) : k())
				};
			u.callback = function (n) {
				s[n] !== a && s[n].call(t)
			}, u.init()
		}, n.fn.navigation = function (e) {
			return this.each(function () {
				if (a === n(this).data("navigation")) {
					var i = new n.navigation(this, e);
					n(this).data("navigation", i)
				}
			})
		}
	}
		(jQuery, window, document), $(document).ready(function () {
			$("#navigation").navigation()
		});


	// Script Show Calling Number
	$('#number').on('click', function () {
		var tel = $(this).data('last');
		$(this).find('span').html('<a href="tel:' + tel + '">' + tel + '</a>');
	});


	// Script For Select Adult & Child Number
	$(function () {

		var guestAmount = $('#guestNo');

		$('#cnt-up').on('click', function () {
			guestAmount.val(Math.min(parseInt($('#guestNo').val()) + 1, 20));
		});
		$('#cnt-down').on('click', function () {
			guestAmount.val(Math.max(parseInt($('#guestNo').val()) - 1, 1));
		});

	});

	$(function () {

		var guestAmount = $('#kidsNo');

		$('#kcnt-up').on('click', function () {
			guestAmount.val(Math.min(parseInt($('#kidsNo').val()) + 1, 20));
		});
		$('#kcnt-down').on('click', function () {
			guestAmount.val(Math.max(parseInt($('#kidsNo').val()) - 1, 0));
		});
	});


	// Check In & Check Out Daterange Script
	// $(function() {
	//   $('input[name="checkout"]').daterangepicker({
	// 	singleDatePicker: true,
	//   });
	// 	$('input[name="checkout"]').val('');
	// 	$('input[name="checkout"]').attr("placeholder","Check Out");
	// });
	// $(function() {
	//   $('input[name="checkin"]').daterangepicker({
	// 	singleDatePicker: true,

	//   });
	// 	$('input[name="checkin"]').val('');
	// 	$('input[name="checkin"]').attr("placeholder","Check In");
	// });


	// Tooltip
/*	$('[data-toggle="tooltip"]').tooltip();*/

	// Range Slider Script
	$(".js-range-slider").ionRangeSlider({
		type: "double",
		min: 0,
		max: 1000,
		from: 200,
		to: 500,
		grid: true
	});

	// Bottom To Top Scroll Script
	$(window).on('scroll', function () {
		var height = $(window).scrollTop();
		if (height > 100) {
			$('#back2Top').fadeIn();
		} else {
			$('#back2Top').fadeOut();
		}
	});


	// Script For Fix Header on Scroll
	$(window).on('scroll', function () {
		var scroll = $(window).scrollTop();

		if (scroll >= 50) {
			$(".header").addClass("header-fixed");
		} else {
			$(".header").removeClass("header-fixed");
		}
	});

	// fullwidth home slider
	function inlineCSS() {
		$(".home-slider .item").each(function () {
			var attrImageBG = $(this).attr('data-background-image');
			var attrColorBG = $(this).attr('data-background-color');
			if (attrImageBG !== undefined) {
				$(this).css('background-image', 'url(' + attrImageBG + ')');
			}
			if (attrColorBG !== undefined) {
				$(this).css('background', '' + attrColorBG + '');
			}
		});
	}
	inlineCSS();

	// Search Radio
	function searchTypeButtons() {
		$('.property_search_filter label.active input[type="radio"]').prop('checked', true);
		var buttonWidth = $('.property_search_filter label.active').width();
		var arrowDist = $('.property_search_filter label.active').position();
		$('.property_search_filter-arrow').css('left', arrowDist + (buttonWidth / 2));
		$('.property_search_filter label').on('change', function () {
			$('.property_search_filter input[type="radio"]').parent('label').removeClass('active');
			$('.property_search_filter input[type="radio"]:checked').parent('label').addClass('active');
			var buttonWidth = $('.property_search_filter label.active').width();
			var arrowDist = $('.property_search_filter label.active').position().left;
			$('.property_search_filter-arrow').css({
				'left': arrowDist + (buttonWidth / 1.7),
				'transition': 'left 0.4s cubic-bezier(.95,-.41,.19,1.44)'
			});
		});
	}
	if ($(".hero_banner").length) {
		searchTypeButtons();
		$(window).on('load resize', function () {
			searchTypeButtons();
		});
	}

	$('.swipebox').swipebox();

	//$('.select2').select2({
	//	placeholder: "Chọn",
	//	allowClear: true
	//});

});

const Events = (function () {
	function init() {
		const targets = document.querySelectorAll("[data-x-click]")
		if (!targets) return

		targets.forEach((eventTarget) => {
			const attributes = eventTarget.getAttribute('data-x-click').split(', ')

			attributes.forEach((el) => {
				const target = document.querySelector(`[data-x=${el}]`)

				eventTarget.addEventListener('click', () => {
					const toggleClass = target.getAttribute('data-x-toggle')
					target.classList.toggle(toggleClass)
					
				})
			})
		})
	}
	return {
		init: init,
	}
})()

/*Select Property price*/
$('#numdayoff').select2({
	placeholder: "Chọn số ngày offline",
	allowClear: true
});

$('#NumberFloor').select2({
	placeholder: "Chọn số tầng",
	allowClear: true
});
$('#NumberBedRoom').select2({
	placeholder: "Chọn số phòng ngủ",
	allowClear: true
});
$('#NumberWc').select2({
	placeholder: "Chọn số phòng vệ sinh",
	allowClear: true
});
$('#NumberBalcony').select2({
	placeholder: "Chọn số ban công",
	allowClear: true
});
$('#DirectionHouse').select2({
	placeholder: "Chọn hướng nhà",
	allowClear: true
});
$('#Source').select2({
	placeholder: "Chọn  nguồn đầu chủ",
	allowClear: true
});
$('#IdStatus').select2({
	placeholder: "Chọn trạng thái",
	allowClear: true
});
$('#IdRE').select2({
	placeholder: "Chọn phân loại",
	allowClear: true
});
$('#status').select2({
	placeholder: "Chọn trạng thái",
	allowClear: true
});
$('#TypeRE').select2({
	placeholder: "Loại dự án",
	allowClear: true
});
$('#IdType').select2({
	placeholder: "Chọn loại dự án",
	allowClear: true
});
$('#IdProvince').select2({
	placeholder: "Chọn Tỉnh Thành",
	allowClear: true
});

$('#idposition').select2({
	placeholder: "Chọn vị trí",
	allowClear: true
});

$('#IdDistrict').select2({
	placeholder: "Chọn tỉnh thành",
	allowClear: true
});

$('#district').select2({
	placeholder: "Chọn quận huyện",
	allowClear: true
});
$('#adward').select2({
	placeholder: "Chọn xã phường",
	allowClear: true
});
$('#IdWard').select2({
	placeholder: "Chọn xã phường",
	allowClear: true
});
$('#type').select2({
	placeholder: "Loại Video",
	allowClear: true
});

$('#IdCompany').select2({
	placeholder: "Chọn công ty",
	allowClear: true
});

$('#companyid').select2({
	placeholder: "Chọn công ty",
	allowClear: true
});

$('#department').select2({
	placeholder: "Chọn phòng ban",
	allowClear: true
});

$('#DepartmentId').select2({
	placeholder: "Chọn vị trí",
	allowClear: true
});

$('#GioiTinh').select2({
	placeholder: "Chọn giới tính",
	allowClear: true
});
$('#ctvstatus').select2({
	placeholder: "Chọn trạng thái",
	allowClear: true
});
$('#IdCongty').select2({
	placeholder: "Chọn công ty",
	allowClear: true
});

function initComponents() {
	Events.init();
}


window.onload = function () {

	document.fonts.ready.then(function () {
		initComponents()
	});

	
	var _refid = getParameterByName('refid');
	if (_refid) {
		$.ajax({
			type: 'POST',
			url: '/Home/CheckidExisting',
			data: { id: _refid },
			cache: false,
			async: true,
			success: function (result) {
				if (result == "1") {
					$("#Refid").val(_refid);
					$("#Refid").prop('readonly', true);
				}
			},
			error: function () {
				alert('Failed to receive the Data');
			}
		})

		
	}
	var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
	var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
		return new bootstrap.Tooltip(tooltipTriggerEl)
	})

}
	var swiper = new Swiper(".mySwiper", {
		spaceBetween: 10,
		slidesPerView: 5,
		freeMode: true,
		watchSlidesProgress: true,
	});
	var swiper2 = new Swiper(".mySwiper2", {
		spaceBetween: 10,
	navigation: {
		nextEl: ".swiper-button-next",
	prevEl: ".swiper-button-prev",
		},
	thumbs: {
		swiper: swiper,
		},
	});


