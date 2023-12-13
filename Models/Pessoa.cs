using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ganrielapi.Models;

public class Pessoa
{
    public Pessoa()
    {
        PessoasLivros = new Collection<LivroPessoa>();
        ContatosPessoas = new Collection<ContatoPessoa>();
    }

    [Key]
    public int PessoaId { get; set; }

    [Required]
    [Column(TypeName = "varchar(60)")]
    public string NomePessoa { get; set; }

    [Required]
    [Column(TypeName = "varchar(60)")]
    public string SobrenomePessoa { get; set; }

    [Required]
    [Column(TypeName = "char(11)")]
    public string CpfPessoa { get; set; }

    [Required]
    public DateTime DtNascimento { get; set; }

    [Required]
    public int TipoPessoaId { get; set; }
    [JsonIgnore]
    public TipoPessoa TipoPessoa { get; set; }

    [JsonIgnore]
    public ICollection<LivroPessoa> PessoasLivros { get; set; }

    public ICollection<EnderecoPessoa> EnderecoPessoa { get; set; }

    public ICollection<ContatoPessoa> ContatosPessoas { get; set; }
}
