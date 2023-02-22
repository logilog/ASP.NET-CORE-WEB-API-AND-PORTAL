using Business.BusinessProfile;
using Business.Interfaces;
using Entities.BUSINESS;
using Entities.General;
using Entities.REPOSITORY;
using Helpers.Extensions;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class PoliceService : IPoliceService
    {
        private readonly IPoliceRepository _policeRepository;
        private readonly ILogger<PoliceService> _logger;
        public PoliceService(
            ILogger<PoliceService> Logger,
            IPoliceRepository policeRepository)
        {
            _logger = Logger;
            _policeRepository = policeRepository;
        }
        public async Task<ResultModel<Police>> Get(Police police)
        {
            ResultModel<Police> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<PoliceDTO>(police);
                MiddlewareResult<PoliceDTO> policeDTO = await _policeRepository.Get(dbEntity);

                if (!policeDTO.Success)
                {
                    _logger.LogWarning(policeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<Police>>(policeDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Police>(false, "Poliçe bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<List<Police>>> GetList()
        {
            ResultModel<List<Police>> Result = null;
            try
            {
                MiddlewareResult<List<PoliceDTO>> policeDTO = await _policeRepository.GetList();

                if (!policeDTO.Success)
                {
                    _logger.LogWarning(policeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<List<Police>>>(policeDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Police>>(false, "Poliçe liste bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<object>> Add(Police police)
        {
            ResultModel<object> Result = null;
            try
            {
                if (police.POLID == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<PoliceDTO>(police);
                MiddlewareResult<object> policeDTO = await _policeRepository.Add(dbEntity);

                if (!policeDTO.Success)
                {
                    _logger.LogWarning(policeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(policeDTO);

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
        public async Task<ResultModel<object>> Update(Police police)
        {
            ResultModel<object> Result = null;
            try
            {
                if (police.ID == null || police.POLID == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<PoliceDTO>(police);
                MiddlewareResult<object> policeDTO = await _policeRepository.Update(dbEntity);

                if (!policeDTO.Success)
                {
                    _logger.LogWarning(policeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(policeDTO);

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
