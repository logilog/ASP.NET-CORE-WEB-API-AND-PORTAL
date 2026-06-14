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
    public class TakimRepository : ITakimRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public TakimRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<TakimDTO>> Get(TakimDTO takimDTO)
        {
            MiddlewareResult<TakimDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", takimDTO.ID);
                    parameters.Add("@KISAKOD", takimDTO.KISAKOD);

                    var DbResult = await connection.QuerySingleOrDefaultAsync<TakimDTO>("SELECT ID, AD, KISAKOD, GRUP, KITA, BAYRAK FROM TAKIM WHERE ( @ID IS NULL OR ID=@ID ) AND ( @KISAKOD IS NULL OR KISAKOD=@KISAKOD );", parameters);
                    Result = new MiddlewareResult<TakimDTO>(DbResult);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<TakimDTO>(false, "Takım bilgisi alınmadı.", $"TakimRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<TakimDTO>>> GetList()
        {
            MiddlewareResult<List<TakimDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<TakimDTO>("SELECT ID, AD, KISAKOD, GRUP, KITA, BAYRAK FROM TAKIM ORDER BY GRUP, AD;");
                    Result = new MiddlewareResult<List<TakimDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<TakimDTO>>(false, "Takım liste bilgisi alınmadı.", $"TakimRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(TakimDTO takimDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AD", takimDTO.AD);
                    parameters.Add("@KISAKOD", takimDTO.KISAKOD);
                    parameters.Add("@GRUP", takimDTO.GRUP);
                    parameters.Add("@KITA", takimDTO.KITA);
                    parameters.Add("@BAYRAK", takimDTO.BAYRAK);

                    var DbResult = await connection.ExecuteAsync("INSERT INTO TAKIM (AD,KISAKOD,GRUP,KITA,BAYRAK) VALUES (@AD,@KISAKOD,@GRUP,@KITA,@BAYRAK);", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Takım ekleme yapılamadı.", $"TakimRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(TakimDTO takimDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", takimDTO.ID);
                    parameters.Add("@AD", takimDTO.AD);
                    parameters.Add("@KISAKOD", takimDTO.KISAKOD);
                    parameters.Add("@GRUP", takimDTO.GRUP);
                    parameters.Add("@KITA", takimDTO.KITA);
                    parameters.Add("@BAYRAK", takimDTO.BAYRAK);

                    var DbResult = await connection.ExecuteAsync("UPDATE TAKIM SET AD=@AD,KISAKOD=@KISAKOD,GRUP=@GRUP,KITA=@KITA,BAYRAK=@BAYRAK WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Takım güncelleme yapılamadı.", $"TakimRepository Update {ex.GetErrorDetail()}");
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
