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
    public class TahminService : ITahminService
    {
        private readonly ITahminRepository _tahminRepository;
        private readonly ILogger<TahminService> _logger;
        public TahminService(
            ILogger<TahminService> Logger,
            ITahminRepository tahminRepository)
        {
            _logger = Logger;
            _tahminRepository = tahminRepository;
        }
        public async Task<ResultModel<List<Tahmin>>> GetListByKullanici(int kullaniciId)
        {
            ResultModel<List<Tahmin>> Result = null;
            try
            {
                MiddlewareResult<List<TahminDTO>> tahminDTO = await _tahminRepository.GetListByKullanici(kullaniciId);

                if (!tahminDTO.Success)
                {
                    _logger.LogWarning(tahminDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<List<Tahmin>>>(tahminDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetListByKullanici {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Tahmin>>(false, "Tahmin liste bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Add(Tahmin tahmin)
        {
            ResultModel<object> Result = null;
            try
            {
                if (tahmin.KULLANICIID == null || tahmin.MACID == null || tahmin.EVSAHIBISKOR == null || tahmin.DEPLASMANSKOR == null)
                {
                    return new ResultModel<object>(false, "Tahmin bilgileri eksik, lütfen kontrol ediniz.");
                }
                if (tahmin.EVSAHIBISKOR < 0 || tahmin.DEPLASMANSKOR < 0)
                {
                    return new ResultModel<object>(false, "Skorlar negatif olamaz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<TahminDTO>(tahmin);
                MiddlewareResult<object> tahminDTO = await _tahminRepository.Add(dbEntity);

                if (!tahminDTO.Success)
                {
                    _logger.LogWarning(tahminDTO.ServiceMessage);
                    return new ResultModel<object>(false, string.IsNullOrEmpty(tahminDTO.Message)
                        ? "Tahmin kaydedilemedi. Maç başlamış olabilir."
                        : tahminDTO.Message);
                }

                Result = new ResultModel<object>(true, "Tahmininiz kaydedildi.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Add {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Tahmin kaydedilirken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> MacSonuclariniPuanla(int macId, int evSkor, int depSkor)
        {
            ResultModel<object> Result = null;
            try
            {
                MiddlewareResult<object> tahminDTO = await _tahminRepository.Puanla(macId, evSkor, depSkor);

                if (!tahminDTO.Success)
                {
                    _logger.LogWarning(tahminDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(tahminDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"MacSonuclariniPuanla {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Tahminler puanlanırken hata oluştu.");
            }
            return Result;
        }
    }
}
