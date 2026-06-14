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
    public class TahminRepository : ITahminRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public TahminRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<List<TahminDTO>>> GetListByKullanici(int kullaniciId)
        {
            MiddlewareResult<List<TahminDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@KULLANICIID", kullaniciId);

                    var DbResult = await connection.QueryAsync<TahminDTO>(
                        "SELECT t.ID, t.KULLANICIID, t.MACID, t.EVSAHIBISKOR, t.DEPLASMANSKOR, t.PUAN, t.TAHMINTARIHI, " +
                        "e.AD AS EVSAHIBIAD, d.AD AS DEPLASMANAD, m.TARIH AS MACTARIHI, m.DURUM AS MACDURUM, " +
                        "m.EVSAHIBISKOR AS GERCEKEVSKOR, m.DEPLASMANSKOR AS GERCEKDEPSKOR " +
                        "FROM TAHMIN t JOIN MAC m ON m.ID = t.MACID " +
                        "JOIN TAKIM e ON e.ID = m.EVSAHIBIID JOIN TAKIM d ON d.ID = m.DEPLASMANID " +
                        "WHERE t.KULLANICIID = @KULLANICIID ORDER BY m.TARIH;", parameters);
                    Result = new MiddlewareResult<List<TahminDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<TahminDTO>>(false, "Tahmin liste bilgisi alınmadı.", $"TahminRepository GetListByKullanici {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(TahminDTO tahminDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@KULLANICIID", tahminDTO.KULLANICIID);
                    parameters.Add("@MACID", tahminDTO.MACID);
                    parameters.Add("@EVSAHIBISKOR", tahminDTO.EVSAHIBISKOR);
                    parameters.Add("@DEPLASMANSKOR", tahminDTO.DEPLASMANSKOR);

                    // Maç henüz başlamadıysa tahmini ekle; aynı maça tahmin varsa güncelle (kilit + upsert tek sorguda).
                    var DbResult = await connection.ExecuteAsync(
                        "INSERT INTO TAHMIN (KULLANICIID, MACID, EVSAHIBISKOR, DEPLASMANSKOR, TAHMINTARIHI) " +
                        "SELECT @KULLANICIID, @MACID, @EVSAHIBISKOR, @DEPLASMANSKOR, NOW() " +
                        "FROM MAC WHERE ID = @MACID AND TARIH > NOW() " +
                        "ON DUPLICATE KEY UPDATE EVSAHIBISKOR = @EVSAHIBISKOR, DEPLASMANSKOR = @DEPLASMANSKOR, TAHMINTARIHI = NOW();", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, DbResult > 0);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Tahmin kaydedilemedi.", $"TahminRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Puanla(int macId, int evSkor, int depSkor)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MACID", macId);
                    parameters.Add("@E", evSkor);
                    parameters.Add("@D", depSkor);

                    // Tam skor = 5, doğru sonuç + gol farkı = 3, sadece doğru sonuç (galip/beraberlik) = 2, diğer = 0
                    var DbResult = await connection.ExecuteAsync(
                        "UPDATE TAHMIN SET PUAN = CASE " +
                        "WHEN EVSAHIBISKOR = @E AND DEPLASMANSKOR = @D THEN 5 " +
                        "WHEN SIGN(EVSAHIBISKOR - DEPLASMANSKOR) = SIGN(@E - @D) AND (EVSAHIBISKOR - DEPLASMANSKOR) = (@E - @D) THEN 3 " +
                        "WHEN SIGN(EVSAHIBISKOR - DEPLASMANSKOR) = SIGN(@E - @D) THEN 2 " +
                        "ELSE 0 END " +
                        "WHERE MACID = @MACID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult >= 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Tahminler puanlanamadı.", $"TahminRepository Puanla {ex.GetErrorDetail()}");
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
