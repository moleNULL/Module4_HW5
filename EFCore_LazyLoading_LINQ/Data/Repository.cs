using EFCore_LazyLoading_LINQ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_LazyLoading_LINQ.Data
{
    internal class Repository
    {
        private readonly ApplicationContext _dbContext;

        public Repository(DbContextOptions<ApplicationContext> options)
        {
            _dbContext = new ApplicationContext(options);
        }

        // 1. LINQ request to join 3 table using LEFT JOIN
        public void JoinThreeTables()
        {
            Console.WriteLine("1. Join Employees, Offices and Titles using LEFT Join:\n");

            var employees = from emp in _dbContext.Employees
                            join o in _dbContext.Offices on emp.OfficeId equals o.OfficeId
                            join t in _dbContext.Titles on emp.TitleId equals t.TitleId
                            select new
                            {
                                FullName = emp.FirstName + " " + emp.LastName,
                                Office = o.Title,
                                Title = t.Name
                            };

            foreach (var employee in employees)
            {
                Console.WriteLine("  " + employee.FullName + " | " + employee.Title + " | " + employee.Office);
            }

            PrintQueryString(employees.ToQueryString());
        }

        // 2. LINQ request for DateTime.Now - HiredDate filtered on the server
        public void DifferenceBetweenTodayAndHiredDate()
        {
            Console.WriteLine("2. Difference between HiredDate and today for employees" +
                " who was born before 2000:\n");

            var workersInfo = _dbContext.Employees
                .Where(e => e.DateOfBirth > new DateTime(2000, 1, 1))
                .Select(e => new
                {
                    FullName = e.FirstName + " " + e.LastName,
                    DaysSinceHiring = (DateTime.Now - e.HiredDate).Days
                });

            foreach (var workerInfo in workersInfo)
            {
                Console.WriteLine("  Name: " + workerInfo.FullName + " | DaysSinceHiring: "
                    + workerInfo.DaysSinceHiring + " days");
            }

            PrintQueryString(workersInfo.ToQueryString());
        }

        // 3. LINQ request to update 2 entities in one transaction
        public void UpdateTwoEntitiesInTransaction()
        {
            Console.WriteLine("3. Update 2 entities (Title and Office) in one transaction\n");

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Title devOps = _dbContext.Titles.First(t => t.Name == "DevOps");
                    devOps.Name = "DevOps Engineer";
                    _dbContext.Titles.Update(devOps);
                    _dbContext.SaveChanges();

                    Office officeUS = _dbContext.Offices.First(o => o.Title == "Goranjo Inc");
                    officeUS.Title = "Dappler-Pack Goranjo Inc.";
                    _dbContext.Offices.Update(officeUS);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    Console.WriteLine("Success! Transaction commit");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    Console.WriteLine("Failure! Transaction rollback");
                }
            }

            Console.WriteLine();
        }

        // 4. LINQ request to add 1 Employee record with Title, Project and Office
        public void AddEmployee()
        {
            Console.WriteLine("4. Add 1 Employee record using Title, Project and Office:\n");

            Project? project = _dbContext.Projects.FirstOrDefault(p => p.ProgrammingLanguage == "C#");
            Title? title = _dbContext.Titles.FirstOrDefault(t => t.Name == "Junior Developer");
            Office? office = _dbContext.Offices.FirstOrDefault(o => o.Location == "USA");

            if (project is not null && title is not null && office is not null)
            {
                Employee emp = new Employee
                {
                    FirstName = "James",
                    LastName = "Last",
                    HiredDate = new DateTime(2010, 5, 10),
                    DateOfBirth = new DateTime(1993, 6, 19),

                    Projects = new List<Project> { project },
                    Title = title,
                    Office = office
                };

                _dbContext.Employees.Add(emp);
                _dbContext.SaveChanges();

                Console.WriteLine("Successfully added new Employee record:");
                Console.WriteLine($"  Name: {emp.FirstName + " " + emp.LastName}");
                Console.WriteLine($"  HiredDate: {emp.HiredDate.ToShortDateString()}");
                Console.WriteLine($"  DateOfBirth: {emp.DateOfBirth}");
                Console.WriteLine($"  Projects: {emp.Projects[0].ProjectId}");
                Console.WriteLine($"  Title: {emp.TitleId}");
                Console.WriteLine($"  Office: {emp.OfficeId}");

                Console.WriteLine();
            }
        }

        // 5. LINQ request to remove 1 Employee record
        public void RemoveEmployee()
        {
            Console.WriteLine("5. Remove 1 Employee record:\n");

            string lastName = "Kim";
            Employee? employee = _dbContext.Employees.FirstOrDefault(e => e.LastName == lastName);

            if (employee is not null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();

                Console.WriteLine($"Successfully removed Employee record with LastName: {lastName}");
                Console.WriteLine();
            }
        }

        // 6. LINQ request to group Employee by [Title].Name if Name doesn't contain 'a'
        public void GroupEmployeesByTitle()
        {
            Console.WriteLine("6. Group Employees by Title if Title.Name doesn't contain 'a'");

            var groups = _dbContext.Employees.
                    Where(e => !EF.Functions.Like(e!.Title!.Name!, "%a%"))
                    .GroupBy(e => e!.Title!.Name).Select(g => new
                    {
                        Title = g.Key,
                        Count = g.Count()
                    });

            foreach (var item in groups)
            {
                Console.WriteLine("  " + item.Title + " - " + item.Count);
            }

            PrintQueryString(groups.ToQueryString());
        }

        // Print formatted raw SQL request
        private void PrintQueryString(string queryString)
        {
            int repeatCount = Console.WindowWidth;

            Console.WriteLine();
            Console.WriteLine(new string('-', repeatCount));
            Console.WriteLine("\tGenerated SQL request:\n");
            Console.WriteLine(queryString);
            Console.WriteLine(new string('-', repeatCount));

            Console.WriteLine();
        }
    }
}
