function operatorFilterCount() {
	var opCount = $('.operatorCount:last-child').html();
	$('#opCount span').html(opCount);
};
$(document).ready(function () {
	
	operatorFilterCount();
});
var filterPropertyResult = $('#filterPropertyResult');
var detailPropertyResult = $('#detailPropertyResult');

//get the max price on select and checkbox
//$('body').on('change', '#Pr_Filter_For', function () {
//	console.log('test');
//	console.log($(this).val());
//});


//property filter form
$('#property-form-submit').on('click', function (e) {
	$("#filterPropertyResult").append("<div class='loader_new-sub'></div>");
	console.log('property filter click');
		var formData = new FormData();
	var Pr_For = $('#Pr_Filter_For').val();
	if (Pr_For != "All") {
		formData.append("CMCW_PropertyFor", Pr_For);
	} else {
		formData.append("CMCW_PropertyFor", "");
	}

	var Pr_ListingType = $('#pr_Filter_ListingType').val();
	if (Pr_ListingType == "All") {
		formData.append("ListingType", "");
		formData.append("CommercialType", "");
		formData.append("CoworkingType", "");
	} else if(Pr_ListingType == "Commercial") {
		formData.append("ListingType", Pr_ListingType);
		var Pr_CommercialCategory = $('#pr_Filter_CommercialCategory').val();
		if (Pr_CommercialCategory != "All") {
			formData.append("CommercialType", Pr_CommercialCategory);
		} else {
			formData.append("CommercialType", "");
		}
	}
	else if(Pr_ListingType == "Co-Working") {
		formData.append("ListingType", Pr_ListingType);
		var Pr_CoworkingCategory = $('#pr_Filter_CoworkingCategory').val();
		if (Pr_CoworkingCategory != "All") {
			formData.append("CoworkingType", Pr_CoworkingCategory);
		} else {
			formData.append("CoworkingType", "");
		}
	}

	var Pr_Location = $('#pr_Filter_PropertyLocation').val();
	if (Pr_Location != "All") {
		formData.append("Locality", Pr_Location);
	} else {
		formData.append("Locality", "");
	}

	//Health check
	if ($('#pr_Filter_healthCheck').prop("checked") == true) {
		formData.append("IsPerformGBC", true);
	} else {
		formData.append("IsPerformGBC", "");
	}
	//green building check
	if ($('#pr_Filter_greenBuilding').prop("checked") == true) {
		formData.append("IsPerformHealthCheck", true);
	} else {
		formData.append("IsPerformHealthCheck", "");
	}
	//console.log($('#pr_Filter_healthCheck').prop("checked"))
	//console.log($('#pr_Filter_greenBuilding').prop("checked"))



	var Pr_PriceMin =  document.getElementsByName('PriceMin').value;
	formData.append("PriceMin", Pr_PriceMin);

	var Pr_PriceMax = document.getElementsByName('PriceMax').value;
	formData.append("PriceMax", Pr_PriceMax);

	//for (var pair of formData.entries()) {
 //   console.log(pair[0]+ ' - ' +pair[1]); 
	//}

	$.ajax({
		type: "POST",
		url: "/Filter/PropertyFilterCriteria",
		data: formData,
		dataType: "html",
		processData: false,
		contentType: false,
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			operatorFilterCount();
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
});
//Property list all
function propertyListByAll() {
	$("#filterPropertyResult").append("<div class='loader_new-sub'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByAll",
		data: {},
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Property details List by its userid
function propertyListByUserId(user) {
	$("#detailPropertyResult").append("<div class='loader_new-sub'></div>");
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
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
			//console.log(response);
		}
	});
	
} 
//Property Location filter
function propertyListByLocation(location) {
	$("#filterPropertyResult").append("<div class='loader_new-sub'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByLocation",
		data: { Location: location },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Property Type filter
function propertyListByType(type) {
	$("#filterPropertyResult").append("<div class='loader_new-sub'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByType",
		data: { Type: type },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Property User filter
function propertyListByUser(user) {
	$("#filterPropertyResult").append("<div class='loader_new-sub'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByUser",
		data: { User: user },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Property List by its userid
function propertyListByUserIdAndListingId(user, listingId) {
	$("#detailPropertyResult").append("<div class='loader_new-sub'></div>");
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
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
			//console.log(response);
		}
	});
	
} 
//property list category filter
$(document).on('change', '#pr_Filter_ListingType', function (event) {
	var listingType = $(this).val();
	if (listingType == "All") {
		$('.CommercialCategory-Main').addClass('display-none');
		$('.CoworkingCategory-Main').addClass('display-none');
	} else if (listingType == "Commercial") {
		$('.CommercialCategory-Main').removeClass('display-none');
		$('.CoworkingCategory-Main').addClass('display-none');
	} else if (listingType == "Co-Working") {
		$('.CommercialCategory-Main').addClass('display-none');
		$('.CoworkingCategory-Main').removeClass('display-none');
	}
});


var filterOperatorResult = $('#filterOperatorResult');
//Operator list all
function operatorListByAll() {
	$("#filterOperatorResult").append("<div class='loader_new-sub'></div>");
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
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Operator list by operator
function operatorListByUser(user) {
	$("#filterOperatorResult").append("<div class='loader_new-sub'></div>");
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
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
}
//Operator list by filter form
$(document).on('change', '.OperatorLocation', function (event) {
	var operatorListTag = $(this).closest('.form-group').siblings('.OperatorName-Main').find(".OperatorName");
	$.ajax({
		type: "GET",
		url: "/Filter/GetOperatorsListForFilter",
		data: { Location: $('#Op_Filter_CityName').val() },
		context: this,
		dataType: "json",
		success: function (response) {
			console.log(response);
			//console.log($(this).closest('.operator-main-div').html())
			operatorListTag.html("");
			for (var key in response) {
				if (response.hasOwnProperty(key)) {
					//console.log(response[key].companyName)
					if (response[key].companyName != null) {

						operatorListTag.append("<option value="+response[key].companyName+">" + response[key].companyName + "</option>");
					}
				}
			}
			operatorListTag.select2();
			//console.log($('.propertyDropdown').html());
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
});
//operator filter form
$('#operator-form-submit').on('click', function (e) {
	$("#filterOperatorResult").append("<div class='loader_new-sub'></div>");
		var formData = new FormData();
	var OpCity = $('#Op_Filter_CityName').val();
	if (OpCity != "All") {
		formData.append("CityName", OpCity);
	} else {
		formData.append("CityName", "");
	}
	var OpOperator = $('#Op_Filter_OperatorName').select2('data')[0]['text'];
	if (OpOperator != "All") {
		formData.append("OperatorName", OpOperator);
	} else {
		formData.append("OperatorName", "");
	}

	$.ajax({
		type: "POST",
		url: "/Filter/OperatorFilterCriteria",
		data: formData,
		dataType: "html",
		processData: false,
		contentType: false,
		success: function (response) {
			//console.log(response);
			filterOperatorResult.html('');
			filterOperatorResult.html(response);
			operatorFilterCount();
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
});


//people list by filter form
$(document).on('change', '.PeopleLocation', function (event) {
	//console.log('test')
	var peopleListTag = $(this).closest('.form-group').siblings('.PeopleName-Main').find(".PeopleName");
	$.ajax({
		type: "GET",
		url: "/Filter/GetPeopleListForFilter",
		data: { Location: $('#pe_Filter_PeopleLocation').val() },
		context: this,
		dataType: "json",
		success: function (response) {
			console.log(response);
			//console.log($(this).closest('.operator-main-div').html())
			peopleListTag.html("");
			for (var key in response) {
				if (response.hasOwnProperty(key)) {
					//console.log(response[key].companyName)
					if (response[key].rE_FullName != null) {

						peopleListTag.append("<option value="+response[key].rE_FullName+">" + response[key].rE_FullName + "</option>");
					}
				}
			}
			peopleListTag.select2();
			//console.log($('.propertyDropdown').html());
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
});
//people list
//people filter form
$('#people-form-submit').on('click', function (e) {
	$("#filterProfessionalResult").append("<div class='loader_new-sub'></div>");
		var formData = new FormData();
	var Pe_Role = $('#Pe_Filter_Role').val();
	if (Pe_Role != "All") {
		formData.append("Role", Pe_Role);
	} else {
		formData.append("Role", "");
	}

	var Pe_Location = $('#pe_Filter_PeopleLocation').val();
	if (Pe_Location != "All") {
		formData.append("Locality", Pe_Location);
	} else {
		formData.append("Locality", "");
	}

	var pe_Name = $('#Pe_Filter_People').select2('data')[0]['text'];
	console.log(pe_Name);
	if (pe_Name != "All") {
		var pe_NameSplit = pe_Name.split("-");
		//console.log(pe_NameSplit)
		formData.append("FirstName", pe_NameSplit[0]);
		formData.append("LastName", pe_NameSplit[1]);
	} else {
		formData.append("FirstName", "");
		formData.append("LastName", "");
	}
	//for (var pair of formData.entries()) {
 //   console.log(pair[0]+ ' - ' +pair[1]); 
	//}
	$.ajax({
		type: "POST",
		url: "/Filter/PeopleFilterCriteria",
		data: formData,
		dataType: "html",
		processData: false,
		contentType: false,
		success: function (response) {
			//console.log(response);
			filterProfessionalResult.html('');
			filterProfessionalResult.html(response);
			Peoplecarousel();
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
});

var filterProfessionalResult = $('#filterProfessionalResult');
//People list all
function peopleListByAll() {
	$("#filterProfessionalResult").append("<div class='loader_new-sub'></div>");
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
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});

} 

//People list by ListingId
function peopleListByUser(listingId) {
	$("#filterProfessionalResult").append("<div class='loader_new-sub'></div>");
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
			PaginationCall();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
}
$('body').on('click', '.listNavigation', function () {
	console.log('test');
});
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
	$('.filter-select:visible').append('<select class="form-control basic-select dd-filter property_select">'
		 +'<option selected value="tab-01">Property/Space</option>'
		 +'<option value="tab-02">Owner/Operator</option>'
		 +'<option value="tab-03">RE Professionals/People</option>'
	+'</select>');
} else if (listShowType == 2) {
	$('.nav-link, .tab-pane').removeClass('active');
	$('.tab-pane').removeClass('show');
	$('#tab-02-tab.nav-link, #tab-02.tab-pane').addClass('active');
	$('#tab-02.tab-pane').addClass('show');
	$('.filter-select:visible').append('<select class="form-control basic-select dd-filter operator_select">'
		 +'<option value="tab-01">Property/Space</option>'
		 +'<option selected value="tab-02">Owner/Operator</option>'
		 +'<option value="tab-03">RE Professionals/People</option>'
	+'</select>');

} else if (listShowType == 3) {
	$('.nav-link, .tab-pane').removeClass('active');
	$('.tab-pane').removeClass('show');
	$('#tab-03-tab.nav-link, #tab-03.tab-pane').addClass('active');
	$('#tab-03.tab-pane').addClass('show');
	$('.filter-select:visible').append('<select class="form-control basic-select dd-filter people_select">'
		 +'<option value="tab-01">Property/Space</option>'
		 +'<option value="tab-02">Owner/Operator</option>'
		 +'<option selected value="tab-03">RE Professionals/People</option>'
	+'</select>');
}


//professional sliders section
Peoplecarousel = function () {
	//console.log('call carousel')
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
                        items: $this.data('xs-items') ? $this.data('xs-items') : 2
                    },
                    768: {
                        items: $this.data('sm-items') ? $this.data('sm-items') : 3
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

