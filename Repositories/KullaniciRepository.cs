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
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly RepositoryContext _repositoryContext;
        public KullaniciRepository(MySqlConnection MySqlConnection, RepositoryContext RepositoryContext)     
        {
            _mySqlConnection = MySqlConnection;
            _repositoryContext = RepositoryContext;
        }
        public async Task<MiddlewareResult<KullaniciDTO>> Get(KullaniciDTO kullaniciDTO)
        {
            MiddlewareResult<KullaniciDTO> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", kullaniciDTO.ID);
                    parameters.Add("@AD", kullaniciDTO.AD);
                    parameters.Add("@SIFRE", kullaniciDTO.SIFRE);
                    parameters.Add("@KULLANICIADI", kullaniciDTO.KULLANICIADI);
                    parameters.Add("@SOYAD", kullaniciDTO.SOYAD);
                    parameters.Add("@AKTIF", kullaniciDTO.AKTIF);
                    parameters.Add("@ROL", kullaniciDTO.ROL);





                    var DbResult = await connection.QuerySingleOrDefaultAsync<KullaniciDTO>("SELECT ID, AD, SIFRE, KULLANICIADI, SOYAD, AKTIF, ROL FROM T_KULLANICI WHERE ( @ID IS NULL OR ID=@ID ) AND ( @AD IS NULL OR AD=@AD ) AND ( @SIFRE IS NULL OR SIFRE=@SIFRE ) AND ( @KULLANICIADI IS NULL OR KULLANICIADI=@KULLANICIADI ) AND ( @SOYAD IS NULL OR SOYAD=@SOYAD ) AND ( @AKTIF IS NULL OR AKTIF=@AKTIF ) AND ( @ROL IS NULL OR ROL=@ROL ) ;", parameters);
                    Result = new MiddlewareResult<KullaniciDTO>(DbResult);
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<KullaniciDTO>(false, "Kullanici bilgisi alınmadı.", $"KullaniciRepository Get {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<List<KullaniciDTO>>> GetList()
        {
            MiddlewareResult<List<KullaniciDTO>> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var DbResult = await connection.QueryAsync<KullaniciDTO>("SELECT * FROM T_KULLANICI;");
                    Result = new MiddlewareResult<List<KullaniciDTO>>(DbResult.ToList());
                }

            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<List<KullaniciDTO>>(false, "Kullanici liste bilgisi alınmadı.", $"KullaniciRepository GetList {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Add(KullaniciDTO kullaniciDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@KULLANICIADI", kullaniciDTO.KULLANICIADI);
                    parameters.Add("@SIFRE", kullaniciDTO.SIFRE);
                    parameters.Add("@SOYAD", kullaniciDTO.SOYAD);
                    parameters.Add("@AD", kullaniciDTO.AD);
                    parameters.Add("@AKTIF", kullaniciDTO.AKTIF);
                    parameters.Add("@ROL", kullaniciDTO.ROL);



                    var DbResult = await connection.ExecuteAsync("INSERT INTO T_KULLANICI (KULLANICIADI,SIFRE,SOYAD,AD,AKTIF,ROL) VALUES (@KULLANICIADI,@SIFRE,@SOYAD,@AD,@AKTIF,@ROL) ;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Kullanici ekleme yapılamadı.", $"KullaniciRepository Add {ex.GetErrorDetail()}");
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
        public async Task<MiddlewareResult<object>> Update(KullaniciDTO kullaniciDTO)

        {
            MiddlewareResult<object> Result = null;
            try
            {
                using (var connection = _repositoryContext.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", kullaniciDTO.ID);
                    parameters.Add("@KULLANICIADI", kullaniciDTO.KULLANICIADI);
                    parameters.Add("@SIFRE", kullaniciDTO.SIFRE);
                    parameters.Add("@SOYAD", kullaniciDTO.SOYAD);
                    parameters.Add("@AD", kullaniciDTO.AD);
                    parameters.Add("@AKTIF", kullaniciDTO.AKTIF);
                    parameters.Add("@ROL", kullaniciDTO.ROL);



                    var DbResult = await connection.ExecuteAsync("UPDATE T_KULLANICI SET KULLANICIADI=@KULLANICIADI,SIFRE=@SIFRE,SOYAD=@SOYAD,AD=@AD,AKTIF=@AKTIF,ROL=@ROL WHERE ID=@ID;", parameters);
                    Result = new MiddlewareResult<object>(DbResult > 0, true);
                }
            }
            catch (System.Exception ex)
            {
                Result = new MiddlewareResult<object>(false, "Kullanici güncelleme yapılamadı.", $"KullaniciRepository Update {ex.GetErrorDetail()}");
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
