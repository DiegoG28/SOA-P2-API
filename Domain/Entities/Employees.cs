using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Employees")]
	public class Employees
	{
		[Key]
		public int Id { get; set; }

		[Range(1, int.MaxValue)]
		[Required]
		public int Number { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Required]
		public DateTime EntryDate { get; set; }

        [Column(TypeName = "bit")]
		[Required]
        public bool Status { get; set; }

		[ForeignKey("Persons")]
		public int PersonId { get; set; }

		public virtual Persons Person { get; set; }

        public virtual ICollection<EmployeesHasAssets> EmployeesHasAssets { get; set; } = new List<EmployeesHasAssets>();

    }
}

