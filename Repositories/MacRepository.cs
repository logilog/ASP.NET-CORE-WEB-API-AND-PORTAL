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
    public class MacRepository : IMacRepository
    {
        private const string SelectBase =
            "SELECT m.ID, m.EVSAHIBIID, m.DEPLASMANID, e.AD AS EVSAHIBIAD, d.AD AS DEPLASMANAD, " +
            "e.BAYRAK AS EVSAHIBIBAYRAK, d.BAYRAK AS DEPLASMANBAYRAK, m.TARIH, m.GRUP, m.ASAMA, " +
            "m.STADYUM, m.SEHIR, m.EVSAHIBISKOR, m.DEPLASMANSKOR, m.DURUM " +
            "FROM MAC m JOIN TAKIM e ON e.ID = m.EVSAHIBIID JOIN TAKIM d ON d.ID = m.DEPLASMANID ";

        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public MacRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<MacDTO>> Get(MacDTO macDTO)
        {
            MiddlewareResult<MacDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", macDTO.ID);

                    var DbResult = await connection.QuerySingleOrDefaultAsync<MacDTO>(SelectBase + "WHERE ( @ID IS NULL OR m.ID=@ID );", parameters);
                    Result = new MiddlewareResult<MacDTO>(DbResult);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<MacDTO>(false, "Maç bilgisi alınmadı.", $"MacRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<MacDTO>>> GetList()
        {
            MiddlewareResult<List<MacDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<MacDTO>(SelectBase + "ORDER BY m.TARIH;");
                    Result = new MiddlewareResult<List<MacDTO>>(DbResult.ToList());
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<MacDTO>>(false, "Maç liste bilgisi alınmadı.", $"MacRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(MacDTO macDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@EVSAHIBIID", macDTO.EVSAHIBIID);
                    parameters.Add("@DEPLASMANID", macDTO.DEPLASMANID);
                    parameters.Add("@TARIH", macDTO.TARIH);
                    parameters.Add("@GRUP", macDTO.GRUP);
                    parameters.Add("@ASAMA", macDTO.ASAMA);
                    parameters.Add("@STADYUM", macDTO.STADYUM);
                    parameters.Add("@SEHIR", macDTO.SEHIR);
                    parameters.Add("@DURUM", macDTO.DURUM);

                    var DbResult = await connection.ExecuteAsync("INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM) VALUES (@EVSAHIBIID,@DEPLASMANID,@TARIH,@GRUP,@ASAMA,@STADYUM,@SEHIR,COALESCE(@DURUM,'BEKLIYOR'));", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Maç ekleme yapılamadı.", $"MacRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(MacDTO macDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", macDTO.ID);
                    parameters.Add("@EVSAHIBIID", macDTO.EVSAHIBIID);
                    parameters.Add("@DEPLASMANID", macDTO.DEPLASMANID);
                    parameters.Add("@TARIH", macDTO.TARIH);
                    parameters.Add("@GRUP", macDTO.GRUP);
                    parameters.Add("@ASAMA", macDTO.ASAMA);
                    parameters.Add("@STADYUM", macDTO.STADYUM);
                    parameters.Add("@SEHIR", macDTO.SEHIR);
                    parameters.Add("@DURUM", macDTO.DURUM);

                    var DbResult = await connection.ExecuteAsync("UPDATE MAC SET EVSAHIBIID=@EVSAHIBIID,DEPLASMANID=@DEPLASMANID,TARIH=@TARIH,GRUP=@GRUP,ASAMA=@ASAMA,STADYUM=@STADYUM,SEHIR=@SEHIR,DURUM=@DURUM WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Maç güncelleme yapılamadı.", $"MacRepository Update {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> SonucGuncelle(MacDTO macDTO)
        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", macDTO.ID);
                    parameters.Add("@EVSAHIBISKOR", macDTO.EVSAHIBISKOR);
                    parameters.Add("@DEPLASMANSKOR", macDTO.DEPLASMANSKOR);

                    var DbResult = await connection.ExecuteAsync("UPDATE MAC SET EVSAHIBISKOR=@EVSAHIBISKOR,DEPLASMANSKOR=@DEPLASMANSKOR,DURUM='BITTI' WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Maç sonucu güncellenemedi.", $"MacRepository SonucGuncelle {ex.GetErrorDetail()}");
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
