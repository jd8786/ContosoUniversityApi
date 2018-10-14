using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IEnrollmentsService
    {
        Enrollment Add(Enrollment enrollment);
    }
}
