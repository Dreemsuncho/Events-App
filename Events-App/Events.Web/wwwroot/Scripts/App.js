window.Events = {};

(function (ev) {
   let z = 6;
   let refreshDraggable = function () {
      $('.dtp-content').addClass('draggable');
      $('.draggable').mousedown(function (ev) { $(this).css('z-index', z++); });
      $('.draggable').draggable();
   };
   ev.refreshDragabble = refreshDraggable;
}(window.Events));

(function (ev) {
   ev.rootPath = '';
   ev.infoMessage = '';
   ev.emailPattern = /[a-zA-Z]+@[a-zA-Z]+\.[a-zA-Z]+/;
   ev.emailPatternMessage = 'Login email is invalid, must match (a-z)@(a-z).(a-z)';
   ev.mustEqual = (val, other) => val === other;
   ev.mustMatch = (val, regex) => regex.test(val);
}(window.Events));

(function (ev) {
   ev.viewModelHelper = function () {

      let self = this;

      self.isLoading = ko.observable(false);
      self.showErrors = function (errors) {
         if (errors)
            errors.forEach(e => toastr.error(e));
      };

      self.isValid = ko.observable(true);
      self.errors = ko.observableArray();

      self.GET = function (url, data, success, failure) {
         self.isLoading(true);

         $.get(Events.rootPath + url, data, success)
            .fail(function (result) {
               self.showErrors(result.responseJSON);
            })
            .always(function () {
               self.isLoading(false);
               Events.refreshDragabble();
            });
      };

      self.POST = function (url, data, success, failure) {
         self.isLoading(true);

         $.post(Events.rootPath + url, data, success)
            .fail(function (result) {
               self.showErrors(result.responseJSON);
            })
            .always(function () {
               self.isLoading(false);
               Events.refreshDragabble();
            });
      };

      let stateInfo = {};
      self.pushUrlState = function (code, url) {
         stateInfo = { Code: code, Url: Events.rootPath + url };
      };

      self.statePopped = false;
      self.handleUrlState = function (initialState) {
         if (!self.statePopped) {
            if (initialState) {
               history.replaceState(stateInfo.Code, null, stateInfo.Url);
               initialState = false;
            } else {
               history.pushState(stateInfo.Code, null, stateInfo.Url);
            }
         } else {
            self.statePopped = false;
         }

         return initialState;
      };
   }
}(window.Events));

ko.bindingHandlers.loadingWhen = {
   init: function (element) {
      // cache a reference to the element as we use it multiple times below
      let $element = $(element);

      // get the current value of the css 'position' property
      let elementPosition = $element.css('position');

      // create the new div with the 'loader' class and hide it
      let $loader = $('<div>').addClass('loader').hide();

      // add the loader to the original element
      $element.append($loader);

      // make sure that we can absolutely position the loader against the original element
      if (elementPosition === 'auto' || elementPosition === 'static')
         $element.css('position', 'relative');

      // center the loader
      $loader.css({
         position: 'absolute',
         top: '50%',
         left: '50%',
         'margin-top': -($element.height() / 2) + 'px',
         'margin-left': -($loader.width() / 2) + 'px'
      });
   },
   update: function (element, valueAccessor) {
      // unwrap the value of the flag using knockout utils
      let isLoading = ko.utils.unwrapObservable(valueAccessor());

      // get reference to the parent element
      let $element = $(element);

      // get reference to the loader
      let $loader = $element.find('div.loader');
      // get reference to every other element in the parent
      let $childrenToHide = $element.find(':not(div.loader)');

      // if we are currently loading
      if (isLoading) {
         // hide and disable the children
         $childrenToHide.css('visibility', 'hidden').attr('disabled', 'disabled');
         // show the loader
         $loader.fadeIn();
      } else {
         // otherwise fad out the loader
         $loader.fadeOut('fast');
         // and enable the children
         $childrenToHide.css('visibility', 'visible').removeAttr('disabled');
      }
   }
}

// Here's a custom Knockout binding that makes elements shown/hidden via jQuery's fadeIn()/fadeOut() methods
// Could be stored in a separate utility library
ko.bindingHandlers.fadeVisible = {
   init: function (element, valueAccessor) {
      // Initially set the element to be instantly visible/hidden depending on the value
      var value = ko.utils.unwrapObservable(valueAccessor());
      // Use "unwrapObservable" so we can handle values that may or may not be observable
      $(element).toggle(ko.unwrap(value));
   },
   update: function (element, valueAccessor) {
      // Whenever the value subsequently changes, slowly fade the element in or out
      var value = valueAccessor();
      ko.unwrap(value) ? $(element).fadeIn('slow') : $(element).hide();
   }
};

// this code is executed when must show success message after redirect.
$(function () {
   let params = window.location.href.split('??');

   if (params.length > 1) {
      // then clear the url for prevent another refresh and shows message again.
      toastr.options.onHidden = function () { history.pushState(null, null, params[0]); };
      toastr.success(params[1].replace(/%20/g, " "));
   };
});
