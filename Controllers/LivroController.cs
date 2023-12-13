using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ganrielapi.Data;
using ganrielapi.Models;
using System.Diagnostics.Contracts;

namespace ganrielapi.Controllers;

[ApiController]
[Route("[Controller]")]
public class LivroController : ControllerBase
{
    private readonly BDContext _context;
    public LivroController  (BDContext context) 
    { 
        _context = context; 
    }

    [HttpGet("ListLi")]
    public async Task<ActionResult> ListarLivros()
    {
        try
        {
            ICollection<Livro> livros = await _context.Livros.Take(25)
                                                .AsNoTracking().ToListAsync();
            if (livros is null)
                return NotFound("Não foi possível exibir os livros cadastrados" +
                    ", por favor, verifique se existe algum livro cadastro.");

            return Ok(livros);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("InserirLi")]
    public async Task<ActionResult> InserirLivro(Livro livro)
    {
        try
        {
            Editora livroEditora = await _context.Editoras
                .FirstOrDefaultAsync(le => le.EditoraId == livro.EditoraId);

            StatusLivro livroStatus = await _context.StatusLivros
                .FirstOrDefaultAsync(ls => ls.StatusLivroId == livro.StatusLivroId);

            if (livroEditora is null)
                return NotFound("Editora não encontrada.");
            if (livroStatus is null)
                return NotFound("Status não cadastrado.");

            if (livro.NomeLivro.Length <= 1)
                throw new Exception("Nome do livro inválido.");
            if (livro.DescLivro.Length <= 30)
                throw new Exception("Descrição do livro inválido");

            if (livro.QtdeLivro < 0)
                throw new Exception("Quantidade do livro inválida.");

            Estoque estoque = await _context.Estoques.FirstOrDefaultAsync(es => es.EstoqueId == 1);
            int QtdeAtualEstoque = estoque.QtdLivroEstoque;

            estoque.QtdLivroEstoque = QtdeAtualEstoque + livro.QtdeLivro;

            _context.Estoques.Update(estoque);
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();

            return Ok($"Livro cadastrado com sucesso, o seu id é '{livro.LivroId}'. " +
                $"\nQuantidade do estoque atualizada.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("AtEd")]
    public async Task<ActionResult> AtualizarEditora(int idLivro, int idEditora)
    {
        try
        {
            Livro livro = await _context.Livros.FirstOrDefaultAsync(li => li.LivroId == idLivro);

            if (livro is null)
                throw new Exception("Livro não encontrado.");

            Editora editora = await _context.Editoras.FirstOrDefaultAsync(edi => edi.EditoraId == idEditora);

            if (editora is null)
                throw new Exception("Editora não cadastrada.");

            livro.EditoraId = idEditora;

            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();

            return Ok($"Editora do livro '{livro.NomeLivro}' atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("AtEdMassa")]
    public async Task<ActionResult> AtualizarEditoraMassa(int idEditoraAntigo, int idEditoraNovo)
    {
        try
        {
            int contQtdLivro = 0;

            while (true)
            {
                Livro liEditora = _context.Livros.FirstOrDefault(li => li.EditoraId == idEditoraAntigo);

                if (liEditora is null && contQtdLivro >= 1)
                    return Ok($"{contQtdLivro} livros atualizados com sucesso!");
                if (liEditora is null && contQtdLivro == 1)
                    return Ok($"{contQtdLivro} livro atualizado com sucesso!");

                if (liEditora is null && contQtdLivro == 0)
                    return Ok($"Não existe um livro associado a editora com id '{idEditoraAntigo}'.");

                liEditora.EditoraId = idEditoraNovo;

                _context.Livros.Update(liEditora);
                await _context.SaveChangesAsync();

                contQtdLivro++;
            }
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("AtQtde")]
    public async Task<ActionResult> AtualizarQtdeLivro(int idLivro, int qtdeLivro)
    {
        try
        {
            Livro livro = await _context.Livros.FirstOrDefaultAsync(li => li.LivroId == idLivro);
            Estoque estoque = await _context.Estoques.FirstOrDefaultAsync(es => es.EstoqueId == 1);

            if (livro is null)
                return NotFound("Livro não encontrado.");

            int QtdeEstoqueAtual = (estoque.QtdLivroEstoque - livro.QtdeLivro) + qtdeLivro;

            livro.QtdeLivro = qtdeLivro;
            estoque.QtdLivroEstoque = QtdeEstoqueAtual;

            _context.Estoques.Update(estoque);
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();

            return Ok("Quantidade de livros atualizada com sucesso!" +
                "\nQuantidade do estoque atualizada.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> ExcluirLivro(int idLivro)
    {
        try
        {
            Livro livro = await _context.Livros.FirstOrDefaultAsync(li => li.LivroId == idLivro);

            if (livro is null)
                return NotFound("Livro não encontrado.");

            LivroPessoa livroPessoa = await _context.LivrosPessoas
            .FirstOrDefaultAsync(li => li.LivroId == idLivro && li.StatusAssociacao == 2);

            if (livroPessoa != null)
            {
                Pessoa pessoaLivro = await _context.Pessoas.FirstOrDefaultAsync(pe => pe.PessoaId == livroPessoa.PessoaId);

                throw new Exception($"Não foi possível excluir um livro" +
                     $", pois o cliente '{pessoaLivro.NomePessoa} {pessoaLivro.SobrenomePessoa}'está com associada a esse livro" +
                     $" ,por favor mude os status de associação para continuar.");
            }

            Estoque estoque = await _context.Estoques.FirstOrDefaultAsync(es => es.EstoqueId == 1);

            estoque.QtdLivroEstoque = estoque.QtdLivroEstoque - livro.QtdeLivro;

            _context.Estoques.Update(estoque);
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return Ok($"Livro '{livro.NomeLivro}' foi removido com sucesso!" +
                $"Quantidade do estoque atualizada.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("EmpreLi")]
    public async Task<ActionResult> EmprestarLivro(int idCliente, int idLivro, int qtde)
    {
        try
        {
            if (idCliente == 2)
                throw new Exception("Não será possível emprestar um livro para essa pessoa.");

            Pessoa cliente = await _context.Pessoas.FirstOrDefaultAsync(cli => cli.PessoaId == idCliente);
            Livro livro = await _context.Livros.FirstOrDefaultAsync(li => li.LivroId == idLivro);
            Estoque estoque = await _context.Estoques.FirstOrDefaultAsync(es => es.EstoqueId == 1);

            if (livro.QtdeLivro < qtde)
                throw new Exception($"A quantidade no estoque do livro '{livro.NomeLivro}' é inválida.");

            if (livro.QtdeLivro <= 0)
                throw new Exception($"O livro '{livro.NomeLivro}' estar indisponível no momento, verifique a quantidade.");

            if (livro.StatusLivroId == 3)
                throw new Exception($"O livro '{livro.NomeLivro}' estar indisponível no momento, verifique o status do livro ");

            LivroPessoa associacao = new LivroPessoa();

            associacao.LivroId = idLivro;
            associacao.PessoaId = idCliente;
            associacao.StatusAssociacao = 2;
            associacao.QtdeEmprestada = qtde;
            associacao.DataDevolucao = DateTime.Now.AddMonths(1);

            int qtdeLivro = livro.QtdeLivro - qtde;
            int QtdeEstoqueAtual = (estoque.QtdLivroEstoque - livro.QtdeLivro) + qtdeLivro;

            livro.QtdeLivro = qtdeLivro;
            estoque.QtdLivroEstoque = QtdeEstoqueAtual;

            await _context.LivrosPessoas.AddAsync(associacao);
            _context.Estoques.Update(estoque);
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();

            string mensagem = string.Format("Livro '{0}' foi emprestado para '{1} {2}' " +
                "A data de devolução é {3:dd} de {3:MMMM} de {3:yyyy}" +
                "\nQuantidade do estoque atualizada.",
                livro.NomeLivro, cliente.NomePessoa, cliente.SobrenomePessoa, associacao.DataDevolucao);

            return Ok(mensagem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("DevLi")]
    public async Task<ActionResult> DevolverLivro(int idAssociacao)
    {
        try
        {
            LivroPessoa livroPessoa = await _context.LivrosPessoas.FirstOrDefaultAsync(lp => lp.LivroPessoaId == idAssociacao);

            if (livroPessoa is null)
                return NotFound("Não foi possível encontrar.");
            if (livroPessoa.StatusAssociacao != 2)
                throw new Exception("Esse livro não pode ser devolvido.");

            livroPessoa.StatusAssociacao = 1;

            Livro livro = await _context.Livros.FirstOrDefaultAsync(li => li.LivroId == livroPessoa.LivroId);
            Estoque estoque = await _context.Estoques.FirstOrDefaultAsync(es => es.EstoqueId == 1);

            livroPessoa.DataDevolucao = DateTime.Now;

            estoque.QtdLivroEstoque = (estoque.QtdLivroEstoque - livro.QtdeLivro) + (livro.QtdeLivro + livroPessoa.QtdeEmprestada);
            livro.QtdeLivro = livro.QtdeLivro + livroPessoa.QtdeEmprestada;

            _context.LivrosPessoas.Update(livroPessoa);
            _context.Estoques.Update(estoque);
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();

            string mensagem = string.Format($"Livro '{livro.NomeLivro}' devolvido com sucesso.\n" +
                $"Quantidade do estoque atualizada.\n" +
                $"Quantidade de livros atualizados com sucesso!");

            return Ok(mensagem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListEmpre")]
    public async Task<ActionResult> ListarLivrosEmprestados()
    {
        try
        {
            List<LivroPessoa> livroPessoa = await _context.LivrosPessoas.AsNoTracking().ToListAsync();

            if (livroPessoa is null)
                return NotFound("Lista de livros emprestados não encontrado.");

            List<LivroPessoa> livroEmpre = livroPessoa.FindAll(li => li.StatusAssociacao == 2);

            if (livroEmpre is null)
                return NotFound("No momento não existe nenhum livro emprestado.");

            return Ok(livroEmpre);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListLiDisp")]
    public async Task<ActionResult> ListarLivrosDisponiveis()
    {
        try
        {
            List<Livro> livroDispo = await _context.Livros.AsNoTracking().ToListAsync();

            if (livroDispo is null)
                throw new Exception("Não foi possível exibir a lista de livros disponíveis.");

            List<Livro> livroDisponivel = livroDispo.FindAll(li => li.QtdeLivro > 0);

            if (livroDisponivel is null)
                return NotFound("No momento não existe um livro disponível.");

            return Ok(livroDisponivel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListLiIndi")]
    public async Task<ActionResult> ListarLivrosIndisponiveis()
    {
        try
        {
            List<Livro> livroDispo = await _context.Livros.AsNoTracking().ToListAsync();

            if (livroDispo is null)
                throw new Exception("Não foi possível exibir a lista de livros disponíveis.");

            List<Livro> livroIndisponivel = livroDispo.FindAll(li => li.QtdeLivro == 0);

            if (livroIndisponivel is null)
                return NotFound("No momento não existe nenhum livro indisponivel.");

            return Ok(livroIndisponivel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
