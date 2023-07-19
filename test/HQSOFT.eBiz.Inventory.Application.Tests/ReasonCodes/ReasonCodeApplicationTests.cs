using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodesAppServiceTests : InventoryApplicationTestBase
    {
        private readonly IReasonCodesAppService _reasonCodesAppService;
        private readonly IRepository<ReasonCode, Guid> _reasonCodeRepository;

        public ReasonCodesAppServiceTests()
        {
            _reasonCodesAppService = GetRequiredService<IReasonCodesAppService>();
            _reasonCodeRepository = GetRequiredService<IRepository<ReasonCode, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _reasonCodesAppService.GetListAsync(new GetReasonCodesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("959a0b04-3737-403a-b4e4-df429751332e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _reasonCodesAppService.GetAsync(Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ReasonCodeUpdateDto
            {
                ReasonCodeID = "e9902a7f",
                Descr = "7443ee297a484d3e97f8c52439b28041f1dbf",
                Usage = default,
                SubMask = "b1e7b241f8794efb8b5211651418a52463e0dc4b115046c186f185160a3cbf41ac13111196e7",
                AccountID = 1511642797,
                SubID = 1420994020,
                SalesAcctID = 545854151,
                SalesSubID = 1680564586
            };

            var serviceResult = await _reasonCodesAppService.CreateAsync(input);

            // Assert
            var result = await _reasonCodeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ReasonCodeID.ShouldBe("e9902a7f");
            result.Descr.ShouldBe("7443ee297a484d3e97f8c52439b28041f1dbf");
            result.Usage.ShouldBe(default);
            result.SubMask.ShouldBe("b1e7b241f8794efb8b5211651418a52463e0dc4b115046c186f185160a3cbf41ac13111196e7");
            result.AccountID.ShouldBe(1511642797);
            result.SubID.ShouldBe(1420994020);
            result.SalesAcctID.ShouldBe(545854151);
            result.SalesSubID.ShouldBe(1680564586);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ReasonCodeUpdateDto()
            {
                ReasonCodeID = "2ce8091f9f",
                Descr = "939ea6713930470e813eda11e68de0b338de28061cb743a9b9e5ec32c5660",
                Usage = default,
                SubMask = "4195cf283aa34bfa",
                AccountID = 1723766668,
                SubID = 1120206428,
                SalesAcctID = 1074165932,
                SalesSubID = 1545231668
            };

            // Act
            var serviceResult = await _reasonCodesAppService.UpdateAsync(Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5"), input);

            // Assert
            var result = await _reasonCodeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ReasonCodeID.ShouldBe("2ce8091f9f");
            result.Descr.ShouldBe("939ea6713930470e813eda11e68de0b338de28061cb743a9b9e5ec32c5660");
            result.Usage.ShouldBe(default);
            result.SubMask.ShouldBe("4195cf283aa34bfa");
            result.AccountID.ShouldBe(1723766668);
            result.SubID.ShouldBe(1120206428);
            result.SalesAcctID.ShouldBe(1074165932);
            result.SalesSubID.ShouldBe(1545231668);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _reasonCodesAppService.DeleteAsync(Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5"));

            // Assert
            var result = await _reasonCodeRepository.FindAsync(c => c.Id == Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5"));

            result.ShouldBeNull();
        }
    }
}