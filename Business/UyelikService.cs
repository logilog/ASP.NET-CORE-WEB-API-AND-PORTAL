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
    public class UyelikService : IUyelikService
    {
        private readonly IUyelikRepository _uyelikRepository;
        private readonly ILogger<UyelikService> _logger;
        public UyelikService(
            ILogger<UyelikService> Logger,
            IUyelikRepository uyelikRepository)
        {
            _logger = Logger;
            _uyelikRepository = uyelikRepository;
        }
        public async Task<ResultModel<List<UyelikPaket>>> Paketler()
        {
            ResultModel<List<UyelikPaket>> Result = null;
            try
            {
                MiddlewareResult<List<UyelikPaketDTO>> paketDTO = await _uyelikRepository.PaketGetList();

                if (!paketDTO.Success)
                {
                    _logger.LogWarning(paketDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<List<UyelikPaket>>>(paketDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Paketler {ex.GetErrorDetail()}");
                Result = new ResultModel<List<UyelikPaket>>(false, "Üyelik paketleri alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<Uyelik>> Durum(int kullaniciId)
        {
            ResultModel<Uyelik> Result = null;
            try
            {
                MiddlewareResult<UyelikDTO> uyelikDTO = await _uyelikRepository.GetByKullanici(kullaniciId);

                if (!uyelikDTO.Success)
                {
                    _logger.LogWarning(uyelikDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<Uyelik>>(uyelikDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Durum {ex.GetErrorDetail()}");
                Result = new ResultModel<Uyelik>(false, "Üyelik durumu alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> SatinAl(Uyelik uyelik)
        {
            ResultModel<object> Result = null;
            try
            {
                if (uyelik.KULLANICIID == null || string.IsNullOrEmpty(uyelik.PAKETKOD))
                {
                    return new ResultModel<object>(false, "Üyelik bilgileri eksik, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<UyelikDTO>(uyelik);
                MiddlewareResult<object> uyelikDTO = await _uyelikRepository.SatinAl(dbEntity);

                if (!uyelikDTO.Success)
                {
                    _logger.LogWarning(uyelikDTO.ServiceMessage);
                    return new ResultModel<object>(false, string.IsNullOrEmpty(uyelikDTO.Message)
                        ? "Üyelik satın alınamadı. Geçersiz paket olabilir."
                        : uyelikDTO.Message);
                }

                Result = new ResultModel<object>(true, "Üyeliğiniz başarıyla aktifleştirildi. İyi şanslar!");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"SatinAl {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Üyelik satın alınırken hata oluştu.");
            }
            return Result;
        }
    }
}
