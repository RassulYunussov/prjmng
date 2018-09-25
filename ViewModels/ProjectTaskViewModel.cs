using System;
using System.ComponentModel.DataAnnotations;
using prjmng.Models;

namespace prjmng.ViewModels
{
    public class ProjectTaskViewModel
    {
        [Required]
        public string Name { get; set; }
        public int? TaskAssigneeId { get; set; }
        public int ProjectId { get; set; } 
        public int? ParentTaskId {get;set;}
        public TaskState? State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}