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
    public class ZeylRepository : IZeylRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public ZeylRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<ZeylDTO>> Get(ZeylDTO zeylDTO)
        {
            MiddlewareResult<ZeylDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", zeylDTO.ID);
                    parameters.Add("@POLID", zeylDTO.POLID);
                    parameters.Add("@TARIFENO", zeylDTO.TARIFENO);
                    parameters.Add("@ZEYLNO", zeylDTO.ZEYLNO);
                    parameters.Add("@ZEYLTIP", zeylDTO.ZEYLTIP);


                    var DbResult = await connection.QuerySingleOrDefaultAsync<ZeylDTO>("SELECT ID, POLID, TARIFENO, ZEYLNO,ZEYLTIP FROM T_ZEYL WHERE ( @ID IS NULL OR ID=@ID ) AND ( @POLID IS NULL OR POLID=@POLID ) AND ( @TARIFENO IS NULL OR TARIFENO=@TARIFENO ) AND ( @ZEYLNO IS NULL OR ZEYLNO=@ZEYLNO ) AND ( @ZEYLTIP IS NULL OR ZEYLTIP=@ZEYLTIP );", parameters);
                    Result = new MiddlewareResult<ZeylDTO>(DbResult);
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<ZeylDTO>(false, "Zeyl bilgisi alınmadı.", $"ZeylRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<ZeylDTO>>> GetList()
        {
            MiddlewareResult<List<ZeylDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<ZeylDTO>("SELECT * FROM T_ZEYL;");
                    Result = new MiddlewareResult<List<ZeylDTO>>(DbResult.ToList());
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<ZeylDTO>>(false, "Zeyl liste bilgisi alınmadı.", $"ZeylRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(ZeylDTO zeylDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    
                    parameters.Add("@POLID", zeylDTO.POLID);
                    parameters.Add("@TARIFENO", zeylDTO.TARIFENO);
                    parameters.Add("@ZEYLNO", zeylDTO.ZEYLNO);
                    parameters.Add("@ZEYLTIP", zeylDTO.ZEYLTIP);

                    var DbResult = await connection.ExecuteAsync("INSERT INTO T_ZEYL (POLID,TARIFENO,ZEYLNO,ZEYLTIP) VALUES (@POLID,@TARIFENO,@ZEYLNO,@ZEYLTIP) ;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Zeyl ekleme yapılamadı.", $"ZeylRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(ZeylDTO zeylDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", zeylDTO.ID);

                    parameters.Add("@POLID", zeylDTO.POLID);
                    parameters.Add("@TARIFENO", zeylDTO.TARIFENO);
                    parameters.Add("@ZEYLNO", zeylDTO.ZEYLNO);
                    parameters.Add("@ZEYLTIP", zeylDTO.ZEYLTIP);

                    var DbResult = await connection.ExecuteAsync("UPDATE T_ZEYL SET POLID=@POLID,TARIFENO=@TARIFENO,ZEYLNO=@ZEYLNO,ZEYLTIP=@ZEYLTIP WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "ZEYL güncelleme yapılamadı.", $"ZeylRepository Update {ex.GetErrorDetail()}");
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








/*namespace Repositories
{
    public class ZeylRepository : IZeylRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        public ZeylRepository(MySqlConnection MySqlConnection)
        {
            _mySqlConnection = MySqlConnection;
        }
        public async Task<ZeylDTO> Get(ZeylDTO zeylDTO)
        {
            ZeylDTO Result = null;
            try
            {
                await _mySqlConnection.OpenAsync();

                using var command = new MySqlCommand("SELECT ID,POLID,ZEYLNO,TARIFENO,ZEYLTIP FROM T_ZEYL WHERE ( @ID IS NULL OR ID=@ID ) AND ( @POLID IS NULL OR POLID=@POLID ) AND ( @ZEYLNO IS NULL OR ZEYLNO=@ZEYLNO )AND ( @TARIFENO IS NULL OR TARIFENO=@TARIFENO )AND ( @ZEYLTIP IS NULL OR ZEYLTIP=@ZEYLTIP ) ;", _mySqlConnection);

                command.Parameters.Add(new MySqlParameter() { ParameterName = "@ID", Value = zeylDTO.ID, MySqlDbType = MySqlDbType.Int32 });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@POLID", Value = zeylDTO.POLID, MySqlDbType = MySqlDbType.VarChar });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@ZEYLNO", Value = zeylDTO.ZEYLNO, MySqlDbType = MySqlDbType.VarChar });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@TARIFENO", Value = zeylDTO.TARIFENO, MySqlDbType = MySqlDbType.VarChar });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@ZEYLTIP", Value = zeylDTO.ZEYLTIP, MySqlDbType = MySqlDbType.VarChar });

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Result = new ZeylDTO()
                    {

                        POLID = reader["POLID"]?.ToString(),
                        ZEYLNO = reader["ZEYLNO"]?.ToString(),
                        TARIFENO = reader["TARIFENO"]?.ToString(),
                        ZEYLTIP = reader["ZEYLTIP"]?.ToString()

                    };
                }

            }
            catch (System.Exception ex)
            {
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
        public async Task<List<ZeylDTO>> GetList()
        {
            List<ZeylDTO> Result = null;
            try
            {
                await _mySqlConnection.OpenAsync();

                using var command = new MySqlCommand("SELECT * FROM ZEYL;", _mySqlConnection);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (Result == null)
                    {
                        Result = new List<ZeylDTO>();
                    }

                    Result.Add(new ZeylDTO() { POLID = reader["POLID"]?.ToString(), ZEYLNO = reader["ZEYLNO"]?.ToString(), TARIFENO = reader["TARIFENO"]?.ToString(), ZEYLTIP = reader["ZEYLTIP"]?.ToString() });
                }

            }
            catch (System.Exception ex)
            {
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
        public async Task<ZeylDTO> Add(ZeylDTO zeylDTO)

        {
            ZeylDTO Result = null;
            try
            {
                await _mySqlConnection.OpenAsync();
                using var command = new MySqlCommand("INSERT INTO T_ZEYL VALUES (@ID,@ZEYLTIP,@ACIKLAMA) ;", _mySqlConnection);


                command.Parameters.Add(new MySqlParameter() { ParameterName = "@ID", Value = zeylDTO.ID, MySqlDbType = MySqlDbType.Int32 });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@POLID", Value = zeylDTO.POLID, MySqlDbType = MySqlDbType.VarChar });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@ZEYLNO", Value = zeylDTO.ZEYLNO, MySqlDbType = MySqlDbType.VarChar });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@TARIFENO", Value = zeylDTO.TARIFENO, MySqlDbType = MySqlDbType.VarChar });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "@ZEYLTIP", Value = zeylDTO.ZEYLTIP, MySqlDbType = MySqlDbType.VarChar });
            }
            catch (System.Exception)
            {
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
        public bool Update()
        {
            return false;
        }
        public bool Delete()
        {
            return false;
        }
    }
}
*/