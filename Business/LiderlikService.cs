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
    public class LiderlikService : ILiderlikService
    {
        private readonly ILiderlikRepository _liderlikRepository;
        private readonly ILogger<LiderlikService> _logger;
        public LiderlikService(
            ILogger<LiderlikService> Logger,
            ILiderlikRepository liderlikRepository)
        {
            _logger = Logger;
            _liderlikRepository = liderlikRepository;
        }
        public async Task<ResultModel<List<Liderlik>>> GetList()
        {
            ResultModel<List<Liderlik>> Result = null;
            try
            {
                MiddlewareResult<List<LiderlikDTO>> liderlikDTO = await _liderlikRepository.GetList();

                if (!liderlikDTO.Success)
                {
                    _logger.LogWarning(liderlikDTO.ServiceMessage);
                }

                var mapped = BusinessMapper.Mapper.Map<ResultModel<List<Liderlik>>>(liderlikDTO);

                if (mapped != null && mapped.Success && mapped.Data != null)
                {
                    int sira = 1;
                    foreach (var item in mapped.Data)
                    {
                        item.SIRA = sira++;
                    }
                }

                Result = mapped;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Liderlik>>(false, "Liderlik bilgisi alınırken hata oluştu.");
            }
            return Result;
        }
    }
}
