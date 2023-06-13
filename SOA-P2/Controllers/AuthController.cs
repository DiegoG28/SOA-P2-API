using System;
using Domain.Entities.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;

namespace SOA_P2.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController: Controller
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

        [HttpPost]
        public IActionResult Index([FromBody] LoginRequest credentials)
        {
            var isValidUser = _authService.ValidateEmployeeLogin(credentials.Email, credentials.Password);

            if (!isValidUser)
            {
                return Unauthorized("Acceso incorrecto");
            }

            return Ok("Acceso correcto");
        }
    }
}

