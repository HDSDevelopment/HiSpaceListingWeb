function operatorFilterCount() {
	var opCount = $('.operatorCount:last-child').html();
	$('#opCount span').html(opCount);
};
$(document).ready(function () {
	
	operatorFilterCount();
});
var filterPropertyResult = $('#filterPropertyResult');
var detailPropertyResult = $('#detailPropertyResult');
//Property list all
function propertyListByAll() {
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByAll",
		data: {},
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
}
//Property List by its userid
function propertyListByUserId(user) {
	$.ajax({
		type: "GET",
		url: "/Detail/SelectOperatorPropertyListByAll",
		data: { User: user },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			detailPropertyResult.html('');
			detailPropertyResult.html(response);
			Peoplecarousel();
		},
		error: function (response) {
			alert("server not ready please try afterwards");
			//console.log(response);
		}
	});
} 
//Property List by its userid
function propertyListByUserIdAndListingId(user,listingId) {
	$.ajax({
		type: "POST",
		url: "/Detail/SelectOperatorPropertyListByUserIdAndListingId",
		data: {User:user,ListingId:listingId},
		dataType: "html",
		//dataType: "html",
		success: function (response) {
			//console.log(response);
			detailPropertyResult.html('');
			detailPropertyResult.html(response);
			Peoplecarousel();
		},
		error: function (response) {
			alert("server not ready please try afterwards");
			//console.log(response);
		}
	});
} 
//Property Location filter
function propertyListByLocation(location) {
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByLocation",
		data: { Location: location },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
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
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
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
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
}

var filterOperatorResult = $('#filterOperatorResult');
//Operator list all
function operatorListByAll() {
	$.ajax({
		type: "GET",
		url: "/Filter/OperatorListByAll",
		data: {},
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterOperatorResult.html('');
			filterOperatorResult.html(response);
			operatorFilterCount();
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
	
}
//Operator list by operator
function operatorListByUser(user) {
	$.ajax({
		type: "GET",
		url: "/Filter/OperatorListByUserId",
		data: { User: user},
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterOperatorResult.html('');
			filterOperatorResult.html(response);
			operatorFilterCount();
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
}
var filterProfessionalResult = $('#filterProfessionalResult');
//People list all
function peopleListByAll() {
	$.ajax({
		type: "GET",
		url: "/Filter/PeopleListByAll",
		data: {},
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterProfessionalResult.html('');
			filterProfessionalResult.html(response);
			Peoplecarousel();
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});

} 
//People list by ListingId
function peopleListByUser(listingId) {
	$.ajax({
		type: "GET",
		url: "/Filter/PeopleListByListingId",
		data: { ListingId: listingId },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterProfessionalResult.html('');
			filterProfessionalResult.html(response);
			Peoplecarousel();
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

//$('body').on('click', '.listNavigation', function () {
//	console.log('test');
//});
function GetURLParameter(sParam) {
	var sPageURL = window.location.search.substring(1);
	var sURLVariables = sPageURL.split('&');
	for (var i = 0; i < sURLVariables.length; i++) {
		var sParameterName = sURLVariables[i].split('=');
		if (sParameterName[0] == sParam) {
			return decodeURIComponent(sParameterName[1]);
		}
	}
}

var listShowType = GetURLParameter('ListShowType');
console.log(listShowType);
if (listShowType == 1) {
	$('.nav-link, .tab-pane').removeClass('active');
	$('.tab-pane').removeClass('show');
	$('#tab-01-tab.nav-link, #tab-01.tab-pane').addClass('active');
	$('#tab-01.tab-pane').addClass('show');

} else if (listShowType == 2) {
	$('.nav-link, .tab-pane').removeClass('active');
	$('.tab-pane').removeClass('show');
	$('#tab-02-tab.nav-link, #tab-02.tab-pane').addClass('active');
	$('#tab-02.tab-pane').addClass('show');
} else if (listShowType == 3) {
	$('.nav-link, .tab-pane').removeClass('active');
	$('.tab-pane').removeClass('show');
	$('#tab-03-tab.nav-link, #tab-03.tab-pane').addClass('active');
	$('#tab-03.tab-pane').addClass('show');
}
