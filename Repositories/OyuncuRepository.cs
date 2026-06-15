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
    public class OyuncuRepository : IOyuncuRepository
    {
        private const string SelectBase =
            "SELECT o.ID, o.TAKIMID, o.AD, o.NUMARA, o.MEVKI, t.AD AS TAKIMAD " +
            "FROM OYUNCU o JOIN TAKIM t ON t.ID = o.TAKIMID ";

        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public OyuncuRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<OyuncuDTO>> Get(OyuncuDTO oyuncuDTO)
        {
            MiddlewareResult<OyuncuDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", oyuncuDTO.ID);
                    var DbResult = await connection.QuerySingleOrDefaultAsync<OyuncuDTO>(SelectBase + "WHERE ( @ID IS NULL OR o.ID=@ID );", parameters);
                    Result = new MiddlewareResult<OyuncuDTO>(DbResult);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<OyuncuDTO>(false, "Oyuncu bilgisi alınmadı.", $"OyuncuRepository Get {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<List<OyuncuDTO>>> GetListByTakim(int takimId)
        {
            MiddlewareResult<List<OyuncuDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TAKIMID", takimId);
                    var DbResult = await connection.QueryAsync<OyuncuDTO>(SelectBase + "WHERE o.TAKIMID=@TAKIMID ORDER BY o.NUMARA;", parameters);
                    Result = new MiddlewareResult<List<OyuncuDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<OyuncuDTO>>(false, "Kadro bilgisi alınmadı.", $"OyuncuRepository GetListByTakim {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<object>> Add(OyuncuDTO oyuncuDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TAKIMID", oyuncuDTO.TAKIMID);
                    parameters.Add("@AD", oyuncuDTO.AD);
                    parameters.Add("@NUMARA", oyuncuDTO.NUMARA);
                    parameters.Add("@MEVKI", oyuncuDTO.MEVKI);
                    var DbResult = await connection.ExecuteAsync("INSERT INTO OYUNCU (TAKIMID,AD,NUMARA,MEVKI) VALUES (@TAKIMID,@AD,@NUMARA,@MEVKI);", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Oyuncu ekleme yapılamadı.", $"OyuncuRepository Add {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<object>> Update(OyuncuDTO oyuncuDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", oyuncuDTO.ID);
                    parameters.Add("@TAKIMID", oyuncuDTO.TAKIMID);
                    parameters.Add("@AD", oyuncuDTO.AD);
                    parameters.Add("@NUMARA", oyuncuDTO.NUMARA);
                    parameters.Add("@MEVKI", oyuncuDTO.MEVKI);
                    var DbResult = await connection.ExecuteAsync("UPDATE OYUNCU SET TAKIMID=@TAKIMID,AD=@AD,NUMARA=@NUMARA,MEVKI=@MEVKI WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Oyuncu güncelleme yapılamadı.", $"OyuncuRepository Update {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
    }
}
