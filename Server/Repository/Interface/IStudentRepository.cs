using MudBlazorUICRUDApp.Shared.Models;

namespace MudBlazorUICRUDApp.Server.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int Id);
        Task<ResponseResult> AddStudent(Student student);
        Task<ResponseResult> UpdateStudent(Student student);
        Task<ResponseResult> DeleteStudent(int Id);
    }
}
