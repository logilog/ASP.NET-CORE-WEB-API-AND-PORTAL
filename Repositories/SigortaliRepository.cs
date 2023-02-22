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
    public class SigortaliRepository : ISigortaliRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public SigortaliRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<SigortaliDTO>> Get(SigortaliDTO sigortaliDTO)
        {
            MiddlewareResult<SigortaliDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", sigortaliDTO.ID);
                    parameters.Add("@AD", sigortaliDTO.AD);
                    parameters.Add("@PID", sigortaliDTO.PID);
                    parameters.Add("@POLID", sigortaliDTO.POLID);
                    parameters.Add("@SOYAD", sigortaliDTO.SOYAD);
                    parameters.Add("@ZEYLNO", sigortaliDTO.ZEYLNO);
                    



                    var DbResult = await connection.QuerySingleOrDefaultAsync<SigortaliDTO>("SELECT ID, AD,SOYAD,POLID,PID ,ZEYLNO FROM T_SIGORTALI WHERE ( @ID IS NULL OR ID=@ID ) AND ( @POLID IS NULL OR POLID=@POLID ) AND ( @PID IS NULL OR PID=@PID) AND ( @ZEYLNO IS NULL OR ZEYLNO =@ZEYLNO ) AND ( @AD IS NULL OR AD=@AD) AND ( @SOYAD IS NULL OR SOYAD =@SOYAD )  ;", parameters);
                    Result = new MiddlewareResult<SigortaliDTO>(DbResult);
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<SigortaliDTO>(false, "Sigortali bilgisi alınmadı.", $"SigortaliRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<SigortaliDTO>>> GetList()
        {
            MiddlewareResult<List<SigortaliDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<SigortaliDTO>("SELECT * FROM T_SIGORTALI;");
                    Result = new MiddlewareResult<List<SigortaliDTO>>(DbResult.ToList());
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<SigortaliDTO>>(false, "Sigortali liste bilgisi alınmadı.", $"SigortaliRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(SigortaliDTO sigortaliDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AD", sigortaliDTO.AD);
                    parameters.Add("@PID", sigortaliDTO.PID);
                    parameters.Add("@POLID", sigortaliDTO.POLID);
                    parameters.Add("@SOYAD", sigortaliDTO.SOYAD);
                    parameters.Add("@ZEYLNO", sigortaliDTO.ZEYLNO);

                    var DbResult = await connection.ExecuteAsync("INSERT INTO T_SIGORTALI (POLID,AD,PID,SOYAD,ZEYLNO) VALUES (@POLID,@AD,@PID,@SOYAD,@ZEYLNO) ;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Sigortali ekleme yapılamadı.", $"PoliceRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(SigortaliDTO sigortaliDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", sigortaliDTO.ID);
                    parameters.Add("@AD", sigortaliDTO.AD);
                    parameters.Add("@PID", sigortaliDTO.PID);
                    parameters.Add("@POLID", sigortaliDTO.POLID);
                    parameters.Add("@SOYAD", sigortaliDTO.SOYAD);
                    parameters.Add("@ZEYLNO", sigortaliDTO.ZEYLNO);

                    var DbResult = await connection.ExecuteAsync("UPDATE T_SIGORTALI SET POLID=@POLID,AD=@AD,PID=@PID,SOYAD=@SOYAD,ZEYLNO=@ZEYLNO WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Sigortali güncelleme yapılamadı.", $"SigortaliRepository Update {ex.GetErrorDetail()}");
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
