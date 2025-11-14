namespace FuturoDoTrabalho.API.Models;

/// <summary>
/// Representa um candidato que se inscreveu em uma vaga.
/// </summary>
public class Candidato
{
    public int Id { get; set; }
    
    /// <summary>
    /// Nome completo do candidato.
    /// </summary>
    public string Nome { get; set; } = string.Empty;
    
    /// <summary>
    /// Habilidades técnicas e competências do candidato (ex: "C#, Python, SQL").
    /// </summary>
    public string Habilidades { get; set; } = string.Empty;
    
    /// <summary>
    /// ID da vaga em que o candidato se inscreveu.
    /// </summary>
    public int VagaId { get; set; }
    
    /// <summary>
    /// Referência à vaga relacionada (navegação).
    /// </summary>
    public Vaga? Vaga { get; set; }
}

