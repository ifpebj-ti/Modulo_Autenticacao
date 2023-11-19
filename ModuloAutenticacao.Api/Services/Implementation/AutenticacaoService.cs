using Microsoft.IdentityModel.Tokens;
using ModuloAutenticacao.Api.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace ModuloAutenticacao.Api.Services.AutenticacaoService
{
    public class AutenticacaoService : IAutenticacaoService
    {

        private readonly IConfiguration _configuration;


        public AutenticacaoService (IConfiguration configuration)
        {
            _configuration = configuration;
        }

    
        public string CriarToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", usuario.email.ToString()),
                new Claim("nivel_acesso", usuario.nivel_de_acesso.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        
    }
}
