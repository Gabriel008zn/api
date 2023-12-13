using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ganrielapi.Models;

public class StatusLivro
{
    public StatusLivro()
    {
        Livros = new Collection<Livro>();
    }

    [Key]
    public int StatusLivroId { get; set; }

    [Required]
    [Column(TypeName = "varchar(15)")]
    public string DescStatusLivroId { get; set; }

    public ICollection<Livro> Livros { get; set; }
}
