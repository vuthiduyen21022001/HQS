using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.Inventory.LotSerClasses;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClassesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ILotSerClassRepository _lotSerClassRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LotSerClassesDataSeedContributor(ILotSerClassRepository lotSerClassRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _lotSerClassRepository = lotSerClassRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _lotSerClassRepository.InsertAsync(new LotSerClass
            (
                id: Guid.Parse("30dfa936-7f61-4cd5-996c-144768aeadcc"),
                classID: "9975d6c7d5324cddaa7204f69611d7558552c2bf9e8c430c",
                description: "1a60013d825741eb8b0ab4458f7eb77ff94",
                trackingMethod: default,
                trackExpriationDate: true,
                requiredforDropShip: true,
                assignMethod: default,
                issueMethod: default,
                shareAutoIncremenitalValueBetwenAllClassItems: true,
                autoIncremetalValue: 948387218,
                autoGenerateNextNumber: true,
                maxAutoNumbers: 1151109171
            ));

            await _lotSerClassRepository.InsertAsync(new LotSerClass
            (
                id: Guid.Parse("307e59c7-7efc-4c32-aee7-f7257f21170e"),
                classID: "5f2ee53ba33b4504b213796c4d70afe42a0d7e8a2d8",
                description: "9364ab591a334250ba69e069742a8817e43f8491467a4757af400e68bcff5839c24cf13cb61",
                trackingMethod: default,
                trackExpriationDate: true,
                requiredforDropShip: true,
                assignMethod: default,
                issueMethod: default,
                shareAutoIncremenitalValueBetwenAllClassItems: true,
                autoIncremetalValue: 919143531,
                autoGenerateNextNumber: true,
                maxAutoNumbers: 1041665966
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}