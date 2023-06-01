using System;
namespace Service.IServices
{
	public interface IEmailService
	{
        void SendReminderEmails();
        void SendSmtpEmail(string recipient, string subject, string body);
    }
}

