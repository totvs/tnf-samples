using System.Threading.Tasks;
using Tnf.Sample.Tasks.Dtos;
using Tnf.Application.Services;
using Tnf.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Tnf.Sample.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);
        System.Threading.Tasks.Task Create(CreateTaskInput input);
    }
}