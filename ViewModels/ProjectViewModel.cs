using System.ComponentModel.DataAnnotations;

namespace prjmng.ViewModels
{
    public class ProjectViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int ManagerId { get; set; }
    }
}