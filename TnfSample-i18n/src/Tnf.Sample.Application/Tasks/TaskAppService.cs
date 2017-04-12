using Tnf.Sample.Tasks.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Localization;
using Tnf.Application.Services.Dto;
using Tnf.Domain.Repositories;
using Tnf.Domain.Uow;
using Tnf.Linq.Extensions;
using Tnf.Runtime.Validation;
using Tnf.Application.Services;

namespace Tnf.Sample.Tasks
{
    public class TaskAppService : SampleAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository;

        public TaskAppService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;            
        }

        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                      .GetAll()
                      .ToListAsync();

            return new ListResultDto<TaskListDto>(
                ObjectMapper.Map<List<TaskListDto>>(tasks)
            );
        }

        [DisableValidation]
        public async System.Threading.Tasks.Task Create(CreateTaskInput input)
        {
            var task = ObjectMapper.Map<Task>(input);
            await _taskRepository.InsertAsync(task);
        }

    }
}
