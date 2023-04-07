namespace Shared.Model
{
    public class ErrorResponse
    {
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public IReadOnlyDictionary<string, string> Errors { get; set; }
    }
}
