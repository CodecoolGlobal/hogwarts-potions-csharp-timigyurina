using AutoMapper;
using HogwartsPotions.Data;
using HogwartsPotions.Models.DTOs;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/api/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRoomDTO>>> GetAllRooms()
        {
            IEnumerable<Room> rooms = await _unitOfWork.RoomRepository.GetAllAsync();
            IEnumerable<GetRoomDTO> roomDTOs = _mapper.Map<IEnumerable<GetRoomDTO>>(rooms);
            return Ok(roomDTOs);
        }

        [HttpGet("rat-owners")]
        public async Task<ActionResult<IEnumerable<GetRoomDTO>>> GetRoomsOfRatOwners()
        {
            //IEnumerable<Room> roomsOfRatOwners = await _unitOfWork.RoomRepository.GetAllAsync(r => r.Residents.All(res => res.PetType == PetType.None || res.PetType == PetType.Rat));
            IEnumerable<Room> roomsOfRatOwners = await _unitOfWork.RoomRepository.GetRoomsOfRatOwners();

            IEnumerable<GetRoomDTO> roomDTOs = _mapper.Map<IEnumerable<GetRoomDTO>>(roomsOfRatOwners);
            return Ok(roomDTOs);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<GetRoomDTO>>> GetAvailableRooms()
        {
            IEnumerable<Room> availableRooms = await _unitOfWork.RoomRepository.GetAvailable();
            IEnumerable<GetRoomDTO> roomDTOs = _mapper.Map<IEnumerable<GetRoomDTO>>(availableRooms);
            return Ok(roomDTOs);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetRoomDTO>> GetRoomById(int id)
        {
            Room? room = await _unitOfWork.RoomRepository.GetAsync(id);

            if (room == null)
            {
                return NotFound("room was not found");
            }

            GetRoomDTO roomDTO = _mapper.Map<GetRoomDTO>(room);

            return Ok(roomDTO);
        }


        [HttpPost]
        public async Task<ActionResult<Room>> AddRoom([FromBody] RoomDTO roomDTO)
        {
            if (int.TryParse(roomDTO.House, out var houseType))  // Do not let the user put in a number as a string becuse it will be stored..
            {
                return BadRequest($"House type should be a valid House name in {nameof(AddRoom)}");
            }


            Room room;
            try
            {
                room = _mapper.Map<Room>(roomDTO);
            }
            catch (Exception)
            {
                return BadRequest($"House type does not exist in {nameof(AddRoom)}");
            }

            //await _unitOfWork.CommitAsync();  // We can either call the (I)UnitOfWork's Commit/CommitAsync method here (in the Controller), or do the savings in the (I)GenericRepository (only for the Add, Updtae and Delete methods)
            Room? createdRoom = await _unitOfWork.RoomRepository.AddAsync(room);

            if (createdRoom == null)
            {
                return BadRequest($"There were some errors during the creation of the Room in {nameof(AddRoom)}, Room could not be created");
            }

            GetRoomDTO createdRoomDTO = _mapper.Map<GetRoomDTO>(createdRoom);

            return CreatedAtAction("GetRoomById", new { id = createdRoom.Id }, createdRoomDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomById(int id, [FromBody] RoomDTO roomDTO)
        {
            Room? room = await _unitOfWork.RoomRepository.GetAsync(id);

            if (room == null)
            {
                // throw new NotFoundException(nameof(PutCountry), id);
                return NotFound();
            }

            _mapper.Map(roomDTO, room);

            try
            {
                await _unitOfWork.RoomRepository.UpdateAsync(room);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.RoomRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            GetRoomDTO updatedRoomDTO = _mapper.Map<GetRoomDTO>(room);

            return Ok(updatedRoomDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomById(int id)
        {
            Room? room = await _unitOfWork.RoomRepository.GetAsync(id);

            if (room == null)
            {
                // throw new NotFoundException(nameof(PutCountry), id);
                return NotFound();
            }

            await _unitOfWork.RoomRepository.DeleteAsync(id);

            return NoContent();
        }




        //// Would only be needed if the RoonController derived from Controller, rather than ControllerBase. More info: 
        //https://stackoverflow.com/questions/55239380/why-derive-from-controllerbase-vs-controller-for-asp-net-core-web-api
        //    https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-7.0
        ////protected override void Dispose(bool disposing)
        //{
        //    _unitOfWork.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
