using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuturoDoTrabalho.API.Data;
using FuturoDoTrabalho.API.Models;
using FuturoDoTrabalho.API.DTOs;

namespace FuturoDoTrabalho.API.Controllers.V1;

/// <summary>
/// Controller para gerenciar candidatos que se inscrevem em vagas (versão 1 da API).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CandidatosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CandidatosController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os candidatos cadastrados, incluindo informações da vaga.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Candidato>>> GetCandidatos()
    {
        var candidatos = await _context.Candidatos
            .Include(c => c.Vaga)
            .ToListAsync();
        
        return Ok(candidatos);
    }

    /// <summary>
    /// Retorna um candidato específico pelo ID, incluindo informações da vaga.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Candidato>> GetCandidato(int id)
    {
        var candidato = await _context.Candidatos
            .Include(c => c.Vaga)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (candidato == null)
        {
            return NotFound(new { mensagem = $"Candidato com ID {id} não encontrado." });
        }

        return Ok(candidato);
    }

    /// <summary>
    /// Retorna todos os candidatos de uma vaga específica.
    /// </summary>
    [HttpGet("vaga/{vagaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Candidato>>> GetCandidatosPorVaga(int vagaId)
    {
        var vagaExiste = await _context.Vagas.AnyAsync(v => v.Id == vagaId);
        if (!vagaExiste)
        {
            return NotFound(new { mensagem = $"Vaga com ID {vagaId} não encontrada." });
        }

        var candidatos = await _context.Candidatos
            .Where(c => c.VagaId == vagaId)
            .Include(c => c.Vaga)
            .ToListAsync();

        return Ok(candidatos);
    }

    /// <summary>
    /// Cria um novo candidato e o associa a uma vaga.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Candidato>> PostCandidato(CriarCandidatoDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome) || 
            string.IsNullOrWhiteSpace(dto.Habilidades))
        {
            return BadRequest(new { mensagem = "Nome e Habilidades são obrigatórios." });
        }

        // Verifica se a vaga existe
        var vaga = await _context.Vagas.FindAsync(dto.VagaId);
        if (vaga == null)
        {
            return NotFound(new { mensagem = $"Vaga com ID {dto.VagaId} não encontrada." });
        }

        var candidato = new Candidato
        {
            Nome = dto.Nome,
            Habilidades = dto.Habilidades,
            VagaId = dto.VagaId
        };

        _context.Candidatos.Add(candidato);
        await _context.SaveChangesAsync();

        // Recarrega com a vaga incluída
        await _context.Entry(candidato).Reference(c => c.Vaga).LoadAsync();

        return CreatedAtAction(nameof(GetCandidato), new { id = candidato.Id }, candidato);
    }

    /// <summary>
    /// Atualiza os dados de um candidato existente.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutCandidato(int id, AtualizarCandidatoDTO dto)
    {
        var candidato = await _context.Candidatos.FindAsync(id);

        if (candidato == null)
        {
            return NotFound(new { mensagem = $"Candidato com ID {id} não encontrado." });
        }

        // Se está tentando alterar a vaga, verifica se ela existe
        if (dto.VagaId.HasValue && dto.VagaId.Value != candidato.VagaId)
        {
            var vagaExiste = await _context.Vagas.AnyAsync(v => v.Id == dto.VagaId.Value);
            if (!vagaExiste)
            {
                return NotFound(new { mensagem = $"Vaga com ID {dto.VagaId.Value} não encontrada." });
            }
            candidato.VagaId = dto.VagaId.Value;
        }

        // Atualiza apenas os campos fornecidos
        if (!string.IsNullOrWhiteSpace(dto.Nome))
            candidato.Nome = dto.Nome;
        
        if (!string.IsNullOrWhiteSpace(dto.Habilidades))
            candidato.Habilidades = dto.Habilidades;

        await _context.SaveChangesAsync();

        // Recarrega com a vaga incluída
        await _context.Entry(candidato).Reference(c => c.Vaga).LoadAsync();

        return Ok(candidato);
    }

    /// <summary>
    /// Remove um candidato do sistema.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCandidato(int id)
    {
        var candidato = await _context.Candidatos.FindAsync(id);
        
        if (candidato == null)
        {
            return NotFound(new { mensagem = $"Candidato com ID {id} não encontrado." });
        }

        _context.Candidatos.Remove(candidato);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

