namespace Domain.Request;

public class IsUserRequest  
{
    /// <summary>
    /// Usernme, is required
    /// </summary>
    public string username { get; set; }

    /// <summary>
    /// Fingerprint, is required
    /// </summary>

    public string fingerprint { get; set; }
}
