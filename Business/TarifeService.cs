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
    public class TarifeService : ITarifeService
    {
        private readonly ITarifeRepository _tarifeRepository;
        private readonly ILogger<TarifeService> _logger;
        public TarifeService(
            ILogger<TarifeService> Logger,
            ITarifeRepository tarifeRepository)
        {
            _logger = Logger;
            _tarifeRepository = tarifeRepository;
        }
        public async Task<ResultModel<Tarife>> Get(Tarife tarife)
        {
            ResultModel<Tarife> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<TarifeDTO>(tarife);
                MiddlewareResult<TarifeDTO> tarifeDTO = await _tarifeRepository.Get(dbEntity);

                if (!tarifeDTO.Success)
                {
                    _logger.LogWarning(tarifeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<Tarife>>(tarifeDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<Tarife>(false, "tarife bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<List<Tarife>>> GetList()
        {
            ResultModel<List<Tarife>> Result = null;
            try
            {
                MiddlewareResult<List<TarifeDTO>> tarifeDTO = await _tarifeRepository.GetList();

                if (!tarifeDTO.Success)
                {
                    _logger.LogWarning(tarifeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<List<Tarife>>>(tarifeDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Tarife>>(false, "Tarife liste bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<object>> Add(Tarife tarife)
        {
            ResultModel<object> Result = null;
            try
            {
                if (tarife.TARIFENO == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<TarifeDTO>(tarife);
                MiddlewareResult<object> tarifeDTO = await _tarifeRepository.Add(dbEntity);

                if (!tarifeDTO.Success)
                {
                    _logger.LogWarning(tarifeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(tarifeDTO);

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
        public async Task<ResultModel<object>> Update(Tarife tarife)
        {
            ResultModel<object> Result = null;
            try
            {
                if (tarife.ID == null || tarife.TARIFENO == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<TarifeDTO>(tarife);
                MiddlewareResult<object> tarifeDTO = await _tarifeRepository.Update(dbEntity);

                if (!tarifeDTO.Success)
                {
                    _logger.LogWarning(tarifeDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(tarifeDTO);

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
