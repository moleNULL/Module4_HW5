namespace EFCore_LazyLoading_LINQ.Data.Entities
{
    public class Title
    {
        public int TitleId { get; set; }
        public string? Name { get; set; }

        public virtual List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
