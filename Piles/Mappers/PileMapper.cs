using Piles.Data;
using Piles.Models;
using System.Collections.Generic;

namespace Piles.Mappers
{
    public static class PileMapper
    {
        public static PileEntity ToPileEntity(Pile pile)
        {
            return new PileEntity()
            {
                Title = pile.Title,
            };
        }

        public static Pile ToPile(PileEntity pileEntity)
        {
            ICollection<Rumination> ruminations = new List<Rumination>();

            foreach (RuminationEntity ruminationEntity in pileEntity.Ruminations)
            {
                ruminations.Add(RuminationMapper.ToRumination(ruminationEntity));
            }

            return new Pile(pileEntity.Title, ruminations);
        }
    }
}
