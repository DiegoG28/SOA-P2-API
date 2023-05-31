using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities

{
    [Table("EmployeesHasAssets")]
    public class EmployeesHasAssets
	{
        [Key]
        public int Id { get; set; }

        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
        [JsonIgnore]
        public Employees Employee { get; set; }


        [ForeignKey("Assets")]
        public int AssetId { get; set; }
        [JsonIgnore]
        public Assets Asset { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime AssignamentDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime DeliveryDate { get; set; }
    }
}

