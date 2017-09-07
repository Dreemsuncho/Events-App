
(function (ev) {
   let IndexViewModel = function (page, events) {
      function apiIndexGet(date, page) {
        let d = new Date(date || 0).toLocaleDateString();
         let urlAsString = 'api/home/index?date=' + d + '&page=' + page;

         self.viewModelHelper.GET(urlAsString, null, function (result) {
            self.model.Events(result);
            self.isVisible(visible());
            self.model.Page(page);
            window.history.pushState(null, null, '?date=' + d + '&page='+ page);
         });
      };


      let self = this;
      self.viewModelHelper = new Events.viewModelHelper();
      self.model = new Events.IndexModel(page, events);

      let visible = _ => self.model.Events().length > 0;
      self.isVisible = ko.observable(visible());

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
