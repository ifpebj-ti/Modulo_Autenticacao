namespace ModuloAutenticacao.Api.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> SalvarUsuario(CreateUsuarioDTO request);
        Task<Usuario> GetUserByEmail(string email);
        Task<Usuario> GetUserByMatricula(string matricula);

        bool VerifyPasswordHash(string senha, byte[] senhaHash, byte[] senhadSalt);
        
    

    }
}
