using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using HQSOFT.eBiz.Inventory.ReasonCodes;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IReasonCodeRepository _reasonCodeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ReasonCodesDataSeedContributor(IReasonCodeRepository reasonCodeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _reasonCodeRepository = reasonCodeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _reasonCodeRepository.InsertAsync(new ReasonCode
            (
                id: Guid.Parse("dba4926c-f377-4024-9ac5-1bad758846d5"),
                reasonCodeID: "539134f68b914f3d902171cf8c33ffc53d1d21d1c5644067aec8916f6003657a1525",
                descr: "38f6987b20c34b068caea59db82cd0be46711",
                usage: default,
                subMask: "b61ec47e5c22437894efc44b9d16eba09a7cd524e86848d984399d9c365297",
                accountID: 1031969414,
                subID: 2058000623,
                salesAcctID: 81518776,
                salesSubID: 1439063180
            ));

            await _reasonCodeRepository.InsertAsync(new ReasonCode
            (
                id: Guid.Parse("959a0b04-3737-403a-b4e4-df429751332e"),
                reasonCodeID: "7a0fccb9709347cb9c262e310ab72f",
                descr: "6fedb6",
                usage: default,
                subMask: "74f5d5e68400473bbf23cae1ee566f5fbb5fb1fd925040b69d1aa08391ef9d",
                accountID: 133794788,
                subID: 816262006,
                salesAcctID: 1829372458,
                salesSubID: 1486874692
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}