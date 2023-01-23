using AutoMapper;
using HogwartsPotions.Models.DTOs.RoomDTOs;
using HogwartsPotions.Models.DTOs.StudentDTOs;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<Room, GetRoomDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<Room, RoomDTOWithId>().ReverseMap();

            CreateMap<Student, GetStudentDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, StudentDTOWithId>().ReverseMap();
        }
    }
}
