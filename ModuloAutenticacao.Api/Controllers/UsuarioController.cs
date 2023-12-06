using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModuloAutenticacao.Api.Domain;
using ModuloAutenticacao.Api.DTOs;
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
    private  readonly IFilialRepository _filialRepository;


    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository, IAutenticacaoService autenticacaoService, IFilialRepository filialRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
        _autenticacaoService = autenticacaoService;
        _filialRepository = filialRepository;
    
    }

    [HttpPost, Authorize]
    [Route("CadastrarUsuario")]
    public async Task<IActionResult> Post([FromBody] UsuarioDTO request)
    {


        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogWarning("Criando usuário....");
        try
        {
            // Verificando se já existe usuário com o email fornecido
            var existeEmail = await _usuarioRepository.BuscarUsuarioPorEmail(request.email);
            if (existeEmail != null)
            {
                return Conflict("Usuário com o email fornecido já existe.");
            }

            // Verificando se já existe usuário com a matricula fornecida
            var existeMatricula = await _usuarioRepository.BuscarUsuarioPorMatricula(request.matricula);
            if (existeMatricula != null)
            {
                return Conflict("Usuário com a matrícula fornecida já existe.");
            }

            //Verificando se filial fornecida existe
            var existeFilial = await _filialRepository.BuscarFilialPorId(request.id_filial);
            if (existeFilial == null)
            {
                return Conflict("ID de filial não existe.");
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
    
    [HttpPost("FazerLogin")]
    public async Task<ActionResult<string>> Login(LoginDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if(request.email == "" || request.senha == "" ) return BadRequest("Os campos de email e senha preecisam ser preenchidos.");

        _logger.LogWarning(string.Format("{0} efetuando login...", request.email ));

        try
        {
            Usuario usuario = await _usuarioRepository.BuscarUsuarioPorEmail(request.email);
            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(request.senha, usuario.senhaHash);

            if (usuario == null || !senhaCorreta)
            {
                return BadRequest("Usuário ou inválido.");
            }

            string Token = _autenticacaoService.CriarToken(usuario);
            _logger.LogWarning("Usuário {0} logando...", usuario.email);
            return StatusCode(200, Token);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex);
        }
        


    

        

    }

    

    

    
}