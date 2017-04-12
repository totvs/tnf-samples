using System;
using System.ComponentModel.DataAnnotations;
using Tnf.AutoMapper;

namespace Tnf.Sample.Tasks.Dtos
{
    [AutoMapTo(typeof(Task))]
    public class CreateTaskInput
    {
        [Required]
        [MaxLength(Task.MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(Task.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}