using System;
namespace Domain.Entities.ViewModels
{
	public class EmployeeAssetViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}

