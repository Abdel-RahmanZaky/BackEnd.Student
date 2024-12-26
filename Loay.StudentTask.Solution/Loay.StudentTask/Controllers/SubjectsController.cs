using Loay.StudentTask.Core;
using Loay.StudentTask.Core.Models;
using Loay.StudentTask.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loay.StudentTask.Controllers
{
    public class SubjectsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Subject>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetAllSubjects()
        {
            var subjects = await _unitOfWork.Repository<Subject>().GetAllAsync();
            if (subjects is null)
                return NotFound(new ApiResponse(404)); // 404

            return Ok(subjects);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Subject), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            var subject = await _unitOfWork.Repository<Subject>().GetAsync(id);
            if (subject is null)
                return NotFound(new ApiResponse(404)); // 404
            return Ok(subject);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateSubject(Subject subject)
        {
            if (subject is null)
                return BadRequest(new ApiResponse(400));

            await _unitOfWork.Repository<Subject>().AddAsync(subject);

            await _unitOfWork.CompleteAsync();

            return Ok(subject);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Subject), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult> UpdateSubject(int id, Subject subject)
        {
            if (id != subject.Id)
                return BadRequest(new ApiResponse(400));

            var currentSubject = await _unitOfWork.Repository<Subject>().GetAsync(id);
            if (currentSubject is null)
                return NotFound(new ApiResponse(404));

            currentSubject.Name = subject.Name;

            _unitOfWork.Repository<Subject>().Update(currentSubject);
            await _unitOfWork.CompleteAsync();
            return Ok(currentSubject);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var subject = await _unitOfWork.Repository<Subject>().GetAsync(id);
            if (subject is null)
                return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Subject>().Delete(subject);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
