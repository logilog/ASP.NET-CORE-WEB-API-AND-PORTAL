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
    public class LiderlikRepository : ILiderlikRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public LiderlikRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<List<LiderlikDTO>>> GetList()
        {
            MiddlewareResult<List<LiderlikDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<LiderlikDTO>(
                        "SELECT k.ID AS KULLANICIID, k.KULLANICIADI, " +
                        "TRIM(CONCAT(COALESCE(k.AD,''), ' ', COALESCE(k.SOYAD,''))) AS ADSOYAD, " +
                        "COALESCE(SUM(t.PUAN),0) AS TOPLAMPUAN, " +
                        "COUNT(t.ID) AS TAHMINSAYISI, " +
                        "COALESCE(SUM(CASE WHEN t.PUAN = 5 THEN 1 ELSE 0 END),0) AS TAMSKORSAYISI " +
                        "FROM T_KULLANICI k LEFT JOIN TAHMIN t ON t.KULLANICIID = k.ID " +
                        "GROUP BY k.ID, k.KULLANICIADI, k.AD, k.SOYAD " +
                        "ORDER BY TOPLAMPUAN DESC, TAMSKORSAYISI DESC, TAHMINSAYISI DESC;");
                    Result = new MiddlewareResult<List<LiderlikDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<LiderlikDTO>>(false, "Liderlik bilgisi alınmadı.", $"LiderlikRepository GetList {ex.GetErrorDetail()}");
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
