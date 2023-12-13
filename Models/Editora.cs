
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ganrielapi.Models;

public class Editora
{
    public Editora()
    {
        Livros = new Collection<Livro>();
    }

    [Key]
    public int EditoraId { get; set; }

    [Required]
    [Column(TypeName = "varchar(60)")]
    public string NomeEditora { get; set; }

    [Column(TypeName = "varchar(150)")]
    public string DescEditora { get; set; }

    [Required]
    [Column(TypeName = "char(14)")]
    public string CnpjEditora { get; set; }

    [Required]
    public ICollection<Livro> Livros { get; set; }
}
