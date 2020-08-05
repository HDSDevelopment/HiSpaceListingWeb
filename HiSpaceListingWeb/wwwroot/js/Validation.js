
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



var regx = {
	"websiteRegx": { "rule": /^((ftp|http|https):\/\/)?(www.)?(?!.*(ftp|http|https|www.))[a-zA-Z0-9_-]+(\.[a-zA-Z]+)+((\/)[\w#]+)*(\/\w+\?[a-zA-Z0-9_]+=\w+(&[a-zA-Z0-9_]+=\w+)*)?$/, "error": "Invalid Website Url" },
	"emptyRegx": { "rule": /([^\s])/, "error": "The field is required" },
	"phoneRegx": { "rule": /^\d{10}$/, "error": "Invalid Phone no" },
	"emailRegx": { "rule": /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/, "error": "Invalid Email" },
	//"passwordRegx": { "rule": /^[A-Za-z]\w{5,26}$/, "error": "Minimum password length is minmum 6" },
	"passwordRegx": { "rule": /[0-9a-zA-Z]{6,}/, "error": "Minimum password length is minmum 6" },
	"checkboxRegx": { "rule": /true/, "error": "cannot be unchecked" },
	//"gstRegx": { "rule": /\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}/, "error": "Invalid GST" },
	"gstRegx": { "rule": /^([0][1-9]|[1-2][0-9]|[3][0-7])([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$/, "error": "Invalid GST No." },
	"panRegx": { "rule": /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/, "error": "Invalid PAN No." },
	"aadhaarRegx": { "rule": /^([0-9]){12}$/, "error": "Invalid Aadhaar No." },
	"postRegx": { "rule": /^[1-9][0-9]{5}$/, "error": "Invalid Postal No." },
	//"postRegx": { "rule": /^\d{6}$/, "error": "Invalid Postal No." },
	"numberRegx": { "rule": /^[+-]?\d+(\.\d+)?$/, "error": "Invalid No." }	
}
function validate(formData, rules) {
	//console.log(formData);
	$.each(rules, function (i, n) {
		var input = $(`#${formData.id} #${n.id}`);
		//console.log(input)
		if (input.prop("tagName") == "textarea") {
			var value = input.html();
			//console.log(value);
		} else {
			var value = input.val();
			//console.log(value);
		}
		
		$.each(n.validation, function (j, k) {
			//if (regx[k]["rule"] == "checkboxRegx") {
			if (n.validation[0] == "checkboxRegx") {
				value = input.prop("checked").toString();
				console.log(value);
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
	let formData = { "id": "login form"};
	$(`#${formData.id} .error`).html(``);

	let rules = [
		{
			"id": "Email", "validation": ["emptyRegx", "emailRegx"]},
		{ "id": "Password", "validation": ["emptyRegx"]},
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

	let rules = [
		{ "id": "sign-CompanyName", "validation": ["emptyRegx"] },
		{ "id": "sign-Phone", "validation": ["emptyRegx", "phoneRegx"] },
		{
			"id": "sign-Email", "validation": ["emptyRegx", "emailRegx"] },
		{ "id": "sign-Password", "validation": ["emptyRegx", "passwordRegx"] },
		//{ "id": "sign-Website", "validation": ["emptyRegx"] },
		//{ "id": "sign-Postalcode", "validation": ["emptyRegx"] },

	];
	//console.log(rules)
	var User_Website = "sign-Website";
	var User_Postalcode = "sign-Postalcode";
	if ($(`#${User_Website}`).val() && $(`#${User_Website}`).val() != "") {
		rules.push({ "id": "sign-Website", "validation": ["websiteRegx"] });
	}
	if ($(`#${User_Postalcode}`).val() && $(`#${User_Postalcode}`).val() != "") {
		rules.push({ "id": "sign-Postalcode", "validation": ["postRegx"] });
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
	let formData = { "id": "editUserData" };
	$(`#${formData.id} .error`).html(``);
	let rules = [
		{ "id": "CompanyName", "validation": ["emptyRegx"] },
		{ "id": "Phone", "validation": ["emptyRegx", "phoneRegx"] },
		{ "id": "Email", "validation": ["emptyRegx", "emailRegx"] },
		//{ "id": "Website", "validation": ["emptyRegx"] },
		{ "id": "Address", "validation": ["emptyRegx"] },
		{ "id": "Postalcode", "validation": ["emptyRegx", "postRegx"] },
		{ "id": "PrimaryContactName", "validation": ["emptyRegx"] },
		{ "id": "PrimaryContactPhone", "validation": ["emptyRegx", "phoneRegx"] },
		{ "id": "GSTIN", "validation": ["emptyRegx", "gstRegx"] },
		{ "id": "PAN", "validation": ["emptyRegx", "panRegx"] },
		{ "id": "UAN", "validation": ["emptyRegx", "aadhaarRegx"] },
		{ "id": "TermsAndConditions", "validation": ["checkboxRegx"] }
	];
	if ($("#Password").length) {
		rules.push({
			"id": "Password", "validation": ["emptyRegx","passwordRegx"] });
	} else {

	}
	var User_Website = "Website";
	if ($(`#${User_Website}`).val() && $(`#${User_Website}`).val() != "") {
		rules.push({
			"id": "Website", "validation": ["websiteRegx"] });
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

//Basic information validation on user
function basicInformationValidate(e) {
	let formData = { "id": "property-lister form" };
	$(`#${formData.id} .error`).html(``);
	let rules = [
		{ "id": "User_CompanyName", "validation": ["emptyRegx"] },
		{ "id": "User_Phone", "validation": ["emptyRegx", "phoneRegx"] },
		{ "id": "User_Email", "validation": ["emptyRegx", "emailRegx"] },
		{ "id": "User_TermsAndConditions", "validation": ["checkboxRegx"] }
	];
	var User_Website = "User_Website";
	var User_Postalcode = "User_Postalcode";
	var User_PrimaryContactPhone = "User_PrimaryContactPhone";
	var User_GSTIN = "User_GSTIN";
	var User_PAN = "User_PAN";
	if ($(`#${User_Website}`).val() && $(`#${User_Website}`).val() != "") {
		rules.push({ "id": "User_Website", "validation": ["websiteRegx"] });
	}
	if ($(`#${User_Postalcode}`).val() && $(`#${User_Postalcode}`).val()) {
		rules.push({ "id": "User_Postalcode", "validation": ["postRegx"] });
	}
	if ($(`#${User_PrimaryContactPhone}`).val() && $(`#${User_PrimaryContactPhone}`).val()) {
		rules.push({ "id": "User_PrimaryContactPhone", "validation": ["phoneRegx"] });
	}
	if ($(`#${User_GSTIN}`).val() && $(`#${User_GSTIN}`).val()) {
		rules.push({ "id": "User_GSTIN", "validation": ["gstRegx"] });
	}
	if ($(`#${User_PAN}`).val() && $(`#${User_PAN}`).val() != "") {
		rules.push({ "id": "User_PAN", "validation": ["panRegx"] });
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










