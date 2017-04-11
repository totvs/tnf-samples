using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tnf.Architecture.Dto;
using Tnf.AutoMapper;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Entities
{
    [AutoMap(typeof(CountryDto))]
    [Table("Countries")]
    public class Country : Entity
    {
        public const int MaxNameLength = 256;

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public Country()
        {
        }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
