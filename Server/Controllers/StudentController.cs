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
                var result = await _studentRepository.DeleteStudent(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UploadStudentImage")]
        public ActionResult<UploadResult> UploadStudentImage(IFormFile file)
        {
            var UploadResult = new UploadResult();
            string fileName = file.FileName;
            var path = Path.Combine(_env.ContentRootPath, "images", fileName);

            using FileStream fs = new(path, FileMode.Create);
            file.CopyTo(fs);

            UploadResult.UploadFilePath = Path.Combine("images", fileName);

            return Ok(UploadResult);
        }

        [HttpPost]
        [Route("DeleteStudentImage")]
        public ActionResult<UploadResult> DeleteStudentImage([FromBody] string fileName)
        {
            var UploadResult = new UploadResult();

            var path = Path.Combine(_env.ContentRootPath, fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            UploadResult.UploadFilePath = "";

            return Ok(UploadResult);
        }
    }
}
