namespace EFCore_LazyLoading_LINQ.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public decimal Budget { get; set; }
        public string? ProgrammingLanguage { get; set; }
        public DateTime StartedTime { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
