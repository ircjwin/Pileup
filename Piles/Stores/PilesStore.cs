using Piles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task MakePile(Pile pile)
        {
            await _pileup.AddPile(pile);
            _piles.Add(pile);
        }

        private async Task Initialize()
        {
            IEnumerable<Pile> piles = await _pileup.GetAllPiles();

            _piles.Clear();
            _piles.AddRange(piles);
        }
    }
}
