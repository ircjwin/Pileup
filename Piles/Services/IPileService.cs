using Piles.DbContexts;
using Piles.Models;
using Piles.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Services
{
    public interface IPileService
    {
        Task<IList<Pile>> GetAllPilesAsync();
        Task<Pile> GetPileByKeyAsync(int origin, DateTime createdOn);
        void CreatePile(Pile pile, PilesDbContext pilesDbContext);
        void UpdatePile(Pile pile, PilesDbContext pilesDbContext);
        void DeletePile(Pile pile, PilesDbContext pilesDbContext);
        void Save(ICollection<(OperationType, Pile)> unsavedPiles);
    }
}
