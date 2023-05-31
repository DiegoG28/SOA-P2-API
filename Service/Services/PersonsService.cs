using System;
using Domain.Entities;
using Domain.Entities.ViewModels;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.DAO;
using Service.IServices;

namespace Service.Services
{
    public class PersonsService : IPersonService
    {
        private readonly ILogger<PersonsService> _logger;
        public readonly PersonsRepository personsRepository;

        public PersonsService(ILogger<PersonsService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            personsRepository = new PersonsRepository(context);
        }

        public Persons CreatePerson(Persons person)
        {
            try
            {
                Persons createdPerson = personsRepository.CreatePerson(person);
                return createdPerson;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

    }
}

