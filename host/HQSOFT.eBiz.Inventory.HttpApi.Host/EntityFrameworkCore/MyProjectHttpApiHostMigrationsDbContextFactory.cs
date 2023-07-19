using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HQSOFT.eBiz.Inventory.EntityFrameworkCore;

public class InventoryHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<InventoryHttpApiHostMigrationsDbContext>
{
    public InventoryHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<InventoryHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Inventory"));

        return new InventoryHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
