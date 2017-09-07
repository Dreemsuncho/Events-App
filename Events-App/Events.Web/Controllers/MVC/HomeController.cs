using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Events.Data.Contracts;
using Events.Data.Repositories;
using Events.Web.Core;
using Events.Web.Models;
using System.Threading.Tasks;

namespace Events.Web.Controllers
{
    public class HomeController : CustomControllerBase
    {
        private readonly IDataRepositoryFactory _dataRepositoryFactory;

        public HomeController(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [HttpGet]
        public ActionResult Index(DateTime? date, int page = 0)
        {
            var eventsRepository = _dataRepositoryFactory.GetRepository<EventsRepository>();
            
            var events = eventsRepository.Get(e => e.IsPublic && e.StartDate >= (date ?? new DateTime(1970, 01, 01)), page, _pageSize);

            var model = new IndexModel
            {
                Page = page,
                Events = events
            };

            return View(model);
        }
    }
}
