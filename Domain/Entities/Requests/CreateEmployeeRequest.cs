﻿using System;
using Domain.Entities;

namespace Domain.Entities.Requests
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CURP { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public DateTime EntryDate { get; set; }
        public string Assets { get; set; }
    }

    public class AssetAssignment
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

}

