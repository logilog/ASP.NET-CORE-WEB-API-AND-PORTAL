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
    public class PuanDurumuRepository : IPuanDurumuRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public PuanDurumuRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<List<PuanDurumuDTO>>> GetList()
        {
            MiddlewareResult<List<PuanDurumuDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    // Sadece oynanmış (BITTI) maçlardan, ev+deplasman perspektifi birleştirilerek hesaplanır.
                    var DbResult = await connection.QueryAsync<PuanDurumuDTO>(
                        "SELECT t.GRUP, t.ID AS TAKIMID, t.AD AS TAKIMAD, t.BAYRAK, " +
                        "COALESCE(COUNT(x.mid),0) AS OM, COALESCE(SUM(x.g),0) AS G, COALESCE(SUM(x.b),0) AS B, " +
                        "COALESCE(SUM(x.m),0) AS M, COALESCE(SUM(x.af),0) AS AG, COALESCE(SUM(x.ye),0) AS YG, " +
                        "COALESCE(SUM(x.af),0) - COALESCE(SUM(x.ye),0) AS AVERAJ, COALESCE(SUM(x.p),0) AS PUAN " +
                        "FROM TAKIM t LEFT JOIN ( " +
                        "  SELECT m.ID mid, m.EVSAHIBIID tk, m.EVSAHIBISKOR af, m.DEPLASMANSKOR ye, " +
                        "   (m.EVSAHIBISKOR>m.DEPLASMANSKOR) g,(m.EVSAHIBISKOR=m.DEPLASMANSKOR) b,(m.EVSAHIBISKOR<m.DEPLASMANSKOR) m, " +
                        "   CASE WHEN m.EVSAHIBISKOR>m.DEPLASMANSKOR THEN 3 WHEN m.EVSAHIBISKOR=m.DEPLASMANSKOR THEN 1 ELSE 0 END p " +
                        "  FROM MAC m WHERE m.DURUM='BITTI' " +
                        "  UNION ALL " +
                        "  SELECT m.ID, m.DEPLASMANID, m.DEPLASMANSKOR, m.EVSAHIBISKOR, " +
                        "   (m.DEPLASMANSKOR>m.EVSAHIBISKOR),(m.DEPLASMANSKOR=m.EVSAHIBISKOR),(m.DEPLASMANSKOR<m.EVSAHIBISKOR), " +
                        "   CASE WHEN m.DEPLASMANSKOR>m.EVSAHIBISKOR THEN 3 WHEN m.DEPLASMANSKOR=m.EVSAHIBISKOR THEN 1 ELSE 0 END " +
                        "  FROM MAC m WHERE m.DURUM='BITTI' " +
                        ") x ON x.tk = t.ID " +
                        "GROUP BY t.GRUP, t.ID, t.AD, t.BAYRAK " +
                        "ORDER BY t.GRUP, PUAN DESC, AVERAJ DESC, AG DESC;");
                    Result = new MiddlewareResult<List<PuanDurumuDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<PuanDurumuDTO>>(false, "Puan durumu alınmadı.", $"PuanDurumuRepository GetList {ex.GetErrorDetail()}");
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
