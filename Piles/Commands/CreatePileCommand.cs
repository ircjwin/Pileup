using Piles.Models;

namespace Piles.Commands
{
    internal class CreatePileCommand : CommandBase
    {
        private readonly Pileup _pileup;

        public CreatePileCommand(Pileup pileup)
        {
            _pileup = pileup;
        }
        public override void Execute(object parameter)
        {
            _pileup.AddPile();
        }
    }
}
