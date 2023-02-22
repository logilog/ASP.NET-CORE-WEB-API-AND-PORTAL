using Business.BusinessProfile;
using Business.Interfaces;
using Entities.BUSINESS;
using Entities.General;
using Entities.REPOSITORY;
using Helpers.Extensions;
using Microsoft.Extensions.Logging;
using Repositories;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class ZeylService : IZeylService
    {
        private readonly IZeylRepository _zeylRepository;
        private readonly ILogger<ZeylService> _logger;
        public ZeylService(
            ILogger<ZeylService> Logger,
            IZeylRepository zeylRepository)
        {
            _logger = Logger;
            _zeylRepository = zeylRepository;
        }
        public async Task<ResultModel<Zeyl>> Get(Zeyl zeyl)
        {
            ResultModel<Zeyl> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<ZeylDTO>(zeyl);
                MiddlewareResult<ZeylDTO> zeylDTO = await _zeylRepository.Get(dbEntity);

                if (!zeylDTO.Success)
                {
                    _logger.LogWarning(zeylDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<Zeyl>>(zeylDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Zeyl>(false, "Zeyl bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<List<Zeyl>>> GetList()
        {
            ResultModel<List<Zeyl>> Result = null;
            try
            {
                MiddlewareResult<List<ZeylDTO>> zeylDTO = await _zeylRepository.GetList();

                if (!zeylDTO.Success)
                {
                    _logger.LogWarning(zeylDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<List<Zeyl>>>(zeylDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Zeyl>>(false, "Zeyl liste bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<object>> Add(Zeyl zeyl)
        {
            ResultModel<object> Result = null;
            try
            {
                if (zeyl.POLID== null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<ZeylDTO>(zeyl);
                MiddlewareResult<object> zeylDTO = await _zeylRepository.Add(dbEntity);

                if (!zeylDTO.Success)
                {
                    _logger.LogWarning(zeylDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(zeylDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Add {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Ekleme sırasında hata oluştu.");

            }
            finally
            {

            }
            return Result;

        }
        public async Task<ResultModel<object>> Update(Zeyl zeyl)
        {
            ResultModel<object> Result = null;
            try
            {
                if (zeyl.ID == null || zeyl.POLID == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<ZeylDTO>(zeyl);
                MiddlewareResult<object> zeylDTO = await _zeylRepository.Update(dbEntity);

                if (!zeylDTO.Success)
                {
                    _logger.LogWarning(zeylDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(zeylDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Update {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Güncelleme sırasında hata oluştu.");
            }
            finally
            {

            }
            return Result;
        }
    }
}
