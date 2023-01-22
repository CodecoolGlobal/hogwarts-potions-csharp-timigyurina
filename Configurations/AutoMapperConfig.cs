using AutoMapper;
using HogwartsPotions.Models.DTOs;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<Room, GetRoomDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();

            CreateMap<Student, GetStudentDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
