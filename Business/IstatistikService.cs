using Business.BusinessProfile;
using Business.Interfaces;
using Entities.BUSINESS;
using Entities.General;
using Entities.REPOSITORY;
using Helpers.Extensions;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System.Threading.Tasks;

namespace Business
{
    public class IstatistikService : IIstatistikService
    {
        private readonly IIstatistikRepository _istatistikRepository;
        private readonly ILogger<IstatistikService> _logger;
        public IstatistikService(
            ILogger<IstatistikService> Logger,
            IIstatistikRepository istatistikRepository)
        {
            _logger = Logger;
            _istatistikRepository = istatistikRepository;
        }
        public async Task<ResultModel<Istatistik>> Get()
        {
            ResultModel<Istatistik> Result = null;
            try
            {
                MiddlewareResult<IstatistikDTO> dto = await _istatistikRepository.Get();
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<Istatistik>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Istatistik>(false, "İstatistik alınırken hata oluştu.");
            }
            return Result;
        }
    }
}
