(function(ev){
  let LoginModel = function (){

    let self = this;

    self.LoginEmail = ko.observable().extend({
       required: { message: 'Login email is required' },
       validation: {
          validator: Events.mustMatch,
          message: Events.emailPatternMessage,
          params: Events.emailPattern
       }
    });
    self.Password = ko.observable().extend({
       required: { message: "Password is required" }
    });
    self.ConfirmPassword = ko.observable().extend({
       validation: {
          validator: Events.mustEqual,
          message: "Passwords do not match",
          params: self.Password
       }
    });
  };
  ev.LoginModel = LoginModel;
}(window.Events));
