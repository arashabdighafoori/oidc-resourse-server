namespace Domain.Request;

public class IsUserRequest  
{
    /// <summary>
    /// Usernme, is required
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Fingerprint, is required
    /// </summary>

    public string Fingerprint { get; set; }
}
