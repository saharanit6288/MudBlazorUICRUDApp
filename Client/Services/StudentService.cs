using MudBlazorUICRUDApp.Client.Services.Interface;
using MudBlazorUICRUDApp.Shared.Models;
using System.Net.Http.Json;

namespace MudBlazorUICRUDApp.Client.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseResult> CreateStudent(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Student/AddStudent", student);
            return await response.Content.ReadFromJsonAsync<ResponseResult>();
        }

        public async Task<ResponseResult> DeleteStudent(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Student/DeleteStudent/" + id);
            return await response.Content.ReadFromJsonAsync<ResponseResult>();
        }

        public async Task<UploadResult> DeleteStudentImage(string fileName)
        {
            var response= await _httpClient.PostAsJsonAsync("api/Student/DeleteStudentImage", fileName);
            return await response.Content.ReadFromJsonAsync<UploadResult>();
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _httpClient.GetFromJsonAsync<Student>("api/Student/GetStudent/" + id);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _httpClient.GetFromJsonAsync<Student[]>("api/Student/GetStudents");
        }

        public async Task<ResponseResult> UpdateStudent(Student student)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Student/UpdateStudent", student);
            return await response.Content.ReadFromJsonAsync<ResponseResult>();
        }

        public async Task<UploadResult> UploadStudentImage(MultipartFormDataContent content)
        {
            var response= await _httpClient.PostAsync("api/Student/UploadStudentImage", content);
            return await response.Content.ReadFromJsonAsync<UploadResult>();
        }
    }
}
