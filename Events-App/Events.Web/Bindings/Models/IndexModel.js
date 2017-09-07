(function (ev) {
	let IndexModel = function (page, events) {

		let self = this;

		self.Events = ko.observableArray(events);
		self.Page = ko.observable(page);
		self.Date = ko.observable();
	};
	ev.IndexModel = IndexModel;
}(window.Events));
