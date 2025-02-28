using Piles.Data;
using Piles.Models;

namespace Piles.Mapper
{
    public static class PileMapper
    {
        public static PileEntity ToPileEntity(Pile pile)
        {
            return new PileEntity()
            {
                Justification = pile.Justification,
            };
        }
    }
}
