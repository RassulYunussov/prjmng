using System;

namespace prjmng.Models
{
    public enum TaskState
    {
        NotCompleted,
        Completed
    }
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Assignee TaskAssignee { get; set; }
        public Project TaskProject { get; set; } 
        public ProjectTask ParentTask {get;set;}
        public TaskState State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ushort SubtaskAmount { get; set; }
    }
}