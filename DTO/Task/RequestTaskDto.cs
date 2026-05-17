using System.ComponentModel.DataAnnotations;
using TaskMini.Common.Enum;

namespace TaskMini.DTO.Task
{
    public class RequestTaskDto
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        public TaskItemStatus? Status { get; set; }

        public string? Search {  get; set; }




    }
}
