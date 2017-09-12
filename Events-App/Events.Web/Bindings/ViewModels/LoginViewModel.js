(function (ev) {
   let LoginViewModel = function (returnUrl) {

      let self = this;

      self.viewModelHelper = new Events.viewModelHelper();
      self.viewModel = new Events.LoginModel();

      self.login = function (model) {
         let errors = ko.validation.group(model);
         let isValid = errors().length === 0;

         if (isValid) {
            let unmappedModel = ko.toJS(model);
            $.extend(unmappedModel, { returnUrl: returnUrl });

            self.viewModelHelper.POST('api/account/login', unmappedModel, function (result) {
               let url = Events.rootPath + (result.returnUrl || '/').substr(1);
               window.location.href = url + '??Welcome ' + result.loginEmail + '!';
             });
         } else {
            self.viewModelHelper.showErrors(errors());
         }
      };
   };
   ev.LoginViewModel = LoginViewModel;
}(window.Events))
