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
    public class TarifeRepository : ITarifeRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public TarifeRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<TarifeDTO>> Get(TarifeDTO tarifeDTO)
        {
            MiddlewareResult<TarifeDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", tarifeDTO.ID);
                    parameters.Add("@KISAAD", tarifeDTO.KISAAD);
                    parameters.Add("@TARIFENO", tarifeDTO.TARIFENO);
                    parameters.Add("@UZUNAD", tarifeDTO.UZUNAD);
                    


                    var DbResult = await connection.QuerySingleOrDefaultAsync<TarifeDTO>("SELECT ID, KISAAD, TARIFENO, UZUNAD FROM TARIFE WHERE ( @ID IS NULL OR ID=@ID ) AND ( @KISAAD IS NULL OR KISAAD=@KISAAD ) AND ( @TARIFENO IS NULL OR TARIFENO=@TARIFENO ) AND ( @UZUNAD IS NULL OR UZUNAD=@UZUNAD );", parameters);
                    Result = new MiddlewareResult<TarifeDTO>(DbResult);
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<TarifeDTO>(false, "Tarife bilgisi alınmadı.", $"TarifeRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<TarifeDTO>>> GetList()
        {
            MiddlewareResult<List<TarifeDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<TarifeDTO>("SELECT * FROM TARIFE;");
                    Result = new MiddlewareResult<List<TarifeDTO>>(DbResult.ToList());
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<TarifeDTO>>(false, "Tarife liste bilgisi alınmadı.", $"TarifeRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(TarifeDTO tarifeDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@KISAAD", tarifeDTO.KISAAD);
                    parameters.Add("@TARIFENO", tarifeDTO.TARIFENO);
                    parameters.Add("@UZUNAD", tarifeDTO.UZUNAD);

                    var DbResult = await connection.ExecuteAsync("INSERT INTO TARIFE (KISAAD,TARIFENO,UZUNAD) VALUES (@KISAAD,@TARIFENO,@UZUNAD) ;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "tarife ekleme yapılamadı.", $"TarifeRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(TarifeDTO tarifeDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", tarifeDTO.ID);
                    parameters.Add("@KISAAD", tarifeDTO.KISAAD);
                    parameters.Add("@TARIFENO", tarifeDTO.TARIFENO);
                    parameters.Add("@UZUNAD", tarifeDTO.UZUNAD);

                    var DbResult = await connection.ExecuteAsync("UPDATE TARIFE SET KISAAD=@KISAAD,TARIFENO=@TARIFENO,UZUNAD=@UZUNAD WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Tarife güncelleme yapılamadı.", $"PoliceRepository Update {ex.GetErrorDetail()}");
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
