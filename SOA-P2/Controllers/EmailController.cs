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

            // Enviar correo electrónico
            var subject = "Activo próximo a entregar";
            var htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./../../../Reminder.html");
            var htmlBody = System.IO.File.ReadAllText(htmlFilePath);

            var recipient = "diegogutcat28@gmail.com";
            _emailService.SendSmtpEmail(recipient, subject, htmlBody);
            

            return Ok("Ok");
        }
    }

}
