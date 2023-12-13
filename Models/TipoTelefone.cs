using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oganrielapi.Models;

public class TipoTelefone
{
    public TipoTelefone()
    {
        ContatosFornecedores = new Collection<ContatoPessoa>();
    }

    [Key]
    public int TipoTelefoneId { get; set; }

    [Required]
    public string DescTipoTelefone { get; set; }

    [JsonIgnore]
    public ICollection<ContatoPessoa> ContatosFornecedores { get; set; }
}
