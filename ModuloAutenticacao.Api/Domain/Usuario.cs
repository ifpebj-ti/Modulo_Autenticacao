using System.ComponentModel.DataAnnotations;

namespace ModuloAutenticacao.Api.Domain;
public class Usuario
{

    [Required]
    [Key]
    public int id_usuario { get; set; }

    [Required]
    [MaxLength(50)]
    public string nome { get; set; }

    [Required]
    [MaxLength(60)]
    public string matricula { get; set; }

    [MaxLength(60)]
    public string nivel_de_acesso { get; set; }

    [Required]
    [MaxLength(50)]
    public string email { get; set; }

    [Required]
    public int id_filial { get; set; }

    [Required]
    [MaxLength(25)]
    public string celular { get; set; }

    [Required]
    public DateTime data_nascimento { get; set; }

    [Required]
    public DateTime data_admissão { get; set; }

    [Required]
    public bool status { get; set; } = false;
    
    public string senhaHash { get; set; }
    public string senhaSalt { get; set; }

    // public string RefreshToken { get; set; } = string.Empty;
    // public DateTime TokenCreated { get; set; }
    // public DateTime TokenExpires { get; set; }


}