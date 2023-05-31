using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Assets")]
	public class Assets
	{
		[Key]
		public int Id { get; set; }

        [StringLength(50)]
		[Required]
		public string Name { get; set; }

        [StringLength(255)]
		[Required]
		public string Description { get; set; }

        [Column(TypeName = "bit")]
		[Required]
		public bool Status { get; set; }
    }
}

