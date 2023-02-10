using MudBlazorUICRUDApp.Shared.Models;

namespace MudBlazorUICRUDApp.Client.Services.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<ResponseResult> CreateStudent(Student student);
        Task<ResponseResult> UpdateStudent(Student student);
        Task<ResponseResult> DeleteStudent(int id);
        Task<UploadResult> UploadStudentImage(MultipartFormDataContent content);
        Task<UploadResult> DeleteStudentImage(string fileName);
    }
}
