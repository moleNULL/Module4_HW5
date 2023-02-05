namespace EFCore_LazyLoading_LINQ.Entities
{
    public class Title
    {
        public int TitleId { get; set; }
        public string? Name { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
