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
    public class HaberService : IHaberService
    {
        private readonly IHaberRepository _haberRepository;
        private readonly ILogger<HaberService> _logger;
        public HaberService(
            ILogger<HaberService> Logger,
            IHaberRepository haberRepository)
        {
            _logger = Logger;
            _haberRepository = haberRepository;
        }
        public async Task<ResultModel<Haber>> Get(Haber haber)
        {
            ResultModel<Haber> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<HaberDTO>(haber);
                MiddlewareResult<HaberDTO> dto = await _haberRepository.Get(dbEntity);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<Haber>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Haber>(false, "Haber bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<List<Haber>>> GetList()
        {
            ResultModel<List<Haber>> Result = null;
            try
            {
                MiddlewareResult<List<HaberDTO>> dto = await _haberRepository.GetList();
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<List<Haber>>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Haber>>(false, "Haber listesi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Add(Haber haber)
        {
            ResultModel<object> Result = null;
            try
            {
                if (string.IsNullOrEmpty(haber.BASLIK))
                {
                    return new ResultModel<object>(false, "Başlık zorunludur.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<HaberDTO>(haber);
                MiddlewareResult<object> dto = await _haberRepository.Add(dbEntity);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Add {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Ekleme sırasında hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Update(Haber haber)
        {
            ResultModel<object> Result = null;
            try
            {
                if (haber.ID == null || string.IsNullOrEmpty(haber.BASLIK))
                {
                    return new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<HaberDTO>(haber);
                MiddlewareResult<object> dto = await _haberRepository.Update(dbEntity);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(dto);
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
