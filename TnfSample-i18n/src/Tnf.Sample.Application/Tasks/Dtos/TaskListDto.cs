using System;
using Tnf.Application.Services.Dto;
using Tnf.AutoMapper;
using Tnf.Domain.Entities.Auditing;

namespace Tnf.Sample.Tasks.Dtos
{
    [AutoMapFrom(typeof(Task))]
    public class TaskListDto : EntityDto, IHasCreationTime
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }
        
    }
}