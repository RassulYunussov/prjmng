namespace prjmng.Models
{
    public class Project 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manager ProjectManager { get; set; }
    }
}