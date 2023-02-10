using Microsoft.EntityFrameworkCore;
using MudBlazorUICRUDApp.Server.Data;
using MudBlazorUICRUDApp.Server.Repository.Interface;
using MudBlazorUICRUDApp.Shared.Models;

namespace MudBlazorUICRUDApp.Server.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> AddStudent(Student student)
        {
            ResponseResult res = new ResponseResult();
            List<string> errors = new List<string>();
            try
            {
                var result = await _context.Students.AddAsync(student);
                var data = await _context.SaveChangesAsync();

                if(data == 1)
                    res.Successful = true;

                return res;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                res.Successful = false;
                res.Errors= errors;

                return res;
            }
        }

        public async Task<ResponseResult> DeleteStudent(int Id)
        {
            ResponseResult res = new ResponseResult();
            List<string> errors = new List<string>();
            try
            {
                var result = await _context.Students.Where(w => w.StudentID == Id).FirstOrDefaultAsync();

                if (result != null)
                {
                    _context.Students.Remove(result);
                    var data = await _context.SaveChangesAsync();

                    if (data == 1)
                        res.Successful = true;                    
                }
                return res;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                res.Successful = false;
                res.Errors = errors;

                return res;
            }
        }

        public async Task<Student> GetStudent(int Id)
        {
            return await _context.Students
                .Where(w => w.StudentID == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<ResponseResult> UpdateStudent(Student student)
        {
            ResponseResult res = new ResponseResult();
            List<string> errors = new List<string>();
            try
            {
                var result = await _context.Students.Where(w => w.StudentID == student.StudentID).FirstOrDefaultAsync();

                if (result != null)
                {
                    result.Name = student.Name;
                    result.Address = student.Address;
                    result.Age = student.Age;
                    result.Email = student.Email;
                    result.DateOfBrith = student.DateOfBrith;
                    result.Gender = student.Gender;
                    result.ContactNo = student.ContactNo;
                    result.PhotoPath = student.PhotoPath;

                    var data = await _context.SaveChangesAsync();

                    if (data == 1)
                        res.Successful = true;

                }

                return res;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                res.Successful = false;
                res.Errors = errors;

                return res;
            }
        }
    }
}
