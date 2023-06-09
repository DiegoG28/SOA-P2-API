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
                var htmlTemplate = System.IO.File.ReadAllText("Reminder.html");
                
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

