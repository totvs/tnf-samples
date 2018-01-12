﻿using System;
using System.ComponentModel.DataAnnotations;
using Tnf.Repositories.Entities;

namespace Case2.Infra.Entities
{
    public class Employee : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
