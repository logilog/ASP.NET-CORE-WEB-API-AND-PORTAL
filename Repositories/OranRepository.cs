using Dapper;
using Entities.General;
using Entities.REPOSITORY;
using Helpers.Extensions;
using MySqlConnector;
using Repositories.Interfaces;
using System.Threading.Tasks;

namespace Repositories
{
    public class OranRepository : IOranRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public OranRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<OranDTO>> GetByMac(int macId)
        {
            MiddlewareResult<OranDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MACID", macId);
                    var DbResult = await connection.QuerySingleOrDefaultAsync<OranDTO>(
                        "SELECT ID, MACID, MS1, MSX, MS2, ALT25, UST25, KGVAR, KGYOK FROM ORAN WHERE MACID=@MACID;", parameters);
                    Result = new MiddlewareResult<OranDTO>(DbResult);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<OranDTO>(false, "Oran bilgisi alınmadı.", $"OranRepository GetByMac {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<object>> Kaydet(OranDTO oranDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MACID", oranDTO.MACID);
                    parameters.Add("@MS1", oranDTO.MS1);
                    parameters.Add("@MSX", oranDTO.MSX);
                    parameters.Add("@MS2", oranDTO.MS2);
                    parameters.Add("@ALT25", oranDTO.ALT25);
                    parameters.Add("@UST25", oranDTO.UST25);
                    parameters.Add("@KGVAR", oranDTO.KGVAR);
                    parameters.Add("@KGYOK", oranDTO.KGYOK);
                    var DbResult = await connection.ExecuteAsync(
                        "INSERT INTO ORAN (MACID,MS1,MSX,MS2,ALT25,UST25,KGVAR,KGYOK) VALUES (@MACID,@MS1,@MSX,@MS2,@ALT25,@UST25,@KGVAR,@KGYOK) " +
                        "ON DUPLICATE KEY UPDATE MS1=@MS1,MSX=@MSX,MS2=@MS2,ALT25=@ALT25,UST25=@UST25,KGVAR=@KGVAR,KGYOK=@KGYOK;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Oran kaydedilemedi.", $"OranRepository Kaydet {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
    }
}
