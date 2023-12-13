using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ganrielapi.Models;

public class EnderecoPessoa
{

    [Key]
    public int EnderecoPessoaId { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string EnderecoForn { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string NumeroEndereco { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string ComplementoEndereco { get; set; }

    [Required]
    [Column(TypeName = "varchar(40)")]
    public string BairroEndereco { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string CidadeEndereco { get; set; }

    [Required]
    [Column(TypeName = "varchar(13)")]
    public string CepEndereo { get; set; }

    [Required]
    [Column(TypeName = "char(2)")]
    public string UfEndereco { get; set; }

    [Required]
    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; }
}
