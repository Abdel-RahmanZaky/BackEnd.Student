using Loay.StudentTask.Core;
using Loay.StudentTask.Core.Models;
using Loay.StudentTask.DTOs;
using Loay.StudentTask.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loay.StudentTask.Controllers
{
    public class StudentSubjectsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentSubjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentSubject>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<IEnumerable<StudentSubject>>> GetAllStudentSubjects()
        {
            var data = await _unitOfWork.Repository<StudentSubject>().GetAllAsync();
            if (data is null)
                return NotFound(new ApiResponse(404)); // 404

            return Ok(data);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentSubject), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<StudentSubject>> GetStudentSubjectById(int id)
        {
            var studentSubject = await _unitOfWork.Repository<StudentSubject>().GetAsync(id);
            if (studentSubject is null)
                return NotFound(new ApiResponse(404)); // 404
            return Ok(studentSubject);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateStudentSubject([FromBody] StudentSubjectDto model)
        {
            if (model.StudentId == 0 || model.SubjectId == 0)
            {
                return BadRequest(new ApiResponse(400, "StudentId and SubjectId are required."));
            }
            var studentSubject = new StudentSubject
            {
                StudentId = model.StudentId,
                SubjectId = model.SubjectId
            };
            await _unitOfWork.Repository<StudentSubject>().AddAsync(studentSubject);
            await _unitOfWork.CompleteAsync();

            return Ok(model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StudentSubjectDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult> UpdateStudentSubject(int id, UpdateStudentSubjectDto model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400));

            var existingStudentSubject = await _unitOfWork.Repository<StudentSubject>().GetAsync(id);
            if (existingStudentSubject is null)
                return NotFound(new ApiResponse(404));


            existingStudentSubject.StudentId = model.StudentId;
            existingStudentSubject.SubjectId = model.SubjectId;

            _unitOfWork.Repository<StudentSubject>().Update(existingStudentSubject);
            await _unitOfWork.CompleteAsync();
            return Ok(existingStudentSubject);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult> DeleteStudentSubject(int id)
        {
            var data = await _unitOfWork.Repository<StudentSubject>().GetAsync(id);
            if (data is null)
                return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<StudentSubject>().Delete(data);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
