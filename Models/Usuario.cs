namespace FuturoDoTrabalho.API.Models;

/// <summary>
/// Representa um usuário do sistema, que pode ser um recrutador ou administrador.
/// </summary>
public class Usuario
{
    public int Id { get; set; }
    
    /// <summary>
    /// Nome completo do usuário.
    /// </summary>
    public string Nome { get; set; } = string.Empty;
    
    /// <summary>
    /// Email único do usuário, usado para login.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Tipo/perfil do usuário: "Recrutador", "Admin", etc.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;
}

