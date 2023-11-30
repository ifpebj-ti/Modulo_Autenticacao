using ModuloAutenticacao.Api.Domain;

namespace ModuloAutenticacao.Api.Services.Interface;

    public interface IAutenticacaoService
    {
        
        string CriarToken(Usuario usuario);
    }

