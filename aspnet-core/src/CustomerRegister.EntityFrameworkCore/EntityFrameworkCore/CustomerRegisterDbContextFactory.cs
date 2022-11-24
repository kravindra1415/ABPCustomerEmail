using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CustomerRegister.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class CustomerRegisterDbContextFactory : IDesignTimeDbContextFactory<CustomerRegisterDbContext>
{
    public CustomerRegisterDbContext CreateDbContext(string[] args)
    {
        CustomerRegisterEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CustomerRegisterDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new CustomerRegisterDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CustomerRegister.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
