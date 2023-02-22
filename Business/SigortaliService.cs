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
    public class SigortaliService : ISigortaliService
    {
        private readonly ISigortaliRepository _sigortaliRepository;
        private readonly ILogger<SigortaliService> _logger;
        public SigortaliService(
            ILogger<SigortaliService> Logger,
            ISigortaliRepository sigortaliRepository)
        {
            _logger = Logger;
            _sigortaliRepository = sigortaliRepository;
        }
        public async Task<ResultModel<sigortali>> Get(sigortali sigortali)
        {
            ResultModel<sigortali> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<SigortaliDTO>(sigortali);
                MiddlewareResult<SigortaliDTO> sigortaliDTO = await _sigortaliRepository.Get(dbEntity);

                if (!sigortaliDTO.Success)
                {
                    _logger.LogWarning(sigortaliDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<sigortali>>(sigortaliDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<sigortali>(false, "sigortali bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<List<sigortali>>> GetList()
        {
            ResultModel<List<sigortali>> Result = null;
            try
            {
                MiddlewareResult<List<SigortaliDTO>> sigortaliDTO = await _sigortaliRepository.GetList();

                if (!sigortaliDTO.Success)
                {
                    _logger.LogWarning(sigortaliDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<List<sigortali>>>(sigortaliDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<sigortali>>(false, "sigortali liste bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<object>> Add(sigortali sigortali)
        {
            ResultModel<object> Result = null;
            try
            {
                if (sigortali.AD == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<SigortaliDTO>(sigortali);
                MiddlewareResult<object> sigortaliDTO = await _sigortaliRepository.Add(dbEntity);

                if (!sigortaliDTO.Success)
                {
                    _logger.LogWarning(sigortaliDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(sigortaliDTO);

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
        public async Task<ResultModel<object>> Update(sigortali sigortali)
        {
            ResultModel<object> Result = null;
            try
            {
                if (sigortali.ID == null || sigortali.AD == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<SigortaliDTO>(sigortali);
                MiddlewareResult<object> sigortaliDTO = await _sigortaliRepository.Update(dbEntity);

                if (!sigortaliDTO.Success)
                {
                    _logger.LogWarning(sigortaliDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(sigortaliDTO);

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
