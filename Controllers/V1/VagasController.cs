using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuturoDoTrabalho.API.Data;
using FuturoDoTrabalho.API.Models;
using FuturoDoTrabalho.API.DTOs;

namespace FuturoDoTrabalho.API.Controllers.V1;

/// <summary>
/// Controller para gerenciar vagas de emprego (versão 1 da API).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class VagasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VagasController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todas as vagas cadastradas, incluindo seus candidatos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Vaga>>> GetVagas()
    {
        var vagas = await _context.Vagas
            .Include(v => v.Candidatos)
            .ToListAsync();
        
        return Ok(vagas);
    }

    /// <summary>
    /// Retorna uma vaga específica pelo ID, incluindo seus candidatos.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vaga>> GetVaga(int id)
    {
        var vaga = await _context.Vagas
            .Include(v => v.Candidatos)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vaga == null)
        {
            return NotFound(new { mensagem = $"Vaga com ID {id} não encontrada." });
        }

        return Ok(vaga);
    }

    /// <summary>
    /// Cria uma nova vaga de emprego.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Vaga>> PostVaga(CriarVagaDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Titulo) || 
            string.IsNullOrWhiteSpace(dto.Descricao) || 
            string.IsNullOrWhiteSpace(dto.Area))
        {
            return BadRequest(new { mensagem = "Título, Descrição e Área são obrigatórios." });
        }

        var vaga = new Vaga
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Area = dto.Area,
            DataPublicacao = DateTime.UtcNow
        };

        _context.Vagas.Add(vaga);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVaga), new { id = vaga.Id }, vaga);
    }

    /// <summary>
    /// Atualiza os dados de uma vaga existente.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutVaga(int id, AtualizarVagaDTO dto)
    {
        var vaga = await _context.Vagas.FindAsync(id);

        if (vaga == null)
        {
            return NotFound(new { mensagem = $"Vaga com ID {id} não encontrada." });
        }

        // Atualiza apenas os campos fornecidos
        if (!string.IsNullOrWhiteSpace(dto.Titulo))
            vaga.Titulo = dto.Titulo;
        
        if (!string.IsNullOrWhiteSpace(dto.Descricao))
            vaga.Descricao = dto.Descricao;
        
        if (!string.IsNullOrWhiteSpace(dto.Area))
            vaga.Area = dto.Area;

        await _context.SaveChangesAsync();

        return Ok(vaga);
    }

    /// <summary>
    /// Remove uma vaga do sistema. Candidatos associados também serão removidos (cascade).
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVaga(int id)
    {
        var vaga = await _context.Vagas.FindAsync(id);
        
        if (vaga == null)
        {
            return NotFound(new { mensagem = $"Vaga com ID {id} não encontrada." });
        }

        _context.Vagas.Remove(vaga);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

