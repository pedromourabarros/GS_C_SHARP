namespace FuturoDoTrabalho.API.DTOs;

/// <summary>
/// DTO para criação de vaga.
/// </summary>
public class CriarVagaDTO
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
}

/// <summary>
/// DTO para atualização de vaga.
/// </summary>
public class AtualizarVagaDTO
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public string? Area { get; set; }
}

