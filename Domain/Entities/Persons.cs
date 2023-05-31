using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{

	[Table("Persons")]
	public class Persons
	{
		[Key]
		public int Id { get; set; }

        [StringLength(50)]
		[Required]
		public string Name { get; set; }

		[StringLength(50)]
		[Required]
		public string LastName { get; set; }

		[StringLength(18)]
		[Required]
		public string CURP { get; set; }

		[DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Required]
		public DateTime BirthDate { get; set; }


		[StringLength(30)]
		[Required]
		public string Email { get; set; }

        [JsonIgnore]
        public virtual Employees Employees { get; set; }

	}
}

