namespace EFCore_LazyLoading_LINQ.Entities
{
    public class EmployeeProject
    {
        public int EmployeeProjectId { get; set; }
        public decimal Rate { get; set; }
        public DateTime StartedTime { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
