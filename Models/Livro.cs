using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ganrielapi.Models;

public class Livro
{
    public Livro()
    {
        LivrosPessoas = new Collection<LivroPessoa>();
    }

    [Key]
    public int LivroId { get; set; }

    [Required]
    [Column(TypeName = "varchar(60)")]
    public string NomeLivro { get; set; }

    [Required]
    [MaxLength(350)]
    public string DescLivro { get; set; }

    [Required]
    public int StatusLivroId { get; set; }
    public StatusLivro StatusLivro { get; set; }

    [Required]
    public int EditoraId { get; set; }
    public Editora Editora { get; set; }

    public int QtdeLivro { get; set; }

    [JsonIgnore]
    public ICollection<LivroPessoa> LivrosPessoas { get; set; }

    [Required]
    public int EstoqueId { get; set; }
    [JsonIgnore]
    public Estoque Estoque { get; set; }
}