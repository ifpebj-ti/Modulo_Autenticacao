using ModuloAutenticacao.Api.Domain;
using ModuloAutenticacao.Api.DTOs;

namespace ModuloAutenticacao.Api.Repository.Interface;

    public interface IUsuarioRepository
    {
        Task<Usuario> SalvarUsuario(UsuarioDTO request);
        Task<Usuario> BuscarUsuarioPorEmail(string email);
        Task<Usuario> BuscarUsuarioPorMatricula(string matricula);
        Task<Usuario> BuscarFilialPorId (int id_filial);
        
        
    

    }

