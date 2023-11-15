namespace ModuloAutenticacao.Api.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> SalvarUsuario(CreateUsuarioDTO request);
        Task<Usuario> GetUserByEmail(string email);
        Task<Usuario> GetUserByMatricula(string matricula);

        bool VerificarHashSenha(string senha, byte[] senhaHash, byte[] senhadSalt);
        public string CreateToken(Usuario usuario);
        
    

    }
}
