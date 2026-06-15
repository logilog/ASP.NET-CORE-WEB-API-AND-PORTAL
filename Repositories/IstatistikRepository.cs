using Dapper;
using Entities.General;
using Entities.REPOSITORY;
using Helpers.Extensions;
using MySqlConnector;
using Repositories.Interfaces;
using System.Threading.Tasks;

namespace Repositories
{
    public class IstatistikRepository : IIstatistikRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public IstatistikRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<IstatistikDTO>> Get()
        {
            MiddlewareResult<IstatistikDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QuerySingleOrDefaultAsync<IstatistikDTO>(
                        "SELECT " +
                        "(SELECT COUNT(*) FROM MAC) AS TOPLAMMAC, " +
                        "(SELECT COUNT(*) FROM MAC WHERE DURUM='BITTI') AS OYNANANMAC, " +
                        "(SELECT COALESCE(SUM(EVSAHIBISKOR+DEPLASMANSKOR),0) FROM MAC WHERE DURUM='BITTI') AS TOPLAMGOL, " +
                        "ROUND((SELECT COALESCE(SUM(EVSAHIBISKOR+DEPLASMANSKOR),0) FROM MAC WHERE DURUM='BITTI') / NULLIF((SELECT COUNT(*) FROM MAC WHERE DURUM='BITTI'),0),2) AS MACBASIGOL, " +
                        "(SELECT COUNT(*) FROM TAKIM) AS TOPLAMTAKIM, " +
                        "(SELECT COUNT(*) FROM OYUNCU) AS TOPLAMOYUNCU, " +
                        "(SELECT COUNT(*) FROM TAHMIN) AS TOPLAMTAHMIN, " +
                        "(SELECT o.AD FROM GOL g JOIN OYUNCU o ON o.ID=g.OYUNCUID GROUP BY o.ID,o.AD ORDER BY COUNT(*) DESC, o.AD LIMIT 1) AS GOLKRALI, " +
                        "(SELECT COUNT(*) FROM GOL g2 GROUP BY g2.OYUNCUID ORDER BY COUNT(*) DESC LIMIT 1) AS GOLKRALIGOL;");
                    Result = new MiddlewareResult<IstatistikDTO>(DbResult);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<IstatistikDTO>(false, "İstatistik alınmadı.", $"IstatistikRepository Get {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
    }
}
