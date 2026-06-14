using Business.BusinessProfile;
using Business.Interfaces;
using Entities.BUSINESS;
using Entities.General;
using Entities.REPOSITORY;
using Helpers.Extensions;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class TakimService : ITakimService
    {
        private readonly ITakimRepository _takimRepository;
        private readonly ILogger<TakimService> _logger;
        public TakimService(
            ILogger<TakimService> Logger,
            ITakimRepository takimRepository)
        {
            _logger = Logger;
            _takimRepository = takimRepository;
        }
        public async Task<ResultModel<Takim>> Get(Takim takim)
        {
            ResultModel<Takim> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<TakimDTO>(takim);
                MiddlewareResult<TakimDTO> takimDTO = await _takimRepository.Get(dbEntity);

                if (!takimDTO.Success)
                {
                    _logger.LogWarning(takimDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<Takim>>(takimDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Takim>(false, "Takım bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<List<Takim>>> GetList()
        {
            ResultModel<List<Takim>> Result = null;
            try
            {
                MiddlewareResult<List<TakimDTO>> takimDTO = await _takimRepository.GetList();

                if (!takimDTO.Success)
                {
                    _logger.LogWarning(takimDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<List<Takim>>>(takimDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Takim>>(false, "Takım liste bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Add(Takim takim)
        {
            ResultModel<object> Result = null;
            try
            {
                if (string.IsNullOrEmpty(takim.AD))
                {
                    return new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<TakimDTO>(takim);
                MiddlewareResult<object> takimDTO = await _takimRepository.Add(dbEntity);

                if (!takimDTO.Success)
                {
                    _logger.LogWarning(takimDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(takimDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Add {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Ekleme sırasında hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Update(Takim takim)
        {
            ResultModel<object> Result = null;
            try
            {
                if (takim.ID == null || string.IsNullOrEmpty(takim.AD))
                {
                    return new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<TakimDTO>(takim);
                MiddlewareResult<object> takimDTO = await _takimRepository.Update(dbEntity);

                if (!takimDTO.Success)
                {
                    _logger.LogWarning(takimDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(takimDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Update {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Güncelleme sırasında hata oluştu.");
            }
            return Result;
        }
    }
}
