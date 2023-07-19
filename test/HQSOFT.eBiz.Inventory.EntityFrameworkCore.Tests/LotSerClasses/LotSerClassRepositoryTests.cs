using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using HQSOFT.eBiz.Inventory.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClassRepositoryTests : InventoryEntityFrameworkCoreTestBase
    {
        private readonly ILotSerClassRepository _lotSerClassRepository;

        public LotSerClassRepositoryTests()
        {
            _lotSerClassRepository = GetRequiredService<ILotSerClassRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerClassRepository.GetListAsync(
                    classID: "9975d6c7d5324cddaa7204f69611d7558552c2bf9e8c430c",
                    description: "1a60013d825741eb8b0ab4458f7eb77ff94",
                    trackingMethod: default,
                    trackExpriationDate: true,
                    requiredforDropShip: true,
                    assignMethod: default,
                    issueMethod: default,
                    shareAutoIncremenitalValueBetwenAllClassItems: true,
                    autoGenerateNextNumber: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _lotSerClassRepository.GetCountAsync(
                    classID: "5f2ee53ba33b4504b213796c4d70afe42a0d7e8a2d8",
                    description: "9364ab591a334250ba69e069742a8817e43f8491467a4757af400e68bcff5839c24cf13cb61",
                    trackingMethod: default,
                    trackExpriationDate: true,
                    requiredforDropShip: true,
                    assignMethod: default,
                    issueMethod: default,
                    shareAutoIncremenitalValueBetwenAllClassItems: true,
                    autoGenerateNextNumber: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}