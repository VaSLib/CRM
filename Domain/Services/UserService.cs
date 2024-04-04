using DAL.Entity;
using Domain.Result;

namespace Domain.Services;

public class UserService
{
   private readonly IBaseRepository<User> _userRepository;

    public UserService(IBaseRepository<Report> reportRepository, ILogger logger, IBaseRepository<User> userRepository, IReportValidator reportValidator, IMapper mapper)
    {
        _reportRepository = reportRepository;
        _logger = logger;
        _userRepository = userRepository;
        _reportValidator = reportValidator;
        _mapper = mapper;
    }


    public async Task<BaseResult<UserDto>> CreateReportAsync(CreateUserDto dto)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Id == dto.UserId);
            var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r => r.Name == dto.Name);
            var result = _reportValidator.CreateValidator(report, user);
            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                };
            }

            report = new Report()
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = user.Id
            };

            await _reportRepository.CreateAsync(report);

            return new BaseResult<ReportDto>()
            {
                Data = _mapper.Map<ReportDto>(report)
            };


        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError,
            };
        }
    }
}
