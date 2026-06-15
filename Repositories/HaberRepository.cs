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
    public class HaberRepository : IHaberRepository
    {
        private const string Cols = "ID, BASLIK, OZET, ICERIK, GORSEL, YAZAR, TARIH";

        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public HaberRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<HaberDTO>> Get(HaberDTO haberDTO)
        {
            MiddlewareResult<HaberDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", haberDTO.ID);
                    var DbResult = await connection.QuerySingleOrDefaultAsync<HaberDTO>($"SELECT {Cols} FROM HABER WHERE ( @ID IS NULL OR ID=@ID );", parameters);
                    Result = new MiddlewareResult<HaberDTO>(DbResult);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<HaberDTO>(false, "Haber bilgisi alınmadı.", $"HaberRepository Get {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<List<HaberDTO>>> GetList()
        {
            MiddlewareResult<List<HaberDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<HaberDTO>($"SELECT {Cols} FROM HABER ORDER BY TARIH DESC;");
                    Result = new MiddlewareResult<List<HaberDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<HaberDTO>>(false, "Haber listesi alınmadı.", $"HaberRepository GetList {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<object>> Add(HaberDTO haberDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@BASLIK", haberDTO.BASLIK);
                    parameters.Add("@OZET", haberDTO.OZET);
                    parameters.Add("@ICERIK", haberDTO.ICERIK);
                    parameters.Add("@GORSEL", haberDTO.GORSEL);
                    parameters.Add("@YAZAR", haberDTO.YAZAR);
                    parameters.Add("@TARIH", haberDTO.TARIH);
                    var DbResult = await connection.ExecuteAsync("INSERT INTO HABER (BASLIK,OZET,ICERIK,GORSEL,YAZAR,TARIH) VALUES (@BASLIK,@OZET,@ICERIK,@GORSEL,@YAZAR,COALESCE(@TARIH,NOW()));", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Haber ekleme yapılamadı.", $"HaberRepository Add {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
        public async Task<MiddlewareResult<object>> Update(HaberDTO haberDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", haberDTO.ID);
                    parameters.Add("@BASLIK", haberDTO.BASLIK);
                    parameters.Add("@OZET", haberDTO.OZET);
                    parameters.Add("@ICERIK", haberDTO.ICERIK);
                    parameters.Add("@GORSEL", haberDTO.GORSEL);
                    parameters.Add("@YAZAR", haberDTO.YAZAR);
                    var DbResult = await connection.ExecuteAsync("UPDATE HABER SET BASLIK=@BASLIK,OZET=@OZET,ICERIK=@ICERIK,GORSEL=@GORSEL,YAZAR=@YAZAR WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Haber güncelleme yapılamadı.", $"HaberRepository Update {ex.GetErrorDetail()}");
            }
            finally
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open) await _mySqlConnection.CloseAsync();
            }
            return Result;
        }
    }
}
