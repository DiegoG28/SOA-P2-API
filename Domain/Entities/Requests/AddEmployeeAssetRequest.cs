using System;
namespace Domain.Entities.Requests
{
	public class AddEmployeeAssetRequest
	{
        public int EmployeeId { get; set; }
        public int AssetId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}

