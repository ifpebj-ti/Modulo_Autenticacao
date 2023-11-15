
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModuloAutenticacao.Api.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ModuloAutenticacao.Api.Repository.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DbContexto Contexto;
        private readonly IConfiguration _configuration;


        public UsuarioRepository (DbContexto contexto, IConfiguration configuration)
        {
            Contexto = contexto;
            _configuration = configuration;
        }

        
        public async Task<Usuario> SalvarUsuario(CreateUsuarioDTO request)
        {
            CriarHashSenha(request.senha, out byte[] senhaHash, out byte[] senhaSalt);
            
            var usuario = new Usuario
            {
                matricula = request.matricula,
                nome = request.nome,
                nivel_de_acesso = request.nivel_de_acesso,
                email = request.email,
                id_filial = request.id_filial,
                celular = request.celular,
                data_nascimento = request.data_nascimento,
                data_admissão = request.data_admissão,
                status = true,
                senhaHash = senhaHash,
                senhaSalt = senhaSalt
            
            };
            await Contexto.Usuario.AddAsync(usuario);
            await Contexto.SaveChangesAsync();
            return usuario;
        }
        private void CriarHashSenha(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificarHashSenha(string senha, byte[] senhaHash, byte[] senhadSalt)
        {
            using (var hmac = new HMACSHA512(senhadSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);
            }
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await Contexto.Usuario.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<Usuario> GetUserByMatricula(string matricula)
        {
            return await Contexto.Usuario.FirstOrDefaultAsync(u => u.matricula == matricula);
        }

        public string CreateToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, usuario.email),
                new Claim(ClaimTypes.Role, usuario.nivel_de_acesso)
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
