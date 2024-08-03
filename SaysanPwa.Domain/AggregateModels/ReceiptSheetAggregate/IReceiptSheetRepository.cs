using SaysanPwa.Domain.SeedWorker;

namespace SaysanPwa.Domain.AggregateModels.ReceiptSheetAggregate;

public interface IReceiptSheetRepository : IRepository<Tbl_DA>
{
    public Task<IEnumerable<tbl_Sandog>> CashDesks(CancellationToken cancellationToken = default);
    public Task<IEnumerable<tbl_Hesab>> BankAccounts(CancellationToken cancellationToken = default);
    public Task<SysResult<bool>> AddReceiptSheet(Tbl_DA tbl_DA, List<tbl_Daryaft_Chek> checks, List<tbl_DA_H> haveleha, List<tbl_DA_K> kartkhan);//, List<Tbl_DA> sandogh
}