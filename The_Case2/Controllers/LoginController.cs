using AutoMapper;
using Business.Interfaces;
using Entities.API;
using Entities.BUSINESS;
using Entities.CONFS;
using Entities.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace The_Case2.Controllers
{
    public class LoginController : The_Case2BaseController
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly JwtOptions _jwtOptions;
        private readonly IMapper _mapper;
        public LoginController(
            IKullaniciService KullaniciService,
            IOptions<JwtOptions> JwtOptions,
            IMapper Mapper
            )
        {

            _kullaniciService = KullaniciService;
            _jwtOptions = JwtOptions.Value;
            _mapper = Mapper;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<LoginResult>> Token([FromBody] LoginInput LoginInputs)
        {
            ResultModel<LoginResult> ApiResult;
            var KullaniciModel = _mapper.Map<kullanici>(LoginInputs);
            ResultModel<kullanici> Result = await _kullaniciService.Get(KullaniciModel);
            if (Result.Success)
            {
                var UserClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,Result.Data.AD),
                    new Claim(ClaimTypes.Surname,Result.Data.SOYAD),
                    new Claim("UserName",Result.Data.KULLANICIADI)
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: UserClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                ApiResult = new ResultModel<LoginResult>(new LoginResult() { Token = tokenString });
            }
            else
            {
                ApiResult = new ResultModel<LoginResult>(false, Result.Message);
            }

            return ApiResult;
        }
    }
}