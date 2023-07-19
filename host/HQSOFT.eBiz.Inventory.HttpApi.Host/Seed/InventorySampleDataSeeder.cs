using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace HQSOFT.eBiz.Inventory.Seed;

/* You can use this file to seed some sample data
 * to test your module easier.
 *
 * This class is shared among these projects:
 * - HQSOFT.eBiz.Inventory.AuthServer
 * - HQSOFT.eBiz.Inventory.Web.Unified (used as linked file)
 */
public class InventorySampleDataSeeder : ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {

    }
}
