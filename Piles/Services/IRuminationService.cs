﻿using Piles.DbContexts;
using Piles.Models;
using Piles.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IRuminationService
    {
        Task<ICollection<Rumination>> GetRuminationsByPileKeyAsync(int pileOrigin, DateTime pileCreatedOn);
        Task<Rumination> GetRuminationByKeyAsync(int origin, DateTime createdOn);
        void CreateRumination(Rumination rumination, Pile pile, PilesDbContext pilesDbContext);
        void UpdateRumination(Rumination rumination, Pile pile, PilesDbContext pilesDbContext);
        void DeleteRumination(Rumination rumination, Pile pile, PilesDbContext pilesDbContext);
        void Save(ICollection<(OperationType, (Rumination, Pile))> unsavedRuminations);
    }
}
