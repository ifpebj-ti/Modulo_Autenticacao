
using Microsoft.EntityFrameworkCore;
using ModuloAutenticacao.Api.Repository.Interface;

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
            
            var usuario = new Usuario
            {
                matricula = request.matricula,
                nome = request.nome,
                nivel_de_acesso = request.nivel_de_acesso,
                email = request.email,
                senha = request.senha,
                id_filial = request.id_filial,
                celular = request.celular,
                data_nascimento = request.data_nascimento,
                data_admissão = request.data_admissão,
                status = true,
            
            };
            await Contexto.Usuario.AddAsync(usuario);
            await Contexto.SaveChangesAsync();
            return usuario;
        }
        
    }
}
