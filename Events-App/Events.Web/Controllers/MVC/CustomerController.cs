using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

using Events.Web.Core;
using Events.Data.Contracts;
using Events.Data.Entities;


namespace Events.Web.Controllers
{
    [Route("api/customer")]
    public class CustomerController : CustomControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("comments/create")]
        public ActionResult CreateComment(CommentModel model)
        {
            ActionResult response = null;

            var errors = new List<string>();

            var accountRepository = _unitOfWork.AccountRepository;

            var userName = User.Identity.Name;

            Account author = null;

            if (userName != null)
                author = accountRepository.GetByLogin(userName);

            var eventsRepository = _unitOfWork.EventsRepository;

            var ev = eventsRepository.Get(model.EventId);

            var comment = new Comment { Author = author, Text = model.Text };

            ev.Comments.Add(comment);

            try
            {
                if (_unitOfWork.Commit() > 0)
                {
                    response = StatusCode((int)HttpStatusCode.Created, comment);
                }
                else
                {
                    errors.Add("Cannot add comment to event.");
                    response = StatusCode((int)HttpStatusCode.BadRequest, errors);
                }

            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                response = StatusCode((int)HttpStatusCode.BadRequest, errors);
            }

            return response;
        }
    }

    public class CommentModel
    {
        public string Text { get; set; }
        public Guid EventId { get; set; }
    }
}
