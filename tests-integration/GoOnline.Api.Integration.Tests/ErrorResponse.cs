namespace GoOnline.Api.Integration.Tests;

public class ErrorResponse
{
    public bool Success { get; set; }
    public int Status { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}
