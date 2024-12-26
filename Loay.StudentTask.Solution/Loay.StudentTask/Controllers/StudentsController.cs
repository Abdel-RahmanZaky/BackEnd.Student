using Loay.StudentTask.Core;
using Loay.StudentTask.Core.Models;
using Loay.StudentTask.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loay.StudentTask.Controllers
{
    public class StudentsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudent()
        {
            var students = await _unitOfWork.Repository<Student>().GetAllAsync();
            if (students is null)
                return NotFound(new ApiResponse(404)); // 404

            return Ok(students);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<Student>> GetStudent(int studentId)
        {
            var student = await _unitOfWork.Repository<Student>().GetAsync(studentId);
            if (student is null)
                return NotFound(new ApiResponse(404)); // 404
            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateStudent(Student student)
        {
            if (student is null)
                return BadRequest(new ApiResponse(400));

            await _unitOfWork.Repository<Student>().AddAsync(student);

            await _unitOfWork.CompleteAsync();

            return Ok(student);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest(new ApiResponse(400));

            var currentStudent = await _unitOfWork.Repository<Student>().GetAsync(id);
            if (currentStudent is null)
                return NotFound(new ApiResponse(404));

            currentStudent.Name = student.Name;
            currentStudent.Age = student.Age;

            _unitOfWork.Repository<Student>().Update(currentStudent);
            await _unitOfWork.CompleteAsync();
            return Ok(currentStudent);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _unitOfWork.Repository<Student>().GetAsync(id);
            if(student is null)
                return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Student>().Delete(student);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
