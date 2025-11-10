namespace FuturoDoTrabalho.API.DTOs;

/// <summary>
/// DTO para criação de usuário.
/// </summary>
public class CriarUsuarioDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
}

/// <summary>
/// DTO para atualização de usuário.
/// </summary>
public class AtualizarUsuarioDTO
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Tipo { get; set; }
}

