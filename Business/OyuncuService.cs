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
    public class OyuncuService : IOyuncuService
    {
        private readonly IOyuncuRepository _oyuncuRepository;
        private readonly ILogger<OyuncuService> _logger;
        public OyuncuService(
            ILogger<OyuncuService> Logger,
            IOyuncuRepository oyuncuRepository)
        {
            _logger = Logger;
            _oyuncuRepository = oyuncuRepository;
        }
        public async Task<ResultModel<Oyuncu>> Get(Oyuncu oyuncu)
        {
            ResultModel<Oyuncu> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<OyuncuDTO>(oyuncu);
                MiddlewareResult<OyuncuDTO> dto = await _oyuncuRepository.Get(dbEntity);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<Oyuncu>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Oyuncu>(false, "Oyuncu bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<List<Oyuncu>>> GetListByTakim(int takimId)
        {
            ResultModel<List<Oyuncu>> Result = null;
            try
            {
                MiddlewareResult<List<OyuncuDTO>> dto = await _oyuncuRepository.GetListByTakim(takimId);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<List<Oyuncu>>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetListByTakim {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Oyuncu>>(false, "Kadro bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Add(Oyuncu oyuncu)
        {
            ResultModel<object> Result = null;
            try
            {
                if (oyuncu.TAKIMID == null || string.IsNullOrEmpty(oyuncu.AD))
                {
                    return new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<OyuncuDTO>(oyuncu);
                MiddlewareResult<object> dto = await _oyuncuRepository.Add(dbEntity);
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
        public async Task<ResultModel<object>> Update(Oyuncu oyuncu)
        {
            ResultModel<object> Result = null;
            try
            {
                if (oyuncu.ID == null || string.IsNullOrEmpty(oyuncu.AD))
                {
                    return new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<OyuncuDTO>(oyuncu);
                MiddlewareResult<object> dto = await _oyuncuRepository.Update(dbEntity);
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
