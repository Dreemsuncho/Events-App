(function (ev) {
   let LoginViewModel = function () {

      let self = this;

      self.viewModelHelper = new Events.viewModelHelper();
      self.viewModel = new Events.LoginModel();

      self.login = function (model) {
         let errors = ko.validation.group(model);
         let isValid = errors().length === 0;

         if (isValid) {
            let unmappedModel = ko.toJS(model);

            self.viewModelHelper.POST('api/account/login', unmappedModel, function () {
               window.location.href = Events.rootPath;
            });
         } else {
            self.viewModelHelper.showErrors(errors());
         }
      };
   };
   ev.LoginViewModel = LoginViewModel;
}(window.Events))
