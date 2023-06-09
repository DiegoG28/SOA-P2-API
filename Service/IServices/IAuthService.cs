using System;
namespace Service.IServices
{
	public interface IAuthService
	{
		public bool ValidateEmployeeLogin(string email, string password);
	}
}

