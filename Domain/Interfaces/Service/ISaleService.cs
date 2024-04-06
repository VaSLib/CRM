using Domain.Contracts.Sale;
using Domain.Result;

namespace Domain.Services
{
    public interface ISaleService
    {
        Task<BaseResult<SaleDto>> CreateSaleAsync(SaleCreateDto SaleCreateDto);
        Task<CollectionResult<SaleDto>> GetAllSalesAsync();
        Task<CollectionResult<SaleDto>> GetAllYourSalesAsync();
    }
}