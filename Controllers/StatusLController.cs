using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ganrielapi.Data;
using ganrielapi.Models;

namespace ganrielapi.Controllers;

[Route("[controller]")]
[ApiController]
public class StatusLController : ControllerBase
{
    private readonly BDContext _context;

    public StatusLController(BDContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> ListarStatus()
    {
        try
        {
            var lista = await _context.StatusLivros
                         .AsNoTracking().ToListAsync();

            return Ok(lista);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> DefaultStatus(int idStatusAntigo, int idStatusNovo)
    {
        int contQtdLivro = 0;

        try
        {
            StatusLivro status = _context.StatusLivros.FirstOrDefault(sta => sta.StatusLivroId == idStatusAntigo);

            if (status is null)
                throw new Exception("Não foi possível encontrar um status com o Id informado.");

            while (true)
            {
                Livro liStatus = _context.Livros.FirstOrDefault(li => li.StatusLivroId == idStatusAntigo);

                if (liStatus is null)
                    return Ok($"{contQtdLivro} livros atualizados com sucesso!");

                liStatus.StatusLivroId = idStatusNovo;

                _context.Livros.Update(liStatus);
                await _context.SaveChangesAsync();

                contQtdLivro++;
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
