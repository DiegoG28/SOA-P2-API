using System;
using Domain.Entities;
using Domain.Entities.ViewModels;

namespace Service.IServices
{
	public interface IPersonService
	{
        public Persons CreatePerson(Persons person);
    }
}

