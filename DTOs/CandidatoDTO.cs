namespace FuturoDoTrabalho.API.DTOs;

/// <summary>
/// DTO para criação de candidato.
/// </summary>
public class CriarCandidatoDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Habilidades { get; set; } = string.Empty;
    public int VagaId { get; set; }
}

/// <summary>
/// DTO para atualização de candidato.
/// </summary>
public class AtualizarCandidatoDTO
{
    public string? Nome { get; set; }
    public string? Habilidades { get; set; }
    public int? VagaId { get; set; }
}

