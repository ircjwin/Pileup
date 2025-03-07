using Piles.Models;
using System.Threading.Tasks;

namespace Piles.Commands
{
    internal class CreatePileCommand : AsyncCommandBase
    {
        private readonly Pileup _pileup;

        public CreatePileCommand(Pileup pileup)
        {
            _pileup = pileup;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _pileup.AddPile();
        }
    }
}
