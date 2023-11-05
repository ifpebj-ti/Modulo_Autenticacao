namespace ModuloAutenticacao.Api.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> SalvarUsuario(UsuarioDTO request);
    

    }
}
