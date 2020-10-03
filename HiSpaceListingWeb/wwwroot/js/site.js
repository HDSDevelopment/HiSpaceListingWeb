// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$('#DOJ, #DOR, #FromDate, #ToDate').datetimepicker({
//	//debug: true,
//	format: 'YYYY-MM-DD',
//	//daysOfWeekDisabled: [1, 2, 3, 4, 5],
//});

//add padding to the top for scrolling
//$(window).on('scroll', function () {
//	if ($(".navbar").hasClass("is-sticky")) {
//		$('.scroll-padding').css({ "padding-top": "150px" });
//	} else {
//		$('.scroll-padding').css({ "padding-top": "0" });
//	}
//});

//$(document).ready(function() {

//    // Executes when the HTML document is loaded and the DOM is ready
//    $('body').addClass('body-height__adjust')
//});


////Loader and ready
//$(window).on('load', function () {
//	$('body').addClass('body-height__adjust')
//	$('body').append('<div style="" id="loadingDiv"><div class="loader">Loading...</div></div>');	
//	setTimeout(removeLoader, 2000000); //wait for page load PLUS two seconds.
//});
//function removeLoader() {
//	$("#loadingDiv").fadeOut(500, function () {
//		//consol.log('test')
//		// fadeOut complete. Remove the loading div
//		$("#loadingDiv").remove(); //makes page more lightweight 
//		$('body').removeClass('body-height__adjust')
		
//	});
//}
/*---------------------------------------------------------------------*/
/*-------------------New Window style start----------------------------*/
/*---------------------------------------------------------------------*/
function newWindow(url){
	window.open(url, "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,width=1200,height=750,top=20,left=10");
}

/*---------------------------------------------------------------------*/
/*-------------------New Window style end------------------------------*/
/*---------------------------------------------------------------------*/
$(window).on('load', function() {
    $(".loader_new-sub").fadeOut("slow");
    $(".loader_new").fadeOut("slow");
});
//Remove the sub loader
function removeLoader() {
	setTimeout(function () {
$(".loader_new-sub").fadeOut("slow");
	$(".loader_new-sub").remove();
	}, 100);
	
};

//Stop Form Submission of Enter Key Press
function stopRKey(evt) {
	var evt = (evt) ? evt : ((event) ? event : null);
	var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
	if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;

$('#BuildYear, #RecentInnovation, #BuildYearDiv, #RecentInnovationDiv').datetimepicker({
	//viewMode: 'years',
	//format: 'LT'
	format: 'DD-MM-YYYY',
	//debug: true,
});
function userCheck(check) {
	if (check == 0) {
		return confirm("Are your sure want to approve the user as background details checked?");
	}
	else if (check == 1) {
		return confirm("Are your sure want to recheck the user as background details?");
	}
}
//admin level user check
function adminLevelUserCheck(check) {
	if (check == 0) {
		return confirm("Are your sure want to block the HiSpace user?");
	}
	else if (check == 1) {
		return confirm("Are your sure want to activate the HiSpace user?");
	}
}
//user level listing check
function userLevelListCheck(check) {
	if (check == 0) {
		return confirm("Are your sure want to deactivate the space/professional?");
	}
	else if (check == 1) {
		return confirm("Are your sure want to activate the space/professional?");
	}
}
//Admin level listing check
function adminLevelListCheck(check) {
	if (check == 0) {
		return confirm("Are your sure want to deactivate the space/professional?");
	}
	else if (check == 1) {
		return confirm("Are your sure want to activate the space/professional?");
	}
}
function reCheck(check) {
	if (check == 0) {
		return confirm("Are your sure want to approve the Real-Estate Professional?");
	}
	else if (check == 1) {
		return confirm("Are your sure want to disapprove the Real-Estate Professional?");
	}
}
//checking already signed user
function errAlreadySignedUp(email,obj) {
	//alert('In errAlreadySignedUp');
	//console.log($(obj).html())
	$.ajax({
		type: "GET",
		url: "/User/CheckAlreadySignedUp",
		data: { Email: email },
		context: this,
		//dataType: "html",
		success: function (response) {
			console.log(response);
			if (response == "1") {
				//console.log($(obj).html());
				$(obj).siblings('.error').html('<span>Already signed up</span>');
				$(obj).val('');
			}
			else{
				$(obj).siblings('.error').html('<span></span>');
			}
		},
		error: function (response) {
			alert("server not ready please try afterwards");
		}
	});
}
//pagination 
function PaginationCall() {
	$(".paginate").paginga({
		// use default options
		itemsPerPage: 5
	});
};
//Get the location name using the geolocator
function getLocationUsingPostalCode(postal, obj) {
	console.log(postal)
	if (postal != "" || postal != null) {
		$.ajax({
			type: "GET",
			url: "https://maps.googleapis.com/maps/api/geocode/json?address=" + postal + "&key=AIzaSyDGZoZkDdeXNlOPbKUG1KIXvBY5cUgQQgk",
			context: this,
			//dataType: "html",
			success: function (response) {
				console.log(response.results[0]);
				var signup_location, signup_state, signup_country;
				if (response.status == "OK") {
					//console.log(response.status);
					var address_components = response.results[0].address_components;
					$.each(address_components, function (index, component) {
						var types = component.types;
						$.each(types, function (index, type) {
							if (type == 'administrative_area_level_2') {
								signup_location = component.long_name;
								$('.sign-city').val(signup_location);
							}
							if (type == 'administrative_area_level_1') {
								signup_state = component.long_name;
								$('.sign-state').val(signup_state);
							}
							if (type == 'country') {
								signup_country = component.long_name;
								$('.sign-country').val(signup_country);
							}
						});
					});
					console.log(signup_location + ", " + signup_state + ", " + signup_country);
					console.log($('.sign-country').val())
				} else {
					$('#sign-Postalcode').siblings('.error').html("Invalid Postal Code");
					$(obj).siblings('.error').html("Invalid Postal Code");
				}
			},
			error: function (response) {
				alert("Please fill the postal code");
			}
		});
	}
}


$(document).ready(function () {
	
	PaginationCall();
	// Initialize Editor
	$(document).on('click', '.page-nav', function () {
		PaginationCall();
	});
	// Initialize Editor
	$('.textarea-editor').summernote({
		//height: 200, // set editor height
		minHeight: 150 // set minimum height of editor
		//maxHeight: null, // set maximum height of editor
		//focus: true // set focus to editable area after initializing summernote
	});
	//signup radio button change
	//$('#signup input[type=radio][name=UserType]').change(function () {
	$(document).on('change','#signup input[type=radio][name=UserType]', function () {
		//operator
		if (this.value == '1') {
			$('.operator-fields').removeClass('d-none');
			$('#sign-CompanyName + label').html('Operator/Company Name*');
		}
		//user
		else if (this.value == '2') {
			$('.operator-fields').addClass('d-none');
			$('#sign-CompanyName + label').html('Name*');
		}
	});
	//addons status alert section start
	$('.status-alert__amenities').each(function (i, obj) {
		if ($(this).html() == 0) {
			$(this).addClass('status-alert__empty');
			$(this).attr('data-original-title','Please add the Amenities to the property')
		}
	});
	$('.status-alert__facilities').each(function (i, obj) {
		if ($(this).html() == 0) {
			$(this).addClass('status-alert__empty');
			$(this).attr('data-original-title', 'Please add the Facilities to the property')
		}
	});
	$('.status-alert__projects').each(function (i, obj) {
		if ($(this).html() == 0) {
			$(this).addClass('status-alert__empty');
			$(this).attr('data-original-title', 'Please add your project inside the RE-Professionals')
		}
	});
	$('.status-alert__hc').each(function (i, obj) {
		//console.log($(this).attr('status-check'))
		if ($(this).attr('status-check') == 0) {
			$(this).addClass('status-alert__empty');
			$(this).html('<i class="fas fa-frown"></i>');
			$(this).attr('data-original-title', 'Please check the health of the property')
		}
	});
	$('.status-alert__gbc').each(function (i, obj) {
		//console.log($(this).attr('status-check'))
		if ($(this).attr('status-check') == 0) {
			$(this).addClass('status-alert__empty');
			$(this).attr('data-original-title', 'Please fill the green building check for the property')
		}
	});
	//addons status alert section end
	var optionsWeb = {
		step_size: 1,
		symbols: {
			fontawesome_star: {
				base: '<i class="far fa-star"></i>',
				hover: '<i class="fas fa-star"></i>',
				selected: '<i class="fas fa-star text-sec"></i>',
			},
		},
		selected_symbol_type: 'fontawesome_star',
		cursor: 'pointer',
	}
	$(".rate-web").rate(optionsWeb);
	$('.v2,.v3').addClass('d-none');
	$("#cartBtn-1, #cartBtn-2, #cartBtn-3").click(function () {
		var sec = $(this).attr('cart-section');
		//console.log(sec);
		$('html, body').animate({
			scrollTop: $("#card-" + sec).offset().top - 50
		}, 1000);
	});
	$('body').on('click', '#f-common', function () {
		$('.modal').animate({
			scrollTop: $("#custom-section__f").offset().top
		}, 1000);
	})
	$('body').on('click', '#f_custom', function () {
		$('.modal').animate({
			scrollTop: '0px'
		}, 1000);
	})
	$('body').on('click', '#a-common', function () {
		$('.modal').animate({
			scrollTop: $("#custom-section__a").offset().top
		}, 1000);
	})
	$('body').on('click', '#a_custom', function () {
		$('.modal').animate({
			scrollTop: '0px'
		}, 1000);
	})



	//$('#datetimepicker-01, #datetimepicker-02, #RecentInnovation,#datetimepicker-recent-cw, #BuildYear,#datetimepicker-builtyear-cw').datetimepicker({
	//	format: 'L'
	//});
	//$('#datetimepicker-03, #datetimepicker-04').datetimepicker({
	//	format: 'LT'
	//});

	$('#listingTable').DataTable();
	$('#linkOperators').DataTable();
	$('#userEnquiry').DataTable();
	$('#hispaceUserList').DataTable();
	$('#hispaceUser').DataTable({
		"order": [[0, "desc"]]
	});

	//$('#example tbody').on('click', 'tr', function () {
	//	var data = table.row(this).data();
	//	alert('You clicked on ' + data[0] + '\'s row');
	//});
	//tab navigation section
	if ($('.hi-tab .active.show').attr('data-id')) {
		$('.tab-back-btn, .tab-submit').css('display', 'none');
	}

	//filter selection
	$('#filter-type').on('change', function () {
		var value = $(this).val();
		//alert(value)
		if (value == "Commercial") {
			$('.v2,.v3').addClass('d-none');
			$('.v1,.price_range').removeClass('d-none');
		}
		else if (value == "Co-working") {
			$('.v1,.v3').addClass('d-none');
			$('.v2,.price_range').removeClass('d-none');
		}
		else if (value == "RE-Professionals") {
			$('.v1,.v2,.price_range').addClass('d-none');
			$('.v3').removeClass('d-none');
		}
	});

	//edit section on listing form 
	
	if ($('#ListingType').val() == "Commercial") {
		$('.form-type').removeClass('type-2');
		$('.form-type').removeClass('type-3');
		$('.form-type').addClass('type-1');
		$('.type-2__input, .type-3, .type-style.type-2, .type-style.type-3, .type-2-sub__input').addClass('d-none');
		$('.type-1__input, .type-style.type-1, .form-type.type-1, .input-occupancy').removeClass('d-none');
		if ($('input[name="CMCW_PropertyFor"]:checked').val() == "Rental") {
			$('.for-rental').removeClass('d-none');
			$('.for-sale').addClass('d-none');
		} else if ($('input[name="CMCW_PropertyFor"]:checked').val() == "Sale") {
			$('.for-rental').addClass('d-none');
			$('.for-sale').removeClass('d-none');
		}
		//console.log($('input[name="CMCW_PropertyFor"]:checked').val());
	} else if ($('#ListingType').val() == "Co-Working") {
		//alert($('#ListingType').val())
		$('.form-type').removeClass('type-1');
		$('.form-type').removeClass('type-3');
		$('.form-type').addClass('type-2');
		$('.type-1__input, .type-3, .type-style.type-1, .type-style.type-3').addClass('d-none');
		$('.type-2__input, .type-style.type-2, .form-type.type-2, .type-2-sub__input, .input-occupancy').removeClass('d-none');
		if ($('input[name="CMCW_PropertyFor"]:checked').val() == "Rental") {
			$('.for-rental').removeClass('d-none');
			$('.for-sale').addClass('d-none');
		} else if ($('input[name="CMCW_PropertyFor"]:checked').val() == "Sale") {
			$('.for-rental').addClass('d-none');
			$('.for-sale').removeClass('d-none');
		}
		if ($('#CoworkingType').val() == "Office") {
			//alert($('#CoworkingType').val())
			$('.type-2-sub__input, .input-occupancy').removeClass('d-none');

			if ($('#CW_Coworking').val() != "") {
				//console.log($('#CW_Coworking').parents().closest('.check-view').html())
				$('#CW_Coworking').parents().closest('.check-view').removeClass('d-none');
				$('#CW_Coworking').parents().closest('.check-view').siblings('.checkbox').find('input').attr('checked', true);
			}
			if ($('#CW_PrivateOffice').val() != "") {
				$('#CW_PrivateOffice').parents().closest('.check-view').removeClass('d-none');
				$('#CW_PrivateOffice').parents().closest('.check-view').siblings('.checkbox').find('input').attr('checked', true);
			}
			if ($('#CW_MeetingRoom').val() != "") {
				$('#CW_MeetingRoom').parents().closest('.check-view').removeClass('d-none');
				$('#CW_MeetingRoom').parents().closest('.check-view').siblings('.checkbox').find('input').attr('checked', true);
			}
		} else if ($('#CoworkingType').val() == "Cafe") {
			//alert($('#CoworkingType').val())
			$('.type-2-sub__input, .input-occupancy').addClass('d-none');
		}
		
		


	} else if ($('#ListingType').val() == "RE-Professional") {
		$('.type-3').removeClass('d-none');
		$('.form-type.type-1, .form-type.type-2, .type-style.type-1, .type-style.type-2').addClass('d-none');
	}


	//type selection
	$('#ListingType').on('change', function () {
		var value = $(this).val();
		var coWorkingValue = $('#CoworkingType').val();
		//alert(value)
		//if (coWorkingValue == "Cafe") {
		//	$('.type-2-sub__input').addClass('d-none');
		//}
		//else if (coWorkingValue == "Office") {
		//	$('.type-2-sub__input').removeClass('d-none');
		//}
		if (value == "Commercial") {
			$('.form-type').removeClass('type-2');
			$('.form-type').removeClass('type-3');
			$('.form-type').addClass('type-1');
			$("input[name=CMCW_PropertyFor][value='Rental']").prop("checked", true);
			$('.type-2__input, .type-3, .type-style.type-2, .type-style.type-3, .type-2-sub__input, .for-sale').addClass('d-none');
			$('.type-1__input, .type-style.type-1, .form-type.type-1, .input-occupancy, .for-rental').removeClass('d-none');
		}
		else if (value == "Co-Working") {
			$('.form-type').removeClass('type-1');
			$('.form-type').removeClass('type-3');
			$('.form-type').addClass('type-2');
			$("input[name=CMCW_PropertyFor][value='Rental']").prop("checked", true);
			$('.type-1__input, .type-3, .type-style.type-1, .type-style.type-3, .for-sale').addClass('d-none');
			$('.type-2__input, .type-style.type-2, .form-type.type-2, .type-2-sub__input, .input-occupancy, .for-rental').removeClass('d-none');
		}
		else if (value == "RE-Professional") {
			$('.type-3').removeClass('d-none');
			$('.form-type.type-1, .form-type.type-2, .type-style.type-1, .type-style.type-2, .for-sale, .for-rental').addClass('d-none');
		}
	});
	$('input[type=radio][name=CMCW_PropertyFor]').change(function () {
		if (this.value == 'Rental') {
			$('.for-rental').removeClass('d-none');
			$('.for-sale').addClass('d-none');
		}
		else if (this.value == 'Sale') {
			$('.for-rental').addClass('d-none');
			$('.for-sale').removeClass('d-none');
		}
	});
	$('#CoworkingType').on('change', function () {
		var value = $(this).val();
		//alert(value)
		if (value == "Office") {
			$('.type-2-sub__input, .input-occupancy').removeClass('d-none');
		}
		else if (value == "Cafe") {
			$('.type-2-sub__input, .input-occupancy').addClass('d-none');
		}
	});
	//checkbox conditon to show
	$('.type-2-sub input[type="checkbox"]').click(function () {
		if ($(this).prop("checked") == true) {
			//console.log("Checkbox is checked.");
			$(this).parent().parent().siblings('.check-view').removeClass('d-none');
		}
		else if ($(this).prop("checked") == false) {
			//console.log("Checkbox is unchecked.");
			$(this).parent().parent().siblings('.check-view').addClass('d-none');
		}
	});
	//amenities section price
	$(document).on('change', '.am-option', function () {
		var value = $(this).val();
		//alert(value)
		if (value == "Free") {
			$(this).closest('.amenities-upload__row').children('.am-price, .am-partial').addClass('d-none');
		}
		else if (value == "Paid") {
			//$('.am-price').removeClass('d-none');
			//$('.am-partial').addClass('d-none');
			$(this).closest('.amenities-upload__row').children('.am-price').removeClass('d-none');
			$(this).closest('.amenities-upload__row').children('.am-partial').addClass('d-none');
		}
		else if (value == "PartiallyPaid") {
			//$('.am-partial').removeClass('d-none');
			//$('.am-price, .am-price').addClass('d-none');
			$(this).closest('.amenities-upload__row').children('.am-partial').removeClass('d-none');
			$(this).closest('.amenities-upload__row').children('.am-price').removeClass('d-none');
		}
	});
	
	

});

////map section
//// Initialize and add the map
function initMapLatLon() {
	//alert('a')
	console.log($('#lat').html());
	console.log($('#lon').html());
	var lat_num = parseFloat($('#lat').html());
	var lon_num = parseFloat($('#lon').html());
	var map_zoom = parseInt($('#map-zoom').html());
	console.log(lat_num)
	// The location of Uluru
	var uluru = { lat: lat_num, lng: lon_num };
	// The map, centered at Uluru
	var map = new google.maps.Map(
		document.getElementById('map'), { zoom: map_zoom, center: uluru });
	console.log(map);
	// The marker, positioned at Uluru
	var marker = new google.maps.Marker({ position: uluru, map: map });
}


/*********tab navigation section start*********/

//on focus input
$('.required-input').focus(function () {
	$(this).parents('.form-group').siblings('.hi-error').addClass('d-none');
});

//on blur input
$('.required-input').blur(function () {
	if ($(this).val() == "") {
		$(this).parents('.form-group').siblings('.hi-error').removeClass('d-none');
	}
});

function tabNavigation(nav, e) {
	
	var tabLength = parseInt($('.hi-tab').find('.nav-link').length);
	var currentTab = parseInt($('.hi-tab .active').attr('data-id'));
	var isValid = true;
	let formData = { "id": "addListingForm" };
	//console.log($(`#${formData.id}`).html())
	$(`#${formData.id} .error`).html(``);
	//console.log('click')
	//disable the back btn on first tab
	if (currentTab == 2) {
		//alert('a')
		$('.tab-back-btn').css('display', 'none');
	}
	else {
		$('.tab-next-btn').css('display', 'inline-block');
		$('.tab-submit').css('display', 'none');
	}

	//click back button
	if (nav == 0) {
		$("[data-id='" + (currentTab - 1) + "']").click();
	}
	//click next button
	else if (nav == 1) {
		if (currentTab == 1) {
			//console.log(1);
			let rules = [
				//{ "id": "Email", "validation": ["emptyRegx", "emailRegx"] },
				//{ "id": "Phone", "validation": ["emptyRegx", "phoneRegx"] }
			];

			if ($("#ListingType").val() == "Commercial") {
				console.log('comm');
				rules.push({ "id": "Name", "validation": ["emptyRegx"] });
				console.log(rules);
			}
			else if ($("#ListingType").val() == "Co-Working") {
				//console.log('cow');
				rules.push({ "id": "Name", "validation": ["emptyRegx"] });
				let option_shared_space = $("#option_shared_space");
				let option_private_space = $("#option_private_space");
				let option_meeting_room = $("#option_meeting_room");

				if (option_shared_space.prop("checked")) {
					rules.push({ "id": "CW_Coworking", "validation": ["emptyRegx"] });
					rules.push({ "id": "CW_CoworkingSeats", "validation": ["emptyRegx"] });
				}

				if (option_private_space.prop("checked")) {
					rules.push({ "id": "CW_PrivateOffice", "validation": ["emptyRegx"] });
					rules.push({ "id": "CW_PrivateOfficeSeats", "validation": ["emptyRegx"] });
				}

				if (option_meeting_room.prop("checked")) {
					rules.push({ "id": "CW_MeetingRoom", "validation": ["emptyRegx"] });
					rules.push({ "id": "CW_MeetingRoomSeats", "validation": ["emptyRegx"] });
				}
				//console.log(rules);
			}
			else if ($("#ListingType").val() == "RE-Professional") {
				//console.log('rep');
				rules.push({ "id": "RE_FirstName", "validation": ["emptyRegx"] });
				rules.push({ "id": "RE_LastName", "validation": ["emptyRegx"] });
				//console.log(rules);
			}

			validate(formData, rules);
			$(`#${formData.id} .error`).each(function (i) {

				if ($(this).is(':empty')) {

				}
				else {

					isValid = false;
					//e.preventDefault();
					return;
				}
			})
			if (!isValid) {

				return false;
			}
		}
		else if (currentTab == 2) {
			console.log(2);
			let rules = [
				{ "id": "locality", "validation": ["emptyRegx"] },
				{ "id": "administrative_area_level_1", "validation": ["emptyRegx"] },
				{ "id": "country", "validation": ["emptyRegx"] },
				{ "id": "postal_code", "validation": ["emptyRegx"] }
			];
			validate(formData, rules);
			$(`#${formData.id} .error`).each(function (i) {
				if ($(this).is(':empty')) {
				}
				else {
					isValid = false;
					//e.preventDefault();
					return;
				}
			})
			if (!isValid) {
				return false;
			}
		}
		else if (currentTab == 3) {

		}

		checkRequiredInput(currentTab);

		//to show submit button on the last tab
		if ((currentTab) == (tabLength - 1)) {
			//alert('a')
			$('.tab-next-btn').css('display', 'none');
			$('.tab-submit').css('display', 'inline-block');
		}
		
	}
	//click submit button
	else if (nav == 2) {
		//e.preventDefault();
		//return false;
		console.log(3);
		let rules = [
			{ "id": "Description", "validation": ["emptyRegx"] },
			{ "id": "Email", "validation": ["emptyRegx", "emailRegx"] },
			{ "id": "Phone", "validation": ["emptyRegx", "phoneRegx"] }
		];
		console.log(rules)
		validate(formData, rules);
		$(`#${formData.id} .error`).each(function (i) {
			if ($(this).is(':empty')) {
			}
			else {
				if (currentTab == tabLength) {
					$('.tab-back-btn').css('display', 'inline-block');
					$('.tab-next-btn').css('display', 'none');
					$('.tab-submit').css('display', 'inline-block');
				}
				console.log('failed')
				isValid = false;
				e.preventDefault();
				return;
			}
		})
		if (!isValid) {
			if (currentTab == tabLength) {
				$('.tab-back-btn').css('display', 'inline-block');
				$('.tab-next-btn').css('display', 'none');
				$('.tab-submit').css('display', 'inline-block');
			}
			console.log('fail')
			return false;
		}
	}
};

function checkRequiredInput(currentTab) {
	var dataId = $("[data-id='" + (currentTab + 1) + "']");
	//check required class in input
	if ($('input:visible').hasClass('required-input')) {
		//each condition to find the empty inputs
		$('.required-input:visible').each(function (i) {
			if ($(this).val() == "") {
				$(this).parents('.form-group').siblings('.hi-error').removeClass('d-none');
			}
			else {
				$('.tab-back-btn').css('display', 'inline-block');
				dataId.click();
				dataId.removeClass('pointer-event-none');
				dataId.parents('.nav-item').removeClass('cursor-no-drop');
			}
		});
	}
	else {
		$('.tab-back-btn').css('display', 'inline-block');
		dataId.click();
		dataId.removeClass('pointer-event-none');
		dataId.parents('.nav-item').removeClass('cursor-no-drop');
	}
};

$('.hi-tab .nav-link').on('click', function () {
	var tabLength = parseInt($('.hi-tab').find('.nav-link').length);
	var currentTab = parseInt($(this).attr('data-id'));
	if (currentTab == 1) {
		$('.tab-back-btn').css('display', 'none');
		$('.tab-next-btn').css('display', 'inline-block');
		$('.tab-submit').css('display', 'none');
	}
	else if (currentTab == tabLength) {
		$('.tab-back-btn').css('display', 'inline-block');
		$('.tab-next-btn').css('display', 'none');
		$('.tab-submit').css('display', 'inline-block');
	}
	else {
		$('.tab-back-btn').css('display', 'inline-block');
		$('.tab-next-btn').css('display', 'inline-block');
		$('.tab-submit').css('display', 'none');
	}
});

//***********************user section end****************************//

//$('input[name="workingHours.Is24"]').click(function () {
$('body').on('click', 'input[name="WorkingHours.Is24"]', function () {
	//alert('a');
	//$('input[name="AllTimeCheck"], input[name="MonToFriCheck"], input[name="MonToSatCheck"], input[name="CustomCheck"]').click(function () {
	//console.log($('#AllTimeCheck option:selected').text());
	$('#AllTimeCheck').val("false");
	$('#MonToFriCheck').val("false");
	$('#MonToSatCheck').val("false");
	$('#CustomCheck').val("false");
	if ($(this).prop("checked")) {
		$(this).siblings('select').val("true");
		//console.log($('#AllTimeCheck option:selected').text());
	}

	//alert($(this).attr('radio-attr'))
	$('.form-radio label').removeClass('radio-active');
	$(this).parents('label').addClass('radio-active');
	if ($(this).attr('radio-attr') == "24/7") {
		$('.sch-custom, .sch-weekdays, .sch-weeksaturday').addClass('cursor-no-drop');
		$('.sch-custom > div, .sch-weekdays > div, .sch-weeksaturday > div').addClass('pointer-event-none');
	}
	else if ($(this).attr('radio-attr') == "weekdays") {
		$('.sch-weekdays').removeClass('cursor-no-drop');
		$('.sch-weekdays > div').removeClass('pointer-event-none');

		$('.sch-custom, .sch-weeksaturday').addClass('cursor-no-drop');
		$('.sch-custom > div, .sch-weeksaturday > div').addClass('pointer-event-none');
	}
	else if ($(this).attr('radio-attr') == "weeksaturday") {
		$('.sch-weeksaturday').removeClass('cursor-no-drop');
		$('.sch-weeksaturday > div').removeClass('pointer-event-none');

		$('.sch-custom, .sch-weekdays').addClass('cursor-no-drop');
		$('.sch-custom > div, .sch-weekdays > div').addClass('pointer-event-none');
	}
	else if ($(this).attr('radio-attr') == "custom") {
		$('.sch-custom').removeClass('cursor-no-drop');
		$('.sch-custom > div').removeClass('pointer-event-none');

		$('.sch-weekdays, .sch-weeksaturday').addClass('cursor-no-drop');
		$('.sch-weekdays > div, .sch-weeksaturday > div').addClass('pointer-event-none');
	}
});

$('body').on('click', '.sch-status', function () {
	if ($(this).prop("checked") == true) {
		$(this).parents('.sch-checkbox').siblings('.sch-time').css('display', 'block');
		$(this).siblings('strong').html('Open');
	}
	else if ($(this).prop("checked") == false) {
		$(this).parents('.sch-checkbox').siblings('.sch-time').css('display', 'none');
		$(this).siblings('strong').html('Closed');
	}
});
//schedular section end

$(function () {

	//model open for the login section
	$('body').on('click', '.modal-link__login', function (e) {
		e.preventDefault();

		$("#modal-container__login").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__login" class="modal login fade modal-try" tabindex="-1" role="dialog" aria-labelledby="loginModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-content" role="document"' +
				data + '</div></div>').modal();

		});

	});

	//model open for the signup section
	$('body').on('click', '.modal-link__signup', function (e) {
		e.preventDefault();

		$("#modal-container__signup").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__signup" class="modal signup fade modal-try" tabindex="-1" role="dialog" aria-labelledby="signupModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-content" role="document"' +
				data + '</div></div>').modal();
			if ($('#modal-container__signup').length) {
				
			} else {
				//$("body").addClass("overflow-hidden");
				//$('.signup').css('overflow', 'auto');
			}
		});
	});

	//body remove class from hidden on the popup modal
	$(document).on("click", ".modal .close", function () {
		//$("body").removeClass("overflow-hidden");
	});
	//model open for the list image section
	$('body').on('click', '.modal-link__image', function (e) {
		e.preventDefault();

		$("#modal-container__image").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__image" class="modal image fade modal-try" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-lg modal-content" role="document"' +
				data + '</div></div>').modal();
			
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);
		});
		setTimeout(function () {
			removeLoader();
		}, 2000);
	});

	//model open for the Linked re-professional detail view
	$('body').on('click', '.modal-link__repro', function (e) {
		e.preventDefault();

		$("#modal-container__repro").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__repro" class="modal image fade modal-try" tabindex="-1" role="dialog" aria-labelledby="reproModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-lg modal-content" role="document"' +
				data + '</div></div>').modal();
			
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);
		});
		setTimeout(function () {
			removeLoader();
		}, 2000);
	});

	//model open for the list hours section
	$('body').on('click', '.modal-link__hours', function (e) {
		e.preventDefault();

		$("#modal-container__hours").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__hours" class="modal hours fade modal-try" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-lg modal-content" role="document"' +
				data + '</div></div>').modal();
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);
		});
		setTimeout(function () {
			//console.log($('#AllTimeCheck').html())
			//console.log($('#MonToFriCheck option:selected').val())
			//console.log($('#MonToSatCheck option:selected').val())
			//console.log($('#CustomCheck option:selected').val())
			if ($('#AllTimeCheck option:selected').text() == "True") {
				$('#AllTimeCheck').parents('label').find('[type=radio]').prop("checked", true);
				$('#AllTimeCheck').parents('label').addClass('radio-active');
			}
			else if ($('#MonToFriCheck option:selected').text() == "True") {
				$('#MonToFriCheck').parents('label').find('[type=radio]').prop("checked", true);
				$('#MonToFriCheck').parents('.radio').siblings().removeClass('cursor-no-drop');
				$('#MonToFriCheck').parents('.radio').siblings().children().removeClass('pointer-event-none');
				$('#MonToFriCheck').parents('.radio').siblings().find('input').prop("checked", true);
				$('#MonToFriCheck').parents('.radio').siblings().find('input').parents('.sch-checkbox').siblings('.sch-time').css('display', 'block');
				$('#MonToFriCheck').parents('.radio').siblings().find('input').siblings('strong').html('Open');
				$('#MonToFriCheck').parents('label').addClass('radio-active');
			}
			else if ($('#MonToSatCheck option:selected').text() == "True") {
				$('#MonToSatCheck').parents('label').find('[type=radio]').prop("checked", true);
				$('#MonToSatCheck').parents('.radio').siblings().removeClass('cursor-no-drop');
				$('#MonToSatCheck').parents('.radio').siblings().children().removeClass('pointer-event-none');
				$('#MonToSatCheck').parents('.radio').siblings().find('input').prop("checked", true);
				$('#MonToSatCheck').parents('.radio').siblings().find('input').parents('.sch-checkbox').siblings('.sch-time').css('display', 'block');
				$('#MonToSatCheck').parents('.radio').siblings().find('input').siblings('strong').html('Open');
				$('#MonToSatCheck').parents('label').addClass('radio-active');
			}
			else if ($('#CustomCheck option:selected').text() == "True") {
				$('#CustomCheck').parents('label').find('[type=radio]').prop("checked", true);
				$('#CustomCheck').parents('.radio').siblings().removeClass('cursor-no-drop');
				$('#CustomCheck').parents('.radio').siblings().children().removeClass('pointer-event-none');
				$('#CustomCheck').parents('.radio').siblings().find('input').prop("checked", true);
				$('#CustomCheck').parents('.radio').siblings().find('input').parents('.sch-checkbox').siblings('.sch-time').css('display', 'block');
				$('#CustomCheck').parents('.radio').siblings().find('input').siblings('strong').html('Open');
				$('#CustomCheck').parents('label').addClass('radio-active');
			}

			//display view in the scheduler section
			if ($('#Display_view #AllTimeCheck option:selected').text() == "True") {
				$('.sch-2, .sch-3, .sch-4').css('display', 'none');
				$('.sch-1').css('display', 'block');
			}
			else if ($('#Display_view #MonToFriCheck option:selected').text() == "True") {
				$('.sch-1, .sch-3, .sch-4').css('display', 'none');
				$('.sch-2').css('display', 'block');
			}
			else if ($('#Display_view #MonToSatCheck option:selected').text() == "True") {
				$('.sch-1, .sch-2, .sch-4').css('display', 'none');
				$('.sch-3').css('display', 'block');
			}
			else if ($('#Display_view #CustomCheck option:selected').text() == "True") {
				$('.sch-1, .sch-2, .sch-3').css('display', 'none');
				$('.sch-4').css('display', 'block');
			}

			removeLoader();
		}, 2000);
	});

	//model open for the list project section
	$('body').on('click', '.modal-link__project', function (e) {
		e.preventDefault();

		$("#modal-container__project").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__project" class="modal project fade modal-info" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-xl modal-content" role="document"' +
				data + '</div></div>').modal();
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);

		});
		setTimeout(function () {
			$('.project-upload__row').each(function (e) {
				//console.log($(this).find('.operatorDropdown').attr('class'));
				if ($(this).find('.operatorName').hasClass('cantedit') == true) {
					$('.basic-select + .select2-container').addClass("event-none");
					console.log('yes')
				} else {
					operatorListDropdown();
					$('.basic-select').select2();
					console.log('no')

				}
			})
			
			removeLoader();
		}, 2000);
		
	});

	//model open for the list amenities section
	$('body').on('click', '.modal-link__amenities', function (e) {
		e.preventDefault();

		$("#modal-container__amenities").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__amenities" class="modal amenities fade modal-try" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-xl modal-content" role="document"' +
				data + '</div></div>').modal();
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);
		});

	//select type on edit
	setTimeout(function () {
		$('.amenities-upload__row').each(function (e) {
			var valueType = $(this).find('.amenityType').val();
			if (valueType == "Free") {
				$(this).children('.am-price, .am-partial').addClass('d-none');
			}
			else if (valueType == "Paid") {
				$(this).children('.am-price').removeClass('d-none');
				$(this).children('.am-partial').addClass('d-none');
			}
			else if (valueType == "PartiallyPaid") {
				$(this).children('.am-partial').removeClass('d-none');
				$(this).children('.am-price').removeClass('d-none');
			}
		});
		removeLoader();
	}, 2000);
	});

	//model open for the list facilities section
	$('body').on('click', '.modal-link__facilities', function (e) {
		e.preventDefault();

		$("#modal-container__facilities").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__facilities" class="modal facilities fade modal-sec" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-lg modal-content" role="document"' +
				data + '</div></div>').modal();
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);
		});
		setTimeout(function () {
			removeLoader();
		}, 2000);
	});

	//model open for the list filter section
	$('body').on('click', '.modal-link__filter', function (e) {
		e.preventDefault();

		$("#modal-container__filter").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__filter" class="modal filter fade modal-sec" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-lg modal-content" role="document"' +
				data + '</div></div>').modal();

		});
	});

	//model open for the Health check section
	$('body').on('click', '.modal-link__healthCheck', function (e) {
		e.preventDefault();

		$("#modal-container__healthCheck").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__healthCheck" class="modal filter fade modal-sec" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-lg modal-content" role="document"' +
				data + '</div></div>').modal();
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);
		});
		//select type on edit
		setTimeout(function () {
			if ($('.healthCheck-data').length) {
				//AQI
				var AQI_Data = $('#AQI_Data').attr('value');
				$('#AQI_Data').val(AQI_Data).prop('selected', true);
				if (AQI_Data == "0-50") {
					$('#AQI_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (AQI_Data == "51-100") {
					$('#AQI_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (AQI_Data == "101-200") {
					$('#AQI_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (AQI_Data == "201-300") {
					$('#AQI_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (AQI_Data == "301-400") {
					$('#AQI_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (AQI_Data == "401-500") {
					$('#AQI_Grade span').attr('class', 'text-danger font-weight-bold');
				}
				//Temperature
				var Temperature_Data = $('#Temperature_Data').attr('value');
				$('#Temperature_Data').val(Temperature_Data).prop('selected', true);
				if (Temperature_Data == "<95") {
					$('#Temperature_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (Temperature_Data == "95-99.9") {
					$('#Temperature_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (Temperature_Data == "100-104") {
					$('#Temperature_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (Temperature_Data == ">104") {
					$('#Temperature_Grade span').attr('class', 'text-pry font-weight-bold');
				}
				//Humidity
				var Humidity_Data = $('#Humidity_Data').attr('value');
				$('#Humidity_Data').val(Humidity_Data).prop('selected', true);
				if (Humidity_Data == "<30") {
					$('#Humidity_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (Humidity_Data == "30-50") {
					$('#Humidity_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (Humidity_Data == ">50") {
					$('#Humidity_Grade span').attr('class', 'text-pry font-weight-bold');
				} 
				//CO2
				var CO2_Data = $('#CO2_Data').attr('value');
				$('#CO2_Data').val(CO2_Data).prop('selected', true);
				if (CO2_Data == "<400") {
					$('#CO2_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (CO2_Data == "400-599") {
					$('#CO2_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (CO2_Data == "600-999") {
					$('#CO2_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (CO2_Data == "1000-1499") {
					$('#CO2_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (CO2_Data == "1500-1999") {
					$('#CO2_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (CO2_Data == ">2000") {
					$('#CO2_Grade span').attr('class', 'text-danger font-weight-bold');
					}
				//CO
				var CO_Data = $('#CO_Data').attr('value');
				$('#CO_Data').val(CO_Data).prop('selected', true);
				if (CO_Data == "<4.4") {
					$('#CO_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (CO_Data == "4.4-9.4") {
					$('#CO_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (CO_Data == "9.5-12.4") {
					$('#CO_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (CO_Data == "12.5-15.4") {
					$('#CO_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (CO_Data == "15.5-30.4") {
					$('#CO_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (CO_Data == "30.5-40.4") {
					$('#CO_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (CO_Data == ">40.5") {
					$('#CO_Grade span').attr('class', 'text-danger font-weight-bold');
					} 
				//PM2.5
				var PM2Point5_Data = $('#PM2Point5_Data').attr('value');
				$('#PM2Point5_Data').val(PM2Point5_Data).prop('selected', true);
				if (PM2Point5_Data == "<15.4") {
					$('#PM2Point5_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (PM2Point5_Data == "15.4-40.4") {
					$('#PM2Point5_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (PM2Point5_Data == "40.5-65.4") {
					$('#PM2Point5_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (PM2Point5_Data == "65.5-150.4") {
					$('#PM2Point5_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (PM2Point5_Data == "150.5-250.4") {
					$('#PM2Point5_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (PM2Point5_Data == "250.5-350.4") {
					$('#PM2Point5_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (PM2Point5_Data == ">350.5") {
					$('#PM2Point5_Grade span').attr('class', 'text-danger font-weight-bold');
					} 
				//PM10
				var PM10_Data = $('#PM10_Data').attr('value');
				$('#PM10_Data').val(PM10_Data).prop('selected', true);
				if (PM10_Data == "<=54") {
					$('#PM10_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (PM10_Data == "55-154") {
					$('#PM10_Grade span').attr('class', 'text-sec font-weight-bold');
				} else if (PM10_Data == "155-254") {
					$('#PM10_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (PM10_Data == "255-354") {
					$('#PM10_Grade span').attr('class', 'text-pry font-weight-bold');
				} else if (PM10_Data == "355-424") {
					$('#PM10_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (PM10_Data == "425-504") {
					$('#PM10_Grade span').attr('class', 'text-danger font-weight-bold');
				} else if (PM10_Data == ">505") {
					$('#PM10_Grade span').attr('class', 'text-danger font-weight-bold');
					} 
				//Moisture
				var Moisture_Data = $('#Moisture_Data').attr('value');
				$('#Moisture_Data').val(Moisture_Data).prop('selected', true);
				if (Moisture_Data == "5%-12%") {
					$('#Moisture_Grade span').html('<i class="fab fa-pagelines"></i>');
					$('#Moisture_Grade span').attr('class', 'text-sec');
				} else if (Moisture_Data == "13%-17%") {
					$('#Moisture_Grade span').html('<i class="fab fa-pagelines"></i>');
					$('#Moisture_Grade span').attr('class', 'text-pry');
				} else if (Moisture_Data == "<5% or >17%") {
					$('#Moisture_Grade span').html('<i class="fab fa-pagelines"></i>');
					$('#Moisture_Grade span').attr('class', 'text-danger');
					}
			}
			removeLoader();
		}, 2000);
	});

	//model open for the GreenBuilding check section
	$('body').on('click', '.modal-link__greenBuildingData', function (e) {
		e.preventDefault();

		$("#modal-container__greenBuildingData").remove();

		$.get($(this).data("targeturl"), function (data) {

			$('<div id="modal-container__greenBuildingData" class="modal filter fade modal-sec" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">' +
				'<div id="modalbody" class="modal-dialog modal-lg modal-content" role="document"' +
				data + '</div></div>').modal();
			setTimeout(function () {
				$(".modal:visible .modal-dialog").append("<div class='loader_new-sub'></div>");
			}, 500);
		});

		setTimeout(function () {

			if ($('.greenBuildingCheck-data').length) {
				$('.gb-input__status, .gb-input__checklist, .gb-input__authority, .rating, .gb-input__apply').addClass("event-none");
				if ($('#gb-radio-1').attr('data-radio') == "Yes") {
					$('#gb-radio-1 input[name="radio_1"][value="Yes"]').prop("checked", true);
					$('.gb-input__authority, .gb-input__certificate').removeClass('display-none');
					$('.gb-input__apply, .gb-input__checklist, .gb-data').addClass('display-none');
					$('.gb-data').removeClass('display-none');
					var CertifiedBy = $('#CertifiedBy').attr('value');
					$('#CertifiedBy').val(CertifiedBy).prop('selected', true);
				} else if ($('#gb-radio-1').attr('data-radio') == "No") {
					$('#gb-radio-1 input[name="radio_1"][value="No"]').prop("checked", true);
					$('.gb-input__checklist').removeClass('display-none');
					$('.gb-data, .gb-input__authority, .gb-data').addClass('display-none');
					$('input[name="radio_2"][value="Yes"]').prop("checked", true);
					$('.gb-data, .gb-input__apply').removeClass('display-none');
					if ($('#gb-radio-3').attr('data-radio') == "Yes") {
						$('#gb-radio-3 input[name="radio_3"][value="Yes"]').prop("checked", true);
					} else {
						$('#gb-radio-3 input[name="radio_3"][value="No"]').prop("checked", true);
					}
				}else if ($('#gb-radio-1').attr('data-radio') == "Applied") {
					$('#gb-radio-1 input[name="radio_1"][value="Applied"]').prop("checked", true);
					$('.gb-input__authority').removeClass('display-none');
					$('.gb-input__certificate, .gb-input__apply, .gb-input__checklist, .gb-data').addClass('display-none');
					$('.gb-data').removeClass('display-none');
					var CertifiedBy = $('#CertifiedBy').attr('value');
					$('#CertifiedBy').val(CertifiedBy).prop('selected', true);
				}
			}
			var options = {
				step_size: 1,
				symbols: {
					fontawesome_star: {
						base: '<i class="far fa-star"></i>',
						hover: '<i class="fas fa-star"></i>',
						selected: '<i class="fas fa-star text-pry"></i>',
					},
				},
				selected_symbol_type: 'fontawesome_star',
				cursor: 'pointer',
			}
			$(".rate").rate(options);
			$(".rate").on("change", function (ev, data) {
				$(this).siblings('.rate-value').val($(this).rate("getValue"));
				console.log($(this).siblings('.rate-value').val());
			});
			removeLoader();
		}, 2000)
		
	});

});

//--------------------------------------------------------------------------------------------------//
//************************************adding image section start************************************//
//--------------------------------------------------------------------------------------------------//
//image upload section start
function addImage(obj) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	//debugger
	var listingId = $(obj).attr('data-listingid');
	$('.image-upload').prepend(
		'<div class="row image-upload__row">' +
		'<div class=" col-md-4 col-sm-6 align-self-center">' +
		'<div class="display-none">' +
		'<div class="form-group">' +
		'<input type="text" class="form-control imageId event-none" placeholder="id" value="0">' +
		'<label for="input" class="control-label">Image id</label><i class="bar"></i>' +
		'</div>' +
		'</div>' +
		'<div class="form-group">' +
		'<input type="text" class="form-control imageName" placeholder="Image Name">' +
		'<label for="input" class="control-label">Image Name*</label><i class="bar"></i>' +
		'<div class="error"></div>'+
		'</div>	' +
		'</div>' +
		'<div class=" col-md-4 col-sm-6 align-self-center">' +
		'<div class="form-group">' +
		'<input type="file" class="form-control imageFilePath"  accept="image/x-png,image/gif,image/jpeg">' +
		'<label for="input" class="control-label">Upload Image*</label><i class="bar"></i>' +
		'<div class="error"></div>'+
		'</div>' +
		'</div>' +
		//'<div class="col-md-2 col-sm-6 m-b--15 align-self-center">' +
		//'<div class="checkbox m-0">' +
		//'<label>' +
		//'<input type="checkbox" class="imageCheck" /><i class="helper"></i> Active' +
		//'</label>' +
		//'</div>' +
		//'</div>' +
		'<div class="col-md-4 col-sm-6 align-self-center">' +
		'<span class="addon-add delete-btn tooltip-wrapper text-sec" onclick="AddImageForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="submit and upload the file"><i class="fas fa-cloud-upload-alt btn-icon text-sec"></i> Upload</span>' +
		'<span class="addon-edit delete-btn tooltip-wrapper display-none text-info" onclick="EditImageForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="Edit the file"><i class="fas fa-edit btn-icon text-info"></i> Edit</span>' +
		'<span class="addon-delete delete-btn tooltip-wrapper text-danger" onclick="deleteRowImage(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to delete the row"><i class="fas fa-trash-alt btn-icon text-danger"></i> Delete</span>' +
		//'<span class="addon-clear delete-btn tooltip-wrapper display-none" onclick="clearRowImage(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i></span>'+
		'</div>' +
		'</div>'
	);
	removeLoader();
};

//delete the row
function deleteRowImage(that) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	$(that).closest('.image-upload__row').remove();
	removeLoader();
	//console.log($(that).parents().find('.image-upload__row').html());
};

//image upload section end
function AddImageForm(obj) {
	//debugger
	var listingId = $(obj).attr('data-listingid');
	//console.log(listingId)
	var formData = new FormData();
	var row = $(obj).closest('.image-upload__row');
	var iImageId;
	var files = $(row).find('.imageFilePath').get(0).files;
	var iName = $(row).find('.imageName').val();
	var iImagePath = $(row).find('.imageFilePath').val();
	//if ($(row).find('.imageCheck').is(':checked')) {
			iStatus = true;
	//	} else {
	//		iStatus = false;
	//}

	//image validation start
	if (iName == "") {
		$(row).find('.imageName').siblings('.error').html('This feild is required');
	}
	if (iImagePath == "") {
		$(row).find('.imageFilePath').siblings('.error').html('This feild is required');
	}
	if (iName == "" || iImagePath == "") {
		console.log('b')
		return false;
	} else {
		$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	}
	//image validation end

	if ($(row).find('.imageId').length) {
		iImageId = $(row).find('.imageId').val();
	} else {
		iImageId = 0;
	}
	formData.append("File_Image", files[0]);
	formData.append("Name", iName);
	formData.append("ImageUrl", iImagePath);
	formData.append("Status", iStatus);
	formData.append("ListingId", listingId);
	formData.append("ListingImagesId", iImageId);

	//var url = '@Url.Action("UploadImage", "Addons")';
	$.ajax({
		type: "POST",
		url: "/Addons/UploadImage",
		data: formData,
		processData: false,
		contentType: false,
		success: function (response) {
			if (response != null) {
				//console.log(response);
				$(row).addClass("addons-row");
				$(row).removeClass("addons-row__edit");
				$(row).find('.imageName').addClass("event-none");
				$(row).find('.imageId').attr("value",response.listingImagesId);
				//$(row).find('.imageFilePath').parent().parent().find('.addon-image__div').remove();
				$(row).find('.imageFilePath').parent().parent().append(
					'<div class="addon-image__div"><a href=' + response.imageUrl +' target="_blank"><img class="addon-image" alt="name" src=' + response.imageUrl +' /></a></div>'
				);
				$(row).find('.imageFilePath').parent().addClass("display-none");
				//$(row).find('.imageCheck').prop("checked", response.status);
				$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
				$(obj).siblings('.addon-delete').attr('onclick', "deleteImage(this," + response.listingImagesId + ")");
				//$(obj).siblings('.addon-clear').addClass('display-none');
				if ($(obj).siblings('.addon-clear').length) {
					$(obj).siblings('.addon-clear').addClass('display-none');
				} else {
					$(obj).parent().append(
						'<span class="addon-clear delete-btn tooltip-wrapper display-none text-pry" onclick="clearRowImage(this,' + response.listingImagesId + ')" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i> Clear</span>'
					);
				};
				$(obj).addClass('display-none');
				removeLoader();
			}
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please upload afterwards");
		}
	});
}

//edit image section
function EditImageForm(obj) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	var row = $(obj).closest('.image-upload__row');
	$(row).removeClass("addons-row");
	$(row).addClass("addons-row__edit");
	$(row).find('.imageName').removeClass("event-none");
	$(row).find('.imageFilePath').parent().removeClass("display-none");
	$(row).find('.imageFilePath').parent().siblings(".addon-image__div").remove();
	$(row).find('.imageFilePath').parent().css("margin-bottom","0");
	$(obj).siblings('.addon-add, .addon-clear').removeClass('display-none');
	$(obj).siblings('.addon-delete').addClass('display-none');
	$(obj).addClass('display-none');
	removeLoader();
}

//Reset image section
function clearRowImage(obj, imageId) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	//console.log(imageId)
	var row = $(obj).closest('.image-upload__row');
	$.ajax({
		type: "GET",
		url: "/Addons/GetImage",
		dataType: "json",
		data: { id: imageId },
		success: function (response) {
			if (response != null) {
				console.log(response);
				$(row).addClass("addons-row");
				$(row).removeClass("addons-row__edit");
				$(row).find('.imageId').attr("value",response.listingImagesId);
				$(row).find('.imageName').addClass("event-none");
				$(row).find('.error').html("");
				$(row).find('.imageName').val(response.name);
				$(row).find('.imageFilePath').parent().parent().append(
					'<div class="addon-image__div"><a href=' + response.imageUrl +' target="_blank"><img class="addon-image" alt="name" src=' + response.imageUrl + ' /></a></div>'
				);
				$(row).find('.imageFilePath').parent().addClass("display-none");
				//$(row).find('.imageCheck').prop("checked", response.status);
				$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
				$(obj).siblings('.addon-add').addClass('display-none');
				$(obj).addClass('display-none');
				removeLoader();
			}
		},
		error: function (response) {
			removeLoader();
			alert(response);
		}
	})
}

//Delete image section
function deleteImage(obj, imageId) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	var row = $(obj).closest('.image-upload__row');
	if (imageId != 0) {
		$.ajax({
			type: "GET",
			url: "/Addons/DeleteImage",
			dataType: "json",
			data: { id: imageId },
			success: function (response) {
				if (response != null) {
					console.log(response);
					deleteRowImage(obj);
				}
			},
			error: function (response) {
				removeLoader();
				alert("server not ready please delete afterwards");
			}
		})
	}
	else {
		deleteRowImage(obj);
	}
}
//--------------------------------------------------------------------------------------------------//
//************************************adding image section end**************************************//
//--------------------------------------------------------------------------------------------------//

//--------------------------------------------------------------------------------------------------//
//************************************adding Project section start************************************//
//--------------------------------------------------------------------------------------------------//
//$(document).ready(function () {
	//Property list all
	function operatorListDropdown() {
		$.ajax({
			type: "GET",
			url: "/Addons/OperatorListDropdownl",
			data: {},
			context: this,
			dataType: "json",
			success: function (response) {
				//console.log(response);
				$(".project-upload__row:first-child .operatorDropdown").html("");
				$(".project-upload__row:first-child .operatorDropdown").append("<option user-id='Please select' value='Please select'>--Please select--</option>");
				for (var key in response) {
					if (response.hasOwnProperty(key)) {
						//console.log(response[key].companyName)
						$(".project-upload__row:first-child .operatorDropdown").append("<option user-id=" + response[key].userId + " value=" + response[key].companyName + ">" + response[key].companyName + "</option>");
					}
				}
				//console.log($('.project-upload__row:first-child. operatorDropdown').html());
				$('.project-upload__row:first-child .operatorDropdown').select2();

			},
			error: function (response) {
				alert("server not ready please try afterwards");
			}
		});
	}
	$(document).on('click', '.displayExistingOperator', function (event) {
		$(this).closest('.operator-nonexist').addClass('display-none');
		$(this).closest('.operator-nonexist').siblings('.operator-exist').removeClass('display-none');
		//property section
		$(this).closest('.operator-main-div').siblings('.property-main-div').find('.property-nonexist').addClass('display-none');
		$(this).closest('.operator-main-div').siblings('.property-main-div').find('.property-exist').removeClass('display-none');
		//remove the property list button class to activate
		$(this).closest('.operator-main-div').siblings('.property-main-div').find('.property-nonexist .displayExistingProperty').removeClass('event-none');
	});
	$(document).on('click', '.displayNonExistingOperator', function (event) {
		$(this).closest('.operator-exist').siblings('.operator-nonexist').removeClass('display-none');
		$(this).closest('.operator-exist').addClass('display-none');
		//property section
		$(this).closest('.operator-main-div').siblings('.property-main-div').find('.property-nonexist').removeClass('display-none');
		$(this).closest('.operator-main-div').siblings('.property-main-div').find('.property-exist').addClass('display-none');
		//disable the property list button
		$(this).closest('.operator-main-div').siblings('.property-main-div').find('.property-nonexist .displayExistingProperty').addClass('event-none');
	});
	$(document).on('click', '.displayExistingProperty', function (event) {
		$(this).closest('.property-nonexist').addClass('display-none');
		$(this).closest('.property-nonexist').siblings('.property-exist').removeClass('display-none');
	});
	$(document).on('click', '.displayNonExistingProperty', function (event) {
		$(this).closest('.property-exist').siblings('.property-nonexist').removeClass('display-none');
		$(this).closest('.property-exist').addClass('display-none');
	});
	$(document).on('change', '.operatorName', function (event) {
		//console.log($(this).closest('.operatorDropdown').select2().find(":selected").data("user-id"));
		var testInteger = /^\d+$/.test($(this).val());
		//console.log($(this).val());
		//console.log($(this).find(":selected").html());
		//console.log($(this).find(":selected").attr('user-id'));
		//console.log($(this).prop('user-id'));
		if ($(this).val() != "Please select") {
			//console.log($(this).closest('.operator-main-div').siblings('.property-main-div').html());
			//console.log(testInteger);
			//$(this).closest('.operator-main-div').siblings('.property-main-div').removeClass('event-none__property');
			var propertyListTag = $(this).closest('.operator-main-div').siblings('.property-main-div').find(".propertyDropdown");
			//console.log($(this).closest('.operator-main-div').html())
			$.ajax({
				type: "GET",
				url: "/Addons/PropertyListDropdownl",
				data: { UserId: $(this).find(":selected").attr('user-id') },
				context: this,
				dataType: "json",
				success: function (response) {
					console.log(response);
					//console.log($(this).closest('.operator-main-div').html())
					propertyListTag.html("");
					propertyListTag.append("<option value='Please select'>--Please select--</option>");

					for (var key in response) {
						if (response.hasOwnProperty(key)) {
							//console.log(response[key].companyName)
							if (response[key].name != null) {

								propertyListTag.append("<option value="+response[key].name+">" + response[key].name + "</option>");
							}
						}
					}
					propertyListTag.select2();
					//console.log($('.propertyDropdown').html());
				},
				error: function (response) {
					alert("server not ready please try afterwards");
				}
			});
			$(this).closest('.operator-main-div').siblings('.property-main-div').removeClass('event-none__property');

		}
		//else if ($(this).val() != "Please select" && testInteger == false) {
		//	console.log(1)
		//	$(this).closest('.operator-main-div').siblings('.property-main-div').removeClass('event-none__property');
		//}
		else if ($(this).val() == "Please select") {
			//console.log(2)
			$(this).closest('.operator-main-div').siblings('.property-main-div').addClass('event-none__property');
		}
	});
//})



//project upload section start
function addProject(obj) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	operatorListDropdown();

	var listingId = $(obj).attr('data-listingid');
	$('.project-upload').prepend(
		'<div class="row project-upload__row mb-4">' +
		'<div class="display-none">'+
		'<div class="form-group">'+
		'<input type="text" class="form-control projectId event-none" placeholder="id" value="0">'+
		'<label for="input" class="control-label">Project id</label><i class="bar"></i>'+
		'</div>'+
		'</div>' +
		//operator List
		'<div class=" col-md-3 col-sm-6 align-self-center operator-main-div">' +

		'<div class="operator-exist">' +
		'<div class="font-11">If Operator Not Exist click <span class="displayNonExistingOperator bg-primary small-btn__action">Add</span></div>' +
		'<div class="form-group operatorDropdown-div">' +
		'<select class="form-control basic-select operatorDropdown operatorName">' +
		'</select>' +
		'<label for="input" class="control-label">Operator Name*</label><i class="bar"></i>' +
		'</div>' +
		'</div>' +

		'<div class="operator-nonexist display-none">' +
		'<div class="font-11">Select on listed operator <span class="displayExistingOperator bg-sec small-btn__action">Operator List</span></div>' +
		'<div class="form-group">' +
		'<input type="text" user-id="new" class="form-control operatorName" placeholder="Operator Name">' +
		'<label for="input" class="control-label">Operator Name*</label><i class="bar"></i>' +
		'</div>' +
		'</div>' +

		'</div>' +
		//Property list
		'<div class=" col-md-3 col-sm-6  align-self-center property-main-div event-none__property">' +

		'<div class="property-exist">' +
		'<div class="font-11">If Property Not Exist click <span class="displayNonExistingProperty bg-primary small-btn__action">Add</span></div>' +
		'<div class="form-group propertyDropdown-div">' +
		'<select class="form-control basic-select propertyDropdown propertyName">' +
		'<option value=" ">Select Operator First</option>' +
		'</select>' +
		'<label for="input" class="control-label">Property Name*</label><i class="bar"></i>' +
		'</div>' +
		'</div>' +

		'<div class="property-nonexist display-none">' +
		'<div class="font-11">Select on listed property <span class="displayExistingProperty bg-sec small-btn__action">property List</span></div>' +
		'<div class="form-group">' +
		'<input type="text" class="form-control propertyName" placeholder="property Name">' +
		'<label for="input" class="control-label">Property Name*</label><i class="bar"></i>' +
		'</div>' +
		'</div>' +

		'</div>' +

		
		
		'<div class=" col-md-3 col-sm-6 align-self-center">'+
			'<div class="form-group">'+
				'<select class="form-control basic-select ProjectRole">'+
					'<option value="PropertyDeveloper">Property Developer</option>'+
					'<option value="Leasing">Leasing</option>'+
					'<option value="InteriorDesigner">Interior Designer</option>'+
					'<option value="CoworkingArchitecture">Co-working Architecture</option>'+
					'<option value="Investor">Investor</option>'+
					'<option value="PropertyOwner">Property Owner</option>'+
					'<option value="PropertyOperator">Property Operator</option>'+
				'</select>'+
				'<label for="input" class="control-label">RE Professional Role</label><i class="bar"></i>'+
			'</div>'+
		'</div>' +
		'<div class=" col-md-3 col-sm-6 align-self-center">'+
			'<div class="form-group">'+
				'<input type="text" class="form-control PropertyReraId" placeholder="ABCD12345">'+
					'<label for="input" class="control-label">RERA ID</label><i class="bar"></i>'+
								'</div>'+
			'</div>'+
			'<div class=" col-md-3 col-sm-6 align-self-center">'+
				'<div class="form-group">'+
					'<select class="form-control basic-select PropertyAdditionalIdName">'+
						'<option value="SurveyNumber">Survey No.</option>'+
						'<option value="PropertyTaxBillNumber">Property Tax Bill No.</option>'+
						'<option value="CTSNumber">CTS No.</option>'+
						'<option value="MilkatNumber">Milkat No.</option>'+
						'<option value="GatNumber">Gat No.</option>'+
						'<option value="PlotNumber">Plot No.</option>'+
					'</select>'+
					'<label for="input" class="control-label">Additional Document</label><i class="bar"></i>'+
				'</div>'+
			'</div>'+
			'<div class=" col-md-3 col-sm-6 align-self-center">'+
				'<div class="form-group">'+
					'<input type="text" class="form-control PropertyAdditionalIdNumber" placeholder="ABCD12345">'+
						'<label for="input" class="control-label">Document Value/No.</label><i class="bar"></i>'+
								'</div>'+
		'</div>' +
		'<div class=" col-md-6 col-sm-12 align-self-center">' +
		'<div class="form-group">' +
		'<textarea type="text" class="form-control projectDesc" rows="3" placeholder="Enter your text..."></textarea>' +
		'<label for="input" class="control-label">Description*</label><i class="bar"></i>' +
		'<div class="error"></div>'+
		'</div>' +
		'</div>' +
		'<div class=" col-md-3 col-sm-6 align-self-center">' +
		'<div class="form-group">' +
		'<input type="file" class="form-control projectImage" accept="project/*">' +
		'<label for="input" class="control-label">Upload project Image*</label><i class="bar"></i>' +
		'<div class="error"></div>'+
		'</div>' +
		'</div>' +

		'<div class=" col-md-2 col-sm-6 align-self-center">' +
		'<div class="form-group">' +
		'<input type="file" class="form-control projectDoucument" accept="project/*">' +
		'<label for="input" class="control-label">Upload project Document</label><i class="bar"></i>' +
		'</div>' +
		'</div>' +
		//'<div class="col-md-1 col-sm-6 m-b--15 align-self-center">' +
		//'<div class="checkbox m-0">' +
		//'<label>' +
		//'<input type="checkbox" /><i class="helper"></i> Active' +
		//'</label>' +
		//'</div>' +
		//'</div>' +
		'<div class="col-md-12 col-sm-6 align-self-center text-center">' +
		'<span class="addon-add delete-btn tooltip-wrapper text-sec" onclick="AddProjectForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="submit and upload the file"><i class="fas fa-cloud-upload-alt btn-icon text-sec"></i> Upload</span>' +
		'<span class="addon-edit delete-btn tooltip-wrapper display-none text-info" onclick="EditProjectForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="Edit the file"><i class="fas fa-edit btn-icon text-info"></i> Edit</span>' +
		'<span class="addon-delete delete-btn tooltip-wrapper text-danger" onclick="deleteRowProject(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to delete the row"><i class="fas fa-trash-alt btn-icon text-danger"></i> Delete</span>' +
		//'<span class="addon-clear delete-btn tooltip-wrapper display-none" onclick="clearRowImage(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i></span>'+
		'</div>' +
		'</div>'
	);
	$('.project-upload__row:first-child .ProjectRole').select2();
	$('.project-upload__row:first-child .PropertyAdditionalIdName').select2();
	$('.project-upload__row:first-child .operatorDropdown').select2();
	$('.project-upload__row:first-child .propertyDropdown').select2();
	removeLoader();
}

function deleteRowProject(obj) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	$(obj).closest('.project-upload__row').remove();
	removeLoader();
};

//project upload section end
function AddProjectForm(obj) {
	var listingId = $(obj).attr('data-listingid');
	//console.log(listingId)
	var formData = new FormData();
	var row = $(obj).closest('.project-upload__row');
	var pStatus = true;
	var pProjectId;
	var Imagefiles = $(row).find('.projectImage').get(0).files;
	var Documentfiles = $(row).find('.projectDoucument').get(0).files;

	var pName;
	if ($('.propertyDropdown').is(':visible')) {
		pName = $(row).find('.propertyName:visible option:selected').text();
	} else {
		pName = $(row).find('.propertyName:visible').val();
	}	 
	console.log(pName)

	var oName;
	if ($('.operatorDropdown').is(':visible')) {
	oName = $(row).find('.operatorName:visible option:selected').text();
	} else {
		oName = $(row).find('.operatorName:visible').val();
	}
	console.log(oName)


	var pImagePath = $(row).find('.projectImage').val();
	var pDocumentPath = $(row).find('.projectDoucument').val();
	var pDesc = $(row).find('.projectDesc').val();
	var pRole = $(row).find('.ProjectRole').val();
	var pRera = $(row).find('.PropertyReraId').val();
	var pAdditionalIdName = $(row).find('.PropertyAdditionalIdName').val();
	var pAdditionalIdValue = $(row).find('.PropertyAdditionalIdNumber').val();
	//project validation start
	if (pDesc == "") {
		$(row).find('.projectDesc').siblings('.error').html('This feild is required');
	}
	if (pImagePath == "") {
		$(row).find('.projectImage').siblings('.error').html('This feild is required');
	}
	if (pDesc == "" || pImagePath == "") {
		return false;
	} else {
		$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	}
	//project validation end
	if ($(row).find('.projectId').length) {
		pProjectId = $(row).find('.projectId').val();
	} else {
		pProjectId = 0;
	}
	formData.append("File_Image", Imagefiles[0]);
	formData.append("File_Document", Documentfiles[0]);
	formData.append("REProfessionalMasterId", pProjectId);
	formData.append("ListingId", listingId);
	formData.append("ProjectName", pName);
	formData.append("OperatorName", oName);
	formData.append("ImageUrl", pImagePath);
	formData.append("DocumentUrl", pDocumentPath);
	formData.append("Status", pStatus);
	formData.append("Description", pDesc);
	formData.append("ProjectRole", pRole);
	formData.append("PropertyReraId", pRera);
	formData.append("PropertyAdditionalIdName", pAdditionalIdName);
	formData.append("PropertyAdditionalIdNumber", pAdditionalIdValue);
	//console.log(formData)
	//var url = '@Url.Action("UploadImage", "Addons")';
	$.ajax({
		type: "POST",
		url: "/Addons/UploadProject",
		data: formData,
		processData: false,
		contentType: false,
		success: function (response) {
			if (response != null) {
				console.log(response);
				$(row).addClass("addons-row");
				$(row).removeClass("addons-row__edit");

				$(row).find('.propertyName').addClass("event-none");
				$(row).find('.propertyName + .select2-container').addClass("event-none");

				$(row).find('.operatorName').addClass("event-none");
				$(row).find('.operatorName + .select2-container').addClass("event-none");

				$(row).find('.projectDesc').addClass("event-none");
				$(row).find('.PropertyReraId').addClass("event-none");
				$(row).find('.PropertyAdditionalIdNumber').addClass("event-none");
				$(row).find('.ProjectRole + .select2-container').addClass("event-none");
				$(row).find('.PropertyAdditionalIdName + .select2-container').addClass("event-none");

				$(row).find('.projectId').val(response.reProfessionalMasterId);
				console.log($(row).find('.projectId').val());
				if (response.imageUrl != null) {
					$(row).find('.projectImage').parent().parent().append(
						'<div class="addon-image__div"><a href=' + response.imageUrl + ' target="_blank"><img class="addon-image" alt="name" src=' + response.imageUrl + ' /></a></div>'
					);
					$(row).find('.projectImage').parent().addClass("display-none");
				} else {
					$(row).find('.projectImage').parent().parent().append(
						'<div class="addon-image__div pop-image__error">No Image Uploaded</div>'
					);
					$(row).find('.projectImage').parent().addClass("display-none");
				}
				$(row).find('.projectDesc').html(response.description);
				if (response.documentUrl) {
					$(row).find('.projectDoucument').parent().parent().append(
						'<div class="addon-image__div"><a href=' + response.documentUrl + ' target="_blank"><i class="fas fa-file-alt"></i>Click to view</a></div>'
					);
					$(row).find('.projectDoucument').parent().addClass("display-none");
				} else {
					$(row).find('.projectDoucument').parent().parent().append(
						'<div class="addon-image__div pop-image__error">No Document Uploaded</div>'
					);
					$(row).find('.projectDoucument').parent().addClass("display-none");
				}
				
				//$(row).find('.imageCheck').prop("checked", response.status);
				$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
				$(obj).siblings('.addon-delete').attr('onclick', "deleteProject(this," + response.reProfessionalMasterId + ")");
				//$(obj).siblings('.addon-clear').addClass('display-none');
				if ($(obj).siblings('.addon-clear').length) {
					$(obj).siblings('.addon-clear').addClass('display-none');
				} else {
					$(obj).parent().append(
						'<span class="addon-clear delete-btn tooltip-wrapper display-none text-pry" onclick="clearRowProject(this,' + response.reProfessionalMasterId + ')" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i> Clear</span>'
					);
				};
				$(obj).addClass('display-none');
				removeLoader();
			}
		},
		error: function (response) {
			removeLoader();
			alert("server not ready please upload afterwards");
		}
	});
}

//edit project section
function EditProjectForm(obj) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	var row = $(obj).closest('.project-upload__row');
	$(row).removeClass("addons-row");
	$(row).addClass("addons-row__edit");

	//operator name section


	$(row).find('.propertyName').removeClass("event-none");
	$(row).find('.propertyName.cantedit').addClass("event-none");
	$(row).find('.propertyName + .select2-container').removeClass("event-none");
	$(row).find('.operatorName').removeClass("event-none");
	$(row).find('.operatorName.cantedit').addClass("event-none");
	$(row).find('.operatorName + .select2-container').removeClass("event-none");


	$(row).find('.projectDesc').removeClass("event-none");
	$(row).find('.ProjectRole').removeClass("event-none");
	$(row).find('.PropertyAdditionalIdName').removeClass("event-none");
	$(row).find('.PropertyReraId').removeClass("event-none");
	$(row).find('.PropertyAdditionalIdNumber').removeClass("event-none");
	$(row).find('.ProjectRole + .select2-container').removeClass("event-none");
	$(row).find('.PropertyAdditionalIdName, .ProjectRole').select2();
	//$(row).find('.ProjectRole').select2();
	$(row).find('.PropertyAdditionalIdName + .select2-container').removeClass("event-none");

	$(row).find('.projectImage').parent().removeClass("display-none");
	$(row).find('.projectDoucument').parent().removeClass("display-none");
	$(row).find('.projectImage').parent().siblings(".addon-image__div").remove();
	$(row).find('.projectDoucument').parent().siblings(".addon-image__div").remove();
	$(row).find('.projectImage').parent().css("margin-bottom", "0");
	$(row).find('.projectDoucument').parent().css("margin-bottom", "0");
	$(obj).siblings('.addon-add, .addon-clear').removeClass('display-none');
	$(obj).siblings('.addon-delete').addClass('display-none');
	$(obj).addClass('display-none');
	removeLoader();
}

//Reset project section
function clearRowProject(obj, projectId) {
	//console.log(imageId)
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	var row = $(obj).closest('.project-upload__row');
	$.ajax({
		type: "GET",
		url: "/Addons/GetProject",
		dataType: "json",
		data: { id: projectId },
		success: function (response) {
			if (response != null) {
				console.log(response);
				$(row).addClass("addons-row");
				$(row).removeClass("addons-row__edit");
				$(row).find('.projectId').val(response.reProfessionalMasterId);

				$(row).find('.propertyName').addClass("event-none");
				$(row).find('.propertyName + .select2-container').addClass("event-none");
				$(row).find('.operatorName').addClass("event-none");
				$(row).find('.operatorName + .select2-container').addClass("event-none");

				$(row).find('.projectDesc').addClass("event-none");
				$(row).find('.PropertyReraId').addClass("event-none");
				$(row).find('.PropertyAdditionalIdNumber').addClass("event-none");
				$(row).find('.ProjectRole + .select2-container').addClass("event-none");
				$(row).find('.PropertyAdditionalIdName + .select2-container').addClass("event-none");

				$(row).find('.propertyName').val(response.projectName);
				$(row).find('.ProjectRole').val(response.projectRole);
				$(row).find('.projectDesc').html(response.description);
				if (response.imageUrl != null) {
					$(row).find('.projectImage').parent().parent().append(
						'<div class="addon-image__div"><a href=' + response.imageUrl + ' target="_blank"><img class="addon-image" alt="name" src=' + response.imageUrl + ' /></a></div>'
					);
					$(row).find('.projectImage').parent().addClass("display-none");
				}
				else {
					$(row).find('.projectImage').parent().parent().append(
						'<div class="addon-image__div pop-image__error">No Image Uploaded</div>'
					);
					$(row).find('.projectImage').parent().addClass("display-none");
				}
				if (response.documentUrl) {
					$(row).find('.projectDoucument').parent().parent().append(
						'<div class="addon-image__div"><a href=' + response.documentUrl + ' target="_blank"><i class="fas fa-file-alt"></i>Click to view</a></div>'
					);
					$(row).find('.projectDoucument').parent().addClass("display-none");
				} else {
					$(row).find('.projectDoucument').parent().parent().append(
						'<div class="addon-image__div pop-image__error">No Document Uploaded</div>'
					);
					$(row).find('.projectDoucument').parent().addClass("display-none");
				}
				
				//$(row).find('.imageCheck').prop("checked", response.status);
				$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
				$(obj).siblings('.addon-add').addClass('display-none');
				$(obj).addClass('display-none');
				removeLoader();
			}
		},
		error: function (response) {
			removeLoader();
			alert(response);
		}
	})
}


//Delete project section
function deleteProject(obj, projectId) {
	$(".modal:visible .modal-body").append("<div class='loader_new-sub'></div>");
	var row = $(obj).closest('.project-upload__row');
	if (projectId != 0) {
		$.ajax({
			type: "GET",
			url: "/Addons/DeleteProject",
			dataType: "json",
			data: { id: projectId },
			success: function (response) {
				if (response != null) {
					console.log(response);
					deleteRowProject(obj);
				}
			},
			error: function (response) {
				removeLoader();
				alert("server not ready please delete afterwards");
			}
		})
	}
	else {
		deleteRowProject(obj);
	}
}
//project upload section end
//--------------------------------------------------------------------------------------------------//
//************************************adding Project section end************************************//
//--------------------------------------------------------------------------------------------------//

//--------------------------------------------------------------------------------------------------//
//************************************adding Amenity section start**********************************//
//--------------------------------------------------------------------------------------------------//

//amenities upload section start
function addAmenities(obj) {
	var listingId = $(obj).attr('data-listingid');
	$('.amenities-upload').append(
		'<div class="row amenities-upload__row ">' +
		'<div class="display-none">'+
		'<div class="form-group">'+
		'<input type="text" class="form-control amenityId" placeholder="00" value="0">'+
		'<label for="input" class="control-label">Amenity Id</label><i class="bar"></i>'+
		'</div>'+
		'</div>' +
		'<div class="display-none">'+
		'<div class="form-group">'+
		'<input type="text" class="form-control amenityMasterId" placeholder="00" value="0">'+
		'<label for="input" class="control-label">Master Id</label><i class="bar"></i>'+
		'</div>'+
		'</div>'+
		'<div class=" col-md-3 col-sm-4">' +
		'<div class="form-group">' +
		'<input type="text" class="form-control amenityName" placeholder="Name">' +
		'<label for="input" class="control-label">Amenity</label><i class="bar"></i>' +
		'<div class="error"></div>'+
		'</div>' +
		'</div>' +
		'<div class="col-md-2 col-sm-4 col-6 ">' +
		'<div class="form-group">' +
		'<select class="form-control basic-select am-option amenityType" id="">' +
		'<option value="Free">Free</option>' +
		'<option value="Paid">Paid</option>' +
		'<option value="PartiallyPaid">Partially Paid</option>' +
		'</select>' +
		'<label for="" class="control-label">Cost</label>' +
		'<i class="bar"></i>' +
		'</div>' +
		'</div>' +
		'<div class=" am-price col-md-2 col-sm-3 d-none">' +
		'<div class="form-group">' +
		'<input type="number" class="form-control amenityPrice" placeholder="00">' +
		'<label for="input" class="control-label">Price/Usage</label><i class="bar"></i>' +
		'<div class="error"></div>'+
		'</div>' +
		'</div>' +
		'<div class=" am-partial col-md-2 col-sm-3 d-none">' +
		'<div class="form-group">' +
		'<input type="number" class="form-control amenityCount" placeholder="00">' +
		'<label for="input" class="control-label">Free Count</label><i class="bar"></i>' +
		'<div class="error"></div>'+
		'</div>' +
		'</div>	' +


		'<div class="col-md-1 col-sm-6 m-b--15 align-self-center">' +
		'<div class="checkbox m-0">' +
		'<label>' +
		'<input type="checkbox" class="amenityStatus" /><i class="helper"></i> Active' +
		'</label>' +
		'</div>' +
		'</div>' +
		'<div class="col-md-2 col-sm-6 align-self-center">' +
		'<span class="addon-add delete-btn tooltip-wrapper text-sec" onclick="AddAmenityForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="submit and upload the file"><i class="fas fa-save btn-icon text-sec"></i> Save</span>' +
		'<span class="addon-edit delete-btn tooltip-wrapper display-none text-info" onclick="EditAmenityForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="Edit the file"><i class="fas fa-edit btn-icon text-info"></i> Edit</span>' +
		'<span class="addon-delete delete-btn tooltip-wrapper text-danger" onclick="deleteRowAmenities(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to delete the row"><i class="fas fa-trash-alt btn-icon text-danger"></i> Delete</span>' +
		//'<span class="addon-clear delete-btn tooltip-wrapper display-none" onclick="clearRowImage(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i></span>'+
		'</div>' +
		'</div>'
	);
}
function deleteRowAmenities(that) {
	$(that).closest('.amenities-upload__row').remove();
};

//Amenity upload section end
function AddAmenityForm(obj) {
	var listingId = $(obj).attr('data-listingid');
	//console.log(listingId)
	var formData = new FormData();
	var row = $(obj).closest('.amenities-upload__row');
	if ($(row).find('.amenityStatus').is(':checked')) {
		amenityStatus = true;
	} else {
		amenityStatus = false;
	}
	//console.log(amenityStatus)
	var amenityName = $(row).find('.amenityName').val();
	if ($(row).find('.amenityId').length) {
		amenityId = $(row).find('.amenityId').val();
	} else {
		amenityId = 0;
	}
	if ($(row).find('.amenityMasterId').length) {
		amenityMasterId = $(row).find('.amenityMasterId').val();
	} else {
		amenityMasterId = 0;
	}
	var amenityType = $(row).find('.amenityType').val();
	var amenityPrice = $(row).find('.amenityPrice').val();
	var amenityCount = $(row).find('.amenityCount').val();

	//Amenities validation start
	if (amenityType == "PartiallyPaid") {
		if (amenityName == "") {
			$(row).find('.amenityName').siblings('.error').html('This feild is required');
		}
		if (amenityPrice == "") {
			$(row).find('.amenityPrice').siblings('.error').html('This feild is required');
		}
		if (amenityCount == "") {
			$(row).find('.amenityCount').siblings('.error').html('This feild is required');
		}
		if (amenityPrice == "" || amenityCount == "" || amenityName == "") {
			return false;
		}
	} else if (amenityType == "Paid") {
		if (amenityName == "") {
			$(row).find('.amenityName').siblings('.error').html('This feild is required');
		}
		if (amenityPrice == "") {
			$(row).find('.amenityPrice').siblings('.error').html('This feild is required');
		}
		if (amenityPrice == "" || amenityName == "") {
			return false;
		}
	}
	else if (amenityType == "Free") {
		if (amenityName == "") {
			$(row).find('.amenityName').siblings('.error').html('This feild is required');
			return false;
		}
	}
	
	//Amenities validation end

	formData.append("AmenityId", amenityId);
	formData.append("ListingId", listingId);
	formData.append("AmenityMasterId", amenityMasterId);
	formData.append("Name", amenityName);
	formData.append("AmenitiesPayment", amenityType);
	formData.append("PartialCount", amenityCount);
	formData.append("Price", amenityPrice);
	formData.append("Status", amenityStatus);
	//console.log(formData);
	// Display the key/value pairs
	//for (var pair of formData.entries()) {
	//	console.log(pair[0] + ', ' + pair[1]);
	//}

	$.ajax({
		type: "POST",
		url: "/Addons/UploadAmenity",
		data: formData,
		processData: false,
		contentType: false,
		success: function (response) {
			if (response != null) {
				console.log(response);
				$(row).addClass("addons-row__light");
				$(row).removeClass("addons-row__edit");
				$(row).find('.amenityName, .amenityType, .amenityCount, .amenityPrice, .amenityStatus').addClass("event-none");

				$(row).find('.amenityId').val(response.amenityId);
				$(row).find('.amenityMasterId').val(response.amenityMasterId);
				$(row).find('.amenityName').val(response.name);
				$(row).find('.amenityStatus').prop("checked", response.status);
				$(row).find('.amenityType').val(response.amenitiesPayment);
				$(row).find('.amenityCount').val(response.partialCount);
				$(row).find('.amenityPrice').val(response.price);

				if ($(obj).siblings('.addon-delete').length) {
					$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
					$(obj).siblings('.addon-delete').attr('onclick', "deleteAmenity(this," + response.amenityId + ")");
				} else {
					$(obj).siblings('.addon-edit').removeClass('display-none');
				}

				if ($(obj).siblings('.addon-clear').length) {
					$(obj).siblings('.addon-clear').addClass('display-none');
				} else {
					$(obj).parent().append(
						'<span class="addon-clear delete-btn tooltip-wrapper display-none text-pry" onclick="clearRowAmenity(this,' + response.amenityId + ')" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i> Clear</span>'
					);
				};
				$(obj).addClass('display-none');
			}
		},
		error: function (response) {
			alert("server not ready please upload afterwards");
		}
	});
}

//edit amenity section
function EditAmenityForm(obj) {
	var row = $(obj).closest('.amenities-upload__row');
	$(row).removeClass("addons-row__light");
	$(row).addClass("addons-row__edit");
	$(row).find('.amenityName, .amenityType, .amenityCount, .amenityPrice, .amenityStatus').removeClass("event-none");

	$(obj).siblings('.addon-add, .addon-clear').removeClass('display-none');
	$(obj).siblings('.addon-delete').addClass('display-none');
	$(obj).addClass('display-none');
}

//Reset amenity section
function clearRowAmenity(obj, amenityId) {
	//console.log(imageId)
	var row = $(obj).closest('.amenities-upload__row');
	$.ajax({
		type: "GET",
		url: "/Addons/GetAmenity",
		dataType: "json",
		data: { id: amenityId },
		success: function (response) {
			if (response != null) {
				console.log(response);
				$(row).addClass("addons-row__light");
				$(row).removeClass("addons-row__edit");
				$(row).find('.amenityName, .amenityType, .amenityCount, .amenityPrice, .amenityStatus').addClass("event-none");

				$(row).find('.amenityId').val(response.amenityId);
				$(row).find('.amenityMasterId').val(response.amenityMasterId);
				$(row).find('.amenityName').val(response.name);
				$(row).find('.amenityStatus').prop("checked", response.status);
				$(row).find('.amenityType').val(response.amenitiesPayment);
				$(row).find('.amenityCount').val(response.partialCount);
				$(row).find('.amenityPrice').val(response.price);

				if ($(obj).siblings('.addon-delete').length) {
					$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
					$(obj).siblings('.addon-delete').attr('onclick', "deleteAmenity(this," + response.amenityId + ")");
				} else {
					$(obj).siblings('.addon-edit').removeClass('display-none');
				}

				//$(row).find('.imageCheck').prop("checked", response.status);
				$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
				$(obj).siblings('.addon-add').addClass('display-none');
				$(obj).addClass('display-none');
			}
		},
		error: function (response) {
			alert(response);
		}
	})
}

//Delete amenity section
function deleteAmenity(obj, amenityId) {
	var row = $(obj).closest('.amenities-upload__row');
	if (amenityId != 0) {
		$.ajax({
			type: "GET",
			url: "/Addons/DeleteAmenity",
			dataType: "json",
			data: { id: amenityId },
			success: function (response) {
				if (response != null) {
					console.log(response);
					deleteRowAmenities(obj);
				}
			},
			error: function (response) {
				alert("server not ready please delete afterwards");
			}
		})
	}
	else {
		deleteRowAmenities(obj);
	}
}
//amenities upload section end
//--------------------------------------------------------------------------------------------------//
//************************************adding Amenity section end************************************//
//--------------------------------------------------------------------------------------------------//

//--------------------------------------------------------------------------------------------------//
//************************************adding Facility section start************************************//
//--------------------------------------------------------------------------------------------------//
//facilities upload section start
function addFacilities(obj) {
	var listingId = $(obj).attr('data-listingid');
	$('.facilities-upload').append(
		'<div class="row facilities-upload__row">' +
		'<div class="display-none">'+
		'<div class="form-group">'+
		'<input type="text" class="form-control facilityMasterId" placeholder="00" value="0">'+
		'<label for="input" class="control-label">Master Id</label><i class="bar"></i>'+
		'</div>'+
		'</div>'+
		'<div class="display-none">'+
		'<div class="form-group">'+
		'<input type="text" class="form-control facilityId" placeholder="00" value="0">'+
		'<label for="input" class="control-label">Facility Id</label><i class="bar"></i>'+
		'</div>'+
		'</div>'+
		'<div class=" col-md-3 col-sm-4 ">' +
		'<div class="form-group">' +
		'<input type="text" class="form-control facilityName" placeholder="Name">' +
		'<label for="input" class="control-label">Facility</label><i class="bar"></i>' +
		'</div>' +
		'</div>' +
		'<div class="col-lg-3 col-md-3 col-sm-4 col-6 ">' +
		'<div class="form-group">' +
		'<select class="form-control am-option facilityDistance" id="">' +
		'<option value="0 to .5KM">0 to .5KM</option>' +
		'<option value=".5KM to 1KM">.5KM to 1KM</option>' +
		'<option value="1KM to 2KM">1KM to 2KM</option>' +
		'<option value="2KM to 3KM">2KM to 3KM</option>' +
		'<option value="3KM to 4KM">3KM to 4KM</option>' +
		'<option value="4KM to 5KM">4KM to 5KM</option>' +
		'<option value="5KM to 6KM">5KM to 6KM</option>' +
		'<option value="6KM to 7KM">6KM to 7KM</option>' +
		'<option value="7KM to 8KM">7KM to 8KM</option>' +
		'<option value="8KM to 9KM">8KM to 9KM</option>' +
		'<option value="9KM to 10KM">9KM to 10KM</option>' +
		'<option value="Above 10KM">Above 10KM</option>' +
		'</select>' +
		'<label for="" class="control-label">Distance</label>' +
		'<i class="bar"></i>' +
		'</div>' +
		'</div>' +
		'<div class="col-md-2 col-sm-6 m-b--15 align-self-center">' +
		'<div class="checkbox m-0">' +
		'<label>' +
		'<input type="checkbox" class="facilityStatus" /><i class="helper"></i> Active' +
		'</label>' +
		'</div>' +
		'</div>' +
		'<div class="col-md-3 col-sm-6 align-self-center">' +
		'<span class="addon-add delete-btn tooltip-wrapper text-sec" onclick="AddFacilityForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="submit and upload the file"><i class="fas fa-save btn-icon text-sec"></i> Save</span>' +
		'<span class="addon-edit delete-btn tooltip-wrapper display-none text-info" onclick="EditFacilityForm(this);" data-listingid=' + listingId + ' data-toggle="tooltip" data-placement="top" title="" data-original-title="Edit the file"><i class="fas fa-edit btn-icon text-info"></i> Edit</span>' +
		'<span class="addon-delete delete-btn tooltip-wrapper text-danger" onclick="deleteRowFacilities(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to delete the row"><i class="fas fa-trash-alt btn-icon text-danger"></i> Delete</span>' +
		//'<span class="addon-clear delete-btn tooltip-wrapper display-none" onclick="clearRowImage(this)" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i></span>'+
		'</div>' +
		'</div>'
	);
}
function deleteRowFacilities(that) {
	$(that).closest('.facilities-upload__row').remove();
};

//Facility upload section end
function AddFacilityForm(obj) {
	var listingId = $(obj).attr('data-listingid');
	//console.log(listingId)
	var formData = new FormData();
	var row = $(obj).closest('.facilities-upload__row');
	if ($(row).find('.facilityStatus').is(':checked')) {
		facilityStatus = true;
	} else {
		facilityStatus = false;
	}
	var facilityName = $(row).find('.facilityName').val();
	if ($(row).find('.facilityId').length) {
		facilityId = $(row).find('.facilityId').val();
	} else {
		facilityId = 0;
	}
	if ($(row).find('.facilityMasterId').length) {
		facilityMasterId = $(row).find('.facilityMasterId').val();
	} else {
		facilityMasterId = 0;
	}
	var facilityDistance = $(row).find('.facilityDistance').val();

	formData.append("facilityId", facilityId);
	formData.append("ListingId", listingId);
	formData.append("facilityMasterId", facilityMasterId);
	formData.append("Name", facilityName);
	formData.append("facilityDistance", facilityDistance);
	formData.append("Status", facilityStatus);
	//console.log(formData);
	// Display the key/value pairs
	//for (var pair of formData.entries()) {
	//	console.log(pair[0] + ', ' + pair[1]);
	//}

	$.ajax({
		type: "POST",
		url: "/Addons/UploadFacility",
		data: formData,
		processData: false,
		contentType: false,
		success: function (response) {
			if (response != null) {
				console.log(response);
				$(row).addClass("addons-row__light");
				$(row).removeClass("addons-row__edit");
				$(row).find('.facilityName, .facilityDistance, .facilityStatus').addClass("event-none");

				$(row).find('.facilityId').val(response.facilityId);
				$(row).find('.facilityMasterId').val(response.facilityMasterId);
				$(row).find('.facilityName').val(response.name);
				$(row).find('.facilityStatus').prop("checked", response.status);
				$(row).find('.facilityDistance').val(response.facilityDistance);

				if ($(obj).siblings('.addon-delete').length) {
					$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
					$(obj).siblings('.addon-delete').attr('onclick', "deleteFacility(this," + response.facilityId + ")");
				} else {
					$(obj).siblings('.addon-edit').removeClass('display-none');
				}

				if ($(obj).siblings('.addon-clear').length) {
					$(obj).siblings('.addon-clear').addClass('display-none');
				} else {
					$(obj).parent().append(
						'<span class="addon-clear delete-btn tooltip-wrapper display-none text-pry" onclick="clearRowFacility(this,' + response.facilityId + ')" data-toggle="tooltip" data-placement="top" title="" data-original-title="Click to clear"><i class="fas fa-times btn-icon text-pry"></i> Clear</span>'
					);
				};
				$(obj).addClass('display-none');
			}
		},
		error: function (response) {
			alert("server not ready please upload afterwards");
		}
	});
}

//edit facility section
function EditFacilityForm(obj) {
	var row = $(obj).closest('.facilities-upload__row');
	$(row).removeClass("addons-row__light");
	$(row).addClass("addons-row__edit");
	$(row).find('.facilityName, .facilityDistance, .facilityStatus').removeClass("event-none");

	$(obj).siblings('.addon-add, .addon-clear').removeClass('display-none');
	$(obj).siblings('.addon-delete').addClass('display-none');
	$(obj).addClass('display-none');
}

//Reset facility section
function clearRowFacility(obj, facilityId) {
	//console.log(imageId)
	var row = $(obj).closest('.facilities-upload__row');
	$.ajax({
		type: "GET",
		url: "/Addons/GetFacility",
		dataType: "json",
		data: { id: facilityId },
		success: function (response) {
			if (response != null) {
				//console.log(response);
				$(row).addClass("addons-row__light");
				$(row).removeClass("addons-row__edit");
				$(row).find('.facilityName, .facilityDistance, .facilityStatus').addClass("event-none");

				$(row).find('.facilityId').val(response.facilityId);
				$(row).find('.facilityMasterId').val(response.facilityMasterId);
				$(row).find('.facilityName').val(response.name);
				$(row).find('.facilityStatus').prop("checked", response.status);
				$(row).find('.facilityDistance').val(response.facilityDistance);

				if ($(obj).siblings('.addon-delete').length) {
					$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
					$(obj).siblings('.addon-delete').attr('onclick', "deleteFacility(this," + response.facilityId + ")");
				} else {
					$(obj).siblings('.addon-edit').removeClass('display-none');
				}

				$(obj).siblings('.addon-edit, .addon-delete').removeClass('display-none');
				$(obj).siblings('.addon-add').addClass('display-none');
				$(obj).addClass('display-none');
			}
		},
		error: function (response) {
			alert(response);
		}
	})
}

//Delete facility section
function deleteFacility(obj, facilityId) {
	var row = $(obj).closest('.facilities-upload__row');
	if (facilityId != 0) {
		$.ajax({
			type: "GET",
			url: "/Addons/DeleteFacility",
			dataType: "json",
			data: { id: facilityId },
			success: function (response) {
				if (response != null) {
					console.log(response);
					deleteRowFacilities(obj);
				}
			},
			error: function (response) {
				alert("server not ready please delete afterwards");
			}
		})
	}
	else {
		deleteRowFacilities(obj);
	}
}
//facilities upload section end
//--------------------------------------------------------------------------------------------------//
//************************************adding Facility section end************************************//
//--------------------------------------------------------------------------------------------------//


//--------------------------------------------------------------------------------------------------//
//**********************************adding Healthcheck section start********************************//
//--------------------------------------------------------------------------------------------------//
//$('body').on('click', '.health-upload__row .basic-select', function () {
//	if ($(this).val() == 0) {
//		$(this).closest('.health-upload__row').find('.check-grade span').html('Select the value');
//	} else {
//	}
//})
//AQI
$('body').on('click', '#AQI_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	console.log($(this).val());
	if ($(this).val() == "0-50") {
		console.log('a')
		grade.html('Good');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "51-100") {
		grade.html('Moderate');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "101-200") {
		grade.html('Unhealthy for Sensitive Groups');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "201-300") {
		grade.html('Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "301-400") {
		grade.html('Very Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == "401-500") {
		grade.html('Hazardous');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});
//Temperature
$('body').on('click', '#Temperature_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	if ($(this).val() == "<95") {
		grade.html('Cold - Action Required');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "95-99.9") {
		grade.html('Excellent');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "100-104") {
		grade.html('Good');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == ">104") {
		grade.html('Hot - Action Required');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});
//Humidity
$('body').on('click', '#Humidity_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	if ($(this).val() == "<30") {
		grade.html('Too Dry');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "30-50") {
		grade.html('Moderate');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == ">50") {
		grade.html('Too High');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});
//CO2
$('body').on('click', '#CO2_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	if ($(this).val() == "<400") {
		grade.html('Excellent');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "400-599") {
		grade.html('Good');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "600-999") {
		grade.html('Fair');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "1000-1499") {
		grade.html('Moderate');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "1500-1999") {
		grade.html('Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == ">2000") {
		grade.html('Hazardous');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});
//CO
$('body').on('click', '#CO_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	if ($(this).val() == "<4.4") {
		grade.html('Good');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "4.4-9.4") {
		grade.html('Moderate');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "9.5-12.4") {
		grade.html('Unhealthy for Sensitive Groups');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "12.5-15.4") {
		grade.html('Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "15.5-30.4") {
		grade.html('Very Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == "30.5-40.4") {
		grade.html('Hazardous');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == ">40.5") {
		grade.html('Very Hazardous');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});
//PM2.5
$('body').on('click', '#PM2Point5_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	if ($(this).val() == "<15.4") {
		grade.html('Good');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "15.4-40.4") {
		grade.html('Moderate');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "40.5-65.4") {
		grade.html('Unhealthy for Sensitive Groups');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "65.5-150.4") {
		grade.html('Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "150.5-250.4") {
		grade.html('Very Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == "250.5-350.4") {
		grade.html('Hazardous');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == ">350.5") {
		grade.html('Very Hazardous');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});
//PM10
$('body').on('click', '#PM10_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	if ($(this).val() == "<=54") {
		grade.html('Good');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "55-154") {
		grade.html('Moderate');
		grade.attr('class', '');
		grade.attr('class', 'text-sec font-weight-bold');
	} else if ($(this).val() == "155-254") {
		grade.html('Unhealthy for Sensitive Groups');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "255-354") {
		grade.html('Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-pry font-weight-bold');
	} else if ($(this).val() == "355-424") {
		grade.html('Very Unhealthy');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == "425-504") {
		grade.html('Hazardous');
		grade.attr('class', '');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == ">505") {
		grade.html('Very Hazardous');
		grade.attr('class', 'text-danger font-weight-bold');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});
//Moisture
$('body').on('click', '#Moisture_Data', function () {
	var grade = $(this).closest('.health-upload__row').find('.check-grade span');
	if ($(this).val() == "5%-12%") {
		grade.html('<i class="fab fa-pagelines"></i>');
		grade.attr('class', '');
		grade.attr('class', 'text-sec');
	} else if ($(this).val() == "13%-17%") {
		grade.html('<i class="fab fa-pagelines"></i>');
		grade.attr('class', '');
		grade.attr('class', 'text-pry');
	} else if ($(this).val() == "<5% or >17%") {
		grade.html('<i class="fab fa-pagelines"></i>');
		grade.attr('class', '');
		grade.attr('class', 'text-danger');
	} else if ($(this).val() == 0) {
		grade.html('Select the value');
		grade.attr('class', '');
	}
});

//Health check upload section end
function AddHealthCheckForm(obj) {
	//debugger
	var listingId = $(obj).attr('data-listingid');
	//console.log(listingId)
	var formData = new FormData();
	var row = $(obj).closest('.modal-body');
	var AQI_Data = $('#AQI_Data').val();
	var AQI_Grade = $('#AQI_Grade span').html();
	var Temperature_Data = $('#Temperature_Data').val();
	var Temperature_Grade = $('#Temperature_Grade span').html();
	var Humidity_Data = $('#Humidity_Data').val();
	var Humidity_Grade = $('#Humidity_Grade span').html();
	var CO2_Data = $('#CO2_Data').val();
	var CO2_Grade = $('#CO2_Grade span').html();
	var CO_Data = $('#CO_Data').val();
	var CO_Grade = $('#CO_Grade span').html();
	var PM2Point5_Data = $('#PM2Point5_Data').val();
	var PM2Point5_Grade = $('#PM2Point5_Grade span').html();
	var PM10_Data = $('#PM10_Data').val();
	var PM10_Grade = $('#PM10_Grade span').html();
	var Moisture_Data = $('#Moisture_Data').val();
	var Moisture_Grade;
	if (Moisture_Data == "5%-12%") {
		Moisture_Grade = "Green";
	} else if (Moisture_Data == "13%-17%") {
		Moisture_Grade = "Amber";
	} else if (Moisture_Data == "<5% or >17%") {
		Moisture_Grade = "Red";
	}
	var Status = true;
	var CreatedDateTime;
	var HealthCheckId;
	if ($('#CreatedDateTime').length) {
		CreatedDateTime = $('#CreatedDateTime').html();
	}
	else {
		CreatedDateTime = "";
	}
	if ($('#HealthCheckId').length) {
		HealthCheckId = $('#HealthCheckId').html();
	}
	else {
		HealthCheckId = 0;
	}

	formData.append("AQI_Data", AQI_Data);
	formData.append("AQI_Grade", AQI_Grade);
	formData.append("Temperature_Data", Temperature_Data);
	formData.append("Temperature_Grade", Temperature_Grade);
	formData.append("Humidity_Data", Humidity_Data);
	formData.append("Humidity_Grade", Humidity_Grade);
	formData.append("CO2_Data", CO2_Data);
	formData.append("CO2_Grade", CO2_Grade);
	formData.append("CO_Data", CO_Data);
	formData.append("CO_Grade", CO_Grade);
	formData.append("PM2Point5_Data", PM2Point5_Data);
	formData.append("PM2Point5_Grade", PM2Point5_Grade);
	formData.append("PM10_Data", PM10_Data);
	formData.append("PM10_Grade", PM10_Grade);
	formData.append("Moisture_Data", Moisture_Data);
	formData.append("Moisture_Grade", Moisture_Grade);
	formData.append("Status", Status);
	formData.append("ListingId", listingId);
	formData.append("HealthCheckId", HealthCheckId);
	formData.append("CreatedDateTime", CreatedDateTime);

	//var url = '@Url.Action("UploadImage", "Addons")';
	$.ajax({
		type: "POST",
		url: "/Addons/UploadHealthCheck",
		data: formData,
		processData: false,
		contentType: false,
		success: function (response) {
			if (response != null) {
				console.log(response);
				//$(row).addClass("addons-row");
				$('#ListingId').html(response.listingId);
				$('#HealthCheckId').html(response.healthCheckId);
				$('#CreatedDateTime').html(response.createdDateTime);
				$(row).removeClass("addons-row__edit");
				$('#AQI_Data, #AQI_Grade, #Temperature_Data, #Temperature_Grade, #Humidity_Data, #Humidity_Grade, #CO2_Data, #CO2_Grade, #CO_Data, #CO_Grade, #PM2Point5_Data, #PM2Point5_Grade, #PM10_Data, #PM10_Grade, #Moisture_Data, #Moisture_Grade ').addClass("event-none");

				$(obj).siblings('.addon-edit').removeClass('display-none');
				$(obj).siblings('.addon-clear').addClass('display-none');
				$(obj).addClass('display-none');
			}
		},
		error: function (response) {
			alert("server not ready please upload afterwards");
		}
	});
}

//edit facility section
function EditHealthCheckForm(obj) {
	var row = $(obj).closest('.modal-body');
	//$(row).removeClass("addons-row__light");
	$(row).addClass("addons-row__edit");
	$('#AQI_Data, #AQI_Grade, #Temperature_Data, #Temperature_Grade, #Humidity_Data, #Humidity_Grade, #CO2_Data, #CO2_Grade, #CO_Data, #CO_Grade, #PM2Point5_Data, #PM2Point5_Grade, #PM10_Data, #PM10_Grade, #Moisture_Data, #Moisture_Grade ').removeClass("event-none");

	$(obj).siblings('.addon-add, .addon-clear').removeClass('display-none');
	$(obj).addClass('display-none');
}

//--------------------------------------------------------------------------------------------------//
//**********************************adding Healthcheck section end********************************//
//--------------------------------------------------------------------------------------------------//

//--------------------------------------------------------------------------------------------------//
//*****************************adding Green Building data section start***************************//
//--------------------------------------------------------------------------------------------------//
$('body').on('click', '.radio_1', function () {
	var value = $(this).val();
	//console.log(value);
	if (value == "Yes") {
		$('.gb-input__authority, .gb-input__certificate').removeClass('display-none');
		$('.gb-input__apply, .gb-input__checklist, .gb-data').addClass('display-none');
	} else if (value == "No") {
		$('.gb-input__checklist').removeClass('display-none');
		$('.gb-data, .gb-input__authority, .gb-data').addClass('display-none');
	} else if (value == "Applied") {
		$('.gb-input__authority').removeClass('display-none');
		$('.gb-input__certificate, .gb-input__apply, .gb-input__checklist, .gb-data').addClass('display-none');
	}
})
$('body').on('click', '.radio_2', function () {
	var value = $(this).val();
	//console.log(value)
	if (value == "Yes") {
		$('.gb-data, .gb-input__apply').removeClass('display-none');
		$('.rating').removeClass('event-none');
		$('.rate-value').attr('value', '');
		$('.rate-base-layer').css('color', '#aaa');
		$('.rate-base-layer span i').removeClass('fas');
		$('.rate-base-layer span i').addClass('far');
		//$('.rating').removeAttr('data-rate-value');
	} else if (value == "No") {
		$('.gb-data').addClass('display-none');
	}
})
$('body').on('change', '#CertifiedBy', function () {
	var value = $(this).val();
	//console.log(value)
	if (value != 0) {
		$('.gb-data').removeClass('display-none');
		$('.gb-input__apply').addClass('display-none');
		$('.rating').addClass('event-none');
		$('.rate-value').attr('value', 5);
		$('.rate-base-layer').css('color', 'orange');
		$('.rate-base-layer span i').removeClass('far');
		$('.rate-base-layer span i').addClass('fas');
	} else {
		$('.gb-data').addClass('display-none');
	}
})

function AddGreenBuildingForm(obj) {
	//debugger
	var listingId = $(obj).attr('data-listingid');
	//console.log(listingId)
	var formData = new FormData();
	var row = $(obj).closest('.modal-body');
	var CertificationStatus = $("input[name='radio_1']:checked").val();
	var IntrestedToApply = $("input[name='radio_3']:checked").val();
	var CertifiedBy = $("#CertifiedBy").val();
	var CertificationNumber = $("#CertificationNumber").val();
	var SF_GreenPolicy = $("#SF_GreenPolicy").val();
	var SF_WasteCD = $("#SF_WasteCD").val();
	var SF_Commuting = $("#SF_Commuting").val();
	var SF_Landscaping = $("#SF_Landscaping").val();
	var SF_NonRoof = $("#SF_NonRoof").val();
	var SF_Roof = $("#SF_Roof").val();
	var SF_PollutionReduction = $("#SF_PollutionReduction").val();
	var SF_BuildingOM = $("#SF_BuildingOM").val();
	var WE_Fixtures = $("#WE_Fixtures").val();
	var WE_Harvesting = $("#WE_Harvesting").val();
	var WE_Treatment = $("#WE_Treatment").val();
	var WE_Reuse = $("#WE_Reuse").val();
	var WE_Metering = $("#WE_Metering").val();
	var WE_TurfArea = $("#WE_TurfArea").val();
	var EE_Refrigerants = $("#EE_Refrigerants").val();
	var EE_MinimumEP = $("#EE_MinimumEP").val();
	var EE_ImprovedEP = $("#EE_ImprovedEP").val();
	var EE_OnSiteRE = $("#EE_OnSiteRE").val();
	var EE_OffSiteRE = $("#EE_OffSiteRE").val();
	var EE_EnergyMetering = $("#EE_EnergyMetering").val();
	var HC_SmokeControl = $("#HC_SmokeControl").val();
	var HC_VEntilation = $("#HC_VEntilation").val();
	var HC_CO2Control = $("#HC_CO2Control").val();
	var HC_PollutionEquipment = $("#HC_PollutionEquipment").val();
	var HC_EcoChemicals = $("#HC_EcoChemicals").val();
	var HC_ThermalComfort = $("#HC_ThermalComfort").val();
	var HC_AbledPeople = $("#HC_AbledPeople").val();
	var HC_Facilities = $("#HC_Facilities").val();
	var IC_Innovation = $("#IC_Innovation").val();
	var IC_IGBC = $("#IC_IGBC").val();
	var Status = true;
	var CreatedDateTime;
	var GreenBuildingCheckId;
	if ($('#GreenBuildingAddForm #CreatedDateTime').length) {
		CreatedDateTime = $('#GreenBuildingAddForm #CreatedDateTime').html();
	}
	else {
		CreatedDateTime = "";
	}
	if ($('#GreenBuildingCheckId').length) {
		GreenBuildingCheckId = $('#GreenBuildingCheckId').html();
	}
	else {
		GreenBuildingCheckId = 0;
	}

	formData.append("GreenBuildingCheckId", GreenBuildingCheckId);
	formData.append("CreatedDateTime", CreatedDateTime);
	formData.append("listingId", listingId);
	formData.append("CertificationStatus", CertificationStatus);
	formData.append("CertifiedBy", CertifiedBy);
	formData.append("IntrestedToApply", IntrestedToApply);
	formData.append("CertificationNumber", CertificationNumber);
	formData.append("SF_GreenPolicy", SF_GreenPolicy);
	formData.append("SF_WasteCD", SF_WasteCD);
	formData.append("SF_Commuting", SF_Commuting);
	formData.append("SF_Landscaping", SF_Landscaping);
	formData.append("SF_NonRoof", SF_NonRoof);
	formData.append("SF_Roof", SF_Roof);
	formData.append("SF_PollutionReduction", SF_PollutionReduction);
	formData.append("SF_BuildingOM", SF_BuildingOM);
	formData.append("WE_Fixtures", WE_Fixtures);
	formData.append("WE_Harvesting", WE_Harvesting);
	formData.append("WE_Treatment", WE_Treatment);
	formData.append("WE_Reuse", WE_Reuse);
	formData.append("WE_Metering", WE_Metering);
	formData.append("WE_TurfArea", WE_TurfArea);
	formData.append("EE_Refrigerants", EE_Refrigerants);
	formData.append("EE_MinimumEP", EE_MinimumEP);
	formData.append("EE_ImprovedEP", EE_ImprovedEP);
	formData.append("EE_OnSiteRE", EE_OnSiteRE);
	formData.append("EE_OffSiteRE", EE_OffSiteRE);
	formData.append("EE_EnergyMetering", EE_EnergyMetering);
	formData.append("HC_SmokeControl", HC_SmokeControl);
	formData.append("HC_VEntilation", HC_VEntilation);
	formData.append("HC_CO2Control", HC_CO2Control);
	formData.append("HC_PollutionEquipment", HC_PollutionEquipment);
	formData.append("HC_EcoChemicals", HC_EcoChemicals);
	formData.append("HC_ThermalComfort", HC_ThermalComfort);
	formData.append("HC_AbledPeople", HC_AbledPeople);
	formData.append("HC_Facilities", HC_Facilities);
	formData.append("IC_Innovation", IC_Innovation);
	formData.append("IC_IGBC", IC_IGBC);
	formData.append("Status", Status);
	//console.log(formData);
	// Display the key/value pairs
	for (var pair of formData.entries()) {
		console.log(pair[0] + ', ' + pair[1]);
	}
	$.ajax({
		type: "POST",
		url: "/Addons/UploadGreenBuildingData",
		data: formData,
		processData: false,
		contentType: false,
		success: function (response) {
			if (response != null) {
				console.log(response);
				//$(row).addClass("addons-row");
				$('#GreenBuildingCheckId').html(response.greenBuildingCheckId);
				$('#CreatedDateTime').html(response.createdDateTime);
				$(row).removeClass("addons-row__edit");
				$('.gb-input__status, .gb-input__checklist, .gb-input__authority, .rating, .gb-input__apply').addClass("event-none");

				$(obj).siblings('.addon-edit').removeClass('display-none');
				$(obj).siblings('.addon-clear').addClass('display-none');
				$(obj).addClass('display-none');
			}
		},
		error: function (response) {
			alert("server not ready please upload afterwards");
		}
	});
}

//edit facility section
function EditGreenBuildingForm(obj) {
	var row = $(obj).closest('.modal-body');
	//$(row).removeClass("addons-row__light");
	$(row).addClass("addons-row__edit");
	$('.gb-input__status, .gb-input__checklist, .gb-input__authority, .rating, .gb-input__apply').removeClass("event-none");

	$(obj).siblings('.addon-add, .addon-clear').removeClass('display-none');
	$(obj).addClass('display-none');
}
//--------------------------------------------------------------------------------------------------//
//*****************************adding Green Building data section end*******************************//
//--------------------------------------------------------------------------------------------------//

//automatically re-prof added and maintain 
if ($('#listingTable').find('tr').hasClass('blink-bg') == true) {
	console.log('yes')
	$('.listingtable #show_add_view').addClass('event-none');
	$('.re-prof__nodata').removeClass('display-none');
}