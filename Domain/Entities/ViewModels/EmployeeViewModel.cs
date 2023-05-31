using System;
using Domain.Entities.ViewModels;

namespace Domain.Entities
{
	public class EmployeeViewModel
	{
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public int EmployeeNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CURP { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public List<EmployeeAssetViewModel> Assets { get; set; }
    }
}

