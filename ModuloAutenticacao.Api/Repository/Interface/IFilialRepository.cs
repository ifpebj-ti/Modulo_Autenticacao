using ModuloAutenticacao.Api.Domain;
using ModuloAutenticacao.Api.DTOs;

namespace ModuloAutenticacao.Api.Repository.Interface;

    public interface IFilialRepository
    {
        Task<Filial> SalvarFilial(FilialDTO request);
        Task<Filial> BuscarFilialPorId (int id_filial);
        Task<Filial> BuscarFilialPorNome (string nome);
        Task<Filial> BuscarFilialPorCNPJ (string cnpj);
        
    }

