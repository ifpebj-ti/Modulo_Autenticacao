namespace ModuloAutenticacao.Api.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> SalvarUsuario(UsuarioDTO request);
        Task<Usuario> GetUserByEmail(string email);
        Task<Usuario> GetUserByMatricula(string matricula);
    

    }
}
