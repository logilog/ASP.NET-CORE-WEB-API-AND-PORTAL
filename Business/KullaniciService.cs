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
    public class KullaniciService : IKullaniciService
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly ILogger<KullaniciService> _logger;
        public KullaniciService(
            ILogger<KullaniciService> Logger,
            IKullaniciRepository kullaniciRepository)
        {
            _logger = Logger;
            _kullaniciRepository = kullaniciRepository;
        }
        public async Task<ResultModel<kullanici>> Get(kullanici kullanici)
        {
            ResultModel<kullanici> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<KullaniciDTO>(kullanici);
                MiddlewareResult<KullaniciDTO> kullaniciDTO = await _kullaniciRepository.Get(dbEntity);

                if (!kullaniciDTO.Success)
                {
                    _logger.LogWarning(kullaniciDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<kullanici>>(kullaniciDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<kullanici>(false, "kullanici bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<List<kullanici>>> GetList()
        {
            ResultModel<List<kullanici>> Result = null;
            try
            {
                MiddlewareResult<List<KullaniciDTO>> kullaniciDTO = await _kullaniciRepository.GetList();

                if (!kullaniciDTO.Success)
                {
                    _logger.LogWarning(kullaniciDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<List<kullanici>>>(kullaniciDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<kullanici>>(false, "kullanici liste bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<object>> Add(kullanici kullanici)
        {
            ResultModel<object> Result = null;
            try
            {
                if (kullanici.AD == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<KullaniciDTO>(kullanici);
                MiddlewareResult<object> kullaniciDTO = await _kullaniciRepository.Add(dbEntity);

                if (!kullaniciDTO.Success)
                {
                    _logger.LogWarning(kullaniciDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(kullaniciDTO);

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
        public async Task<ResultModel<object>> Update(kullanici kullanici)
        {
            ResultModel<object> Result = null;
            try
            {
                if (kullanici.ID == null || kullanici.AD == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<KullaniciDTO>(kullanici);
                MiddlewareResult<object> kullaniciDTO = await _kullaniciRepository.Update(dbEntity);

                if (!kullaniciDTO.Success)
                {
                    _logger.LogWarning(kullaniciDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(kullaniciDTO);

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
