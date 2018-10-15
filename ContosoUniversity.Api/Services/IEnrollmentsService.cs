using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IEnrollmentsService
    {
        Enrollment Get(int id);

        Enrollment Add(Enrollment enrollment);
    }
}
