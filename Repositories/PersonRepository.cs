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
    public class PersonRepository : IPersonRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public PersonRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<PersonDTO>> Get(PersonDTO personDTO)
        {
            MiddlewareResult<PersonDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@PID", personDTO.PID);
                    parameters.Add("@AD", personDTO.AD);
                    parameters.Add("@SOYAD", personDTO.SOYAD);
                    parameters.Add("@KIMLIKNO", personDTO.KIMLIKNO);
                    parameters.Add("@DOGUMYERI", personDTO.DOGUMYERI);
                    parameters.Add("@ULKEUYRUK", personDTO.ULKEUYRUK);
                    parameters.Add("@ID", personDTO.ID);
                    
                    
                    var DbResult = await connection.QuerySingleOrDefaultAsync<PersonDTO>("SELECT PID,AD,SOYAD,KIMLIKNO,DOGUMYERI,ULKEUYRUK,ID FROM T_PERSON WHERE ( @ID IS NULL OR ID=@ID ) AND ( @KIMLIKNO IS NULL OR KIMLIKNO=@KIMLIKNO ) AND ( @SOYAD IS NULL OR SOYAD=@SOYAD ) AND ( @ULKEUYRUK IS NULL OR ULKEUYRUK=@ULKEUYRUK ) AND ( @PID IS NULL OR PID=@PID ) AND ( @AD IS NULL OR AD=@AD) AND ( @DOGUMYERI IS NULL OR DOGUMYERI=@DOGUMYERI) ;", parameters);
                    Result = new MiddlewareResult<PersonDTO>(DbResult);
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<PersonDTO>(false, "Person bilgisi alınmadı.", $"PersonRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<PersonDTO>>> GetList()
        {
            MiddlewareResult<List<PersonDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<PersonDTO>("SELECT * FROM T_PERSON;");
                    Result = new MiddlewareResult<List<PersonDTO>>(DbResult.ToList());
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<PersonDTO>>(false, "Person liste bilgisi alınmadı.", $"PersonDTORepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(PersonDTO personDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    
                    parameters.Add("@KIMLIKNO", personDTO.KIMLIKNO);
                    parameters.Add("@PID", personDTO.PID);
                    parameters.Add("@SOYAD", personDTO.SOYAD);
                    parameters.Add("@AD", personDTO.AD);
                    parameters.Add("@DOGUMYERI", personDTO.DOGUMYERI);
                    parameters.Add("@ULKEUYRUK", personDTO.ULKEUYRUK);



                    var DbResult = await connection.ExecuteAsync("INSERT INTO T_PERSON (KIMLIKNO,PID,SOYAD,AD,DOGUMYERI,ULKEUYRUK) VALUES (@KIMLIKNO,@PID,@SOYAD,@AD,@DOGUMYERI,@ULKEUYRUK) ;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Person ekleme yapılamadı.", $"PersonRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(PersonDTO personDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", personDTO.ID);
                    parameters.Add("@KIMLIKNO", personDTO.KIMLIKNO);
                    parameters.Add("@PID", personDTO.PID);
                    parameters.Add("@SOYAD", personDTO.SOYAD);
                    parameters.Add("@AD", personDTO.AD);
                    parameters.Add("@DOGUMYERI", personDTO.DOGUMYERI);
                    parameters.Add("@ULKEUYRUK", personDTO.ULKEUYRUK);

                    var DbResult = await connection.ExecuteAsync("UPDATE T_PERSON SET KIMLIKNO=@KIMLIKNO,PID=@PID,SOYAD=@SOYAD,AD=@AD,DOGUMYERI=@DOGUMYERI, ULKEUYRUK=@ULKEUYRUK WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "PERSON güncelleme yapılamadı.", $"PersonRepository Update {ex.GetErrorDetail()}");
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
