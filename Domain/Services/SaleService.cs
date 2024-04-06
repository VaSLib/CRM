using AutoMapper;
using DAL.Entity;
using DAL.Enum;
using DAL.Repositories.Interfaces;
using Domain.Contracts.Contact;
using Domain.Contracts.Lead;
using Domain.Contracts.Sale;
using Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Domain.Services;

public class SaleService : ISaleService
{
    private readonly IBaseRepository<Sale> _saleRepository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;
    public SaleService(IBaseRepository<Sale> saleRepository, IHttpContextAccessor accessor, IMapper mapper)
    {
        _saleRepository = saleRepository;
        if (accessor.HttpContext == null)
        {
            throw new ArgumentException(nameof(accessor.HttpContext));
        }
        _httpContext = accessor.HttpContext;
        _mapper = mapper;
    }

    public async Task<CollectionResult<SaleDto>> GetAllSalesAsync()
    {
        SaleDto[] sales;
        try
        {
            sales = await _saleRepository.GetAll()
                .Select(s => _mapper.Map<SaleDto>(s))
                .ToArrayAsync();

        }
        catch (Exception ex)
        {
            return new CollectionResult<SaleDto>()
            {
                ErrorMessage = ex.Message,
            };
        }

        if (!sales.Any())
        {
            return new CollectionResult<SaleDto>()
            {
                ErrorMessage = "Sale not found",
            };
        }

        return new CollectionResult<SaleDto>()
        {
            Data = sales,
            Count = sales.Length

        };
    }

    public async Task<CollectionResult<SaleDto>> GetAllYourSalesAsync()
    {
        SaleDto[] sales;
        try
        {
            int? userIdValue = null;

            var claim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                userIdValue = userId;
            }

            sales = await _saleRepository.GetAll()
                .Where(s => s.SalerId == userIdValue)
                .Select(s => _mapper.Map<SaleDto>(s))
                .ToArrayAsync();
            if (!sales.Any())
            {
                return new CollectionResult<SaleDto>()
                {
                    ErrorMessage = "Sale not found",
                };
            }
            return new CollectionResult<SaleDto>()
            {
                Data = sales,
                Count = sales.Length

            };
        }
        catch (Exception ex)
        {
            return new CollectionResult<SaleDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<SaleDto>> CreateSaleAsync(SaleCreateDto SaleCreateDto)
    {
        try
        {
            var sale = _mapper.Map<Sale>(SaleCreateDto);

            sale.DateOfSale = DateTime.Now;

            await _saleRepository.CreateAsync(sale);


            return new BaseResult<SaleDto>()
            {
                Data = _mapper.Map<SaleDto>(sale)
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<SaleDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }


}
