using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ganrielapi.Data;
using ganrielapi.Models;
using System.ComponentModel.DataAnnotations;

namespace ganrielapi.Controllers;

[ApiController]
[Route("[Controller]")]
public class EditoraController : ControllerBase
{
    private readonly BDContext _context;
    public EditoraController(BDContext context)
    {
        _context = context;
    }

    [HttpGet("ListEd")]
    public async Task<ActionResult> ListarEditora()
    {
        try
        {
            ICollection<Editora> editoras = await _context.Editoras.AsNoTracking().ToListAsync();

            if (editoras is null)
                return NotFound("Não foi possível exibir as editoras, " +
                    "por favor verifique se existe alguma editora cadastrada.");

            return Ok(editoras);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListEdLi")]
    public async Task<ActionResult> ListarEditoraLivro()
    {
        try
        {
            List<Editora> editoraLivro = await _context.Editoras
                                                .Include(li => li.Livros).ToListAsync();

            return Ok(editoraLivro);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> InserirEditora(Editora editora)
    {
        try
        {
            Editora editoraCnpj = await _context.Editoras
                .FirstOrDefaultAsync(edi => edi.CnpjEditora == editora.CnpjEditora);

            if (editoraCnpj != null)
                throw new Exception("Cnpj informado já está cadastrado no sistema.");

            if (editora.NomeEditora.Length <= 5)
                throw new Exception("Nome da editora inválido.");
            if (editora.CnpjEditora.Length != 14)
                throw new Exception("Cnpj inválido.");

            await _context.Editoras.AddAsync(editora);
            await _context.SaveChangesAsync();

            return Ok("Editora cadastrada com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> RemoverEditora(int id)
    {
        try
        {
            Editora editora = await _context.Editoras.FirstOrDefaultAsync(edi => edi.EditoraId == id);
            Livro livroEditora = await _context.Livros.FirstOrDefaultAsync(le => le.EditoraId == editora.EditoraId);

            if (livroEditora != null)
                throw new Exception($"Não foi possivel excluir a editora '{editora.NomeEditora}'" +
                    $", pois o livro '{livroEditora.NomeLivro}' está associado a essa editora, " +
                    $"por favor mude a editora desse livro para continuar.");

            if (editora is null)
                throw new Exception("Não foi possível encontrar um editora com os dados informados.");

            _context.Editoras.Remove(editora);
            await _context.SaveChangesAsync();

            return Ok($"Editora '{editora.NomeEditora}' removida com sucesso!");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> ConsultarEditora(int id)
    {
        try
        {
            Editora editora = await _context.Editoras.AsNoTracking()
                .Include(li => li.Livros).FirstOrDefaultAsync(ed => ed.EditoraId == id);

            if (editora is null)
                return NotFound("Editora não encontrada.");

            return Ok(editora);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
