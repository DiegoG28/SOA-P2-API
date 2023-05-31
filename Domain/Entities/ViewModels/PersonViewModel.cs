using System;


namespace Domain.Entities.ViewModels
{
	public class PersonViewModel
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string CURP { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }
    }
}

