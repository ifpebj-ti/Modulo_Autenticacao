
using Microsoft.EntityFrameworkCore;
using ModuloAutenticacao.Api.Repository.Interface;
using System.Security.Cryptography;

namespace ModuloAutenticacao.Api.Repository.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DbContexto Contexto;

        public UsuarioRepository (DbContexto contexto)
        {
            Contexto = contexto;
        }

        
        public async Task<Usuario> SalvarUsuario(UsuarioDTO request)
        {
            CreatePasswordHash(request.senha, out byte[] senhaHash, out byte[] senhaSalt);
            
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
        private void CreatePasswordHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }
        
    }
}
