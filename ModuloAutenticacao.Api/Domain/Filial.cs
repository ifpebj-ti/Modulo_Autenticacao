using System.ComponentModel.DataAnnotations;

public class Filial
{

    [Required]
    [Key]
    public int Id_Filial{ get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }

    [Required]
    public string Id_endereco { get; set; }

    [Required]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required]
    [MaxLength(25)]
    public string Celular { get; set; }

    [Required]
    [MaxLength(25)]
    public string Telefone_fixo { get; set; }

    [Required]
    [MaxLength(25)]
    public string Cnpj { get; set; }
    


}