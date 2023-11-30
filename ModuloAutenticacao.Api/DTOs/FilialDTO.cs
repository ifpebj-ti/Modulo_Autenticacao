namespace ModuloAutenticacao.Api.DTOs;
public record FilialDTO(
    string nome,
    int id_endereco,
    string email,
    string celular,
    string telefone_fixo,
    string cnpj

);