using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.Inventory.LotSerSegments;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ILotSerSegmentRepository _lotSerSegmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly LotSerClassesDataSeedContributor _lotSerClassesDataSeedContributor;

        public LotSerSegmentsDataSeedContributor(ILotSerSegmentRepository lotSerSegmentRepository, IUnitOfWorkManager unitOfWorkManager, LotSerClassesDataSeedContributor lotSerClassesDataSeedContributor)
        {
            _lotSerSegmentRepository = lotSerSegmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _lotSerClassesDataSeedContributor = lotSerClassesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _lotSerClassesDataSeedContributor.SeedAsync(context);

            await _lotSerSegmentRepository.InsertAsync(new LotSerSegment
            (
                id: Guid.Parse("67221fbd-922a-443c-8876-9cc3e9b9bab4"),
                segmentID: 63809165,
                asgmentType: default,
                value: "569dfee66679466a9b96d85bc7a7231df0bb17d6e1d744559e045db134",
                lotSerClassId: null
            ));

            await _lotSerSegmentRepository.InsertAsync(new LotSerSegment
            (
                id: Guid.Parse("43bbddcf-b008-4822-8db3-2af370e46460"),
                segmentID: 1190530769,
                asgmentType: default,
                value: "6496d293266c411cb930bd7cba11b11e2495f1964a8f43df9075ce00d7d56e42803be88a5b9842f6b31dd54",
                lotSerClassId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}