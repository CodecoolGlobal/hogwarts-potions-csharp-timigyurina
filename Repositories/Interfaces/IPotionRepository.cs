﻿using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Repositories.Interfaces
{
    public interface IPotionRepository : IGenericRepository<Potion>
    {
        Task<Potion?> CreateNewAsync(Student creator, BrewingStatus brewingStatus, Recipe recipe);
        Task<IEnumerable<Potion>> GetStudentPotions(int studentId);
        Task<Potion?> GetPotionWithDetails(int id);
    }
}
