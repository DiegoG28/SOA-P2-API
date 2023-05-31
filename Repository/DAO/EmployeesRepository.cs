using System;
using Domain.Entities;
using Domain.Entities.Requests;
using Domain.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.DAO
{
    public class EmployeesRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Employees> GetEmployees()
        {
            var query = _context.Employees
                .Include(e => e.Person)
                .Include(e => e.EmployeesHasAssets)
                .ThenInclude(eha => eha.Asset);

            return query.ToList();
        }

        public Employees GetEmployeeById(int employeeId)
        {
            var query = _context.Employees
                .Include(e => e.Person)
                .Include(e => e.EmployeesHasAssets)
                .ThenInclude(eha => eha.Asset)
                .Where(e => e.Id == employeeId);

            var employee = query.SingleOrDefault();
            return employee;
        }

        public Employees UpdateEmployee(int employeeId, EditEmployeeRequest changes)
        {
            var employee = GetEmployeeById(employeeId);

            if (employee == null)
            {
                return null;
            }

            

            foreach (var property in changes.GetType().GetProperties())
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(changes);

                if (propertyValue != null)
                {
                    switch (propertyName)
                    {
                        case "Number":
                            employee.Number = (int)propertyValue;
                            break;
                        case "Name":
                            employee.Person.Name = (string)propertyValue;
                            break;
                        case "LastName":
                            employee.Person.LastName = (string)propertyValue;
                            break;
                        case "CURP":
                            employee.Person.CURP = (string)propertyValue;
                            break;
                        case "BirthDate":
                            employee.Person.BirthDate = (DateTime)propertyValue;
                            break;
                        case "Email":
                            employee.Person.Email = (string)propertyValue;
                            break;
                        default:
                            break;
                    }
                }
            }

            _context.SaveChanges();

            return employee;
        }

        public Employees CreateEmployee(Employees employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        public bool DeleteEmployee(int employeeId)
        {
            var employee = GetEmployeeById(employeeId);

            if (employee == null)
            {
                return false;
            }

            _context.EmployeesHasAssets.RemoveRange(employee.EmployeesHasAssets);
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return true;
        }

        public bool RemoveAssetFromEmployee(int employeeId, int assetId)
        {
            var employee = GetEmployeeById(employeeId);

            if (employee == null)
            {
                return false; // El empleado no existe
            }

            var employeeAsset = employee.EmployeesHasAssets.FirstOrDefault(eha => eha.AssetId == assetId);
            if (employeeAsset == null)
            {
                return false; // El empleado existe pero el activo no
            }

            employee.EmployeesHasAssets.Remove(employeeAsset);
            _context.SaveChanges();

            return true;
        }

        public void AddAssetsToEmployee(List<EmployeesHasAssets> assets)
        {
            _context.EmployeesHasAssets.AddRange(assets);
            _context.SaveChanges();
        }

    }
}

