
(function (e) {
   let RegisterViewModel = function () {

      let self = this;

      self.viewModelHelper = new Events.viewModelHelper();

      let initialState = "step1";
      self.viewMode = ko.observable(initialState); // step1, step2, confirm
      self.step1 = ko.observable(new Events.RegisterModelStep1());
      self.step2 = ko.observable(new Events.RegisterModelStep2());

      self.prevStep = function () {
         if (self.viewMode() === 'step2')
            self.viewMode('step1');
         else
            self.viewMode('step2');
      };

      self.nextStep = function (model) {
         let errors = ko.validation.group(model);
         let isValid = errors().length === 0;

         if (isValid) {
            let unmappedModel = ko.toJS(model);

            if (self.viewMode() === 'step1') {
               self.viewModelHelper.POST('api/account/register/validate1', unmappedModel, function (result) {
                  self.viewMode('step2');
               });
            } else {
               self.viewModelHelper.POST('api/account/register/validate2', unmappedModel, function (result) {
                  self.viewMode('confirm')
               });
            }
         } else {
            self.viewModelHelper.showErrors(errors());
         }
      };

      self.register = function () {
         let unmappedModel;
         unmappedModel = $.extend(unmappedModel, ko.toJS(self.step1))
         unmappedModel = $.extend(unmappedModel, ko.toJS(self.step2))

         self.viewModelHelper.POST('api/account/register', unmappedModel, function (result) {
            window.location.href = Events.rootPath + `??Welcome ${result}!`;
         });
      };

      // push state when initially get the page
      pushState();
      self.viewMode.subscribe(function () {
         pushState();
      });

      window.onpopstate = function (arg) {
         if (arg.state) {
            self.viewModelHelper.statePopped = true;
            self.viewMode(arg.state);
         }
      };

      function pushState() {
         self.viewModelHelper.pushUrlState(self.viewMode(), 'account/register');
         initialState = self.viewModelHelper.handleUrlState(initialState);
      };
   };
   e.RegisterViewModel = RegisterViewModel;
}(window.Events))
