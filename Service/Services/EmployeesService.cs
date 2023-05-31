using System;
using Domain.Entities;
using Domain.Entities.Requests;
using Domain.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.DAO;
using Service.IServices;

namespace Service.Services
{
    public class EmployeesService : IEmployeeService
    {
        private readonly ILogger<EmployeesService> _logger;
        private readonly EmployeesRepository employeesRepository;
        private readonly PersonsRepository personsRepository;

        public EmployeesService(ILogger<EmployeesService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            employeesRepository = new EmployeesRepository(context);
            personsRepository = new PersonsRepository(context);
        }

        public List<EmployeeViewModel> GetEmployees()
        {
            try
            {
                var employees = employeesRepository.GetEmployees();

                var employeesViewMoel = employees.Select(employee => MapToEmployeeViewModel(employee)).ToList();

                return employeesViewMoel;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<EmployeeViewModel>();

            }

        }


        public string DeleteEmployee(int employeeId)
        {
            try
            {
                bool employeeWasDeleted = employeesRepository.DeleteEmployee(employeeId);

                if (employeeWasDeleted)
                {
                    return "Empleado eliminado";
                }
                else
                {
                    return "El empleado no existe";
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return "Hubo un problema al eliminar el empleado";
            }
        }



        public string RemoveAssetFromEmployee(int employeeId, int assetId)
        {
            try
            {
                bool assetWasRemoved = employeesRepository.RemoveAssetFromEmployee(employeeId, assetId);

                if (assetWasRemoved)
                {
                    return "Activo eliminado del empleado";
                }
                else
                {
                    return "El empleado o el activo no existen o no están asociados";
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return "Hubo un problema al eliminar el activo del empleado";
            }
        }

        public void AddAssetsToEmployee(List<AddEmployeeAssetRequest> assets)
        {
            var employeesHasAssets = assets.Select(asset => new EmployeesHasAssets
            {
                EmployeeId = asset.EmployeeId,
                AssetId = asset.AssetId,
                AssignamentDate = DateTime.Now,
                DeliveryDate = asset.DeliveryDate,
                ReleaseDate = asset.ReleaseDate
            }).ToList();

            employeesRepository.AddAssetsToEmployee(employeesHasAssets);
        }

        public Employees CreateEmployee(CreateEmployeeRequest newEmployee)
        {
            try
            {
                var person = new Persons
                {
                    Name = newEmployee.Name,
                    LastName = newEmployee.LastName,
                    CURP = newEmployee.CURP,
                    BirthDate = newEmployee.BirthDate,
                    Email = newEmployee.Email
                };

                var createdPerson = personsRepository.CreatePerson(person);

                var random = new Random();
                var employee = new Employees
                {
                    Number = random.Next(1, 1001),
                    EntryDate = newEmployee.EntryDate,
                    Status = true,
                    PersonId = createdPerson.Id
                };

                var createdEmployee = employeesRepository.CreateEmployee(employee);

                if (newEmployee.Assets != null && newEmployee.Assets.Count > 0)
                {
                    var employeeAssets = newEmployee.Assets.Select(asset => new EmployeesHasAssets
                    {
                        EmployeeId = createdEmployee.Id,
                        AssetId = asset.Id,
                        AssignamentDate = DateTime.Now,
                        DeliveryDate = asset.DeliveryDate,
                        ReleaseDate = asset.ReleaseDate
                    }).ToList();

                    employeesRepository.AddAssetsToEmployee(employeeAssets);
                }

                return createdEmployee;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw; // O maneja el error de alguna otra manera según tus necesidades
            }
        }

        public Employees UpdateEmployee(int employeeId, EditEmployeeRequest changes)
        {
            try
            {
                var updatedEmployee = employeesRepository.UpdateEmployee(employeeId, changes);

                Console.WriteLine(updatedEmployee.Person.Name);

                if (updatedEmployee == null)
                {
                    return null;
                }


                return updatedEmployee;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        private EmployeeViewModel MapToEmployeeViewModel(Employees employee)
        {
            var employeeViewModel = new EmployeeViewModel
            {
                EmployeeId = employee.Id,
                EmployeeNumber = employee.Number,
                EntryDate = employee.EntryDate,
                PersonId = employee.Person.Id,
                Name = employee.Person.Name,
                LastName = employee.Person.LastName,
                CURP = employee.Person.CURP,
                BirthDate = employee.Person.BirthDate,
                Email = employee.Person.Email,
                Assets = employee.EmployeesHasAssets.Select(eha => new EmployeeAssetViewModel
                {
                    Id = eha.Asset.Id,
                    Name = eha.Asset.Name,
                    AssignmentDate = eha.AssignamentDate,
                    DeliveryDate = eha.DeliveryDate,
                    ReleaseDate = eha.ReleaseDate,
                }).ToList()
            };

            return employeeViewModel;
        }
    }
}

