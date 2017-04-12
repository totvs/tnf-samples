using System;
using System.Collections.Generic;
using System.Linq;
using Tnf.Sample.Tasks;
using Tnf.Sample.Tasks.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tnf.Localization;

namespace Tnf.Sample.Web.Models.Tasks
{
    public class IndexViewModel
    {
        public IReadOnlyList<TaskListDto> Tasks { get; }

        public TaskState? SelectedTaskState { get; set; }

        public IndexViewModel(IReadOnlyList<TaskListDto> tasks)
        {
            Tasks = tasks;
        }

        public string GetTaskLabel(TaskListDto task)
        {
            switch (task.State)
            {
                case TaskState.Open:
                    return "label-success";
                default:
                    return "label-default";
            }
        }

        public List<SelectListItem> GetTasksStateSelectListItems(ILocalizationManager localizationManager)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",//localizationManager.GetString(SimpleTaskAppConsts.LocalizationSourceName, "AllTasks"),
                    Value = "",
                    Selected = SelectedTaskState == null
                }
            };
            
            list.AddRange(Enum.GetValues(typeof(TaskState))
                    .Cast<TaskState>()
                    .Select(state =>
                        new SelectListItem
                        {
                            Text = "",//localizationManager.GetString(SimpleTaskAppConsts.LocalizationSourceName, $"TaskState_{state}"),
                            Value = state.ToString(),
                            Selected = state == SelectedTaskState
                        })
            );

            return list;
        }
    }
}
