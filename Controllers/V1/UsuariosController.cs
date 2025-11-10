using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuturoDoTrabalho.API.Data;
using FuturoDoTrabalho.API.Models;
using FuturoDoTrabalho.API.DTOs;

namespace FuturoDoTrabalho.API.Controllers.V1;

/// <summary>
/// Controller para gerenciar usuários do sistema (versão 1 da API).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class UsuariosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsuariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os usuários cadastrados.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    /// <summary>
    /// Retorna um usuário específico pelo ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return NotFound(new { mensagem = $"Usuário com ID {id} não encontrado." });
        }

        return Ok(usuario);
    }

    /// <summary>
    /// Cria um novo usuário no sistema.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Usuario>> PostUsuario(CriarUsuarioDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome) || 
            string.IsNullOrWhiteSpace(dto.Email) || 
            string.IsNullOrWhiteSpace(dto.Tipo))
        {
            return BadRequest(new { mensagem = "Nome, Email e Tipo são obrigatórios." });
        }

        // Verifica se já existe um usuário com o mesmo email
        var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
        if (emailExiste)
        {
            return BadRequest(new { mensagem = "Já existe um usuário com este email." });
        }

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Tipo = dto.Tipo
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
    }

    /// <summary>
    /// Atualiza os dados de um usuário existente.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutUsuario(int id, AtualizarUsuarioDTO dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return NotFound(new { mensagem = $"Usuário com ID {id} não encontrado." });
        }

        // Verifica se o email já está em uso por outro usuário
        if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != usuario.Email)
        {
            var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email && u.Id != id);
            if (emailExiste)
            {
                return BadRequest(new { mensagem = "Já existe um usuário com este email." });
            }
        }

        // Atualiza apenas os campos fornecidos
        if (!string.IsNullOrWhiteSpace(dto.Nome))
            usuario.Nome = dto.Nome;
        
        if (!string.IsNullOrWhiteSpace(dto.Email))
            usuario.Email = dto.Email;
        
        if (!string.IsNullOrWhiteSpace(dto.Tipo))
            usuario.Tipo = dto.Tipo;

        await _context.SaveChangesAsync();

        return Ok(usuario);
    }

    /// <summary>
    /// Remove um usuário do sistema.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        
        if (usuario == null)
        {
            return NotFound(new { mensagem = $"Usuário com ID {id} não encontrado." });
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

