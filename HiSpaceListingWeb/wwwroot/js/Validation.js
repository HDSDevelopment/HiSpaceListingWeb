
/*on focus and blur error on all section*/
function errShow(a, b, c) {
	if (a == '') {
		$("#" + b + "").html('<span>The field is required</span>');
	}
};

function errHide(a, b) {
	$("#" + b + "").html('');
};

function loginValidate(e) {
	var email = $('#Email').val();
	var password = $('#Password').val();
	if (email == "") {
		$('#error_Email').html('<span>Please Enter Email</span>');
	}
	if (password == "") {
		$('#error_Password').html('<span>Please Enter Password</span>');
	}
	if (email = "" || password == "") {
		event.preventDefault();
	}
}

function signupValidate(e) {
	var CompanyName = $('#CompanyName').val();
	var Phone = $('#Phone').val();
	var Email = $('#Email').val();
	var Password = $('#Password').val();
	if (CompanyName == "") {
		$('#error_CompanyName-s').html('<span>Please Enter Name</span>');
	}
	if (Phone == "") {
		$('#error_Phone-s').html('<span>Please Enter Number</span>');
	}
	if (Email == "") {
		$('#error_Email-s').html('<span>Please Enter Email</span>');
	}
	if (Password == "") {
		$('#error_Password-s').html('<span>Please Enter Password</span>');
	}
	if (CompanyName = "" || Phone == "" || Email == "" || Password == "") {
		event.preventDefault();
	}
}
function EditUserValidate(e) {
	var CompanyName = $('#CompanyName').val();
	var Phone = $('#Phone').val();
	var Email = $('#Email').val();
	var Address = $('#Address').val();
	var Postalcode = $('#Postalcode').val();
	var PrimaryContactName = $('#PrimaryContactName').val();
	var PrimaryContactPhone = $('#PrimaryContactPhone').val();
	var GSTIN = $('#GSTIN').val();
	var PAN = $('#PAN').val();
	var UAN = $('#UAN').val();

	if (CompanyName == "") {
		$('#error_CompanyName-e').html('<span>Please Enter Name</span>');
	}
	if (Phone == "") {
		$('#error_Phone-e').html('<span>Please Enter Number</span>');
	}
	if (Email == "") {
		$('#error_Email-e').html('<span>Please Enter Email</span>');
	}
	if (Address == "") {
		$('#error_Address-e').html('<span>Please Enter Address</span>');
	}
	if (Postalcode == "") {
		$('#error_Postalcode-e').html('<span>Please Enter Postalcode</span>');
	}
	if (PrimaryContactName == "") {
		$('#error_PrimaryContactName-e').html('<span>Add Contact Name</span>');
	}
	if (PrimaryContactPhone == "") {
		$('#error_PrimaryContactPhone-e').html('<span>Add Contact Number</span>');
	}
	if (GSTIN == "") {
		$('#error_GSTIN-e').html('<span>Please Enter GSTIN</span>');
	}
	if (PAN == "") {
		$('#error_PAN-e').html('<span>Please Enter PAN</span>');
	}
	if (UAN == "") {
		$('#error_UAN-e').html('<span>Please Enter UAN</span>');
	}


	if (CompanyName = "" || Phone == "" || Email == "" || Address == "" || Postalcode == "" || PrimaryContactName == "" || PrimaryContactPhone == "" || GSTIN == "" || PAN == "" || UAN == "") {
		event.preventDefault();
	}
}