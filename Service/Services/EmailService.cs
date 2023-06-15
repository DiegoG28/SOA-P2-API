using System;
using Microsoft.Extensions.Configuration;
using Service.IServices;
using System.Net;
using System.Net.Mail;
using System.Text;
using Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmailService> _logger;
        string htmlTemplate = "<!DOCTYPE html>\n<html>\n<head>\n    <title>Bienvenidos al equipo</title>\n    <style>\n        body {\n            background-color: #f2f2f2;\n            font-family: Arial, sans-serif;\n            text-align: center;\n        }\n\n        h1 {\n            color: #ff6600;\n            font-size: 36px;\n            margin-top: 50px;\n        }\n\n        p {\n            color: #333333;\n            font-size: 20px;\n            margin-top: 30px;\n        }\n\n        .highlight {\n            color: #ff6600;\n            font-weight: bold;\n        }\n    </style>\n</head>\n\n<body>\n    <h1>Estimada/o {name}</h1>\n    <p>\n        Este es un recordatorio para la entrega de\n        <span class=\"highlight\">{asset}</span> antes del <span class=\"highlight\">{deliveryDate}</span>.\n    </p>\n    <p>\n        ¡Gracias!\n    </p>\n</body>\n</html>";

        public EmailService(IConfiguration configuration, ApplicationDbContext context, ILogger<EmailService> logger)
        {
            _context = context;
            this.configuration = configuration;
            _logger = logger;
        }

        public void SendReminderEmails()
        {
            DateTime deliveryDateLimit = DateTime.Today.AddDays(3);
            var employeesToRemind = _context.EmployeesHasAssets
                .Include(eha => eha.Employee.Person)
                .Include(eha => eha.Asset)
                .Where(eha => eha.DeliveryDate <= deliveryDateLimit)
                .ToList();

            _logger.LogInformation("Accediendo a SendReminder");

            foreach (var employeeAsset in employeesToRemind)
            {
                string employeeName = employeeAsset.Employee.Person.Name;
                string assetName = employeeAsset.Asset.Name;
                string deliveryDate = employeeAsset.DeliveryDate.ToString("dd/MM/yyyy");
                
                string employeeEmail = employeeAsset.Employee.Person.Email;
                string emailSubject = "Recordatorio de entrega de activo";
                string emailBody = htmlTemplate
                 .Replace("{name}", employeeName)
                 .Replace("{asset}", assetName)
                 .Replace("{deliveryDate}", deliveryDate);

                SendSmtpEmail(employeeEmail, emailSubject, emailBody);

            }
        }

        public void SendSmtpEmail(string recipient, string subject, string body)
        {
            string senderEmail = configuration["GmailConfig:SenderSmtpEmail"];
            string senderName = configuration["GmailConfig:SenderSmtpName"];
            string senderPassword = configuration["GmailConfig:SenderSmtpPassword"];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderEmail, senderName);
            message.To.Add(recipient);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.Send(message);
        }
    }
}

