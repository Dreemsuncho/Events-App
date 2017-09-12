using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Events.Data.Contracts;
using Events.Data.Repositories;
using System.Linq.Expressions;
using Events.Data.Entities;

namespace Events.Web.Controllers
{
    [Route("api")]
    public class HomeApiController : CustomControllerBase
    {
        private readonly IDataRepositoryFactory _dataRepositoryFactory;

        public HomeApiController(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [HttpGet("home/index")]
        public ActionResult Index(DateTime date, int page)
        {
            ActionResult response = null;

            var errors = new List<string>();

            var eventsRepository = _dataRepositoryFactory.GetRepository<EventsRepository>();

            var events = eventsRepository.GetAllWithComments(e => e.IsPublic && e.StartDate >= date, page, _pageSize);

            if (events == null)
                errors.Add("Cannot evaluate this operation");

            if (!errors.Any())
                response = Ok(events);
            else
                response = NotFound(errors);

            return response;
        }
    }
}
