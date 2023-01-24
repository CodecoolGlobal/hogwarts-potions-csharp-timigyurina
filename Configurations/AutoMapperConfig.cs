using AutoMapper;
using HogwartsPotions.Models.DTOs.IngredientDTOs;
using HogwartsPotions.Models.DTOs.PotionDTOs;
using HogwartsPotions.Models.DTOs.RecipeDTOs;
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

            CreateMap<Potion, GetPotionDTO>().ReverseMap();
            CreateMap<Potion, GetPotionDTOWithDetails>().ReverseMap();
            CreateMap<Potion, GetPotionDTOWithRecipeAndPotionIngredientDetails>().ReverseMap();

            CreateMap<Ingredient, IngredientDTOWithId>().ReverseMap();

            CreateMap<Recipe, RecipeDTOWithId>().ReverseMap();
            CreateMap<Recipe, GetRecipeDTOWithDetails>().ReverseMap();

            CreateMap<PotionIngredient, PotionIngredientDTO>().ReverseMap();
            CreateMap<Consistency, ConsistencyDTO>().ReverseMap();
        }
    }
}
