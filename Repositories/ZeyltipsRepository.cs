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
    public class ZeyltipsRepository : IZeyltipsRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public ZeyltipsRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<ZeyltipsDTO>> Get(ZeyltipsDTO zeyltipsDTO)
        {
            MiddlewareResult<ZeyltipsDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", zeyltipsDTO.ID);
                    parameters.Add("@ACIKLAMA", zeyltipsDTO.ACIKLAMA);
                    parameters.Add("@ZEYLTIP", zeyltipsDTO.ZEYLTIP);

                    var DbResult = await connection.QuerySingleOrDefaultAsync<ZeyltipsDTO>("SELECT ID, ACIKLAMA, ZEYLTIP FROM ZEYLTIPS WHERE ( @ID IS NULL OR ID=@ID ) AND ( @ACIKLAMA IS NULL OR ACIKLAMA=@ACIKLAMA ) AND ( @ZEYLTIP IS NULL OR ZEYLTIP=@ZEYLTIP ) ;", parameters);
                    Result = new MiddlewareResult<ZeyltipsDTO>(DbResult);
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<ZeyltipsDTO>(false, "Zeyltips bilgisi alınmadı.", $"ZeyltipsRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<ZeyltipsDTO>>> GetList()
        {
            MiddlewareResult<List<ZeyltipsDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<ZeyltipsDTO>("SELECT * FROM ZEYLTIPS;");
                    Result = new MiddlewareResult<List<ZeyltipsDTO>>(DbResult.ToList());
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<ZeyltipsDTO>>(false, "Zeyltips liste bilgisi alınmadı.", $"ZeyltipsRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(ZeyltipsDTO zeyltipsDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ACIKLAMA", zeyltipsDTO.ACIKLAMA);
                    parameters.Add("@ZEYLTIP", zeyltipsDTO.ZEYLTIP);

                    var DbResult = await connection.ExecuteAsync("INSERT INTO ZEYLTIPS (ACIKLAMA,ZEYLTIP) VALUES (@ACIKLAMA,@ZEYLTIP) ;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Zeyltips ekleme yapılamadı.", $"PoliceRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(ZeyltipsDTO zeyltipsDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", zeyltipsDTO.ID);
                    parameters.Add("@ACIKLAMA", zeyltipsDTO.ACIKLAMA);
                    parameters.Add("@ZEYLTIP", zeyltipsDTO.ZEYLTIP);

                    var DbResult = await connection.ExecuteAsync("UPDATE ZEYLTIPS SET ACIKLAMA=@ACIKLAMA,ZEYLTIP=@ZEYLTIP WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "ZEYLTIP güncelleme yapılamadı.", $"ZeyltipsRepository Update {ex.GetErrorDetail()}");
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
