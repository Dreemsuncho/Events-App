using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

using Events.Web.Core;
using Events.Data.Contracts;
using Events.Data.Entities;
using Events.Web.Models;

namespace Events.Web.Controllers
{
    [Route("api/customer")]
    public class CustomerApiController : CustomControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("comments/create")]
        public ActionResult CreateComment(CommentModel model)
        {
            ActionResult response = null;

            var accountRepository = _unitOfWork.AccountRepository;
            var eventsRepository = _unitOfWork.EventsRepository;

            var userName = User.Identity.Name;
            var author = accountRepository.GetByLogin(userName);

            var comment = new Comment { Author = author, Text = model.Text };

            var ev = eventsRepository.Get(model.EventId);
            ev.Comments.Add(comment);

            var errors = new List<string>();
            try
            {
                if (_unitOfWork.Commit() > 0)
                    response = StatusCode((int)HttpStatusCode.Created, comment);
                else
                    errors.Add("Cannot add comment to event.");
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            if (errors.Count > 0)
                response = StatusCode((int)HttpStatusCode.BadRequest, errors);

            return response;
        }
    }
}
