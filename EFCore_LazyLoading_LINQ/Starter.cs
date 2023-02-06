using System.Globalization; // needed to print money (Project.Budget) in this format "19,950,000"
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EFCore_LazyLoading_LINQ.Data.Entities;
using EFCore_LazyLoading_LINQ.Data;

namespace EFCore_LazyLoading_LINQ
{
    internal class Starter
    {
        public static void Run()
        {
            try
            {
                var options = CreateDbOptions();
                InitializeDbIfNotExists(options);

                var repository = new Repository(options);
                repository.JoinThreeTables();
                repository.DifferenceBetweenTodayAndHiredDate();
                repository.UpdateTwoEntitiesInTransaction();
                repository.AddEmployee();
                repository.RemoveEmployee();
                repository.GroupEmployeesByTitle();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception! {ex.Message}");
            }
        }

        // Create options that will be used in every DbContext
        private static DbContextOptions<ApplicationContext> CreateDbOptions()
        {
            string jsonSettingsFile = "appsettings.json";

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(jsonSettingsFile);

            var config = builder.Build();
            string? connectionString = config.GetConnectionString("DefaultConnection");

            if (connectionString is null)
            {
                throw new Exception("connectionString is null");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;

            return options;
        }

        // If there are no data in tables - insert default values
        private static void InitializeDbIfNotExists(DbContextOptions<ApplicationContext> options)
        {
            using (var db = new ApplicationContext(options))
            {
                if (!db.Clients.Any() || !db.Projects.Any())
                {
                    InsertDataIntoDB(options);
                }
                else
                {
                    Console.WriteLine("Already have data");
                }
            }
        }

        // Fill tables with data: Client, Project, Title, Office and Employee
        private static void InsertDataIntoDB(DbContextOptions<ApplicationContext> options)
        {
            using (var db = new ApplicationContext(options))
            {
#region Clients
                Client olenaPetrenko = new Client()
                {
                    FirstName = "Olena",
                    LastName = "Petrenko",
                    Email = "olenapetrenko@gmail.com",
                    PhoneNumber = "+380671234567",
                    City = "Kyiv",
                };

                Client volodymyrIvanov = new Client()
                {
                    FirstName = "Volodymyr",
                    LastName = "Ivanov",
                    Email = "volodymyrivanov@hotmail.com",
                    PhoneNumber = "+380934567890",
                    City = "Kharkiv"
                };

                Client mykhailoKovalchuk = new Client()
                {
                    FirstName = "Mykhailo",
                    LastName = "Kovalchuk",
                    Email = "mykhailokovalchuk@gmail.com",
                    PhoneNumber = "+380987654321",
                    City = "Bakhmut"
                };

                Client yevheniyaPopova = new Client()
                {
                    FirstName = "Yevheniya",
                    LastName = "Popova",
                    Email = "yevheniyapopova@hotmail.com",
                    PhoneNumber = "+380507890123",
                    City = "Sievierodonetsk"
                };

                Client andriyShevchenko = new Client
                {
                    FirstName = "Andriy",
                    LastName = "Shevchenko",
                    Email = "andriyshevchenko@gmail.com",
                    PhoneNumber = "+380992345678",
                    City = "Vovchansk"
                };

                Client nataliyaYaremchuk = new Client()
                {
                    FirstName = "Nataliya",
                    LastName = "Yaremchuk",
                    Email = "nataliyayaremchuk@hotmail.com",
                    PhoneNumber = "+380937890123",
                    City = "Starobilsk",
                };
#endregion

                db.Clients.AddRange(olenaPetrenko, volodymyrIvanov, mykhailoKovalchuk, yevheniyaPopova, andriyShevchenko, nataliyaYaremchuk);
                db.SaveChanges();

#region Projects
                Project steamBay = new Project()
                {
                    Name = "SteamBay",
                    Budget = 5100999m,
                    ProgrammingLanguage = "C#",
                    StartedTime = new DateTime(2008, 3, 17),

                    ClientId = nataliyaYaremchuk.ClientId
                };

                Project teleScheme = new Project()
                {
                    Name = "TeleScheme",
                    Budget = 7300999m,
                    ProgrammingLanguage = "Go",
                    StartedTime = new DateTime(2018, 12, 25),

                    ClientId = andriyShevchenko.ClientId
                };

                Project ukrtelecom = new Project()
                {
                    Name = "Ukrtelecom",
                    Budget = 153556m,
                    ProgrammingLanguage = "PHP",
                    StartedTime = new DateTime(1993, 5, 10),

                    ClientId = mykhailoKovalchuk.ClientId
                };
#endregion

                db.Projects.AddRange(steamBay, teleScheme, ukrtelecom);
                db.SaveChanges();

#region Titles
                Title juniorDeveloper = new Title
                {
                    Name = "Junior Developer"
                };

                Title middleDeveloper = new Title
                {
                    Name = "Middle Developer"
                };

                Title devOps = new Title
                {
                    Name = "DevOps"
                };
#endregion

                db.Titles.AddRange(juniorDeveloper, middleDeveloper, devOps);
                db.SaveChanges();

#region Offices
                Office officeUS = new Office
                {
                    Title = "Goranjo Inc",
                    Location = "USA"
                };

                Office officeKorea = new Office
                {
                    Title = "TeleScheme Korea",
                    Location = "South Korea"
                };

                Office officeJapan = new Office
                {
                    Title = "TeleScheme",
                    Location = "Japan"
                };

                Office officeUA = new Office
                {
                    Title = "Ukrtelecom",
                    Location = "Ukraine"
                };
#endregion

                db.Offices.AddRange(officeUS, officeKorea, officeJapan, officeUA);
                db.SaveChanges();

#region Employees
                Employee japEmp1 = new Employee
                {
                    FirstName = "Haruka",
                    LastName = "Nakamura",
                    HiredDate = new DateTime(2022, 2, 24),
                    DateOfBirth = new DateTime(2000, 1, 31),

                    Office = officeJapan,
                    Title = juniorDeveloper
                };

                Employee japEmp2 = new Employee
                {
                    FirstName = "Ayumi",
                    LastName = "Matsumoto",
                    HiredDate = new DateTime(2014, 12, 28),
                    DateOfBirth = new DateTime(1994, 6, 15),

                    Office = officeUS,
                    Title = middleDeveloper
                };

                Employee korEmp1 = new Employee
                {
                    FirstName = "Min-ji",
                    LastName = "Kim",
                    HiredDate = new DateTime(2023, 1, 18),
                    DateOfBirth = new DateTime(2002, 2, 11),

                    Office = officeUS,
                    Title = devOps
                };

                Employee korEmp2 = new Employee
                {
                    FirstName = "Hye-rin",
                    LastName = "Lee",
                    HiredDate = new DateTime(2009, 5, 16),
                    DateOfBirth = new DateTime(1989, 11, 21),

                    Office = officeKorea,
                    Title = middleDeveloper
                };

                Employee ukrEmp1 = new Employee
                {
                    FirstName = "Volodymyr",
                    LastName = "Koval",
                    HiredDate = new DateTime(1999, 3, 8),
                    DateOfBirth = new DateTime(1971, 3, 20),

                    Office = officeUA,
                    Title = devOps
                };

                Employee ukrEmp2 = new Employee
                {
                    FirstName = "Andriy",
                    LastName = "Zhylka",
                    HiredDate = new DateTime(2018, 8, 29),
                    DateOfBirth = new DateTime(1988, 6, 14),

                    Office = officeUA,
                    Title = middleDeveloper
                };
#endregion

                db.Employees.AddRange(japEmp1, japEmp2, korEmp1, korEmp2, ukrEmp1, ukrEmp2);
                db.SaveChanges();
            }
        }

        // Delete all data from table: Client, Project [for Debug]
        private static void DeleteDataFromDb(DbContextOptions<ApplicationContext> options)
        {
            using (var db = new ApplicationContext(options))
            {
                db.Clients.RemoveRange(db.Clients);
                db.Projects.RemoveRange(db.Projects);
                db.Employees.RemoveRange(db.Employees);
                db.Titles.RemoveRange(db.Titles);
                db.Offices.RemoveRange(db.Offices);
                db.EmployeeProjects.RemoveRange(db.EmployeeProjects);
                db.SaveChanges();
            }
        }
    }
}
