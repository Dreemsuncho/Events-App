(function (ev) {
	let LoginModel = function () {

		let self = this;

		self.LoginEmail = ko.observable().extend({
			required: { message: 'Login email is required' },
			validation: { validator: Events.mustMatch, message: Events.emailPatternMessage, params: Events.emailPattern }
		});
		self.Password = ko.observable().extend({
			required: { message: "Password is required" }
		});
		self.RememberMe = ko.observable(false);
	};
	ev.LoginModel = LoginModel;
}(window.Events));
