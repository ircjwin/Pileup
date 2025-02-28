using Piles.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Stores
{
    public class PilesStore
    {
        private readonly Pileup _pileup;

        private readonly List<Pile> _piles;
        public IEnumerable<Pile> Piles => _piles;

        private Lazy<Task> _initializeLazy;

        public PilesStore(Pileup pileup)
        {
            _pileup = pileup;
            _initializeLazy = new Lazy<Task>(Initialize);

            _piles = new List<Pile>();
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task MakePile()
        {
            await _pileup.AddPile();
            await this.Load();
        }

        private async Task Initialize()
        {
            IEnumerable<Pile> piles = await _pileup.GetAllPiles();

            _piles.Clear();
            _piles.AddRange(piles);

            if (_piles.Count == 0)
            {
                IEnumerable<Rumination> ruminations = new List<Rumination>();
                Pile pile = new Pile("General", ruminations);
                _piles.Add(pile);
            }
        }
    }
}
