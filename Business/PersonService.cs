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
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonService> _logger;
        public PersonService(
            ILogger<PersonService> Logger,
            IPersonRepository personRepository)
        {
            _logger = Logger;
            _personRepository = personRepository;
        }
        public async Task<ResultModel<person>> Get(person person)
        {
            ResultModel<person> Result = null;
            try
            {
                var dbEntity = BusinessMapper.Mapper.Map<PersonDTO>(person);
                MiddlewareResult<PersonDTO> personDTO = await _personRepository.Get(dbEntity);

                if (!personDTO.Success)
                {
                    _logger.LogWarning(personDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<person>>(personDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Get {ex.GetErrorDetail()}");
                Result = new ResultModel<person>(false, "person bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<List<person>>> GetList()
        {
            ResultModel<List<person>> Result = null;
            try
            {
                MiddlewareResult<List<PersonDTO>> personDTO = await _personRepository.GetList();

                if (!personDTO.Success)
                {
                    _logger.LogWarning(personDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<List<person>>>(personDTO);

                Result = businessEntity;

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<person>>(false, "person liste bilgisi alınırken hata oluştu.");
            }

            return Result;
        }
        public async Task<ResultModel<object>> Add(person person)
        {
            ResultModel<object> Result = null;
            try
            {
                if (person.AD == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<PersonDTO>(person);
                MiddlewareResult<object> personDTO = await _personRepository.Add(dbEntity);

                if (!personDTO.Success)
                {
                    _logger.LogWarning(personDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(personDTO);

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
        public async Task<ResultModel<object>> Update(person person)
        {
            ResultModel<object> Result = null;
            try
            {
                if (person.ID == null || person.AD == null)
                {
                    Result = new ResultModel<object>(false, "Bilgiler hatalı, lütfen kontrol ediniz.");
                    return Result;
                }
                var dbEntity = BusinessMapper.Mapper.Map<PersonDTO>(person);
                MiddlewareResult<object> personDTO = await _personRepository.Update(dbEntity);

                if (!personDTO.Success)
                {
                    _logger.LogWarning(personDTO.ServiceMessage);//Servis mesajını dışarı vermedik sadece log seviyesinde bıraktık
                }

                var businessEntity = BusinessMapper.Mapper.Map<ResultModel<object>>(personDTO);

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
