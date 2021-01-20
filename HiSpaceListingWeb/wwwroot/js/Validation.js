
/*on focus and blur error on all section*/
//function errShow(a, b, c) {
//	if (a == '') {
//		$("#" + b + "").html('<span>The field is required</span>');
//	}
//};

//function errHide(a, b) {
//	$("#" + b + "").html('');
//};

//function loginValidate(e) {
//	var email = $('#Email').val();
//	var password = $('#Password').val();
//	if (email == "") {
//		$('#error_Email').html('<span>Please Enter Email</span>');
//	}
//	if (password == "") {
//		$('#error_Password').html('<span>Please Enter Password</span>');
//	}
//	if (email = "" || password == "") {
//		event.preventDefault();
//	}
//}

//function signupValidate(e) {
//	var CompanyName = $('#CompanyName').val();
//	var Phone = $('#Phone').val();
//	var Email = $('#Email').val();
//	var Password = $('#Password').val();
//	if (CompanyName == "") {
//		$('#error_CompanyName-s').html('<span>Please Enter Name</span>');
//	}
//	if (Phone == "") {
//		$('#error_Phone-s').html('<span>Please Enter Number</span>');
//	}
//	if (Email == "") {
//		$('#error_Email-s').html('<span>Please Enter Email</span>');
//	}
//	if (Password == "") {
//		$('#error_Password-s').html('<span>Please Enter Password</span>');
//	}
//	if (CompanyName = "" || Phone == "" || Email == "" || Password == "") {
//		event.preventDefault();
//	}
//}
//function EditUserValidate(e) {
//	var CompanyName = $('#CompanyName').val();
//	var Phone = $('#Phone').val();
//	var Email = $('#Email').val();
//	var Address = $('#Address').val();
//	var Postalcode = $('#Postalcode').val();
//	var PrimaryContactName = $('#PrimaryContactName').val();
//	var PrimaryContactPhone = $('#PrimaryContactPhone').val();
//	var GSTIN = $('#GSTIN').val();
//	var PAN = $('#PAN').val();
//	var UAN = $('#UAN').val();

//	if (CompanyName == "") {
//		$('#error_CompanyName-e').html('<span>Please Enter Name</span>');
//	}
//	if (Phone == "") {
//		$('#error_Phone-e').html('<span>Please Enter Number</span>');
//	}
//	if (Email == "") {
//		$('#error_Email-e').html('<span>Please Enter Email</span>');
//	}
//	if (Address == "") {
//		$('#error_Address-e').html('<span>Please Enter Address</span>');
//	}
//	if (Postalcode == "") {
//		$('#error_Postalcode-e').html('<span>Please Enter Postalcode</span>');
//	}
//	if (PrimaryContactName == "") {
//		$('#error_PrimaryContactName-e').html('<span>Add Contact Name</span>');
//	}
//	if (PrimaryContactPhone == "") {
//		$('#error_PrimaryContactPhone-e').html('<span>Add Contact Number</span>');
//	}
//	if (GSTIN == "") {
//		$('#error_GSTIN-e').html('<span>Please Enter GSTIN</span>');
//	}
//	if (PAN == "") {
//		$('#error_PAN-e').html('<span>Please Enter PAN</span>');
//	}
//	if (UAN == "") {
//		$('#error_UAN-e').html('<span>Please Enter UAN</span>');
//	}


//	if (CompanyName = "" || Phone == "" || Email == "" || Address == "" || Postalcode == "" || PrimaryContactName == "" || PrimaryContactPhone == "" || GSTIN == "" || PAN == "" || UAN == "") {
//		event.preventDefault();
//	}
//}

//if (navigator.userAgent.indexOf("MSIE") == 1) {
//	alert("Please use IE");
//} else {
//	alert("Please use Chrome");
//}
//if (navigator.appName != "Microsoft Internet Explorer")
//{
//    alert("Please use IE");
//}
//var ua = window.navigator.userAgent;
//var isIE = /MSIE|Trident|Edge\//.test(ua);

//if (isIE) {
//	//IE specific code goes here
//	console.log('test')
//} else {
//	console.log('a')
//}


var regx = {
	"websiteRegx": { "rule": /^((ftp|http|https):\/\/)?(www.)?(?!.*(ftp|http|https|www.))[a-zA-Z0-9_-]+(\.[a-zA-Z]+)+((\/)[\w#]+)*(\/\w+\?[a-zA-Z0-9_]+=\w+(&[a-zA-Z0-9_]+=\w+)*)?$/, "error": "Invalid Website Url" },
	"emptyRegx": { "rule": /([^\s])/, "error": "The field is required" },
	"phoneRegx": { "rule": /^\d{10}$/, "error": "Invalid Phone no" },
	//"emailRegx": { "rule": /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/, "error": "Invalid Email" },
	"emailRegx": { "rule": /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w+)$/, "error": "Invalid Email" },
	//"passwordRegx": { "rule": /^[A-Za-z]\w{5,26}$/, "error": "Minimum password length is minmum 6" },
	"passwordRegx": { "rule": /[0-9a-zA-Z\d$@$!%*?&.]{6,}/, "error": "Minimum password length is minmum 6" },
	"checkboxRegx": { "rule": /true/, "error": "cannot be unchecked" },
	//"selectRegx": { "rule": /PleaseSelect/, "error": "cannot be unselect" },
	//"gstRegx": { "rule": /\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}/, "error": "Invalid GST" },
	"gstRegx": { "rule": /^([0][1-9]|[1-2][0-9]|[3][0-7])([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$/, "error": "Invalid GST No." },
	"panRegx": { "rule": /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/, "error": "Invalid PAN No." },
	"aadhaarRegx": { "rule": /^([0-9]){12}$/, "error": "Invalid Aadhaar No." },
	"postRegx": { "rule": /^[1-9][0-9]{5}$/, "error": "Invalid Postal No." },
	//"postRegx": { "rule": /^\d{6}$/, "error": "Invalid Postal No." },
	"numberRegx": { "rule": /^[+-]?\d+(\.\d+)?$/, "error": "Invalid No." },
	"googleCapcha": { "rule": /([^\s])/, "error": "You can't leave Captcha Code empty" }
}
function validate(formData, rules) {
	console.log(formData);
	$.each(rules, function (i, n) {
		var input = $(`#${formData.id} #${n.id}`);
		//var input = $("#"+formData.id+ "#"+n.id);
		console.log(input)
		console.log(input.prop("tagName"))
		if (input.prop("tagName") == "textarea") {
			var value = input.html();
			//console.log(value);
		}
		else if (input.prop("tagName") == "SELECT") {
			if (input.val() == "PleaseSelect") {
				var value = "";
			}
			else {
				var value = input.val();
			}
			//console.log(value);
		}
		else if (input.prop("tagName") == "DIV") {
			var captchaId = input.prop("id");
			if (captchaId == "html_element1") {
				var value = grecaptcha.getResponse(html_element1);
				//console.log(value);
			}
		}
		else {
			var value = input.val();
			//console.log(value);
		}

		$.each(n.validation, function (j, k) {
			//if (regx[k]["rule"] == "checkboxRegx") {
			if (n.validation[0] == "checkboxRegx") {
				value = input.prop("checked").toString();
				//console.log(value);
			}
			//console.log(n.validation[0]);
			//console.log(value);
			if (!value.match(regx[k]["rule"])) {
				input.siblings(".error").append(`<span> ${regx[k]["error"]}<br/></span>`);
			}
		})
	})

}

/*on focus and blur error on all section*/
//function errShow(a, b, c) {
//	if (a == '') {
//		$("#" + b + "").html('<span>The field is required</span>');
//	}
//};

//function errHide(a, b) {
//	$("#" + b + "").html('');
//};

//Login validation
function loginValidate(e) {
	let formData = { "id": "login form" };
	$(`#${formData.id} .error`).html(``);

	let rules = [
		{
			"id": "Email", "validation": ["emptyRegx"]
		},
		{ "id": "Password", "validation": ["emptyRegx"] },
	];

	validate(formData, rules);

	$(`#${formData.id} .error`).each(function (i) {
		if ($(this).is(':empty')) {
		}
		else {
			e.preventDefault();
			return;
		}
	})
}

//forgot password validation
function forgotValidate(e) {
	//e.preventDefault();
	let formData = { "id": "forgot-form" };
	$(`#${formData.id} .error`).html(``);

	let rules = [
		{ "id": "forgot-email", "validation": ["emptyRegx"] },
	];
	validate(formData, rules);

	$(`#${formData.id} .error`).each(function (i) {
		if ($(this).is(':empty')) {
			forgotPassword();
		}
		else {
			e.preventDefault();
			return;
		}
	})
}

//enquiryFormValidate
function enquiryFormValidate(e) {
	//e.preventDefault();
	let formData = { "id": "enquiryForm" };
	$(`#${formData.id} .error`).html(``);

	let rules = [{ "id": "Sender_Name", "validation": ["emptyRegx"] },
	{ "id": "Sender_Phone", "validation": ["emptyRegx"] },
	{ "id": "Sender_Email", "validation": ["emptyRegx"] },
	{ "id": "Sender_Message", "validation": ["emptyRegx"] },
	];

	validate(formData, rules);

	$(`#${formData.id} .error`).each(function (i) {
		if ($(this).is(':empty')) {
		}
		else {
			e.preventDefault();
			return;
		}
	})
}

//image upload validation
//function ImageUploadValidate(e) {
//	let formData = { "id": "login form" };
//}

//Signup validation
function signupValidate(e) {

	let formData = { "id": "signup form" };
	$(`#${formData.id} .error`).html(``);
	let rules;

	if ($('#sign-Address').is(':visible')) {
		rules = [
			{ "id": "sign-CompanyName", "validation": ["emptyRegx"] },
			{ "id": "sign-Phone", "validation": ["emptyRegx", "phoneRegx"] },
			{ "id": "sign-Email", "validation": ["emptyRegx"] },
			{ "id": "sign-Password", "validation": ["emptyRegx", "passwordRegx"] },
			{ "id": "sign-Address", "validation": ["emptyRegx"] },
			{ "id": "sign-Postalcode", "validation": ["emptyRegx", "postRegx"] },
			{ "id": "TermsAndConditions", "validation": ["checkboxRegx"] },
		];
	} else {
		rules = [
			{ "id": "sign-CompanyName", "validation": ["emptyRegx"] },
			{ "id": "sign-Phone", "validation": ["emptyRegx", "phoneRegx"] },
			{ "id": "sign-Email", "validation": ["emptyRegx"] },
			{ "id": "sign-Password", "validation": ["emptyRegx", "passwordRegx"] },
			{ "id": "TermsAndConditions", "validation": ["checkboxRegx"] },
		];
	}


	//console.log(rules)
	var User_Website = "sign-Website";
	if ($(`#${User_Website}`).val() && $(`#${User_Website}`).val() != "") {
		rules.push({ "id": "sign-Website", "validation": ["websiteRegx"] });
	}
	validate(formData, rules);

	$(`#${formData.id} .error`).each(function (i) {
		if ($(this).is(':empty')) {
		}
		else {
			e.preventDefault();
			return;
		}
	})
}

//validation for editing the user data
function EditUserValidate(e) {
	//console.log($("#User_ProofName").find(':selected'));
	//console.log($("#User_ProofName").val());
	let formData = { "id": "editUserData" };
	$(`#${formData.id} .error`).html(``);
	let rules = [
		{ "id": "CompanyName", "validation": ["emptyRegx"] },
		{ "id": "Phone", "validation": ["emptyRegx", "phoneRegx"] },
		{ "id": "Email", "validation": ["emptyRegx"] },
		{ "id": "Address", "validation": ["emptyRegx"] },
		{ "id": "Postalcode", "validation": ["emptyRegx", "postRegx"] },
		{ "id": "TermsAndConditions", "validation": ["checkboxRegx"] },
		//{ "id": "ProofNumber", "validation": ["emptyRegx"] },
		//{ "id": "ProofName", "validation": ["emptyRegx"] },
	];
	var User_Website = "Website";
	//e.preventDefault();
	if ($(`#${User_Website}`).val() && $(`#${User_Website}`).val() != "") {
		rules.push({
			"id": "Website", "validation": ["websiteRegx"]
		});
	}
	validate(formData, rules);

	$(`#${formData.id} .error`).each(function (i) {
		if ($(this).is(':empty')) {

		}
		else {
			e.preventDefault();
			return;
		}
	})
}

//validation for admin creating the user data
function adminAddUserValidate(e) {
	//console.log($("#User_ProofName").find(':selected'));
	//console.log($("#User_ProofName").val());
	let formData = { "id": "adminAddUser" };
	$(`#${formData.id} .error`).html(``);
	let rules = [
		{ "id": "CompanyName", "validation": ["emptyRegx"] },
		{ "id": "Phone", "validation": ["emptyRegx", "phoneRegx"] },
		{ "id": "Email", "validation": ["emptyRegx"] },
		{ "id": "Password", "validation": ["emptyRegx", "passwordRegx"] },
		{ "id": "Address", "validation": ["emptyRegx"] },
		{ "id": "Postalcode", "validation": ["emptyRegx", "postRegx"] },
		{ "id": "TermsAndConditions", "validation": ["checkboxRegx"] },
	];
	var User_Website = "Website";
	if ($(`#${User_Website}`).val() && $(`#${User_Website}`).val() != "") {
		rules.push({
			"id": "Website", "validation": ["websiteRegx"]
		});
	}
	validate(formData, rules);

	$(`#${formData.id} .error`).each(function (i) {
		if ($(this).is(':empty')) {

		}
		else {
			e.preventDefault();
			return;
		}
	})
}

//validation for investor form
function investorCreateValidate(e) {
	let formData = { "id": "investorCreate" };
	$(`#${formData.id} .error`).html(``);
	let rules = [
		{ "id": "FirstName", "validation": ["emptyRegx"] },
		{ "id": "LastName", "validation": ["emptyRegx"] },
		{ "id": "Phone", "validation": ["emptyRegx", "phoneRegx"] },
		{ "id": "Email", "validation": ["emptyRegx"] },
		{ "id": "InvestmentType", "validation": ["emptyRegx"] },
		{ "id": "PropertyType", "validation": ["emptyRegx"] },
		{ "id": "Currency", "validation": ["emptyRegx"] },
		//{
		//	"id": "MinRange", "validation": ["emptyRegx", "numberRegx"] },
		//{
		//	"id": "MaxRange", "validation": ["emptyRegx", "numberRegx"] },
		{ "id": "During", "validation": ["emptyRegx"] },
		{ "id": "countryId", "validation": ["emptyRegx"] },
		{ "id": "stateId", "validation": ["emptyRegx"] },
		{ "id": "cityId", "validation": ["emptyRegx"] },
		{ "id": "Neighborhood", "validation": ["emptyRegx"] },
		{ "id": "Comment", "validation": ["emptyRegx"] },
	];
	validate(formData, rules);

	var appended = false;
	$(`#${formData.id} .error`).each(function (i) {
		if ($(this).is(':empty')) {
			if (!appended) {
				$("body").append("<div class='loader_new'></div>");
				appended = true;
			}
		}
		else {
			e.preventDefault();
			return;
		}
	})
}

//Basic information validation on user
function basicInformationValidate(e) {
	let formData = { "id": "property-lister form" };
	$(`#${formData.id} .error`).html(``);
	let rules = [
		{ "id": "User_CompanyName", "validation": ["emptyRegx"] },
		{ "id": "User_Phone", "validation": ["emptyRegx", "phoneRegx"] },
		{ "id": "User_Email", "validation": ["emptyRegx"] },
		{ "id": "User_TermsAndConditions", "validation": ["checkboxRegx"] }
	];
	var User_Website = "User_Website";
	var User_Postalcode = "User_Postalcode";
	if ($(`#${User_Website}`).val() && $(`#${User_Website}`).val() != "") {
		rules.push({ "id": "User_Website", "validation": ["websiteRegx"] });
	}
	if ($(`#${User_Postalcode}`).val() && $(`#${User_Postalcode}`).val() != "") {
		rules.push({ "id": "User_Postalcode", "validation": ["postRegx"] });
	}

	validate(formData, rules);
	$(`#${formData.id} .error`).each(function (i) {

		if ($(this).is(':empty')) {

		}
		else {
			e.preventDefault();
			return;
		}


	})
}

//image validation
$(document).on("change", "input[type='file']", function (e) {
	console.log(this.files[0]);
	if (this.files[0].size > 3250585) {
		alert("File is too big! max-size 3mb limit");
		this.value = "";
	};
})

//image validation
$(document).on("focus", "input", function (e) {
	var input = $(this);
	input.siblings(".error").html(``);
})
$(document).on("change", "select", function (e) {
	var input = $(this);
	input.siblings(".error").html(``);
})










