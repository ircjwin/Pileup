using Piles.Data;
using Piles.Models;

namespace Piles.Mappers
{
    public static class RuminationMapper
    {
        public static RuminationEntity ToRuminationEntity(Rumination rumination)
        {
            return new RuminationEntity()
            {
                Description = rumination.Description,
            };
        }

        public static Rumination ToRumination(RuminationEntity ruminationEntity)
        {
            return new Rumination(ruminationEntity.Description);
        }
    }
}
