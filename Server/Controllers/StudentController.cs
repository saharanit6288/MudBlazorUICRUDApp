using Microsoft.AspNetCore.Mvc;
using MudBlazorUICRUDApp.Server.Repository.Interface;
using MudBlazorUICRUDApp.Shared.Models;

namespace MudBlazorUICRUDApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _env;

        public StudentController(IStudentRepository studentRepository, IWebHostEnvironment env)
        {
            _env = env;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                var result = await _studentRepository.GetStudents();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetStudent/{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await _studentRepository.GetStudent(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest("No Input");
                }
                // Get the offset from current time in UTC time
                DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                // Get the unix timestamp in seconds
                string unixTime = dto.ToUnixTimeSeconds().ToString();

                //Upload File
                if (!string.IsNullOrEmpty(student.FileName))
                {
                    student.FileName = unixTime + "_" + student.FileName;
                    //If we want to create wwwroot folder in Server project
                    if (string.IsNullOrWhiteSpace(_env.WebRootPath))
                    {
                        _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    }
                    var path = Path.Combine(_env.WebRootPath, "images", student.FileName);
                    var fs = System.IO.File.Create(path);
                    fs.Write(student.FileContent, 0, student.FileContent.Length);
                    fs.Close();

                    student.PhotoPath = Path.Combine("images", student.FileName);
                }

                var result = await _studentRepository.AddStudent(student);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest("No Input");
                }
                // Get the offset from current time in UTC time
                DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                // Get the unix timestamp in seconds
                string unixTime = dto.ToUnixTimeSeconds().ToString();

                var stu = await _studentRepository.GetStudent(student.StudentID);

                //Upload File
                if (!string.IsNullOrEmpty(student.FileName))
                {
                    //If we want to create wwwroot folder in Server project
                    if (string.IsNullOrWhiteSpace(_env.WebRootPath))
                    {
                        _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    }

                    //Delete File
                    if (!string.IsNullOrEmpty(stu.PhotoPath))
                    {
                        var pathDel = Path.Combine(_env.WebRootPath, stu.PhotoPath);

                        if (System.IO.File.Exists(pathDel))
                        {
                            System.IO.File.Delete(pathDel);
                        }
                    }

                    student.FileName = unixTime + "_" + student.FileName;
                    var path = Path.Combine(_env.WebRootPath, "images", student.FileName);
                    var fs = System.IO.File.Create(path);
                    fs.Write(student.FileContent, 0, student.FileContent.Length);
                    fs.Close();

                    student.PhotoPath = Path.Combine("images", student.FileName);
                }

                var result = await _studentRepository.UpdateStudent(student);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteStudent/{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var stu = await _studentRepository.GetStudent(id);

                //Delete File
                if(stu != null && !string.IsNullOrEmpty(stu.PhotoPath))
                {
                    //If we want to create wwwroot folder in Server project
                    if (string.IsNullOrWhiteSpace(_env.WebRootPath))
                    {
                        _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    }
                    var path = Path.Combine(_env.WebRootPath, stu.PhotoPath);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                var result = await _studentRepository.DeleteStudent(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //[Route("UploadStudentImage")]
        //public ActionResult<UploadResult> UploadStudentImage(IFormFile file)
        //{
        //    var UploadResult = new UploadResult();
        //    string fileName = file.FileName;
        //    var path = Path.Combine(_env.ContentRootPath, "images", fileName);

        //    using FileStream fs = new(path, FileMode.Create);
        //    file.CopyTo(fs);

        //    UploadResult.UploadFilePath = Path.Combine("images", fileName);

        //    return Ok(UploadResult);
        //}

        //[HttpPost]
        //[Route("DeleteStudentImage")]
        //public ActionResult<UploadResult> DeleteStudentImage([FromBody] string fileName)
        //{
        //    var UploadResult = new UploadResult();

        //    var path = Path.Combine(_env.ContentRootPath, fileName);

        //    if (System.IO.File.Exists(path))
        //    {
        //        System.IO.File.Delete(path);
        //    }

        //    UploadResult.UploadFilePath = "";

        //    return Ok(UploadResult);
        //}
    }
}
