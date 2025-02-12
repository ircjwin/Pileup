using System.ComponentModel.DataAnnotations;

namespace Piles.Data
{
    public class RuminationDTO
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public PileDTO PileDTO { get; set; }
    }
}
