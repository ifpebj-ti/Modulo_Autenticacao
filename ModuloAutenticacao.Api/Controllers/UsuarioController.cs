using Microsoft.AspNetCore.Mvc;
using ModuloAutenticacao.Api.Repository.Interface;
using ModuloAutenticacao.Api.Services.Interface;


namespace ModuloAutenticacao.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsuarioController : ControllerBase
{
        
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    private  readonly IAutenticacaoService _autenticacaoService;


    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository, IAutenticacaoService autenticacaoService)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
        _autenticacaoService = autenticacaoService;
    
    }

    [HttpPost]
    [Route("CadastrarUsuario")]
    public async Task<IActionResult> Post([FromBody] CreateUsuarioDTO request)
    {


        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogWarning("Criando usuário....");
        try
        {
            // Check if the user with the given email already exists
            var existingUserByEmail = await _usuarioRepository.GetUserByEmail(request.email);
            if (existingUserByEmail != null)
            {
                return Conflict("Usuário com o email fornecido já existe.");
            }

            // Check if the user with the given matricula already exists
            var existingUserByMatricula = await _usuarioRepository.GetUserByMatricula(request.matricula);
            if (existingUserByMatricula != null)
            {
                return Conflict("Usuário com a matrícula fornecida já existe.");
            }

            Usuario usuario = await _usuarioRepository.SalvarUsuario(request);
            _logger.LogWarning("Usuário "+ usuario.nome + " criado, com email " + usuario.email + ".");
            
            return StatusCode(201, usuario);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest();
        }

    }
    
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDTO request)
    {
        var usuario = await _usuarioRepository.GetUserByEmail(request.email);
        bool senhaCorreta = BCrypt.Net.BCrypt.Verify(request.senha, usuario.senhaHash);


        if (usuario == null || !senhaCorreta)
        {
            return BadRequest("User email or password invalidate.");
        }

        string Token = _autenticacaoService.CreateToken(usuario);

        return StatusCode(200, Token);
    }

    

    

    
}