using System;
using Domain.Entities;
using Repository.Context;

namespace Repository.DAO
{
	public class PersonsRepository
	{
        private readonly ApplicationDbContext _context;

        public PersonsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Persons CreatePerson(Persons person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();

            return person;
        }
    }
}

