using AutoMapper;
using HogwartsPotions.Data;
using HogwartsPotions.Models.DTOs.Room;
using HogwartsPotions.Models.DTOs.Student;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetStudentDTO>>> GetAllStudents()
        {
            IEnumerable<Student> students = await _unitOfWork.StudentRepository.GetAllAsync();
            IEnumerable<GetStudentDTO> studentDTOs = _mapper.Map<IEnumerable<GetStudentDTO>>(students);
            return Ok(studentDTOs);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentDTO>> GetStudentById(int id)
        {
            Student? student = await _unitOfWork.StudentRepository.GetWithDetails(id);

            if (student == null)
            {
                return NotFound("student was not found");
            }

            GetStudentDTO studentDTO = _mapper.Map<GetStudentDTO>(student);
           // studentDTO.Room = await _unitOfWork.RoomRepository.GetAsync(studentDTO.RoomId);

            return Ok(studentDTO);
        }


        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<GetStudentDTO>> AddStudent(StudentDTO studentDTO)
        {
            
            if (!CheckTypes(studentDTO))  
            {
                return BadRequest($"House or Pet type should be a valid name in {nameof(AddStudent)}");
            }
        
            Student student;
            try
            {
                student = _mapper.Map<Student>(studentDTO);
            }
            catch (Exception)
            {
                return BadRequest($"Invalid House or Pet type in {nameof(AddStudent)}");
            }

            Student? createdStudent = await _unitOfWork.StudentRepository.AddAsync(student);

            if (createdStudent == null)
            {
                return BadRequest($"There were some errors during the creation of the Student in {nameof(AddStudent)}, Student could not be created");
            }

            GetStudentDTO createdStudentDTO = _mapper.Map<GetStudentDTO>(createdStudent);

            return CreatedAtAction("GetStudentById", new { id = createdStudent.Id }, createdStudentDTO);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentById(int id, StudentDTO studentDTO)
        {
            Student? student = await _unitOfWork.StudentRepository.GetAsync(id);

            if (student == null)
            {
                // throw new NotFoundException(nameof(PutCountry), id);
                return NotFound();
            }

            if (!CheckTypes(studentDTO))
            {
                return BadRequest($"House or Pet type should be a valid name in {nameof(AddStudent)}");
            }

            try
            {
                _mapper.Map(studentDTO, student);
            }
            catch (Exception)
            {
                return BadRequest($"Invalid House or Pet type in {nameof(UpdateStudentById)}");
            }
           

            try
            {
                await _unitOfWork.StudentRepository.UpdateAsync(student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.StudentRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            GetStudentDTO updatedStudentDTO = _mapper.Map<GetStudentDTO>(student);

            return Ok(updatedStudentDTO);
        }


        [HttpGet("assign/{studentId}")]  // returns both the student and the list of available rooms of student's house -> display a form
        public async Task<ActionResult<IEnumerable<GetRoomDTO>>> AssignRoom(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            Student? student = await _unitOfWork.StudentRepository.GetAsync(studentId);

            if (student == null)
            {
                // throw new NotFoundException(nameof(PutCountry), id);
                return NotFound();
            }

            IEnumerable<Room> availableRoomsWithHouse = await _unitOfWork.RoomRepository.GetAvailableOfHouse(student.HouseType);

            IEnumerable<GetRoomDTO> availableRoomsWithHouseDTOs = _mapper.Map<IEnumerable<GetRoomDTO>> (availableRoomsWithHouse);

            StudentWithPossibleRooms studentWithPossibleHouses = new()
            {
                Student = _mapper.Map<GetStudentDTO>(student),
                PossibleRooms = availableRoomsWithHouseDTOs.ToHashSet()
            };

            return Ok(studentWithPossibleHouses);
        }


        // PUT: api/students/assign/5
        [HttpPut("assign/{id}")]
        public async Task<IActionResult> AssignRoom(int id, StudentAssignDTO studentAssignDTO)
        {
            Student? studentToBeAssignedTo = await _unitOfWork.StudentRepository.GetAsync(id);

            if (studentToBeAssignedTo == null)
            {
                return NotFound($"No student with the id of {id} was found");
            }

            Room? roomToBeAssigned = await _unitOfWork.RoomRepository.GetAsync(studentAssignDTO.RoomId);
            if (roomToBeAssigned == null)
            {
                return NotFound($"No room with the id of {studentAssignDTO.RoomId} was found");
            }

            if (studentToBeAssignedTo.HouseType != roomToBeAssigned.House)
            {
                return BadRequest("The Houses of student and Room must be the same");
            }

            if (roomToBeAssigned.Residents.Count >= roomToBeAssigned.Capacity)
            {
                return BadRequest("The Room's number of Students has reached its maximum Capacity");
            }

            Student? assignedStudent;
            try
            {
                assignedStudent = await _unitOfWork.StudentRepository.AssignRoomTo(id, studentAssignDTO.RoomId);
            }
            catch (Exception exc)
            {
                return BadRequest($"{exc.Message} (in {nameof(AssignRoom)})");
            }

            StudentDTO assignedStudentDTO = _mapper.Map<StudentDTO>(assignedStudent);
            return Ok(assignedStudentDTO);


        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            Student? student = await _unitOfWork.StudentRepository.GetAsync(id);

            if (student == null)
            {
                // throw new NotFoundException(nameof(PutCountry), id);
                return NotFound();
            }

            await _unitOfWork.StudentRepository.DeleteAsync(id);

            return NoContent();
        }


        private bool CheckTypes(StudentDTO studentDTO)
        {
            if (int.TryParse(studentDTO.HouseType, out var houseType))
            {
                return false;
            }
            if (int.TryParse(studentDTO.PetType, out var petType))
            {
                return false;
            }

            return true;
        }

    }
}
