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
    public class ZeyltipsService : IZeyltipsService
    {
        private readonly IZeyltipsRepository _zeyltipsRepository;
        private readonly ILogger<ZeyltipsService> _logger;
        public ZeyltipsService(
            ILogger<ZeyltipsService> Logger,
            IZeyltipsRepository zeyltipsRepository)
        {
            _logger = Logger;
            _zeyltipsRepository = zeyltipsRepository;
        }
        public async Task<ResultModel<Zeyltips>> Get(Zeyltips zeyltips)
        {
            ResultModel<Zeyltips> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<ZeyltipsDTO>(zeyltips);
                MiddlewareResult<ZeyltipsDTO> zeyltipsDTO = await _zeyltipsRepository.Get(dbEntity);

                if (!zeyltipsDTO.Success)
                {
                    _logger.LogWarning(zeyltipsDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<Zeyltips>>(zeyltipsDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Zeyltips>(false, "Zeyltips bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<List<Zeyltips>>> GetList()
        {
            ResultModel<List<Zeyltips>> Result = null;
            try
            {
                MiddlewareResult<List<ZeyltipsDTO>> zeyltipsDTO = await _zeyltipsRepository.GetList();

                if (!zeyltipsDTO.Success)
                {
                    _logger.LogWarning(zeyltipsDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<List<Zeyltips>>>(zeyltipsDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Zeyltips>>(false, "zeyltips liste bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<object>> Add(Zeyltips zeyltips)
        {
            ResultModel<object> Result = null;
            try
            {
                if (zeyltips.ZEYLTIP == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<ZeyltipsDTO>(zeyltips);
                MiddlewareResult<object> zeyltipsDTO = await _zeyltipsRepository.Add(dbEntity);

                if (!zeyltipsDTO.Success)
                {
                    _logger.LogWarning(zeyltipsDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(zeyltipsDTO);

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
        public async Task<ResultModel<object>> Update(Zeyltips zeyltips)
        {
            ResultModel<object> Result = null;
            try
            {
                if (zeyltips.ID == null || zeyltips.ZEYLTIP == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<ZeyltipsDTO>(zeyltips);
                MiddlewareResult<object> zeyltipsDTO = await _zeyltipsRepository.Update(dbEntity);

                if (!zeyltipsDTO.Success)
                {
                    _logger.LogWarning(zeyltipsDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(zeyltipsDTO);

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
