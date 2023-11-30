using System.ComponentModel.DataAnnotations;

namespace ModuloAutenticacao.Api.Domain;

public class Enderecos
{

    [Required]
    [Key]
    public int Id_endereco{ get; set; }

    [Required]
    [MaxLength(100)]
    public string Pais { get; set; }

    [Required]
    public string Estado { get; set; }

    [Required]
    [MaxLength(50)]
    public string Cidade { get; set; }

    [Required]
    [MaxLength(25)]
    public string Bairro { get; set; }

    [Required]
    [MaxLength(25)]
    public string Rua { get; set; }

    [Required]
    [MaxLength(25)]
    public string Numero { get; set; }

    [Required]
    [MaxLength(25)]
    public string Cep { get; set; }
    


}





