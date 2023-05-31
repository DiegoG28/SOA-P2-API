using System;
using Domain.Entities;
using Domain.Entities.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using Service.Services;

namespace SOA_P2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) { 
            _employeeService = employeeService;
		
		}

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            if (ModelState.IsValid)
            {
                var createdEmployee = _employeeService.CreateEmployee(request);
                if (createdEmployee != null)
                {
                    return Ok(createdEmployee);
                }
                return BadRequest("No se pudo crear el empleado.");
            }
            return BadRequest("Datos del empleado no válidos.");
        }

        [HttpPatch("{employeeId}")]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EditEmployeeRequest changes)
        {
            var updatedEmployee = _employeeService.UpdateEmployee(employeeId, changes);
            Console.WriteLine(employeeId);
            if (updatedEmployee != null)
            {
                return Ok(updatedEmployee);
            }
            return NotFound("Empleado no encontrado.");
        }


        [HttpDelete("{employeeId}")]
        public IActionResult Delete(int employeeId)
        {
            string message = _employeeService.DeleteEmployee(employeeId);
            return Ok(message);
        }

        [HttpDelete("{employeeId}/Assets/{assetId}")]
        public IActionResult RemoveAsset(int employeeId, int assetId)
        {
            string message = _employeeService.RemoveAssetFromEmployee(employeeId, assetId);
            return Ok(message);
        }

    }
}

