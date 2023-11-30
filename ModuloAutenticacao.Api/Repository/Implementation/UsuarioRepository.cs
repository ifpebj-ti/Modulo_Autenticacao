using Microsoft.EntityFrameworkCore;
using ModuloAutenticacao.Api.Domain;
using ModuloAutenticacao.Api.DTOs;
using ModuloAutenticacao.Api.Repository.Interface;

namespace ModuloAutenticacao.Api.Repository.Implementation;

    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DbContexto Contexto;


        public UsuarioRepository (DbContexto contexto)
        {
            Contexto = contexto;
        }

        
        public async Task<Usuario> SalvarUsuario(UsuarioDTO request)
        {
            string senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senha);
            string senhaSalt = BCrypt.Net.BCrypt.GenerateSalt();
            
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
    
        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            return await Contexto.Usuario.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<Usuario> BuscarUsuarioPorMatricula(string matricula)
        {
            return await Contexto.Usuario.FirstOrDefaultAsync(u => u.matricula == matricula);
        }

        public async Task<Usuario> BuscarFilialPorId (int id_filial)
        {
            return await Contexto.Usuario.FirstOrDefaultAsync(u => u.id_filial == id_filial);
        }


        
    }

