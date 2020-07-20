
var filterResult = $('#filterPropertyResult');
//Property Location filter
function propertyListByLocation(location) {
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByLocation",
		data: { Location: location },
		dataType: "html",
		success: function (response) {
			console.log(response);
			filterResult.html('');
			filterResult.html(response);
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
}
//Property Type filter
function propertyListByType(type) {
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByType",
		data: { Type: type },
		dataType: "html",
		success: function (response) {
			console.log(response);
			filterResult.html('');
			filterResult.html(response);
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
}
//Property User filter
function propertyListByUser(user) {
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByUser",
		data: { User: user },
		dataType: "html",
		success: function (response) {
			console.log(response);
			filterResult.html('');
			filterResult.html(response);
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
}
//professional sliders section
Peoplecarousel = function () {
    var owlslider = jQuery("div.owl-carousel");
    if (owlslider.length > 0) {
        owlslider.each(function () {
            var $this = $(this),
                $items = ($this.data('items')) ? $this.data('items') : 5,
                $loop = ($this.attr('data-loop')) ? $this.data('loop') : true,
                $navdots = ($this.data('nav-dots')) ? $this.data('nav-dots') : true,
                $navarrow = ($this.data('nav-arrow')) ? $this.data('nav-arrow') : true,
                $autoplay = ($this.attr('data-autoplay')) ? $this.data('autoplay') : true,
                $autospeed = ($this.attr('data-autospeed')) ? $this.data('autospeed') : 5000,
                $smartspeed = ($this.attr('data-smartspeed')) ? $this.data('smartspeed') : 1000,
                $autohgt = ($this.data('autoheight')) ? $this.data('autoheight') : false,
                $space = ($this.attr('data-space')) ? $this.data('space') : 15,
                $animateOut = ($this.attr('data-animateOut')) ? $this.data('animateOut') : true;

            $(this).owlCarousel({
                loop: false,
                items: $items,
                responsive: {
                    0: {
                        items: $this.data('xx-items') ? $this.data('xx-items') : 1
                    },
                    480: {
                        items: $this.data('xs-items') ? $this.data('xs-items') : 1
                    },
                    768: {
                        items: $this.data('sm-items') ? $this.data('sm-items') : 2
                    },
                    980: {
                        items: $this.data('md-items') ? $this.data('md-items') : 3
                    },
                    1200: {
                        items: $items
                    }
                },
                dots: $navdots,
                autoplayTimeout: $autospeed,
                smartSpeed: $smartspeed,
                autoHeight: $autohgt,
                margin: $space,
                nav: $navarrow,
                navigation: true,
                navText: ["<i class='fas fa-chevron-left'></i>", "<i class='fas fa-chevron-right'></i>"],
                autoplay: false,
                autoplayHoverPause: true
            });
        });
    }
}
Peoplecarousel();