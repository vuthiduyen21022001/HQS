using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClassesAppServiceTests : InventoryApplicationTestBase
    {
        private readonly ILotSerClassesAppService _lotSerClassesAppService;
        private readonly IRepository<LotSerClass, Guid> _lotSerClassRepository;

        public LotSerClassesAppServiceTests()
        {
            _lotSerClassesAppService = GetRequiredService<ILotSerClassesAppService>();
            _lotSerClassRepository = GetRequiredService<IRepository<LotSerClass, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _lotSerClassesAppService.GetListAsync(new GetLotSerClassesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("307e59c7-7efc-4c32-aee7-f7257f21170e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _lotSerClassesAppService.GetAsync(Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new LotSerClassCreateDto
            {
                ClassID = "bb487b7d49384b3aa32188f55f119624903ee9f9cd8b4003a29251917939ed9e37a",
                Description = "9a552dd3ba934893b7430b385e1551d1a86c9500",
                TrackingMethod = default,
                TrackExpriationDate = true,
                RequiredforDropShip = true,
                AssignMethod = default,
                IssueMethod = default,
                ShareAutoIncremenitalValueBetwenAllClassItems = true,
                AutoIncremetalValue = 1878971542,
                AutoGenerateNextNumber = true,
                MaxAutoNumbers = 1757763660
            };

            // Act
            var serviceResult = await _lotSerClassesAppService.CreateAsync(input);

            // Assert
            var result = await _lotSerClassRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ClassID.ShouldBe("bb487b7d49384b3aa32188f55f119624903ee9f9cd8b4003a29251917939ed9e37a");
            result.Description.ShouldBe("9a552dd3ba934893b7430b385e1551d1a86c9500");
            result.TrackingMethod.ShouldBe(default);
            result.TrackExpriationDate.ShouldBe(true);
            result.RequiredforDropShip.ShouldBe(true);
            result.AssignMethod.ShouldBe(default);
            result.IssueMethod.ShouldBe(default);
            result.ShareAutoIncremenitalValueBetwenAllClassItems.ShouldBe(true);
            result.AutoIncremetalValue.ShouldBe(1878971542);
            result.AutoGenerateNextNumber.ShouldBe(true);
            result.MaxAutoNumbers.ShouldBe(1757763660);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new LotSerClassUpdateDto()
            {
                ClassID = "c492f46712054b0b81d51482c5b8d750e34d13680ae64bb0ad6a5a1aaedeb5ef593e5470426",
                Description = "f184c266",
                TrackingMethod = default,
                TrackExpriationDate = true,
                RequiredforDropShip = true,
                AssignMethod = default,
                IssueMethod = default,
                ShareAutoIncremenitalValueBetwenAllClassItems = true,
                AutoIncremetalValue = 1350539046,
                AutoGenerateNextNumber = true,
                MaxAutoNumbers = 582883605
            };

            // Act
            var serviceResult = await _lotSerClassesAppService.UpdateAsync(Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc"), input);

            // Assert
            var result = await _lotSerClassRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ClassID.ShouldBe("c492f46712054b0b81d51482c5b8d750e34d13680ae64bb0ad6a5a1aaedeb5ef593e5470426");
            result.Description.ShouldBe("f184c266");
            result.TrackingMethod.ShouldBe(default);
            result.TrackExpriationDate.ShouldBe(true);
            result.RequiredforDropShip.ShouldBe(true);
            result.AssignMethod.ShouldBe(default);
            result.IssueMethod.ShouldBe(default);
            result.ShareAutoIncremenitalValueBetwenAllClassItems.ShouldBe(true);
            result.AutoIncremetalValue.ShouldBe(1350539046);
            result.AutoGenerateNextNumber.ShouldBe(true);
            result.MaxAutoNumbers.ShouldBe(582883605);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _lotSerClassesAppService.DeleteAsync(Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc"));

            // Assert
            var result = await _lotSerClassRepository.FindAsync(c => c.Id == Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc"));

            result.ShouldBeNull();
        }
    }
}