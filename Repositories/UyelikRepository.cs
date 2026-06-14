using Dapper;
using Entities.General;
using Entities.REPOSITORY;
using Helpers.Extensions;
using MySqlConnector;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class UyelikRepository : IUyelikRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public UyelikRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<List<UyelikPaketDTO>>> PaketGetList()
        {
            MiddlewareResult<List<UyelikPaketDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<UyelikPaketDTO>("SELECT ID, KOD, AD, ACIKLAMA, AYLIKUCRET, OZELLIKLER FROM UYELIKPAKET ORDER BY AYLIKUCRET;");
                    Result = new MiddlewareResult<List<UyelikPaketDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<UyelikPaketDTO>>(false, "Üyelik paketleri alınmadı.", $"UyelikRepository PaketGetList {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.CloseAsync();
                }
            }
            return Result;
        }
        public async Task<MiddlewareResult<UyelikDTO>> GetByKullanici(int kullaniciId)
        {
            MiddlewareResult<UyelikDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@KULLANICIID", kullaniciId);

                    var DbResult = await connection.QuerySingleOrDefaultAsync<UyelikDTO>(
                        "SELECT ID, KULLANICIID, PAKETKOD, TUTAR, BASLANGIC, BITIS, DURUM FROM UYELIK " +
                        "WHERE KULLANICIID = @KULLANICIID AND DURUM = 'AKTIF' AND BITIS > NOW() " +
                        "ORDER BY BITIS DESC LIMIT 1;", parameters);
                    Result = new MiddlewareResult<UyelikDTO>(DbResult);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<UyelikDTO>(false, "Üyelik bilgisi alınmadı.", $"UyelikRepository GetByKullanici {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.CloseAsync();
                }
            }
            return Result;
        }
        public async Task<MiddlewareResult<object>> SatinAl(UyelikDTO uyelikDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@KULLANICIID", uyelikDTO.KULLANICIID);
                    parameters.Add("@PAKETKOD", uyelikDTO.PAKETKOD);

                    // Ücret katalogdan (sunucu tarafından) alınır; istemciden gelen tutara güvenilmez.
                    var DbResult = await connection.ExecuteAsync(
                        "INSERT INTO UYELIK (KULLANICIID, PAKETKOD, TUTAR, BASLANGIC, BITIS, DURUM) " +
                        "SELECT @KULLANICIID, p.KOD, p.AYLIKUCRET, NOW(), DATE_ADD(NOW(), INTERVAL 1 MONTH), 'AKTIF' " +
                        "FROM UYELIKPAKET p WHERE p.KOD = @PAKETKOD;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, DbResult > 0);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Üyelik satın alınamadı.", $"UyelikRepository SatinAl {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.CloseAsync();
                }
            }
            return Result;
        }
    }
}
