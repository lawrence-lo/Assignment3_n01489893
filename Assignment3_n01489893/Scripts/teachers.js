// This file is connected to the project via Shared/_Layout.cshtml

// AJAX for AddTeacher
function AddTeacher(event) {

	// Perform client side validation
	validateForm();

	// Make an AJAX call if fields are valid
	if (validateForm() === true) {

		//goal: send a request which looks like this:
		//POST : http://localhost:51326/api/TeacherData/AddTeacher
		//with POST data of TeacherFname, TeacherLname, EmployeeNumber etc.

		var URL = "http://localhost:51326/api/TeacherData/AddTeacher/";

		var rq = new XMLHttpRequest();

		var TeacherFname = document.getElementById('TeacherFname').value;
		var TeacherLname = document.getElementById('TeacherLname').value;
		var EmployeeNumber = document.getElementById('EmployeeNumber').value;
		var TeacherHireDate = document.getElementById('TeacherHireDate').value;
		var TeacherSalary = document.getElementById('TeacherSalary').value;

		var TeacherData = {
			"TeacherFname": TeacherFname,
			"TeacherLname": TeacherLname,
			"EmployeeNumber": EmployeeNumber,
			"TeacherHireDate": TeacherHireDate,
			"TeacherSalary": TeacherSalary
		};

		rq.open("POST", URL, true);
		rq.setRequestHeader("Content-Type", "application/json");
		rq.onreadystatechange = function () {
			//ready state should be 4 AND status should be 200
			if (rq.readyState == 4 && rq.status == 200) {
				//request is successful and the request is finished

				//nothing to render, the method returns nothing.
			}
		}

		//POST information sent through the .send() method
		rq.send(JSON.stringify(TeacherData));

	} else {
		return false;
	};
};

// AJAX for DeleteTeacher
function DeleteTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:51326/api/TeacherData/DeleteTeacher/3
	//with POST data of the ID of the teacher

	//Get TeacherId from URL
	var TeacherId = window.location.pathname.split("/").pop();

	var URL = "http://localhost:51326/api/TeacherData/DeleteTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();

	rq.open("POST", URL, true);
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.
		}
	}

	//POST information sent through the .send() method
	rq.send();
};

// AJAX for UpdateTeacher
function UpdateTeacher(event) {

	// Perform client side validation
	validateForm();

	// Make an AJAX call if fields are valid
	if (validateForm() === true) {

		//goal: send a request which looks like this:
		//POST : http://localhost:51326/api/TeacherData/UpdateTeacher
		//with POST data of TeacherFname, TeacherLname, EmployeeNumber etc.

		//Get TeacherId from URL
		var TeacherId = window.location.pathname.split("/").pop();

		var URL = "http://localhost:51326/api/TeacherData/UpdateTeacher/" + TeacherId;

		var rq = new XMLHttpRequest();

		var TeacherFname = document.getElementById('TeacherFname').value;
		var TeacherLname = document.getElementById('TeacherLname').value;
		var EmployeeNumber = document.getElementById('EmployeeNumber').value;
		var TeacherHireDate = document.getElementById('TeacherHireDate').value;
		var TeacherSalary = document.getElementById('TeacherSalary').value;

		var TeacherData = {
			"TeacherFname": TeacherFname,
			"TeacherLname": TeacherLname,
			"EmployeeNumber": EmployeeNumber,
			"TeacherHireDate": TeacherHireDate,
			"TeacherSalary": TeacherSalary
		};

		rq.open("POST", URL, true);
		rq.setRequestHeader("Content-Type", "application/json");
		rq.onreadystatechange = function () {
			//ready state should be 4 AND status should be 200
			if (rq.readyState == 4 && rq.status == 200) {
				//request is successful and the request is finished

				//nothing to render, the method returns nothing.
			}
		}

		//POST information sent through the .send() method
		rq.send(JSON.stringify(TeacherData));

	} else {
		return false;
	};
};

// This function will change the backgorund color and set the field on focus if value is missing
// Returns false if any value is missing
function validateForm() {

	var formHandle = document.forms.addTeacherForm;
	var TeacherFname = formHandle.TeacherFname;
	var TeacherLname = formHandle.TeacherLname;
	var EmployeeNumber = formHandle.EmployeeNumber;
	var TeacherHireDate = formHandle.TeacherHireDate;
	var TeacherSalary = formHandle.TeacherSalary;

	// Reset background color
	TeacherFname.style.background = "none";
	TeacherLname.style.background = "none";
	EmployeeNumber.style.background = "none";
	TeacherHireDate.style.background = "none";
	TeacherSalary.style.background = "none";

	// Validate TeacherFname
	if (TeacherFname.value === "" || TeacherFname.value === null) {
		TeacherFname.style.background = "#58a4b0";
		TeacherFname.focus();
		return false;
	}

	// Validate TeacherLname
	if (TeacherLname.value === "" || TeacherLname.value === null) {
		TeacherLname.style.background = "#58a4b0";
		TeacherLname.focus();
		return false;
	}

	// Validate EmployeeNumber
	if (EmployeeNumber.value === "" || EmployeeNumber.value === null) {
		EmployeeNumber.style.background = "#58a4b0";
		EmployeeNumber.focus();
		return false;
	}

	// Validate TeacherHireDate
	if (TeacherHireDate.value === "" || TeacherHireDate.value === null) {
		TeacherHireDate.style.background = "#58a4b0";
		TeacherHireDate.focus();
		return false;
	}

	// Validate TeacherSalary
	if (TeacherSalary.value === "" || TeacherSalary.value === null) {
		TeacherSalary.style.background = "#58a4b0";
		TeacherSalary.focus();
		return false;
	}

	return true;
};