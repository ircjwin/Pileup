using System.ComponentModel.DataAnnotations;

namespace Piles.Data
{
    public class RuminationEntity
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

        public int PileId { get; set; }
        public virtual PileEntity Pile { get; set; } = null!;
    }
}
