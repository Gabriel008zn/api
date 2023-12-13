using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ganrielapi.Ultils;
using ganrielapi.Data;
using ganrielapi.Models;

namespace ganrielapi.Controllers;

[ApiController]
[Route("[Controller]")]
public class EnderecoPessoaController : ControllerBase
{
    private readonly BDContext _context;
    public EnderecoPessoaController(BDContext context) { _context = context; }
    public Verificar ver = new Verificar();

    [HttpPost]
    public async Task<ActionResult> InserirEnderecoPessoa(EnderecoPessoa endereco)
    {
        try
        {
            if (!ver.VerificarEndereco(endereco))
                throw new Exception("Endereço inválido.");

            if (endereco.PessoaId <= 0)
                return NotFound($"Não foi possível encontrar uma pessoa com o id '{endereco.PessoaId}'.");

            await _context.EnderecosPessoas.AddAsync(endereco);
            await _context.SaveChangesAsync();

            return Ok("Endereço adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> AlterarEnderecoPessoa(int id, EnderecoPessoa endereco)
    {
        try
        {
            Pessoa pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(idFor => idFor.PessoaId == id);

            if (pessoa is null)
                return NotFound("Não foi possivel encontrar uma pessoa com esse id.");

            if (!ver.VerificarEndereco(endereco))
                throw new Exception("Endereço inválido.");

            _context.EnderecosPessoas.Update(endereco);
            await _context.SaveChangesAsync();

            string mensagemConclusao = string.Format($"Endereço da pessoa '{pessoa.NomePessoa}' " +
                $"foi atualizado com sucesso!");

            return Ok(mensagemConclusao);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
