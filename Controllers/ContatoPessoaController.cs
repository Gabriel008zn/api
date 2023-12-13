using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ganrielapi.Data;
using ganrielapi.Models;

namespace ganrielapi.Controllers;

[ApiController]
[Route("[Controller]")]
public class ContatoPessoaController : ControllerBase
{
    private readonly BDContext _context;
    public ContatoPessoaController(BDContext context) { _context = context; }

    [HttpGet]
    public async Task<ActionResult> ListarTiposTelefone()
    {   
        try
        {
            List<TipoTelefone> tipos = await _context.TiposTelefones
                .AsNoTracking()
                .ToListAsync();

            if (tipos.Count() < 1)
                return NotFound("Não foi possível encontrar um tipo de telefone.");

            return Ok(tipos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> InserirContato(ContatoPessoa contato)
    {
        try
        {
            //Metodo para verificar o Email

            if (contato.PessoaId <= 0)
                return NotFound($"Não foi possível encontrar uma pessoa com o id '{contato.PessoaId}'.");

            if (contato.NumeroTelefone.Length <= 7)
                throw new Exception("Número de telefone inválido.");

            await _context.ContatosPessoas.AddAsync(contato);
            await _context.SaveChangesAsync();

            return Ok("Contato cadastrado com sucesso!");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeletarContato(int idPessoa, int idContato)
    {
        try
        {
            ContatoPessoa contato = await _context.ContatosPessoas
                .FirstOrDefaultAsync(co => co.PessoaId == idPessoa && co.ContatoPessoaId == idContato);

            Pessoa pessoa = await _context.Pessoas.FirstOrDefaultAsync(pe => pe.PessoaId == idPessoa);

            if (contato is null)
                return NotFound($"Não foi possível apagar o contato do(a) '{pessoa.NomePessoa} " +
                    $"{pessoa.SobrenomePessoa}'");

            _context.ContatosPessoas.Remove(contato);
            await _context.SaveChangesAsync();

            return Ok($"Contato excluido com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
