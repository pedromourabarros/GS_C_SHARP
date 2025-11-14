namespace FuturoDoTrabalho.API.Models;

/// <summary>
/// Representa uma vaga de emprego para profissões digitais do futuro.
/// </summary>
public class Vaga
{
    public int Id { get; set; }
    
    /// <summary>
    /// Título da vaga (ex: "Desenvolvedor Full Stack", "Especialista em IA").
    /// </summary>
    public string Titulo { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição detalhada da vaga, requisitos e responsabilidades.
    /// </summary>
    public string Descricao { get; set; } = string.Empty;
    
    /// <summary>
    /// Área de atuação (ex: "Tecnologia", "Marketing Digital", "Data Science").
    /// </summary>
    public string Area { get; set; } = string.Empty;
    
    /// <summary>
    /// Data em que a vaga foi publicada no sistema.
    /// </summary>
    public DateTime DataPublicacao { get; set; }
    
    /// <summary>
    /// Lista de candidatos que se inscreveram nesta vaga.
    /// </summary>
    public List<Candidato> Candidatos { get; set; } = new();
}

