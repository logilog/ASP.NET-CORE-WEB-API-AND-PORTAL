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
    public class PoliceRepository : IPoliceRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public PoliceRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<PoliceDTO>> Get(PoliceDTO policeDTO)
        {
            MiddlewareResult<PoliceDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", policeDTO.ID);
                    parameters.Add("@POLID", policeDTO.POLID);
                    parameters.Add("@SONZEYLNO", policeDTO.SONZEYLNO);

                    var DbResult = await connection.QuerySingleOrDefaultAsync<PoliceDTO>("SELECT ID, POLID, SONZEYLNO, BRANSKOD FROM T_POLICE WHERE ( @ID IS NULL OR ID=@ID ) AND ( @POLID IS NULL OR POLID=@POLID ) AND ( @SONZEYLNO IS NULL OR SONZEYLNO=@SONZEYLNO ) ;", parameters);
                    Result = new MiddlewareResult<PoliceDTO>(DbResult);
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<PoliceDTO>(false, "Police bilgisi alınmadı.", $"PoliceRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<PoliceDTO>>> GetList()
        {
            MiddlewareResult<List<PoliceDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<PoliceDTO>("SELECT * FROM T_POLICE;");
                    Result = new MiddlewareResult<List<PoliceDTO>>(DbResult.ToList());
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<PoliceDTO>>(false, "Police liste bilgisi alınmadı.", $"PoliceRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(PoliceDTO policeDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@POLID", policeDTO.POLID);
                    parameters.Add("@SONZEYLNO", policeDTO.SONZEYLNO);
                    parameters.Add("@BRANSKOD", policeDTO.BRANSKOD);

                    var DbResult = await connection.ExecuteAsync("INSERT INTO T_POLICE (POLID,SONZEYLNO,BRANSKOD) VALUES (@POLID,@SONZEYLNO,@BRANSKOD) ;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Poliçe ekleme yapılamadı.", $"PoliceRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(PoliceDTO policeDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", policeDTO.ID);
                    parameters.Add("@POLID", policeDTO.POLID);
                    parameters.Add("@SONZEYLNO", policeDTO.SONZEYLNO);
                    parameters.Add("@BRANSKOD", policeDTO.BRANSKOD);

                    var DbResult = await connection.ExecuteAsync("UPDATE T_POLICE SET POLID=@POLID,SONZEYLNO=@SONZEYLNO,BRANSKOD=@BRANSKOD WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Poliçe güncelleme yapılamadı.", $"PoliceRepository Update {ex.GetErrorDetail()}");
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
        public bool Delete()
        {
            return false;
        }
    }
}
