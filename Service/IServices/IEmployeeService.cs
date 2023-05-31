using System;
using Domain.Entities;
using Domain.Entities.Requests;
using Service.Services;

namespace Service.IServices
{
	public interface IEmployeeService
	{
		List<EmployeeViewModel> GetEmployees();
		Employees CreateEmployee(CreateEmployeeRequest request);
		Employees UpdateEmployee(int employeeId, EditEmployeeRequest changes);
		string DeleteEmployee(int employeeId);
        // bool AddAssetsToEmployee(List<EmployeesHasAssets> employeesAssets);
        string RemoveAssetFromEmployee(int employeeId, int assetId);
	}
}

