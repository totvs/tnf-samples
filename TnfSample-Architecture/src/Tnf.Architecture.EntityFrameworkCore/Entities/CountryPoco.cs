using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Entities
{
    [Table("Countries")]
    public class CountryPoco : Entity
    {
        public const int MaxNameLength = 256;

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public CountryPoco()
        {
        }

        public CountryPoco(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
