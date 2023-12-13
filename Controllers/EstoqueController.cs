using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ganrielapi.Data;
using ganrielapi.Models;

namespace ganrielapi.Controllers;

[ApiController]
[Route("[Controller]")]
public class EstoqueController : ControllerBase
{
    private readonly BDContext _context;
    public EstoqueController(BDContext context) { _context = context; }

    [HttpGet("ListLiEs")]
    public async Task<ActionResult> ListarLivrosEstoque()
    {
        try
        {
            List<Estoque> estoqueLivros = await _context.Estoques.Include(li => li.LivrosEstoque)
                                            .AsNoTracking().ToListAsync();

            if (estoqueLivros is null)
                return NotFound("Não foi possível realizar a exibição do estoque.");

            return Ok(estoqueLivros);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("InfoEstoque")]
    public async Task<ActionResult> ConsultarInfoEstoque()
    {
        try
        {
            Estoque informacoesEstoque = await _context.Estoques.AsNoTracking()
                                            .FirstOrDefaultAsync(es => es.EstoqueId == 1);

            if (informacoesEstoque is null)
                return NotFound("Estoque não encontrado.");

            return Ok(informacoesEstoque);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
