using System.Globalization; // needed to print money (Project.Budget) in this format "19,950,000"
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EFCore_LazyLoading_LINQ.Entities;

namespace EFCore_LazyLoading_LINQ
{
    internal class Starter
    {
        public static void Run()
        {
            var options = CreateDbOptions();

            InsertDataIntoDB(options);
            SelectDataFromDB(options);
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
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            return options;
        }

        // Fill tables with data: Client, Project
        private static void InsertDataIntoDB(DbContextOptions<ApplicationContext> options)
        {
            using (var db = new ApplicationContext(options))
            {
                // Projects
                Project steamBay = new Project()
                {
                    Name = "SteamBay",
                    Budget = 5100999m,
                    ProgrammingLanguage = "C#",
                    StartedTime = new DateTime(2008, 3, 17)
                };

                Project teleScheme = new Project()
                {
                    Name = "TeleScheme",
                    Budget = 7300999m,
                    ProgrammingLanguage = "Go",
                    StartedTime = new DateTime(2018, 12, 25)
                };

                Project ukrtelecom = new Project()
                {
                    Name = "Ukrtelecom",
                    Budget = 153556m,
                    ProgrammingLanguage = "PHP",
                    StartedTime = new DateTime(1993, 5, 10)
                };

                db.Projects.AddRange(steamBay, teleScheme, ukrtelecom);

                // Clients
                Client olenaPetrenko = new Client()
                {
                    FirstName = "Olena",
                    LastName = "Petrenko",
                    Email = "olenapetrenko@gmail.com",
                    PhoneNumber = "+380671234567",
                    City = "Kyiv"
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

                db.Clients.AddRange(olenaPetrenko, volodymyrIvanov, mykhailoKovalchuk, yevheniyaPopova, andriyShevchenko, nataliyaYaremchuk);

                // Unite clients with projects
                olenaPetrenko.Projects.AddRange(new List<Project>() { teleScheme, ukrtelecom });
                volodymyrIvanov.Projects.Add(steamBay);
                mykhailoKovalchuk.Projects.Add(ukrtelecom);
                yevheniyaPopova.Projects.Add(teleScheme);
                andriyShevchenko.Projects.AddRange(new List<Project>() { ukrtelecom, steamBay });
                nataliyaYaremchuk.Projects.Add(steamBay);

                db.SaveChanges();
            }
        }

        // Read data from table: Client, Project
        private static void SelectDataFromDB(DbContextOptions<ApplicationContext> options)
        {
            using (var db = new ApplicationContext(options))
            {
                var clients = db.Clients.ToList();

                Console.WriteLine("Clients:");
                foreach (var client in clients)
                {
                    Console.WriteLine($"    Id: {client.ClientId}");
                    Console.WriteLine($"    Full Name: {client.FirstName} {client.LastName}");
                    Console.WriteLine($"    Email: {client.Email}");
                    Console.WriteLine($"    Phone: {client.PhoneNumber}");
                    Console.WriteLine($"    City: {client.City}");

                    Console.WriteLine();
                }

                Console.WriteLine("\n\n");

                var projects = db.Projects.ToList();

                Console.WriteLine("Projects:");
                foreach (var project in projects)
                {
                    Console.WriteLine($"    Id: {project.ProjectId}");
                    Console.WriteLine($"    ProjectName: {project.Name}");
                    Console.WriteLine($"    Programming Language: {project.ProgrammingLanguage}");
                    Console.WriteLine($"    Budget: ${project.Budget.ToString("N0", new CultureInfo("en-US"))}");
                    Console.WriteLine($"    StartedTime: {project.StartedTime.ToShortDateString()}");

                    Console.WriteLine();
                }
            }
        }
    }
}
