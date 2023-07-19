using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using HQSOFT.eBiz.Inventory.EntityFrameworkCore;
using Xunit;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodeRepositoryTests : InventoryEntityFrameworkCoreTestBase
    {
        private readonly IReasonCodeRepository _reasonCodeRepository;

        public ReasonCodeRepositoryTests()
        {
            _reasonCodeRepository = GetRequiredService<IReasonCodeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _reasonCodeRepository.GetListAsync(
                    reasonCodeID: "539134f68b914f3d902171cf8c33ffc53d1d21d1c5644067aec8916f6003657a1525",
                    descr: "38f6987b20c34b068caea59db82cd0be46711",
                    usage: default,
                    subMask: "b61ec47e5c22437894efc44b9d16eba09a7cd524e86848d984399d9c365297"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _reasonCodeRepository.GetCountAsync(
                    reasonCodeID: "7a0fccb9709347cb9c262e310ab72f",
                    descr: "6fedb6",
                    usage: default,
                    subMask: "74f5d5e68400473bbf23cae1ee566f5fbb5fb1fd925040b69d1aa08391ef9d"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}