using Microsoft.EntityFrameworkCore;
using ModuloAutenticacao.Api.Domain;
using ModuloAutenticacao.Api.DTOs;
using ModuloAutenticacao.Api.Repository.Interface;

namespace ModuloAutenticacao.Api.Repository.Implementation;

    public class FilialRepository : IFilialRepository
    {

        private readonly DbContexto Contexto;


        public FilialRepository (DbContexto contexto)
        {
            Contexto = contexto;
        }

        public async Task<Filial> SalvarFilial(FilialDTO request)
        {
           
            
            var filial = new Filial
            {
                nome = request.nome,
                id_endereco = request.id_endereco,
                email = request.email,
                celular = request.celular,
                telefone_fixo = request.telefone_fixo,
                cnpj = request.cnpj
    
            };
            await Contexto.Filial.AddAsync(filial);
            await Contexto.SaveChangesAsync();
            return filial;
        }

        public async Task<Filial> BuscarFilialPorId (int id_filial)
        {
            return await Contexto.Filial.FirstOrDefaultAsync(u => u.id_filial == id_filial);
        }

        public async Task<Filial> BuscarFilialPorCNPJ (string cnpj)
        {
            return await Contexto.Filial.FirstOrDefaultAsync(u => u.cnpj == cnpj);
        }
        public async Task<Filial> BuscarFilialPorNome (string nome)
        {
            return await Contexto.Filial.FirstOrDefaultAsync(u => u.nome == nome);
        }


        
    }

