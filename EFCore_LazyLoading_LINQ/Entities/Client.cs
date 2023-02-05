namespace EFCore_LazyLoading_LINQ.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }

        public int ProjectId { get; set; }
        public virtual List<Project> Projects { get; set; } = new List<Project>();
    }
}
