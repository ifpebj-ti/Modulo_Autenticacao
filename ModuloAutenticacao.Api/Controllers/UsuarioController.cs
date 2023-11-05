using Microsoft.AspNetCore.Mvc;
using ModuloAutenticacao.Api.Repository.Interface;


namespace ModuloAutenticacao.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsuarioController : ControllerBase
{
        
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    [Route("CadastrarUsuario")]
    public async Task<IActionResult> Post([FromBody] UsuarioDTO request)
    {


        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogWarning("Criando usuário....");
        try
        {

            Usuario usuario = await _usuarioRepository.SalvarUsuario(request);
            _logger.LogWarning("Usuário "+ usuario.nome + " criado, com email " + usuario.email + ".");
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest();
        }

    }

    

    
}