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
    public class GolRepository : IGolRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public GolRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<List<GolDTO>>> GetByMac(int macId)
        {
            MiddlewareResult<List<GolDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MACID", macId);
                    var DbResult = await connection.QueryAsync<GolDTO>(
                        "SELECT g.ID, g.MACID, g.OYUNCUID, g.TAKIMID, g.DAKIKA, g.PENALTI, " +
                        "o.AD AS OYUNCUAD, t.AD AS TAKIMAD, t.BAYRAK " +
                        "FROM GOL g JOIN OYUNCU o ON o.ID=g.OYUNCUID JOIN TAKIM t ON t.ID=g.TAKIMID " +
                        "WHERE g.MACID=@MACID ORDER BY g.DAKIKA;", parameters);
                    Result = new MiddlewareResult<List<GolDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<GolDTO>>(false, "Maç golleri alınmadı.", $"GolRepository GetByMac {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<List<GolKraliDTO>>> GolKraliList()
        {
            MiddlewareResult<List<GolKraliDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<GolKraliDTO>(
                        "SELECT o.ID AS OYUNCUID, o.AD AS OYUNCUAD, t.AD AS TAKIMAD, t.BAYRAK, COUNT(g.ID) AS GOLSAYISI " +
                        "FROM GOL g JOIN OYUNCU o ON o.ID=g.OYUNCUID JOIN TAKIM t ON t.ID=g.TAKIMID " +
                        "GROUP BY o.ID, o.AD, t.AD, t.BAYRAK " +
                        "ORDER BY GOLSAYISI DESC, o.AD LIMIT 100;");
                    Result = new MiddlewareResult<List<GolKraliDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<GolKraliDTO>>(false, "Gol krallığı alınmadı.", $"GolRepository GolKraliList {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<object>> Add(GolDTO golDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MACID", golDTO.MACID);
                    parameters.Add("@OYUNCUID", golDTO.OYUNCUID);
                    parameters.Add("@TAKIMID", golDTO.TAKIMID);
                    parameters.Add("@DAKIKA", golDTO.DAKIKA);
                    parameters.Add("@PENALTI", golDTO.PENALTI ?? false);
                    var DbResult = await connection.ExecuteAsync("INSERT INTO GOL (MACID,OYUNCUID,TAKIMID,DAKIKA,PENALTI) VALUES (@MACID,@OYUNCUID,@TAKIMID,@DAKIKA,@PENALTI);", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Gol ekleme yapılamadı.", $"GolRepository Add {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
    }
}
