using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ganrielapi.Models;

public class Estoque
{
    public Estoque()
    {
        LivrosEstoque = new Collection<Livro>();
    }

    [Key]
    public int EstoqueId { get; set; }

    [Required]
    public int QtdLivroEstoque { get; set; }

    [Required]
    [Column(TypeName = "varchar(60)")]
    public string NomeEstoque { get; set; }

    [Column(TypeName = "varchar(150)")]
    public string DescEstoque { get; set; }

    public ICollection<Livro> LivrosEstoque { get; set; }
}
