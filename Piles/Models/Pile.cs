using Microsoft.Extensions.FileProviders;
using Piles.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.Models
{
    public class Pile
    {
        public string Justification { get; set; }

        // TODO: Boolean that determines if Pile tab is visible on View
        public bool IsVisible { get; set; }

        public IEnumerable<Rumination> Ruminations { get; set; }

        private readonly IRuminationProvider _ruminationProvider;

        private readonly IRuminationCreator _ruminationCreator;

        public Pile(string justification, IEnumerable<Rumination> ruminations, IRuminationProvider ruminationProvider = null, IRuminationCreator ruminationCreator = null)
        {
            Justification = justification;
            Ruminations = ruminations;
            _ruminationProvider = ruminationProvider;
            _ruminationCreator = ruminationCreator;
        }

        public async Task<IEnumerable<Rumination>> GetAllRuminations()
        {
            return await _ruminationProvider.GetAllRuminations();
        }

        public void AddPile()
        {
            Rumination rumination = new Rumination("New Task");
            _ruminationCreator.CreateRumination(rumination, this);
        }
    }
}
