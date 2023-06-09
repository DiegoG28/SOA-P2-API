using System;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.DAO;
using Service.IServices;

namespace Service.Services
{
	public class AuthService: IAuthService
	{
		private readonly ILogger<AuthService> _logger;
		public readonly EmployeesRepository employeeRepository;
		public AuthService(ILogger<AuthService> logger, ApplicationDbContext context)
		{
			_logger = logger;
			employeeRepository = new EmployeesRepository(context);
		}

		public bool ValidateEmployeeLogin(string email, string password)
		{
			var user = employeeRepository.ValidateEmployeeLogin(email, password);

			if (user == null)
			{
				return false;
			}

			return true;
		}
	}
}

