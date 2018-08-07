namespace ContosoUniversity.Api.Models
{
    public interface IApiResponse
    {
        bool IsSuccess { get; set; }

        string Message { get; set; }
    }
}
