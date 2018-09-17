using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IStudentService
    {
        Task<List<StudentInfo>> GetStudentInfosAsync();

        Task<StudentInfo> GetStudentInfoByIdAsync(int studentInfoId);

        Task<StudentInfo> CreateStudentInfoAsync(StudentInfo studentInfo);

        Task<bool> UpdateStudentInfoAsync(StudentInfo studentInfo);

        Task<bool> DeleteStudentInfoAsync(int studentInfoId);
    }
}
