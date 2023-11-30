using Microsoft.AspNetCore.Mvc;
using ModuloAutenticacao.Api.Domain;
using ModuloAutenticacao.Api.DTOs;
using ModuloAutenticacao.Api.Repository.Interface;


namespace ModuloAutenticacao.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class FilialController : ControllerBase
{
        
    private readonly ILogger<FilialController> _logger;
    private readonly IFilialRepository _filialRepository;


    public FilialController(ILogger<FilialController> logger, IFilialRepository filialRepository)
    {
        _logger = logger;
        _filialRepository = filialRepository;
    
    }

    [HttpPost]
    [Route("CriarFilial")]
    public async Task<IActionResult> Post([FromBody] FilialDTO request)
    {


        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogWarning("Criando filial....");
        try
        {

            var nomeFilialExiste = await _filialRepository.BuscarFilialPorNome(request.nome);
            if (nomeFilialExiste != null)
            {
                return Conflict("Nome da filial já existe.");
            }

            var cnpjFilialExiste = await _filialRepository.BuscarFilialPorCNPJ(request.cnpj);
    
            if (cnpjFilialExiste != null)
            {
                return Conflict("Já existe uma filial com esse CNPJ.");
            }

            Filial filial = await _filialRepository.SalvarFilial(request);
            _logger.LogWarning("Filial "+ filial.nome + " criada.");

            
            return StatusCode(201, filial);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest();
        }
    }
    
}