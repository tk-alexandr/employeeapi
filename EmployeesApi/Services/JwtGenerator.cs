using EmployeesApi.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeesApi.Services
{
    public class JwtGenerator
    {
        private readonly JwtHeader    _jwtHeader;
        private readonly IList<Claim> _jwtClaims;
        private readonly DateTime     _jwtDate;
        private readonly int          _tokenLifetimeInSeconds;

        public JwtGenerator(IOptions<JwtOptions> jwtOptions)
        {
            var credentials = new SigningCredentials(
                key: new SymmetricSecurityKey(
                    Encoding.UTF32.GetBytes(jwtOptions.Value.PrivateKey)
                ),
                algorithm: SecurityAlgorithms.HmacSha256
            );

            _jwtHeader              = new JwtHeader(credentials);
            _jwtClaims              = new List<Claim>();
            _jwtDate                = DateTime.UtcNow;
            _tokenLifetimeInSeconds = jwtOptions.Value.LifetimeInSeconds;
        }

        public JwtGenerator AddClaim(Claim claim)
        {
            _jwtClaims.Add(claim);
            return this;
        }

        public long GetTokenExpirationInUnixTime => new DateTimeOffset(
            _jwtDate.AddSeconds(_tokenLifetimeInSeconds)
        ).ToUnixTimeMilliseconds();

        public string GetToken()
        {
            var jwt = new JwtSecurityToken(_jwtHeader,
                                           new JwtPayload(
                                               audience: "employee_app",
                                               issuer: "employee_app",
                                               notBefore: _jwtDate,
                                               expires: _jwtDate.AddSeconds(_tokenLifetimeInSeconds),
                                               claims: _jwtClaims));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
