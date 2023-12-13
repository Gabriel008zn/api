using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ganrielapi.Models;

public class TipoPessoa
{
    public TipoPessoa ()
    {
        Pessoas = new Collection<Pessoa>();
    }

    [Key]
    public int TipoPessoaId { get; set; }

    [Required]
    [Column(TypeName = "varchar(15)")]
    public string DescTipoPessoa { get; set; }

    [JsonIgnore]
    public ICollection<Pessoa> Pessoas { get; set; }
}
