using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.Sample.Tasks;
using Tnf.Sample.Tasks.Dtos;
using Tnf.Sample.Web.Models.Tasks;

namespace Tnf.Sample.Web.Controllers
{
    public class TasksController : SimpleTaskAppControllerBase
    {
        private readonly ITaskAppService _taskAppService;

        public TasksController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public async Task<ActionResult> Index(GetAllTasksInput input)
        {
            var output = await _taskAppService.GetAll(input);

            var model = new IndexViewModel(output.Items)
            {
                SelectedTaskState = input.State
            };

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }
    }
}
