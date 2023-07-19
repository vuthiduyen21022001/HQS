using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentsAppServiceTests : InventoryApplicationTestBase
    {
        private readonly ILotSerSegmentsAppService _lotSerSegmentsAppService;
        private readonly IRepository<LotSerSegment, Guid> _lotSerSegmentRepository;

        public LotSerSegmentsAppServiceTests()
        {
            _lotSerSegmentsAppService = GetRequiredService<ILotSerSegmentsAppService>();
            _lotSerSegmentRepository = GetRequiredService<IRepository<LotSerSegment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _lotSerSegmentsAppService.GetListAsync(new GetLotSerSegmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.LotSerSegment.Id == Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4")).ShouldBe(true);
            result.Items.Any(x => x.LotSerSegment.Id == Guid.Parse("43bbddcf-b008-4822-8db3-2af370e46460")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _lotSerSegmentsAppService.GetAsync(Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new LotSerSegmentCreateDto
            {
                SegmentID = 1140956289,
                AsgmentType = default,
                Value = "83a9d261abfe4516b9942b8af"
            };

            // Act
            var serviceResult = await _lotSerSegmentsAppService.CreateAsync(input);

            // Assert
            var result = await _lotSerSegmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SegmentID.ShouldBe(1140956289);
            result.AsgmentType.ShouldBe(default);
            result.Value.ShouldBe("83a9d261abfe4516b9942b8af");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new LotSerSegmentUpdateDto()
            {
                SegmentID = 1496750594,
                AsgmentType = default,
                Value = "4b1767f72a2241cf935d7777bf8457a69b76085702fe45c"
            };

            // Act
            var serviceResult = await _lotSerSegmentsAppService.UpdateAsync(Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4"), input);

            // Assert
            var result = await _lotSerSegmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SegmentID.ShouldBe(1496750594);
            result.AsgmentType.ShouldBe(default);
            result.Value.ShouldBe("4b1767f72a2241cf935d7777bf8457a69b76085702fe45c");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _lotSerSegmentsAppService.DeleteAsync(Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4"));

            // Assert
            var result = await _lotSerSegmentRepository.FindAsync(c => c.Id == Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4"));

            result.ShouldBeNull();
        }
    }
}