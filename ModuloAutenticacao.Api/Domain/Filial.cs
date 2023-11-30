using System.ComponentModel.DataAnnotations;

namespace ModuloAutenticacao.Api.Domain;
public class Filial
{

    [Required]
    [Key]
    public int id_filial{ get; set; }

    [Required]
    [MaxLength(100)]
    public string nome { get; set; }

    [Required]
    public int id_endereco { get; set; }

    [Required]
    [MaxLength(50)]
    public string email { get; set; }

    [Required]
    [MaxLength(25)]
    public string celular { get; set; }

    [Required]
    [MaxLength(25)]
    public string telefone_fixo { get; set; }

    [Required]
    [MaxLength(25)]
    public string cnpj { get; set; }
    


}