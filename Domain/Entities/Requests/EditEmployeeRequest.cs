using System;
namespace Domain.Entities.Requests
{
	public class EditEmployeeRequest
	{
        public int? Number { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? CURP { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
    }
}

