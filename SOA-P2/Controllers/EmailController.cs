using System;
using Domain.Entities;
using Domain.Entities.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;

namespace SOA_P2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : Controller
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            _emailService.SendReminderEmails();
            return Ok("Ok");
        }
    }

}
