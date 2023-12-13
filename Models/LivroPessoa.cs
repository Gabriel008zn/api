using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ganrielapi.Models;

public class LivroPessoa
{
    [Key]
    public int LivroPessoaId { get; set; }

    [Required]
    public int LivroId { get; set; }
    [JsonIgnore]
    public Livro Livro { get; set; }

    [Required]
    public int PessoaId { get; set; }
    [JsonIgnore]
    public Pessoa Pessoa { get; set; }

    public int StatusAssociacao { get; set; }

    public int QtdeEmprestada { get; set; }

    public DateTime DataDevolucao { get; set; }
}
