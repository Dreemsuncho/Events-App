
(function (ev) {
   let ChangePasswordViewModel = function () {

      let self = this;

      self.viewModelHelper = new Events.viewModelHelper();
      self.model = new Events.ChangePasswordModel();

      self.changePassword = function (model) {
         let errors = ko.validation.group(model);
         let isValid = errors().length === 0;

         if (isValid) {
            let unmappedModel = ko.toJS(model);

            self.viewModelHelper.POST("api/account/chpassword", unmappedModel, function () {
               window.location.href = Events.rootPath + '??Success change your password';
            });
         } else {
            self.viewModelHelper.showErrors(errors());
         }
      };
   };
   ev.ChangePasswordViewModel = ChangePasswordViewModel;
}(window.Events));
