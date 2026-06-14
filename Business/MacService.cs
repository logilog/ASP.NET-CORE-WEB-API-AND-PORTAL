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
    public class MacService : IMacService
    {
        private readonly IMacRepository _macRepository;
        private readonly ITahminService _tahminService;
        private readonly ILogger<MacService> _logger;
        public MacService(
            ILogger<MacService> Logger,
            IMacRepository macRepository,
            ITahminService tahminService)
        {
            _logger = Logger;
            _macRepository = macRepository;
            _tahminService = tahminService;
        }
        public async Task<ResultModel<Mac>> Get(Mac mac)
        {
            ResultModel<Mac> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<MacDTO>(mac);
                MiddlewareResult<MacDTO> macDTO = await _macRepository.Get(dbEntity);

                if (!macDTO.Success)
                {
                    _logger.LogWarning(macDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<Mac>>(macDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Mac>(false, "Maç bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<List<Mac>>> GetList()
        {
            ResultModel<List<Mac>> Result = null;
            try
            {
                MiddlewareResult<List<MacDTO>> macDTO = await _macRepository.GetList();

                if (!macDTO.Success)
                {
                    _logger.LogWarning(macDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<List<Mac>>>(macDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Mac>>(false, "Maç liste bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Add(Mac mac)
        {
            ResultModel<object> Result = null;
            try
            {
                if (mac.EVSAHIBIID == null || mac.DEPLASMANID == null || mac.TARIH == null)
                {
                    return new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                }
                if (mac.EVSAHIBIID == mac.DEPLASMANID)
                {
                    return new ResultModel<object>(false, "Bir takım kendisiyle eşleşemez.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<MacDTO>(mac);
                MiddlewareResult<object> macDTO = await _macRepository.Add(dbEntity);

                if (!macDTO.Success)
                {
                    _logger.LogWarning(macDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(macDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Add {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Ekleme sırasında hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Update(Mac mac)
        {
            ResultModel<object> Result = null;
            try
            {
                if (mac.ID == null || mac.EVSAHIBIID == null || mac.DEPLASMANID == null)
                {
                    return new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<MacDTO>(mac);
                MiddlewareResult<object> macDTO = await _macRepository.Update(dbEntity);

                if (!macDTO.Success)
                {
                    _logger.LogWarning(macDTO.ServiceMessage);
                }

                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(macDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Update {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Güncelleme sırasında hata oluştu.");
            }
            return Result;
        }
        /// <summary>
        /// Maç sonucunu kaydeder ve ardından o maça yapılan tüm tahminleri otomatik puanlar.
        /// </summary>
        public async Task<ResultModel<object>> SonucGir(Mac mac)
        {
            ResultModel<object> Result = null;
            try
            {
                if (mac.ID == null || mac.EVSAHIBISKOR == null || mac.DEPLASMANSKOR == null)
                {
                    return new ResultModel<object>(false, "Maç ve skor bilgileri zorunludur.");
                }
                if (mac.EVSAHIBISKOR < 0 || mac.DEPLASMANSKOR < 0)
                {
                    return new ResultModel<object>(false, "Skorlar negatif olamaz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<MacDTO>(mac);
                MiddlewareResult<object> macDTO = await _macRepository.SonucGuncelle(dbEntity);

                if (!macDTO.Success)
                {
                    _logger.LogWarning(macDTO.ServiceMessage);
                    return BusinessMapper.Mapper.Map<ResultModel<object>>(macDTO);
                }

                // Sonuç kaydedildi -> tahminleri puanla
                await _tahminService.MacSonuclariniPuanla(mac.ID.Value, mac.EVSAHIBISKOR.Value, mac.DEPLASMANSKOR.Value);

                Result = new ResultModel<object>(true, "Maç sonucu kaydedildi ve tahminler puanlandı.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"SonucGir {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Sonuç kaydedilirken hata oluştu.");
            }
            return Result;
        }
    }
}
