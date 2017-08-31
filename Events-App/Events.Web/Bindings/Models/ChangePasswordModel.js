(function (ev) {
	let ChangePasswordModel = function () {

		let self = this;

		self.OldPassword = ko.observable().extend({
			required: { message: "Old password is required" }
		});
		self.NewPassword = ko.observable().extend({
			required: { message: "New password is required" }
		});
		self.ConfirmPassword = ko.observable().extend({
			validation: { validator: Events.mustEqual, message: "Passwords do not match", params: self.NewPassword }
		});
	};
	ev.ChangePasswordModel = ChangePasswordModel;
}(window.Events));