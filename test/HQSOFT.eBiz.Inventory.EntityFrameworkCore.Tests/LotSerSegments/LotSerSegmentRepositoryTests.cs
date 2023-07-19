using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentRepositoryTests : InventoryEntityFrameworkCoreTestBase
    {
        private readonly ILotSerSegmentRepository _lotSerSegmentRepository;

        public LotSerSegmentRepositoryTests()
        {
            _lotSerSegmentRepository = GetRequiredService<ILotSerSegmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerSegmentRepository.GetListAsync(
                    asgmentType: default,
                    value: "569dfee66679466a9b96d85bc7a7231df0bb17d6e1d744559e045db134"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerSegmentRepository.GetCountAsync(
                    asgmentType: default,
                    value: "6496d293266c411cb930bd7cba11b11e2495f1964a8f43df9075ce00d7d56e42803be88a5b9842f6b31dd54"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}