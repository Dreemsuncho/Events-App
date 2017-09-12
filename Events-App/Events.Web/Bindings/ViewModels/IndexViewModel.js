
(function (ev) {
   let IndexViewModel = function (page, events) {

      let defineFlagObservables = function (array) {
         array.forEach(e => {
            e.detailsIsVisible = ko.observable(false);
            e.commentFormVisible = ko.observable(false);
         });
      };
      defineFlagObservables(events);

      function apiIndexGet(date, page) {
         let d = new Date(date || 0).toLocaleDateString();
         let urlAsString = 'api/home/index?date=' + d + '&page=' + page;

         self.viewModelHelper.GET(urlAsString, null, function (result) {
            defineFlagObservables(result);

            self.model.Events(result);
            self.isVisible(visible());
            self.model.Page(page);
            window.history.pushState(null, null, '?date=' + d + '&page=' + page);
         });
      };


      let self = this;

      self.viewModelHelper = new Events.viewModelHelper();
      self.model = new Events.IndexModel(page, events);
      self.eventDetails = new Events.EventDetailsViewModel();

      let visible = _ => self.model.Events().length > 0;
      self.isVisible = ko.observable(visible());

      self.toggleDetails = function (result) {
         result.detailsIsVisible(!result.detailsIsVisible());
      };

      self.toggleCommentForm = function (result) {
         self.eventDetails.commentText('');
         result.commentFormVisible(!result.commentFormVisible());
      };


      self.prevPage = function () {
         apiIndexGet(self.model.Date(), --page);
      };

      self.nextPage = function () {
         apiIndexGet(self.model.Date(), ++page);
      };


      $('.datepicker').bootstrapMaterialDatePicker({ weekStart: 0, time: false, clearButton: true }).on('change', function (e, d) {
         let date = new Date(d || 0);
         apiIndexGet(date, page);
      });
   };
   ev.IndexViewModel = IndexViewModel;
}(window.Events));

(function (ev) {
   let EventDetailsViewModel = function () {

      let self = this;

      self.viewModelHelper = new Events.viewModelHelper();
      self.commentText = ko.observable();

      self.createComment = function (model) {
         model.commentFormVisible(false);
         let apiModel = { text: self.commentText(), eventId: model.id };
         self.commentText('');

         self.viewModelHelper.POST('api/customer/comments/create', apiModel, function (result) {
            let listItem = getListItem(result);
            $('#' + model.id).append(listItem);
         });
      }

      function getListItem(result) {
         let author = (result.author ? result.author.firstName : 'Anonymous') + ' says:';
         let text = result.text;
         let date = 'Published: ' + new Date(result.date).toLocaleDateString();

         return `<li>
                    <strong>${ author }</strong>
                    <i class="light-blue">${ text }</i>
                    <p>${ date }</p>
                 </li>`;
      };
   };
   ev.EventDetailsViewModel = EventDetailsViewModel;
}(window.Events))
