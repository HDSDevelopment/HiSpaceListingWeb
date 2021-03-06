﻿function operatorFilterCount() {
	var opCount = $('.operatorCount:last-child').html();
	$('#opCount span').html(opCount);
};
$(document).ready(function () {
	$("#propertySearchHistory").prependTo("#propertySearchHistoryMove");
	$("#operatorSearchHistory").prependTo("#operatorSearchHistoryMove");
	$("#peopleSearchHistory").prependTo("#peopleSearchHistoryMove");
	operatorFilterCount();
});
var filterPropertyResult = $('#filterPropertyResult');
var detailPropertyResult = $('#detailPropertyResult');
//get the max price on select and checkbox
//$('body').on('change', '#Pr_Filter_For', function () {
//	console.log('test');
//	console.log($(this).val());
//});

//change the price range lable
$('#Pr_Filter_For').on('change', function () {
	console.log($(this).val());
	if ($(this).val() == "All") {
		$('.slider-labels__main .price-lable').html('Price/Rent Range');
	} else if ($(this).val() == "Rental") {
		$('.slider-labels__main .price-lable').html('Rent Range');
	} else if ($(this).val() == "Sale") {
		$('.slider-labels__main .price-lable').html('Price Range');
	}
});

//property filter form
function propertyFromSearchMethod(CurrentPage) {
	var methodName = "propertyFromSearchMethod";
	$("#filterPropertyResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterPropertyResult").offset().top - 100
	},
		'slow');
	//$("#filterPropertyResult").append("<div class='loader_new-sub'></div>");
	//console.log('property filter click');
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
	//set the value empty
	if (Pr_For == "All" || Pr_For == "Sale") {
		$('#pr_Filter_hour').prop('checked', false);
		$('#pr_Filter_day').prop('checked', false);
		$('#pr_Filter_month').prop('checked', false);
	}
	//hour
	if ($('#pr_Filter_hour').prop("checked") == true) {
		formData.append("IsPerformHour", true);
	} else {
		formData.append("IsPerformHour", false);
	}
	//day
	if ($('#pr_Filter_day').prop("checked") == true) {
		formData.append("IsPerformDay", true);
	} else {
		formData.append("IsPerformDay", false);
	}
	//month
	if ($('#pr_Filter_month').prop("checked") == true) {
		formData.append("IsPerformMonth", true);
	} else {
		formData.append("IsPerformMonth", false);
	}


	var Pr_PriceMin =  document.getElementsByName('PriceMin').value;
	formData.append("PriceMin", Pr_PriceMin);

	var Pr_PriceMax = document.getElementsByName('PriceMax').value;
	formData.append("PriceMax", Pr_PriceMax);

	//Current page
	formData.append("CurrentPage", CurrentPage);

	for (var pair of formData.entries()) {
    console.log(pair[0]+ ' - ' +pair[1]); 
	}

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
			$("#propertySearchHistoryMove").empty();
			$("#propertySearchHistory").prependTo("#propertySearchHistoryMove");
			//operatorFilterCount();
			createPaginationRows($('#page_property_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
};

//property history search
function propertySearchFunction(obj) {
	var methodName = "propertySearchFunction";
	var CurrentPage = 1;
	$("#filterPropertyResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterPropertyResult").offset().top - 100
	},
		'slow');
	var activeRow = $(obj).closest('.sh-data');
	console.log('property filter click');
	var formData = new FormData();
	var Pr_For = $(activeRow).find('.Pr_Filter_For').html();
	if (Pr_For != "All") {
		formData.append("CMCW_PropertyFor", Pr_For);
	} else {
		formData.append("CMCW_PropertyFor", "");
	}


	var Pr_ListingType = $(activeRow).find('.pr_Filter_ListingType').html();
	if (Pr_ListingType == "All") {
		formData.append("ListingType", "");
		formData.append("CommercialType", "");
		formData.append("CoworkingType", "");
	} else if (Pr_ListingType == "Commercial") {
		formData.append("ListingType", Pr_ListingType);
		var Pr_CommercialCategory = $(activeRow).find('.pr_Filter_CommercialCategory').html();
		if (Pr_CommercialCategory != "All") {
			formData.append("CommercialType", Pr_CommercialCategory);
		} else {
			formData.append("CommercialType", "");
		}
	}
	else if (Pr_ListingType == "Co-Working") {
		formData.append("ListingType", Pr_ListingType);
		var Pr_CoworkingCategory = $(activeRow).find('.pr_Filter_CoworkingCategory').html();
		if (Pr_CoworkingCategory != "All") {
			formData.append("CoworkingType", Pr_CoworkingCategory);
		} else {
			formData.append("CoworkingType", "");
		}
	}

	var Pr_Location = $(activeRow).find('.pr_Filter_PropertyLocation').html();
	if (Pr_Location != "All") {
		formData.append("Locality", Pr_Location);
	} else {
		formData.append("Locality", "");
	}

	//Health check
	if ($(activeRow).find('.pr_Filter_healthCheck').html() == "True") {
		formData.append("IsPerformGBC", true);
	} else {
		formData.append("IsPerformGBC", "");
	}
	//green building check
	if ($(activeRow).find('.pr_Filter_greenBuilding').html() == "True") {
		formData.append("IsPerformHealthCheck", true);
	} else {
		formData.append("IsPerformHealthCheck", "");
	}
	//console.log($('#pr_Filter_healthCheck').prop("checked"))
	//console.log($('#pr_Filter_greenBuilding').prop("checked"))
	//set the value empty
	//if (Pr_For == "All" || Pr_For == "Sale") {
	//	$('#pr_Filter_hour').prop('checked', false);
	//	$('#pr_Filter_day').prop('checked', false);
	//	$('#pr_Filter_month').prop('checked', false);
	//}
	//hour
	if ($(activeRow).find('.pr_Filter_hour').html() == "True") {
		formData.append("IsPerformHour", true);
	} else {
		formData.append("IsPerformHour", false);
	}
	//day
	if ($(activeRow).find('.pr_Filter_day').html() == "True") {
		formData.append("IsPerformDay", true);
	} else {
		formData.append("IsPerformDay", false);
	}
	//month
	if ($(activeRow).find('.pr_Filter_month').html() == "True") {
		formData.append("IsPerformMonth", true);
	} else {
		formData.append("IsPerformMonth", false);
	}


	var Pr_PriceMin = $(activeRow).find('.slider-range-value1').html();
	formData.append("PriceMin", Pr_PriceMin);

	var Pr_PriceMax = $(activeRow).find('.slider-range-value2').html();
	formData.append("PriceMax", Pr_PriceMax);

	for (var pair of formData.entries()) {
		console.log(pair[0] + ' - ' + pair[1]);
	}
	$.ajax({
		type: "POST",
		url: "/Filter/PropertyFilterCriteriaHistory",
		data: formData,
		dataType: "html",
		processData: false,
		contentType: false,
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			$("#propertySearchHistoryMove").empty();
			$("#propertySearchHistory").prependTo("#propertySearchHistoryMove");
			//operatorFilterCount();
			createPaginationRows($('#page_property_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}

			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
}
//property history delete
function propertySearchHistoryRemove(obj, id) {
	//id = $(obj).closest('.sh-data').find('.Pr_Filter_id').html();
	$("#detailPropertyResult").append("<div class='loader_new'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/DeletePropertySearchCriteria",
		data: { id: id },
		dataType: "html",
		success: function (response) {
			console.log(response);
			console.log($(obj).html());
			$(obj).closest('.sh-data').remove();
			removeLoader();
		},
		error: function (response) {
			alert("server not ready please try afterwards");
			removeLoader();
			//console.log(response);
		}
	});
}

//Property list all
function propertyListByAll(CurrentPage) {
	//var propertyModel = @Html.Raw(Json.Serialize(Model.Listings));
	//console.log(propertyModel);
	var methodName = "propertyListByAll";
	console.log(CurrentPage);
	$("#filterPropertyResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterPropertyResult").offset().top - 100
	},
		'slow');

	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByAll",
		data: { CurrentPage: CurrentPage},
		dataType: "html",
		success: function (response) {
			
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			$("#propertySearchHistoryMove").empty();
			$("#propertySearchHistory").prependTo("#propertySearchHistoryMove");
			//console.log($('#page_property_count').html());
			createPaginationRows($('#page_property_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
}

//pagination list function
function createPaginationRows(count, CurrentPage, methodName) {
	if (count > 5) {
		//goble declaration
		noOfButtons = count / 5;

		//console.log(noOfButtons);
		//console.log(typeof noOfButtons);
		//console.log(Number.isInteger(noOfButtons));
		if (!Number.isInteger(noOfButtons)) {
			noOfButtons = parseInt(noOfButtons + 1);
		}
	} else {
		noOfButtons = 0;
	}
		console.log(noOfButtons);
		for (var i = 1; i <= noOfButtons; i++) {
			//console.log('test' + i);
			if (methodName == "propertyListByAll") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(' + i + ');">' + i + '</a>');
			}
			else if (methodName == "propertyListByLocation") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(\'' + methodLocation + ' \',' + i + ');">' + i + '</a>');
			}
			else if (methodName == "propertyListByType") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(\'' + methodType + '\',' + i + ');">' + i + '</a>');
			}
			else if (methodName == "propertyListByUser") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(\'' + methodUser + '\',' + i + ');">' + i + '</a>');
			}
			else if ((methodName == "propertyFromSearchMethod") || (methodName == "propertySearchFunction")) {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_' + i + '" data-page="' + i + '" class="" onclick="' + "propertyFromSearchMethod" + '(' + i + ');">' + i + '</a>');
			}
			else if (methodName == "operatorListByAll") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_operator_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(' + i + ');">' + i + '</a>');
			}
			else if (methodName == "operatorListByUser") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_operator_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(\'' + methodOperatorUser+'\',' + i + ');">' + i + '</a>');
			}
			else if ((methodName == "operatorFromSearchMethod") || (methodName == "operatorSearchFunction")) {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_operator_' + i + '" data-page="' + i + '" class="" onclick="' + "operatorFromSearchMethod" + '(' + i + ');">' + i + '</a>');
			} 
			else if (methodName == "peopleListByAll") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_people_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(' + i + ');">' + i + '</a>');
			}
			else if (methodName == "peopleListByUser") {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_people_' + i + '" data-page="' + i + '" class="" onclick="' + methodName + '(\'' + methodListingId + '\',' + i + ');">' + i + '</a>');
			}
			else if ((methodName == "peopleFromSearchMethod") || (methodName == "peopleSearchFunction")) {
				$('.pageNumbers').append('<a href="javascript:void(0);" id="custom_page_people_' + i + '" data-page="' + i + '" class="" onclick="' + "peopleFromSearchMethod" + '(' + i + ');">' + i + '</a>');
			} 
		}
		//first,next,previous,last button function
		if (methodName == "propertyListByAll") {
			$('.firstPage').attr('onclick', methodName + '(1)');
			$('.previousPage').attr('onclick', methodName + '(' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(' + noOfButtons + ')');
		}
		else if (methodName == "propertyListByLocation") {
			$('.firstPage').attr('onclick', methodName + '(\'' + methodLocation + ' \',1)');
			$('.previousPage').attr('onclick', methodName + '(\'' + methodLocation + ' \',' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(\'' + methodLocation + ' \',' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(\'' + methodLocation + ' \',' + noOfButtons + ')');
		}
		else if (methodName == "propertyListByType") {
			$('.firstPage').attr('onclick', methodName + '(\'' + methodType + '\',1)');
			$('.previousPage').attr('onclick', methodName + '(\'' + methodType + '\',' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(\'' + methodType + '\',' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(\'' + methodType + '\',' + noOfButtons + ')');
		}
		else if (methodName == "propertyListByUser") {
			$('.firstPage').attr('onclick', methodName + '(\'' + methodUser + '\',1)');
			$('.previousPage').attr('onclick', methodName + '(\'' + methodUser + '\',' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(\'' + methodUser + '\',' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(\'' + methodUser + '\',' + noOfButtons + ')');
		}
		else if ((methodName == "propertyFromSearchMethod") || (methodName == "propertySearchFunction")) {
			$('.firstPage').attr('onclick', "propertyFromSearchMethod" + '(1)');
			$('.previousPage').attr('onclick', "propertyFromSearchMethod" + '(' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', "propertyFromSearchMethod" + '(' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', "propertyFromSearchMethod" + '(' + noOfButtons + ')');
		}
		else if (methodName == "operatorListByAll") {
			$('.firstPage').attr('onclick', methodName + '(1)');
			$('.previousPage').attr('onclick', methodName + '(' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(' + noOfButtons + ')');
		}
		else if (methodName == "operatorListByUser") {
			$('.firstPage').attr('onclick', methodName + '(\'' + methodOperatorUser +'\',1)');
			$('.previousPage').attr('onclick', methodName + '(\'' + methodOperatorUser +'\',' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(\'' + methodOperatorUser +'\',' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(\'' + methodOperatorUser +'\',' + noOfButtons + ')');
		}
		else if ((methodName == "operatorFromSearchMethod") || (methodName == "operatorSearchFunction")) {
			$('.firstPage').attr('onclick', "operatorFromSearchMethod" + '(1)');
			$('.previousPage').attr('onclick', "operatorFromSearchMethod" + '(' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', "operatorFromSearchMethod" + '(' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', "operatorFromSearchMethod" + '(' + noOfButtons + ')');
		}
		else if (methodName == "peopleListByAll") {
			$('.firstPage').attr('onclick', methodName + '(1)');
			$('.previousPage').attr('onclick', methodName + '(' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(' + noOfButtons + ')');
	}
		else if (methodName == "peopleListByUser") {
			$('.firstPage').attr('onclick', methodName + '(\'' + methodListingId + '\',1)');
			$('.previousPage').attr('onclick', methodName + '(\'' + methodListingId + '\',' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', methodName + '(\'' + methodListingId + '\',' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', methodName + '(\'' + methodListingId + '\',' + noOfButtons + ')');
	}
		else if ((methodName == "peopleFromSearchMethod") || (methodName == "peopleSearchFunction")) {
			$('.firstPage').attr('onclick', "peopleFromSearchMethod" + '(1)');
			$('.previousPage').attr('onclick', "peopleFromSearchMethod" + '(' + CurrentPage + '-1)');
			$('.nextPage').attr('onclick', "peopleFromSearchMethod" + '(' + CurrentPage + '+1)');
			$('.lastPage').attr('onclick', "peopleFromSearchMethod" + '(' + noOfButtons + ')');
		}
		//else if (methodName == "operatorSearchFunction") {
		//	$('.firstPage').attr('onclick', methodName + '(1)');
		//	$('.previousPage').attr('onclick', methodName + '(' + CurrentPage + '-1)');
		//	$('.nextPage').attr('onclick', methodName + '(' + CurrentPage + '+1)');
		//	$('.lastPage').attr('onclick', methodName + '(' + noOfButtons + ')');
		//}
		//active the select page button
		if ((methodName == "propertyListByAll") || (methodName == "propertyListByLocation") || (methodName == "propertyListByType") || (methodName == "propertyListByUser") || (methodName == "propertyFromSearchMethod") || (methodName == "propertySearchFunction")) {
			$('#custom_page_' + CurrentPage).addClass('active');
		} else if ((methodName == "operatorListByAll") || (methodName == "operatorListByUser") || (methodName == "operatorFromSearchMethod") || (methodName == "operatorSearchFunction")) {
			$('#custom_page_operator_' + CurrentPage).addClass('active');
		} else if ((methodName == "peopleListByAll") || (methodName == "peopleListByUser") || (methodName == "peopleFromSearchMethod")) {
			$('#custom_page_people_' + CurrentPage).addClass('active');
		}
		
	}

//Property details List by its userid
function propertyListByUserId(user) {
	$("#detailPropertyResult").append("<div class='loader_new'></div>");
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
			//PaginationCall();
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
function propertyListByLocation(location, CurrentPage) {
	var methodName = "propertyListByLocation";
	methodLocation = location;
	$("#filterPropertyResult").append("<div class='loader_new'></div>");
	$('html,body').animate({
		scrollTop: $("#filterPropertyResult").offset().top - 100
	},
		'slow');
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByLocation",
		data: { Location: location, CurrentPage: CurrentPage },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			$("#propertySearchHistoryMove").empty();
			$("#propertySearchHistory").prependTo("#propertySearchHistoryMove");
			//console.log($('#page_property_count').html());
			createPaginationRows($('#page_property_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Property Type filter
function propertyListByType(type, CurrentPage) {
	var methodName = "propertyListByType";
	methodType = type;
	$("#filterPropertyResult").append("<div class='loader_new'></div>");
	$('html,body').animate({
		scrollTop: $("#filterPropertyResult").offset().top - 100
	},
		'slow');
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByType",
		data: { ListingType: type, CurrentPage: CurrentPage },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			$("#propertySearchHistoryMove").empty();
			$("#propertySearchHistory").prependTo("#propertySearchHistoryMove");
			createPaginationRows($('#page_property_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Property User filter
function propertyListByUser(user, CurrentPage) {
	var methodName = "propertyListByUser";
	methodUser = user;
	$("#filterPropertyResult").append("<div class='loader_new'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/PropertyListByUser",
		data: { User: user, CurrentPage: CurrentPage },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterPropertyResult.html('');
			filterPropertyResult.html(response);
			$("#propertySearchHistoryMove").empty();
			$("#propertySearchHistory").prependTo("#propertySearchHistoryMove");
			createPaginationRows($('#page_property_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
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
	$("#detailPropertyResult").append("<div class='loader_new'></div>");
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
function operatorListByAll(CurrentPage) {
	var methodName = "operatorListByAll";
	$("#filterOperatorResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterOperatorResult").offset().top - 100
	},
		'slow');
	$.ajax({
		type: "GET",
		url: "/Filter/OperatorListByAll",
		data: { CurrentPage: CurrentPage },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterOperatorResult.html('');
			filterOperatorResult.html(response);
			$("#operatorSearchHistoryMove").empty();
			$("#operatorSearchHistory").prependTo("#operatorSearchHistoryMove");
			createPaginationRows($('#page_operator_count').html(), CurrentPage, methodName);
			operatorFilterCount();
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_operator_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}
//Operator list by operator
function operatorListByUser(user, CurrentPage) {
	var methodName = "operatorListByUser";
	methodOperatorUser = user;
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterOperatorResult").offset().top - 100
	},
		'slow');
	$("#filterOperatorResult").append("<div class='loader_new'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/OperatorListByUserId",
		data: { User: user, CurrentPage: CurrentPage },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterOperatorResult.html('');
			filterOperatorResult.html(response);
			$("#operatorSearchHistoryMove").empty();
			$("#operatorSearchHistory").prependTo("#operatorSearchHistoryMove");
			createPaginationRows($('#page_operator_count').html(), CurrentPage, methodName);
			operatorFilterCount();
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_operator_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
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
function operatorFromSearchMethod(CurrentPage) {
	var methodName = "operatorFromSearchMethod";
	$("#filterOperatorResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterOperatorResult").offset().top - 100
	},
		'slow');
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
	//Current page
	formData.append("CurrentPage", CurrentPage);

	for (var pair of formData.entries()) {
		console.log(pair[0] + ' - ' + pair[1]);
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
			$("#operatorSearchHistoryMove").empty();
			$("#operatorSearchHistory").prependTo("#operatorSearchHistoryMove");
			createPaginationRows($('#page_operator_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_operator_' + CurrentPage).position().left);
				console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}

			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
};
//operator search history
function operatorSearchFunction(obj) {
	var methodName = "operatorSearchFunction";
	var CurrentPage = 1;
	$("#filterOperatorResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterOperatorResult").offset().top - 100
	},
		'slow');
	var formData = new FormData();
	var activeRow = $(obj).closest('.sh-data');

	var OpCity = $(activeRow).find('.Op_Filter_CityName').html();
	if (OpCity != "All") {
		formData.append("CityName", OpCity);
	} else {
		formData.append("CityName", "");
	}
	var OpOperator = $(activeRow).find('.Op_Filter_OperatorName').html();
	if (OpOperator != "All") {
		formData.append("OperatorName", OpOperator);
	} else {
		formData.append("OperatorName", "");
	}

	for (var pair of formData.entries()) {
		console.log(pair[0] + ' - ' + pair[1]);
	}

	$.ajax({
		type: "POST",
		url: "/Filter/OperatorFilterCriteriaHistory",
		data: formData,
		dataType: "html",
		processData: false,
		contentType: false,
		success: function (response) {
			//console.log(response);
			filterOperatorResult.html('');
			filterOperatorResult.html(response);
			$("#operatorSearchHistoryMove").empty();
			$("#operatorSearchHistory").prependTo("#operatorSearchHistoryMove");
			createPaginationRows($('#page_operator_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_operator_' + CurrentPage).position().left);
				console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}

			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
}
//operator history delete
function operatorSearchHistoryRemove(obj, id) {
	//id = $(obj).closest('.sh-data').find('.Pr_Filter_id').html();
	$("#detailPropertyResult").append("<div class='loader_new'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/DeleteOperatorSearchCriteria",
		data: { id: id },
		dataType: "html",
		success: function (response) {
			console.log(response);
			console.log($(obj).html());
			$(obj).closest('.sh-data').remove();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
			//console.log(response);
		}
	});
}

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
function peopleFromSearchMethod(CurrentPage) {
	var methodName = "peopleFromSearchMethod";
	$("#filterProfessionalResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterProfessionalResult").offset().top - 100
	},
		'slow');
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
	//Current page
	formData.append("CurrentPage", CurrentPage);
	for (var pair of formData.entries()) {
    console.log(pair[0]+ ' - ' +pair[1]); 
	}
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
			$("#peopleSearchHistoryMove").empty();
			$("#peopleSearchHistory").prependTo("#peopleSearchHistoryMove");
			Peoplecarousel();
			createPaginationRows($('#page_people_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_people_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
};

var filterProfessionalResult = $('#filterProfessionalResult');
//People list all
function peopleListByAll(CurrentPage) {
	var methodName = "peopleListByAll";
	$("#filterProfessionalResult").append("<div class='loader_new'></div>");
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterProfessionalResult").offset().top - 100
	},
		'slow');
	$.ajax({
		type: "GET",
		url: "/Filter/PeopleListByAll",
		data: { CurrentPage: CurrentPage },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterProfessionalResult.html('');
			filterProfessionalResult.html(response);
			$("#peopleSearchHistoryMove").empty();
			$("#peopleSearchHistory").prependTo("#peopleSearchHistoryMove");
			Peoplecarousel();
			createPaginationRows($('#page_people_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_people_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});

} 

//People list by ListingId
function peopleListByUser(listingId, CurrentPage) {
	var methodName = "peopleListByUser";
	methodListingId = listingId;
	$("#filterProfessionalResult").append("<div class='loader_new'></div>");
	$('html,body').animate({
		scrollTop: $("#filterProfessionalResult").offset().top - 100
	},
		'slow');
	$.ajax({
		type: "GET",
		url: "/Filter/PeopleListByListingId",
		data: { ListingId: listingId, CurrentPage: CurrentPage },
		dataType: "html",
		success: function (response) {
			//console.log(response);
			filterProfessionalResult.html('');
			filterProfessionalResult.html(response);
			$("#peopleSearchHistoryMove").empty();
			$("#peopleSearchHistory").prependTo("#peopleSearchHistoryMove");
			Peoplecarousel();
			createPaginationRows($('#page_people_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_people_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
}
//people search history
function peopleSearchFunction(obj) {
	var methodName = "peopleSearchFunction";
	var CurrentPage = 1;
	//scroll animation
	$('html,body').animate({
		scrollTop: $("#filterProfessionalResult").offset().top - 100
	},
		'slow');
	$("#filterProfessionalResult").append("<div class='loader_new'></div>");
	var formData = new FormData();
	var activeRow = $(obj).closest('.sh-data');

	var Pe_Role = $(activeRow).find('.Pe_Filter_Role').html();
	if (Pe_Role != "All") {
		formData.append("Role", Pe_Role);
	} else {
		formData.append("Role", "");
	}

	var Pe_Location = $(activeRow).find('.pe_Filter_PeopleLocation').html();
	if (Pe_Location != "All") {
		formData.append("Locality", Pe_Location);
	} else {
		formData.append("Locality", "");
	}
	var Pe_Fname = $(activeRow).find('.Pe_Filter_PeopleFname').html();
	if (Pe_Fname != "All") {
		formData.append("FirstName", Pe_Fname);
	} else {
		formData.append("FirstName", "");
	}
	var Pe_Lname = $(activeRow).find('.Pe_Filter_PeopleLname').html();
	if (Pe_Lname != "All") {
		formData.append("LastName", Pe_Lname);
	} else {
		formData.append("LastName", "");
	}

	for (var pair of formData.entries()) {
		console.log(pair[0] + ' - ' + pair[1]);
	}

	$.ajax({
		type: "POST",
		url: "/Filter/PeopleFilterCriteriaHistory",
		data: formData,
		dataType: "html",
		processData: false,
		contentType: false,
		success: function (response) {
			//console.log(response);
			filterProfessionalResult.html('');
			filterProfessionalResult.html(response);
			$("#peopleSearchHistoryMove").empty();
			$("#peopleSearchHistory").prependTo("#peopleSearchHistoryMove");
			Peoplecarousel();
			createPaginationRows($('#page_people_count').html(), CurrentPage, methodName);
			//PaginationCall();
			if (CurrentPage == 1) {
				$('.firstPage, .previousPage').addClass('opacity-pointer-none');
			}
			if (noOfButtons == CurrentPage) {
				$('.nextPage, .lastPage').addClass('opacity-pointer-none');
			}
			if (CurrentPage > 1) {
				var btnParentLocation = parseInt($('#custom_page_people_' + CurrentPage).position().left);
				//console.log(btnParentLocation);
				$("div.pageNumbers").scrollLeft(btnParentLocation - 350);
				//console.log($('.pageNumbers').width());
			}
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
		}
	});
}
//people history delete
function peopleSearchHistoryRemove(obj, id) {
	//id = $(obj).closest('.sh-data').find('.Pr_Filter_id').html();
	$("#detailPropertyResult").append("<div class='loader_new'></div>");
	$.ajax({
		type: "GET",
		url: "/Filter/DeletePeopleSearchCriteria",
		data: { id: id },
		dataType: "html",
		success: function (response) {
			console.log(response);
			console.log($(obj).html());
			$(obj).closest('.sh-data').remove();
			removeLoader();
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please try afterwards");
			//console.log(response);
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

