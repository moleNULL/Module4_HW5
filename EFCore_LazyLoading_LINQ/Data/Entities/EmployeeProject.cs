namespace EFCore_LazyLoading_LINQ.Data.Entities
{
    public class EmployeeProject
    {
        public int EmployeeProjectId { get; set; }
        public decimal Rate { get; set; }
        public DateTime StartedTime { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}
