(function($){
	"use strict";
	jQuery(document).on('ready', function () {
		var wind = $(window);
		var parallaxSlider;
		var parallaxSliderOptions = {
			speed: 1000,
			autoplay: true,
			parallax: true,
			loop: true,

			on: {
				init: function () {
					var swiper = this;
					for (var i = 0; i < swiper.slides.length; i++) {
						$(swiper.slides[i])
							.find('.bg-img')
							.attr({
								'data-swiper-parallax': 0.75 * swiper.width
							});
					}
				},
				resize: function () {
					this.update();
				}
			},

			pagination: {
				el: '.slider-prlx .parallax-slider .swiper-pagination',
				dynamicBullets: true,
				clickable: true
			},

			navigation: {
				nextEl: '.slider-prlx .parallax-slider .next-ctrl',
				prevEl: '.slider-prlx .parallax-slider .prev-ctrl'
			}
		};
		parallaxSlider = new Swiper('.slider-prlx .parallax-slider', parallaxSliderOptions);
	
		// Var Background image
		var pageSection = $(".bg-img, section");
		pageSection.each(function (indx) {
			if ($(this).attr("data-background")) {
				$(this).css("background-image", "url(" + $(this).data("background") + ")");
			}
		});
	
        // Header Sticky
		$(window).on('scroll',function() {
            if ($(this).scrollTop() > 120){  
                $('.navbar-area').addClass("is-sticky");
            }
            else{
                $('.navbar-area').removeClass("is-sticky");
            }
        });
        
        // Mean Menu
		jQuery('.mean-menu').meanmenu({
			meanScreenWidth: "991"
        });
	
		// MagnificPopup
		var magnifPopup = function() {
			$('.popup-img').magnificPopup({
				type: 'image',
				removalDelay: 300,
				mainClass: 'mfp-with-zoom',
				gallery: {
					enabled: true
				},
				zoom: {
					enabled: true, // By default it's false, so don't forget to enable it
					duration: 300, // duration of the effect, in milliseconds
					easing: 'ease-in-out', // CSS transition easing function
					// The "opener" function should return the element from which popup will be zoomed in
					// and to which popup will be scaled down
					// By defailt it looks for an image tag:
					opener: function(openerElement) {
						// openerElement is the element on which popup was initialized, in this case its <a> tag
						// you don't need to add "opener" option if this code matches your needs, it's defailt one.
						return openerElement.is('img') ? openerElement : openerElement.find('img');
					}
				}
			});
		};
		// Call the functions
		magnifPopup();
		
        // Popup Video
        $(".popup-video").magnificPopup({
            disableOn: 320,
            type: "iframe",
            mainClass: "mfp-fade",
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false,
        });
		
        // Button Hover JS
        $(function() {
            $('.default-btn')
            .on('mouseenter', function(e) {
                var parentOffset = $(this).offset(),
                relX = e.pageX - parentOffset.left,
                relY = e.pageY - parentOffset.top;
                $(this).find('span').css({top:relY, left:relX})
            })
            .on('mouseout', function(e) {
                var parentOffset = $(this).offset(),
                relX = e.pageX - parentOffset.left,
                relY = e.pageY - parentOffset.top;
                $(this).find('span').css({top:relY, left:relX})
            });
        });
        
        // Testimonial Slider
		$('.testimonial-slider').owlCarousel({
			loop: true,
			nav: true,
			dots: true,
			autoplayHoverPause: true,
            autoplay: true,
            smartSpeed: 1000,
            margin: 20,
            navText: [
                "<i class='fa fa-chevron-left'></i>",
                "<i class='fa fa-chevron-right'></i>"
            ],
			responsive: {
                0: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                1200: {
                    items: 3,
                }
            }
        });
        // Testimonials Two owl
		$('.testimonial-slide-two .owl-carousel').owlCarousel({
			margin: 0,
			autoplay: true,
			autoplayTimeout: 4000,
			nav: false,
			smartSpeed: 800,
			dots: true,
			autoplayHoverPause: true,
			loop: true,
			responsiveClass: true,
			responsive: {
				0: {
					items: 1
				},
				768: {
					items: 2
				},
				1000: {
					items: 3
				}
			}
		});
		
        // Partner Logo
        $("#partner-carousel").owlCarousel({
            margin: 0,
            autoplay: true,
            autoplayTimeout: 4000,
            smartSpeed: 800,
            nav: false,
            dots: false,
            autoplayHoverPause: true,
            loop: true,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                768: {
                    items: 3,
                },
                1000: {
                    items: 5,
                },
            },
        });
		
		//  Star Counter
		$('.counter-number').counterUp({
			delay: 15,
			time: 2000
		});
		
        // FAQ Accordion
        $(function() {
            $('.accordion').find('.accordion-title').on('click', function(){
                // Adds Active Class
                $(this).toggleClass('active');
                // Expand or Collapse This Panel
                $(this).next().slideToggle('slow');
                // Hide The Other Panels
                $('.accordion-content').not($(this).next()).slideUp('slow');
                // Removes Active Class From Other Titles
                $('.accordion-title').not($(this)).removeClass('active');		
            });
        });

        // Go to Top
        $(function(){
            // Scroll Event
            $(window).on('scroll', function(){
                var scrolled = $(window).scrollTop();
                if (scrolled > 600) $('.go-top').addClass('active');
                if (scrolled < 600) $('.go-top').removeClass('active');
            });  
            // Click Event
            $('.go-top').on('click', function() {
                $("html, body").animate({ scrollTop: "0" },  500);
            });
        });
        
        // Count Time 
        function makeTimer() {
            var endTime = new Date("January 25, 2025 20:00:00 PDT");			
            var endTime = (Date.parse(endTime)) / 1000;
            var now = new Date();
            var now = (Date.parse(now) / 1000);
            var timeLeft = endTime - now;
            var days = Math.floor(timeLeft / 86400); 
            var hours = Math.floor((timeLeft - (days * 86400)) / 3600);
            var minutes = Math.floor((timeLeft - (days * 86400) - (hours * 3600 )) / 60);
            var seconds = Math.floor((timeLeft - (days * 86400) - (hours * 3600) - (minutes * 60)));
            if (hours < "10") { hours = "0" + hours; }
            if (minutes < "10") { minutes = "0" + minutes; }
            if (seconds < "10") { seconds = "0" + seconds; }
            $("#days").html(days + "<span>Days</span>");
            $("#hours").html(hours + "<span>Hours</span>");
            $("#minutes").html(minutes + "<span>Minutes</span>");
            $("#seconds").html(seconds + "<span>Seconds</span>");
        }
        setInterval(function() { makeTimer(); }, 1000);
		// .- ..- - .... --- .-. ---... / -... .- .-. .- -.- .- .... / - .... . -- . ...
    });
	
	// Menu isotope and filter
	$(window).on('load', function () {
		var menuIsotope = $('.menu-product-container').isotope({
			itemSelector: '.menu-grid-item',
			layoutMode: 'masonry',
			filter: '.all',
		});
		$('#menu-filter li').on('click', function () {
			$("#menu-filter li").removeClass('filter-active');
			$(this).addClass('filter-active');
			menuIsotope.isotope({
				filter: $(this).data('filter')
			});
		});
	});

	$(function () {
		$("#datepicker").datepicker();
	});

	$(document).ready(function() {
	  $('select').niceSelect();
	});
		
	// WOW JS
	$(window).on ('load', function (){
        if ($(".wow").length) { 
            var wow = new WOW ({
                boxClass:     'wow',      // Animated element css class (default is wow)
                animateClass: 'animated', // Animation css class (default is animated)
                offset:       20,         // Distance to the element when triggering the animation (default is 0)
                mobile:       true,       // Trigger animations on mobile devices (default is true)
                live:         true,       // Act on asynchronously loaded content (default is true)
            });
            wow.init();
        }
    });

    // Preloader Area
	$(window).on('load', function() {
		$('.preloader').fadeOut('500');
	});
}(jQuery));