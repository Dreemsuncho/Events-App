(function (ev) {
	let RegisterModelStep1 = function () {

		let self = this;

		self.FirstName = ko.observable().extend({
			required: { message: 'First name is required' }
		});
		self.LastName = ko.observable().extend({
			required: { message: 'Last name is required' }
		});
	};

	let RegisterModelStep2 = function () {

		let self = this;

		self.LoginEmail = ko.observable().extend({
			required: { message: 'Login email is required' },
			validation: { validator: Events.mustMatch, message: Events.emailPatternMessage, params: Events.emailPattern }
		});
		self.Password = ko.observable().extend({
			required: { message: "Password is required" }
		});
		self.ConfirmPassword = ko.observable().extend({
			validation: { validator: Events.mustEqual, message: "Passwords do not match", params: self.Password }
		});
		self.RememberMe = ko.observable(false);
	};

	ev.RegisterModelStep1 = RegisterModelStep1;
	ev.RegisterModelStep2 = RegisterModelStep2;
}(window.Events));
