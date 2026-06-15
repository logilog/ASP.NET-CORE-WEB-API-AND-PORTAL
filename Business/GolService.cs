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
    public class GolService : IGolService
    {
        private readonly IGolRepository _golRepository;
        private readonly ILogger<GolService> _logger;
        public GolService(
            ILogger<GolService> Logger,
            IGolRepository golRepository)
        {
            _logger = Logger;
            _golRepository = golRepository;
        }
        public async Task<ResultModel<List<Gol>>> MacGollari(int macId)
        {
            ResultModel<List<Gol>> Result = null;
            try
            {
                MiddlewareResult<List<GolDTO>> dto = await _golRepository.GetByMac(macId);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<List<Gol>>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"MacGollari {ex.GetErrorDetail()}");
                Result = new ResultModel<List<Gol>>(false, "Maç golleri alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<List<GolKrali>>> GolKrali()
        {
            ResultModel<List<GolKrali>> Result = null;
            try
            {
                MiddlewareResult<List<GolKraliDTO>> dto = await _golRepository.GolKraliList();
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                var mapped = BusinessMapper.Mapper.Map<ResultModel<List<GolKrali>>>(dto);
                if (mapped != null && mapped.Success && mapped.Data != null)
                {
                    int sira = 1;
                    foreach (var item in mapped.Data) item.SIRA = sira++;
                }
                Result = mapped;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GolKrali {ex.GetErrorDetail()}");
                Result = new ResultModel<List<GolKrali>>(false, "Gol krallığı alınırken hata oluştu.");
            }
            return Result;
        }
        public async Task<ResultModel<object>> Add(Gol gol)
        {
            ResultModel<object> Result = null;
            try
            {
                if (gol.MACID == null || gol.OYUNCUID == null || gol.TAKIMID == null)
                {
                    return new ResultModel<object>(false, "Gol bilgileri eksik, lütfen kontrol ediniz.");
                }
                var dbEntity = BusinessMapper.Mapper.Map<GolDTO>(gol);
                MiddlewareResult<object> dto = await _golRepository.Add(dbEntity);
                if (!dto.Success) _logger.LogWarning(dto.ServiceMessage);
                Result = BusinessMapper.Mapper.Map<ResultModel<object>>(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Add {ex.GetErrorDetail()}");
                Result = new ResultModel<object>(false, "Gol eklenirken hata oluştu.");
            }
            return Result;
        }
    }
}
