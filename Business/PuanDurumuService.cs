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
    public class PuanDurumuService : IPuanDurumuService
    {
        private readonly IPuanDurumuRepository _puanDurumuRepository;
        private readonly ILogger<PuanDurumuService> _logger;
        public PuanDurumuService(
            ILogger<PuanDurumuService> Logger,
            IPuanDurumuRepository puanDurumuRepository)
        {
            _logger = Logger;
            _puanDurumuRepository = puanDurumuRepository;
        }
        public async Task<ResultModel<List<PuanDurumu>>> GetList()
        {
            ResultModel<List<PuanDurumu>> Result = null;
            try
            {
                MiddlewareResult<List<PuanDurumuDTO>> dto = await _puanDurumuRepository.GetList();

                if (!dto.Success)
                {
                    _logger.LogWarning(dto.ServiceMessage);
                }

                var mapped = BusinessMapper.Mapper.Map<ResultModel<List<PuanDurumu>>>(dto);

                if (mapped != null && mapped.Success && mapped.Data != null)
                {
                    string grup = null;
                    int sira = 0;
                    foreach (var item in mapped.Data)
                    {
                        if (item.GRUP != grup) { grup = item.GRUP; sira = 0; }
                        item.SIRA = ++sira;
                    }
                }

                Result = mapped;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetList {ex.GetErrorDetail()}");
                Result = new ResultModel<List<PuanDurumu>>(false, "Puan durumu alınırken hata oluştu.");
            }
            return Result;
        }
    }
}
