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
    public class OranService : IOranService
    {
        private readonly IOranRepository _oranRepository;
        private readonly ILogger<OranService> _logger;
        public OranService(
            ILogger<OranService> Logger,
            IOranRepository oranRepository)
        {
            _logger = Logger;
            _oranRepository = oranRepository;
        }
        public async Task<ResultModel<Oran>> GetByMac(int macId)
        {
            ResultModel<Oran> Result = null;
            try
            {
                MiddlewareResult<OranDTO> dto = await _oranRepository.GetByMac(macId);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<Oran>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetByMac {ex.GetErrorDetail()}");
                Result = new ResultModel<Oran>(false, "Oran bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Kaydet(Oran oran)
        {
            ResultModel<object> Result = null;
            try
            {
                if (oran.MACID == null)
                {
                    return new ResultModel<object>(false, "Maç bilgisi zorunludur.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<OranDTO>(oran);
                MiddlewareResult<object> dto = await _oranRepository.Kaydet(dbEntity);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Kaydet {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Oran kaydedilirken hata oluştu.");
            }
            return Result;
        }
    }
}
